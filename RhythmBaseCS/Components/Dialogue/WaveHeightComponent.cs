namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a wave height component in a dialogue.
	/// </summary>
	/// <param name="height">The height value of the component.</param>
	public struct WaveHeightComponent(float height) : ITextDialogueComponent<WaveHeightComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "waveHeight";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the height of the component.
		/// </summary>
		public float Height { get; set; } = height;
		/// <inheritdoc/>
		public WaveHeightComponent Clone(List<IDialogueComponent> components)
		{
			return new WaveHeightComponent()
			{
				Components = components,
				Height = Height
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("waveHeight", Height.ToString());
	}
}
