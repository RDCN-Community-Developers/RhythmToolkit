using Newtonsoft.Json;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Defines the possible actions for a tag.
	/// </summary>
	[Flags]
	public enum TagActions
	{
		/// <summary>
		/// Represents the run action.
		/// </summary>
		Run = 2,
		/// <summary>
		/// Represents all actions.
		/// </summary>
		All = 1,
		/// <summary>
		/// Represents the enable action.
		/// </summary>
		Enable = 6,
		/// <summary>
		/// Represents the disable action.
		/// </summary>
		Disable = 4
	}
	/// <summary>
	/// Represents a tag action event.
	/// </summary>
	public class TagAction : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TagAction"/> class.
		/// </summary>
		public TagAction() { }
		/// <summary>
		/// Gets or sets the action associated with the tag.
		/// </summary>
		[JsonIgnore]
		public TagActions Action { get; set; } = TagActions.Run;
		/// <summary>
		/// Gets or sets the action tag.
		/// </summary>
		[JsonProperty("Tag")]
		public string ActionTag { get; set; } = "";
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.TagAction;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", ActionTag);
	}
	/// <summary>
	/// Defines special tags for the action.
	/// </summary>
	public enum SpecialTags
	{
#pragma warning disable CS1591
		onHit,
		onMiss,
		onHeldPressHit,
		onHeldReleaseHit,
		onHeldPressMiss,
		onHeldReleaseMiss,
		row0,
		row1,
		row2,
		row3,
		row4,
		row5,
		row6,
		row7,
		row8,
		row9,
		row10,
		row11,
		row12,
		row13,
		row14,
		row15
#pragma warning restore CS1591
	}
}
