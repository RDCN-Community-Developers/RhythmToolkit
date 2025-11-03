namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Water</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Light_Water")]
public struct LightWater : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonProperty("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Alpha</b>.
	/// </summary>
	[RDJsonProperty("Alpha")]
	public float Alpha { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distance</b>.
	/// </summary>
	[RDJsonProperty("Distance")]
	public float Distance { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
}