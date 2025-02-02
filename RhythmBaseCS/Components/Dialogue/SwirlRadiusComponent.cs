namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a swirl radius component in a dialogue.
	/// </summary>
	/// <param name="radius">The radius value of the component.</param>
	public struct SwirlRadiusComponent(float radius) : ITextDialogueComponent<SwirlRadiusComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "swirlRadius";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the height of the component.
		/// </summary>
		public float Radius { get; set; } = radius;
		/// <inheritdoc/>
		public SwirlRadiusComponent Clone(List<IDialogueComponent> components)
		{
			return new SwirlRadiusComponent()
			{
				Components = components,
				Radius = Radius
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("swirlRadius", Radius.ToString());
	}
}
