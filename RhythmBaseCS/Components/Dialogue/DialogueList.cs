namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a list of dialogue lines.
	/// </summary>
	public class DialogueList : List<DialogueLine>
	{
		/// <summary>
		/// Serializes the dialogue list to a string.
		/// </summary>
		/// <returns>A string representation of the dialogue list.</returns>
		public string Serialize() => string.Join("\n", this.Select(i => i.Serialize()));
	}
}
