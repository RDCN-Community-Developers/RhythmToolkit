namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Special Bubble</b>.
/// </summary>
public struct SpecialBubble : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>X</b>.
	/// </summary>
	public float X { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Y</b>.
	/// </summary>
	public float Y { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Rate</b>.
	/// </summary>
	public float Rate { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Special_Bubble";
#else
	public static string Name => "CameraFilterPack_Special_Bubble";
#endif
}