namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixel Pixelisation</b>.
/// </summary>
public struct PixelPixelisation : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Pixelisation</b>.
	/// </summary>
	[RDJsonProperty("_Pixelisation")]
	public float Pixelisation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SizeX</b>.
	/// </summary>
	[RDJsonProperty("_SizeX")]
	public float SizeX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_SizeY</b>.
	/// </summary>
	[RDJsonProperty("_SizeY")]
	public float SizeY { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Pixel_Pixelisation";
#else
	public static string Name => "CameraFilterPack_Pixel_Pixelisation";
#endif
}