using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents the base class for decoration actions.
/// </summary>
[JsonObjectHasSerializer(typeof(RDMemberConverter.BaseDecorationAction<>))]
public abstract record class BaseDecorationAction : BaseEvent, IBaseEvent
{
	public BaseDecorationAction(BaseDecorationAction source) : base(source)
	{
		Target = source.Target;
	}
	/// <inheritdoc/>
	public override Tab Tab => Tab.Decorations;
	/// <summary>
	/// Gets the target identifier.
	/// </summary>
	public virtual string? Target
	{
		get; set
		{
			if (_tick.BaseChart is not null)
				throw new InvalidOperationException($"The property {nameof(Target)} is readonly because it has been added to the chart.");
			field = value;
		}
	}
	public Decoration? Parent => Target is null ? null : TickTime.BaseChart?.Decorations[Target];
	///<inheritdoc/>
	public override TEvent CloneAs<TEvent>()
	{
		TEvent temp = base.CloneAs<TEvent>();
		if (temp is BaseDecorationAction baseDecorationAction)
			baseDecorationAction.Target = Target;
		return temp;
	}
}
