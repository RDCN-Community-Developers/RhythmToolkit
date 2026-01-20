namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera BlueScreen</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_BlueScreen")]
public struct BlendToCameraBlueScreen : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonAlias("BlendFX")]
	public float BlendFX { get; set; }
}