using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <inheritdoc />
	[JsonConverter(typeof(BaseEventConverter))]
	public partial class Comment : BaseDecorationAction, IColorEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Comment"/> class.
		/// </summary>
		public Comment() { }
		/// <inheritdoc />
		public override int Y
		{
			get => Tab == Tabs.Decorations ? base.Y : field;
			set
			{
				if (Tab != Tabs.Decorations)
					field = value;
			}
		}
		/// <summary>
		/// Gets or sets the custom tab.
		/// </summary>
		[RDJsonProperty(name: "tab")]
		[RDJsonDefaultSerializer]
		public Tabs CustomTab { get; set; }
		/// <inheritdoc />
		public override Tabs Tab => CustomTab;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Comment"/> is shown.
		/// </summary>
		public bool Show { get; set; } = false;
		/// <summary>
		/// Gets or sets the text of the comment.
		/// </summary>
		public string Text { get; set; } = "";
		/// <inheritdoc />
		public override string Target => base.Target;
		/// <summary>
		/// Gets or sets the color of the comment.
		/// </summary>
		public PaletteColor Color { get; set; } = new RDColor(0xFFF2E644u);
		/// <inheritdoc />
		public override EventType Type { get; } = EventType.Comment;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", Text);
	}
}
