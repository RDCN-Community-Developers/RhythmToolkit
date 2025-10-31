namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Atmosphere Snow 8bits</b>.
/// </summary>
public struct AtmosphereSnow8Bits : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonProperty("Threshold")]
	public float Threshold { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Atmosphere_Snow_8bits";
#else
	public static string Name => "CameraFilterPack_Atmosphere_Snow_8bits";
#endif
}