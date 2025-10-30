namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents the types of track animations available in the ADTrack system.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum TrackAnimationType
	{
		/// <summary>
		/// No animation.
		/// </summary>
		None,		/// <summary>
		/// Assemble animation type.
		/// </summary>
		Assemble,		/// <summary>
		/// Assemble animation type with a far effect.
		/// </summary>
		Assemble_Far,		/// <summary>
		/// Extend animation type.
		/// </summary>
		Extend,		/// <summary>
		/// Grow animation type.
		/// </summary>
		Grow,		/// <summary>
		/// Grow animation type with a spinning effect.
		/// </summary>
		Grow_Spin,		/// <summary>
		/// Fade animation type.
		/// </summary>
		Fade,		/// <summary>
		/// Drop animation type.
		/// </summary>
		Drop,		/// <summary>
		/// Rise animation type.
		/// </summary>
		Rise
	}
}
