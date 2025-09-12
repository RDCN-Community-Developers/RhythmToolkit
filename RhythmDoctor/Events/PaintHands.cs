using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to paint hands with specified properties.
	/// </summary>
	public class PaintHands : BaseEvent, IEaseEvent, IRoomEvent, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PaintHands"/> class.
		/// </summary>
		public PaintHands()
		{
			TintColor = new PaletteColor(true);
			BorderColor = new PaletteColor(true);
			Rooms = new RDRoom([0]);
			Type = EventType.PaintHands;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets the tint color of the hands.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Tint)}")]
		public PaletteColor TintColor { get; set; }
		/// <summary>
		/// Gets or sets the easing type for the event.
		/// </summary>
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the border style of the hands.
		/// </summary>
		public Borders Border { get; set; }
		/// <summary>
		/// Gets or sets the border color of the hands.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Border)} != RhythmBase.RhythmDoctor.Events.Borders.None")]
		public PaletteColor BorderColor { get; set; }
		/// <summary>
		/// Gets or sets the opacity of the hands.
		/// </summary>
		[Tween]
		public int Opacity { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the hands should be tinted.
		/// </summary>
		public bool Tint { get; set; }
		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		public float Duration { get; set; }
		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; }
		/// <summary>
		/// Gets or sets the player hands associated with the event.
		/// </summary>
		public PlayerHands Hands { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab category of the event.
		/// </summary>
		public override Tabs Tab { get; }
	}
}
