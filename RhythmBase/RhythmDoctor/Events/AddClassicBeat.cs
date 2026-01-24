using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Extensions;
using System.Diagnostics;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event to add a classic beat.
/// </summary>
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public record class AddClassicBeat : BaseBeat
{
	/// <summary>
	/// Gets or sets the tick value.
	/// </summary>
	public float Tick { get; set; } = 1f;
	/// <summary>
	/// Gets or sets the swing value.
	/// </summary>
	[RDJsonCondition("$&.Swing is not 0.5f and not 0f")]
	public float Swing { get; set; }
	/// <summary>
	/// Gets or sets the hold value.
	/// </summary>
	[RDJsonCondition("$&.Hold != 0f")]
	public float Hold { get; set; }
	/// <summary>  
	/// Gets or sets the number of beats in a classic beat pattern.  
	/// </summary>   
	[RDJsonCondition("$&.Length != 7")]
	public int Length { get; set; } = 7;
	/// <summary>
	/// Gets or sets the audio content associated with this instance.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Sound)} is not null")]
	public RDAudio? Sound { get; set; } = null;
	/// <summary>
	/// Gets or sets the classic beat pattern.
	/// </summary>
	[RDJsonCondition($"""
		$&.{nameof(Hold)} != 0f &&
		$&.{nameof(SetXs)} != RhythmBase.RhythmDoctor.Events.{nameof(ClassicBeatPattern)}.{nameof(ClassicBeatPattern.NoChange)}
		""")]
	public ClassicBeatPattern SetXs { get; set; }
	/// <inheritdoc/>
	public override EventType Type => EventType.AddClassicBeat;
	/// <inheritdoc/>
	public override string ToString() => base.ToString() +
		$" {this.Pattern} {((Swing is 0.5f or 0f) ? "" : " Swing")}";
	private string GetDebuggerDisplay() => ToString();
}