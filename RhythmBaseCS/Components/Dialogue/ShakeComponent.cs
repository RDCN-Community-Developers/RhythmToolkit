namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a shake component in a dialogue.
	/// </summary>
	/// <remarks>
	/// This component is used to add a shake effect to the dialogue text.
	/// </remarks>
	public struct ShakeComponent() : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("shake");
	}
}
