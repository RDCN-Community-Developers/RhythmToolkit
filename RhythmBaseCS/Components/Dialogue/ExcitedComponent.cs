namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents an excited component in a dialogue.
	/// </summary>
	public struct ExcitedComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Serialize() => $"[excited]";
	}
}
