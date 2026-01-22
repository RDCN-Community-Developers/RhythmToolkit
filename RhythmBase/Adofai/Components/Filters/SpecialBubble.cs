namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Special Bubble</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Special_Bubble")]
public struct SpecialBubble : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>X</b>.
	/// </summary>
	[RDJsonAlias("X")]
	public float X { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Y</b>.
	/// </summary>
	[RDJsonAlias("Y")]
	public float Y { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Rate</b>.
	/// </summary>
	[RDJsonAlias("Rate")]
	public float Rate { get; set; }
}