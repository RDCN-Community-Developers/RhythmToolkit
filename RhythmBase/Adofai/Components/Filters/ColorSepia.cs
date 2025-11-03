namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Sepia</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Color_Sepia")]
public struct ColorSepia : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Fade</b>.
	/// </summary>
	[RDJsonProperty("_Fade")]
	public float Fade { get; set; }
}