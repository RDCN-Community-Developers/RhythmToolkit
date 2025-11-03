namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Sigmoid</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Edge_Sigmoid")]
public struct EdgeSigmoid : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Gain</b>.
	/// </summary>
	[RDJsonProperty("Gain")]
	public float Gain { get; set; }
}