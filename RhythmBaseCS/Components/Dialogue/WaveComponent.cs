namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a wave component in a dialogue.
	/// </summary>
	public struct WaveComponent() : ITextDialogueComponent<WaveComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "shake";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public WaveComponent Clone(List<IDialogueComponent> components)
		{
			return new WaveComponent()
			{
				Components = components
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("shake");
	}
}
