namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a loud text component in a dialogue.
	/// </summary>
	public struct LoudComponent() : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("loud");
	}
}
