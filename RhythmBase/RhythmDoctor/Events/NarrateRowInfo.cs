
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that narrates row information.
/// </summary>
public class NarrateRowInfo : BaseRowAction
{
	///<inheritdoc/>
	public override EventType Type => EventType.NarrateRowInfo;
	///<inheritdoc/>
	public override Tab Tab => Tab.Sounds;
	/// <summary>
	/// Gets or sets the type of narration information.
	/// </summary>
	public NarrateInfoType InfoType { get; set; } = NarrateInfoType.Online;
	/// <summary>
	/// Gets or sets a value indicating whether the narration is sound only.
	/// </summary>
	public bool SoundOnly { get; set; } = false;
	/// <summary>
	/// Gets or sets the beats to skip during narration.
	/// </summary>
	[RDJsonProperty("narrateSkipBeats")]
	public NarrateSkipBeat NarrateSkipBeat { get; set; } = NarrateSkipBeat.On;
	/// <summary>
	/// Gets or sets the custom pattern for the narration.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(NarrateSkipBeat)} is RhythmBase.RhythmDoctor.Events.{nameof(NarrateSkipBeat)}.{nameof(NarrateSkipBeat.Custom)}")]
	[RDJsonProperty("customPattern")]
	public PatternCollection Pattern { get; set; }
	/// <summary>
	/// Gets or sets a value indicating whether to skip unstable beats.
	/// </summary>
	public bool SkipsUnstable { get; set; } = false;
	/// <summary>  
	/// Gets or sets the custom player option for narrating row information.  
	/// </summary>  
	public PlayerType CustomPlayer { get; set; } = PlayerType.AutoDetect;
	///<inheritdoc/>
	public override string ToString() => base.ToString() + $" {InfoType}:{NarrateSkipBeat}";
}
