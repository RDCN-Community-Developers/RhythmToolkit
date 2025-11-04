using RhythmBase.Global.Components.Vector;

namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur DitherOffset</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blur_DitherOffset")]
public struct BlurDitherOffset : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Level</b>.
	/// </summary>
	[RDJsonProperty("Level")]
	public int Level { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distance</b>.
	/// </summary>
	[RDJsonProperty("Distance")]
	public RDPointN Distance { get; set; }
}