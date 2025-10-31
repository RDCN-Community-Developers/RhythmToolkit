namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Psycho</b>.
/// </summary>
public struct VisionPsycho : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>HoleSize</b>.
	/// </summary>
	[RDJsonProperty("HoleSize")]
	public float HoleSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>HoleSmooth</b>.
	/// </summary>
	[RDJsonProperty("HoleSmooth")]
	public float HoleSmooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Color1</b>.
	/// </summary>
	[RDJsonProperty("Color1")]
	public float Color1 { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Color2</b>.
	/// </summary>
	[RDJsonProperty("Color2")]
	public float Color2 { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Vision_Psycho";
#else
	public static string Name => "CameraFilterPack_Vision_Psycho";
#endif
}