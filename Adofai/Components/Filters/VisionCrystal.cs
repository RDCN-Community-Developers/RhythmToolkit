namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Vision Crystal</b>.
/// </summary>
public struct VisionCrystal : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	public float Value { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>X</b>.
	/// </summary>
	public float X { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>Y</b>.
	/// </summary>
	public float Y { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Vision_Crystal";
#else
	public static string Name => "CameraFilterPack_Vision_Crystal";
#endif
}