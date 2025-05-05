using RhythmBase.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBase.Events
{
	public class ReorderSprite : BaseDecorationAction
	{
		/// <inheritdoc />
		public override EventType Type => throw new NotImplementedException();
		/// <inheritdoc />
		public override Tabs Tab => throw new NotImplementedException();
		public RDSingleRoom NewRoom { get; set; } = RDRoomIndex.Room1;
	}
}
