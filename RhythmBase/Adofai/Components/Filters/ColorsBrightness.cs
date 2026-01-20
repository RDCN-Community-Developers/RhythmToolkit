namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Brightness</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_Brightness")]
public struct ColorsBrightness : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Brightness</b>.
	/// </summary>
	[RDJsonAlias("_Brightness")]
	public float Brightness { get; set; }
}