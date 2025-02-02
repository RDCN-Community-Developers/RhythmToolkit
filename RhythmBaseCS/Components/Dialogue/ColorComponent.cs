namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a color component in a dialogue.
	/// </summary>
	public struct ColorComponent(RDColor color) : ITextDialogueComponent<ColorComponent>
	{
		/// <inheritdoc/>
		public readonly string Name => "color";
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the color of the component.
		/// </summary>
		public RDColor Color { get; set; } = color;
		/// <inheritdoc/>
		public ColorComponent Clone(List<IDialogueComponent> components)
		{
			return new ColorComponent(Color)
			{
				Components = components,
				Color = Color
			};
		}
		ITextDialogueComponent ITextDialogueComponent.Clone(List<IDialogueComponent> components) => Clone(components);
		/// <inheritdoc/>
		public readonly string Serialize() => ((ITextDialogueComponent)this).Serilaize("color", Color.ToString(Color.A == 255 ? "#RRGGBB" : "#RRGGBBAA"));
	}
}
