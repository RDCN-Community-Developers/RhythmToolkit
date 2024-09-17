using System;
namespace RhythmBase.Components
{
	/// <summary>
	/// Bookmark.
	/// </summary>
	public class Bookmark
	{
		/// <summary>
		/// The beat where the bookmark is located.
		/// </summary>
		public Beat Beat { get; set; }
		/// <summary>
		/// Color on bookmark.
		/// </summary>
		public BookmarkColors Color { get; set; }

		public override string ToString() => string.Format("{0}, {1}", Beat, Color);
		/// <summary>
		/// Colors available for bookmarks.
		/// </summary>
		public enum BookmarkColors
		{
			Blue,
			Red,
			Yellow,
			Green
		}
	}
}
