using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to set clap sounds for different players and CPU.
	/// </summary>
	public class SetClapSounds : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetClapSounds"/> class.
		/// </summary>
		public SetClapSounds()
		{
			Type = EventType.SetClapSounds;
			Tab = Tabs.Sounds;
		}		/// <summary>
		/// Gets or sets the clap sound for player 1.
		/// </summary>
		public RDAudio? P1Sound { get; set; }		/// <summary>
		/// Gets or sets the clap sound for player 2.
		/// </summary>
		public RDAudio? P2Sound { get; set; }		/// <summary>
		/// Gets or sets the clap sound for the CPU.
		/// </summary>
		public RDAudio? CpuSound { get; set; }		/// <summary>
		/// Gets or sets the row type for the event.
		/// </summary>
		public RowType RowType { get; set; }		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
	}
}
