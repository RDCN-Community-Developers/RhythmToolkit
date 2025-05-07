using Newtonsoft.Json;
using RhythmBase.Components;
using System.Diagnostics;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event that advances text in a room.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public class AdvanceText : BaseEvent, IRoomEvent,IDurationEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AdvanceText"/> class.
		/// </summary>
		public AdvanceText() { }
		/// <inheritdoc/>
		public override EventType Type => EventType.AdvanceText;
		/// <inheritdoc/>
		[JsonIgnore]
		public RDRoom Rooms
		{
			get => Parent.Rooms;
			set => Parent.Rooms = value;
		}
		/// <inheritdoc/>
		public override Tabs Tab => Tabs.Actions;		/// <summary>
		/// Gets or sets the parent floating text associated with the event.
		/// </summary>
		[JsonIgnore]
		public FloatingText Parent { get; internal set; } = new();		/// <summary>
		/// Gets or sets the fade-out duration for the text.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public float FadeOutDuration { get; set; }
		float IDurationEvent.Duration => FadeOutDuration;
		/// <summary>
		/// Gets the ID of the parent floating text.
		/// </summary>
		[JsonProperty]
		private int Id => Parent.Id;
		/// <inheritdoc/>
		public override string ToString() => base.ToString() + $" Index:{Parent.Children.IndexOf(this)}";
		private string GetDebuggerDisplay() => ToString();
	}
}
