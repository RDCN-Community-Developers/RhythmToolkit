namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents the type of shake effect used in Rhythm Doctor events.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum ShakeType
	{
		/// <summary>
		/// Standard shake effect.
		/// </summary>
		Normal,
		/// <summary>
		/// Smooth shake effect.
		/// </summary>
		Smooth,
		/// <summary>
		/// Rotational shake effect.
		/// </summary>
		Rotate,
		/// <summary>
		/// Bass drop shake effect.
		/// </summary>
		BassDrop,
	}
}
