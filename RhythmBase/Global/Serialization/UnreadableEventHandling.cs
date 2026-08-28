namespace RhythmBase.Global.Serialization;

/// <summary>
/// Actions performed on items with exceptions during reads.
/// </summary>
public enum UnreadableEventHandling
{
	/// <summary>
	/// Stores unreadable events in <see cref="P:RhythmBase.Global.LevelReadOrWriteConfig.UnreadableEvents" /> for restoration.
	/// </summary>
	Store,
	/// <summary>
	/// An exception will be thrown.
	/// </summary>
	ThrowException,
}
