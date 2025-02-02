namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a color component in a dialogue.
	/// </summary>
	public struct ColorComponent(RDColor color) : ITextDialogueComponent
	{
		/// <inheritdoc/>
		public List<IDialogueComponent> Components { get; set; } = [];
		/// <summary>
		/// Gets or sets the color of the component.
		/// </summary>
		public RDColor Color { get; set; } = color;
		/// <inheritdoc/>
		public readonly string Serialize()
		{
			if (Color.A == 255)
				return ((ITextDialogueComponent)this).Serilaize("color", Color.ToString("#RRGGBB"));
			else
				return ((ITextDialogueComponent)this).Serilaize("color", Color.ToString("#RRGGBBAA"));
		}
	}
}
