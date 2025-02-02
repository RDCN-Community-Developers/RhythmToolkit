namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a speed component in a dialogue.
	/// </summary>
	/// <param name="speed">The speed value of the component.</param>
	public struct SpeedComponent(float speed) : ITextDialogueComponent<SpeedComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "speed";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the speed of the component.
		/// </summary>
		public float Speed { get; set; } = speed;
		/// <inheritdoc/>
		public SpeedComponent Clone(List<IDialogueComponent> components)
		{
			return new SpeedComponent()
			{
				Components = components,
				Speed = Speed
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("speed", Speed.ToString());
	}
}
