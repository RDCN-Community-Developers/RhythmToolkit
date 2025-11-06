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
		}
		/// <inheritdoc/>
		public RDRoom Rooms { get; set; } = new RDRoom([0]);
		/// <summary>  
		/// Gets or sets the strength of the BassDrop event.  
		/// </summary>  
		public BassDropStrengthTypes Strength { get; set; } = BassDropStrengthTypes.High;
		/// <inheritdoc/>
		public override EventType Type => EventType.BassDrop;

		/// <inheritdoc/>
		public override Tabs Tab => Tabs.Actions;

		/// <inheritdoc/>
		public override string ToString() => base.ToString() + $" {Strength}";
	}
	/// <summary>  
	/// Defines the strength levels for the BassDrop event.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum BassDropStrengthTypes
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
