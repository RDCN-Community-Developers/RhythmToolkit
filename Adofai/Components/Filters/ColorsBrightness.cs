namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors Brightness</b>.
/// </summary>
public struct ColorsBrightness : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Brightness</b>.
	/// </summary>
	[RDJsonProperty("_Brightness")]
	public float Brightness { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Colors_Brightness";
#else
	public static string Name => "CameraFilterPack_Colors_Brightness";
#endif
}