namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Specifies the types of borders that can be applied.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum Border
	{
		/// <summary>
		/// No border.
		/// </summary>
		None,
		/// <summary>
		/// An outline border.
		/// </summary>
		Outline,
		/// <summary>
		/// A glowing border.
		/// </summary>
		Glow
	}
}
