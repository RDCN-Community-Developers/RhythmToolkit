using RhythmBase.RhythmDoctor.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Config
{
	internal class GlobalConfig
	{
		internal const BeatChangeStrategy DefaultStrategy = BeatChangeStrategy.Default;
		public static BeatChangeStrategy Strategy = DefaultStrategy;
	}
}
