namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a plain text component in a dialogue.
	/// </summary>
	public struct PlainTextComponent(string text) : IDialogueComponent
	{
		/// <summary>
		/// Gets or sets the text of the component.
		/// </summary>
		public string Text { get; set; } = text;
		/// <inheritdoc/>
		public readonly string Serialize() => Text;
		/// <inheritdoc/>
		public readonly string Plain() => Text;
	}
}
