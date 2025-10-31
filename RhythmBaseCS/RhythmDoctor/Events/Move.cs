using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a move event in the rhythm base system.
	/// </summary>
	public class Move : BaseDecorationAction, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Move"/> class.
		/// </summary>
		public Move() { }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; } = EventType.Move;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Decorations;
		/// <summary>
		/// Gets or sets the position of the move event.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Position)} is not null")]
		public RDPointE? Position { get; set; }
		/// <summary>
		/// Gets or sets the scale of the move event.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Scale)} is not null")]
		public RDSizeE? Scale { get; set; }
		/// <summary>
		/// Gets or sets the angle of the move event.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
		public RDExpression? Angle { get; set; }
		/// <summary>
		/// Gets or sets the pivot point of the move event.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Pivot)} is not null")]
		public RDPointE? Pivot { get; set; }
		/// <summary>
		/// Gets or sets the duration of the move event.
		/// </summary>
		public float Duration { get; set; } = 1;
		/// <summary>
		/// Gets or sets the easing type of the move event.
		/// </summary>
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString();
	}
}
