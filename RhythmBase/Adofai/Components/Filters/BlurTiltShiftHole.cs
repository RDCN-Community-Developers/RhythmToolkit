namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Tilt Shift Hole</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Tilt_Shift_Hole")]
public struct BlurTiltShiftHole : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Amount</b>.
	/// </summary>
	[RDJsonAlias("Amount")]
	public float Amount { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>FastFilter</b>.
	/// </summary>
	[RDJsonAlias("FastFilter")]
	public int FastFilter { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Smooth</b>.
	/// </summary>
	[RDJsonAlias("Smooth")]
	public float Smooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PositionX</b>.
	/// </summary>
	[RDJsonAlias("PositionX")]
	public float PositionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PositionY</b>.
	/// </summary>
	[RDJsonAlias("PositionY")]
	public float PositionY { get; set; }
}