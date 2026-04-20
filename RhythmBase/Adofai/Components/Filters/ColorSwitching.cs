namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Switching</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Switching")]
[RDJsonObjectSerializable]
public struct ColorSwitching : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.ColorSwitching;
	/// <summary>
	/// Gets or sets the value of the <b>Color</b>.
	/// </summary>
	[RDJsonAlias("Color")]
	public int Color { get; set; }
}