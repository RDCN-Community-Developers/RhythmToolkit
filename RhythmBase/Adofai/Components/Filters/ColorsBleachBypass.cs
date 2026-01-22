namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors BleachBypass</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Colors_BleachBypass")]
public struct ColorsBleachBypass : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonAlias("Value")]
	public float Value { get; set; }
}