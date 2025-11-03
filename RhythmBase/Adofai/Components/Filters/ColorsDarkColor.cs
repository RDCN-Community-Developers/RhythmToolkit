namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors DarkColor</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_DarkColor")]
public struct ColorsDarkColor : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Alpha</b>.
	/// </summary>
	[RDJsonProperty("Alpha")]
	public float Alpha { get; set; }
}