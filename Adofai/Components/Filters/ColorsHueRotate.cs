namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors HUE Rotate</b>.
/// </summary>
public struct ColorsHueRotate : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Colors_HUE_Rotate";
#else
	public static string Name => "CameraFilterPack_Colors_HUE_Rotate";
#endif
}