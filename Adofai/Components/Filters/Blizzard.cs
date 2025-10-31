namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Blizzard</b>.
/// </summary>
public struct Blizzard : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>_Speed</b>.
	/// </summary>
	[RDJsonProperty("_Speed")]
	public float Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Size</b>.
	/// </summary>
	[RDJsonProperty("_Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>_Fade</b>.
	/// </summary>
	[RDJsonProperty("_Fade")]
	public float Fade { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Blizzard";
#else
	public static string Name => "CameraFilterPack_Blizzard";
#endif
}