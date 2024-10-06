using Newtonsoft.Json;
using RhythmBase.Assets;
namespace RhythmBase.Events
{
	public class PlaySong : BaseBeatsPerMinute, IBarBeginningEvent
	{
		public PlaySong()
		{
			Type = EventType.PlaySong;
			Tab = Tabs.Sounds;
		}

		[JsonProperty("bpm")]
		public override float BeatsPerMinute {
			get => base.BeatsPerMinute;
			set => base.BeatsPerMinute = value;
		}
		[JsonIgnore]
		public TimeSpan Offset
		{
			get => Song.Offset;
			set => Song.Offset = value;
		}
		public bool Loop { get; set; }
		public override EventType Type { get; }
		public override Tabs Tab { get; }
		public override string ToString() => base.ToString() + string.Format(" BPM:{0}, Song:{1}", BeatsPerMinute, Song.Name);
		public Audio Song;
	}
}
