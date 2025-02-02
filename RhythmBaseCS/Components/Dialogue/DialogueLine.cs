namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a line of dialogue, which consists of multiple dialogue components.
	/// </summary>
	public class DialogueLine : List<IDialogueComponent>
	{
		/// <summary>
		/// Gets or sets the character speaking the dialogue line.
		/// </summary>
		public string? Character { get; set; } = "Samurai";
		/// <summary>
		/// Gets or sets the expression of the character.
		/// </summary>
		public string? Expression { get; set; }
		/// <summary>
		/// Serializes the dialogue line to a string.
		/// </summary>
		/// <returns>A string representation of the dialogue line.</returns>
		public string Serialize()
		{
			if (string.IsNullOrWhiteSpace(Character))
				return string.Join("", this.Select(i => i.Serialize()));
			else if (string.IsNullOrWhiteSpace(Expression))
				return $"{Character}:{string.Join("", this.Select(i => i.Serialize()))}";
			else
				return $"{Character}_{Expression}:{string.Join("", this.Select(i => i.Serialize()))}";
		}
		/// <inheritdoc/>
		public override string ToString() => $"{Character}({Expression}):{string.Join("",this.Select(i=>i.Plain()))}";
	}
}
