﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace RhythmBase.Global.Components.RichText
{
	/// <summary>
	/// Represents a rich text string with various styling options.
	/// </summary>
	/// <param name="text">The text content of the rich string.</param>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDPhrase<TStyle>(string text) :
#if NET7_0_OR_GREATER
		IEqualityOperators<RDPhrase<TStyle>, RDPhrase<TStyle>, bool>,
#endif
			IEquatable<RDPhrase<TStyle>>
		where TStyle : IRDRichStringStyle<TStyle>, new()
	{
		/// <summary>
		/// Gets the text content of the rich string.
		/// </summary>
		public string Text { get; internal set; } = text;
		/// <summary>
		/// Gets or sets the events associated with the rich string.
		/// </summary>
		public RDDialogueTone[] Events { get; set; } = [];
		/// <summary>
		/// Gets the length of the text content.
		/// </summary>
		/// <value>The number of characters in the text content.</value>
		public readonly int Length => Text.Length;
#if NETCOREAPP3_0_OR_GREATER
		/// <summary>
		/// Gets the rich string at the specified index.
		/// </summary>
		/// <param name="index">The index of the character.</param>
		/// <returns>A new <see cref="RDPhrase{TStyle}"/> with the character at the specified index.</returns>
		public RDPhrase<TStyle> this[Index index]
		{
			get
			{
				return new RDPhrase<TStyle>
				{
					Text = Text[index].ToString(),
					Style = Style,
					Events = GetEvents(index.GetOffset(Length), 1)
				};
			}
		}
		/// <summary>
		/// Gets the rich string within the specified range.
		/// </summary>
		/// <param name="range">The range of characters.</param>
		/// <returns>A new <see cref="RDPhrase{TStyle}"/> with the characters within the specified range.</returns>
		public RDPhrase<TStyle> this[Range range]
		{
			get
			{
				RDPhrase<TStyle> style = new()
				{
					Text = Text[range],
					Style = Style,
					Events = GetEvents(range.Start.GetOffset(Length), range.End.GetOffset(Length) - range.Start.GetOffset(Length))
				};
				return style;
			}
		}
#endif
		private readonly RDDialogueTone[] GetEvents(int start, int length) => Events
			.Where(e => e.Index >= start && e.Index < start + length)
			.Select(e => new RDDialogueTone(e.Type, e.Index - start))
			.ToArray();
		/// <summary>
		/// Gets a new <see cref="RDPhrase{TStyle}"/> with the same style as the current instance.
		/// </summary>
		public TStyle Style
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			set;
#endif
		} = new();
		/// <summary>
		/// Initializes a new instance of the <see cref="RDPhrase{TStyle}"/> struct with an empty text.
		/// </summary>
		public RDPhrase() : this("") { }
		/// <summary>
		/// Implicitly converts a string to an <see cref="RDPhrase{TStyle}"/>.
		/// </summary>
		/// <param name="text">The text to convert.</param>
		/// <returns>A new <see cref="RDPhrase{TStyle}"/> instance with the specified text.</returns>
		public static implicit operator RDPhrase<TStyle>(string text) => new() { Text = text };
		/// <inheritdoc/>
		public static bool operator ==(RDPhrase<TStyle> left, RDPhrase<TStyle> right) => left.Text == right.Text && left.Style.Equals(right.Style);
		/// <inheritdoc/>
		public static bool operator !=(RDPhrase<TStyle> left, RDPhrase<TStyle> right) => !(left == right);
		/// <inheritdoc/>
		public readonly bool Equals(RDPhrase<TStyle> other) => this == other;
		/// <inheritdoc/>
#if NET6_0_OR_GREATER
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDPhrase<TStyle> && base.Equals(obj);
#else
		public override readonly bool Equals(object? obj) => obj is RDPhrase<TStyle> && base.Equals(obj);
#endif
		/// <inheritdoc/>
		public override readonly int GetHashCode() => Text.GetHashCode();
		/// <inheritdoc/>
		public override readonly string ToString() => Text;
		/// <inheritdoc/>
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
