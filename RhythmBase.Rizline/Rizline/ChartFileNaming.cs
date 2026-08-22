namespace RhythmBase.Rizline;

/// <summary>
/// Rizline naming rule: charts are stored as <c>chart.json</c> (single) or <c>chart_{name}.json</c>
/// (multiple).
/// </summary>
internal sealed class RizlineChartFileNaming : ChartFileNaming
{
	private const string Prefix = "chart";
	private const string PrefixMulti = "chart_";
	private const string Extension = ".json";
	/// <inheritdoc/>
	public override string GetFileName(string chartName) => $"{PrefixMulti}{chartName}{Extension}";
	/// <inheritdoc/>
	public override bool TryGetChartName(string fileName, out string chartName)
	{
		if (fileName.Equals($"chart{Extension}", StringComparison.OrdinalIgnoreCase))
		{
			chartName = "chart";
			return true;
		}
		if (fileName.StartsWith(PrefixMulti, StringComparison.OrdinalIgnoreCase) && fileName.EndsWith(Extension, StringComparison.OrdinalIgnoreCase))
		{
			chartName = fileName[PrefixMulti.Length..^Extension.Length];
			return true;
		}
		chartName = string.Empty;
		return false;
	}
}
/// <summary>
/// Provides the Rizline chart file naming rule.
/// </summary>
internal static class ChartNaming
{
	/// <summary>
	/// The format's chart name ↔ file name mapping.
	/// </summary>
	public static readonly ChartFileNaming Instance = new RizlineChartFileNaming();
}
