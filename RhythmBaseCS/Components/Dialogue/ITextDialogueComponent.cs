namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a text-based dialogue component that can contain other dialogue components.
	/// </summary>
	public interface ITextDialogueComponent : IDialogueComponent
	{
		/// <summary>
		/// Gets or sets the list of child dialogue components.
		/// </summary>
		public List<IDialogueComponent> Components { get; set; }
		/// <inheritdoc/>
		string IDialogueComponent.Plain() => string.Join("", Components.Select(i => i.Plain()));

		/// <summary>
		/// Serializes the text dialogue component with a specified label.
		/// </summary>
		/// <param name="label">The label to use for serialization.</param>
		/// <returns>A string representation of the text dialogue component.</returns>
		string Serilaize(string label) => $"<{label}>{string.Join("", Components.Select(i => i.Serialize()))}</{label}>";

		/// <summary>
		/// Serializes the text dialogue component with a specified label and value.
		/// </summary>
		/// <param name="label">The label to use for serialization.</param>
		/// <param name="value">The value to use for serialization.</param>
		/// <returns>A string representation of the text dialogue component.</returns>
		string Serilaize(string label, string value) => $"<{label}={value}>{string.Join("", Components.Select(i => i.Serialize()))}</{label}>";
		ITextDialogueComponent Clone(List<IDialogueComponent> components);
	}
	/// <summary>
	/// Represents a text-based dialogue component that can contain other dialogue components.
	/// </summary>
	public interface ITextDialogueComponent<TSelf> : ITextDialogueComponent
		where TSelf : ITextDialogueComponent<TSelf>
	{
		/// <summary>
		/// Clones the text dialogue component with a specified list of child components.
		/// </summary>
		/// <param name="components">The list of child dialogue components to include in the clone.</param>
		/// <returns>A new instance of the text dialogue component with the specified child components.</returns>
		TSelf Clone(List<IDialogueComponent> components);
	}
}
