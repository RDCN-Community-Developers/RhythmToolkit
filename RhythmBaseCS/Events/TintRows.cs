using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents an event that tints rows with specified colors and effects.
	/// </summary>
	public class TintRows : BaseRowAnimation, IEaseEvent, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TintRows"/> class.
		/// </summary>
		public TintRows()
		{
			TintColor = new PaletteColor(true);
			BorderColor = new PaletteColor(true);
			Type = EventType.TintRows;
			Tab = Tabs.Actions;
		}

		/// <summary>
		/// Gets or sets the tint color.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public PaletteColor TintColor { get; set; }

		/// <summary>
		/// Gets or sets the easing type for the animation.
		/// </summary>
		public EaseType Ease { get; set; }

		/// <summary>
		/// Gets or sets the border style.
		/// </summary>
		public Borders Border { get; set; }

		/// <summary>
		/// Gets or sets the border color.
		/// </summary>
		[EaseProperty]
		public PaletteColor BorderColor { get; set; }

		/// <summary>
		/// Gets or sets the opacity level.
		/// </summary>
		[EaseProperty]
		public int Opacity { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to apply tint.
		/// </summary>
		public bool Tint { get; set; }

		/// <summary>
		/// Gets or sets the duration of the tint effect.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Gets or sets the row effect.
		/// </summary>
		public RowEffect Effect { get; set; }

		/// <summary>
		/// Gets the event type.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab category.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Gets a value indicating whether to tint all rows.
		/// </summary>
		[JsonIgnore]
		public bool TintAll
		{
			get
			{
				return Parent != null;
			}
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}{1}", Border, (Border == Borders.None) ? "" : (":" + BorderColor.ToString()));

		/// <summary>
		/// Specifies the row effects.
		/// </summary>
		public enum RowEffect
		{
			/// <summary>
			/// No effect.
			/// </summary>
			None,

			/// <summary>
			/// Electric effect.
			/// </summary>
			Electric,

			/// <summary>
			/// Smoke effect.
			/// </summary>
			Smoke
		}
	}
}
