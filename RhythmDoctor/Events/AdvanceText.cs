using RhythmBase.RhythmDoctor.Components;
using System.Diagnostics;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that advances text in a room.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public class AdvanceText : BaseEvent, IRoomEvent, IDurationEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AdvanceText"/> class.
		/// </summary>
		public AdvanceText() { }
		/// <inheritdoc/>
		public override EventType Type => EventType.AdvanceText;
		/// <inheritdoc/>
		[RDJsonIgnore]
		public RDRoom Rooms
		{
			get => Parent.Rooms;
			set => Parent.Rooms = value;
		}
		/// <inheritdoc/>
		public override Tabs Tab => Tabs.Actions;
		/// <summary>
		/// Gets or sets the parent floating text associated with the event.
		/// </summary>
		[RDJsonIgnore]
		public FloatingText Parent { get; internal set; } = new();
		/// <summary>
		/// Gets or sets the fade-out duration for the text.
		/// </summary>
		[RDJsonCondition($"$&.{nameof(FadeOutDuration)} != 0")]
		public float FadeOutDuration { get; set; }
		float IDurationEvent.Duration { get => FadeOutDuration; set => FadeOutDuration = value; }
		/// <summary>
		/// Gets the ID of the parent floating text.
		/// </summary>
		[RDJsonNotIgnore]
		internal int Id => Parent.Id;
		/// <inheritdoc/>
		public override string ToString() => base.ToString() + $" Index:{Parent.Children.IndexOf(this)}";
		private string GetDebuggerDisplay() => ToString();
	}
}