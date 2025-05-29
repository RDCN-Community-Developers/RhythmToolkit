using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.Global.Exceptions
{
	/// <summary>
	/// Exception thrown when an event is unreadable.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="UnreadableEventException"/> class with a specified error message and the unreadable event item.
	/// </remarks>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="item">The unreadable event item.</param>
	public class UnreadableEventException(string message, IBaseEvent item) : RhythmBaseException(message)
	{
		/// <summary>
		/// Gets the unreadable event item.
		/// </summary>
		public IBaseEvent Item => item;
	}
}
