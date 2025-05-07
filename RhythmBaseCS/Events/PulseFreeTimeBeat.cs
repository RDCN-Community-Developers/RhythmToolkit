using Microsoft.VisualBasic.CompilerServices;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents a pulse free time beat event.
	/// </summary>
	public class PulseFreeTimeBeat : BaseBeat
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PulseFreeTimeBeat"/> class.
		/// </summary>
		public PulseFreeTimeBeat()
		{
			Type = EventType.PulseFreeTimeBeat;
		}		/// <summary>
		/// Gets or sets the hold duration.
		/// </summary>
		public float Hold { get; set; }		/// <summary>
		/// Gets or sets the action type.
		/// </summary>
		public ActionType Action { get; set; }		/// <summary>
		/// Gets or sets the custom pulse value.
		/// </summary>
		public uint CustomPulse { get; set; }		/// <summary>
		/// Gets the event type.
		/// </summary>
		public override EventType Type { get; }		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			string Out = "";
			switch (Action)
			{
				case ActionType.Increment:
					Out = ">";
					break;
				case ActionType.Decrement:
					Out = "<";
					break;
				case ActionType.Custom:
					Out = (CustomPulse + 1).ToString();
					break;
				case ActionType.Remove:
					Out = "X";
					break;
			}
			return base.ToString() + $"{Out}";
		}		/// <summary>
		/// Defines the action types for the pulse free time beat.
		/// </summary>
		public enum ActionType
		{
			/// <summary>
			/// Increment action.
			/// </summary>
			Increment,
			/// <summary>
			/// Decrement action.
			/// </summary>
			Decrement,
			/// <summary>
			/// Custom action.
			/// </summary>
			Custom,
			/// <summary>
			/// Remove action.
			/// </summary>
			Remove
		}
	}
}
