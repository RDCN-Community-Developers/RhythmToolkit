namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a whisper component in a dialogue.
	/// </summary>
	public struct WhisperComponent() : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("whisper");
	}
}
