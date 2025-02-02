namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a pitch range component in a dialogue.
	/// </summary>
	/// <param name="pitchRange">The pitch range value of the component.</param>
	public struct PitchRangeComponent(float pitchRange) : ITextDialogueComponent<PitchRangeComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "pitchRange";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the pitch range of the component.
		/// </summary>
		public float PitchRange { get; set; } = pitchRange;
		/// <inheritdoc/>
		public PitchRangeComponent Clone(List<IDialogueComponent> components)
		{
			return new PitchRangeComponent()
			{
				Components = components,
				PitchRange = PitchRange
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("pitchRange", PitchRange.ToString());
	}
}
