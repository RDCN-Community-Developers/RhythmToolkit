namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a pause component in a dialogue.
	/// </summary>
	public struct PauseComponent(TimeSpan duration) : IDialogueComponent
	{
		/// <inheritdoc/>
		public readonly string Name => "pause";
		/// <summary>
		/// Gets or sets the duration of the pause.
		/// </summary>
		public TimeSpan Duration { get; set; } = duration;
		/// <inheritdoc/>
		public readonly string Serialize() => $"[{Duration.TotalSeconds}]";
	}
}
