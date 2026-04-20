using RhythmBase.Global.Components.Vector;

namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Spot</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Spot")]
[RDJsonObjectSerializable]
public struct FxSpot : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxSpot;
	/// <summary>
	/// Gets or sets the value of the <b>center</b>.
	/// </summary>
	public RDPointN Center { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	[RDJsonAlias("Radius")]
	public float Radius { get; set; }
}