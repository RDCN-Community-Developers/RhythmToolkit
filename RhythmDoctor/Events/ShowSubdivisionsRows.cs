using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events
{
	[RDJsonEnumSerializable]
	public enum ShowSubdivisionsRowsMode
	{
		Mini,
		Normal,
	}
	public class ShowSubdivisionsRows : BaseEvent
	{
		public override EventType Type => EventType.ShowSubdivisionsRows;
		public override Tabs Tab => Tabs.Actions;
		public int Subdivisions { get; set; } = 1;
		[RDJsonCondition($"$&.{nameof(ArcAngle)} is not null")]
		public int? ArcAngle { get; set; } = null;
		public float SpinPerSecond { get; set; } = -100f;
		public ShowSubdivisionsRowsMode Mode { get; set; } = ShowSubdivisionsRowsMode.Mini;
	}
}
