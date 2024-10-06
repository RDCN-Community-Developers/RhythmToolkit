namespace RhythmBase.Events
{
	public class SetBeatsPerMinute : BaseBeatsPerMinute
	{
		public SetBeatsPerMinute()
		{
			Type = EventType.SetBeatsPerMinute;
			Tab = Tabs.Sounds;
		}
		/// <inheritdoc/>
		public override EventType Type { get; }
		/// <inheritdoc/>
		public override Tabs Tab { get; }
		/// <inheritdoc/>
		public override string ToString() => base.ToString() + string.Format(" BPM:{0}", BeatsPerMinute);
	}
}
