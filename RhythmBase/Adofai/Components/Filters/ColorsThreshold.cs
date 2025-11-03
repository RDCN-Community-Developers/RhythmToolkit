namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Threshold</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_Threshold")]
public struct ColorsThreshold : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Threshold</b>.
	/// </summary>
	[RDJsonProperty("Threshold")]
	public float Threshold { get; set; }
}