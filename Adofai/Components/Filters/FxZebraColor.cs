namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX ZebraColor</b>.
/// </summary>
public struct FxZebraColor : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	public float Value { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_ZebraColor";
#else
	public static string Name => "CameraFilterPack_FX_ZebraColor";
#endif
}