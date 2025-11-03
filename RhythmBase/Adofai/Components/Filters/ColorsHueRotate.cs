namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors HUE Rotate</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_HUE_Rotate")]
public struct ColorsHueRotate : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
}