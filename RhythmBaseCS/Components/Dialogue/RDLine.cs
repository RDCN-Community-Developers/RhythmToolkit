using System.Diagnostics;
using System.Text;

namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a list of rich text strings.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDLine<TStyle>()
		 : IRDRichTextLine<TStyle>
		where TStyle : IRDRichStringStyle<TStyle>, new()
	{
		/// <summary>
		/// Gets or sets the list of rich text strings.
		/// </summary>
		private RDPhrase<TStyle>[] texts = [];
		/// <summary>
		/// The length of the string.
		/// </summary>
		public readonly int Length => texts.Sum(i => i.Length);
		/// <summary>
		/// Gets or sets the <see cref="RDLine{RDDialoguePhraseStyle}"/> at the specified index.
		/// </summary>
		/// <param name="index">The index of the <see cref="RDLine{RDDialoguePhraseStyle}"/> to get or set.</param>
		/// <returns>The <see cref="RDLine{RDDialoguePhraseStyle}"/> at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
		public RDLine<TStyle> this[Index index]
		{
			get
			{
				int i = index.GetOffset(Length);
				if (Length <= i)
					throw new ArgumentOutOfRangeException(nameof(index));
				int ti = 0;
				while (texts[ti].Length < i)
				{
					i -= texts[ti].Length;
					ti++;
				}
				RDLine<TStyle> line = new()
				{
					texts = [texts[ti][i]]
				};
				return line;
			}
			set
			{
				int i = index.GetOffset(Length);
				if (Length <= i)
					throw new ArgumentOutOfRangeException(nameof(index));
				texts = Concat([this[..i], value, this[(i + 1)..]]).texts;
			}
		}
		/// <summary>
		/// Gets or sets the <see cref="RDLine{RDDialoguePhraseStyle}"/> within the specified range.
		/// </summary>
		/// <param name="range">The range of the <see cref="RDLine{RDDialoguePhraseStyle}"/> to get or set.</param>
		/// <returns>The <see cref="RDLine{RDDialoguePhraseStyle}"/> within the specified range.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the range is out of bounds.</exception>
		public RDLine<TStyle> this[Range range]
		{
			get
			{
				int start = range.Start.GetOffset(Length);
				int end = range.End.GetOffset(Length);
				if (!(start <= end && end <= Length))
					throw new ArgumentOutOfRangeException(nameof(range));
				int ti = 0, tstart, tend;
				RDPhrase<TStyle>[] strings = [];
				while (texts[ti].Length <= start)
				{
					start -= texts[ti].Length;
					end -= texts[ti].Length;
					ti++;
				}
				tstart = ti;
				while (texts[ti].Length < end)
				{
					end -= texts[ti].Length;
					ti++;
				}
				tend = ti;
				if (tstart == tend)
					strings = [texts[tstart][start..end]];
				else
				{
					for (int i = tstart + 1; i < tend; i++)
						strings = [.. strings, texts[i]];
					strings = [texts[tstart][start..], .. strings, texts[tend][..end]];
				}
				RDLine<TStyle> line = new()
				{
					texts = strings
				};
				return line;
			}
			set
			{
				int start = range.Start.GetOffset(Length);
				int end = range.End.GetOffset(Length);
				if (!(start < end && end <= Length))
					throw new ArgumentOutOfRangeException(nameof(range));
				texts = Concat([this[..start], value, this[end..]]).texts;
			}
		}
		/// <inheritdoc/>
		public static RDLine<TStyle> Concat(params RDLine<TStyle>[] lines)
		{
			RDPhrase<TStyle>[] texts = [.. lines[0].texts];
			foreach (RDLine<TStyle> line in lines[1..])
			{
				if (texts[^1].Style == line.texts[0].Style)
				{
					RDPhrase<TStyle> before = texts[^1], after = line.texts[0];
					RDPhrase<TStyle> richString = new(before.Text + after.Text)
					{
						Style = before.Style,
						Events = [.. before.Events, .. after.Events.Select(i => new RDDialogueTone(i.Type, i.Index + before.Length) { Pause = i.Pause })]
					};
					texts = [.. texts[..^1], richString, .. line.texts[1..]];
				}
				else
					texts = [.. texts, .. line.texts];
			}
			return new() { texts = texts };
		}
		/// <summary>
		/// Implicitly converts a <see cref="RDPhrase{RDDialoguePhraseStyle}"/> to a <see cref="RDLine{RDDialoguePhraseStyle}"/>.
		/// </summary>
		/// <param name="text">The <see cref="RDPhrase{RDDialoguePhraseStyle}"/> to convert.</param>
		/// <returns>A new <see cref="RDLine{RDDialoguePhraseStyle}"/> containing the specified <see cref="RDPhrase{RDDialoguePhraseStyle}"/>.</returns>
		public static implicit operator RDLine<TStyle>(RDPhrase<TStyle> text) => new() { texts = [text] };
		/// <summary>
		/// Implicitly converts a <see cref="RDPhrase{RDDialoguePhraseStyle}"/> to a <see cref="RDLine{RDDialoguePhraseStyle}"/>.
		/// </summary>
		/// <param name="texts">The <see cref="RDPhrase{RDDialoguePhraseStyle}"/> to convert.</param>
		/// <returns>A new <see cref="RDLine{RDDialoguePhraseStyle}"/> containing the specified <see cref="RDPhrase{RDDialoguePhraseStyle}"/>.</returns>
		public static implicit operator RDLine<TStyle>(RDPhrase<TStyle>[] texts) => new() { texts = texts };
		/// <summary>
		/// Implicitly converts a <see cref="string"/> to a <see cref="RDLine{RDDialoguePhraseStyle}"/>.
		/// </summary>
		/// <param name="text">The <see cref="string"/> to convert.</param>
		/// <returns>A new <see cref="RDLine{RDDialoguePhraseStyle}"/> containing the specified <see cref="string"/>.</returns>
		public static implicit operator RDLine<TStyle>(string text) => new() { texts = [new RDPhrase<TStyle>(text)] };
		/// <summary>
		/// Deserializes a string into an <see cref="RDLine{RDDialoguePhraseStyle}"/>.
		/// </summary>
		/// <param name="text">The string to deserialize.</param>
		/// <returns>A new <see cref="RDLine{RDDialoguePhraseStyle}"/> containing the deserialized content.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the input text is null.</exception>
		/// <exception cref="FormatException">Thrown when the input text has an invalid format.</exception>
		static public RDLine<TStyle> Deserialize(string text)
		{
			RDLine<TStyle> line = "";
			TStyle style = new();
			int start = 0;
			while (start < text.Length)
			{
				TStyle tempStyle = style;
				int end = text.IndexOf('<', start);
				if (end == -1)
				{
					line += DeserializeStringPart(text[start..], tempStyle);
					break;
				}
				int start2 = text.IndexOf('>', end);
				int end2 = text.IndexOf('<', end + 1);
				if (start2 == -1)
					break;
				if (end2 != -1 && end2 < start2)
				{
					line += DeserializeStringPart(text[start..end2], tempStyle);
					start = end2;
					continue;
				}
				string textpart = text[start..end];
				line += DeserializeStringPart(textpart, tempStyle);
				string[] keyvalue = text[(end + 1)..start2].Split('=', 2);
				if (keyvalue[0].StartsWith('/') && style.ResetProperty(keyvalue[0][1..]))
					start = start2 + 1;
				else if (style.SetProperty(keyvalue[0], keyvalue.Length == 2 ? keyvalue[1] : "true"))
					start = start2 + 1;
				else
					start = start2 + 1;
			}
			return line;
		}
		private static RDPhrase<TStyle> DeserializeStringPart(string text, TStyle style)
		{
			if (!TStyle.HasPhrase)
				return new RDPhrase<TStyle>(text) { Style = style };
			int pstart = 0;
			RDDialogueTone[] events = [];
			while (pstart < text.Length)
			{
				int pend = text.IndexOf('[', pstart);
				if (pend == -1)
				{
					break;
				}
				int pstart2 = text.IndexOf(']', pend);
				int pend2 = text.IndexOf('[', pend + 1);
				if (pstart2 == -1)
					break;
				if (pend2 != -1 && pend2 < pstart2)
				{
					pstart = pend2 + 1;
					continue;
				}
				string btag = text[(pend + 1)..pstart2];
				if (RDDialogueTone.Create(btag, pend, out RDDialogueTone? e))
					events = [.. events, e];
				text = text[..pend] + text[(pstart2 + 1)..];
			}
			return new RDPhrase<TStyle>(text) { Style = style, Events = events };
		}
		/// <summary>
		/// Serializes the current <see cref="RDLine{RDDialoguePhraseStyle}"/> instance to a string.
		/// </summary>
		/// <returns>A string representation of the current <see cref="RDLine{RDDialoguePhraseStyle}"/> instance.</returns>
		/// <remarks>
		/// The serialization process converts the rich text line into a string format, including any styling information.
		/// </remarks>
		public readonly string Serialize()
		{
			StringBuilder sb = new();
			TStyle style = new();
			int ci = 0;
			foreach (RDPhrase<TStyle> str in texts)
			{
				sb.Append(TStyle.GetXmlTag(style, str.Style));
				string part = str.Text;
				int offset = 0;
				foreach (RDDialogueTone e in str.Events)
				{
					string serialized = e.Serialize();
					part = part.Insert(e.Index + offset, serialized);
					offset += serialized.Length;
				}
				sb.Append(part);
				ci += str.Length;
				style = str.Style;
			}
			sb.Append(TStyle.GetXmlTag(style, new()));
			return sb.ToString();
		}
		/// <summary>
		/// Concatenates two <see cref="RDLine{RDDialoguePhraseStyle}"/> instances.
		/// </summary>
		/// <param name="left">The left <see cref="RDLine{RDDialoguePhraseStyle}"/>.</param>
		/// <param name="right">The right <see cref="RDLine{RDDialoguePhraseStyle}"/>.</param>
		/// <returns>A new <see cref="RDLine{RDDialoguePhraseStyle}"/> that is the result of concatenating the two specified instances.</returns>
		public static RDLine<TStyle> operator +(RDLine<TStyle> left, RDLine<TStyle> right) => Concat([.. left.texts, .. right.texts]);
		/// <inheritdoc/>
		public override readonly string ToString() => string.Join("", texts);
		/// <inheritdoc/>
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
