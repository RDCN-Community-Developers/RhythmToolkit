namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors NewPosterize</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_NewPosterize")]
public struct ColorsNewPosterize : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Gamma</b>.
	/// </summary>
	[RDJsonProperty("Gamma")]
	public float Gamma { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Colors</b>.
	/// </summary>
	[RDJsonProperty("Colors")]
	public float Colors { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Green_Mod</b>.
	/// </summary>
	[RDJsonProperty("Green_Mod")]
	public float GreenMod { get; set; }
}