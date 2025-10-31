namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX Hypno</b>.
/// </summary>
public struct FxHypno : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Green</b>.
	/// </summary>
	[RDJsonProperty("Green")]
	public float Green { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Blue</b>.
	/// </summary>
	[RDJsonProperty("Blue")]
	public float Blue { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_Hypno";
#else
	public static string Name => "CameraFilterPack_FX_Hypno";
#endif
}