using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a new window dance event.
/// </summary>
public class NewWindowDance : BaseWindowEvent, IEaseEvent
{
	/// <summary>
	/// Gets or sets the preset.
	/// </summary>
	public WindowDancePreset Preset { get; set; } = WindowDancePreset.Move;
	/// <summary>
	/// Gets or sets the same preset behavior.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Move)}")]
	public SamePresetBehavior SamePresetBehavior { get; set; } = SamePresetBehavior.Reset;
	/// <summary>
	/// Gets or sets the position.
	/// </summary>
	[Tween]
	public RDPointE Position { get; set; } = new(50f, 50f);
	/// <summary>
	/// Gets or sets the reference.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Move)}")]
	public WindowDanceReference Reference { get; set; } = WindowDanceReference.Center;
	/// <summary>
	/// Gets or sets a value indicating whether to use a circle.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Ellipse)} &&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Reset)}
		""")]
	public bool UseCircle { get; set; } = false;
	/// <summary>
	/// Gets or sets the speed.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Wrap)}
			or RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Ellipse)}
			&&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Reset)}
		""")]
	public float Speed { get; set; } = 0;
	/// <summary>
	/// Gets or sets the amplitude.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
		is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Move)}
		or RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Sway)}
		""")]
	public float Amplitude { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the amplitude vector.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
		is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Ellipse)}
		or RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.ShakePer)}
		""")]
	public RDPointE AmplitudeVector { get; set; } = new(0f, 0f);
	/// <summary>
	/// Gets or sets the angle.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Move)}")]
	public float? Angle { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the frequency.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Wrap)}
			or RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.ShakePer)}
			&&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Reset)}
		""")]
	public float Frequency { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the period.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.ShakePer)} &&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Reset)}
		""")]
	public float Period { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the ease type.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Sway)} &&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Reset)}
		""")]
	public WindowDanceEaseType EaseType { get; set; } = WindowDanceEaseType.Repeat;
	/// <summary>
	/// Gets or sets the sub ease type.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Sway)}
			or RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.ShakePer)} &&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Reset)}
		""")]
	public EaseType SubEase { get; set; } = Global.Components.Easing.EaseType.Linear;
	///<inheritdoc/>
	[RDJsonProperty("easingDuration")]
	public float Duration { get; set; } = 0;
	///<inheritdoc/>
	public EaseType Ease { get; set; } = Global.Components.Easing.EaseType.Linear;
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	///<inheritdoc/>
	public override EventType Type { get; } = EventType.NewWindowDance;
	///<inheritdoc/>
	[RDJsonIgnore]
	public override Tab Tab => CustomTab;
	/// <summary>
	/// Gets or sets the custom tab associated with this instance.
	/// </summary>
	[RDJsonConverter(typeof(TabsConverter))]
	[RDJsonCondition($"$&.{nameof(CustomTab)} is not RhythmBase.RhythmDoctor.Events.{nameof(Tab)}.{nameof(Tab.Windows)}")]
	[RDJsonProperty("tab")]
	public Tab CustomTab
	{
		get;
		set => field = value is Tab.Actions or Tab.Windows ? value : throw new InvalidOperationException();
	} = Tab.Actions;
}
