namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a shake radius component in a dialogue.
	/// </summary>
	/// <param name="shakeRadius">The shake radius value of the component.</param>
	public struct ShakeRadiusComponent(float shakeRadius) : ITextDialogueComponent<ShakeRadiusComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "shakeRadius";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the shake radius of the component.
		/// </summary>
		public float ShakeRadius { get; set; } = shakeRadius;
		/// <inheritdoc/>
		public ShakeRadiusComponent Clone(List<IDialogueComponent> components)
		{
			return new ShakeRadiusComponent()
			{
				Components = components,
				ShakeRadius = ShakeRadius
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("shakeRadius", ShakeRadius.ToString());
	}
}
