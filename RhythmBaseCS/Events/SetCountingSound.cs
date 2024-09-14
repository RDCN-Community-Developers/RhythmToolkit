using System;
using RhythmBase.Assets;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public class SetCountingSound : BaseRowAction
	{

		public SetCountingSound()
		{
			Volume = 100;
			Sounds = new LimitedList<Audio>(7U, new Audio());
			Type = EventType.SetCountingSound;
			Tab = Tabs.Sounds;
		}


		public VoiceSources VoiceSource { get; set; }


		public bool Enabled { get; set; }


		public float SubdivOffset { get; set; }


		public int Volume { get; set; }


		public LimitedList<Audio> Sounds { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		public bool ShouldSerializeSounds() => VoiceSource == VoiceSources.Custom;


		public enum VoiceSources
		{

			JyiCount,

			JyiCountFast,

			JyiCountCalm,

			JyiCountTired,

			JyiCountVeryTired,

			JyiCountJapanese,

			IanCount,

			IanCountFast,

			IanCountCalm,

			IanCountSlow,

			IanCountSlower,

			WhistleCount,

			BirdCount,

			ParrotCount,

			OwlCount,

			OrioleCount,

			WrenCount,

			CanaryCount,

			JyiCountLegacy,

			JyiCountEnglish,

			IanCountEnglish,

			IanCountEnglishCalm,

			IanCountEnglishSlow,

			Custom
		}
	}
}
