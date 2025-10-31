namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX DarkMatter</b>.
/// </summary>
public struct FxDarkMatter : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonProperty("Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonProperty("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	[RDJsonProperty("PosX")]
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	[RDJsonProperty("PosY")]
	public float PosY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Zoom</b>.
	/// </summary>
	[RDJsonProperty("Zoom")]
	public float Zoom { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>DarkIntensity</b>.
	/// </summary>
	[RDJsonProperty("DarkIntensity")]
	public float DarkIntensity { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_DarkMatter";
#else
	public static string Name => "CameraFilterPack_FX_DarkMatter";
#endif
}