using System;
using Newtonsoft.Json;
namespace RhythmBase.Events
{
	public class SayReadyGetSetGo : BaseEvent
	{
		public SayReadyGetSetGo()
		{
			Type = EventType.SayReadyGetSetGo;
			Tab = Tabs.Sounds;
		}

		public Words PhraseToSay { get; set; }

		public VoiceSources VoiceSource { get; set; }

		public float Tick { get; set; }

		public uint Volume { get; set; }

		public override EventType Type { get; }

		public override Tabs Tab { get; }

		[JsonIgnore]
		public bool Splitable
		{
			get
			{
				return PhraseToSay == Words.SayReaDyGetSetGoNew || PhraseToSay == Words.SayGetSetGo || PhraseToSay == Words.SayReaDyGetSetOne || PhraseToSay == Words.SayGetSetOne || PhraseToSay == Words.SayReadyGetSetGo;
			}
		}

		public override string ToString() => base.ToString() + string.Format(" {0}: {1}", VoiceSource, PhraseToSay);

		public enum Words
		{
			SayReaDyGetSetGoNew,
			SayGetSetGo,
			SayReaDyGetSetOne,
			SayGetSetOne,
			JustSayRea,
			JustSayDy,
			JustSayGet,
			JustSaySet,
			JustSayAnd,
			JustSayGo,
			JustSayStop,
			JustSayAndStop,
			Count1,
			Count2,
			Count3,
			Count4,
			Count5,
			Count6,
			Count7,
			Count8,
			Count9,
			Count10,
			SayReadyGetSetGo,
			JustSayReady
		}

		public enum VoiceSources
		{
			Nurse,
			NurseTired,
			NurseSwing,
			NurseSwingCalm,
			IanExcited,
			IanCalm,
			IanSlow,
			NoneBottom,
			NoneTop
		}
	}
}
