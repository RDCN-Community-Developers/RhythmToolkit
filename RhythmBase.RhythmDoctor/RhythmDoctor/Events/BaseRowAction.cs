using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a base class for a row event.
/// </summary>
[JsonObjectHasSerializer(typeof(RDMemberConverter.BaseRowAction<>))]
public abstract record class BaseRowAction : BaseEvent
{
	public Row? Parent => TickTime.BaseChart is Level chart && Row >= 0 && Row < chart.Rows.Count ? chart.Rows[Row] : null;
	public BaseRowAction(BaseRowAction source) : base(source)
	{
		Row = source.Row;
	}
	///<inheritdoc/>
	public override TEvent CloneAs<TEvent>()
	{
		TEvent temp = base.CloneAs<TEvent>();
		if (temp is BaseRowAction tempAction)
			tempAction.Row = Row;
		return temp;
	}
	public int Row { get; set
		{
			if (_tick.BaseChart is not null)
				throw new InvalidOperationException($"The property {nameof(Row)} is readonly because it has been added to the chart.");
			field = value;
		}
	}
}
