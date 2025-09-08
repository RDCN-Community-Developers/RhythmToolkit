using Newtonsoft.Json;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event to move a row with various properties such as position, scale, angle, and pivot.
	/// </summary>
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class MoveRow : BaseRowAnimation, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoveRow"/> class.
		/// </summary>
		public MoveRow()
		{
			Type = EventType.MoveRow;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets a value indicating whether a custom position is used.
		/// </summary>
		public bool CustomPosition { get; set; }
		/// <summary>
		/// Gets or sets the target of the move row event.
		/// </summary>
		public MoveRowTargets Target { get; set; }
		/// <summary>
		/// Gets or sets the row position.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"$&.{nameof(CustomPosition)} is true && $&.{nameof(RowPosition)} is not null")]
		public RDPointE? RowPosition { get; set; }
		/// <summary>
		/// Gets or sets the scale.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"$&.{nameof(Scale)} is not null")]
		public RDSizeE? Scale { get; set; }
		/// <summary>
		/// Gets or sets the angle.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
		public RDExpression? Angle { get; set; }
		/// <summary>
		/// Gets or sets the pivot.
		/// </summary>
		[EaseProperty]
		[RDJsonCondition($"$&.{nameof(Pivot)} is not null")]
		public float? Pivot { get; set; }
		/// <summary>
		/// Gets or sets the duration of the move row event.
		/// </summary>
		public float Duration { get; set; }
		/// <summary>
		/// Gets or sets the easing type of the move row event.
		/// </summary>
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab of the event.
		/// </summary>
		public override Tabs Tab { get; }
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
