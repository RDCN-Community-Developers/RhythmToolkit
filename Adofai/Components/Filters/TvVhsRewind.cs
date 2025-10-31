namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV VHS Rewind</b>.
/// </summary>
public struct TvVhsRewind : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Cryptage</b>.
	/// </summary>
	public float Cryptage { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Parasite</b>.
	/// </summary>
	public float Parasite { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Parasite2</b>.
	/// </summary>
	public float Parasite2 { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_VHS_Rewind";
#else
	public static string Name => "CameraFilterPack_TV_VHS_Rewind";
#endif
}