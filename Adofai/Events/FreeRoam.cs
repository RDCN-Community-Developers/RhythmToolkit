using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Adofai.Events;

/// <summary>
/// Represents the Free Roam event in the ADOFAI editor.
/// </summary>
public class FreeRoam : BaseTileEvent, IEaseEvent, ISingleEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.FreeRoam;
	/// <summary>
	/// Gets or sets the duration of the Free Roam event.
	/// </summary>
	public float Duration { get; set; } = 16;
	/// <summary>
	/// Gets or sets the size of the Free Roam area.
	/// </summary>
	public RDSizeN Size { get; set; } = new(4, 4);
	/// <summary>
	/// Gets or sets the position offset for the Free Roam event.
	/// </summary>
	public RDSizeN PositionOffset { get; set; } = new(0, 0);
	/// <summary>
	/// Gets or sets the time at which the Free Roam event ends.
	/// </summary>
	public int OutTime { get; set; } = 4;
	/// <summary>
	/// Gets or sets the easing type for the Free Roam event.
	/// </summary>
	[RDJsonProperty("outEase")]
	public EaseType Ease { get; set; } = EaseType.InOutSine;
	/// <summary>
	/// Gets or sets the hitsound to be played on beats during the Free Roam event.
	/// </summary>
	public HitSound HitsoundOnBeats { get; set; } = HitSound.None;
	/// <summary>
	/// Gets or sets the hitsound to be played off beats during the Free Roam event.
	/// </summary>
	public HitSound HitsoundOffBeats { get; set; } = HitSound.None;
	/// <summary>
	/// Gets or sets the number of countdown ticks for the Free Roam event.
	/// </summary>
	public int CountdownTicks { get; set; } = 4;
	/// <summary>
	/// Gets or sets the angle correction direction for the Free Roam event.
	/// </summary>
	[RDJsonProperty("angleCorrectionDir")]
	public AngleCorrectionDirection AngleCorrectionDirection { get; set; } = AngleCorrectionDirection.Backward;
}
[RDJsonEnumSerializable]
public enum AngleCorrectionDirection
{
#warning Review the names of these enum members for accuracy.
	None,
	Forward,
	Backward,
}