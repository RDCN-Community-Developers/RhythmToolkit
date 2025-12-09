namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to set the volume of the heart explosion sound.
	/// </summary>
	public class SetHeartExplodeVolume : BaseEvent, IBarBeginningEvent
	{
		/// <summary>
		/// Gets or sets the volume of the heart explosion sound.
		/// </summary>
		public uint Volume { get; set; } = 60;
		///<inheritdoc/>
		public override EventType Type => EventType.SetHeartExplodeVolume;
		///<inheritdoc/>
		public override Tab Tab => Tab.Sounds;
	}
}
