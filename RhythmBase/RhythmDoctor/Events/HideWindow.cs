using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that hides or shows a specific window.
/// </summary>
public class HideWindow : BaseWindowEvent
{
  /// <inheritdoc />
  public override EventType Type => EventType.HideWindow;

  /// <summary>
  /// Gets or sets a value indicating whether the target window should be shown.
  /// </summary>
  public bool Show { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the target window should be rendered as transparent.
  /// </summary>
  [RDJsonCondition($"$&.{nameof(Transparent)}")]
  public bool Transparent { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the target window should be frameless (no window chrome).
  /// </summary>
  [RDJsonCondition($"$&.{nameof(Frameless)}")]
  public bool Frameless { get; set; }
}
