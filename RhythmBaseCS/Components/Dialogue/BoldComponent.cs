namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a bold text component in a dialogue.
	/// </summary>
	public struct BoldComponent() : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("bold");
	}
}
