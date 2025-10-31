namespace RhythmBase.Adofai.Components.Filters;
/// <summary>
/// The filter of <b>Colors BleachBypass</b>.
/// </summary>
public struct ColorsBleachBypass : IFilter
{
	/// <summary>
	/// Gets or sets the value of the <b>Value</b>.
	/// </summary>
	public float Value { get; set; }
	/// <inheritdoc/>
#if NETSTANDARD2_0
	public readonly string Name => "CameraFilterPack_Colors_BleachBypass";
#else
	public static string Name => "CameraFilterPack_Colors_BleachBypass";
#endif
}