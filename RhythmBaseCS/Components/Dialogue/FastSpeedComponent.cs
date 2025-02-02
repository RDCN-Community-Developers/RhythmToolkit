namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a fast speed component in a dialogue.
	/// </summary>
	public struct FastSpeedComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Serialize() => $"[fast]";
	}
}
