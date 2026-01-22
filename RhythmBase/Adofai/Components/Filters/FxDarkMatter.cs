namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX DarkMatter</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_DarkMatter")]
public struct FxDarkMatter : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	[RDJsonAlias("PosX")]
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	[RDJsonAlias("PosY")]
	public float PosY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Zoom</b>.
	/// </summary>
	[RDJsonAlias("Zoom")]
	public float Zoom { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DarkIntensity</b>.
	/// </summary>
	[RDJsonAlias("DarkIntensity")]
	public float DarkIntensity { get; set; }
}