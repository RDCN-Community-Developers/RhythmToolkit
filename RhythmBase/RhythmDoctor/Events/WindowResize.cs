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
	[RDJsonProperty("tab")]
	[RDJsonConverter(typeof(TabsConverter))]
	[RDJsonCondition($"$&.{nameof(CustomTab)} is RhythmBase.RhythmDoctor.Events.{nameof(Events.Tab)}.{nameof(Tab.Windows)}")]
	public Tab CustomTab
	{
		get;
		set => field = CustomTab is Tab.Actions or Tab.Windows ? value : throw new InvalidOperationException();
	} = Tab.Actions;
	/// <inheritdoc/>
	public EaseType Ease { get; set; } = EaseType.Linear;
	/// <inheritdoc/>
	public float Duration { get; set; } = 0f;
	/// <inheritdoc/>
	public RDRoom Rooms { get; set; }
}
/// <summary>
/// Specifies how the pivot is interpreted when applying a window resize.
/// </summary>
[RDJsonEnumSerializable]
public enum PivotMode
{
	/// <summary>
	/// Use the default pivot behavior. Typically uses the pivot value directly without special anchoring.
	/// </summary>
	Default,

	/// <summary>
	/// Treat the pivot as an edge anchor. Pivot operations will align content relative to the specified window edge.
	/// </summary>
	AnchorEdge,
}

/// <summary>
/// Describes which edge of the window the content should be anchored to when resizing.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowAnchorType
{
	/// <summary>
	/// No anchoring. Content is not anchored to any specific edge.
	/// </summary>
	None,

	/// <summary>
	/// Anchor to the left edge of the window.
	/// </summary>
	LeftEdge,

	/// <summary>
	/// Anchor to the right edge of the window.
	/// </summary>
	RightEdge,

	/// <summary>
	/// Anchor to the bottom edge of the window.
	/// </summary>
	BottomEdge,

	/// <summary>
	/// Anchor to the top edge of the window.
	/// </summary>
	TopEdge,
}