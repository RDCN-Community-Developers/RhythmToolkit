using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events
{
    internal class SetMainWindow : BaseWindowEvent
    {
        public override EventType Type => EventType.SetMainWindow;
	}
}
