namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Real VHS</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Real_VHS")]
public struct RealVhs : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>TRACKING</b>.
	/// </summary>
	[RDJsonAlias("TRACKING")]
	public float Tracking { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>JITTER</b>.
	/// </summary>
	[RDJsonAlias("JITTER")]
	public float Jitter { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>GLITCH</b>.
	/// </summary>
	[RDJsonAlias("GLITCH")]
	public float Glitch { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>NOISE</b>.
	/// </summary>
	[RDJsonAlias("NOISE")]
	public float Noise { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Constrast</b>.
	/// </summary>
	[RDJsonAlias("Constrast")]
	public float Constrast { get; set; }
}