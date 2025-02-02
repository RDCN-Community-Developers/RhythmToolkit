using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

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
		public readonly int Length => texts.Sum(i => i.Length);
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
				texts = Connect([this[..i], value, this[(i + 1)..]]).texts;
			}
		}
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
				texts = Connect([this[..start], value, this[end..]]).texts;
			}
		}
		private static RDRichTextLine Connect(params RDRichTextLine[] lines)
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
		/// Concatenates two <see cref="RDRichTextLine"/> instances.
		/// </summary>
		/// <param name="left">The left <see cref="RDRichTextLine"/>.</param>
		/// <param name="right">The right <see cref="RDRichTextLine"/>.</param>
		/// <returns>A new <see cref="RDRichTextLine"/> that is the result of concatenating the two specified instances.</returns>
		public static RDRichTextLine operator +(RDRichTextLine left, RDRichTextLine right)
		{
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

	/// <summary>
	/// Represents a rich text string with various styling options.
	/// </summary>
	/// <param name="text">The text content of the rich string.</param>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDRichString(string text)
		: IEqualityOperators<RDRichString, RDRichString, bool>,
			IEquatable<RDRichString>
	{
		/// <summary>
		/// Gets the text content of the rich string.
		/// </summary>
		public string Text { get; internal set; } = text;

		/// <summary>
		/// Gets or sets the color of the text.
		/// </summary>
		public RDColor? Color { get; init; }

		/// <summary>
		/// Gets or sets the speed of the text animation.
		/// </summary>
		public float? Speed { get; init; }

		/// <summary>
		/// Gets or sets the volume of the text.
		/// </summary>
		public float? Volume { get; init; }

		/// <summary>
		/// Gets or sets the pitch of the text.
		/// </summary>
		public float? Pitch { get; init; }

		/// <summary>
		/// Gets or sets the pitch range of the text.
		/// </summary>
		public float? PitchRange { get; init; }

		/// <summary>
		/// Gets or sets a value indicating whether the text should shake.
		/// </summary>
		public bool? Shake { get; init; }

		/// <summary>
		/// Gets or sets the radius of the shake effect.
		/// </summary>
		public float? ShakeRadius { get; init; }

		/// <summary>
		/// Gets or sets a value indicating whether the text should have a wave effect.
		/// </summary>
		public bool? Wave { get; init; }

		/// <summary>
		/// Gets or sets the height of the wave effect.
		/// </summary>
		public float? WaveHeight { get; init; }

		/// <summary>
		/// Gets or sets the speed of the wave effect.
		/// </summary>
		public float? WaveSpeed { get; init; }

		/// <summary>
		/// Gets or sets a value indicating whether the text should have a swirl effect.
		/// </summary>
		public bool? Swirl { get; init; }

		/// <summary>
		/// Gets or sets the radius of the swirl effect.
		/// </summary>
		public float? SwirlRadius { get; init; }

		/// <summary>
		/// Gets or sets the speed of the swirl effect.
		/// </summary>
		public float? SwirlSpeed { get; init; }

		/// <summary>
		/// Gets or sets a value indicating whether the text should be sticky.
		/// </summary>
		public bool? Sticky { get; init; }

		/// <summary>
		/// Gets or sets a value indicating whether the text should be loud.
		/// </summary>
		public bool? Loud { get; init; }

		/// <summary>
		/// Gets or sets a value indicating whether the text should be bold.
		/// </summary>
		public bool? Bold { get; init; }

		/// <summary>
		/// Gets or sets a value indicating whether the text should be whispered.
		/// </summary>
		public bool? Whisper { get; init; }
		public readonly int Length => Text.Length;
		/// <summary>
		/// Gets the rich string at the specified index.
		/// </summary>
		/// <param name="index">The index of the character.</param>
		/// <returns>A new <see cref="RDRichString"/> with the character at the specified index.</returns>
		public RDRichString this[Index index]
		{
			get
			{
				RDRichString style = Style;
				style.Text = Text[index].ToString();
				return style;
			}
		}

		/// <summary>
		/// Gets the rich string within the specified range.
		/// </summary>
		/// <param name="range">The range of characters.</param>
		/// <returns>A new <see cref="RDRichString"/> with the characters within the specified range.</returns>
		public RDRichString this[Range range]
		{
			get
			{
				RDRichString style = Style;
				style.Text = Text[range];
				return style;
			}
		}

		/// <summary>
		/// Gets a new <see cref="RDRichString"/> with the same style as the current instance.
		/// </summary>
		public RDRichString Style => new()
		{
			Bold = Bold,
			Color = Color
		};

		/// <summary>
		/// Initializes a new instance of the <see cref="RDRichString"/> struct with an empty text.
		/// </summary>
		public RDRichString() : this("") { }
		public static implicit operator RDRichString(string text) => new() { Text = text };
		/// <inheritdoc/>
		public static bool operator ==(RDRichString left, RDRichString right) =>
			left.Text == right.Text
				&& left.Color == right.Color
				&& left.Speed == right.Speed
				&& left.Volume == right.Volume
				&& left.Pitch == right.Pitch
				&& left.PitchRange == right.PitchRange
				&& left.Shake == right.Shake
				&& left.ShakeRadius == right.ShakeRadius
				&& left.Wave == right.Wave
				&& left.WaveHeight == right.WaveHeight
				&& left.WaveSpeed == right.WaveSpeed
				&& left.Swirl == right.Swirl
				&& left.SwirlRadius == right.SwirlRadius
				&& left.SwirlSpeed == right.SwirlSpeed
				&& left.Sticky == right.Sticky
				&& left.Loud == right.Loud
				&& left.Bold == right.Bold
				&& left.Whisper == right.Whisper;
		/// <inheritdoc/>
		public static bool operator !=(RDRichString left, RDRichString right) => !(left == right);
		/// <inheritdoc/>
		public readonly bool Equals(RDRichString other) => this == other;
		/// <inheritdoc/>
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDRichString && base.Equals(obj);
		/// <inheritdoc/>
		public override readonly int GetHashCode() => Text.GetHashCode();
		/// <inheritdoc/>
		public override readonly string ToString() => Text;
		/// <inheritdoc/>
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
