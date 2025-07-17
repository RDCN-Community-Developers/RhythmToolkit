namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an event to set the content of a window.  
	/// Inherits from <see cref="BaseWindowEvent"/>.  
	/// </summary>  
	public class SetWindowContent : BaseWindowEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetWindowContent;

		/// <summary>  
		/// Gets or sets the mode for displaying the content.  
		/// Defaults to <see cref="ContentModes.OnTop"/>.  
		/// </summary>  
		public ContentModes ContentMode { get; set; } = ContentModes.OnTop;

		/// <summary>  
		/// Specifies the available modes for displaying content in the window.  
		/// </summary>  
		public enum ContentModes
		{
			/// <summary>  
			/// Displays the content on top of other elements.  
			/// </summary>  
			OnTop,
		}
	}
}
