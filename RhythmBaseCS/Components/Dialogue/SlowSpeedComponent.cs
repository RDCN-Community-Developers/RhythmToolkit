namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a slow speed component in a dialogue.
	/// </summary>
	public struct SlowSpeedComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Serialize() => $"[slow]";
	}
}
