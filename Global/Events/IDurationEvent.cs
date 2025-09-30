namespace RhythmBase.Global.Events
{
	/// <summary>  
	/// Interface representing a duration event.  
	/// </summary>  
	public interface IDurationEvent : IEvent
	{
		/// <summary>  
		/// Gets or sets the duration of the ease event.  
		/// </summary>  
		float Duration { get; set; }
	}
}
