using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace RhythmBase.Components.Dialogue
{

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
		/// Gets or sets the events associated with the rich string.
		/// </summary>
		public RDRichStringEvent[] Events { get; init; } = [];
		/// <summary>
		/// Gets the length of the text content.
		/// </summary>
		/// <value>The number of characters in the text content.</value>
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
				return new RDRichString
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
		/// <returns>A new <see cref="RDRichString"/> with the characters within the specified range.</returns>
		public RDRichString this[Range range]
		{
			get
			{
				RDRichString style = new()
				{
					Text = Text[range],
					Style = Style,
					Events = GetEvents(range.Start.GetOffset(Length), range.End.GetOffset(Length) - range.Start.GetOffset(Length))
				};
				return style;
			}
		}
		private readonly RDRichStringEvent[] GetEvents(int start, int length) => Events
			.Where(e => e.Index >= start && e.Index < start + length)
			.Select(e => new RDRichStringEvent (e.Type, e.Index - start))
			.ToArray();
		/// <summary>
		/// Gets a new <see cref="RDRichString"/> with the same style as the current instance.
		/// </summary>
		public RDRichStringStyle Style { get; init; }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDRichString"/> struct with an empty text.
		/// </summary>
		public RDRichString() : this("") { }
		/// <summary>
		/// Implicitly converts a string to an <see cref="RDRichString"/>.
		/// </summary>
		/// <param name="text">The text to convert.</param>
		/// <returns>A new <see cref="RDRichString"/> instance with the specified text.</returns>
		public static implicit operator RDRichString(string text) => new() { Text = text };
		/// <inheritdoc/>
		public static bool operator ==(RDRichString left, RDRichString right) => left.Text == right.Text && left.Style == right.Style;
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
