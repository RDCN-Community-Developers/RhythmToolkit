namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a very fast speed component in a dialogue.
	/// </summary>
	public struct VeryFastSpeedComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Serialize() => $"[vfast]";
	}
}
