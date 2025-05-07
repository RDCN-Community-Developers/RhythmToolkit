using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a custom event in the ADOFAI game.  
	/// </summary>  
	public class ADCustomEvent : ADBaseEvent
	{
		/// <inheritdoc/>
		public override ADEventType Type => ADEventType.CustomEvent;		/// <summary>  
		/// Gets the actual type of the custom event from the event data.  
		/// </summary>  
		[JsonIgnore]
		public string ActureType => Data["eventType"]?.ToString() ?? "UnknownType";		/// <summary>  
		/// Gets or sets the data associated with the custom event.  
		/// </summary>  
		public JObject Data { get; set; } = [];		/// <summary>  
		/// Returns a string representation of the custom event type.  
		/// </summary>  
		/// <returns>A string that represents the custom event type.</returns>  
		public override string ToString() => ActureType;
	}
}
