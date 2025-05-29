﻿using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Components.Conditions
{
	/// <summary>
	/// Represents a condition based on the last hit in a rhythm game.
	/// </summary>
	public class LastHitCondition : BaseConditional
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LastHitCondition"/> class.
		/// </summary>
		public LastHitCondition()
		{
			Type = ConditionType.LastHit;
		}
		/// <summary>
		/// Gets the type of the condition.
		/// </summary>
		public override ConditionType Type { get; }
		/// <summary>
		/// Gets or sets the row where the last hit occurred.
		/// </summary>
		public sbyte Row { get; set; }
		/// <summary>
		/// Gets or sets the result that determines under what condition the event will be executed.
		/// </summary>
		public HitResult Result { get; set; }
		/// <summary>
		/// Defines the possible results of a hit.
		/// </summary>
		[Flags]
		public enum HitResult
		{
			/// <summary>
			/// The hit was perfect.
			/// </summary>
			Perfect = 0,
			/// <summary>
			/// The hit was slightly early.
			/// </summary>
			SlightlyEarly = 2,
			/// <summary>
			/// The hit was slightly late.
			/// </summary>
			SlightlyLate = 3,
			/// <summary>
			/// The hit was very early.
			/// </summary>
			VeryEarly = 4,
			/// <summary>
			/// The hit was very late.
			/// </summary>
			VeryLate = 5,
			/// <summary>
			/// The hit was either slightly early or slightly late.
			/// </summary>
			AnyEarlyOrLate = 7,
			/// <summary>
			/// The hit was missed.
			/// </summary>
			Missed = 15
		}
	}
}
