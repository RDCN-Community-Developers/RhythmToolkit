using System.Diagnostics.CodeAnalysis;

namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Enum representing the different types of rich string events.
	/// </summary>
	public enum RDRichStringEventType
	{
		/// <summary>
		/// Static event type.
		/// </summary>
		Static,
		/// <summary>
		/// Flash event type.
		/// </summary>
		Flash,
		/// <summary>
		/// Very slow event type.
		/// </summary>
		VerySlow,
		/// <summary>
		/// Slow event type.
		/// </summary>
		Slow,
		/// <summary>
		/// Normal event type.
		/// </summary>
		Normal,
		/// <summary>
		/// Fast event type.
		/// </summary>
		Fast,
		/// <summary>
		/// Very fast event type.
		/// </summary>
		VeryFast,
		/// <summary>
		/// Very very fast event type.
		/// </summary>
		VeryVeryFast,
		/// <summary>
		/// Excited event type.
		/// </summary>
		Excited,
		/// <summary>
		/// Shout event type.
		/// </summary>
		Shout,
		/// <summary>
		/// Shake event type.
		/// </summary>
		Shake,
	}
	/// <summary>
	/// Class representing a rich string event.
	/// </summary>
	/// <param name="Type">Rich string event type.</param>
	/// <param name="Index">Rich string event index.</param>
	public record RDRichStringEvent(RDRichStringEventType Type, int Index)
	{
		/// <summary>
		/// Serializes the rich string event type to its corresponding string representation.
		/// </summary>
		/// <returns>A string representation of the rich string event type.</returns>
		/// <exception cref="NotImplementedException">Thrown when the event type is not implemented.</exception>
		public string Serialize() => "[" + Type switch
		{
			RDRichStringEventType.Static => "static",
			RDRichStringEventType.Flash => "flash",
			RDRichStringEventType.VerySlow => "vslow",
			RDRichStringEventType.Slow => "slow",
			RDRichStringEventType.Normal => "normal",
			RDRichStringEventType.Fast => "fast",
			RDRichStringEventType.VeryFast => "vfast",
			RDRichStringEventType.VeryVeryFast => "vvvfast",
			RDRichStringEventType.Excited => "excited",
			RDRichStringEventType.Shout => "shout",
			RDRichStringEventType.Shake => "shake",
			_ => throw new NotImplementedException(),
		} + "]";
		/// <summary>
		/// Creates a new instance of <see cref="RDRichStringEvent"/> based on the provided type and index.
		/// </summary>
		/// <param name="type">The string representation of the event type.</param>
		/// <param name="index">The index of the event.</param>
		/// <param name="result">The created <see cref="RDRichStringEvent"/> instance if successful, otherwise null.</param>
		/// <returns>True if the event was successfully created, otherwise false.</returns>
		public static bool Create(string type, int index, [NotNullWhen(true)] out RDRichStringEvent? result)
		{
			RDRichStringEventType? eventType = type switch
			{
				"static" => RDRichStringEventType.Static,
				"flash" => RDRichStringEventType.Flash,
				"vslow" => RDRichStringEventType.VerySlow,
				"slow" => RDRichStringEventType.Slow,
				"normal" => RDRichStringEventType.Normal,
				"fast" => RDRichStringEventType.Fast,
				"vfast" => RDRichStringEventType.VeryFast,
				"vvvfast" => RDRichStringEventType.VeryVeryFast,
				"excited" => RDRichStringEventType.Excited,
				"shout" => RDRichStringEventType.Shout,
				"shake" => RDRichStringEventType.Shake,
				_ => null,
			};
			if (eventType == null)
			{
				result = null;
				return false;
			}
			result = new(eventType.Value, index);
			return true;
		}
	}
}