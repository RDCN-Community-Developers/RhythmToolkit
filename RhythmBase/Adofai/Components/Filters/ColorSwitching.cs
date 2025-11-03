namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Switching</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Switching")]
public struct ColorSwitching : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Color</b>.
	/// </summary>
	[RDJsonProperty("Color")]
	public int Color { get; set; }
}