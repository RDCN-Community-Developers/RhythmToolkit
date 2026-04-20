namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Manga Flash</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Manga_Flash")]
[RDJsonObjectSerializable]
public struct DrawingMangaFlash : IFilter
{
	///<inheritdoc/>
	public FilterType Type => FilterType.DrawingMangaFlash;
	/// <summary>
	/// Gets or sets the value of the <b>Size</b>.
	/// </summary>
	[RDJsonAlias("Size")]
	public float Size { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed</b>.
	/// </summary>
	[RDJsonAlias("Speed")]
	public int Speed { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosX</b>.
	/// </summary>
	[RDJsonAlias("PosX")]
	public float PosX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PosY</b>.
	/// </summary>
	[RDJsonAlias("PosY")]
	public float PosY { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
}