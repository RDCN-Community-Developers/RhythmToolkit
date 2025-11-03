namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Adjust FullColors</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_Adjust_FullColors")]
public struct ColorsAdjustFullColors : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Red_R</b>.
	/// </summary>
	[RDJsonProperty("Red_R")]
	public float RedR { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Green_G</b>.
	/// </summary>
	[RDJsonProperty("Green_G")]
	public float GreenG { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Blue_B</b>.
	/// </summary>
	[RDJsonProperty("Blue_B")]
	public float BlueB { get; set; }
}