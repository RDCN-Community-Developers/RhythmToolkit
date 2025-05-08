using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents the base interface for an event in the rhythm base system.
	/// </summary>
	public interface IBaseEvent
	{
		/// <summary>
		/// Gets or sets a value indicating whether the event is active.
		/// </summary>
		bool Active { get; set; }

		/// <summary>
		/// Gets or sets the beat associated with the event.
		/// </summary>
		RDBeat Beat { get; set; }

		/// <summary>
		/// Gets or sets the condition associated with the event.
		/// </summary>
		Condition? Condition { get; set; }

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		Tabs Tab { get; }

		/// <summary>
		/// Gets or sets the tag associated with the event.
		/// </summary>
		string Tag { get; set; }

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		EventType Type { get; }

		/// <summary>
		/// Gets or sets the Y coordinate of the event.
		/// </summary>
		int Y { get; set; }

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		string ToString();
	}
}