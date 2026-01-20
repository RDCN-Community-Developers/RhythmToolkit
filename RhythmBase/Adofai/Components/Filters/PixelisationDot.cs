namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixelisation Dot</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Pixelisation_Dot")]
public struct PixelisationDot : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>LightBackGround</b>.
	/// </summary>
	[RDJsonAlias("LightBackGround")]
	public float LightBackGround { get; set; }
}