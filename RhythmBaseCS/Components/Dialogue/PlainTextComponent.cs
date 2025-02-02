using System.Diagnostics;

namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a plain text component in a dialogue.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct PlainTextComponent(string text) : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Name => "";
		/// <summary>
		/// Gets or sets the text of the component.
		/// </summary>
		public string Text { get; set; } = text;
		/// <inheritdoc/>
		public readonly string Serialize() => Text;
		/// <inheritdoc/>
		public readonly string Plain() => Text;
		/// <inheritdoc/>
		public override readonly string ToString() => Text;
		private string GetDebuggerDisplay() => ToString();
	}
}
