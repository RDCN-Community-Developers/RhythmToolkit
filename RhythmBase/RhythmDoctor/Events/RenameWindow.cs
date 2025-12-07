using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

public class RenameWindow : BaseWindowEvent
{
	public string Text { get; set; } = "";
	public WindowNameAction Action { get; set; } = WindowNameAction.Set;
	public override EventType Type => EventType.RenameWindow;
	public override Tabs Tab { get; }
}
[RDJsonEnumSerializable]
public enum WindowNameAction
{
	Set,
	Append,
	Reset,
}