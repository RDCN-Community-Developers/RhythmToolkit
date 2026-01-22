namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blizzard</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Blizzard")]
public struct Blizzard : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Speed</b>.
	/// </summary>
	[RDJsonAlias("_Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Size</b>.
	/// </summary>
	[RDJsonAlias("_Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Fade</b>.
	/// </summary>
	[RDJsonAlias("_Fade")]
	public float Fade { get; set; }
}