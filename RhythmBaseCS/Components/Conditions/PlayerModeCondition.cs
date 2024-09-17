using System;
namespace RhythmBase.Components.Conditions
{
	/// <summary>
	/// Player mode.
	/// </summary>
	public class PlayerModeCondition : BaseConditional
	{
		public PlayerModeCondition()
		{
			Type = ConditionType.PlayerMode;
		}
		/// <summary>
		/// Enable two-player mode.
		/// </summary>
		public bool TwoPlayerMode { get; set; }

		public override ConditionType Type { get; }
	}
}
