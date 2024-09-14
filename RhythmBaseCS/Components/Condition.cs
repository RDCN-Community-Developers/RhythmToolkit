using Microsoft.VisualBasic.CompilerServices;
using RhythmBase.Exceptions;
using System.Runtime.CompilerServices;
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
		public Condition()
		{
			ConditionLists = [];
		}
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
		/// Converting conditions to strings
		/// </summary>
		/// <returns>A string in the format supported by RDLevel.</returns>
		public string Serialize() => $"{string.Join("&", ConditionLists.Select((i) => (i.Enabled ? "" : "~") + i.Conditional.Id.ToString()))}d{Duration}";
		public override string ToString() => Serialize();
	}
}
