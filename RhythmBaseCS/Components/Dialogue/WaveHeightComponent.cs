namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a wave height component in a dialogue.
	/// </summary>
	/// <param name="height">The height value of the component.</param>
	public struct WaveHeightComponent(float height) : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the height of the component.
		/// </summary>
		public float Height { get; set; } = height;
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("shakeHeight", Height.ToString());
	}
}
