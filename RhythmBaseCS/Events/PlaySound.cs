using RhythmBase.Assets;

namespace RhythmBase.Events
{
	public class PlaySound : BaseEvent
	{
		public PlaySound()
		{
			Type = EventType.PlaySound;
			Tab = Tabs.Sounds;
		}
		public bool IsCustom { get; set; }
		public CustomSoundTypes CustomSoundType { get; set; }
		public Audio Sound { get; set; }
		public override EventType Type { get; }
		public override Tabs Tab { get; }
		public override string ToString() => base.ToString() + string.Format(" {0}", IsCustom ? Sound.ToString() : CustomSoundType.ToString());
		public enum CustomSoundTypes
		{
			CueSound,
			MusicSound,
			BeatSound,
			HitSound,
			OtherSound
		}
	}
}
