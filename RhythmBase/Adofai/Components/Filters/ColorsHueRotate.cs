namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors HUE Rotate</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_HUE_Rotate")]
[RDJsonObjectSerializable]
public struct ColorsHueRotate : IFilter
{
	public FilterType Type => FilterType.ColorsHueRotate;
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
}