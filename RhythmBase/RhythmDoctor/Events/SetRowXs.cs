using RhythmBase.RhythmDoctor.Extensions;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Specifies the style of the synco sound for the SetRowXs event.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum SetRowXsSyncoStyle
	{
		/// <summary>
		/// Use the "Chirp" style for the synco sound.
		/// </summary>
		Chirp,
		/// <summary>
		/// Use the "Beep" style for the synco sound.
		/// </summary>
		Beep,
	}
	/// <inheritdoc />
	//[RDJsonObjectNotSerializable]
	public class SetRowXs : BaseBeat
	{
		/// <inheritdoc />
		public SetRowXs() { }
		/// <inheritdoc />
		public override EventType Type => EventType.SetRowXs;
		/// <summary>
		/// Gets or sets the pattern.
		/// </summary>
		//[JsonConverter(typeof(PatternConverter))]
		public Patterns[] Pattern { get; set; } = new Patterns[6];
		/// <summary>
		/// Gets or sets the synco beat.
		/// </summary>
		public sbyte SyncoBeat { get; set; } = -1;
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
		/// Gets or sets the synco volume.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(SyncoBeat)} >= 0")]
		public int SyncoVolume { get; set; } = 70;
		/// <inheritdoc />
		public override string ToString() => base.ToString() + $" {this.GetPatternString()}";
	}
}
