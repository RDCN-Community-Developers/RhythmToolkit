namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Spot</b>.
/// </summary>
public struct FxSpot : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>center</b>.
	/// </summary>
	[RDJsonProperty("center")]
	public RDPointN Center { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Radius</b>.
	/// </summary>
	[RDJsonProperty("Radius")]
	public float Radius { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Spot";
#else
	public static string Name => "CameraFilterPack_FX_Spot";
#endif
}