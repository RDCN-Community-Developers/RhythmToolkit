namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a very slow speed component in a dialogue.
	/// </summary>
	public struct VerySlowSpeedComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Serialize() => $"[vslow]";
	}
}
