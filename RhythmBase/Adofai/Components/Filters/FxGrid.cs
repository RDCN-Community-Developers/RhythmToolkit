namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Grid</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Grid")]
public struct FxGrid : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonProperty("Distortion")]
	public float Distortion { get; set; }
}