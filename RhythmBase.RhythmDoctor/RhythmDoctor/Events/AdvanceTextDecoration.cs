using RhythmBase.RhythmDoctor.Extensions;

namespace RhythmBase.RhythmDoctor.Events;

public record class AdvanceTextDecoration : BaseDecorationAction, IAdvanceText
{
	/// <inheritdoc/>
	public override EventType Type { get; } = EventType.AdvanceTextDecoration;
	/// <summary>
	/// Gets or sets the duration of the fade-out effect, in beats. A value of null indicates that the duration is not
	/// specified.
	/// </summary>
	/// <remarks>The duration must be a non-negaiive value if specified. If set to zero, the fade-out effect will not
	/// occur.</remarks>
	[JsonAlias("fadeOutDuration")]
	public float? Duration { get; set; }
	float IDurationEvent.Duration { get => Duration ?? this.FrontOrDefault<SetText>()?.Duration ?? -1; set => Duration = value; }
}