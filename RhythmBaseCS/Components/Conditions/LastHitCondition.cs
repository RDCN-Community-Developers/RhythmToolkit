using System;
namespace RhythmBase.Components.Conditions
{
	/// <summary>
	/// Last hit.
	/// </summary>
	public class LastHitCondition : BaseConditional
	{
		public LastHitCondition()
		{
			Type = ConditionType.LastHit;
		}

		public override ConditionType Type { get; }
		/// <summary>
		/// The row.
		/// </summary>
		public sbyte Row { get; set; }
		/// <summary>
		/// determines under what result the event will be executed.
		/// </summary>
		public HitResult Result { get; set; }

		[Flags]
		public enum HitResult
		{
			Perfect = 0,
			SlightlyEarly = 2,
			SlightlyLate = 3,
			VeryEarly = 4,
			VeryLate = 5,
			AnyEarlyOrLate = 7,
			Missed = 15
		}
	}
}
