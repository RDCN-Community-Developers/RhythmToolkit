using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Events;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
namespace RhythmBase.RhythmDoctor.Utils
{
	/// <summary>
	/// Static class providing utility methods.
	/// </summary>
	public static class Utils
	{
		private static readonly BaseEventConverter evc = new();
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
					new AudioConverter(),
					new PaletteColorConverter(),
					new DialogueExchangeConverter(),
					new PatternConverter(),
					new TabsConverter(),
					new RDPointsConverter(),
					new RoomConverter(),
					new SingleRoomConverter(),
					new ExpressionConverter(),
					new FileReferenceConverter(),
				}
			};
		}
		/// <summary>
		/// Converts Xs patterns to string form.
		/// </summary>
		/// <param name="pattern">List of patterns.</param>
		/// <returns>String representation of patterns.</returns>
		public static string GetPatternString(Pattern[] pattern) => string.Join("",
				pattern?.Select(p => p switch
				{
					Pattern.None => "-",
					Pattern.X => "x",
					Pattern.Up => "u",
					Pattern.Down => "d",
					Pattern.Banana => "b",
					Pattern.Return => "r",
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
		public static JsonSerializerOptions GetJsonSerializerOptions(LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = new(Utils.options);
			if (settings.Indented)
				options.WriteIndented = true;
			else
				options.WriteIndented = false;
			LevelConverter levelConverter = new()
			{
				Settings = settings,
			};
			options.Converters.Add(levelConverter);
			return options;
		}

		/// <summary>
		/// Gets the <see cref="JsonSerializerOptions"/> configured for serializing or deserializing a level, and sets the file path for the converter.
		/// </summary>
		/// <param name="dirPath">
		/// The file path to associate with the level converter.
		/// </param>
		/// <param name="settings">
		/// The <see cref="LevelReadOrWriteSettings"/> to use for serialization options. If <c>null</c>, a new instance is used.
		/// </param>
		/// <returns>
		/// A <see cref="JsonSerializerOptions"/> instance configured with converters, indentation settings, and file path.
		/// </returns>
		public static JsonSerializerOptions GetJsonSerializerOptions(string dirPath, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = new(Utils.options);
			if (settings.Indented)
				options.WriteIndented = true;
			else
				options.WriteIndented = false;
			LevelConverter levelConverter = new()
			{
				Settings = settings,
				DirectoryName = dirPath,
			};
			options.Converters.Add(levelConverter);
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
		internal const string RhythmBaseMacroEventDataHeader = "$RhythmBase_MacroData$";
		internal const string RhythmBaseMacroEventHeader = "$RhythmBase_MacroEvent$";
		/// <summary>
		/// Gets a read-only collection of default expressions.
		/// </summary>
		public static ReadOnlyCollection<string> DefaultExpressions { get; } = new([
				"neutral",
				"happy",
				"barely",
				"missed",
				"prehit",
				"beep",
			]);
		/// <summary>
		/// Converts the specified <see cref="IBaseEvent"/> instance to its JSON string representation.
		/// </summary>
		/// <remarks>The JSON output can be customized using the provided <paramref name="options"/>.  If no options
		/// are specified, the default settings are applied.</remarks>
		/// <param name="ev">The <see cref="IBaseEvent"/> instance to serialize. Cannot be <see langword="null"/>.</param>
		/// <param name="options">Optional <see cref="JsonSerializerOptions"/> to configure the serialization process.  If <see langword="null"/>,
		/// default serialization options are used.</param>
		/// <returns>A JSON string representation of the <see cref="IBaseEvent"/> instance.</returns>
		public static string ToJsonString(this IBaseEvent ev, JsonSerializerOptions? options = null)
		{
			options ??= JsonSerializerOptions.Default;
			using MemoryStream stream = new MemoryStream();
			using Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = options.WriteIndented, });
			evc.Write(writer, ev, options);
			writer.Flush();
			return Encoding.UTF8.GetString(stream.ToArray());
		}
		/// <summary>
		/// Deserializes a JSON string into an instance of a type implementing <see cref="IBaseEvent"/>.
		/// </summary>
		/// <param name="json">The JSON string to deserialize. This parameter cannot be <see langword="null"/> or empty.</param>
		/// <param name="options">Optional <see cref="JsonSerializerOptions"/> to customize the deserialization process.  If not provided, the
		/// default options will be used.</param>
		/// <returns>An instance of a type implementing <see cref="IBaseEvent"/> if deserialization is successful;  otherwise, <see
		/// langword="null"/>.</returns>
		public static IBaseEvent? FromJsonString(string json, JsonSerializerOptions? options = null)
		{
			options ??= JsonSerializerOptions.Default;
			Utf8JsonReader reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
			reader.Read();
			return evc.Read(ref reader, typeof(IBaseEvent), options);
		}
		/// <summary>
		/// Bitmask flags that describe the capabilities and application targets of a VFX preset.
		/// </summary>
		/// <remarks>
		/// The values are intended to be combined using bitwise operators to express multiple attributes
		/// (for example: <c>MultiRooms | EnableIntensity</c>). The underlying storage type is <see cref="short"/>.
		/// Use bitwise checks (for example, <c>(attributes &amp; VfxAttribute.EnableIntensity) != 0</c>) to test for features.
		/// </remarks>
		public enum VfxAttribute : short
		{
			/// <summary>
			/// The preset is disabled or not available.
			/// </summary>
			Disabled = 1,
			/// <summary>
			/// The preset applies only to the top room.
			/// </summary>
			TopOnly = 0b100_0,
			/// <summary>
			/// The preset applies to a single room (non-top).
			/// </summary>
			SingleRoom = 0b001_0,
			/// <summary>
			/// The preset applies either to a single room or to the top room.
			/// </summary>
			SingleRoomOrTop = 0b101_0,
			/// <summary>
			/// The preset applies to multiple rooms.
			/// </summary>
			MultiRooms = 0b010_0,
			/// <summary>
			/// The preset applies to multiple rooms and can also affect the top room.
			/// </summary>
			MultiRoomsWithTop = 0b110_0,
			/// <summary>
			/// The preset supports an intensity parameter.
			/// </summary>
			EnableIntensity = 0b001_000_0,
			/// <summary>
			/// The preset supports X/Y parameters.
			/// </summary>
			EnableXY = 0b010_000_0,
			/// <summary>
			/// The preset supports absolute positioning or absolute mode.
			/// </summary>
			Absolute = 0b100_000_0,
			/// <summary>
			/// The preset supports absolute intensity in addition to other attributes.
			/// </summary>
			EnableAbsoluteIntensity = 0b101_000_0,
			/// <summary>
			/// The preset supports absolute X/Y coordinates.
			/// </summary>
			EnableAbsoluteXY = 0b110_000_0,
			/// <summary>
			/// The preset supports easing (smooth interpolation).
			/// </summary>
			EnableEase = 0b1_000_000_0,
			/// <summary>
			/// The preset supports a threshold parameter.
			/// </summary>
			EnableThreshold = 0b10_000_000_0,
			/// <summary>
			/// The preset supports color adjustments.
			/// </summary>
			EnableColor = 0b100_000_000_0,
			/// <summary>
			/// The preset supports a speed parameter.
			/// </summary>
			EnableSpeed = 0b1000_000_000_0,
		}
		/// <summary>
		/// Read-only mapping that associates each <see cref="VfxPreset"/> with the corresponding <see cref="VfxAttribute"/> flags.
		/// </summary>
		/// <remarks>
		/// Use this dictionary to determine what features a given preset supports (for example, whether it affects multiple rooms,
		/// supports intensity, color, easing, etc.). The values are intended to be tested with bitwise operations.
		/// </remarks>
		public static readonly ReadOnlyDictionary<VfxPreset, VfxAttribute> VfxAttributes = new(new Dictionary<VfxPreset, VfxAttribute>
		{
			[VfxPreset.Vignette] = VfxAttribute.MultiRooms,
			[VfxPreset.VignetteFlicker] = VfxAttribute.MultiRooms,
			[VfxPreset.CutsceneMode] = VfxAttribute.MultiRooms,
			[VfxPreset.WavyRows] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase | VfxAttribute.EnableSpeed,
			[VfxPreset.LightStripVert] = VfxAttribute.MultiRooms,
			[VfxPreset.SilhouettesOnHBeat] = VfxAttribute.MultiRooms,
			[VfxPreset.ColourfulShockwaves] = VfxAttribute.MultiRooms,
			[VfxPreset.BassDropOnHit] = VfxAttribute.MultiRooms,
			[VfxPreset.ShakeOnHeartBeat] = VfxAttribute.MultiRooms,
			[VfxPreset.ShakeOnHit] = VfxAttribute.MultiRooms,
			[VfxPreset.NumbersAbovePulses] = VfxAttribute.MultiRooms,
			[VfxPreset.FallingPetals] = VfxAttribute.MultiRooms,
			[VfxPreset.FallingPetalsInstant] = VfxAttribute.MultiRooms,
			[VfxPreset.FallingPetalsSnow] = VfxAttribute.MultiRooms,
			[VfxPreset.FallingLeaves] = VfxAttribute.MultiRooms,
			[VfxPreset.Rain] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Snow] = VfxAttribute.MultiRooms,
			[VfxPreset.Blizzard] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Matrix] = VfxAttribute.MultiRooms,
			[VfxPreset.Diamonds] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase | VfxAttribute.EnableColor,
			[VfxPreset.Confetti] = VfxAttribute.MultiRooms,
			[VfxPreset.ConfettiBurst] = VfxAttribute.MultiRooms,
			[VfxPreset.Balloons] = VfxAttribute.MultiRooms,
			[VfxPreset.VHS] = VfxAttribute.MultiRooms,
			[VfxPreset.Aberration] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.JPEG] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Grain] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Blur] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.RadialBlur] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Fisheye] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.HallOfMirrors] = VfxAttribute.MultiRoomsWithTop,
			[VfxPreset.TileN] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.EnableEase | VfxAttribute.EnableAbsoluteXY,
			[VfxPreset.CustomScreenScroll] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.EnableEase | VfxAttribute.EnableAbsoluteXY,
			[VfxPreset.ScreenWaves] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Mosaic] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.GlassShatter] = VfxAttribute.MultiRooms,
			[VfxPreset.GlitchObstruction] = VfxAttribute.MultiRoomsWithTop,
			[VfxPreset.Noise] = VfxAttribute.MultiRoomsWithTop,
			[VfxPreset.HueShift] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Brightness] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Contrast] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Saturation] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.BlackAndWhite] = VfxAttribute.MultiRoomsWithTop,
			[VfxPreset.Sepia] = VfxAttribute.MultiRoomsWithTop,
			[VfxPreset.Bloom] = VfxAttribute.MultiRooms | VfxAttribute.EnableAbsoluteIntensity | VfxAttribute.EnableEase | VfxAttribute.EnableColor | VfxAttribute.EnableThreshold,
			[VfxPreset.OrangeBloom] = VfxAttribute.MultiRooms,
			[VfxPreset.BlueBloom] = VfxAttribute.MultiRooms,
			[VfxPreset.Funk] = VfxAttribute.MultiRooms,
			[VfxPreset.Drawing] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Dots] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Tutorial] = VfxAttribute.MultiRooms | VfxAttribute.EnableIntensity | VfxAttribute.EnableEase,
			[VfxPreset.Tile2] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.Disabled,
			[VfxPreset.Tile3] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.Disabled,
			[VfxPreset.Tile4] = VfxAttribute.MultiRoomsWithTop | VfxAttribute.Disabled,
			[VfxPreset.ScreenScrollX] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.ScreenScroll] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.ScreenScrollSansVHS] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.ScreenScrollXSansVHS] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.RowGlowWhite] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.RowOutline] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.RowShadow] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.RowAllWhite] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.RowSilhouetteGlow] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.RowPlain] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.Blackout] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.MiawMiaw] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
			[VfxPreset.DisableAll] = VfxAttribute.MultiRooms | VfxAttribute.Disabled,
		});
	}
}
