namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX 8bits</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_8bits")]
public struct Fx8Bits : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>ResolutionX</b>.
	/// </summary>
	[RDJsonProperty("ResolutionX")]
	public int ResolutionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ResolutionY</b>.
	/// </summary>
	[RDJsonProperty("ResolutionY")]
	public int ResolutionY { get; set; }
}