namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing CellShading</b>.
/// </summary>
public struct DrawingCellShading : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>EdgeSize</b>.
	/// </summary>
	public float EdgeSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColorLevel</b>.
	/// </summary>
	public float ColorLevel { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_CellShading";
#else
	public static string Name => "CameraFilterPack_Drawing_CellShading";
#endif
}