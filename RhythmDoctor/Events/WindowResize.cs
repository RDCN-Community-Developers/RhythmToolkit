using RhythmBase.Global.Components.Easing;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents a window resize event.  
	/// </summary>
	public class WindowResize : BaseEvent, IEaseEvent
	{
		private Tabs tab = Tabs.Actions;
		/// <inheritdoc/>
		public override EventType Type => EventType.WindowResize;
		/// <inheritdoc/>
		public override Tabs Tab => CustomTab;
		/// <summary>  
		/// Gets or sets the scale of the window resize event.  
		/// </summary>  
		[EaseProperty]
		public RDSizeE? Scale { get; set; } = new(1, 1);
		/// <summary>  
		/// Gets or sets the pivot point for the window resize event.  
		/// </summary>  
		[EaseProperty]
		public RDPointE? Pivot { get; set; } = new(50f, 50f);
		/// <summary>
		/// Gets or sets the custom tab.
		/// </summary>
		[RDJsonProperty("tab")]
		[RDJsonDefaultSerializer]
		[RDJsonCondition($"$&.{nameof(CustomTab)} is RhythmBase.RhythmDoctor.Events.Tabs.Windows")]
		public Tabs CustomTab
		{
			get => tab;
			set => tab = CustomTab is Tabs.Actions or Tabs.Windows ? value : throw new InvalidOperationException();
		}
		/// <inheritdoc/>
		public EaseType Ease { get; set; }
		/// <inheritdoc/>
		public float Duration { get; set; }
	}
}
