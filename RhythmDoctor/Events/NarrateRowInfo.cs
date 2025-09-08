using Newtonsoft.Json;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that narrates row information.
	/// </summary>
	public class NarrateRowInfo : BaseRowAction
	{
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.NarrateRowInfo;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab => Tabs.Sounds;
		/// <summary>
		/// Gets or sets the type of narration information.
		/// </summary>
		public NarrateInfoTypes InfoType { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the narration is sound only.
		/// </summary>
		public bool SoundOnly { get; set; }
		/// <summary>
		/// Gets or sets the beats to skip during narration.
		/// </summary>
		[RDJsonProperty("narrateSkipBeats")]
		public NarrateSkipBeat NarrateSkipBeat { get; set; } = NarrateSkipBeat.On;
		/// <summary>
		/// Gets or sets the custom pattern for the narration.
		/// </summary>
		[JsonConverter(typeof(PatternConverter))]
		public Patterns[] CustomPattern { get; } = new Patterns[6];
		/// <summary>
		/// Gets or sets a value indicating whether to skip unstable beats.
		/// </summary>
		public bool SkipsUnstable { get; set; }
		/// <summary>  
		/// Gets or sets the custom player option for narrating row information.  
		/// </summary>  
		public NarrateRowInfoCustomPlayer CustomPlayer { get; set; }
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}:{1}", InfoType, NarrateSkipBeat);
	}
	/// <summary>
	/// Specifies the type of narration information.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum NarrateInfoTypes
	{
		/// <summary>
		/// Indicates a connection event.
		/// </summary>
		Connect,
		/// <summary>
		/// Indicates an update event.
		/// </summary>
		Update,
		/// <summary>
		/// Indicates a disconnection event.
		/// </summary>
		Disconnect,
		/// <summary>
		/// Indicates an online event.
		/// </summary>
		Online,
		/// <summary>
		/// Indicates an offline event.
		/// </summary>
		Offline
	}
	/// <summary>
	/// Specifies the beats to skip during narration.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum NarrateSkipBeat
	{
		/// <summary>
		/// Skip beats is on.
		/// </summary>
		On,
		/// <summary>
		/// Custom skip beats.
		/// </summary>
		Custom,
		/// <summary>
		/// Skip beats is off.
		/// </summary>
		Off,
	}
	/// <summary>  
	/// Specifies the custom player options for narrating row information.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum NarrateRowInfoCustomPlayer
	{
		/// <summary>  
		/// Automatically detect the player.  
		/// </summary>  
		AutoDetect,

		/// <summary>  
		/// Player 1.  
		/// </summary>  
		P1,

		/// <summary>  
		/// Player 2.  
		/// </summary>  
		P2,
	}
}
