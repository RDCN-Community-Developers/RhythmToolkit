using System.Text;

namespace RhythmBase.Global.Components.RichText
{
	/// <summary>
	/// Represents a line of dialogue, which consists of multiple dialogue components.
	/// </summary>
	public class RDDialogueBlock
	{
		/// <summary>
		/// Gets or sets the character speaking the dialogue line.
		/// </summary>
		public string? Character { get; set; }
		/// <summary>
		/// Gets or sets the expression of the character.
		/// </summary>
		public string? Expression { get; set; }
		/// <summary>
		/// Gets or sets the content of the dialogue line.
		/// </summary>
		/// <value>
		/// The content of the dialogue line, represented as an <see cref="RDLine{TStyle}"/>.
		/// </value>
		public RDLine<RDDialoguePhraseStyle> Content { get; set; } = "";
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
			sb.Append(Content.Serialize());
			return sb.ToString();
		}
		/// <summary>
		/// Deserializes a string into a <see cref="RDDialogueBlock"/> instance.
		/// </summary>
		/// <param name="str">The string to deserialize.</param>
		/// <returns>A new <see cref="RDDialogueBlock"/> containing the deserialized content.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the input string is null.</exception>
		/// <exception cref="FormatException">Thrown when the input string has an invalid format.</exception>
		public static RDDialogueBlock Deserialize(string str)
		{
			str = str.Trim();
			RDDialogueBlock line = new();
			int mi = str.IndexOf(':');
			if (mi != -1)
			{
				string character = str[..mi];
				if (character.Contains('_'))
				{
					string[] parts = character.Split('_', 2);
					character = parts[0];
					line.Expression = parts[1];
				}
				line.Character = character;
			}
			line.Content = RDLine<RDDialoguePhraseStyle>.Deserialize(str[(mi + 1)..]);
			return line;
		}
		/// <summary>
		/// Deserializes a string into a <see cref="RDDialogueBlock"/> instance, using a lookup of valid expressions.
		/// </summary>
		/// <param name="str">The string to deserialize.</param>
		/// <param name="expressions">A lookup of valid expressions for each character.</param>
		/// <returns>A new <see cref="RDDialogueBlock"/> containing the deserialized content.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the input string is null.</exception>
		/// <exception cref="FormatException">Thrown when the input string has an invalid format.</exception>
		public static RDDialogueBlock Deserialize(string str, ILookup<string, string> expressions)
		{
			str = str.Trim();
			RDDialogueBlock line = new();
			int mi = str.IndexOf(':');
			if (mi != -1)
			{
				string character = str[..mi];
				string expression = "";
				if (character.Contains('_'))
				{
					string[] parts = character.Split('_', 2);
					character = parts[0];
					expression = parts[1];
				}
				else
					character = str;
				if (!expressions.Contains(character))
				{
					line.Content = RDLine<RDDialoguePhraseStyle>.Deserialize(str);
					return line;
				}
				line.Character = character;
				if (expressions[character].Contains(expression))
					line.Expression = expression;
			}
			line.Content = RDLine<RDDialoguePhraseStyle>.Deserialize(str[(mi + 1)..]);
			return line;
		}
		/// <inheritdoc/>
		public override string ToString() => $"{Character}({Expression}):{Content}";
	}
}
