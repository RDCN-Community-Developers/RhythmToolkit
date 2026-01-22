using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>  
/// Represents a window resize event.  
/// </summary>
public class WindowResize : BaseWindowEvent, IEaseEvent, IRoomEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.WindowResize;
	/// <inheritdoc/>
	public override Tab Tab => CustomTab;
	/// <summary>  
	/// Gets or sets the scale of the window resize event.  
	/// </summary>  
	[Tween]
	public RDSizeE? Scale { get; set; } = new(1, 1);
	/// <summary>
	/// Gets or sets the zoom mode applied to the content.
	/// </summary>
	public ZoomMode ZoomMode { get; set; } = ZoomMode.None;
	/// <summary>
	/// Gets or sets the mode used for pivot operations.
	/// </summary>
	public PivotMode PivotMode { get; set; } = PivotMode.Default;
	/// <summary>
	/// Gets or sets the edge of the window to which the content is anchored.
	/// </summary>
	public WindowAnchorType AnchorType { get; set; } = WindowAnchorType.LeftEdge;
	/// <summary>  
	/// Gets or sets the pivot point for the window resize event.  
	/// </summary>  
	[Tween]
	public RDPointE? Pivot { get; set; } = new(50f, 50f);
	/// <summary>
	/// Gets or sets the custom tab.
	/// </summary>
	[RDJsonAlias("tab")]
	[RDJsonConverter(typeof(TabsConverter))]
	[RDJsonCondition($"$&.{nameof(CustomTab)} is RhythmBase.RhythmDoctor.Events.{nameof(Events.Tab)}.{nameof(Tab.Windows)}")]
	public Tab CustomTab
	{
		get;
		set => field = value is Tab.Actions or Tab.Windows ? value : throw new InvalidOperationException();
	} = Tab.Actions;
	/// <inheritdoc/>
	public EaseType Ease { get; set; } = EaseType.Linear;
	/// <inheritdoc/>
	public float Duration { get; set; } = 0f;
	/// <inheritdoc/>
	public RDRoom Rooms { get; set; }
}