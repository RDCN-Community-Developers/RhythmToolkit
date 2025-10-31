
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
		public NarrateInfoType InfoType { get; set; } = NarrateInfoType.Online;
		/// <summary>
		/// Gets or sets a value indicating whether the narration is sound only.
		/// </summary>
		public bool SoundOnly { get; set; } = false;
		/// <summary>
		/// Gets or sets the beats to skip during narration.
		/// </summary>
		[RDJsonProperty("narrateSkipBeats")]
		public NarrateSkipBeat NarrateSkipBeat { get; set; } = NarrateSkipBeat.On;
		/// <summary>
		/// Gets or sets the custom pattern for the narration.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(NarrateSkipBeat)} is RhythmBase.RhythmDoctor.Events.NarrateInfoTypes.{nameof(NarrateSkipBeat.Custom)}")]
		public Patterns[] CustomPattern { get; } = new Patterns[6];
		/// <summary>
		/// Gets or sets a value indicating whether to skip unstable beats.
		/// </summary>
		public bool SkipsUnstable { get; set; } = false;
		/// <summary>  
		/// Gets or sets the custom player option for narrating row information.  
		/// </summary>  
		public PlayerType CustomPlayer { get; set; } = PlayerType.AutoDetect;
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
	public enum NarrateInfoType
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
}
