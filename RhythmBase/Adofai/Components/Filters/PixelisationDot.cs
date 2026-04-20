namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixelisation Dot</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Pixelisation_Dot")]
[RDJsonObjectSerializable]
public struct PixelisationDot : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.PixelisationDot;
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