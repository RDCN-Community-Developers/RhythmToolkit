namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to set the volume of the heart explosion sound.
	/// </summary>
	public class SetHeartExplodeVolume : BaseEvent, IBarBeginningEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetHeartExplodeVolume"/> class.
		/// </summary>
		public SetHeartExplodeVolume() { }
		/// <summary>
		/// Gets or sets the volume of the heart explosion sound.
		/// </summary>
		public uint Volume { get; set; } = 60;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.SetHeartExplodeVolume;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Sounds;
	}
}
