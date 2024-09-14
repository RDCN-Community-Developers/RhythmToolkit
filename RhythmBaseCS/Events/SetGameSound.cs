using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RhythmBase.Assets;
using RhythmBase.Components;
using RhythmBase.Converters;

namespace RhythmBase.Events
{

	public class SetGameSound : BaseEvent
	{

		public SetGameSound()
		{
			Audio = new Audio();
			Type = EventType.SetGameSound;
			Tab = Tabs.Sounds;
		}


		private Audio Audio { get; set; }


		public SoundTypes SoundType { get; set; }


		public string Filename
		{
			get
			{
				return Audio.Name;
			}
			set
			{
				Audio.Name = value;
			}
		}


		public int Volume
		{
			get
			{
				return Audio.Volume;
			}
			set
			{
				Audio.Volume = value;
			}
		}


		public int Pitch
		{
			get
			{
				return Audio.Pitch;
			}
			set
			{
				Audio.Pitch = value;
			}
		}


		public int Pan
		{
			get
			{
				return Audio.Pan;
			}
			set
			{
				Audio.Pan = value;
			}
		}


		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan Offset
		{
			get
			{
				return Audio.Offset;
			}
			set
			{
				Audio.Offset = value;
			}
		}


		public List<SoundSubType> SoundSubtypes { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		private bool ShouldSerialize() => !(SoundType == SoundTypes.ClapSoundHold | SoundType == SoundTypes.FreezeshotSound | SoundType == SoundTypes.BurnshotSound);


		public bool ShouldSerializeFilename() => ShouldSerialize();


		public bool ShouldSerializeVolume() => ShouldSerialize();


		public bool ShouldSerializePitch() => ShouldSerialize();


		public bool ShouldSerializePan() => ShouldSerialize();


		public bool ShouldSerializeOffset() => ShouldSerialize();


		public override string ToString() => base.ToString() + string.Format(" {0}", SoundType);


		public enum SoundTypes
		{

			SmallMistake,

			BigMistake,

			Hand1PopSound,

			Hand2PopSound,

			HeartExplosion,

			HeartExplosion2,

			HeartExplosion3,

			Skipshot,

			ClapSoundHold,

			FreezeshotSound,

			BurnshotSound
		}
	}
}
