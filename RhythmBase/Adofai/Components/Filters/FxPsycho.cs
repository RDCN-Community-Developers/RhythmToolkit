namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Psycho</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_FX_Psycho")]
[RDJsonObjectSerializable]
public struct FxPsycho : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.FxPsycho;
	/// <summary>
	/// Gets or sets the value of the <b>Distortion</b>.
	/// </summary>
	[RDJsonAlias("Distortion")]
	public float Distortion { get; set; }
}