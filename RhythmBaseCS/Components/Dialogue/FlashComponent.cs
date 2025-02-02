namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a component that clear the text effects in a dialogue.
	/// </summary>
	public struct FlashComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Name => "flash";
		/// <inheritdoc/>
		public readonly string Serialize() => $"[flash]";
	}
}
