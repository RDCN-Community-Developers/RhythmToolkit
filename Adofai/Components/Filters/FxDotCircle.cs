namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Dot Circle</b>.
/// </summary>
public struct FxDotCircle : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Dot_Circle";
#else
	public static string Name => "CameraFilterPack_FX_Dot_Circle";
#endif
}