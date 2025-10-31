namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Ascii</b>.
/// </summary>
public struct FxAscii : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Ascii";
#else
	public static string Name => "CameraFilterPack_FX_Ascii";
#endif
}