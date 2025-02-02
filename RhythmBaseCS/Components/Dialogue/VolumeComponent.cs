namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a volume component in a dialogue.
	/// </summary>
	/// <param name="volume">The volume value of the component.</param>
	public struct VolumeComponent(float volume) : ITextDialogueComponent<VolumeComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "volume";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the volume of the component.
		/// </summary>
		public float Volume { get; set; } = volume;
		/// <inheritdoc/>
		public VolumeComponent Clone(List<IDialogueComponent> components)
		{
			return new VolumeComponent()
			{
				Components = components,
				Volume = Volume
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("volume", Volume.ToString());
	}
}
