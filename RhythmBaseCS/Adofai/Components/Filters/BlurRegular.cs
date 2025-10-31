namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blur Regular</b>.
/// </summary>
public struct BlurRegular : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Level</b>.
	/// </summary>
	[RDJsonProperty("Level")]
	public int Level { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Distance</b>.
	/// </summary>
	[RDJsonProperty("Distance")]
	public RDPointN Distance { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blur_Regular";
#else
	public static string Name => "CameraFilterPack_Blur_Regular";
#endif
}