using RhythmBase.RhythmDoctor.Events;
using System.Collections.ObjectModel;

namespace RhythmBase.RhythmDoctor.Utils
{
	/// <summary>  
	/// Utility class for converting between event types and enumerations.  
	/// </summary>  
	public static partial class EventTypeUtils
	{
		/// <summary>  
		/// Converts a type to its corresponding EventType enumeration.  
		/// </summary>  
		/// <param name="type">The type to convert.</param>  
		/// <returns>The corresponding EventType enumeration.</returns>  
		/// <exception cref="IllegalEventTypeException">Thrown when no matching EventType is found or multiple matching EventTypes are found.</exception>  
		public static EventType ToEnum(Type type)
		{
			EventType ConvertToEnum;
			if (EventType_Enums == null)
			{
				string name = type.Name;
				if (!Enum.TryParse(name, out EventType result))
				{
					throw new IllegalEventTypeException(type, "Unable to find a matching EventType.");
				}
				ConvertToEnum = result;
			}
			else
			{
				try
				{
					ConvertToEnum = EventType_Enums[type].Single();
				}
				catch (Exception)
				{
					throw new IllegalEventTypeException(type, "Multiple matching EventTypes were found. Please check if the type is an abstract class type.", new ArgumentException("Multiple matching EventTypes were found. Please check if the type is an abstract class type.", nameof(type)));
				}
			}
			return ConvertToEnum;
		}
		/// <summary>  
		/// Converts a generic event type to its corresponding EventType enumeration.  
		/// </summary>  
		/// <typeparam name="TEvent">The generic event type to convert.</typeparam>  
		/// <returns>The corresponding EventType enumeration.</returns>  
		public static EventType ToEnum<TEvent>() where TEvent : IBaseEvent, new() => ToEnum(typeof(TEvent));
		/// <summary>  
		/// Converts a type to an array of corresponding EventType enumerations.  
		/// </summary>  
		/// <param name="type">The type to convert.</param>  
		/// <returns>An array of corresponding EventType enumerations.</returns>  
		/// <exception cref="IllegalEventTypeException">Thrown when an unexpected exception occurs.</exception>  
		public static EventType[] ToEnums(Type type)
		{
			if (typeof(MacroEvent).IsAssignableFrom(type))
				return [EventType.MacroEvent];
			EventType[] ConvertToEnums;
			ConvertToEnums = EventType_Enums[type];
			return ConvertToEnums;
		}
		/// <summary>  
		/// Converts a generic event type to an array of corresponding EventType enumerations.  
		/// </summary>  
		/// <typeparam name="TEvent">The generic event type to convert.</typeparam>  
		/// <returns>An array of corresponding EventType enumerations.</returns>
		public static EventType[] ToEnums<TEvent>() where TEvent : IBaseEvent => ToEnums(typeof(TEvent));
		/// <summary>  
		/// Converts a string representation of an event type to its corresponding Type.  
		/// </summary>  
		/// <param name="type">The string representation of the event type.</param>  
		/// <returns>The corresponding Type.</returns>
		public static Type ToType(string type)
		{
			Type ConvertToType;
			if (Enum.TryParse(type, out EventType result))
			{
				ConvertToType = result.ToType();
			}
			else
			{
				ConvertToType = EventType.ForwardEvent.ToType();
			}
			return ConvertToType;
		}
		/// <summary>  
		/// Converts an EventType enumeration to its corresponding Type.  
		/// </summary>  
		/// <param name="type">The EventType enumeration to convert.</param>  
		/// <returns>The corresponding Type.</returns>  
		/// <exception cref="IllegalEventTypeException">Thrown when the value does not exist in the EventType enumeration.</exception>
		public static Type ToType(this EventType type)
		{
			Type ConvertToType;
			if (Enum_EventType == null)
			{
				return Type.GetType($"{typeof(IBaseEvent).Namespace}.{type}") ?? throw new RhythmBaseException(
					$"Illegal Type: {type}.");
			}
			else
			{
				try
				{
					ConvertToType = Enum_EventType[type];
				}
				catch
				{
					throw new IllegalEventTypeException(type.ToString(), "This value does not exist in the EventType enumeration.");
				}
			}
			return ConvertToType;
		}

