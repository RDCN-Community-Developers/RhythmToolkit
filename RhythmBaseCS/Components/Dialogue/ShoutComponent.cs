namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a shout component in a dialogue.
	/// </summary>
	public struct ShoutComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Serialize() => $"[shout]";
	}
}
