using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

public class HideWindow : BaseWindowEvent
{
  public override EventType Type => EventType.HideWindow;
  public bool Show { get; set; }
  [RDJsonCondition($"$&.{nameof(Transparent)}")]
  public bool Transparent { get; set; }
  [RDJsonCondition($"$&.{nameof(Frameless)}")]
  public bool Frameless { get; set; }
}
