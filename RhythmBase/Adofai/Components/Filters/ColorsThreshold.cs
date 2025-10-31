namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Threshold</b>.
/// </summary>
public struct ColorsThreshold : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonProperty("Threshold")]
	public float Threshold { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Colors_Threshold";
#else
	public static string Name => "CameraFilterPack_Colors_Threshold";
#endif
}