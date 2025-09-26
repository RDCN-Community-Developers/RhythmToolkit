using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
		public FloatingText? Parent { get; internal set; }
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
		internal int Id => Parent?.Id ?? -1;
		/// <inheritdoc/>
		public override string ToString()
		{
			string[]? texts = Parent?.Texts();
			int? index = Parent?.Children.IndexOf(this);
			if (texts is not null && index is not null && texts.Length > index + 1)
			{
				return base.ToString() + $" \"{texts[index.Value + 1]}\"";
			}
			return base.ToString() + $" ?";
		}

		private string GetDebuggerDisplay() => ToString();
	}
}