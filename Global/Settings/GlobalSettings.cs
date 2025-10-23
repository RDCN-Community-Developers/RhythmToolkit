using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Global.Settings
{
	/// <summary>
	/// Provides global configuration settings for the application.
	/// </summary>
	/// <remarks>This class contains static properties that define application-wide settings. These settings can be
	/// accessed and modified globally throughout the application.</remarks>
	public static class GlobalSettings
	{
		/// <summary>
		/// Gets or sets the path to the directory used for caching temporary files.
		/// </summary>
		/// <remarks>The value of this property can be customized to specify a different directory for caching
		/// purposes. Ensure that the specified path is valid and accessible to avoid runtime errors.</remarks>
		public static string CachePath { get; set; } = Path.GetTempPath();
	}
}
