namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Specifies the type of row in the rhythm base.
/// </summary>
[RDJsonEnumSerializable]
public enum RowType
{
	/// <summary>
	/// Represents a classic row type.
	/// </summary>
	Classic,
	/// <summary>
	/// Represents a oneshot row type.
	/// </summary>
	Oneshot
}
