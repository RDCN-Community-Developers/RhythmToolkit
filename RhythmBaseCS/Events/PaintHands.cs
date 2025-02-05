using RhythmBase.Components;
using RhythmBase.Components.Easing;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event to paint hands with specified properties.
	/// </summary>
	public class PaintHands : BaseEvent, IEaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PaintHands"/> class.
		/// </summary>
		public PaintHands()
		{
			TintColor = new PaletteColor(true);
			BorderColor = new PaletteColor(true);
			Rooms = new RDRoom(true, new byte[1]);
			Type = EventType.PaintHands;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets the tint color of the hands.
		/// </summary>
		[EaseProperty]
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
		[EaseProperty]
		public PaletteColor BorderColor { get; set; }

		/// <summary>
		/// Gets or sets the opacity of the hands.
		/// </summary>
		[EaseProperty]
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

		/// <summary>
		/// Specifies the border styles available for the hands.
		/// </summary>
		public enum Borders
		{
			/// <summary>
			/// No border.
			/// </summary>
			None,

			/// <summary>
			/// Outline border.
			/// </summary>
			Outline,

			/// <summary>
			/// Glow border.
			/// </summary>
			Glow
		}
	}
}
