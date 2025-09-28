using System.Text.Json;

namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a custom event in the ADOFAI game.  
	/// </summary>  
	public class ForwardEvent : BaseEvent, IForwardEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.ForwardEvent;
		/// <summary>  
		/// Gets the actual type of the custom event from the event data.  
		/// </summary>  
		public string ActureType
		{
			get
			{
				return Data.GetProperty("eventType").ToString() ?? "UnknownType";
			}
			set
			{
			//	Data.GetProperty("eventType")
			}
		}
		public ForwardEvent() { }
		public ForwardEvent(JsonDocument doc)
		{

		}
		/// <summary>  
		/// Gets or sets the data associated with the custom event.  
		/// </summary>  
		public JsonElement Data { get; set; } = new();
		/// <summary>  
		/// Returns a string representation of the custom event type.  
		/// </summary>  
		/// <returns>A string that represents the custom event type.</returns>  
		public override string ToString() => ActureType;
	}
}
