using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>  
	/// Interface representing an ease event.  
	/// </summary>  
	public interface IEaseEvent
	{
		/// <summary>  
		/// Gets or sets the type of easing.  
		/// </summary>  
		Ease.EaseType Ease { get; set; }

		/// <summary>  
		/// Gets or sets the duration of the ease event.  
		/// </summary>  
		float Duration { get; set; }
	}
}
