using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that sets the main application window.
/// </summary>
public record class SetMainWindow : BaseWindowEvent
{
	///<inheritdoc/>
	public override EventType Type => EventType.SetMainWindow;
}
