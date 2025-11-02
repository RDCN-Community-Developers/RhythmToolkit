namespace RhythmBase.Global.Settings
{
	/// <summary>
	/// Provides global configuration settings for the application.
	/// </summary>
	/// <remarks>
	/// This static class contains application-wide configuration values and defaults.
	/// Settings declared here are intended to be globally accessible and stable across the runtime.
	/// </remarks>
	public static class GlobalSettings
	{
		/// <summary>
		/// Gets or sets the path to the directory used for caching temporary files.
		/// </summary>
		/// <remarks>
		/// Defaults to the operating system's temporary directory as returned by <see cref="Path.GetTempPath"/>.
		/// Consumers may override this path to direct cache files to a specific location; ensure the
		/// configured directory exists and the application has appropriate read/write permissions.
		/// </remarks>
		/// <value>
		/// A string representing an absolute or relative filesystem path used for temporary cache files.
		/// </value>
		public static string CachePath { get; set; } = Path.GetTempPath();

		/// <summary>
		/// The minimum supported A Dance of Fire and Air (AdoFai) format version that this application supports.
		/// </summary>
		/// <remarks>
		/// This constant represents the lowest AdoFai version the codebase guarantees compatibility with.
		/// Use this value to validate incoming files or to inform users when a file's version is unsupported.
		/// </remarks>
		public const int MinimumSupportedVersionAdofai = 15;

		/// <summary>
		/// The default AdoFai format version assumed when creating or exporting content.
		/// </summary>
		/// <remarks>
		/// Use this constant when a version must be assigned implicitly (for example, when creating new content)
		/// and no explicit version is provided by the caller. Keep this value in sync with library capabilities.
		/// </remarks>
		public const int DefaultVersionAdofai = 15;

		/// <summary>
		/// The minimum supported version of Rhythm Doctor that this application targets.
		/// </summary>
		/// <remarks>
		/// This value is used to determine compatibility and may be referenced when importing/exporting
		/// Rhythm Doctor assets, selecting feature subsets, or displaying compatibility warnings to users.
		/// </remarks>
		public const int MinimumSupportedVersionRhythmDoctor = 54;

		/// <summary>
		/// The default Rhythm Doctor version used when no explicit target version is specified.
		/// </summary>
		/// <remarks>
		/// This constant defines the default target version for generation or export operations. Update this
		/// value when the codebase adds support for newer Rhythm Doctor versions and you want the default
		/// behavior to change accordingly.
		/// </remarks>
		public const int DefaultVersionRhythmDoctor = 64;
	}
}
