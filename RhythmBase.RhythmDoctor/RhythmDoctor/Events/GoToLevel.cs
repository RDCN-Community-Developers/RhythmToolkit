using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

[JsonObjectSerializable]
public record class GoToLevel : BaseEvent, IChartFileEvent
{
	public override EventType Type => EventType.GoToLevel;
	public override Tab Tab => Tab.Actions;
	public GoToLevelAction Action { get; set; }
	[JsonAlias("rdlevel")]
	[JsonCondition($"$&.{nameof(Action)} is not {nameof(GoToLevelAction)}.{nameof(GoToLevelAction.LoadNext)}")]
	public FileReference Chart { get; set; }
	[JsonCondition($"$&.{nameof(Action)} is not {nameof(GoToLevelAction)}.{nameof(GoToLevelAction.SetNext)}")]
	public bool Skippable { get; set; }
	[JsonCondition($"!$&.{nameof(Chart)}.{nameof(FileReference.IsEmpty)}")]
	public bool FadeOut { get; set; }
	[JsonCondition($"!$&.{nameof(Chart)}.{nameof(FileReference.IsEmpty)}")]
	public bool StartImmediately { get; set; }
	[JsonCondition($"!$&.{nameof(Chart)}.{nameof(FileReference.IsEmpty)}")]
	public bool KeepMistakes { get; set; }
	[JsonCondition($"!$&.{nameof(Chart)}.{nameof(FileReference.IsEmpty)}")]
	public bool DontUpdateRestart { get; set; }
	IEnumerable<FileReference> IChartFileEvent.ChartFiles => Chart.IsEmpty ? [] : [Chart];
	IEnumerable<FileReference> IFileEvent.Files => Chart.IsEmpty ? [] : [Chart];
}
