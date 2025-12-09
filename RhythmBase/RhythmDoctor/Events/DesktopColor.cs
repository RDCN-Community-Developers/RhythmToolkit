using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that sets the simulated desktop background for the simulated desktop mode.
/// </summary>
public class DesktopColor : BaseWindowEvent, IEaseEvent
{
	///<inheritdoc/>
	public override int Y => 0;

	/// <summary>
	/// Optional start color for an eased transition.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(StartColor)} is not null")]
	public PaletteColor? StartColor { get; set; }

	/// <summary>
	/// Optional end (or sole) color for the desktop background.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(EndColor)} is not null")]
	public PaletteColor? EndColor { get; set; }
	///<inheritdoc/>
	public override EventType Type => EventType.DesktopColor;
	///<inheritdoc/>
	public EaseType Ease { get; set; }
	///<inheritdoc/>
	public float Duration { get; set; }
}
