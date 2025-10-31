namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Film Grain</b>.
/// </summary>
public struct FilmGrain : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Film_Grain";
#else
	public static string Name => "CameraFilterPack_Film_Grain";
#endif
}