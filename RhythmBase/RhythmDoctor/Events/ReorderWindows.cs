using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that reorders the windows (window slots) in the Rhythm Doctor UI.
/// </summary>
public record class ReorderWindows : BaseWindowEvent
{
	/// <inheritdoc/>
	public override int Y => 0;

	/// <summary>
	/// Gets or sets the order of rooms used to rearrange windows.
	/// </summary>
	[RDJsonConverter(typeof(RoomOrderConverter))]
	public RoomOrder Order { get; set; } = new();
	///<inheritdoc/>
	public override EventType Type => EventType.ReorderWindows;
}
