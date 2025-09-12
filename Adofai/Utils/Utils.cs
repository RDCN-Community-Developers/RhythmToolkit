using RhythmBase.Adofai.Events;
using System.Collections.ObjectModel;
namespace RhythmBase.Adofai.Utils
{
	/// <summary>
	/// Useful utils.
	/// </summary>
	public static class Utils
	{
		/// <summary>  
		/// Represents the angle used for mid-spin calculations.  
		/// </summary>  
		public const float MidSpinAngle = 999f;
		/// <summary>
		/// Converts a given type to an ADEventType enumeration.
		/// </summary>
		/// <param name="type">The type to convert.</param>
		/// <returns>The corresponding ADEventType enumeration.</returns>
		/// <exception cref="IllegalEventTypeException">Thrown when no matching EventType is found or multiple matching EventTypes are found.</exception>
		public static EventType ADConvertToEnum(Type type)
		{
			if (ADETypesToEnum == null)
			{
				string name = type.Name;
				if (Enum.TryParse(name, out EventType result))
					return result;
				throw new IllegalEventTypeException(type, "Unable to find a matching EventType.");
			}
			EventType ADConvertToEnum;
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
		/// Converts a generic type to an ADEventType enumeration.
		/// </summary>
		/// <typeparam name="T">The type to convert, which must inherit from ADBaseEvent and have a parameterless constructor.</typeparam>
		/// <returns>The corresponding ADEventType enumeration.</returns>
		public static EventType ConvertToADEnum<T>() where T : BaseEvent, new() => ADConvertToEnum(typeof(T));
		/// <summary>
		/// Converts a generic type to an array of ADEventType enumerations.
		/// </summary>
		/// <typeparam name="T">The type to convert, which must inherit from BaseEvent.</typeparam>
		/// <returns>An array of corresponding ADEventType enumerations.</returns>
		/// <exception cref="IllegalEventTypeException">Thrown when no matching EventType is found.</exception>
		public static EventType[] ConvertToADEnums<T>() where T : RhythmBase.RhythmDoctor.Events.BaseEvent
		{
			EventType[] ConvertToADEnums;
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
		/// Converts a string representation of an ADEventType to a Type.
		/// </summary>
		/// <param name="type">The string representation of the ADEventType.</param>
		/// <returns>The corresponding Type.</returns>
		public static Type ADConvertToType(string type)
		{
			bool flag = Enum.TryParse(type, out EventType result);
			Type ADConvertToType;
			if (flag)
				ADConvertToType = ConvertToType(result);
			else
				ADConvertToType = ConvertToType(Events.EventType.CustomEvent);
			return ADConvertToType;
		}
		/// <summary>
		/// Converts an ADEventType enumeration to a Type.
		/// </summary>
		/// <param name="type">The ADEventType enumeration to convert.</param>
		/// <returns>The corresponding Type.</returns>
		/// <exception cref="RhythmBaseException">Thrown when the type is illegal.</exception>
		/// <exception cref="IllegalEventTypeException">Thrown when the value does not exist in the EventType enumeration.</exception>
		public static Type ConvertToType(this EventType type)
		{
			Type ConvertToType;
			if (ADEnumToEType == null)
				ConvertToType = Type.GetType($"{typeof(BaseEvent).Namespace}.{type}")
					?? throw new RhythmBaseException($"Illegal Type: {type}.");
			else
				try
				{
					ConvertToType = ADEnumToEType[type];
				}
				catch
				{
					throw new IllegalEventTypeException(type.ToString(), "This value does not exist in the EventType enumeration.");
				}
			return ConvertToType;
		}
		///// <summary>
		///// Gets a JsonSerializer configured with the necessary converters for the given ADLevel and settings.
		///// </summary>
		///// <param name="adlevel">The ADLevel instance.</param>
		///// <param name="settings">The LevelReadOrWriteSettings instance.</param>
		///// <returns>A configured JsonSerializer instance.</returns>
		//public static JsonSerializerSettings GetSerializer(this ADLevel adlevel, LevelReadOrWriteSettings settings)
		//{
		//	JsonSerializerSettings EventsSerializer = new()
		//	{
		//		ContractResolver = new ContractResolver()
		//	};
		//	IList<JsonConverter> converters = EventsSerializer.Converters;
		//	converters.Add(new StringEnumConverter());
		//	//converters.Add(new ColorConverter());
		//	converters.Add(new TileConverter(adlevel));
		//	converters.Add(new CustomTileEventConverter(adlevel, settings));
		//	converters.Add(new CustomEventConverter(adlevel, settings));
		//	converters.Add(new BaseTileEventConverter<BaseTileEvent>(adlevel, settings));
		//	converters.Add(new BaseEventConverter<BaseEvent>(adlevel, settings));
		//	return EventsSerializer;
		//}
		private static readonly ReadOnlyCollection<Type> ADETypes = (from i in typeof(BaseEvent).Assembly.GetTypes()
																	 where typeof(BaseEvent).IsAssignableFrom(i)
																	 select i).ToList().AsReadOnly();
		/// <summary>
		/// A dictionary that records the correspondence of ADEventType to event types inheriting from ADBaseEvent.
		/// </summary>
		public static readonly ReadOnlyDictionary<Type, EventType[]> ADETypesToEnum = new(ADETypes.ToDictionary((Type i) => i, (Type i) => (from j in ADETypes
																																			where (j == i || i.IsAssignableFrom(j)) && !j.IsAbstract
																																			select j).Select((Type j) => ADConvertToEnum(j)).ToArray()));
		/// <summary>
		/// A dictionary that records the correspondence of event types inheriting from ADBaseEvent to ADEventType.
		/// </summary>
		public static readonly ReadOnlyDictionary<EventType, Type> ADEnumToEType = new(((EventType[])Enum.GetValues(typeof(EventType))).ToDictionary((EventType i) => i, ConvertToType));
	}
}
