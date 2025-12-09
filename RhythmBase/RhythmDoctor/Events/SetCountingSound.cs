using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents the different voice sources for the counting sound.
/// </summary>
[RDJsonEnumSerializable]
public enum CountingSoundVoiceSource
{
	/// <summary>
	/// Jyi Count
	/// </summary>
	JyiCount,
	/// <summary>
	/// Jyi Count Fast
	/// </summary>
	JyiCountFast,
	/// <summary>
	/// Jyi Count Calm
	/// </summary>
	JyiCountCalm,
	/// <summary>
	/// Jyi Count Tired
	/// </summary>
	JyiCountTired,
	/// <summary>
	/// Jyi Count Very Tired
	/// </summary>
	JyiCountVeryTired,
	/// <summary>
	/// Jyi Count Japanese
	/// </summary>
	JyiCountJapanese,
	/// <summary>
	/// Ian Count
	/// </summary>
	IanCount,
	/// <summary>
	/// Ian Count Fast
	/// </summary>
	IanCountFast,
	/// <summary>
	/// Ian Count Calm
	/// </summary>
	IanCountCalm,
	/// <summary>
	/// Ian Count Slow
	/// </summary>
	IanCountSlow,
	/// <summary>
	/// Ian Count Slower
	/// </summary>
	IanCountSlower,
	/// <summary>
	/// Whistle Count
	/// </summary>
	WhistleCount,
	/// <summary>
	/// Bird Count
	/// </summary>
	BirdCount,
	/// <summary>
	/// Parrot Count
	/// </summary>
	ParrotCount,
	/// <summary>
	/// Owl Count
	/// </summary>
	OwlCount,
	/// <summary>
	/// Oriole Count
	/// </summary>
	OrioleCount,
	/// <summary>
	/// Wren Count
	/// </summary>
	WrenCount,
	/// <summary>
	/// Canary Count
	/// </summary>
	CanaryCount,
	/// <summary>
	/// Spear Count
	/// </summary>
	SpearCount,
	/// <summary>
	/// Jyi Count Legacy
	/// </summary>
	JyiCountLegacy,
	/// <summary>
	/// Jyi Count English
	/// </summary>
	JyiCountEnglish,
	/// <summary>
	/// Ian Count English
	/// </summary>
	IanCountEnglish,
	/// <summary>
	/// Ian Count English Calm
	/// </summary>
	IanCountEnglishCalm,
	/// <summary>
	/// Ian Count English Slow
	/// </summary>
	IanCountEnglishSlow,
	/// <summary>
	/// Custom
	/// </summary>
	Custom
}
/// <summary>
/// Represents an action to set the counting sound in the rhythm base.
/// </summary>
//[RDJsonObjectNotSerializable]
public class SetCountingSound : BaseRowAction
{
	/// <summary>
	/// Gets or sets the voice source for the counting sound.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(VoiceSource)} != RhythmBase.RhythmDoctor.Events.CountingSoundVoiceSources.Custom")]
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
	[RDJsonCondition($"$&.{nameof(Enabled)} && $&.{nameof(VoiceSource)} == RhythmBase.RhythmDoctor.Events.CountingSoundVoiceSources.Custom")]
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
