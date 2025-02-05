using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to play a song with specific beats per minute and other properties.
	/// </summary>
	public class PlaySong : BaseBeatsPerMinute, IBarBeginningEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlaySong"/> class.
		/// </summary>
		public PlaySong()
		{
			Type = EventType.PlaySong;
			Tab = Tabs.Sounds;
		}

		/// <summary>
		/// Gets or sets the beats per minute (BPM) for the song.
		/// </summary>
		[JsonProperty("bpm")]
		public override float BeatsPerMinute
		{
			get => base.BeatsPerMinute;
			set => base.BeatsPerMinute = value;
		}

		/// <summary>
		/// Gets or sets the offset time for the song.
		/// </summary>
		[JsonIgnore]
		public TimeSpan Offset
		{
			get => Song.Offset;
			set => Song.Offset = value;
		}

		/// <summary>
		/// Gets or sets a value indicating whether the song should loop.
		/// </summary>
		public bool Loop { get; set; }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" BPM:{0}, Song:{1}", BeatsPerMinute, Song.Filename);

		/// <summary>
		/// Gets or sets the song to be played.
		/// </summary>
		public RDAudio Song = new();
	}
}
