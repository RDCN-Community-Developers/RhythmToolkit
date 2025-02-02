namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a swirl speed component in a dialogue.
	/// </summary>
	/// <param name="speed">The speed value of the component.</param>
	public struct SwirlSpeedComponent(float speed) : ITextDialogueComponent<SwirlSpeedComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "swirlSpeed";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the speed of the component.
		/// </summary>
		public float Speed { get; set; } = speed;
		/// <inheritdoc/>
		public SwirlSpeedComponent Clone(List<IDialogueComponent> components)
		{
			return new SwirlSpeedComponent()
			{
				Components = components,
				Speed = Speed
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("swirlSpeed", Speed.ToString());
	}
}
