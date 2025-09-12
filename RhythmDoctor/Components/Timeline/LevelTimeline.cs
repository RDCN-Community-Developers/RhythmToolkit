using RhythmBase.RhythmDoctor.Components.TimeLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Components.Timeline
{
	public class LevelTimeline
	{
		public RowTimeline[] Rows { get; internal set; }
		public DecorationTimeline[] Decorations { get; internal set; }
		public RoomTimeline[] Rooms { get; internal set; }
	}
}
