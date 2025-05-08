using Newtonsoft.Json;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a Tint event which is a type of BaseDecorationAction and implements IEaseEvent.
	/// </summary>
	public class Tint : BaseDecorationAction, IEaseEvent, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Tint"/> class.
		/// </summary>
		public Tint()
		{
			BorderColor = new PaletteColor(true);
			TintColor = new PaletteColor(true)
			{
				Color = RDColor.White
			};
			Type = EventType.Tint;
			Tab = Tabs.Decorations;
		}

		/// <summary>
		/// Gets or sets the ease type for the tint event.
		/// </summary>
		public EaseType Ease { get; set; }

		/// <summary>
		/// Gets or sets the border type for the tint event.
		/// </summary>
		public Borders Border { get; set; }

		/// <summary>
		/// Gets or sets the border color for the tint event.
		/// </summary>
		[EaseProperty]
		public PaletteColor BorderColor { get; set; }

		/// <summary>
		/// Gets or sets the opacity for the tint event.
		/// </summary>
		[EaseProperty]
		public int Opacity { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this event is a tint.
		/// </summary>
		[JsonProperty("tint")]
		public bool IsTint { get; set; }

		/// <summary>
		/// Gets or sets the tint color for the tint event.
		/// </summary>
		[EaseProperty]
		public PaletteColor TintColor { get; set; }

		/// <summary>
		/// Gets or sets the duration of the tint event.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab where the event is categorized.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}{1}", Border, Border == Borders.None ? "" : ":" + BorderColor.ToString());
	}
}
