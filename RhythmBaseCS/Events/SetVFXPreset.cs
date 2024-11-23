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
			Rooms = new RDRoom(true, new byte[1]);
			Color = new PaletteColor(false);
			Type = EventType.SetVFXPreset;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; }

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
			Balloons,

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
