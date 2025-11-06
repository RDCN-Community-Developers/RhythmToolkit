using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to move a row with various properties such as position, scale, angle, and pivot.
	/// </summary>
	public class MoveRow : BaseRowAnimation, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoveRow"/> class.
		/// </summary>
		public MoveRow() { }
		/// <summary>
		/// Gets or sets a value indicating whether a custom position is used.
		/// </summary>
		[RDJsonProperty("customPosition")]
		[RDJsonCondition($"$&.{nameof(Target)} is RhythmBase.RhythmDoctor.Events.{nameof(MoveRowTargets)}.{nameof(MoveRowTargets.WholeRow)}")]
		public bool EnableCustomPosition { get; set; } = true;
		/// <summary>
		/// Gets or sets the target of the move row event.
		/// </summary>
		public MoveRowTargets Target { get; set; } = MoveRowTargets.WholeRow;
		/// <summary>
		/// Gets or sets the row position.
		/// </summary>
		[Tween]
		[RDJsonProperty("rowPosition")]
		[RDJsonCondition($"$&.{nameof(EnableCustomPosition)} is true && $&.{nameof(Position)} is not null")]
		public RDPointE? Position { get; set; }
		/// <summary>
		/// Gets or sets the scale.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Scale)} is not null")]
		public RDSizeE? Scale { get; set; }
		/// <summary>
		/// Gets or sets the angle.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
		public RDExpression? Angle { get; set; }
		/// <summary>
		/// Gets or sets the pivot.
		/// </summary>
		[Tween]
		[RDJsonCondition($"$&.{nameof(Target)} is RhythmBase.RhythmDoctor.Events.{nameof(MoveRowTargets)}.{nameof(MoveRowTargets.WholeRow)} &&" +
			$"$&.{nameof(Pivot)} is not null")]
		public float? Pivot { get; set; }
		/// <summary>
		/// Gets or sets the duration of the move row event.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(EnableCustomPosition)}
			""")]
		public float Duration { get; set; } = 1;
		/// <summary>
		/// Gets or sets the easing type of the move row event.
		/// </summary>
		[RDJsonCondition($"""
			$&.{nameof(EnableCustomPosition)}
			""")]
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.MoveRow;

		/// <summary>
		/// Gets the tab of the event.
		/// </summary>
		public override Tabs Tab => Tabs.Actions;
	}
	/// <summary>
	/// Specifies the targets for the move row event.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum MoveRowTargets
	{
		/// <summary>
		/// Target the whole row.
		/// </summary>
		WholeRow,
		/// <summary>
		/// Target the heart.
		/// </summary>
		Heart,
		/// <summary>
		/// Target the character.
		/// </summary>
		Character
	}
}
