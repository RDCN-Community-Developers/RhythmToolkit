using System;

namespace RhythmBase.Events
{

	public class SetOneshotWave : BaseBeat
	{

		public SetOneshotWave()
		{
			Type = EventType.SetOneshotWave;
		}


		public Waves WaveType { get; set; }


		public int Height { get; set; }


		public int Width { get; set; }


		public override EventType Type { get; }


		public enum Waves
		{

			BoomAndRush,

			Ball,

			Spring,

			Spike,

			SpikeHuge,

			Single
		}
	}
}
