using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents the base interface for an event in the rhythm base system.
	/// </summary>
	public interface IBaseEvent : IEvent
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
		Condition Condition { get; set; }
		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		Tabs Tab { get; }
		/// <summary>
		/// Gets or sets the tag associated with the event.
		/// </summary>
		string Tag { get; set; }
		/// <summary>
		/// Gets or sets the run tag associated with the event.
		/// </summary>
		bool RunTag { get; set; }
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		EventType Type { get; }
		/// <summary>
		/// Gets or sets the Y coordinate of the event.
		/// </summary>
		int Y { get; set; }
		/// <summary>
		/// Gets or sets the <see cref="JsonElement"/> associated with the specified property name.
		/// </summary>
		/// <param name="propertyName">The name of the property whose value is to be retrieved or set. The property name is case-sensitive.</param>
		/// <returns></returns>
		JsonElement this[string propertyName] { get; set; }
		/// <summary>  
		/// Creates a deep copy of the current event instance.  
		/// </summary>  
		/// <typeparam name="TEvent">The type of the event to clone, which must implement <see cref="IBaseEvent"/> and have a parameterless constructor.</typeparam>  
		/// <returns>A new instance of <typeparamref name="TEvent"/> that is a copy of the current event.</returns>  
		TEvent Clone<TEvent>() where TEvent : IBaseEvent, new();

		/// <summary>  
		/// Creates a deep copy of the current event instance as an <see cref="IBaseEvent"/>.  
		/// </summary>  
		/// <returns>A new instance of <see cref="IBaseEvent"/> that is a copy of the current event.</returns>  
		IBaseEvent Clone();
	}
}