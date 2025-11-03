namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Edge Neon</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Edge_Neon")]
public struct EdgeNeon : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>EdgeWeight</b>.
	/// </summary>
	[RDJsonProperty("EdgeWeight")]
	public float EdgeWeight { get; set; }
}