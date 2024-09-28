using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using System.Collections.ObjectModel;

namespace RhythmBase.Utils
{
	public static class EventTypeUtils
	{
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static EventType ToEnum(Type type)
		{
			EventType ConvertToEnum;
			if (EventType_Enums == null)
			{
				string name = type.Name;
				EventType result;
				if (!Enum.TryParse(name, out result))
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
				catch (Exception ex)
				{
					throw new IllegalEventTypeException(type, "Multiple matching EventTypes were found. Please check if the type is an abstract class type.", new ArgumentException("Multiple matching EventTypes were found. Please check if the type is an abstract class type.", "type"));
				}
			}
			return ConvertToEnum;
		}
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static EventType ToEnum<TEvent>() where TEvent : IBaseEvent, new() => ToEnum(typeof(TEvent));
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static EventType[] ToEnums(Type type)
		{
			EventType[] ConvertToEnums;
			try
			{
				ConvertToEnums = EventType_Enums[type];
			}
			catch (Exception ex)
			{
				throw new IllegalEventTypeException(type, "This exception is not expected. Please contact the developer to handle this exception.", ex);
			}
			return ConvertToEnums;
		}
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static EventType[] ToEnums<TEvent>() where TEvent : IBaseEvent=>ToEnums(typeof(TEvent));
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static Type ToType(string type)
		{
			EventType result;
			Type ConvertToType;
			if (Enum.TryParse(type, out result))
			{
				ConvertToType = result.ToType();
			}
			else
			{
				ConvertToType = EventType.CustomEvent.ToType();
			}
			return ConvertToType;
		}
		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>
		public static Type ToType(this EventType type)
		{
			Type ConvertToType;
			if (Enum_EventType == null)
			{
				Type result = Type.GetType(string.Format("{0}.{1}", typeof(IBaseEvent).Namespace, type));
				if (result == null)
				{
					throw new RhythmBaseException(string.Format("Illegal Type: {0}.", type));
				}
				ConvertToType = result;
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

		private static readonly ReadOnlyCollection<Type> EventTypes = (from i in typeof(IBaseEvent).Assembly.GetTypes()
																	   where i.IsAssignableTo(typeof(IBaseEvent))
																	   select i)
			.ToList()
			.AsReadOnly();
		/// <summary>
		/// A dictionary that records the correspondence of event types inheriting from <see cref="T:RhythmBase.Events.IBaseEvent" /> to <see cref="T:RhythmBase.Events.EventType" />.
		/// </summary>
		private static readonly ReadOnlyDictionary<Type, EventType[]> EventType_Enums = EventTypes.ToDictionary((Type i) => i, (Type i) => (from j in EventTypes
																																			where (j == i || j.IsAssignableTo(i)) && !j.IsAbstract
																																			select j)
		.Select((Type j) => ToEnum(j))
		.ToArray())
		.AsReadOnly();
		/// <summary>
		/// A dictionary that records the correspondence of <see cref="T:RhythmBase.Events.EventType" /> to event types inheriting from <see cref="T:RhythmBase.Events.IBaseEvent" />.
		/// </summary>
		private static readonly ReadOnlyDictionary<EventType, Type> Enum_EventType = Enum.GetValues<EventType>().ToDictionary((EventType i) => i, (EventType i) => i.ToType()).AsReadOnly();
		/// <summary>
		/// Event types that inherit from <see cref="T:RhythmBase.Events.BaseRowAction" />.
		/// </summary>
		public static readonly ReadOnlyCollection<EventType> RowTypes = ToEnums<BaseRowAction>().AsReadOnly();
		/// <summary>
		/// Event types that inherit from <see cref="T:RhythmBase.Events.BaseDecorationAction" />
		/// </summary>
		public static readonly ReadOnlyCollection<EventType> DecorationTypes = ToEnums<BaseDecorationAction>().AsReadOnly();

		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForGameplay = new(
		[
			EventType.HideRow,
			EventType.ChangePlayersRows,
			EventType.FinishLevel,
			EventType.ShowHands,
			EventType.SetHandOwner,
			EventType.SetPlayStyle
		]);

		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForEnvironment = new(
		[
			EventType.SetTheme,
			EventType.SetBackgroundColor,
			EventType.SetForeground,
			EventType.SetSpeed,
			EventType.Flash,
			EventType.CustomFlash
		]);

		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForRowFX = new(
		[
			EventType.HideRow,
			EventType.MoveRow,
			EventType.PlayExpression,
			EventType.TintRows
		]);

		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForCameraFX = new(
		[
			EventType.MoveCamera,
			EventType.ShakeScreen,
			EventType.FlipScreen,
			EventType.PulseCamera
		]);

		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForVisualFX = new(
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

		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForText = new(
		[
			EventType.TextExplosion,
			EventType.ShowDialogue,
			EventType.ShowStatusSign,
			EventType.FloatingText,
			EventType.AdvanceText
		]);

		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForUtility = new(
		[
			EventType.Comment,
			EventType.TagAction,
			EventType.CallCustomMethod
		]);
	}
}
