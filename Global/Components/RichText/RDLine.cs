﻿using System.Diagnostics;
using System.Text;

namespace RhythmBase.Global.Components.RichText
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
#if NETCOREAPP3_0_OR_GREATER
		/// <summary>
		/// Gets or sets the <see cref="RDLine{RDPhraseStyle}"/> at the specified index.
		/// </summary>
		/// <param name="index">The index of the <see cref="RDLine{RDPhraseStyle}"/> to get or set.</param>
		/// <returns>The <see cref="RDLine{RDPhraseStyle}"/> at the specified index.</returns>
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
		/// Gets or sets the <see cref="RDLine{RDPhraseStyle}"/> within the specified range.
		/// </summary>
		/// <param name="range">The range of the <see cref="RDLine{RDPhraseStyle}"/> to get or set.</param>
		/// <returns>The <see cref="RDLine{RDPhraseStyle}"/> within the specified range.</returns>
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
#endif
		/// <inheritdoc/>
		public
#if NET7_0_OR_GREATER
			static
#endif
			RDLine<TStyle> Concat(params RDLine<TStyle>[] lines)
		{
			RDPhrase<TStyle>[] texts = [.. lines[0].texts];
#if NETSTANDARD2_0
			foreach (RDLine<TStyle> line in lines.Skip(1))
			{
				if (texts.Last().Style.Equals(line.texts[0].Style))
				{
					RDPhrase<TStyle> before = texts.Last(), after = line.texts[0];
					RDPhrase<TStyle> richString = new(before.Text + after.Text)
					{
						Style = before.Style,
						Events = [.. before.Events, .. after.Events.Select(i => new RDDialogueTone(i.Type, i.Index + before.Length) { Pause = i.Pause })]
					};
					texts = [.. texts.Take(texts.Length - 1), richString, .. line.texts.Skip(1)];
				}
				else
					texts = [.. texts, .. line.texts];
			}
#else
			foreach (RDLine<TStyle> line in lines[1..])
			{
				if (texts[^1].Style.Equals(line.texts[0].Style))
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
#endif
			return new() { texts = texts };
		}
		/// <summary>
		/// Implicitly converts a <see cref="RDPhrase{RDPhraseStyle}"/> to a <see cref="RDLine{RDPhraseStyle}"/>.
		/// </summary>
		/// <param name="text">The <see cref="RDPhrase{RDPhraseStyle}"/> to convert.</param>
		/// <returns>A new <see cref="RDLine{RDPhraseStyle}"/> containing the specified <see cref="RDPhrase{RDPhraseStyle}"/>.</returns>
		public static implicit operator RDLine<TStyle>(RDPhrase<TStyle> text) => new() { texts = [text] };
		/// <summary>
		/// Implicitly converts a <see cref="RDPhrase{RDPhraseStyle}"/> to a <see cref="RDLine{RDPhraseStyle}"/>.
		/// </summary>
		/// <param name="texts">The <see cref="RDPhrase{RDPhraseStyle}"/> to convert.</param>
		/// <returns>A new <see cref="RDLine{RDPhraseStyle}"/> containing the specified <see cref="RDPhrase{RDPhraseStyle}"/>.</returns>
		public static implicit operator RDLine<TStyle>(RDPhrase<TStyle>[] texts) => new() { texts = texts };
		/// <summary>
		/// Implicitly converts a <see cref="string"/> to a <see cref="RDLine{RDPhraseStyle}"/>.
		/// </summary>
		/// <param name="text">The <see cref="string"/> to convert.</param>
		/// <returns>A new <see cref="RDLine{RDPhraseStyle}"/> containing the specified <see cref="string"/>.</returns>
		public static implicit operator RDLine<TStyle>(string text) => new() { texts = [new RDPhrase<TStyle>(text)] };
		/// <summary>
		/// Deserializes a string into an <see cref="RDLine{RDPhraseStyle}"/>.
		/// </summary>
		/// <param name="text">The string to deserialize.</param>
		/// <returns>A new <see cref="RDLine{RDPhraseStyle}"/> containing the deserialized content.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the input text is null.</exception>
		/// <exception cref="FormatException">Thrown when the input text has an invalid format.</exception>

#if NET7_0_OR_GREATER
		static
#endif
		public RDLine<TStyle> Deserialize(string text)
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
#if NETCOREAPP3_0_OR_GREATER
					line += DeserializeStringPart(text[start..], tempStyle);
#else
					line += DeserializeStringPart(text.Substring(start), tempStyle);
#endif
					break;
				}
				int start2 = text.IndexOf('>', end);
				int end2 = text.IndexOf('<', end + 1);
				if (start2 == -1)
					break;
				if (end2 == -1)
					end2 = text.Length;
#if NETCOREAPP3_0_OR_GREATER
				line += DeserializeStringPart(text[start..end2], tempStyle);
				string textpart = text[start..end];
				string[] keyvalue = text[(end + 1)..start2].Split('=', 2);
				if (keyvalue[0].StartsWith('/') && style.ResetProperty(keyvalue[0][1..]))
#else
				line += DeserializeStringPart(text.Substring(start, end2 - start), tempStyle);
				string textpart = text.Substring(start, end - start);
				string[] keyvalue = text.Substring(end + 1, start2 - (end + 1)).Split(['='], 2);
				if (keyvalue[0].StartsWith("/") && style.ResetProperty(keyvalue[0].Substring(1)))
#endif
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
			if (!
#if NET7_0_OR_GREATER
				TStyle
#else
				style
#endif
				.HasPhrase)
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
#if NETSTANDARD2_0
				string btag = text.Substring(pend + 1, pstart2 - (pend + 1));
#else
				string btag = text[(pend + 1)..pstart2];
#endif
				if (RDDialogueTone.Create(btag, pend, out RDDialogueTone? e) && e is RDDialogueTone ei)
					events = events.Concat(new[] { ei }).ToArray();
#if NETSTANDARD2_0
				text = text.Substring(0, pend) + text.Substring(pstart2 + 1);
#else
				text = text[..pend] + text[(pstart2 + 1)..];
#endif
			}
			return new RDPhrase<TStyle>(text) { Style = style, Events = events };
		}
		/// <summary>
		/// Serializes the current <see cref="RDLine{RDPhraseStyle}"/> instance to a string.
		/// </summary>
		/// <returns>A string representation of the current <see cref="RDLine{RDPhraseStyle}"/> instance.</returns>
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
				sb.Append(
#if NET7_0_OR_GREATER
					TStyle
#else
					str.Style
#endif
					.GetXmlTag(style, str.Style));
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
			sb.Append(
#if NET7_0_OR_GREATER
				TStyle
#else
								style
#endif
.GetXmlTag(style, new()));
			return sb.ToString();
		}
		/// <summary>
		/// Concatenates two <see cref="RDLine{RDPhraseStyle}"/> instances.
		/// </summary>
		/// <param name="left">The left <see cref="RDLine{RDPhraseStyle}"/>.</param>
		/// <param name="right">The right <see cref="RDLine{RDPhraseStyle}"/>.</param>
		/// <returns>A new <see cref="RDLine{RDPhraseStyle}"/> that is the result of concatenating the two specified instances.</returns>
		public static RDLine<TStyle> operator +(RDLine<TStyle> left, RDLine<TStyle> right) =>
#if NETSTANDARD2_0
			left.
#endif
			Concat([.. left.texts, .. right.texts]);
		/// <inheritdoc/>
		public override readonly string ToString() => string.Join("", texts);
		/// <inheritdoc/>
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
