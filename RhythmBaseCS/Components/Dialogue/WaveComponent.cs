namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a wave component in a dialogue.
	/// </summary>
	public struct WaveComponent() : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("shake");
	}
}
