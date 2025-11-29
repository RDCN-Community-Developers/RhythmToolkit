using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to set clap sounds for different players and CPU.
	/// </summary>
	public class SetClapSounds : BaseEvent, IAudioFileEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetClapSounds"/> class.
		/// </summary>
		public SetClapSounds()
		{
			Type = EventType.SetClapSounds;
			Tab = Tabs.Sounds;
		}
		/// <summary>
		/// Gets or sets the clap sound for player 1.
		/// </summary>
		[RDJsonCondition("$&.P1Sound is not null")]
		public RDAudio? P1Sound { get; set; } = new RDAudio()
		{
			Filename = "ClapHit",
		};
		/// <summary>
		/// Gets or sets the clap sound for player 2.
		/// </summary>
		[RDJsonCondition("$&.P2Sound is not null")]
		public RDAudio? P2Sound { get; set; }
		/// <summary>
		/// Gets or sets the clap sound for the CPU.
		/// </summary>
		[RDJsonCondition("$&.CpuSound is not null")]
		public RDAudio? CpuSound { get; set; }
		/// <summary>
		/// Gets or sets the row type for the event.
		/// </summary>
		public RowTypes RowType { get; set; } = RowTypes.Classic;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.SetClapSounds;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Sounds;
		IEnumerable<FileReference> IAudioFileEvent.AudioFiles
		{
			get
			{
				IEnumerable<FileReference> files = [];
				if (P1Sound is not null && P1Sound.IsFile)
					files = files.Append(P1Sound.Filename);
				if (P2Sound is not null && P2Sound.IsFile)
					files = files.Append(P2Sound.Filename);
				if (CpuSound is not null && CpuSound.IsFile)
					files = files.Append(CpuSound.Filename);
				return files;
			}
		}

		IEnumerable<FileReference> IFileEvent.Files 
		{
			get
			{
				IEnumerable<FileReference> files = [];
				if (P1Sound is not null && P1Sound.IsFile)
					files = files.Append(P1Sound.Filename);
				if (P2Sound is not null && P2Sound.IsFile)
					files = files.Append(P2Sound.Filename);
				if (CpuSound is not null && CpuSound.IsFile)
					files = files.Append(CpuSound.Filename);
				return files;
			}
		}
	}
}
