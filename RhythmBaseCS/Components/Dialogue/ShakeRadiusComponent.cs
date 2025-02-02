namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a shake radius component in a dialogue.
	/// </summary>
	/// <param name="volume">The shake radius value of the component.</param>
	public struct ShakeRadiusComponent(float volume) : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the shake radius of the component.
		/// </summary>
		public float ShakeRadius { get; set; } = volume;
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("shakeRadius", ShakeRadius.ToString());
	}
}
