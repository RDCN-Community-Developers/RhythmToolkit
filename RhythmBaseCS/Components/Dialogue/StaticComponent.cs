namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a component that clear the text effects in a dialogue.
	/// </summary>
	public struct StaticComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Name => "static";
		/// <inheritdoc/>
		public readonly string Serialize() => $"[static]";
	}
}
