namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors NewPosterize</b>.
/// </summary>
public struct ColorsNewPosterize : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Gamma</b>.
	/// </summary>
	public float Gamma { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Colors</b>.
	/// </summary>
	public float Colors { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Green_Mod</b>.
	/// </summary>
	[RDJsonProperty("Green_Mod")]
	public float GreenMod { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Colors_NewPosterize";
#else
	public static string Name => "CameraFilterPack_Colors_NewPosterize";
#endif
}