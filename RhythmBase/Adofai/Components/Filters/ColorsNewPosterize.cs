namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors NewPosterize</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_NewPosterize")]
[RDJsonObjectSerializable]
public struct ColorsNewPosterize : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.ColorsNewPosterize;
	/// <summary>
	/// Gets or sets the value of the <b>Gamma</b>.
	/// </summary>
	[RDJsonAlias("Gamma")]
	public float Gamma { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Colors</b>.
	/// </summary>
	[RDJsonAlias("Colors")]
	public float Colors { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Green_Mod</b>.
	/// </summary>
	[RDJsonAlias("Green_Mod")]
	public float GreenMod { get; set; }
}