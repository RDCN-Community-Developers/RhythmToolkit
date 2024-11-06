using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to set a VFX preset.
	/// </summary>
	public class SetVFXPreset : BaseEvent, IEaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetVFXPreset"/> class.
		/// </summary>
		public SetVFXPreset()
		{
			Rooms = new Room(true, new byte[1]);
			Color = new PaletteColor(false);
			Type = EventType.SetVFXPreset;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public Room Rooms { get; set; }

		/// <summary>
		/// Gets or sets the VFX preset.
		/// </summary>
		public Presets Preset { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the VFX is enabled.
		/// </summary>
		public bool Enable { get; set; }

		/// <summary>
		/// Gets or sets the threshold value for the VFX.
		/// </summary>
		[EaseProperty]
		public float Threshold { get; set; }

		/// <summary>
		/// Gets or sets the intensity of the VFX.
		/// </summary>
		[EaseProperty]
		public float Intensity { get; set; }

		/// <summary>
		/// Gets or sets the color of the VFX.
		/// </summary>
		[EaseProperty]
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
		public PaletteColor Color { get; set; }

		/// <summary>
		/// Gets or sets the X coordinate for the VFX.
		/// </summary>
		[EaseProperty]
		public float FloatX { get; set; }

		/// <summary>
		/// Gets or sets the Y coordinate for the VFX.
		/// </summary>
		[EaseProperty]
		public float FloatY { get; set; }

		/// <summary>
		/// Gets or sets the easing type for the VFX.
		/// </summary>
		public Ease.EaseType Ease { get; set; }

		/// <summary>
		/// Gets or sets the duration of the VFX.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => $"{base.ToString()} {Preset}";

		/// <summary>
		/// Determines whether the Enable property should be serialized.
		/// </summary>
		/// <returns>true if the Enable property should be serialized; otherwise, false.</returns>
		public bool ShouldSerializeEnable() => Preset != Presets.DisableAll;

		/// <summary>
		/// Determines whether the Threshold property should be serialized.
		/// </summary>
		/// <returns>true if the Threshold property should be serialized; otherwise, false.</returns>
		public bool ShouldSerializeThreshold() => Enable && Preset == Presets.Bloom;

		/// <summary>
		/// Determines whether the Intensity property should be serialized.
		/// </summary>
		/// <returns>true if the Intensity property should be serialized; otherwise, false.</returns>
		public bool ShouldSerializeIntensity() => Enable && PropertyHasDuration() && Preset is not (Presets.TileN or Presets.CustomScreenScroll);

		/// <summary>
		/// Determines whether the Color property should be serialized.
		/// </summary>
		/// <returns>true if the Color property should be serialized; otherwise, false.</returns>
		public bool ShouldSerializeColor() => Enable && (Preset is Presets.Bloom or Presets.Tutorial);

		/// <summary>
		/// Determines whether the FloatX property should be serialized.
		/// </summary>
		/// <returns>true if the FloatX property should be serialized; otherwise, false.</returns>
		public bool ShouldSerializeFloatX() => Enable && (Preset is Presets.TileN or Presets.CustomScreenScroll);

		/// <summary>
		/// Determines whether the FloatY property should be serialized.
		/// </summary>
		/// <returns>true if the FloatY property should be serialized; otherwise, false.</returns>
		public bool ShouldSerializeFloatY() => ShouldSerializeFloatX();

		/// <summary>
		/// Determines whether the Ease property should be serialized.
		/// </summary>
		/// <returns>true if the Ease property should be serialized; otherwise, false.</returns>
		public bool ShouldSerializeEase() => Enable && PropertyHasDuration();

		/// <summary>
		/// Determines whether the Duration property should be serialized.
		/// </summary>
		/// <returns>true if the Duration property should be serialized; otherwise, false.</returns>
		public bool ShouldSerializeDuration() => Enable && PropertyHasDuration();

		/// <summary>
		/// Determines whether the property has a duration.
		/// </summary>
		/// <returns>true if the property has a duration; otherwise, false.</returns>
		private bool PropertyHasDuration() => new Presets[]
			{
								Presets.HueShift,
								Presets.Brightness,
								Presets.Contrast,
								Presets.Saturation,
								Presets.Rain,
								Presets.Bloom,
								Presets.TileN,
								Presets.CustomScreenScroll,
								Presets.JPEG,
								Presets.Mosaic,
								Presets.ScreenWaves,
								Presets.Grain,
								Presets.Blizzard,
								Presets.Drawing,
								Presets.Aberration,
								Presets.Blur,
								Presets.RadialBlur,
								Presets.Dots,
								Presets.Tutorial,
			}.Contains(Preset);

		/// <summary>
		/// Enum representing various VFX presets.
		/// </summary>
		public enum Presets
		{
#pragma warning disable CS1591
			SilhouettesOnHBeat,
			Vignette,
			VignetteFlicker,
			ColourfulShockwaves,
			BassDropOnHit,
			ShakeOnHeartBeat,
			ShakeOnHit,
			WavyRows,
			LightStripVert,
			VHS,
			CutsceneMode,
			HueShift,
			Brightness,
			Contrast,
			Saturation,
			Noise,
			GlitchObstruction,
			Rain,
			Matrix,
			Confetti,
			FallingPetals,
			FallingPetalsInstant,
			FallingPetalsSnow,
			Snow,
			Bloom,
			OrangeBloom,
			BlueBloom,
			HallOfMirrors,
			TileN,
			Sepia,
			CustomScreenScroll,
			JPEG,
			NumbersAbovePulses,
			Mosaic,
			ScreenWaves,
			Funk,
			Grain,
			Blizzard,
			Drawing,
			Aberration,
			Blur,
			RadialBlur,
			Dots,
			DisableAll,
			Diamonds,
			Tutorial,

			//旧版特效
			BlackAndWhite,
			Blackout,
			ScreenScrollX,
			ScreenScroll,
			ScreenScrollXSansVHS,
			ScreenScrollSansVHS,
			RowGlowWhite,
			RowAllWhite,
			RowOutline,
			RowShadow,
			RowSilhouetteGlow,
			RowPlain,
			Tile2,
			Tile3,
			Tile4
#pragma warning restore CS1591
		}
	}
}
