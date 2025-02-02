using System.Text;

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
			var sb = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(Character))
			{
				sb.Append(Character);
				if (!string.IsNullOrWhiteSpace(Expression))
				{
					sb.Append('_').Append(Expression);
				}
				sb.Append(':');
			}
			foreach (var component in this)
				sb.Append(component.Serialize());
			return sb.ToString();
		}
		//public static DialogueLine Deserialize(string str, ILookup<string,string> expressions)
		//{
		//	string character  = str[..str.IndexOf(':')];
		//	if(character.Contains('_'))
		//	{
		//		string[] parts = character.Split('_');
		//		character = parts[0];
		//		expressions = expressions.FirstOrDefault(i => i.Key == parts[1]);
		//	}
		//}
		/// <inheritdoc/>
		public override string ToString() => $"{Character}({Expression}):{string.Join("",this.Select(i=>i.Plain()))}";
	}
}
