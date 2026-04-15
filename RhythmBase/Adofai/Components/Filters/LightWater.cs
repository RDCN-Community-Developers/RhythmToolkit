namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Light Water</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Light_Water")]
[RDJsonObjectSerializable]
public struct LightWater : IFilter
{
	public FilterType Type => FilterType.LightWater;
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Alpha</b>.
	/// </summary>
	[RDJsonAlias("Alpha")]
	public float Alpha { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distance</b>.
	/// </summary>
	[RDJsonAlias("Distance")]
	public float Distance { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public float Speed { get; set; }
}