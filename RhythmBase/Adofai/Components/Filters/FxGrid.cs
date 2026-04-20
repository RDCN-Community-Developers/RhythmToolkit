namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Grid</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Grid")]
[RDJsonObjectSerializable]
public struct FxGrid : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxGrid;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}