using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Adofai.Events
{
	public class FreeRoamWarning : BaseTileEvent
	{
		public override EventType Type => EventType.FreeRoamWarning;
		public RDPointN Position { get; set; } = new(1, 0);
	}
}
