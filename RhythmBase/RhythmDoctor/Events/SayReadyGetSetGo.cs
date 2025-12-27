
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that says "Ready, Get Set, Go" with various voice sources and phrases.
/// </summary>
public class SayReadyGetSetGo : BaseEvent, IRoomEvent
{
	/// <summary>
	/// Gets or sets the phrase to say.
	/// </summary>
	public SayReadyGetSetGoWord PhraseToSay { get; set; } = SayReadyGetSetGoWord.SayReaDyGetSetGoNew;
	/// <summary>
	/// Gets or sets the voice source.
	/// </summary>
	public SayReadyGetSetGoVoiceSource VoiceSource { get; set; } = SayReadyGetSetGoVoiceSource.Nurse;
	/// <summary>
	/// Gets or sets the tick value.
	/// </summary>
	public float Tick
	{
		get => Splitable ? field : 1;
		set => field = value;
	} = 1;
	/// <summary>
	/// Gets or sets the volume.
	/// </summary>
	public int Volume { get; set; } = 100;
	/// <summary>
	/// Gets the event type.
	/// </summary>
	public override EventType Type => EventType.SayReadyGetSetGo;
	/// <summary>
	/// Gets the tab.
	/// </summary>
	public override Tab Tab => Tab.Sounds;
	/// <summary>
	/// Gets a value indicating whether the phrase is splitable.
	/// </summary>
	public bool Splitable => PhraseToSay is
		SayReadyGetSetGoWord.SayReaDyGetSetGoNew or
		SayReadyGetSetGoWord.SayGetSetGo or
		SayReadyGetSetGoWord.SayReaDyGetSetOne or
		SayReadyGetSetGoWord.SayGetSetOne or
		SayReadyGetSetGoWord.SayReadyGetSetGo;
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new(0);

	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>A string that represents the current object.</returns>
	public override string ToString() => base.ToString() + $" {VoiceSource}: {PhraseToSay}";
}