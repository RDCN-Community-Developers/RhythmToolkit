namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>EXTRA Rotation</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_EXTRA_Rotation")]
public struct ExtraRotation : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>PositionX</b>.
	/// </summary>
	[RDJsonProperty("PositionX")]
	public float PositionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PositionY</b>.
	/// </summary>
	[RDJsonProperty("PositionY")]
	public float PositionY { get; set; }
}