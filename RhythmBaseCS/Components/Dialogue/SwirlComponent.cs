namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a swirl component in a dialogue.
	/// </summary>
	public struct SwirlComponent() : ITextDialogueComponent<SwirlComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "swirl";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public SwirlComponent Clone(List<IDialogueComponent> components)
		{
			return new SwirlComponent()
			{
				Components = components
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("swirl");
	}
}
