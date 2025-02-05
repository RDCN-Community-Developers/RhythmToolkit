namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a list of dialogue lines.
	/// </summary>
	public class RDDialogueExchange : List<RDDialogueBlock>
	{
		/// <summary>
		/// Serializes the dialogue list to a string.
		/// </summary>
		/// <returns>A string representation of the dialogue list.</returns>
		public string Serialize() => string.Join('\n', this.Select(i => i.Serialize()));
		/// <summary>
		/// Deserializes a string into a <see cref="RDDialogueExchange"/> instance.
		/// </summary>
		/// <param name="text">The string to deserialize.</param>
		/// <returns>A new <see cref="RDDialogueExchange"/> containing the deserialized dialogue lines.</returns>
		public static RDDialogueExchange Deserialize(string text) => [.. text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(RDDialogueBlock.Deserialize)];
		/// <summary>
		/// Deserializes a string into a <see cref="RDDialogueExchange"/> instance, using a lookup of valid expressions.
		/// </summary>
		/// <param name="text">The string to deserialize.</param>
		/// <param name="expressions">A lookup of valid expressions for each character.</param>
		/// <returns>A new <see cref="RDDialogueExchange"/> containing the deserialized dialogue lines.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the input string is null.</exception>
		/// <exception cref="FormatException">Thrown when the input string has an invalid format.</exception>
		public static RDDialogueExchange Deserialize(string text, ILookup<string, string> expressions) => [.. text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(i => RDDialogueBlock.Deserialize(i, expressions))];
		///<inheritdoc/>
		public override string ToString() => string.Join('\n', this.Select(i => i.ToString()));
	}
}