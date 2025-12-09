using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a summary or description for the associated code element.
/// </summary>
[JsonConverter(typeof(BaseEventConverter))]
public partial class Comment : BaseDecorationAction, IColorEvent
{
	/// <inheritdoc />
	public override int Y
	{
		get => Tab == Tab.Decorations ? base.Y : field;
		set
		{
			if (Tab != Tab.Decorations)
				field = value;
		}
	}
	/// <summary>
	/// Gets or sets the custom tab.
	/// </summary>
	[RDJsonProperty(name: "tab")]
	[RDJsonConverter(typeof(TabsConverter))]
	public Tab CustomTab { get; set; }
	/// <inheritdoc />
	public override Tab Tab => CustomTab;
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Comment"/> is shown.
	/// </summary>
	public bool Show { get; set; } = false;
	/// <summary>
	/// Gets or sets the text of the comment.
	/// </summary>
	public string Text { get; set; } = "";
	/// <summary>
	/// Gets or sets the color of the comment.
	/// </summary>
	public PaletteColor Color { get; set; } = new RDColor(0xFFF2E644u);
	/// <inheritdoc />
	public override EventType Type => EventType.Comment;
	///<inheritdoc/>
	public override string ToString() => base.ToString() + $" {Text}";
}
