namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Lines</b>.
/// </summary>
public struct DrawingLines : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Number</b>.
	/// </summary>
	public float Number { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Random</b>.
	/// </summary>
	public float Random { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Drawing_Lines";
#else
	public static string Name => "CameraFilterPack_Drawing_Lines";
#endif
}