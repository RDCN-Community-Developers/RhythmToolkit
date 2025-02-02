namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a sticky component in a dialogue.
	/// </summary>
	public struct StickyComponent() : ITextDialogueComponent<StickyComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "sticky";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public StickyComponent Clone(List<IDialogueComponent> components)
		{
			return new StickyComponent()
			{
				Components = components
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("sticky");
	}
}
