namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors DarkColor</b>.
/// </summary>
public struct ColorsDarkColor : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Alpha</b>.
	/// </summary>
	[RDJsonProperty("Alpha")]
	public float Alpha { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Colors_DarkColor";
#else
	public static string Name => "CameraFilterPack_Colors_DarkColor";
#endif
}