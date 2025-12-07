using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events
{
	public class ReorderWindows : BaseWindowEvent
	{
		/// <inheritdoc/>
		public override int Y => 0;
		[RDJsonConverter(typeof(RoomOrderConverter))]
		public RoomOrder RoomOrder { get; set; } = new();
		public override EventType Type => EventType.ReorderWindows;
	}
}
