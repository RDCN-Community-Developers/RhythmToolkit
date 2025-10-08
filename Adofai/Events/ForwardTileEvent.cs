﻿using System.Text.Json;
namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents a custom tile event in the ADOFAI game.  
	/// </summary>  
	public class ForwardTileEvent : BaseTileEvent, IForwardEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.ForwardTileEvent;
		/// <summary>  
		/// Gets the actual type of the custom tile event from the event data.  
		/// </summary>  
		public string ActureType => Data.GetProperty("eventType").ToString() ?? "UnknownType";
		/// <summary>  
		/// Gets or sets the data associated with the custom tile event.  
		/// </summary>  
		public JsonElement Data { get; set; } = new();
		/// <summary>
		/// Initializes a new instance of the <see cref="ForwardTileEvent"/> class.
		/// </summary>
		public ForwardTileEvent() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="ForwardTileEvent"/> class from a <see cref="JsonDocument"/>.
		/// </summary>
		/// <param name="doc">The <see cref="JsonDocument"/> containing event data.</param>
		public ForwardTileEvent(JsonDocument doc)
		{

		}
		/// <summary>  
		/// Returns a string representation of the custom tile event.  
		/// </summary>  
		/// <returns>A string containing the parent tile's index, angle, and the event type.</returns>  
		public override string ToString() => $"{Parent?.Index}({Parent?.Angle}): {ActureType}";
	}
}
