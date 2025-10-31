﻿namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to set the beats per minute (BPM) in the rhythm base.
	/// </summary>
	public class SetBeatsPerMinute : BaseBeatsPerMinute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetBeatsPerMinute"/> class.
		/// </summary>
		public SetBeatsPerMinute() { }
		/// <inheritdoc/>
		public override float BeatsPerMinute { get => base.BeatsPerMinute; set => base.BeatsPerMinute = value; }
		/// <inheritdoc/>
		public override EventType Type { get; } = EventType.SetBeatsPerMinute;
		/// <inheritdoc/>
		public override Tabs Tab { get; } = Tabs.Sounds;
		/// <inheritdoc/>
		public override string ToString() => base.ToString() + $" BPM:{BeatsPerMinute}";
	}
}
