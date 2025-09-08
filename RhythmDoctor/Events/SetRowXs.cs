using RhythmBase.RhythmDoctor.Extensions;
namespace RhythmBase.RhythmDoctor.Events
{
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
		public float SyncoSwing { get; set; } = 0;
		/// <summary>
		/// Gets or sets a value indicating whether to play the modifier sound.
		/// </summary>
		public bool SyncoPlayModifierSound { get; set; }
		/// <summary>
		/// Gets or sets the synco volume.
		/// </summary>
		public int SyncoVolume { get; set; } = 100;
		/// <inheritdoc />
		public override string ToString() => base.ToString() + string.Format(" {0}", this.GetPatternString());
		private Patterns[] _pattern = new Patterns[6];
	}
}
