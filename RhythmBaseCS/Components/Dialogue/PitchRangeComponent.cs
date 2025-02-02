namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a pitch range component in a dialogue.
	/// </summary>
	/// <param name="pitchRange">The pitch range value of the component.</param>
	public struct PitchRangeComponent(float pitchRange) : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the pitch range of the component.
		/// </summary>
		public float PitchRange { get; set; } = pitchRange;
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("pitchRange", PitchRange.ToString());
	}
}
