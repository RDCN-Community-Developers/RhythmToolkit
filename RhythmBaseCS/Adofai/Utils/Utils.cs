using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RhythmBase.Adofai.Components;
using RhythmBase.Adofai.Converters;
using RhythmBase.Adofai.Events;
using RhythmBase.Converters;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using RhythmBase.Settings;
using System.Collections.ObjectModel;
namespace RhythmBase.Adofai.Utils
{
	[StandardModule]
	public static class Utils
	{
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static ADEventType ADConvertToEnum(Type type)
		{
			if (ADETypesToEnum == null)
			{
				if (type.Name.StartsWith("AD"))
				{
					string name = type.Name[2..];
					if (Enum.TryParse(name, out ADEventType result))
						return result;
				}
				throw new IllegalEventTypeException(type, "Unable to find a matching EventType.");
			}
			ADEventType ADConvertToEnum;
			try
			{
				ADConvertToEnum = ADETypesToEnum![type].Single();
			}
			catch
			{
				throw new IllegalEventTypeException(type, "Multiple matching EventTypes were found. Please check if the type is an abstract class type.", new ArgumentException("Multiple matching EventTypes were found. Please check if the type is an abstract class type.", nameof(type)));
			}
			return ADConvertToEnum;
		}
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static ADEventType ConvertToADEnum<T>() where T : ADBaseEvent, new() => ADConvertToEnum(typeof(T));
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static ADEventType[] ConvertToADEnums<T>() where T : BaseEvent
		{
			ADEventType[] ConvertToADEnums;
			try
			{
				ConvertToADEnums = ADETypesToEnum[typeof(T)];
			}
			catch
			{
				throw new IllegalEventTypeException(typeof(T), "This exception is not expected. Please contact the developer to handle this exception.");
			}
			return ConvertToADEnums;
		}
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static Type ADConvertToType(string type)
		{
			bool flag = Enum.TryParse(type, out ADEventType result);
			Type ADConvertToType;
			if (flag)
				ADConvertToType = ConvertToType(result);
			else
				ADConvertToType = ConvertToType(ADEventType.CustomEvent);
			return ADConvertToType;
		}
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static Type ConvertToType(this ADEventType type)
		{
			Type ConvertToType;
			if (ADEnumToEType == null)
				ConvertToType = Type.GetType(string.Format("{0}.AD{1}", typeof(ADBaseEvent).Namespace, type))
					?? throw new RhythmBaseException(string.Format("Illegal Type: {0}.", type));
			else
				try
				{
					ConvertToType = ADEnumToEType[type];
				}
				catch
				{
					throw new IllegalEventTypeException(Conversions.ToString((int)type), "This value does not exist in the EventType enumeration.");
				}
			return ConvertToType;
		}
		public static JsonSerializer GetSerializer(this ADLevel adlevel, LevelReadOrWriteSettings settings)
		{
			JsonSerializer AllInOneSerializer = new();
			JsonConverterCollection converters = AllInOneSerializer.Converters;
			converters.Add(new StringEnumConverter());
			converters.Add(new ColorConverter());
			converters.Add(new ADTileConverter(adlevel));
			converters.Add(new ADCustomTileEventConverter(adlevel, settings));
			converters.Add(new ADCustomEventConverter(adlevel, settings));
			converters.Add(new ADBaseTileEventConverter<ADBaseTileEvent>(adlevel, settings));
			converters.Add(new ADBaseEventConverter<ADBaseEvent>(adlevel, settings));
			return AllInOneSerializer;
		}
		private static readonly ReadOnlyCollection<Type> ADETypes = (from i in typeof(ADBaseEvent).Assembly.GetTypes()
																	 where i.IsAssignableTo(typeof(ADBaseEvent))
																	 select i).ToList().AsReadOnly();
		/// <summary>
		/// A dictionary that records the correspondence of <see cref="T:RhythmBase.Adofai.Events.ADEventType" /> to event types inheriting from <see cref="T:RhythmBase.Adofai.Events.ADBaseEvent" />.
		/// </summary>
		public static readonly ReadOnlyDictionary<Type, ADEventType[]> ADETypesToEnum = ADETypes.ToDictionary((Type i) => i, (Type i) => (from j in ADETypes
																																		  where (j == i || j.IsAssignableTo(i)) && !j.IsAbstract
																																		  select j).Select((Type j) => ADConvertToEnum(j)).ToArray()).AsReadOnly();
		/// <summary>
		/// A dictionary that records the correspondence of event types inheriting from <see cref="T:RhythmBase.Adofai.Events.ADBaseEvent" /> to <see cref="T:RhythmBase.Adofai.Events.ADEventType" />.
		/// </summary>
		public static readonly ReadOnlyDictionary<ADEventType, Type> ADEnumToEType = Enum.GetValues<ADEventType>().ToDictionary((ADEventType i) => i, ConvertToType).AsReadOnly();
	}
}
