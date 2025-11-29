using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Global.Components
{
	/// <summary>
	/// Represents a reference to a file, providing access to its path and related file information.
	/// </summary>
	/// <remarks>The FileReference struct encapsulates a file path and offers utility members for working with file
	/// references, such as checking existence, retrieving the file extension, and determining if the reference is empty.
	/// </remarks>
	public struct FileReference
	{
		public string Path { get; set; }
		public readonly string Extension => System.IO.Path.GetExtension(Path);
		public readonly bool IsExist(string levelFile) =>
#if NETCOREAPP2_1_OR_GREATER
			System.IO.File.Exists(System.IO.Path.GetFullPath(Path, levelFile));
#else
			System.IO.File.Exists(GetFullPath(Path, levelFile));
		public static string GetFullPath(string path, string basePath)
		{
			if (System.IO.Path.IsPathRooted(path))
				return path;
			return System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(basePath) ?? "", path));
		}
#endif
		public readonly bool IsEmpty => string.IsNullOrEmpty(Path);
		public static implicit operator string(FileReference fileReference) => fileReference.Path;
		public static implicit operator FileReference(string path) => new FileReference { Path = path };
	}
}
