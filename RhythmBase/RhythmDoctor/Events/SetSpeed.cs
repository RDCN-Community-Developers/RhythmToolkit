using RhythmBase.Global.Components.Easing;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that sets the speed in the rhythm base.
	/// </summary>
	public class SetSpeed : BaseEvent, IEaseEvent
	{
		///<inheritdoc/>
		public EaseType Ease { get; set; } = EaseType.Linear;
		/// <summary>
		/// Gets or sets the speed for the event.
		/// </summary>
		[Tween]
		public float Speed { get; set; } = 1f;
		///<inheritdoc/>
		public float Duration { get; set; } = 0;
		///<inheritdoc/>
		public override EventType Type => EventType.SetSpeed;
		///<inheritdoc/>
		public override Tab Tab => Tab.Actions;
		///<inheritdoc/>
		public override string ToString() => base.ToString() + $" Speed:{Speed}";
	}
}
