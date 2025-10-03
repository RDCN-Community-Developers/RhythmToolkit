using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an action to set the beat sound in the rhythm base.
	/// </summary>
	public class SetBeatSound : BaseRowAction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetBeatSound"/> class.
		/// </summary>
		public SetBeatSound()
		{
			Sound = new RDAudio();
			Type = EventType.SetBeatSound;
			Tab = Tabs.Sounds;
		}
		/// <summary>
		/// Gets or sets the audio sound for the beat.
		/// </summary>
		public RDAudio Sound { get; set; } = new()
		{
			Filename = "Shaker",
		};
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
	}
}
