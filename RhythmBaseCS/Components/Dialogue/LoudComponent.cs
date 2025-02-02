namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a loud text component in a dialogue.
	/// </summary>
	public struct LoudComponent() : ITextDialogueComponent<LoudComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "loud";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public LoudComponent Clone(List<IDialogueComponent> components)
		{
			return new LoudComponent()
			{
				Components = components
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("loud");
	}
}
