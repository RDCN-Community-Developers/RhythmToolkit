﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RhythmBase.Converters;
using RhythmBase.Global.Components;
using RhythmBase.Global.Converters;
using RhythmBase.Global.Exceptions;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.Adofai.Components;
namespace RhythmBase.RhythmDoctor.Utils
{
	/// <summary>
	/// Static class providing utility methods.
	/// </summary>
	public static class Utils
	{
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
		public static JsonSerializerSettings GetSerializer(this RDLevel rdlevel, LevelReadOrWriteSettings settings)
		{
			JsonSerializerSettings EventsSerializer = new()
			{
				ContractResolver = new RDContractResolver(),
			};
			IList<JsonConverter> converters = EventsSerializer.Converters;
			converters.Add(new FloatConverter());
			converters.Add(new PaletteColorConverter(rdlevel.ColorPalette));
			converters.Add(new ColorConverter());
			converters.Add(new ConditionalConverter());
			converters.Add(new CharacterConverter());
			converters.Add(new ConditionConverter(rdlevel.Conditionals));
			converters.Add(new TagActionConverter(rdlevel, settings));
			converters.Add(new ShowRoomsConverter(rdlevel, settings));
			converters.Add(new CustomDecorationEventConverter(rdlevel, settings));
			converters.Add(new CustomRowEventConverter(rdlevel, settings));
			converters.Add(new CustomEventConverter(rdlevel, settings));
			converters.Add(new BaseRowActionConverter<BaseRowAction>(rdlevel, settings));
			converters.Add(new BaseDecorationActionConverter<BaseDecorationAction>(rdlevel, settings));
			converters.Add(new BaseEventConverter<IBaseEvent>(rdlevel, settings));
			converters.Add(new BookmarkConverter(rdlevel.Calculator));
			converters.Add(new StringEnumConverter());
			return EventsSerializer;
		}
		/// <summary>  
		/// Gets the JSON serializer settings for the specified level read or write settings.  
		/// </summary>  
		/// <param name="settings">The settings for reading or writing the level. Defaults to a new instance of <see cref="LevelReadOrWriteSettings"/> if not provided.</param>  
		/// <returns>A <see cref="JsonSerializerSettings"/> object configured with the necessary converters and contract resolver.</returns>  
		public static JsonSerializerSettings GetSerializer(LevelReadOrWriteSettings? settings = default)
		{
			settings ??= new LevelReadOrWriteSettings();
			JsonSerializerSettings EventsSerializer = new()
			{
				ContractResolver = new RDContractResolver(),
			};
			IList<JsonConverter> converters = EventsSerializer.Converters;
			converters.Add(new FloatConverter());
			converters.Add(new PaletteColorConverter(new RDColor[21]));
			converters.Add(new ColorConverter());
			converters.Add(new ConditionalConverter());
			converters.Add(new CharacterConverter());
			converters.Add(new ConditionConverter([]));
			converters.Add(new TagActionConverter(settings));
			converters.Add(new ShowRoomsConverter(settings));
			converters.Add(new CustomDecorationEventConverter(settings));
			converters.Add(new CustomRowEventConverter(settings));
			converters.Add(new CustomEventConverter(settings));
			converters.Add(new BaseRowActionConverter<BaseRowAction>(settings));
			converters.Add(new BaseDecorationActionConverter<BaseDecorationAction>(settings));
			converters.Add(new BaseEventConverter<IBaseEvent>(settings));
			converters.Add(new BookmarkConverter());
			converters.Add(new StringEnumConverter());
			return EventsSerializer;
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
