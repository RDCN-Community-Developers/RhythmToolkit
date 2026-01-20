namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Tilt Shift</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Tilt_Shift")]
public struct BlurTiltShift : IFilter
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
	/// Gets or sets the value of the <b>Position</b>.
	/// </summary>
	[RDJsonAlias("Position")]
	public float Position { get; set; }
}