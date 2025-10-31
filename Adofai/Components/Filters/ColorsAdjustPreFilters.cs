namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Adjust PreFilters</b>.
/// </summary>
public struct ColorsAdjustPreFilters : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	public float FadeFX { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Colors_Adjust_PreFilters";
#else
	public static string Name => "CameraFilterPack_Colors_Adjust_PreFilters";
#endif
}