using RhythmBase.Global.Components.Vector;

namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Dithering2x2</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Dithering2x2")]
[RDJsonObjectSerializable]
public struct BlurDitheringToxTo : IFilter
{
	public FilterType Type => FilterType.BlurDitheringToxTo;
	/// <summary>
	/// Gets or sets the value of the <b>Level</b>.
	/// </summary>
	[RDJsonAlias("Level")]
	public int Level { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distance</b>.
	/// </summary>
	[RDJsonAlias("Distance")]
	public RDPointN Distance { get; set; }
}