namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Defines the possible transition types for hiding the row.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum Transitions
	{
		/// <summary>
		/// Smooth transition.
		/// </summary>
		Smooth,
		/// <summary>
		/// Instant transition.
		/// </summary>
		Instant,
		/// <summary>
		/// Full transition.
		/// </summary>
		Full,
		/// <summary>
		/// Represents a placeholder or default value indicating the absence of a specific option or selection.
		/// </summary>
		None,
	}
}
