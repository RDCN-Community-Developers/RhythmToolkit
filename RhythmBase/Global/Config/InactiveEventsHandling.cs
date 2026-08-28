namespace RhythmBase.Global.Settings;

/// <summary>
/// Actions performed on inactive items at read or write times.
/// </summary>
public enum InactiveEventsHandling
{
	/// <summary>
	/// Retaining inactivated events to the level on reads.
	/// Write inactivated events on writes.
	/// </summary>
	Retain,
	/// <summary>
	/// Dumps inactivated events to <see cref="P:RhythmBase.Global.Settings.LevelReadOrWriteSettings.InactiveEvents" /> on reads and writes.
	/// </summary>
	Store,
	/// <summary>
	/// Ignore inactivation events on reads and writes.
	/// </summary>
	Ignore
}
