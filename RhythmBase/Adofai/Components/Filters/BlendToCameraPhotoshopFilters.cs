namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera PhotoshopFilters</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_PhotoshopFilters")]
public struct BlendToCameraPhotoshopFilters : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
}