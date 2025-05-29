using Newtonsoft.Json;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents an event that calls a custom method.
	/// </summary>
	public partial class CallCustomMethod : BaseEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CallCustomMethod"/> class.
		/// </summary>
		public CallCustomMethod() { }
		/// <summary>
		/// Gets or sets the name of the method to be called.
		/// </summary>
		public string MethodName { get; set; } = "";
		/// <summary>
		/// Gets or sets the execution time of the method.
		/// </summary>
		public ExecutionTimeOptions ExecutionTime { get; set; }
		/// <summary>
		/// Gets or sets the sort offset for the event.
		/// </summary>
		public int SortOffset { get; set; }
		/// <inheritdoc/>
		public override EventType Type => EventType.CallCustomMethod;
		/// <inheritdoc/>
		[JsonIgnore]
		public RDRoom Rooms { get; set; } = RDRoom.Default();
		/// <inheritdoc/>
		public override Tabs Tab => Tabs.Actions;
		/// <inheritdoc/>
		public override string ToString() => base.ToString() + $" {MethodName}";
		/// <summary>
		/// Specifies the execution time options for the method.
		/// </summary>
		public enum ExecutionTimeOptions
		{
			/// <summary>
			/// Execute the method on prebar.
			/// </summary>
			OnPrebar,
			/// <summary>
			/// Execute the method on bar.
			/// </summary>
			OnBar
		}
	}
}
