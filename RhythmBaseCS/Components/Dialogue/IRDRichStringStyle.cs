using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace RhythmBase.Components.Dialogue
{
	/// <summary>
	/// Defines the interface for rich string styles.
	/// </summary>
	/// <typeparam name="TSelf">The type that implements this interface.</typeparam>
	public interface IRDRichStringStyle<TSelf> : IEqualityOperators<TSelf, TSelf, bool>, IEquatable<TSelf>
	where TSelf : IRDRichStringStyle<TSelf>
	{
		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		bool Equals([NotNullWhen(true)] object? obj) => obj is TSelf e && Equals(e);

		/// <summary>
		/// Gets the closing tag for the specified name.
		/// </summary>
		/// <param name="name">The name of the tag.</param>
		/// <returns>The closing tag for the specified name.</returns>
		static string GetCloseTag(string name) => $"</{name}>";

		/// <summary>
		/// Gets the opening tag for the specified name and optional argument.
		/// </summary>
		/// <param name="name">The name of the tag.</param>
		/// <param name="arg">The optional argument for the tag.</param>
		/// <returns>The opening tag for the specified name and optional argument.</returns>
		static string GetOpenTag(string name, string? arg = null)=> arg is null ? $"<{name}>" : $"<{name}={arg}>";

		/// <summary>
		/// Generates an XML tag representing the differences between two <see cref="RDDialoguePhraseStyle"/> instances.
		/// </summary>
		/// <param name="before">The initial <see cref="RDDialoguePhraseStyle"/> instance.</param>
		/// <param name="after">The modified <see cref="RDDialoguePhraseStyle"/> instance.</param>
		/// <returns>A string containing the XML tag that represents the differences between the two instances.</returns>
		static abstract string GetXmlTag(TSelf before, TSelf after);

		/// <summary>
		/// Resets the property of the rich string style based on the provided name.
		/// </summary>
		/// <param name="name">The name of the property to reset.</param>
		/// <returns>True if the property was successfully reset; otherwise, false.</returns>
		bool ResetProperty(string name);

		/// <summary>
		/// Sets the property of the rich string style based on the provided name and value.
		/// </summary>
		/// <param name="name">The name of the property to set.</param>
		/// <param name="value">The value to set for the property.</param>
		/// <returns>True if the property was successfully set; otherwise, false.</returns>
		bool SetProperty(string name, string value);

		/// <summary>
		/// Tries to add a tag to the specified string based on the provided name and boolean values.
		/// </summary>
		/// <param name="tag">The string to which the tag will be added.</param>
		/// <param name="name">The name of the tag.</param>
		/// <param name="before">A boolean value indicating whether the tag is before.</param>
		/// <param name="after">A boolean value indicating whether the tag is after.</param>
		static void TryAddTag(ref string tag, string name, bool before, bool after)
		{
			if (before != after)
				tag += after
				? GetOpenTag(name)
				: GetCloseTag(name);
		}

		/// <summary>
		/// Tries to add a tag to the specified string based on the provided name and optional string values.
		/// </summary>
		/// <param name="tag">The string to which the tag will be added.</param>
		/// <param name="name">The name of the tag.</param>
		/// <param name="before">An optional string value indicating the tag before.</param>
		/// <param name="after">An optional string value indicating the tag after.</param>
		static void TryAddTag(ref string tag, string name, string? before, string? after)
		{
			if (before != after)
				tag += after is null
				? GetCloseTag(name)
				: before is null
				? GetOpenTag(name, after)
				: GetCloseTag(name) + GetOpenTag(name, after);
		}
		/// <summary>
		/// Gets a value indicating whether the style has a phrase.
		/// </summary>
		static abstract bool HasPhrase { get; }
	}
}
