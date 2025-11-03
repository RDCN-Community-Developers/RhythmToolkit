namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera LighterColor</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_LighterColor")]
public struct BlendToCameraLighterColor : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
}