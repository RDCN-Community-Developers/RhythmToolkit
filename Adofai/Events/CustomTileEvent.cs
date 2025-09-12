//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//namespace RhythmBase.Adofai.Events
//{
//	/// <summary>  
//	/// Represents a custom tile event in the ADOFAI game.  
//	/// </summary>  
//	public class CustomTileEvent : BaseTileEvent
//	{
//		/// <inheritdoc/>
//		public override EventType Type => EventType.CustomTileEvent;
//		/// <summary>  
//		/// Gets the actual type of the custom tile event from the event data.  
//		/// </summary>  
//		[JsonIgnore]
//		public string ActureType => Data["eventType"]?.ToString()??"UnknownType";
//		/// <summary>  
//		/// Gets or sets the data associated with the custom tile event.  
//		/// </summary>  
//		public JObject Data { get; set; } = [];
//		/// <summary>  
//		/// Returns a string representation of the custom tile event.  
//		/// </summary>  
//		/// <returns>A string containing the parent tile's index, angle, and the event type.</returns>  
//		public override string ToString() => $"{Parent?.Index}({Parent?.Angle}): {ActureType}";
//	}
//}
