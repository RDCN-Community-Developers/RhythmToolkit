using RhythmBase.Global.Components.Easing;
using RhythmBase.Global.Components.Vector;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents an event to set the content of a window.  
	/// Inherits from <see cref="BaseWindowEvent"/>.  
	/// </summary>  
	public class SetWindowContent : BaseWindowEvent, IEaseEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.SetWindowContent;

		/// <summary>  
		/// Gets or sets the mode for displaying the content.  
		/// Defaults to <see cref="WindowContentModes.OnTop"/>.  
		/// </summary>  
		public WindowContentModes ContentMode { get; set; } = WindowContentModes.OnTop;
		public int RoomIndex { get; set; } = 0;
		[RDJsonCondition($"$&.{nameof(Position)} is not null")]
		public RDPoint? Position { get; set; }
		[RDJsonCondition($"$&.{nameof(Zoom)} is not null")]
		public int? Zoom { get; set; }
		[RDJsonCondition($"$&.{nameof(Angle)} is not null")]
		public float? Angle { get; set; }
		public EaseType Ease { get; set; }
		public float Duration { get; set; }
	}

	/// <summary>  
	/// Specifies the available modes for displaying content in the window.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum WindowContentModes
	{
		/// <summary>  
		/// Displays the content on top of other elements.  
		/// </summary>  
		OnTop,
		Room,
	}
}
