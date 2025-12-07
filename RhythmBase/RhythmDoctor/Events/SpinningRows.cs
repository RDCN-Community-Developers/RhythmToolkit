using RhythmBase.Global.Components.Easing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Events;
/// <summary>
/// Represents an event that applies a spinning rows animation with configurable easing and duration.[Not used]
/// </summary>
public class SpinningRows : BaseWindowEvent, IEaseEvent
{
	public string Text { get; set; } = "";
	public EaseType Ease { get; set; }
	public float Duration { get; set; }
	public override EventType Type => EventType.SpinningRows;
	public override Tabs Tab => Tabs.Windows;
}