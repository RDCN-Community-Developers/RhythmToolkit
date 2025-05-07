namespace RhythmBase.Components.Easing
{
	/// <summary>
	/// Represents a node in the easing process.
	/// </summary>
	/// <param name="target">The target value of the easing node.</param>
	public struct EaseNode(float target)
	{
		/// <summary>
		/// Gets or sets the time at which the easing node starts.
		/// </summary>
		public float Time { get; set; } = 0;		/// <summary>
		/// Gets or sets the target value of the easing node.
		/// </summary>
		public float Target { get; set; } = target;		/// <summary>
		/// Gets or sets the duration of the easing node.
		/// </summary>
		public float Duration { get; set; } = 0;		/// <summary>
		/// Gets or sets the type of easing to be applied.
		/// </summary>
		public EaseType Type { get; set; } = EaseType.Linear;
	}
}
