namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing CellShading2</b>.
/// </summary>
public struct DrawingCellShadingTo : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>EdgeSize</b>.
	/// </summary>
	[RDJsonProperty("EdgeSize")]
	public float EdgeSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorLevel</b>.
	/// </summary>
	[RDJsonProperty("ColorLevel")]
	public float ColorLevel { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Blur</b>.
	/// </summary>
	[RDJsonProperty("Blur")]
	public float Blur { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_CellShading2";
#else
	public static string Name => "CameraFilterPack_Drawing_CellShading2";
#endif
}