		private static readonly ReadOnlyCollection<Type> EventTypes = (from i in typeof(IBaseEvent).Assembly.GetTypes().Where(i => i.Namespace == typeof(IBaseEvent).Namespace)
																	   where typeof(IBaseEvent).IsAssignableFrom(i)
																	   select i)
			.ToList()
			.AsReadOnly();
		/// <summary>  
		/// A dictionary that records the correspondence of event types inheriting from <see cref="T:RhythmBase.Events.IBaseEvent" /> to <see cref="T:RhythmBase.Events.EventType" />.  
		/// </summary>  
		private static readonly ReadOnlyDictionary<Type, EventType[]> EventType_Enums = new(EventTypes.ToDictionary((i) => i, (i) => (from j in EventTypes
																																	  where (j == i || i.IsAssignableFrom(j)) && !j.IsAbstract
																																	  select ToEnum(j))
			.ToArray()));
		internal static readonly ReadOnlyCollection<Type> MacroTypes = (from i in AppDomain.CurrentDomain.GetAssemblies().SelectMany(i => i.GetTypes())
																		where
																			typeof(MacroEvent).IsAssignableFrom(i) &&
																			i != typeof(MacroEvent) &&
																			i != typeof(MacroEvent<>)
																		select i)
			.ToList()
			.AsReadOnly();
		/// <summary>  
		/// A dictionary that records the correspondence of <see cref="T:RhythmBase.Events.EventType" /> to event types inheriting from <see cref="T:RhythmBase.Events.IBaseEvent" />.  
		/// </summary>  
		private static readonly ReadOnlyDictionary<EventType, Type> Enum_EventType = new(((EventType[])Enum.GetValues(typeof(EventType))).ToDictionary((i) => i, (i) => i.ToType()));

		/// <summary>  
		/// Event types that inherit from <see cref="T:RhythmBase.Events.BaseRowAction" />.  
		/// </summary>  
		public static readonly ReadOnlyCollection<EventType> RowTypes = new(ToEnums<BaseRowAction>());
		/// <summary>  
		/// Event types that inherit from <see cref="T:RhythmBase.Events.BaseDecorationAction" />.  
		/// </summary>  
		public static readonly ReadOnlyCollection<EventType> DecorationTypes = new(ToEnums<BaseDecorationAction>());
		/// <summary>  
		/// Custom event types.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> CustomTypes => new(
		[
			EventType.ForwardEvent,
			EventType.ForwardRowEvent,
			EventType.ForwardDecorationEvent,
			EventType.MacroEvent,
		]);
		/// <summary>  
		/// Event types for gameplay.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> EventTypeEnumsForGameplay => new(
		[
			EventType.HideRow,
			EventType.ChangePlayersRows,
			EventType.FinishLevel,
			EventType.ShowHands,
			EventType.SetHandOwner,
			EventType.SetPlayStyle
		]);
		/// <summary>  
		/// Event types for environment.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> EventTypeEnumsForEnvironment => new(
		[
			EventType.SetTheme,
			EventType.SetBackgroundColor,
			EventType.SetForeground,
			EventType.SetSpeed,
			EventType.Flash,
			EventType.CustomFlash
		]);
		/// <summary>  
		/// Event types for row effects.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> EventTypeEnumsForRowFX => new(
		[
			EventType.HideRow,
			EventType.MoveRow,
			EventType.PlayExpression,
			EventType.TintRows
		]);
		/// <summary>  
		/// Event types for camera effects.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> EventTypeEnumsForCameraFX => new(
		[
			EventType.MoveCamera,
			EventType.ShakeScreen,
			EventType.FlipScreen,
			EventType.PulseCamera
		]);
		/// <summary>  
		/// Event types for visual effects.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> EventTypeEnumsForVisualFX => new(
		[
			EventType.SetVFXPreset,
			EventType.SetSpeed,
			EventType.Flash,
			EventType.CustomFlash,
			EventType.BassDrop,
			EventType.InvertColors,
			EventType.Stutter,
			EventType.PaintHands,
			EventType.NewWindowDance
		]);
		/// <summary>  
		/// Event types for text effects.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> EventTypeEnumsForText => new(
		[
			EventType.TextExplosion,
			EventType.ShowDialogue,
			EventType.ShowStatusSign,
			EventType.FloatingText,
			EventType.AdvanceText
		]);
		/// <summary>  
		/// Event types for utility actions.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> EventTypeEnumsForUtility => new(
		[
			EventType.Comment,
			EventType.TagAction,
			EventType.CallCustomMethod
		]);

		/// <summary>  
		/// Event types that inherit from classic row actions.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> EventTypeEnumsForRowClassic => new(
		[
		   EventType.AddClassicBeat,
		   EventType.AddFreeTimeBeat,
		   EventType.PulseFreeTimeBeat,
		   EventType.SetRowXs,
		]);
		/// <summary>  
		/// Event types that inherit from oneshot row actions.  
		/// </summary>  
		public static ReadOnlyCollection<EventType> EventTypeEnumsForRowOneshot => new(
		[
		   EventType.AddOneshotBeat,
		   EventType.SetOneshotWave,
		]);
	}
}