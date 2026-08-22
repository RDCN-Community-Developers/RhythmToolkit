namespace RhythmBase.BeatBlock;

/// <summary>
/// BeatBlock naming rule: a chart named <c>easy</c> is stored as <c>chart-easy.json</c>.
/// </summary>
internal sealed class BeatBlockChartFileNaming : ChartFileNaming
{
	private const string Prefix = "chart-";
	private const string Extension = ".json";
	/// <inheritdoc/>
	public override string GetFileName(string chartName) => $"{Prefix}{chartName}{Extension}";
	/// <inheritdoc/>
	public override bool TryGetChartName(string fileName, out string chartName)
	{
		if (fileName.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase) && fileName.EndsWith(Extension, StringComparison.OrdinalIgnoreCase))
		{
			chartName = fileName[Prefix.Length..^Extension.Length];
			return true;
		}
		chartName = string.Empty;
		return false;
	}
}
/// <summary>
/// Provides the BeatBlock chart file naming rule.
/// </summary>
internal static class ChartNaming
{
	/// <summary>
	/// The format's chart name ↔ file name mapping.
	/// </summary>
	public static readonly ChartFileNaming Instance = new BeatBlockChartFileNaming();
}
