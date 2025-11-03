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
	[RDJsonProperty("Pencil_Color")]
	public RDColor PencilColor { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Pencil_Size</b>.
	/// </summary>
	[RDJsonProperty("Pencil_Size")]
	public float PencilSize { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Pencil_Correction</b>.
	/// </summary>
	[RDJsonProperty("Pencil_Correction")]
	public float PencilCorrection { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Intensity</b>.
	/// </summary>
	[RDJsonProperty("Intensity")]
	public float Intensity { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Speed_Animation</b>.
	/// </summary>
	[RDJsonProperty("Speed_Animation")]
	public float SpeedAnimation { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Corner_Lose</b>.
	/// </summary>
	[RDJsonProperty("Corner_Lose")]
	public float CornerLose { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Fade_With_Original</b>.
	/// </summary>
	[RDJsonProperty("Fade_With_Original")]
	public float FadeWithOriginal { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Back_Color</b>.
	/// </summary>
	[RDJsonProperty("Back_Color")]
	public RDColor BackColor { get; set; }
}