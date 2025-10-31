namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Tilt Shift Hole</b>.
/// </summary>
public struct BlurTiltShiftHole : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Amount</b>.
	/// </summary>
	public float Amount { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>FastFilter</b>.
	/// </summary>
	public int FastFilter { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Smooth</b>.
	/// </summary>
	public float Smooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PositionX</b>.
	/// </summary>
	public float PositionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PositionY</b>.
	/// </summary>
	public float PositionY { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Tilt_Shift_Hole";
#else
	public static string Name => "CameraFilterPack_Blur_Tilt_Shift_Hole";
#endif
}