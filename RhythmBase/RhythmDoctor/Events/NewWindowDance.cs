using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents the presets.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowDancePreset
{
	/// <summary>
	/// Move preset.
	/// </summary>
	Move,
	/// <summary>
	/// Sway preset.
	/// </summary>
	Sway,
	/// <summary>
	/// Wrap preset.
	/// </summary>
	Wrap,
	/// <summary>
	/// Ellipse preset.
	/// </summary>
	Ellipse,
	/// <summary>
	/// Shake per preset.
	/// </summary>
	ShakePer
}
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
	[RDJsonCondition($"$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.WindowDancePresets.Move")]
	public SamePresetBehavior SamePresetBehavior { get; set; } = SamePresetBehavior.Reset;
	/// <summary>
	/// Gets or sets the position.
	/// </summary>
	[Tween]
	public RDPointE Position { get; set; } = new(50f, 50f);
	/// <summary>
	/// Gets or sets the reference.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Move")]
	public WindowDanceReference Reference { get; set; } = WindowDanceReference.Center;
	/// <summary>
	/// Gets or sets a value indicating whether to use a circle.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Ellipse &&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
		""")]
	public bool UseCircle { get; set; } = false;
	/// <summary>
	/// Gets or sets the speed.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Wrap
			or RhythmBase.RhythmDoctor.Events.WindowDancePresets.Ellipse
			&&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
		""")]
	public float Speed { get; set; } = 0;
	/// <summary>
	/// Gets or sets the amplitude.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
		is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Move
		or RhythmBase.RhythmDoctor.Events.WindowDancePresets.Sway
		""")]
	public float Amplitude { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the amplitude vector.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
		is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Ellipse
		or RhythmBase.RhythmDoctor.Events.WindowDancePresets.ShakePer
		""")]
	public RDPointE AmplitudeVector { get; set; } = new(0f, 0f);
	/// <summary>
	/// Gets or sets the angle.
	/// </summary>
	[Tween]
	[RDJsonCondition($"$&.{nameof(Preset)} is not RhythmBase.RhythmDoctor.Events.WindowDancePresets.Move")]
	public float? Angle { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the frequency.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Wrap
			or RhythmBase.RhythmDoctor.Events.WindowDancePresets.ShakePer
			&&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
		""")]
	public float Frequency { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the period.
	/// </summary>
	[Tween]
	[RDJsonCondition($"""
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.WindowDancePresets.ShakePer &&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
		""")]
	public float Period { get; set; } = 0f;
	/// <summary>
	/// Gets or sets the ease type.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Preset)} is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Sway &&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
		""")]
	public WindowDanceEaseTypes EaseType { get; set; } = WindowDanceEaseTypes.Repeat;
	/// <summary>
	/// Gets or sets the sub ease type.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Preset)}
			is RhythmBase.RhythmDoctor.Events.WindowDancePresets.Sway
			or RhythmBase.RhythmDoctor.Events.WindowDancePresets.ShakePer &&
		$&.{nameof(SamePresetBehavior)} is RhythmBase.RhythmDoctor.Events.SamePresetBehaviors.Reset
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
	public override Tab Tab { get; } = Tab.Actions;
}
/// <summary>
/// Represents the same preset behaviors.
/// </summary>
[RDJsonEnumSerializable]
public enum SamePresetBehavior
{
	/// <summary>
	/// Reset behavior.
	/// </summary>
	Reset,
	/// <summary>
	/// Keep behavior.
	/// </summary>
	Keep
}
/// <summary>
/// Represents the references.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowDanceReference
{
	/// <summary>
	/// Center reference.
	/// </summary>
	Center,
	/// <summary>
	/// Edge reference.
	/// </summary>
	Edge
}
/// <summary>
/// Represents the ease types.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowDanceEaseTypes
{
	/// <summary>
	/// Repeat ease type.
	/// </summary>
	Repeat,
	/// <summary>
	/// Mirror ease type.
	/// </summary>
	Mirror,
}
