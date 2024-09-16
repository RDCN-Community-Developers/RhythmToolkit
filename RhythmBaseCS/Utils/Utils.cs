using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RhythmBase.Assets;
using RhythmBase.Components;
using RhythmBase.Converters;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using RhythmBase.Settings;
using System.Collections.ObjectModel;

namespace RhythmBase.Utils
{

	[StandardModule]
	public static class Utils
	{
		/// <summary>
		/// Converts percentage point to pixel point with default screen size (352 * 198).
		/// </summary>

		public static PointE PercentToPixel(PointE point) => PercentToPixel(point, RDScreenSize);

		/// <summary>
		/// Converts percentage point to pixel point with specified size.
		/// </summary>
		/// <param name="size">Specified size.</param>

		public static PointE PercentToPixel(PointE point, RDSizeE size)
		{
			PointE PercentToPixel = new(point.X * size.Width / 100f, point.Y * size.Height / 100f);
			return PercentToPixel;
		}

		/// <summary>
		/// Converts pixel point to percentage point with default screen size (352 * 198).
		/// </summary>

		public static (float? X, float? Y) PixelToPercent((float X, float Y) point) => PixelToPercent(point, (352f, 198f));

		/// <summary>
		/// Converts pixel point to percentage point with specified size.
		/// </summary>
		/// <param name="size">Specified size.</param>

		public static (float? X, float? Y) PixelToPercent((float? X, float? Y) point, (float? X, float? Y) size)
		{
			(float? X, float? Y) PixelToPercent = (point.X * 100f / size.X, point.Y * 100f / size.Y);
			return PixelToPercent;
		}

		/// <summary>
		/// Converts Xs patterns to string form.
		/// </summary>
		/// <param name="list">String pattern.</param>
		/// <returns></returns>

		public static string GetPatternString(LimitedList<Patterns> list)
		{
			string @out = "";
			foreach (Patterns item in list)
			{
				switch (item)
				{
					case Patterns.None:
						@out += "-";
						break;
					case Patterns.X:
						@out += "x";
						break;
					case Patterns.Up:
						@out += "u";
						break;
					case Patterns.Down:
						@out += "d";
						break;
					case Patterns.Banana:
						@out += "b";
						break;
					case Patterns.Return:
						@out += "r";
						break;
				}
			}
			return @out;
		}


		public static float DegreeToRadius(float degree) => 3.1415927f * degree / 180f;


		public static float RadiusToDegree(float radius) => radius * 180f / 3.1415927f;

		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>

		public static EventType ConvertToEnum(Type type)
		{
			EventType ConvertToEnum;
			if (EventTypeToEnums == null)
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
					ConvertToEnum = EventTypeToEnums[type].Single();
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

		public static EventType ConvertToEnum<TEvent>() where TEvent : BaseEvent, new() => ConvertToEnum(typeof(TEvent));

		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>

		public static EventType[] ConvertToEnums<TEvent>() where TEvent : BaseEvent
		{
			EventType[] ConvertToEnums;
			try
			{
				ConvertToEnums = EventTypeToEnums[typeof(TEvent)];
			}
			catch (Exception ex)
			{
				throw new IllegalEventTypeException(typeof(TEvent), "This exception is not expected. Please contact the developer to handle this exception.", ex);
			}
			return ConvertToEnums;
		}

		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>

		public static Type ConvertToType(string type)
		{
			EventType result;
			Type ConvertToType;
			if (Enum.TryParse(type, out result))
			{
				ConvertToType = result.ConvertToType();
			}
			else
			{
				ConvertToType = EventType.CustomEvent.ConvertToType();
			}
			return ConvertToType;
		}

		/// <summary>
		/// Conversion between types and enumerations.
		/// </summary>

		public static Type ConvertToType(this EventType type)
		{
			Type ConvertToType;
			if (EnumToEventType == null)
			{
				Type result = Type.GetType(string.Format("{0}.{1}", typeof(BaseEvent).Namespace, type));
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
					ConvertToType = EnumToEventType[type];
				}
				catch (Exception ex)
				{
					throw new IllegalEventTypeException(Conversions.ToString((int)type), "This value does not exist in the EventType enumeration.");
				}
			}
			return ConvertToType;
		}


