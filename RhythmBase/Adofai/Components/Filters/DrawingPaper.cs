namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Drawing Paper</b>.
/// </summary>
[RDJsonSpecialID("CameraFilterPack_Drawing_Paper")]
public struct DrawingPaper : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Pencil_Color</b>.
	/// </summary>
	[RDJsonAlias("Pencil_Color")]
	public RDColor PencilColor { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Pencil_Size</b>.
	/// </summary>
	[RDJsonAlias("Pencil_Size")]
	public float PencilSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Pencil_Correction</b>.
	/// </summary>
	[RDJsonAlias("Pencil_Correction")]
	public float PencilCorrection { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonAlias("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed_Animation</b>.
	/// </summary>
	[RDJsonAlias("Speed_Animation")]
	public float SpeedAnimation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Corner_Lose</b>.
	/// </summary>
	[RDJsonAlias("Corner_Lose")]
	public float CornerLose { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade_With_Original</b>.
	/// </summary>
	[RDJsonAlias("Fade_With_Original")]
	public float FadeWithOriginal { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Back_Color</b>.
	/// </summary>
	[RDJsonAlias("Back_Color")]
	public RDColor BackColor { get; set; }
}