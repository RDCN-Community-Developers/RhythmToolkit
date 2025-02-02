namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a normal speed component in a dialogue.
	/// </summary>
	public struct NormalSpeedComponent : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Name => "normal";
		/// <inheritdoc/>
		public readonly string Serialize() => $"[normal]";
	}
}
