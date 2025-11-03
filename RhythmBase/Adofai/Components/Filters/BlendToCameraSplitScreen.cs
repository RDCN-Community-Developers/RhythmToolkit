namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blend2Camera SplitScreen</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blend2Camera_SplitScreen")]
public struct BlendToCameraSplitScreen : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>BlendFX</b>.
	/// </summary>
	[RDJsonProperty("BlendFX")]
	public float BlendFX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>SplitX</b>.
	/// </summary>
	[RDJsonProperty("SplitX")]
	public float SplitX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>SplitY</b>.
	/// </summary>
	[RDJsonProperty("SplitY")]
	public float SplitY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Smooth</b>.
	/// </summary>
	[RDJsonProperty("Smooth")]
	public float Smooth { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Rotation</b>.
	/// </summary>
	[RDJsonProperty("Rotation")]
	public float Rotation { get; set; }
}