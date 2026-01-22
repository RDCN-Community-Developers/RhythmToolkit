using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a floating text event in a room.
/// </summary>
public class FloatingText : BaseEvent, IRoomEvent, IDurationEvent, IColorEvent
{
	/// <summary>
	/// Gets the type of the event.
	/// </summary>
	public override EventType Type => EventType.FloatingText;
	/// <summary>
	/// Gets the tab associated with the event.
	/// </summary>
	public override Tab Tab => Tab.Actions;
	/// <summary>
	/// Gets the list of child advance texts.
	/// </summary>
	[RDJsonIgnore]
	public List<AdvanceText> Children => _children;
	/// <summary>
	/// Gets or sets the room associated with the event.
	/// </summary>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets the fade out rate of the text.
	/// </summary>
	[RDJsonAlias("fadeOutRate")]
	public float Duration { get; set; }
	/// <summary>
	/// Gets or sets the color of the text.
	/// </summary>
	public PaletteColor Color { get; set; } = RDColor.White;
	/// <summary>
	/// Gets or sets the angle of the text.
	/// </summary>
	public float Angle { get; set; } = 0;
	/// <summary>
	/// Gets or sets the size of the text.
	/// </summary>
	public int Size { get; set; } = 8;
	/// <summary>
	/// Gets or sets the outline color of the text.
	/// </summary>
	public PaletteColor OutlineColor { get; set; } = RDColor.Black;
	/// <summary>
	/// Gets the ID of the event.
	/// </summary>
	[RDJsonNotIgnore]
	[RDJsonAlias("id")]
	internal int _id => _beat.BaseLevel?._floatingTexts.IndexOf(this) ?? -1;
	/// <summary>
	/// Gets or sets the position of the text.
	/// </summary>
	public RDPoint TextPosition { get; set; } = new(50f, 50f);
	/// <summary>
	/// Gets or sets the anchor style of the text.
	/// </summary>
	[RDJsonConverter(typeof(FloatingTextAnchorStylesConverter))]
	public FloatingTextAnchorStyle Anchor { get; set; } = FloatingTextAnchorStyle.Center;
	/// <summary>
	/// Gets or sets a value indicating whether to narrate the text.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Narrate)}")]
	public bool Narrate { get; set; } = true;
	/// <summary>
	/// Gets or sets the narration category of the text.
	/// </summary>
	[RDJsonCondition($"$&.{nameof(Narrate)}")]
	public NarrationCategory NarrationCategory { get; set; } = NarrationCategory.Subtitles;
	/// <summary>
	/// Gets or sets the mode of the text.
	/// </summary>
	public FloatingTextFadeOutMode Mode { get; set; } = FloatingTextFadeOutMode.FadeOut;
	/// <summary>
	/// Gets or sets a value indicating whether to show child texts.
	/// </summary>
	public bool ShowChildren { get; set; } = true;
	/// <summary>
	/// Gets or sets the text content.
	/// </summary>
	public string Text { get; set; } = "等/呀/等/得/好/心/慌……";
	/// <summary>
	/// Gets or sets the font style to use for rendering text.
	/// </summary>
	public RDFontType Font { get; set; } = RDFontType.Default;
	/// <summary>
	/// Initializes a new instance of the <see cref="FloatingText"/> class.
	/// </summary>
	public FloatingText() { }
	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	public override string ToString() => base.ToString() + $" {Text}";
	private readonly List<AdvanceText> _children = [];
}