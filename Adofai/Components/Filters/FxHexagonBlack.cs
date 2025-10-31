namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Hexagon Black</b>.
/// </summary>
public struct FxHexagonBlack : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	public float Value { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Hexagon_Black";
#else
	public static string Name => "CameraFilterPack_FX_Hexagon_Black";
#endif
}