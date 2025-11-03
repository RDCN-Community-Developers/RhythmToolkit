namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV LED</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_TV_LED")]
public struct TvLed : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public int Size { get; set; }
}