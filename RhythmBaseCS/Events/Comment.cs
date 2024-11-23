using Newtonsoft.Json;
using RhythmBase.Components;

namespace RhythmBase.Events
{
	/// <inheritdoc />
	public partial class Comment : BaseDecorationAction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Comment"/> class.
		/// </summary>
		public Comment()
		{
			Text = "";
			Color = new PaletteColor(false)
			{
				Color = RDColor.FromRgba(242, 230, 68)
			};
			Type = EventType.Comment;
		}

		/// <summary>
		/// Gets or sets the custom tab.
		/// </summary>
		[JsonProperty("tab")]
		public Tabs CustomTab { get; set; }

		/// <inheritdoc />
		[JsonIgnore]
		public override Tabs Tab => CustomTab;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Comment"/> is shown.
		/// </summary>
		public bool Show { get; set; }

		/// <summary>
		/// Gets or sets the text of the comment.
		/// </summary>
		public string Text { get; set; }

		/// <inheritdoc />
		public override string Target => base.Target;

		/// <summary>
		/// Gets or sets the color of the comment.
		/// </summary>
		public PaletteColor Color { get; set; }

		/// <inheritdoc />
		public override EventType Type { get; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", Text);
	}
}
