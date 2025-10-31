using RhythmBase.Global.Components.Easing;
namespace RhythmBase.Global.Events
{
	/// <summary>  
	/// Interface representing an ease event.  
	/// </summary>  
	public interface IEaseEvent : IDurationEvent
	{
		/// <summary>  
		/// Gets or sets the type of easing.  
		/// </summary>  
		EaseType Ease { get; set; }
		///// <summary>  
		///// Gets the default ease event.  
		///// </summary>  
		///// <value>  
		///// The default ease event.  
		///// </value>  
		//static abstract IEaseEvent DefaultEvent => default;
	}
}