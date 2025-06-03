using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that shakes the screen.
	/// </summary>
	public class ShakeScreen : BaseEvent, IRoomEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShakeScreen"/> class.
		/// </summary>
		public ShakeScreen()
		{
			Rooms = new RDRoom(true, [0]);
			Type = EventType.ShakeScreen;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; }
		/// <summary>
		/// Gets or sets the shake level of the event.
		/// </summary>
		public ScreenShakeLevels ShakeLevel { get; set; }
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
		public override string ToString() => base.ToString() + string.Format(" {0}", ShakeLevel);
	}
	/// <summary>
	/// Defines the levels of screen shake.
	/// </summary>
	public enum ScreenShakeLevels
	{
		/// <summary>
		/// Low level of screen shake.
		/// </summary>
		Low,
		/// <summary>
		/// Medium level of screen shake.
		/// </summary>
		Medium,
		/// <summary>
		/// High level of screen shake.
		/// </summary>
		High
	}
}
