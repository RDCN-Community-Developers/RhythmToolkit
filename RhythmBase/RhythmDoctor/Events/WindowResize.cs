using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents a window resize event.  
	/// </summary>
	public class WindowResize : BaseEvent, IEaseEvent, IRoomEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.WindowResize;
		/// <inheritdoc/>
		public override Tabs Tab => CustomTab;
		/// <summary>  
		/// Gets or sets the scale of the window resize event.  
		/// </summary>  
		[Tween]
		public RDSizeE? Scale { get; set; } = new(1, 1);
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
		[RDJsonCondition($"$&.{nameof(CustomTab)} is RhythmBase.RhythmDoctor.Events.Tabs.Windows")]
		public Tabs CustomTab
		{
			get;
			set => field = CustomTab is Tabs.Actions or Tabs.Windows ? value : throw new InvalidOperationException();
		} = Tabs.Actions;
		/// <inheritdoc/>
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <inheritdoc/>
		public float Duration { get; set; } = 0f;
		/// <inheritdoc/>
		public RDRoom Rooms { get; set; }
	}
}
