namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX ZebraColor</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_ZebraColor")]
public struct FxZebraColor : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}