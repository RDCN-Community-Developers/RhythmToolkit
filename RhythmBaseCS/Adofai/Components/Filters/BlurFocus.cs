namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Focus</b>.
/// </summary>
public struct BlurFocus : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Size</b>.
	/// </summary>
	[RDJsonProperty("_Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Eyes</b>.
	/// </summary>
	[RDJsonProperty("_Eyes")]
	public float Eyes { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Focus";
#else
	public static string Name => "CameraFilterPack_Blur_Focus";
#endif
}