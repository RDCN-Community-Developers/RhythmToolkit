namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors RgbClamp</b>.
/// </summary>
public struct ColorsRgbClamp : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Red_End</b>.
	/// </summary>
	[RDJsonProperty("Red_End")]
	public float RedEnd { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Green_End</b>.
	/// </summary>
	[RDJsonProperty("Green_End")]
	public float GreenEnd { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Blue_End</b>.
	/// </summary>
	[RDJsonProperty("Blue_End")]
	public float BlueEnd { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>RGB_End</b>.
	/// </summary>
	[RDJsonProperty("RGB_End")]
	public float RgbEnd { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Colors_RgbClamp";
#else
	public static string Name => "CameraFilterPack_Colors_RgbClamp";
#endif
}