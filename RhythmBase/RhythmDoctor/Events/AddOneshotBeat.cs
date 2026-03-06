using RhythmBase.RhythmDoctor.Components;
using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to add a one-shot beat.
/// </summary>
[RDJsonObjectSerializable]
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public record class AddOneshotBeat : BaseBeat
{
	/// <summary>
	/// Gets or sets the type of pulse.
	/// </summary>
	public OneshotPulseShapeType PulseType { get; set; }
	/// <summary>
	/// Gets or sets the number of subdivisions.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Subdivisions)} > 0 &&(
		$&.{nameof(PulseType)}
		is RhythmBase.RhythmDoctor.Events.{nameof(OneshotPulseShapeType)}.{nameof(OneshotPulseShapeType.Square)}
		or RhythmBase.RhythmDoctor.Events.{nameof(OneshotPulseShapeType)}.{nameof(OneshotPulseShapeType.Triangle)}
		)
		""")]
	public byte Subdivisions { get; set; } = 1;
	/// <summary>
	/// Gets or sets a value indicating whether the subdivision sound is enabled.
	/// </summary>
	[RDJsonAlias("subdivSound")]
	[RDJsonCondition($"$&.{nameof(Subdivisions)} > 0")]
	public bool SubdivisionSound { get; set; }
	/// <summary>
	/// 
	/// </summary>
	[RDJsonAlias("subdivTickOverride")]
	[RDJsonCondition($"$&.{nameof(SubdivisionTickOverride)} > 0")]
	public float SubdivisionTickOverride { get; set; }
	/// <summary>
	/// Gets or sets the tick value.
	/// </summary>
	public float Tick { get; set; } = 1f;
	/// <summary>
	/// Gets or sets the number of loops.
	/// </summary>
	[RDJsonAlias("loops")]
	[RDJsonCondition($"$&.{nameof(Loop)} > 0")]
	public uint Loop { get; set; }
	/// <summary>
	/// Gets or sets the interval value.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Interval)} > 0 &&(
		$&.{nameof(Skipshot)} ||
		$&.{nameof(Hold)} ||
		$&.{nameof(FreezeBurnMode)} is not RhythmBase.RhythmDoctor.Events.{nameof(OneshotType)}.{nameof(OneshotType.Wave)} ||
		$&.{nameof(Loop)} > 0)
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
	[RDJsonCondition($"$&.{nameof(FreezeBurnMode)} == RhythmBase.RhythmDoctor.Events.{nameof(OneshotType)}.{nameof(OneshotType.Freezeshot)}")]
	public float Delay
	{
		get;
		set => field = FreezeBurnMode != OneshotType.Freezeshot
			? 0f : value <= 0f
				? 0.5f : value;
	} = 0f;
	[RDJsonCondition($"$&.{nameof(Sound)} != null")]
	public RDAudio? Sound {  get; set; }
	/// <inheritdoc/>
	public override EventType Type => EventType.AddOneshotBeat;
	/// <inheritdoc/>
	public override string ToString() => base.ToString() + $" {FreezeBurnMode} {PulseType}";
	private string GetDebuggerDisplay() => ToString();
}