﻿namespace RhythmBase.Components
{
	/// <summary>
	/// Represents a bookmark in the rhythm base.
	/// </summary>
	public class Bookmark
	{
		/// <summary>
		/// Gets or sets the beat where the bookmark is located.
		/// </summary>
		public RDBeat Beat { get; set; }		/// <summary>
		/// Gets or sets the color of the bookmark.
		/// </summary>
		public BookmarkColors Color { get; set; }		/// <summary>
		/// Returns a string that represents the current bookmark.
		/// </summary>
		/// <returns>A string that represents the current bookmark.</returns>
		public override string ToString() => string.Format("{0}, {1}", Beat, Color);		/// <summary>
		/// Specifies the colors available for bookmarks.
		/// </summary>
		public enum BookmarkColors
		{
			/// <summary>
			/// Represents the color blue.
			/// </summary>
			Blue,
			/// <summary>
			/// Represents the color red.
			/// </summary>
			Red,
			/// <summary>
			/// Represents the color yellow.
			/// </summary>
			Yellow,
			/// <summary>
			/// Represents the color green.
			/// </summary>
			Green
		}
	}
}
