﻿using System;
using System.Text.Json.Serialization;
namespace RhythmBase.Events
{
	public class SetVisible : BaseDecorationAction
	{
		public SetVisible()
		{
			Type = EventType.SetVisible;
			Tab = Tabs.Decorations;
		}
		public override EventType Type { get; }
		public override Tabs Tab { get; }
		public bool Visible { get; set; }
		public override string ToString() => base.ToString() + string.Format(" {0}", Visible);
	}
}
