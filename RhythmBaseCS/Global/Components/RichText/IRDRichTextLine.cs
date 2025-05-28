namespace RhythmBase.Global.Components.RichText
{
	/// <summary>
	/// Represents a line of rich text with a specific style.
	/// </summary>
	/// <typeparam name="TStyle">The type of the style applied to the rich text.</typeparam>
	public interface IRDRichTextLine<TStyle> where TStyle : IRDRichStringStyle<TStyle>, new()
	{
#if NETCOREAPP3_0_OR_GREATER
		/// <summary>
		/// Gets or sets the <see cref="RDLine{TStyle}"/> at the specified index.
		/// </summary>
		/// <param name="index">The index of the rich text line.</param>
		/// <returns>The rich text line at the specified index.</returns>
		RDLine<TStyle> this[Index index] { get; set; }
		/// <summary>
		/// Gets or sets the <see cref="RDLine{TStyle}"/> within the specified range.
		/// </summary>
		/// <param name="range">The range of the rich text lines.</param>
		/// <returns>The rich text lines within the specified range.</returns>
		RDLine<TStyle> this[Range range] { get; set; }
#endif
		/// <summary>
		/// Gets the length of the rich text line.
		/// </summary>
		int Length { get; }
		/// <summary>
		/// Concatenates multiple <see cref="RDLine{TStyle}"/> instances into a single instance.
		/// </summary>
		/// <param name="lines">The rich text lines to concatenate.</param>
		/// <returns>A new <see cref="RDLine{TStyle}"/> containing the concatenated content.</returns>
#if NET7_0_OR_GREATER
		static
#endif
		abstract RDLine<TStyle> Concat(params RDLine<TStyle>[] lines);
		/// <summary>
		/// Deserializes a string into an <see cref="RDLine{TStyle}"/>.
		/// </summary>
		/// <param name="text">The string to deserialize.</param>
		/// <returns>A new <see cref="RDLine{TStyle}"/> containing the deserialized content.</returns>
#if NET7_0_OR_GREATER
		static
#endif
		abstract RDLine<TStyle> Deserialize(string text);
		/// <summary>
		/// Serializes the current <see cref="RDLine{TStyle}"/> instance to a string.
		/// </summary>
		/// <returns>A string representation of the current <see cref="RDLine{TStyle}"/> instance.</returns>
		string Serialize();
	}
}
