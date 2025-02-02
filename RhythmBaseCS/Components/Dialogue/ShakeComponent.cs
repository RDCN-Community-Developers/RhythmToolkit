namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a shake component in a dialogue.
	/// </summary>
	/// <remarks>
	/// This component is used to add a shake effect to the dialogue text.
	/// </remarks>
	public struct ShakeComponent() : ITextDialogueComponent<ShakeComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "shake";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <inheritdoc/>
		public ShakeComponent Clone(List<IDialogueComponent> components)
		{
			return new ShakeComponent()
			{
				Components = components
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("shake");
	}
}
