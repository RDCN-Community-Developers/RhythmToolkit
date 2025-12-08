using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Enum representing various VFX presets.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum VfxPreset
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
		FallingLeaves,
		ConfettiBurst,
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
		Fisheye,
		DisableAll,
		Diamonds,
		Tutorial,
		Balloons,
		GlassShatter,

		WavyRows,
		Tile2,
		Tile3,
		Tile4,
		ScreenScroll,
		ScreenScrollX,
		ScreenScrollSansVHS,
		ScreenScrollXSansVHS,
		RowGlowWhite,
		RowOutline,
		RowShadow,
		RowAllWhite,
		RowSilhouetteGlow,
		RowPlain,
		BlackAndWhite,
		Blackout,
		MiawMiaw,
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
		public SetVFXPreset() { }
		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the VFX preset.
		/// </summary>
		public VfxPreset Preset { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the VFX is enabled.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.DisableAll)}
			""")]
		public bool Enable { get; set; } = true;
		/// <summary>
		/// Gets or sets the threshold value for the VFX.
		/// </summary>
		[Tween]
		[RDJsonCondition($"""
			$&.{nameof(Enable)} && $&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.Bloom)}
			""")]
		public float Threshold { get; set; } = 100f;
		/// <summary>
		/// Gets or sets the intensity of the VFX.
		/// </summary>
		[Tween]
		[RDJsonCondition($"""
			$&.{nameof(Enable)} &&
			(RhythmBase.RhythmDoctor.Events.{nameof(SetVFXPreset)}.{nameof(EaseVfxs)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})
			|| $&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.Diamonds)})
			""")]
		public float Intensity { get; set; } = 0f;
		/// <summary>
		/// Gets or sets the color of the VFX.
		/// </summary>
		[Tween]
		[RDJsonCondition($"""
			$&.{nameof(Enable)} && $&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.Bloom)}
			or RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.Tutorial)}
			""")]
		public PaletteColor Color { get; set; } = RDColor.White;
		/// <summary>
		/// Gets or sets the X coordinate for the VFX.
		/// </summary>
		[Tween]
		[RDJsonCondition($"""
			$&.{nameof(Enable)} && $&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.TileN)}
			or RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.CustomScreenScroll)}
			""")]
		public float? FloatX
		{
			get => Speed.X;
			set
			{
				RDPoint speed = Speed;
				speed.X = value;
				Speed = speed;
			}
		}
		/// <summary>
		/// Gets or sets the Y coordinate for the VFX.
		/// </summary>
		[Tween]
		[RDJsonCondition($"""
			$&.{(nameof(Enable))} && $&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.TileN)}
			or RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.CustomScreenScroll)}
			""")]
		public float? FloatY
		{
			get => Speed.Y;
			set
			{
				RDPoint speed = Speed;
				speed.Y = value;
				Speed = speed;
			}
		}
		/// <summary>
		/// Gets or sets the speed represented as a two-dimensional point.
		/// </summary>
		[Tween]
		[RDJsonCondition($"""
			false
			""")]
		public RDPoint Speed { get;set; } = new(1, 1);
		[RDJsonCondition($"""
			$&.{nameof(Enable)} && $&.{nameof(Preset)} 
			is RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.TileN)}
			or  RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.CustomScreenScroll)}
			""")]
		public RDPoint Amount { get; set; } = new(1, 1);
		/// <summary>
		/// Gets or sets the speed percentage for the effect.
		/// </summary>
		[Tween]
		[RDJsonProperty("speedPerc")]
		[RDJsonCondition($"""
			$&.{nameof(Enable)} && $&.{nameof(Preset)} 
			is RhythmBase.RhythmDoctor.Events.{nameof(VfxPreset)}.{nameof(VfxPreset.WavyRows)}
			""")]
		public float SpeedPercentage { get; set; } = 100f;
		/// <summary>
		/// Gets or sets the easing type for the VFX.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(Enable)} &&
			RhythmBase.RhythmDoctor.Events.{nameof(SetVFXPreset)}.{nameof(EaseVfxs)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})
			""")]
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Gets or sets the duration of the VFX.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(Enable)} &&
			RhythmBase.RhythmDoctor.Events.{nameof(SetVFXPreset)}.{nameof(EaseVfxs)}.{nameof(ReadOnlyEnumCollection<>.Contains)}($&.{nameof(Preset)})
			""")]
		public float Duration { get; set; } = 0f;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.SetVFXPreset;

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab => Tabs.Actions;

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => $"{base.ToString()} {Preset}";
		internal static readonly ReadOnlyEnumCollection<VfxPreset> EaseVfxs = new ReadOnlyEnumCollection<VfxPreset>(
			VfxPreset.HueShift,
			VfxPreset.Brightness,
			VfxPreset.Contrast,
			VfxPreset.Saturation,
			VfxPreset.Rain,
			VfxPreset.Bloom,
			VfxPreset.TileN,
			VfxPreset.CustomScreenScroll,
			VfxPreset.JPEG,
			VfxPreset.Mosaic,
			VfxPreset.ScreenWaves,
			VfxPreset.Grain,
			VfxPreset.Blizzard,
			VfxPreset.Drawing,
			VfxPreset.Aberration,
			VfxPreset.Blur,
			VfxPreset.RadialBlur,
			VfxPreset.Dots,
			VfxPreset.Tutorial,
			VfxPreset.Fisheye
			);
	}
}
