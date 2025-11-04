using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
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
/// <summary>
/// Specifies the direction used for angle correction when Free Roam mode adjusts or normalizes angles.
/// </summary>
/// <remarks>
/// Angle correction controls how the editor corrects or resolves angular discontinuities
/// when exiting or interacting with free roam. Use <see cref="None"/> to disable correction,
/// <see cref="Forward"/> to prefer increasing angle adjustments, or <see cref="Backward"/> to prefer decreasing adjustments.
/// </remarks>
[RDJsonEnumSerializable]
public enum AngleCorrectionDirection
{
	/// <summary>
	/// Do not perform any angle correction.
	/// </summary>
	None,
	/// <summary>
	/// Correct angles by choosing the forward (increasing) rotation direction.
	/// </summary>
	Forward,
	/// <summary>
	/// Correct angles by choosing the backward (decreasing) rotation direction.
	/// </summary>
	Backward,
}