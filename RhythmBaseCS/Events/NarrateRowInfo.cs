using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Converters;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event that narrates row information.
	/// </summary>
	public class NarrateRowInfo : BaseRowAction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NarrateRowInfo"/> class.
		/// </summary>
		public NarrateRowInfo()
		{
			Type = EventType.NarrateRowInfo;
			Tab = Tabs.Actions;
			CustomPattern = new Patterns[6];
		}		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }		/// <summary>
		/// Gets or sets the type of narration information.
		/// </summary>
		public NarrateInfoType InfoType { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether the narration is sound only.
		/// </summary>
		public bool SoundOnly { get; set; }		/// <summary>
		/// Gets or sets the beats to skip during narration.
		/// </summary>
		[JsonProperty("narrateSkipBeats")]
		public NarrateSkipBeats NarrateSkipBeat { get; set; } = NarrateSkipBeats.On;		/// <summary>
		/// Gets or sets the custom pattern for the narration.
		/// </summary>
		[JsonConverter(typeof(PatternConverter))]
		public Patterns[] CustomPattern { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether to skip unstable beats.
		/// </summary>
		public bool SkipsUnstable { get; set; }		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}:{1}", InfoType, NarrateSkipBeat);		/// <summary>
		/// Specifies the type of narration information.
		/// </summary>
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
		public enum NarrateSkipBeats
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
}
