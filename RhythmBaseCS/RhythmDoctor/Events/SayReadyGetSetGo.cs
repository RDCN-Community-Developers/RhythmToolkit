using Newtonsoft.Json;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that says "Ready, Get Set, Go" with various voice sources and phrases.
	/// </summary>
	public class SayReadyGetSetGo : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SayReadyGetSetGo"/> class.
		/// </summary>
		public SayReadyGetSetGo()
		{
			Type = EventType.SayReadyGetSetGo;
			Tab = Tabs.Sounds;
		}

		/// <summary>
		/// Gets or sets the phrase to say.
		/// </summary>
		public Words PhraseToSay { get; set; }

		/// <summary>
		/// Gets or sets the voice source.
		/// </summary>
		public VoiceSources VoiceSource { get; set; } = VoiceSources.Nurse;

		/// <summary>
		/// Gets or sets the tick value.
		/// </summary>
		public float Tick { get; set; }

		/// <summary>
		/// Gets or sets the volume.
		/// </summary>
		public uint Volume { get; set; } = 100;

		/// <summary>
		/// Gets the event type.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Gets a value indicating whether the phrase is splitable.
		/// </summary>
		[JsonIgnore]
		public bool Splitable
		{
			get
			{
				return PhraseToSay == Words.SayReaDyGetSetGoNew || PhraseToSay == Words.SayGetSetGo || PhraseToSay == Words.SayReaDyGetSetOne || PhraseToSay == Words.SayGetSetOne || PhraseToSay == Words.SayReadyGetSetGo;
			}
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}: {1}", VoiceSource, PhraseToSay);

		/// <summary>
		/// Represents the phrases that can be said.
		/// </summary>
		public enum Words
		{
			/// <summary>
			/// Represents the phrase "Ready, Get Set, Go New".
			/// </summary>
			SayReaDyGetSetGoNew,
			/// <summary>
			/// Represents the phrase "Get Set, Go".
			/// </summary>
			SayGetSetGo,
			/// <summary>
			/// Represents the phrase "Ready, Get Set, One".
			/// </summary>
			SayReaDyGetSetOne,
			/// <summary>
			/// Represents the phrase "Get Set, One".
			/// </summary>
			SayGetSetOne,
			/// <summary>
			/// Represents the phrase "Rea".
			/// </summary>
			JustSayRea,
			/// <summary>
			/// Represents the phrase "Dy".
			/// </summary>
			JustSayDy,
			/// <summary>
			/// Represents the phrase "Get".
			/// </summary>
			JustSayGet,
			/// <summary>
			/// Represents the phrase "Set".
			/// </summary>
			JustSaySet,
			/// <summary>
			/// Represents the phrase "And".
			/// </summary>
			JustSayAnd,
			/// <summary>
			/// Represents the phrase "Go".
			/// </summary>
			JustSayGo,
			/// <summary>
			/// Represents the phrase "Stop".
			/// </summary>
			JustSayStop,
			/// <summary>
			/// Represents the phrase "And Stop".
			/// </summary>
			JustSayAndStop,
			/// <summary>
			/// Represents the count "1".
			/// </summary>
			Count1,
			/// <summary>
			/// Represents the count "2".
			/// </summary>
			Count2,
			/// <summary>
			/// Represents the count "3".
			/// </summary>
			Count3,
			/// <summary>
			/// Represents the count "4".
			/// </summary>
			Count4,
			/// <summary>
			/// Represents the count "5".
			/// </summary>
			Count5,
			/// <summary>
			/// Represents the count "6".
			/// </summary>
			Count6,
			/// <summary>
			/// Represents the count "7".
			/// </summary>
			Count7,
			/// <summary>
			/// Represents the count "8".
			/// </summary>
			Count8,
			/// <summary>
			/// Represents the count "9".
			/// </summary>
			Count9,
			/// <summary>
			/// Represents the count "10".
			/// </summary>
			Count10,
			/// <summary>
			/// Represents the phrase "Ready, Get Set, Go".
			/// </summary>
			SayReadyGetSetGo,
			/// <summary>
			/// Represents the phrase "Ready".
			/// </summary>
			JustSayReady
		}

		/// <summary>
		/// Represents the sources of the voice.
		/// </summary>
		public enum VoiceSources
		{
			/// <summary>
			/// Represents the voice source "Nurse".
			/// </summary>
			Nurse,
			/// <summary>
			/// Represents the voice source "Nurse Tired".
			/// </summary>
			NurseTired,
			/// <summary>
			/// Represents the voice source "Nurse Swing".
			/// </summary>
			NurseSwing,
			/// <summary>
			/// Represents the voice source "Nurse Swing Calm".
			/// </summary>
			NurseSwingCalm,
			/// <summary>
			/// Represents the voice source "Ian Excited".
			/// </summary>
			IanExcited,
			/// <summary>
			/// Represents the voice source "Ian Calm".
			/// </summary>
			IanCalm,
			/// <summary>
			/// Represents the voice source "Ian Slow".
			/// </summary>
			IanSlow,
			/// <summary>
			/// Represents the voice source "None Bottom".
			/// </summary>
			NoneBottom,
			/// <summary>
			/// Represents the voice source "None Top".
			/// </summary>
			NoneTop
		}
	}
}
