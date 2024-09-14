using System;

namespace RhythmBase.Settings
{

	public class SpriteReadOrWriteSettings
	{

		public SpriteReadOrWriteSettings()
		{
			OverWrite = false;
			Indented = true;
			IgnoreNullValue = false;
			WithImage = false;
		}

		/// <summary>
		/// Whether to overwrite the source file.
		/// If <see langword="false" /> and the export path is the same as the source path, an exception will be thrown.
		/// Defaults to <see langword="false" />.
		/// </summary>

		public bool OverWrite { get; set; }

		/// <summary>
		/// Use the indent and align expressions property.
		/// Defaults to <see langword="true" />.
		/// </summary>

		public bool Indented { get; set; }

		/// <summary>
		/// Ignore all null values.
		/// </summary>
		/// Defaults to <see langword="false" />.

		public bool IgnoreNullValue { get; set; }

		/// <summary>
		/// Export the image file at the same time as the export.
		/// </summary>
		/// Defaults to <see langword="false" />.

		public bool WithImage { get; set; }
	}
}
