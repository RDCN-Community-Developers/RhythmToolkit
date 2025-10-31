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
		public ShakeScreen() { }
		/// <summary>
		/// Gets or sets the rooms associated with the event.
		/// </summary>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>
		/// Gets or sets the shake level of the event.
		/// </summary>
		public ScreenShakeLevels ShakeLevel { get; set; } = ScreenShakeLevels.Medium;
		/// <summary>
		/// Gets or sets the type of shake effect to apply.
		/// </summary>
		public ShakeType ShakeType { get; set; } = ShakeType.Normal;
		/// <summary>
		/// Gets the type of the event.
		/// </summary>		
		public override EventType Type { get; } = EventType.ShakeScreen;
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; } = Tabs.Actions;
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => base.ToString() + string.Format(" {0}", ShakeLevel);
	}
	/// <summary>
	/// Defines the levels of screen shake.
	/// </summary>
	[RDJsonEnumSerializable]
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
