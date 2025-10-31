namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>VHS Tracking</b>.
/// </summary>
public struct VhsTracking : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Tracking</b>.
	/// </summary>
	[RDJsonProperty("Tracking")]
	public float Tracking { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_VHS_Tracking";
#else
	public static string Name => "CameraFilterPack_VHS_Tracking";
#endif
}