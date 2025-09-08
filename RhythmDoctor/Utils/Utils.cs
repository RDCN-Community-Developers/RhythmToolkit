using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Events;
using System.Text.Json;
namespace RhythmBase.RhythmDoctor.Utils
{
	/// <summary>
	/// Static class providing utility methods.
	/// </summary>
	public static class Utils
	{
		private static JsonSerializerOptions options;
		private static LevelConverter levelConverter;
		static Utils()
		{
			levelConverter = new();
			options = new()
			{
				WriteIndented = true,
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
				AllowTrailingCommas = true,
				Converters =
				{
					new SettingsConverter(),
					new AudioConverter(),
					new PlayerTypeGroupConverter(),
					new CpuTypeGroupConverter(),
					new ConditionalConverter(),
					new PatternConverter(),
					new TabsConverter(),
					new RowConverter(),
					new DecorationConverter(),
					new Converters.Events.BaseEventConverter(),
					new FloatingTextAnchorStylesConverter(),
					new RoomConverter(),
					new SingleRoomConverter(),
					new ExpressionConverter(),
					new RDPointsConverter(),
					levelConverter,
				}
			};
		}
		/// <summary>
		/// Converts Xs patterns to string form.
		/// </summary>
		/// <param name="pattern">List of patterns.</param>
		/// <returns>String representation of patterns.</returns>
		public static string GetPatternString(Patterns[] pattern) => string.Join("",
				pattern?.Select(p => p switch
				{
					Patterns.None => "-",
					Patterns.X => "x",
					Patterns.Up => "u",
					Patterns.Down => "d",
					Patterns.Banana => "b",
					Patterns.Return => "r",
					_ => throw new ConvertingException($"Invalid pattern: {p}")
				}) ?? throw new ConvertingException($"Cannot write pattern."));
		/// <summary>
		/// Gets the JSON serializer settings for the specified level and settings.
		/// </summary>
		/// <param name="rdlevel">The level to serialize.</param>
		/// <param name="settings">The settings for reading or writing the level.</param>
		/// <returns>JSON serializer settings.</returns>
		public static JsonSerializerSettings GetSerializerOld(this RDLevel rdlevel, LevelReadOrWriteSettings settings)
		{
			JsonSerializerSettings EventsSerializer = new()
			{
				ContractResolver = new RDContractResolver(),
			};
			IList<JsonConverter> converters = EventsSerializer.Converters;
			//converters.Add(new FloatConverter());
			//converters.Add(new RhythmBase.Converters.PaletteColorConverter(rdlevel.ColorPalette));
			//converters.Add(new ColorConverter());
			//converters.Add(new ConditionalConverter());
			//converters.Add(new CharacterConverter());
			//converters.Add(new ConditionConverter(rdlevel.Conditionals));
			//converters.Add(new TagActionConverter(rdlevel, settings));
			//converters.Add(new ShowRoomsConverter(rdlevel, settings));
			//converters.Add(new CustomDecorationEventConverter(rdlevel, settings));
			//converters.Add(new CustomRowEventConverter(rdlevel, settings));
			//converters.Add(new CustomEventConverter(rdlevel, settings));
			//converters.Add(new BaseRowActionConverter<BaseRowAction>(rdlevel, settings));
			//converters.Add(new BaseDecorationActionConverter<BaseDecorationAction>(rdlevel, settings));
			converters.Add(new BaseEventConverter<IBaseEvent>(rdlevel, settings));
			//converters.Add(new ReorderSpriteConverter(rdlevel, settings));
			//converters.Add(new BookmarkConverter(rdlevel.Calculator));
			converters.Add(new StringEnumConverter());
			return EventsSerializer;
		}
		/// <summary>  
		/// Gets the JSON serializer settings for the specified level read or write settings.  
		/// </summary>  
		/// <param name="settings">The settings for reading or writing the level. Defaults to a new instance of <see cref="LevelReadOrWriteSettings"/> if not provided.</param>  
		/// <returns>A <see cref="JsonSerializerSettings"/> object configured with the necessary converters and contract resolver.</returns>  
		public static JsonSerializerSettings GetSerializerOld(LevelReadOrWriteSettings? settings = default)
		{
			settings ??= new LevelReadOrWriteSettings();
			JsonSerializerSettings EventsSerializer = new()
			{
				ContractResolver = new RDContractResolver(),
			};
			IList<JsonConverter> converters = EventsSerializer.Converters;
			//converters.Add(new FloatConverter());
			//converters.Add(new RhythmBase.Converters.PaletteColorConverter(new RDColor[21]));
			//converters.Add(new ColorConverter());
			//converters.Add(new ConditionalConverter());
			//converters.Add(new CharacterConverter());
			//converters.Add(new ConditionConverter([]));
			//converters.Add(new TagActionConverter(settings));
			//converters.Add(new ShowRoomsConverter(settings));
			//converters.Add(new CustomDecorationEventConverter(settings));
			//converters.Add(new CustomRowEventConverter(settings));
			//converters.Add(new CustomEventConverter(settings));
			//converters.Add(new BaseRowActionConverter<BaseRowAction>(settings));
			//converters.Add(new BaseDecorationActionConverter<BaseDecorationAction>(settings));
			converters.Add(new BaseEventConverter<IBaseEvent>(settings));
			//converters.Add(new BookmarkConverter());
			converters.Add(new StringEnumConverter());
			return EventsSerializer;
		}
		/// <summary>
		/// Gets the <see cref="JsonSerializerOptions"/> configured for serializing or deserializing a level.
		/// </summary>
		/// <param name="settings">
		/// The <see cref="LevelReadOrWriteSettings"/> to use for serialization options. If <c>null</c>, a new instance is used.
		/// </param>
		/// <returns>
		/// A <see cref="JsonSerializerOptions"/> instance configured with converters and indentation settings.
		/// </returns>
		public static JsonSerializerOptions GetJsonSerializerOptions(LevelReadOrWriteSettings? settings = default)
		{
			settings ??= new LevelReadOrWriteSettings();
			if (settings.Indented)
				options.WriteIndented = true;
			else
				options.WriteIndented = false;
			levelConverter.Filepath = null;
			return options;
		}

		/// <summary>
		/// Gets the <see cref="JsonSerializerOptions"/> configured for serializing or deserializing a level, and sets the file path for the converter.
		/// </summary>
		/// <param name="filepath">
		/// The file path to associate with the level converter.
		/// </param>
		/// <param name="settings">
		/// The <see cref="LevelReadOrWriteSettings"/> to use for serialization options. If <c>null</c>, a new instance is used.
		/// </param>
		/// <returns>
		/// A <see cref="JsonSerializerOptions"/> instance configured with converters, indentation settings, and file path.
		/// </returns>
		public static JsonSerializerOptions GetJsonSerializerOptions(string filepath, LevelReadOrWriteSettings? settings = default)
		{
			GetJsonSerializerOptions(settings);
			levelConverter.Filepath = filepath;
			return options;
		}
		/// <summary>
		/// The default beats per minute.
		/// </summary>
		public const float DefaultBPM = 100f;
		/// <summary>
		/// The default crotchets per bar.
		/// </summary>
		public const int DefaultCPB = 8;
		internal const string RhythmBaseGroupDataHeader = "$RhythmBase_GroupData$";
		internal const string RhythmBaseGroupEventHeader = "$RhythmBase_GroupEvent$";
	}
}
