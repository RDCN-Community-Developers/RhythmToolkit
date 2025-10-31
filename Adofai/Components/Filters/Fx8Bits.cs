namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>FX 8bits</b>.
/// </summary>
public struct Fx8Bits : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>ResolutionX</b>.
	/// </summary>
	public int ResolutionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>ResolutionY</b>.
	/// </summary>
	public int ResolutionY { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_FX_8bits";
#else
	public static string Name => "CameraFilterPack_FX_8bits";
#endif
}