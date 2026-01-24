using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an action to set the counting sound in the rhythm base.
/// </summary>
public record class SetCountingSound : BaseRowAction
{
	/// <summary>
	/// Gets or sets the voice source for the counting sound.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(VoiceSource)} != RhythmBase.RhythmDoctor.Events.{nameof(CountingSoundVoiceSource)}.{nameof(CountingSoundVoiceSource.Custom)}")]
	public CountingSoundVoiceSource VoiceSource { get; set; } = CountingSoundVoiceSource.JyiCount;
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="SetCountingSound"/> is enabled.
	/// </summary>
	public bool Enabled { get; set; } = true;
	/// <summary>
	/// Gets or sets the subdivision offset for the counting sound.
	/// </summary>
	public float SubdivOffset { get; set; } = 0.5f;
	/// <summary>
	/// Gets or sets the volume of the counting sound.
	/// </summary>
	public int Volume { get; set; } = 100;
	/// <summary>
	/// Gets or sets the list of sounds for the counting sound.
	/// </summary>
	[RDJsonConverter(typeof(CountingSoundCollectionConverter))]
	[RDJsonCondition($"$&.{nameof(Enabled)} && $&.{nameof(VoiceSource)} == RhythmBase.RhythmDoctor.Events.{nameof(CountingSoundVoiceSource)}.{nameof(CountingSoundVoiceSource.Custom)}")]
	public RDAudio[] Sounds { get; set; } = [
		new RDAudio(){Filename = "Jyi - ChineseCount1" },
		new RDAudio(){Filename = "Jyi - ChineseCount2" },
		new RDAudio(){Filename = "Jyi - ChineseCount3" },
		new RDAudio(){Filename = "Jyi - ChineseCount4" },
		new RDAudio(){Filename = "Jyi - ChineseCount5" },
		new RDAudio(){Filename = "Jyi - ChineseCount6" },
		new RDAudio(){Filename = "Jyi - ChineseCount7" }
		];
	/// <summary>
	/// Gets the type of the event.
	/// </summary>
	public override EventType Type => EventType.SetCountingSound;

	/// <summary>
	/// Gets the tab associated with the event.
	/// </summary>
	public override Tab Tab => Tab.Sounds;
}
