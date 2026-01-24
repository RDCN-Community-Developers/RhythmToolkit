using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
using System.Diagnostics;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that advances text in a room.
/// </summary>
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public record class AdvanceText : BaseEvent, IRoomEvent, IDurationEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.AdvanceText;
	/// <inheritdoc/>
	[RDJsonIgnore]
	public RDRoom Rooms
	{
		get => Parent?.Rooms ?? new();
		set => Parent?.Rooms = value;
	}
	/// <inheritdoc/>
	public override Tab Tab => Tab.Actions;
	/// <summary>
	/// Gets or sets the parent floating text associated with the event.
	/// </summary>
	[RDJsonIgnore]
	public FloatingText? Parent { get; internal set; }
	///<inheritdoc/>
	[RDJsonAlias("fadeOutDuration")]
	[RDJsonCondition($"$&.{nameof(Duration)} != 0")]
	public float Duration { get; set; }
	/// <summary>
	/// Gets the ID of the parent floating text.
	/// </summary>
	[RDJsonNotIgnore]
	internal int Id => Parent?._id ?? -1;
	/// <inheritdoc/>
	public override string ToString()
	{
		string[]? texts = Parent?.Splitted;
		int? index = Parent?.Children.IndexOf(this);
		if (texts is not null && index is not null && texts.Length > index + 1)
		{
			return base.ToString() + $" \"{texts[index.Value + 1]}\"";
		}
		return base.ToString() + $" ?";
	}
	private string GetDebuggerDisplay() => ToString();
}