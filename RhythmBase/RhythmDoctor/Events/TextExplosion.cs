using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;
namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a text explosion event in a room.
/// </summary>
public class TextExplosion : BaseEvent, IRoomEvent, IColorEvent
{
	///<inheritdoc/>
	public RDRoom Rooms { get; set; } = new RDRoom([0]);
	/// <summary>
	/// Gets or sets the color of the text explosion.
	/// </summary>
	public PaletteColor Color { get; set; } = RDColor.Black;
	/// <summary>
	/// Gets or sets the text to be displayed in the explosion.
	/// </summary>
	public string Text { get; set; } = "";
	/// <summary>
	/// Gets or sets the speed value. The value is constrained to be at least 1.0.
	/// </summary>
	public float Speed { get; set => field = value > 1f ? value : 1f; } = 100f;
	/// <summary>
	/// Gets or sets the direction of the text explosion.
	/// </summary>
	public TextExplosionDirection Direction { get; set; } = TextExplosionDirection.Left;
	/// <summary>
	/// Gets or sets the mode of the text explosion.
	/// </summary>
	public TextExplosionMode Mode { get; set; } = TextExplosionMode.OneColor;
	///<inheritdoc/>
	public override EventType Type => EventType.TextExplosion;
	///<inheritdoc/>
	public override Tab Tab => Tab.Actions;

	/// <summary>
	/// Gets or sets the easing type.
	/// </summary>
	public EaseType Ease { get; set; }
	///<inheritdoc/>
	public override string ToString() => base.ToString() + $" {Text}";
}
