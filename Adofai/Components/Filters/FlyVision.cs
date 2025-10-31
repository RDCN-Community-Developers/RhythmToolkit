namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Fly Vision</b>.
/// </summary>
public struct FlyVision : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Zoom</b>.
	/// </summary>
	public float Zoom { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	public float Distortion { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Fly_Vision";
#else
	public static string Name => "CameraFilterPack_Fly_Vision";
#endif
}