namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents the types of animations used for track disappearance.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum TrackDisappearAnimationTypes
	{
		/// <summary>
		/// No animation is applied.
		/// </summary>
		None,		/// <summary>
		/// The track disappears with a scatter effect.
		/// </summary>
		Scatter,		/// <summary>
		/// The track disappears with a far scatter effect.
		/// </summary>
		Scatter_Far,		/// <summary>
		/// The track retracts before disappearing.
		/// </summary>
		Retract,		/// <summary>
		/// The track shrinks before disappearing.
		/// </summary>
		Shrink,		/// <summary>
		/// The track shrinks and spins before disappearing.
		/// </summary>
		Shrink_Spin,		/// <summary>
		/// The track fades out before disappearing.
		/// </summary>
		Fade
	}
}
