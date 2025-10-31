namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Color Switching</b>.
/// </summary>
public struct ColorSwitching : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Color</b>.
	/// </summary>
	[RDJsonProperty("Color")]
	public int Color { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Color_Switching";
#else
	public static string Name => "CameraFilterPack_Color_Switching";
#endif
}