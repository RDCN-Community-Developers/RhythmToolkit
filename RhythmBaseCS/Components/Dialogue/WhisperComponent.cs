namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a whisper component in a dialogue.
	/// </summary>
	public struct WhisperComponent() : ITextDialogueComponent<WhisperComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "whisper";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public WhisperComponent Clone(List<IDialogueComponent> components)
		{
			return new WhisperComponent()
			{
				Components = components
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("whisper");
	}
}
