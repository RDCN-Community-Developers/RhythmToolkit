using System.Collections;
using System.Text;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// The conditions of the event.
	/// </summary>
	public class Condition
	{
		private readonly HashSet<int> conditions = [];
		/// <summary>
		/// The time of effectiveness of the condition.
		/// </summary>
		public float Duration { get; set; }
		/// <summary>
		/// Gets a value indicating whether the collection contains any conditions.
		/// </summary>
		public bool HasValue => conditions.Count > 0;
		/// <summary>
		/// Gets an array of indices derived from the current set of conditions.
		/// </summary>
		public int[] Indices => [.. conditions.Select(c => ~(1 << 31) & c)];
		/// <summary>
		/// Gets or sets the enabled or disabled state associated with the specified condition index.
		/// </summary>
		/// <remarks>Setting the value to <see langword="null"/> removes any explicit enabled or disabled state for
		/// the specified index.</remarks>
		/// <param name="index">The zero-based index of the condition to retrieve or modify.</param>
		/// <returns>A nullable Boolean value indicating the state of the condition at the specified index: <see langword="true"/> if
		/// enabled; <see langword="false"/> if disabled; or <see langword="null"/> if the state is not set.</returns>
		public bool? this[int index]
		{
			get
			{
				int disabledCondition = (~(1 << 31) & index);
				int enabledCondition = (1 << 31) | index;
				if (conditions.Contains(enabledCondition))
					return true;
				else if (conditions.Contains(disabledCondition))
					return false;
				else
					return null;
			}
			set
			{
				if (value is bool v)
					if (v)
					{
						int disabledCondition = (~(1 << 31) & index);
						conditions.Remove(disabledCondition);
						conditions.Add((1 << 31) | index);
					}
					else
					{
						int enabledCondition = (1 << 31) | index;
						conditions.Remove(enabledCondition);
						conditions.Add(~(1 << 31) & index);
					}
				else
				{
					int disabledCondition = (~(1 << 31) & index);
					int enabledCondition = (1 << 31) | index;
					conditions.Remove(disabledCondition);
					conditions.Remove(enabledCondition);
				}
			}
		}
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
					o[index] = enabled;
					++i;
					continue;
				}
				if (text[i] is 'd')
				{
					o[index] = enabled;
					break;
				}
				throw new RhythmBaseException($"Illegal condition: {text}.");
			}
			float duration = 0;
			i++;
			while (i < text.Length && char.IsDigit(text[i]))
				duration = duration * 10 + (text[i++] - '0');
			if (i < text.Length && text[i] is '.')
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
			foreach (int pair in conditions)
			{
				if (sb.Length > 0)
					sb.Append('&');
				if (pair >> 31 == 0)
					sb.Append('~');
				sb.Append(pair & ~(1 << 31));
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
		/// <summary>
		/// Converts a <see cref="Condition"/> instance to its string representation.
		/// </summary>
		/// <remarks>This operator enables implicit conversion of a <see cref="Condition"/> object to a string,
		/// typically for serialization or display purposes.</remarks>
		/// <param name="c">The <see cref="Condition"/> instance to convert.</param>
		public static implicit operator string(Condition c) => c.Serialize();
		/// <inheritdoc/>
		public override string ToString() => Serialize();
	}
}
