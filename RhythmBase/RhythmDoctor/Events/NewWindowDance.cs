using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a new window dance event.
/// </summary>
[RDJsonObjectSerializable]
public record class NewWindowDance : BaseWindowEvent, IEaseEvent
{
	///<inheritdoc/>
	public override EventType Type { get; } = EventType.NewWindowDance;
	///<inheritdoc/>
	public override Tab Tab => CustomTab;
	/// <summary>
	/// Gets or sets the custom tab associated with this instance.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(CustomTab)} is RhythmBase.RhythmDoctor.Events.{nameof(Tab)}.{nameof(Tab.Windows)}")]
	[RDJsonConverter(typeof(TabsConverter))]
	[RDJsonAlias("tab")]
	public Tab CustomTab
	{
		get;
		set
		{
			field = value is Tab.Actions or Tab.Windows ? value : throw new InvalidOperationException();
		}
	} = Tab.Windows;
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
	public RDPoint Position { get; set; } = new(50f, 50f);
	/// <summary>
	/// Gets or sets the reference.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Move)}")]
	public WindowDanceReference Reference { get; set; } = WindowDanceReference.Center;
	/// <summary>
	/// Gets or sets a value indicating whether to use a circle.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Ellipse)}")]
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
		$&.{nameof(SamePresetBehavior)} is not RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Keep)}
		""")]
	public float Speed { get; set; } = 0;
	/// <summary>
	/// Gets or sets the amplitude.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Amplitude)} is not null &&
		(
		$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Ellipse)} || $&.{nameof(UseCircle)}) &&
		$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.ShakePer)} &&
		$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Move)}
		""")]
	public float? Amplitude { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the amplitude vector.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		(
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Ellipse)} && !$&.{nameof(UseCircle)} ||
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.ShakePer)}) &&
		$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Move)}
		""")]
	public RDPoint AmplitudeVector { get; set; } = new(0f, 0f);
	/// <summary>
	/// Gets or sets the angle.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
		is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Sway)}
		or RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Ellipse)} ||
		($&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Wrap)} &&
		$&.{nameof(SamePresetBehavior)} is not RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Keep)})
		""")]
	public float? Angle { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the frequency.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
		is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Sway)}
		or RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Wrap)}
		or RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.ShakePer)} &&
		$&.{nameof(SamePresetBehavior)} is not RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Keep)}
		""")]
	public float Frequency { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the period.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.ShakePer)} &&
		$&.{nameof(SamePresetBehavior)} is not RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Keep)}
		""")]
	public float Period { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the ease type.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Sway)} &&
		$&.{nameof(SamePresetBehavior)} is not RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Keep)}
		""")]
	public WindowDanceEaseType EaseType { get; set; } = WindowDanceEaseType.Repeat;
	/// <summary>
	/// Gets or sets the sub ease type.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.Sway)}
			or RhythmBase.RhythmDoctor.Events.{nameof(WindowDancePreset)}.{nameof(WindowDancePreset.ShakePer)} &&
		$&.{nameof(SamePresetBehavior)} is not RhythmBase.RhythmDoctor.Events.{nameof(SamePresetBehavior)}.{nameof(SamePresetBehavior.Keep)}
		""")]
	[RDJsonAlias("subEase")]
	public EaseType SubEaseType { get; set; } = Global.Components.Easing.EaseType.Linear;
	///<inheritdoc/>
	[RDJsonAlias("easingDuration")]
	public float Duration { get; set; } = 0;
	///<inheritdoc/>
	public EaseType Ease { get; set; } = Global.Components.Easing.EaseType.Linear;
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	private bool AmplitudeIsVector => (Preset is WindowDancePreset.Ellipse && !UseCircle) || Preset is WindowDancePreset.ShakePer;
	private bool PresetBehaviorIsKeep => SamePresetBehavior is SamePresetBehavior.Keep;
	private bool AngleIf()
	{
		return
			Preset
			is WindowDancePreset.Sway
			or WindowDancePreset.Ellipse ||
			(Preset is WindowDancePreset.Wrap && SamePresetBehavior is not SamePresetBehavior.Keep);
		switch (this.Preset)
		{
			case WindowDancePreset.Sway:
			case WindowDancePreset.Ellipse:
				return true;
			default:
				return this.Preset is WindowDancePreset.Wrap && SamePresetBehavior is not SamePresetBehavior.Keep;
		}
	}
}