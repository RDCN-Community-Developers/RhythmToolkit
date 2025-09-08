using Newtonsoft.Json;
using RhythmBase.Global.Components.Easing;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that sets the speed in the rhythm base.
	/// </summary>
	public class SetSpeed : BaseEvent, IEaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SetSpeed"/> class.
		/// </summary>
		public SetSpeed()
		{
			Type = EventType.SetSpeed;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets the type of easing for the event.
		/// </summary>
		public EaseType Ease { get; set; }
		/// <summary>
		/// Gets or sets the speed for the event.
		/// </summary>
		[EaseProperty]
		public float Speed { get; set; }
		/// <summary>
		/// Gets or sets the duration of the event.
		/// </summary>
		public float Duration { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" Speed:{0}", Speed);
	}
}
