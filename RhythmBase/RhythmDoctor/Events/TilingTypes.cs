namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents the types of tiling that can be applied.
/// </summary>
[RDJsonEnumSerializable]
public enum TilingTypes
{
	/// <summary>
	/// Tiling type where the content scrolls.
	/// </summary>
	Scroll,
	/// <summary>
	/// Tiling type where the content pulses.
	/// </summary>
	Pulse
}
