namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a wave speed component in a dialogue.
	/// </summary>
	/// <param name="speed">The speed value of the component.</param>
	public struct WaveSpeedComponent(float speed) : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the speed of the component.
		/// </summary>
		public float Speed { get; set; } = speed;
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("shakeSpeed", Speed.ToString());
	}
}
