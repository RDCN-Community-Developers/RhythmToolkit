using System.Text.Json;

namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the base class for all ADOFAI events.
	/// </summary>
	public abstract class BaseEvent : IBaseEvent
	{
		/// <inheritdoc/>
		public abstract EventType Type { get; }

		internal Dictionary<string, JsonElement> _extraData = [];
		/// <summary>
		/// Returns a string representation of the event type.
		/// </summary>
		/// <returns>A string that represents the event type.</returns>
		public override string ToString() => Type.ToString();
	}
}
