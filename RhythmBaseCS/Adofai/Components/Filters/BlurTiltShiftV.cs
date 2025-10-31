namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Tilt Shift V</b>.
/// </summary>
public struct BlurTiltShiftV : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Amount</b>.
	/// </summary>
	[RDJsonProperty("Amount")]
	public float Amount { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>FastFilter</b>.
	/// </summary>
	[RDJsonProperty("FastFilter")]
	public int FastFilter { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Smooth</b>.
	/// </summary>
	[RDJsonProperty("Smooth")]
	public float Smooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Position</b>.
	/// </summary>
	[RDJsonProperty("Position")]
	public float Position { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Tilt_Shift_V";
#else
	public static string Name => "CameraFilterPack_Blur_Tilt_Shift_V";
#endif
}