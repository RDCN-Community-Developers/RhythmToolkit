namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a list of dialogue lines.
	/// </summary>
	public class RDDialogueList : List<RDDialogueLine>
	{
		/// <summary>
		/// Serializes the dialogue list to a string.
		/// </summary>
		/// <returns>A string representation of the dialogue list.</returns>
		public string Serialize() => string.Join('\n', this.Select(i => i.Serialize()));
		/// <summary>
		/// Deserializes a string into a <see cref="RDDialogueList"/> instance.
		/// </summary>
		/// <param name="text">The string to deserialize.</param>
		/// <returns>A new <see cref="RDDialogueList"/> containing the deserialized dialogue lines.</returns>
		public static RDDialogueList Deserialize(string text) => [.. text.Split("\r\n").Select(RDDialogueLine.Deserialize)];
		///<inheritdoc/>
		public override string ToString() => string.Join('\n', this.Select(i => i.ToString()));
	}
}
