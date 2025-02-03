using System.Diagnostics;
using System.Text;

namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Represents a list of rich text strings.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDRichTextLine()
	{
		/// <summary>
		/// Gets or sets the list of rich text strings.
		/// </summary>
		private RDRichString[] texts = [];
		/// <summary>
		/// The length of the string.
		/// </summary>
		public readonly int Length => texts.Sum(i => i.Length);
		/// <summary>
		/// Gets or sets the <see cref="RDRichTextLine"/> at the specified index.
		/// </summary>
		/// <param name="index">The index of the <see cref="RDRichTextLine"/> to get or set.</param>
		/// <returns>The <see cref="RDRichTextLine"/> at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
		public RDRichTextLine this[Index index]
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
				RDRichTextLine line = new()
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
		/// Gets or sets the <see cref="RDRichTextLine"/> within the specified range.
		/// </summary>
		/// <param name="range">The range of the <see cref="RDRichTextLine"/> to get or set.</param>
		/// <returns>The <see cref="RDRichTextLine"/> within the specified range.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the range is out of bounds.</exception>
		public RDRichTextLine this[Range range]
		{
			get
			{
				int start = range.Start.GetOffset(Length);
				int end = range.End.GetOffset(Length);
				if (!(start <= end && end <= Length))
					throw new ArgumentOutOfRangeException(nameof(range));
				int ti = 0, tstart, tend;
				RDRichString[] strings = [];
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
				RDRichTextLine line = new()
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
		private static RDRichTextLine Concat(params RDRichTextLine[] lines)
		{
			RDRichString[] texts = [.. lines[0].texts];
			foreach (RDRichTextLine line in lines[1..])
			{
				if (texts[^1].Style == line.texts[0].Style)
				{
					RDRichString richString = texts[^1];
					richString.Text += line.texts[0].Text;
					texts = [.. texts[..^1], richString, .. line.texts[1..]];
				}
				else
					texts = [.. texts, .. line.texts];
			}
			return new() { texts = texts };
		}
		/// <summary>
		/// Implicitly converts a <see cref="RDRichString"/> to a <see cref="RDRichTextLine"/>.
		/// </summary>
		/// <param name="text">The <see cref="RDRichString"/> to convert.</param>
		/// <returns>A new <see cref="RDRichTextLine"/> containing the specified <see cref="RDRichString"/>.</returns>
		public static implicit operator RDRichTextLine(RDRichString text) => new() { texts = [text] };
		/// <summary>
		/// Implicitly converts a <see cref="RDRichString"/> to a <see cref="RDRichTextLine"/>.
		/// </summary>
		/// <param name="texts">The <see cref="RDRichString"/> to convert.</param>
		/// <returns>A new <see cref="RDRichTextLine"/> containing the specified <see cref="RDRichString"/>.</returns>
		public static implicit operator RDRichTextLine(RDRichString[] texts) => new() { texts = texts };
		/// <summary>
		/// Implicitly converts a <see cref="string"/> to a <see cref="RDRichTextLine"/>.
		/// </summary>
		/// <param name="text">The <see cref="string"/> to convert.</param>
		/// <returns>A new <see cref="RDRichTextLine"/> containing the specified <see cref="string"/>.</returns>
		public static implicit operator RDRichTextLine(string text) => new() { texts = [new RDRichString(text)] };
		/// <summary>
		/// Deserializes a string into an <see cref="RDRichTextLine"/>.
		/// </summary>
		/// <param name="text">The string to deserialize.</param>
		/// <returns>A new <see cref="RDRichTextLine"/> containing the deserialized content.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the input text is null.</exception>
		/// <exception cref="FormatException">Thrown when the input text has an invalid format.</exception>
		public static RDRichTextLine Deserialize(string text)
		{
			RDRichTextLine line = "";
			RDRichStringStyle style = new();
			int start = 0;
			while (start < text.Length)
			{
				RDRichStringStyle tempStyle = style;
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
				if (keyvalue[0][0] == '/' && style.RemoveProperty(keyvalue[0][1..]))
					start = start2 + 1;
				else if (style.SetProperty(keyvalue[0], keyvalue.Length == 2 ? keyvalue[1] : "true"))
					start = start2 + 1;
				else
					start = start2 + 1;
			}
			return line;
		}
		private static RDRichString DeserializeStringPart(string text, RDRichStringStyle style)
		{
			int pstart = 0;
			RDRichStringEvent[] events = [];
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
				if (RDRichStringEvent.Create(btag, pend, out RDRichStringEvent? e))
					events = [.. events, e];
				text = text[..pend] + text[(pstart2 + 1)..];
			}
			return new RDRichString(text) { Style = style, Events = events };
		}
		/// <summary>
		/// Serializes the current <see cref="RDRichTextLine"/> instance to a string.
		/// </summary>
		/// <returns>A string representation of the current <see cref="RDRichTextLine"/> instance.</returns>
		/// <remarks>
		/// The serialization process converts the rich text line into a string format, including any styling information.
		/// </remarks>
		public readonly string Serialize()
		{
			StringBuilder sb = new();
			RDRichStringStyle style = new();
			int ci = 0;
			foreach (RDRichString str in texts)
			{
				sb.Append(RDRichStringStyle.GetXmlTag(style, str.Style));
				string part = str.Text;
				int offset = 0;
				foreach (RDRichStringEvent e in str.Events)
				{
					string serialized = e.Serialize();
					part = part.Insert(e.Index + offset, serialized);
					offset += serialized.Length;
				}
				sb.Append(part);
				ci += str.Length;
				style = str.Style;
			}
			sb.Append(RDRichStringStyle.GetXmlTag(style, new()));
			return sb.ToString();
		}
		/// <summary>
		/// Concatenates two <see cref="RDRichTextLine"/> instances.
		/// </summary>
		/// <param name="left">The left <see cref="RDRichTextLine"/>.</param>
		/// <param name="right">The right <see cref="RDRichTextLine"/>.</param>
		/// <returns>A new <see cref="RDRichTextLine"/> that is the result of concatenating the two specified instances.</returns>
		public static RDRichTextLine operator +(RDRichTextLine left, RDRichTextLine right)
		{
			if (right.Length == 0)
				return left;
			if (left.texts[^1].Style == right.texts[0].Style)
			{
				RDRichString richString = left.texts[^1];
				richString.Text += right.texts[0].Text;
				return new()
				{
					texts = [.. left.texts[..^1], richString, .. right.texts[1..]]
				};
			}
			return new()
			{
				texts = [.. left.texts, .. right.texts]
			};
		}
		/// <inheritdoc/>
		public override readonly string ToString() => string.Join("", texts);
		/// <inheritdoc/>
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
