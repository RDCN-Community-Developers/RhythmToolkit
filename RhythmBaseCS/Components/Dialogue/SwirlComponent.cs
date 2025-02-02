namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a swirl component in a dialogue.
	/// </summary>
	public struct SwirlComponent() : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("swirl");
	}
}
