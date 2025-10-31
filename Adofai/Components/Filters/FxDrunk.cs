namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Drunk</b>.
/// </summary>
public struct FxDrunk : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Wavy</b>.
	/// </summary>
	public float Wavy { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade</b>.
	/// </summary>
	public float Fade { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ColoredSaturate</b>.
	/// </summary>
	public float ColoredSaturate { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Drunk";
#else
	public static string Name => "CameraFilterPack_FX_Drunk";
#endif
}