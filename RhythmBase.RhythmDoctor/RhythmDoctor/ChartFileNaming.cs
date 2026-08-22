namespace RhythmBase.RhythmDoctor;

/// <summary>
/// RhythmDoctor naming rule: a chart named <c>main</c> is stored as <c>main.rdlevel</c>.
/// </summary>
internal sealed class RhythmDoctorChartFileNaming : ChartFileNaming
{
	/// <inheritdoc/>
	public override string GetFileName(string chartName) => $"{chartName}.rdlevel";
	/// <inheritdoc/>
	public override bool TryGetChartName(string fileName, out string chartName)
	{
		const string extension = ".rdlevel";
		if (fileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
		{
			chartName = fileName[..^extension.Length];
			return true;
		}
		chartName = string.Empty;
		return false;
	}
}
/// <summary>
/// Provides the RhythmDoctor chart file naming rule.
/// </summary>
internal static class ChartNaming
{
	/// <summary>
	/// The format's chart name ↔ file name mapping.
	/// </summary>
	public static readonly ChartFileNaming Instance = new RhythmDoctorChartFileNaming();
}
