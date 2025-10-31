namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Pixelisation Dot</b>.
/// </summary>
public struct PixelisationDot : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>LightBackGround</b>.
	/// </summary>
	[RDJsonProperty("LightBackGround")]
	public float LightBackGround { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Pixelisation_Dot";
#else
	public static string Name => "CameraFilterPack_Pixelisation_Dot";
#endif
}