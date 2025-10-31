namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>TV VHS</b>.
/// </summary>
public struct TvVhs : IFilter
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
	/// Gets or sets the value of the <b>WhiteParasite</b>.
	/// </summary>
	public float WhiteParasite { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_TV_VHS";
#else
	public static string Name => "CameraFilterPack_TV_VHS";
#endif
}