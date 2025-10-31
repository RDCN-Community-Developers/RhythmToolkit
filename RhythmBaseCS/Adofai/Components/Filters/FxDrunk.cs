namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Drunk</b>.
/// </summary>
public struct FxDrunk : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	[RDJsonProperty("Value")]
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Wavy</b>.
	/// </summary>
	[RDJsonProperty("Wavy")]
	public float Wavy { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	[RDJsonProperty("Fade")]
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColoredSaturate</b>.
	/// </summary>
	[RDJsonProperty("ColoredSaturate")]
	public float ColoredSaturate { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Drunk";
#else
	public static string Name => "CameraFilterPack_FX_Drunk";
#endif
}