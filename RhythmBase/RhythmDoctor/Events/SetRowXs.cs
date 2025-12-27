using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Extensions;
namespace RhythmBase.RhythmDoctor.Events;

/// <inheritdoc />
//[RDJsonObjectNotSerializable]
public class SetRowXs : BaseBeat
{
	/// <inheritdoc />
	public override EventType Type => EventType.SetRowXs;
	/// <summary>
	/// Gets or sets the pattern.
	/// </summary>
	[RDJsonConverter(typeof(PatternConverter))]
	public PatternCollection Pattern { get; set; } = "------";
	/// <summary>
	/// Gets or sets the synco beat.
	/// </summary>
	public int SyncoBeat { get; set; } = -1;
	/// <summary>
	/// Gets or sets the synco swing.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(SyncoBeat)} >= 0")]
	public float SyncoSwing { get; set; } = 0;
	/// <summary>
	/// Gets or sets the synchronization style for row processing.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(SyncoBeat)} >= 0")]
	public SetRowXsSyncoStyle SyncoStyle { get; set; } = SetRowXsSyncoStyle.Chirp;
	/// <summary>
	/// Gets or sets a value indicating whether to play the modifier sound.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(SyncoBeat)} >= 0 && $&.{nameof(SyncoStyle)} is RhythmBase.RhythmDoctor.Events.{nameof(SetRowXsSyncoStyle)}.{nameof(SetRowXsSyncoStyle.Chirp)}")]
	public bool SyncoPlayModifierSound { get; set; } = true;
	/// <summary>
	/// Gets or sets a value indicating whether the sound for turning off the SyncoPlay modifier is enabled.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(SyncoBeat)} >= 0 && $&.{nameof(SyncoStyle)} is RhythmBase.RhythmDoctor.Events.{nameof(SetRowXsSyncoStyle)}.{nameof(SetRowXsSyncoStyle.Chirp)}")]
	public bool SyncoPlayModifierOffSound { get; set; } = true;
	/// <summary>
	/// Gets or sets the synco volume.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(SyncoBeat)} >= 0")]
	public int SyncoVolume { get; set; } = 70;
	/// <summary>
	/// Gets or sets the pitch adjustment value for synchronization operations.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(SyncoBeat)} >= 0")]
	public int SyncoPitch { get; set; } = 100;	
	/// <inheritdoc />
	public override string ToString() => base.ToString() + $" {this.GetPatternString()}";
}
