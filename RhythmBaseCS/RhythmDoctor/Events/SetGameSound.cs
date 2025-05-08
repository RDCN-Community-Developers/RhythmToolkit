using Newtonsoft.Json;
using RhythmBase.Global.Converters;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to set the game sound.
	/// </summary>
	public partial class SetGameSound : BaseEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="SetGameSound"/> class.  
		/// </summary>  
		public SetGameSound()
		{
			Audio = new RDAudio();
			Type = EventType.SetGameSound;
			Tab = Tabs.Sounds;
		}

		/// <summary>  
		/// Gets or sets the audio associated with the event.  
		/// </summary>  
		[JsonIgnore]
		private RDAudio Audio { get; set; }

		/// <summary>  
		/// Gets or sets the type of the sound.  
		/// </summary>  
		public SoundTypes SoundType { get; set; }

		/// <summary>  
		/// Gets or sets the filename of the audio.  
		/// </summary>  
		public string Filename
		{
			get => Audio.Filename;
			set => Audio.Filename = value;
		}

		/// <summary>  
		/// Gets or sets the volume of the audio.  
		/// </summary>  
		public int Volume
		{
			get => Audio.Volume;
			set => Audio.Volume = value;
		}

		/// <summary>  
		/// Gets or sets the pitch of the audio.  
		/// </summary>  
		public int Pitch
		{
			get => Audio.Pitch;
			set => Audio.Pitch = value;
		}

		/// <summary>  
		/// Gets or sets the pan of the audio.  
		/// </summary>  
		public int Pan
		{
			get => Audio.Pan;
			set => Audio.Pan = value;
		}

		/// <summary>  
		/// Gets or sets the offset time of the audio.  
		/// </summary>  
		[JsonConverter(typeof(MilliSecondConverter))]
		public TimeSpan Offset
		{
			get => Audio.Offset;
			set => Audio.Offset = value;
		}

		/// <summary>  
		/// Gets or sets the list of sound subtypes.  
		/// </summary>  
		public List<SoundSubType> SoundSubtypes { get; set; } = [];

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
		public override string ToString() => base.ToString() + string.Format(" {0}", SoundType);
	}
}
