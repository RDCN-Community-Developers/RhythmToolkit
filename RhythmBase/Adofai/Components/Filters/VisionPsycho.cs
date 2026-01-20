namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Psycho</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Vision_Psycho")]
public struct VisionPsycho : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>HoleSize</b>.
	/// </summary>
	[RDJsonAlias("HoleSize")]
	public float HoleSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>HoleSmooth</b>.
	/// </summary>
	[RDJsonAlias("HoleSmooth")]
	public float HoleSmooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Color1</b>.
	/// </summary>
	[RDJsonAlias("Color1")]
	public float Color1 { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Color2</b>.
	/// </summary>
	[RDJsonAlias("Color2")]
	public float Color2 { get; set; }
}