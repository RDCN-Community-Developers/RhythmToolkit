namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a very very fast speed component in a dialogue.
	/// </summary>
	public struct VeryVeryFastSpeedComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Name => "vvvfast";
		/// <inheritdoc/>
		public readonly string Serialize() => $"[vvvfast]";
	}
}
