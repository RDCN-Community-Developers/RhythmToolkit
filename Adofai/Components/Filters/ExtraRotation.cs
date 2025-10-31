namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>EXTRA Rotation</b>.
/// </summary>
public struct ExtraRotation : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>PositionX</b>.
	/// </summary>
	public float PositionX { get; set; }
	/// <summary>
	/// Gets or sets the value of the <b>PositionY</b>.
	/// </summary>
	public float PositionY { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_EXTRA_Rotation";
#else
	public static string Name => "CameraFilterPack_EXTRA_Rotation";
#endif
}