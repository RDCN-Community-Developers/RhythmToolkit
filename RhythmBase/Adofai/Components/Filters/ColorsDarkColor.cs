namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors DarkColor</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_DarkColor")]
[RDJsonObjectSerializable]
public struct ColorsDarkColor : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.ColorsDarkColor;
	/// <summary>
	/// Gets or sets the value of the <b>Alpha</b>.
	/// </summary>
	[RDJsonAlias("Alpha")]
	public float Alpha { get; set; }
}