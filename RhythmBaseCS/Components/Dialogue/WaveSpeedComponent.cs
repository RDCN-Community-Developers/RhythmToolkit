namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a wave speed component in a dialogue.
	/// </summary>
	/// <param name="speed">The speed value of the component.</param>
	public struct WaveSpeedComponent(float speed) : ITextDialogueComponent<WaveSpeedComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "waveSpeed";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the speed of the component.
		/// </summary>
		public float Speed { get; set; } = speed;
		/// <inheritdoc/>
		public WaveSpeedComponent Clone(List<IDialogueComponent> components)
		{
			return new WaveSpeedComponent()
			{
				Components = components,
				Speed = Speed
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("waveSpeed", Speed.ToString());
	}
}
