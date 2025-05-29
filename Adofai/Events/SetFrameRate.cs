﻿namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Represents an event to set the frame rate in the Adofai editor.  
	/// </summary>  
	/// <remarks>  
	/// This event allows enabling or disabling frame rate control and specifies the desired frame rate value.  
	/// </remarks>  
	public class SetFrameRate : BaseTaggedTileAction, IStartEvent
	{
		/// <summary>  
		/// Gets the type of the event, which is <see cref="EventType.SetFrameRate"/>.  
		/// </summary>  
		public override EventType Type => EventType.SetFrameRate;

		/// <summary>  
		/// Gets or sets a value indicating whether the frame rate control is enabled.  
		/// </summary>  
		public bool Enabled { get; set; }

		/// <summary>  
		/// Gets or sets the frame rate value to be applied.  
		/// </summary>  
		public float FrameRate { get; set; }
	}
}
