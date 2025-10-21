using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.Global.Settings
{
	public static class GlobalSettings
	{
		public static string CachePath { get; set; } = Path.GetTempPath();
	}
}
