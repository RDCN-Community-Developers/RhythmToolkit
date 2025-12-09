using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to add a one-shot beat.
/// </summary>
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class AddOneshotBeat : BaseBeat
{
	/// <summary>
	/// Gets or sets the type of pulse.
	/// </summary>
	public OneshotPulseShapeType PulseType { get; set; }
	/// <summary>
	/// Gets or sets the number of subdivisions.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(PulseType)} is not RhythmBase.RhythmDoctor.Events.{nameof(OneshotPulseShapeType)}.{nameof(OneshotPulseShapeType.Wave)}")]
	public byte Subdivisions { get; set; } = 1;
	/// <summary>
	/// Gets or sets a value indicating whether the subdivision sound is enabled.
	/// </summary>
	[RDJsonProperty("subdivSound")]
	[RDJsonCondition($"$&.{nameof(PulseType)} is not RhythmBase.RhythmDoctor.Events.{nameof(OneshotPulseShapeType)}.{nameof(OneshotPulseShapeType.Wave)}")]
	public bool SubdivisionSound { get; set; }
	/// <summary>
	/// Gets or sets the tick value.
	/// </summary>
	public float Tick { get; set; } = 1f;
	/// <summary>
	/// Gets or sets the number of loops.
	/// </summary>
	public uint Loops { get; set; }
	/// <summary>
	/// Gets or sets the interval value.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Skipshot)} ||
		$&.{nameof(FreezeBurnMode)} is not RhythmBase.RhythmDoctor.Events.{nameof(OneshotType)}.{nameof(OneshotType.Wave)} ||
		$&.{nameof(Loops)} > 0
		""")]
	public float Interval { get; set; } = 2;
	/// <summary>
	/// Gets or sets a value indicating whether to skip the shot.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Skipshot)}")]
	public bool Skipshot { get; set; }

	/// <summary>  
	/// Gets or sets a value indicating whether the hold action is enabled.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(Hold)}")]
	public bool Hold { get; set; } = false;

	/// <summary>  
	/// Gets or sets a value indicating whether a sudden hold cue is triggered.  
	/// </summary>  
	[RDJsonCondition($"$&.{nameof(Hold)}")]
	public HoldCue HoldCue { get; set; } = HoldCue.Auto;

	/// <summary>
	/// Gets or sets the freeze burn mode.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(FreezeBurnMode)} is not RhythmBase.RhythmDoctor.Events.{nameof(OneshotType)}.{nameof(OneshotType.Wave)}")]
	public OneshotType FreezeBurnMode { get; set; }
	/// <summary>
	/// Gets or sets the delay value.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(FreezeBurnMode)} == RhythmBase.RhythmDoctor.Events.OneshotTypes.Freezeshot")]
	public float Delay
	{
		get;
		set => field = FreezeBurnMode != OneshotType.Freezeshot
			? 0f : value <= 0f
				? 0.5f : value;
	} = 0f;
	/// <inheritdoc/>
	public override EventType Type => EventType.AddOneshotBeat;
	/// <inheritdoc/>
	public override string ToString() => base.ToString() + $" {FreezeBurnMode} {PulseType}";
	private string GetDebuggerDisplay() => ToString();
}
/// <summary>
/// Represents the freeze burn mode.
/// </summary>
[RDJsonEnumSerializable]
public enum OneshotType
{
	/// <summary>
	/// A wave freeze burn mode.
	/// </summary>
	Wave,
	/// <summary>
	/// A freeze shot mode.
	/// </summary>
	Freezeshot,
	/// <summary>
	/// A burn shot mode.
	/// </summary>
	Burnshot
}
/// <summary>
/// Represents the type of pulse.
/// </summary>
[RDJsonEnumSerializable]
public enum OneshotPulseShapeType
{
	/// <summary>
	/// A wave pulse.
	/// </summary>
	Wave,
	/// <summary>
	/// A square pulse.
	/// </summary>
	Square,
	/// <summary>
	/// A triangle pulse.
	/// </summary>
	Triangle,
	/// <summary>
	/// A heart-shaped pulse.
	/// </summary>
	Heart
}
/// <summary>
/// Specifies when a hold cue should be triggered for a oneshot beat.
/// </summary>
[RDJsonEnumSerializable]
public enum HoldCue
{
	/// <summary>
	/// Let the system select the most appropriate cue timing automatically.
	/// </summary>
	Auto,
	/// <summary>
	/// Force the hold cue to trigger earlier than the default timing.
	/// </summary>
	Early,
	/// <summary>
	/// Force the hold cue to trigger later than the default timing.
	/// </summary>
	Late,
}
