namespace RhythmBase.Global.Settings;

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
}
