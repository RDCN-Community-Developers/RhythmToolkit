using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>  
	/// Represents a BassDrop event in the rhythm base.  
	/// </summary>  
	public class BassDrop : BaseEvent, IRoomEvent
	{
		/// <summary>  
		/// Initializes a new instance of the <see cref="BassDrop"/> class.  
		/// </summary>  
		public BassDrop()
		{
			Rooms = new RDRoom(true, [0]);
			Type = EventType.BassDrop;
			Tab = Tabs.Actions;
		}
		/// <inheritdoc/>
		public RDRoom Rooms { get; set; }
		/// <summary>  
		/// Gets or sets the strength of the BassDrop event.  
		/// </summary>  
		public StrengthType Strength { get; set; }
		/// <inheritdoc/>
		public override EventType Type { get; }
		/// <inheritdoc/>
		public override Tabs Tab { get; }
		/// <inheritdoc/>
		public override string ToString() => base.ToString() + $" {Strength}";
		/// <summary>  
		/// Defines the strength levels for the BassDrop event.  
		/// </summary>  
		public enum StrengthType
		{
			/// <summary>  
			/// Low strength.  
			/// </summary>  
			Low,
			/// <summary>  
			/// Medium strength.  
			/// </summary>  
			Medium,
			/// <summary>  
			/// High strength.  
			/// </summary>  
			High
		}
	}
}