		public static JsonSerializerSettings GetSerializer(this RDLevel rdlevel, LevelReadOrWriteSettings settings)
		{
			JsonSerializerSettings EventsSerializer = new()
			{
				ContractResolver = new RDContractResolver()
			};
			IList<JsonConverter> converters = EventsSerializer.Converters;
			converters.Add(new AssetConverter<SpriteFile>(rdlevel));
			converters.Add(new AssetConverter<ImageFile>(rdlevel));
			converters.Add(new AssetConverter<AudioFile>(rdlevel));
			converters.Add(new AssetConverter<BuiltInAudio>(rdlevel));
			converters.Add(new AudioConverter(rdlevel.Manager));
			converters.Add(new PanelColorConverter(rdlevel.ColorPalette));
			converters.Add(new ColorConverter());
			converters.Add(new ConditionalConverter());
			converters.Add(new CharacterConverter(rdlevel, rdlevel.Assets));
			converters.Add(new ConditionConverter(rdlevel.Conditionals));
			converters.Add(new TagActionConverter(rdlevel, settings));
			converters.Add(new CustomDecorationEventConverter(rdlevel, settings));
			converters.Add(new CustomRowEventConverter(rdlevel, settings));
			converters.Add(new CustomEventConverter(rdlevel, settings));
			converters.Add(new BaseRowActionConverter<BaseRowAction>(rdlevel, settings));
			converters.Add(new BaseDecorationActionConverter<BaseDecorationAction>(rdlevel, settings));
			converters.Add(new BaseEventConverter<BaseEvent>(rdlevel, settings));
			converters.Add(new BookmarkConverter(rdlevel.Calculator));
			converters.Add(new StringEnumConverter());
			return EventsSerializer;
		}


		private static readonly ReadOnlyCollection<Type> EventTypes = (from i in typeof(BaseEvent).Assembly.GetTypes()
																	   where i.IsAssignableTo(typeof(BaseEvent))
																	   select i)
			.ToList()
			.AsReadOnly();

		/// <summary>
		/// A dictionary that records the correspondence of event types inheriting from <see cref="T:RhythmBase.Events.BaseEvent" /> to <see cref="T:RhythmBase.Events.EventType" />.
		/// </summary>

		public static readonly ReadOnlyDictionary<Type, EventType[]> EventTypeToEnums = EventTypes.ToDictionary((Type i) => i, (Type i) => (from j in EventTypes
																																			where (j == i || j.IsAssignableTo(i)) && !j.IsAbstract
																																			select j)
		.Select((Type j) => ConvertToEnum(j))
		.ToArray())
		.AsReadOnly();

		/// <summary>
		/// A dictionary that records the correspondence of <see cref="T:RhythmBase.Events.EventType" /> to event types inheriting from <see cref="T:RhythmBase.Events.BaseEvent" />.
		/// </summary>

		public static readonly ReadOnlyDictionary<EventType, Type> EnumToEventType = Enum.GetValues<EventType>().ToDictionary((EventType i) => i, (EventType i) => i.ConvertToType()).AsReadOnly();

		/// <summary>
		/// Event types that inherit from <see cref="T:RhythmBase.Events.BaseRowAction" />.
		/// </summary>

		public static readonly ReadOnlyCollection<EventType> RowTypes = ConvertToEnums<BaseRowAction>().AsReadOnly();

		/// <summary>
		/// Event types that inherit from <see cref="T:RhythmBase.Events.BaseDecorationAction" />
		/// </summary>

		public static readonly ReadOnlyCollection<EventType> DecorationTypes = ConvertToEnums<BaseDecorationAction>().AsReadOnly();


		public static readonly RDSizeNI RDScreenSize = new(352, 198);


		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForGameplay = new(new EventType[]
		{
			EventType.HideRow,
			EventType.ChangePlayersRows,
			EventType.FinishLevel,
			EventType.ShowHands,
			EventType.SetHandOwner,
			EventType.SetPlayStyle
		});


		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForEnvironment = new(new EventType[]
		{
			EventType.SetTheme,
			EventType.SetBackgroundColor,
			EventType.SetForeground,
			EventType.SetSpeed,
			EventType.Flash,
			EventType.CustomFlash
		});


		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForRowFX = new(new EventType[]
		{
			EventType.HideRow,
			EventType.MoveRow,
			EventType.PlayExpression,
			EventType.TintRows
		});


		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForCameraFX = new(new EventType[]
		{
			EventType.MoveCamera,
			EventType.ShakeScreen,
			EventType.FlipScreen,
			EventType.PulseCamera
		});


		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForVisualFX = new(new EventType[]
		{
			EventType.SetVFXPreset,
			EventType.SetSpeed,
			EventType.Flash,
			EventType.CustomFlash,
			EventType.BassDrop,
			EventType.InvertColors,
			EventType.Stutter,
			EventType.PaintHands,
			EventType.NewWindowDance
		});


		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForText = new(new EventType[]
		{
			EventType.TextExplosion,
			EventType.ShowDialogue,
			EventType.ShowStatusSign,
			EventType.FloatingText,
			EventType.AdvanceText
		});


		public static readonly ReadOnlyCollection<EventType> EventTypeEnumsForUtility = new(new EventType[]
		{
			EventType.Comment,
			EventType.TagAction,
			EventType.CallCustomMethod
		});
	}
}
