namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a bold text component in a dialogue.
	/// </summary>
	public struct BoldComponent() : ITextDialogueComponent<BoldComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "bold";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public BoldComponent Clone(List<IDialogueComponent> components)
		{
			return new BoldComponent()
			{
				Components = components
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("bold");
	}
}
