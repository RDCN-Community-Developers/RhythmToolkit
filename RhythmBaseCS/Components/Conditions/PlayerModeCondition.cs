namespace RhythmBase.Components.Conditions
{
	/// <summary>
	/// Represents a condition based on the player mode.
	/// </summary>
	public class PlayerModeCondition : BaseConditional
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerModeCondition"/> class.
		/// </summary>
		public PlayerModeCondition()
		{
			Type = ConditionType.PlayerMode;
		}

		/// <summary>
		/// Gets or sets a value indicating whether two-player mode is enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if two-player mode is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool TwoPlayerMode { get; set; }

		/// <summary>
		/// Gets the type of the condition.
		/// </summary>
		/// <value>
		/// The type of the condition.
		/// </value>
		public override ConditionType Type { get; }
	}
}
