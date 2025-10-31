namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>NightVision 4</b>.
/// </summary>
public struct NightVision4 : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>FadeFX</b>.
	/// </summary>
	[RDJsonProperty("FadeFX")]
	public float FadeFX { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_NightVision_4";
#else
	public static string Name => "CameraFilterPack_NightVision_4";
#endif
}