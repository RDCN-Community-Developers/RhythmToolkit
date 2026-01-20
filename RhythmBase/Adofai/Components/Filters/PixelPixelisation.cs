namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixel Pixelisation</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Pixel_Pixelisation")]
public struct PixelPixelisation : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Pixelisation</b>.
	/// </summary>
	[RDJsonAlias("_Pixelisation")]
	public float Pixelisation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SizeX</b>.
	/// </summary>
	[RDJsonAlias("_SizeX")]
	public float SizeX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SizeY</b>.
	/// </summary>
	[RDJsonAlias("_SizeY")]
	public float SizeY { get; set; }
}