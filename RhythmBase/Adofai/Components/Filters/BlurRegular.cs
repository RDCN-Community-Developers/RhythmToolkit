using RhythmBase.Global.Components.Vector;

namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Regular</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_Regular")]
[RDJsonObjectSerializable]
public struct BlurRegular : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.BlurRegular;
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