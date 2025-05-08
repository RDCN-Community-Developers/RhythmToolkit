using Newtonsoft.Json;
using RhythmBase.Global.Components;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a move event in the rhythm base system.
	/// </summary>
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class Move : BaseDecorationAction, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Move"/> class.
		/// </summary>
		public Move()
		{
			Type = EventType.Move;
			Tab = Tabs.Decorations;
		}

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }

		/// <summary>
		/// Gets or sets the position of the move event.
		/// </summary>
		[EaseProperty]
		public RDPointE? Position { get; set; }

		/// <summary>
		/// Gets or sets the scale of the move event.
		/// </summary>
		[EaseProperty]
		public RDSizeE? Scale { get; set; }

		/// <summary>
		/// Gets or sets the angle of the move event.
		/// </summary>
		[EaseProperty]
		public RDExpression? Angle { get; set; }

		/// <summary>
		/// Gets or sets the pivot point of the move event.
		/// </summary>
		[EaseProperty]
		public RDPointE? Pivot { get; set; }

		/// <summary>
		/// Gets or sets the duration of the move event.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Gets or sets the easing type of the move event.
		/// </summary>
		public EaseType Ease { get; set; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString();
	}
}
