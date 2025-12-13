using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RhythmBase.Global.Components
{
	/// <summary>
	/// Represents a reference to a file by its path and provides utility members for file-related queries.
	/// </summary>
	/// <remarks>
	/// The <see cref="FileReference"/> struct encapsulates a file path and offers utility members for working
	/// with file references, such as checking existence, retrieving the file extension, and determining if the
	/// reference is empty. Implicit conversions to and from <see cref="string"/> are provided for convenience.
	/// </remarks>
	public struct FileReference : IEqualityComparer<FileReference>
	{
		/// <summary>
		/// Gets or sets the file path for this reference.
		/// </summary>
		/// <remarks>
		/// The path may be absolute or relative. Relative paths are interpreted relative to a provided base file
		/// when calling methods such as <see cref="IsExist(string)"/>.
		/// </remarks>
		public string Path { get; set; }

		/// <summary>
		/// Gets the file extension of the referenced path, including the leading period ('.').
		/// </summary>
		/// <value>
		/// The extension of <see cref="Path"/> as returned by <see cref="System.IO.Path.GetExtension(string)"/>.
		/// If <see cref="Path"/> is null or has no extension, an empty string is returned.
		/// </value>
		public readonly string Extension => System.IO.Path.GetExtension(Path);

		/// <summary>
		/// Determines whether the file referenced by <see cref="Path"/> exists.
		/// </summary>
		/// <param name="levelFile">
		/// A base file path used to resolve relative <see cref="Path"/> values. If <see cref="Path"/> is absolute,
		/// <paramref name="levelFile"/> is ignored.
		/// </param>
		/// <returns>
		/// <c>true</c> if the resolved file path exists on disk; otherwise, <c>false</c>.
		/// </returns>
		public readonly bool IsExist(string levelFile) =>
#if NETCOREAPP2_1_OR_GREATER
				System.IO.File.Exists(System.IO.Path.GetFullPath(Path, levelFile));
#else
			System.IO.File.Exists(GetFullPath(Path, levelFile));

		/// <summary>
		/// Resolves a possibly relative path to an absolute path using a provided base path.
		/// </summary>
		/// <param name="path">The path to resolve. May be absolute or relative.</param>
		/// <param name="basePath">The base file path used to resolve relative paths.</param>
		/// <returns>
		/// The absolute path for <paramref name="path"/>. If <paramref name="path"/> is already rooted, it is returned
		/// unchanged; otherwise it is combined with the directory of <paramref name="basePath"/> and normalized.
		/// </returns>
		private static string GetFullPath(string path, string basePath)
		{
			if (System.IO.Path.IsPathRooted(path))
				return path;
			return System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(basePath) ?? "", path));
		}
#endif

		/// <summary>
		/// Gets a value indicating whether this file reference has no path.
		/// </summary>
		/// <value>
		/// <c>true</c> if <see cref="Path"/> is null or an empty string; otherwise, <c>false</c>.
		/// </value>
		public readonly bool IsEmpty => string.IsNullOrEmpty(Path);

		/// <summary>
		/// Implicitly converts a <see cref="FileReference"/> to a <see cref="string"/> by returning its <see cref="Path"/>.
		/// </summary>
		/// <param name="fileReference">The file reference to convert.</param>
		/// <returns>The <see cref="Path"/> value of <paramref name="fileReference"/>.</returns>
		public static implicit operator string(FileReference fileReference) => fileReference.Path;

		/// <summary>
		/// Implicitly converts a <see cref="string"/> path to a <see cref="FileReference"/>.
		/// </summary>
		/// <param name="path">The path to wrap as a <see cref="FileReference"/>.</param>
		/// <returns>A new <see cref="FileReference"/> instance with <see cref="Path"/> set to <paramref name="path"/>.</returns>
		public static implicit operator FileReference(string path) => new() { Path = path };
		public static bool operator ==(FileReference left, FileReference right) => left.Path == right.Path;
		public static bool operator !=(FileReference left, FileReference right) => left.Path != right.Path;
		///<inheritdoc/>
		public override readonly bool Equals(object? obj) => obj is FileReference other && this == other;
		///<inheritdoc/>
		public readonly override int GetHashCode() => Path.GetHashCode();
		///<inheritdoc/>
		public override string ToString() => Path;
		///<inheritdoc/>
		public bool Equals(FileReference x, FileReference y) => x.Path == y.Path;
		///<inheritdoc/>
		public int GetHashCode(
#if NET7_0_OR_GREATER
				[DisallowNull]
#endif
		FileReference obj) => obj.Path.GetHashCode();
	}
}
