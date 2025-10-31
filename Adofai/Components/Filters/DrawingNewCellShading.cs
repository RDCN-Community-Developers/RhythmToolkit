namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing NewCellShading</b>.
/// </summary>
public struct DrawingNewCellShading : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	public float Threshold { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_NewCellShading";
#else
	public static string Name => "CameraFilterPack_Drawing_NewCellShading";
#endif
}