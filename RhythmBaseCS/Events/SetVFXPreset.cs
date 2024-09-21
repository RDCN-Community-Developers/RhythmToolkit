using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public class SetVFXPreset : BaseEvent, IEaseEvent, IRoomEvent
	{
		public SetVFXPreset()
		{
			Rooms = new Room(true, new byte[1]);
			Color = new PaletteColor(false);
			Type = EventType.SetVFXPreset;
			Tab = Tabs.Actions;
		}
		public Room Rooms { get; set; }
		public Presets Preset { get; set; }
		public bool Enable { get; set; }
		[EaseProperty]
		public float Threshold { get; set; }
		[EaseProperty]
		public float Intensity { get; set; }
		[EaseProperty]
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
		public PaletteColor Color { get; set; }
		[EaseProperty]
		public float FloatX { get; set; }
		[EaseProperty]
		public float FloatY { get; set; }
		public Ease.EaseType Ease { get; set; }
		public float Duration { get; set; }
		public override EventType Type { get; }
		public override Tabs Tab { get; }
		public override string ToString() => $"{base.ToString()} {Preset}";
		public bool ShouldSerializeEnable() => Preset != Presets.DisableAll;
		public bool ShouldSerializeThreshold() => Enable && Preset == Presets.Bloom;
		public bool ShouldSerializeIntensity() => Enable && PropertyHasDuration() && Preset is not (Presets.TileN or Presets.CustomScreenScroll);
		public bool ShouldSerializeColor() => Enable && (Preset is Presets.Bloom or Presets.Tutorial);
		public bool ShouldSerializeFloatX() => Enable && (Preset is Presets.TileN or Presets.CustomScreenScroll);
		public bool ShouldSerializeFloatY() => ShouldSerializeFloatX();
		public bool ShouldSerializeEase() => Enable && PropertyHasDuration();
		public bool ShouldSerializeDuration() => Enable && PropertyHasDuration();
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
		public enum Presets
		{
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
		}
	}
}
