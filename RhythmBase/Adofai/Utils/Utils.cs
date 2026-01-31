using RhythmBase.Adofai.Converters;
using RhythmBase.Adofai.Events;
using System.Collections.ObjectModel;
using System.Text.Json;
namespace RhythmBase.Adofai.Utils
{
	/// <summary>
	/// Useful utils.
	/// </summary>
	public static class Utils
	{
		private static JsonSerializerOptions options;
		static Utils()
		{
			options = new()
			{
				WriteIndented = true,
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
				AllowTrailingCommas = true,
				Converters =
				{
					new LevelConverter(),
					new RDPointsConverter(),
					new TileReferenceConverter(),
					new FileReferenceConverter(),
				}
			};
		}
		/// <summary>  
		/// Represents the angle used for mid-spin calculations.  
		/// </summary>  
		public const float MidSpinAngle = 999f;
		/// <summary>
		/// Converts a given type to an EventType enumeration.
		/// </summary>
		/// <param name="type">The type to convert.</param>
		/// <returns>The corresponding EventType enumeration.</returns>
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
		/// Converts a generic type to an EventType enumeration.
		/// </summary>
		/// <typeparam name="T">The type to convert, which must inherit from ADBaseEvent and have a parameterless constructor.</typeparam>
		/// <returns>The corresponding EventType enumeration.</returns>
		public static EventType ConvertToADEnum<T>() where T : BaseEvent, new() => ADConvertToEnum(typeof(T));
		/// <summary>
		/// Converts a generic type to an array of EventType enumerations.
		/// </summary>
		/// <typeparam name="T">The type to convert, which must inherit from BaseEvent.</typeparam>
		/// <returns>An array of corresponding EventType enumerations.</returns>
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
		/// Converts a string representation of an EventType to a Type.
		/// </summary>
		/// <param name="type">The string representation of the EventType.</param>
		/// <returns>The corresponding Type.</returns>
		public static Type ADConvertToType(string type)
		{
			bool flag = Enum.TryParse(type, out EventType result);
			Type ADConvertToType;
			if (flag)
				ADConvertToType = ConvertToType(result);
			else
				ADConvertToType = ConvertToType(EventType.ForwardEvent);
			return ADConvertToType;
		}
		/// <summary>
		/// Converts an EventType enumeration to a Type.
		/// </summary>
		/// <param name="type">The EventType enumeration to convert.</param>
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
		/// <summary>
		/// Creates and configures a <see cref="JsonSerializerOptions"/> instance for serializing and deserializing JSON data.
		/// </summary>
		/// <remarks>The returned <see cref="JsonSerializerOptions"/> instance includes a <see cref="LevelConverter"/>
		/// configured with the provided <paramref name="filepath"/> and <paramref name="settings"/>. The <see
		/// cref="JsonSerializerOptions.WriteIndented"/> property is set based on the <see
		/// cref="LevelReadOrWriteSettings.Indented"/> value.</remarks>
		/// <param name="filepath">The file path associated with the JSON data. This value is used by the <see cref="LevelConverter"/> to customize
		/// serialization behavior.</param>
		/// <param name="settings">Optional settings that determine how the JSON data is read or written. If <c>null</c>, default settings are used.</param>
		/// <returns>A configured <see cref="JsonSerializerOptions"/> instance with the specified settings and a custom <see
		/// cref="LevelConverter"/> added to the converters collection.</returns>
		public static JsonSerializerOptions GetJsonSerializerOptions(string? filepath = null, LevelReadSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = new(Utils.options);
			LevelConverter levelConverter = new()
			{
				ReadSettings = settings,
				Filepath = filepath
			};
			options.Converters.Add(levelConverter);
			return options;
		}
		/// <summary>
		/// Creates and configures a <see cref="JsonSerializerOptions"/> instance for serializing and deserializing JSON data.
		/// </summary>
		/// <remarks>The returned <see cref="JsonSerializerOptions"/> instance includes a <see cref="LevelConverter"/>
		/// configured with the provided <paramref name="filepath"/> and <paramref name="settings"/>. The <see
		/// cref="JsonSerializerOptions.WriteIndented"/> property is set based on the <see
		/// cref="LevelReadOrWriteSettings.Indented"/> value.</remarks>
		/// <param name="filepath">The file path associated with the JSON data. This value is used by the <see cref="LevelConverter"/> to customize
		/// serialization behavior.</param>
		/// <param name="settings">Optional settings that determine how the JSON data is read or written. If <c>null</c>, default settings are used.</param>
		/// <returns>A configured <see cref="JsonSerializerOptions"/> instance with the specified settings and a custom <see
		/// cref="LevelConverter"/> added to the converters collection.</returns>
		public static JsonSerializerOptions GetJsonSerializerOptions(string? filepath = null, LevelWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = new(Utils.options);
			if (settings.Indented)
				options.WriteIndented = true;
			else
				options.WriteIndented = false;
			LevelConverter levelConverter = new()
			{
				WriteSettings = settings,
				Filepath = filepath
			};
			options.Converters.Add(levelConverter);
			return options;
		}
		private static readonly ReadOnlyCollection<Type> ADETypes = (from i in typeof(BaseEvent).Assembly.GetTypes()
																	 where typeof(BaseEvent).IsAssignableFrom(i)
																	 select i).ToList().AsReadOnly();
		/// <summary>
		/// A dictionary that records the correspondence of EventType to event types inheriting from ADBaseEvent.
		/// </summary>
		public static readonly ReadOnlyDictionary<Type, EventType[]> ADETypesToEnum = new(ADETypes.ToDictionary((Type i) => i, (Type i) => (from j in ADETypes
																																			where (j == i || i.IsAssignableFrom(j)) && !j.IsAbstract
																																			select j).Select((Type j) => ADConvertToEnum(j)).ToArray()));
		/// <summary>
		/// A dictionary that records the correspondence of event types inheriting from ADBaseEvent to EventType.
		/// </summary>
		public static readonly ReadOnlyDictionary<EventType, Type> ADEnumToEType = new(((EventType[])Enum.GetValues(typeof(EventType))).ToDictionary((EventType i) => i, ConvertToType));
	}
}
