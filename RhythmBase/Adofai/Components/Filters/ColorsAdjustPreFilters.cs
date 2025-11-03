namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Adjust PreFilters</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_Adjust_PreFilters")]
public struct ColorsAdjustPreFilters : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	[RDJsonProperty("FadeFX")]
	public float FadeFX { get; set; }
}