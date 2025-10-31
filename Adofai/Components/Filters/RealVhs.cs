namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Real VHS</b>.
/// </summary>
public struct RealVhs : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>TRACKING</b>.
	/// </summary>
	[RDJsonProperty("TRACKING")]
	public float Tracking { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>JITTER</b>.
	/// </summary>
	[RDJsonProperty("JITTER")]
	public float Jitter { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>GLITCH</b>.
	/// </summary>
	[RDJsonProperty("GLITCH")]
	public float Glitch { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>NOISE</b>.
	/// </summary>
	[RDJsonProperty("NOISE")]
	public float Noise { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Constrast</b>.
	/// </summary>
	[RDJsonProperty("Constrast")]
	public float Constrast { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Real_VHS";
#else
	public static string Name => "CameraFilterPack_Real_VHS";
#endif
}