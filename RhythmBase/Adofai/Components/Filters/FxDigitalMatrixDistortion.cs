namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX DigitalMatrixDistortion</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_DigitalMatrixDistortion")]
public struct FxDigitalMatrixDistortion : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}