using Newtonsoft.Json;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Enum representing various VFX presets.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum VFXPresets
	{
#pragma warning disable CS1591
		SilhouettesOnHBeat,
		Vignette,
		VignetteFlicker,
		ColourfulShockwaves,
		BassDropOnHit,
		ShakeOnHeartBeat,
		ShakeOnHit,
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
		FishEye,
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
		Tile4,
		WavyRows,
#pragma warning restore CS1591
	}
	/// <summary>
	/// Represents an event to set a VFX preset.
	/// </summary>
	public class SetVFXPreset : BaseEvent, IEaseEvent, IRoomEvent, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetVFXPreset"/> class.
		/// </summary>
		public SetVFXPreset()
		{
			Rooms = new RDRoom([0]);
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
		public VFXPresets Preset { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the VFX is enabled.
		/// </summary>
		[RDJsonCondition($"""
			$&.Preset is not RhythmBase.RhythmDoctor.Events.VFXPresets.DisableAll
			""")]
		public bool Enable { get; set; }
		/// <summary>
		/// Gets or sets the threshold value for the VFX.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.Enable && $&.Preset
			is RhythmBase.RhythmDoctor.Events.VFXPresets.Bloom
			""")]
		public float Threshold { get; set; }
		/// <summary>
		/// Gets or sets the intensity of the VFX.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.Enable && $&.Preset 
			is RhythmBase.RhythmDoctor.Events.VFXPresets.HueShift
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Brightness
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Contrast
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Saturation
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Rain
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Bloom
			or RhythmBase.RhythmDoctor.Events.VFXPresets.JPEG
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Mosaic
			or RhythmBase.RhythmDoctor.Events.VFXPresets.ScreenWaves
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Grain
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Blizzard
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Drawing
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Aberration
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Blur
			or RhythmBase.RhythmDoctor.Events.VFXPresets.RadialBlur
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Dots
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Tutorial
			""")]
		public float Intensity { get; set; }
		/// <summary>
		/// Gets or sets the color of the VFX.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.Enable && $&.Preset
			is RhythmBase.RhythmDoctor.Events.VFXPresets.Bloom
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Tutorial
			""")]
		public PaletteColor Color { get; set; }
		/// <summary>
		/// Gets or sets the X coordinate for the VFX.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.Enable && $&.Preset
			is RhythmBase.RhythmDoctor.Events.VFXPresets.TileN
			or RhythmBase.RhythmDoctor.Events.VFXPresets.CustomScreenScroll
			""")]
		public float FloatX { get; set; }
		/// <summary>
		/// Gets or sets the Y coordinate for the VFX.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"""
			$&.Enable && $&.Preset
			is RhythmBase.RhythmDoctor.Events.VFXPresets.TileN
			or RhythmBase.RhythmDoctor.Events.VFXPresets.CustomScreenScroll
			""")]
		public float FloatY { get; set; }
		/// <summary>
		/// Gets or sets the easing type for the VFX.
		/// </summary>
		[RDJsonCondition($"""
			$&.Enable && $&.Preset 
			is RhythmBase.RhythmDoctor.Events.VFXPresets.HueShift
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Brightness
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Contrast
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Saturation
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Rain
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Bloom
			or RhythmBase.RhythmDoctor.Events.VFXPresets.TileN
			or RhythmBase.RhythmDoctor.Events.VFXPresets.CustomScreenScroll
			or RhythmBase.RhythmDoctor.Events.VFXPresets.JPEG
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Mosaic
			or RhythmBase.RhythmDoctor.Events.VFXPresets.ScreenWaves
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Grain
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Blizzard
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Drawing
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Aberration
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Blur
			or RhythmBase.RhythmDoctor.Events.VFXPresets.RadialBlur
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Dots
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Tutorial
			""")]
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the duration of the VFX.
		/// </summary>
		[RDJsonCondition($"""
			$&.Enable && $&.Preset 
			is RhythmBase.RhythmDoctor.Events.VFXPresets.HueShift
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Brightness
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Contrast
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Saturation
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Rain
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Bloom
			or RhythmBase.RhythmDoctor.Events.VFXPresets.TileN
			or RhythmBase.RhythmDoctor.Events.VFXPresets.CustomScreenScroll
			or RhythmBase.RhythmDoctor.Events.VFXPresets.JPEG
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Mosaic
			or RhythmBase.RhythmDoctor.Events.VFXPresets.ScreenWaves
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Grain
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Blizzard
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Drawing
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Aberration
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Blur
			or RhythmBase.RhythmDoctor.Events.VFXPresets.RadialBlur
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Dots
			or RhythmBase.RhythmDoctor.Events.VFXPresets.Tutorial
			""")]
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
	}
}
