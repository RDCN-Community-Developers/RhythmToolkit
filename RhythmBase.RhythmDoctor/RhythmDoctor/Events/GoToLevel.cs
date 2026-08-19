using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

[JsonObjectHasSerializer(typeof(Serialization.RDMemberConverter.GoToLevel))]
public record class GoToLevel : BaseEvent, IChartFileEvent
{
	public override EventType Type => EventType.GoToLevel;
	public override Tab Tab => Tab.Actions;
	public GoToLevelAction Action { get; set; }
	public FileReference Chart { get; set; }
	public RhythmDoctor.Components.Chart? ResolvedLevel { get; set; }
	public bool Skippable { get; set; }
	public bool FadeOut { get; set; }
	public bool StartImmediately { get; set; }
	public bool KeepMistakes { get; set; }
	public bool DontUpdateRestart { get; set; }
	IEnumerable<FileReference> IChartFileEvent.ChartFiles => Chart.IsEmpty ? [] : [Chart];
	IEnumerable<FileReference> IFileEvent.Files => Chart.IsEmpty ? [] : [Chart];
}
