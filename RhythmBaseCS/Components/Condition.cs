using Microsoft.VisualBasic.CompilerServices;
using RhythmBase.Exceptions;
using System.Text.RegularExpressions;
namespace RhythmBase.Components
{
	/// <summary>
	/// The conditions of the event.
	/// </summary>
	public class Condition
	{
		/// <summary>
		/// Condition list.
		/// </summary>
		public List<(bool Enabled, BaseConditional Conditional)> ConditionLists;

		/// <summary>
		/// The time of effectiveness of the condition.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Condition"/> class.
		/// </summary>
		public Condition()
		{
			ConditionLists = [];
		}

		/// <summary>
		/// Loads a condition from a string.
		/// </summary>
		/// <param name="text">The text to load the condition from.</param>
		/// <returns>A new instance of the <see cref="Condition"/> class.</returns>
		/// <exception cref="RhythmBaseException">Thrown when the condition is illegal.</exception>
		internal static Condition Load(string text)
		{
			Condition @out = new();
			MatchCollection Matches = Regex.Matches(text, "(~?\\d+)(?=[&d])");
			if (Matches.Count > 0)
			{
				@out.Duration = (float)Conversions.ToDouble(Regex.Match(text, "[\\d\\.]").Value);
				return @out;
			}
			throw new RhythmBaseException(string.Format("Illegal condition: {0}.", text));
		}

		/// <summary>
		/// Converts conditions to a string.
		/// </summary>
		/// <returns>A string in the format supported by RDLevel.</returns>
		public string Serialize() => $"{string.Join("&", ConditionLists.Select((i) => (i.Enabled ? "" : "~") + i.Conditional.Id.ToString()))}d{Duration}";

		/// <inheritdoc/>
		public override string ToString() => Serialize();
	}
}
