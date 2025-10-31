namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Crosshatch</b>.
/// </summary>
public struct DrawingCrosshatch : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Width</b>.
	/// </summary>
	[RDJsonProperty("Width")]
	public float Width { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_Crosshatch";
#else
	public static string Name => "CameraFilterPack_Drawing_Crosshatch";
#endif
}