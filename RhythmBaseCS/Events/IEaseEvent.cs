using RhythmBase.Components.Easing;
namespace RhythmBase.Events
{
	/// <summary>  
	/// Interface representing an ease event.  
	/// </summary>  
	public interface IEaseEvent:IDurationEvent
	{
		/// <summary>  
		/// Gets or sets the type of easing.  
		/// </summary>  
		EaseType Ease { get; set; }
	}
}