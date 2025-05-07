using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the base class for all ADOFAI events.
	/// </summary>
	public abstract class ADBaseEvent
	{
		/// <inheritdoc/>
		public abstract ADEventType Type { get; }
		/// <summary>
		/// Returns a string representation of the event type.
		/// </summary>
		/// <returns>A string that represents the event type.</returns>
		public override string ToString() => Type.ToString();
	}
}
