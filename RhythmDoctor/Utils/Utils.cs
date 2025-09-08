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
					new DialogueExchangeConverter(),
					new PlayerTypeGroupConverter(),
					new CpuTypeGroupConverter(),
					new ConditionalConverter(),
					new PatternConverter(),
					new TabsConverter(),
					new RowConverter(),
					new DecorationConverter(),
					new BaseEventConverter(),
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
			options = new(options);
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
