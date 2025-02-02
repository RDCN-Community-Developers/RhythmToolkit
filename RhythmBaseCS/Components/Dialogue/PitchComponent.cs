namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a pitch component in a dialogue.
	/// </summary>
	/// <param name="pitch">The pitch value of the component.</param>
	public struct PitchComponent(float pitch) : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the pitch of the component.
		/// </summary>
		public float Pitch { get; set; } = pitch;
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("pitch", Pitch.ToString());
	}
}
