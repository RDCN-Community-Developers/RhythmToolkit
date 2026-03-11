using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
using System.Diagnostics;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that advances the text of the <see cref="FloatingText"/>.
/// This event is used to progress through the lines of the <see cref="FloatingText"/>.
/// The event can specify a duration for the fade-out effect when advancing the text,
/// allowing for a smooth transition between lines.
/// </summary>
[RDJsonObjectSerializable]
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
	/// <summary>
	/// Gets or sets the duration of the fade-out effect, in beats. A value of null indicates that the duration is not
	/// specified.
	/// </summary>
	/// <remarks>The duration must be a non-negaiive value if specified. If set to zero, the fade-out effect will not
	/// occur.</remarks>
	[RDJsonAlias("fadeOutDuration")]
	[RDJsonCondition($"$&.{nameof(Duration)} is not null")]
	public float? Duration { get; set; }
	/// <summary>
	/// Gets the ID of the parent floating text.
	/// </summary>
	[RDJsonAlias("id")]
	internal int Id => Parent?._id ?? -1;
	float IDurationEvent.Duration { get => Duration ?? -1; set => Duration = value; }
	/// <inheritdoc/>
	public override string ToString()
	{
		string[]? texts = Parent?.Splitted;
		int? index = Parent?.Children.IndexOf(this);
		return texts is not null && index is not null && texts.Length > index + 1
			? base.ToString() + $" \"{texts[index.Value + 1]}\""
			: base.ToString() + $" ?";
	}
	private string GetDebuggerDisplay() => ToString();
}