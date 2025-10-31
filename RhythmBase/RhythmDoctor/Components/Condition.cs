using System.Text;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// The conditions of the event.
	/// </summary>
	public class Condition
	{
		/// <summary>
		/// Condition list.
		/// </summary>
		public Dictionary<int, bool> ConditionLists { get; } = [];
		/// <summary>
		/// The time of effectiveness of the condition.
		/// </summary>
		public float Duration { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="Condition"/> class.
		/// </summary>
		public Condition()
		{
		}
		/// <summary>
		/// Loads a condition from a string.
		/// </summary>
		/// <param name="text">The text to load the condition from.</param>
		/// <returns>A new instance of the <see cref="Condition"/> class.</returns>
		/// <exception cref="RhythmBaseException">Thrown when the condition is illegal.</exception>
		public static Condition Deserialize(string text)
		{
			// "1&~2&3d4.5"
			int i = 0;
			Condition o = new Condition();
			while (i < text.Length && text[i] != 'd')
			{
				bool enabled = true;
				int index = 0;
				char c = text[i];
				if (text[i] is '~')
				{
					enabled = false;
					++i;
				}
				while (i < text.Length && char.IsDigit(text[i]))
					index = index * 10 + (text[i++] - '0');
				if (text[i] is '&')
				{
					o.ConditionLists[index] = enabled;
					++i;
					continue;
				}
				if (text[i] is 'd')
				{
					o.ConditionLists[index] = enabled;
					break;
				}
				throw new RhythmBaseException($"Illegal condition: {text}.");
			}
			float duration = 0;
			i++;
			while (i < text.Length && char.IsDigit(text[i]))
				duration = duration * 10 + (text[i++] - '0');
			if(i < text.Length && text[i] is '.')
			{
				++i;
				float frac = 0.1f;
				while (i < text.Length && char.IsDigit(text[i]))
				{
					duration += frac * (text[i++] - '0');
					frac *= 0.1f;
				}
			}
			o.Duration = duration;
			return o;
		}
		/// <summary>
		/// Converts conditions to a string.
		/// </summary>
		/// <returns>A string in the format supported by RDLevel.</returns>
		public string Serialize()
		{
			StringBuilder sb = new();
			foreach(var pair in ConditionLists)
			{
				if (sb.Length > 0)
					sb.Append('&');
				if (!pair.Value)
					sb.Append('~');
				sb.Append(pair.Key);
			}
			sb.Append('d').Append(Duration.ToString("0.########"));
			return sb.ToString();
		}
		/// <summary>
		/// Creates a deep copy of the current <see cref="Condition"/> instance.
		/// </summary>
		/// <returns>A new <see cref="Condition"/> object that is a deep copy of the current instance.</returns>
		public Condition Clone()
		{
			return Deserialize(Serialize());
		}
		/// <inheritdoc/>
		public override string ToString() => Serialize();
	}
}
