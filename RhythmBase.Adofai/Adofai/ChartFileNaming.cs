namespace RhythmBase.Adofai;

/// <summary>
/// Adofai naming rule: a chart named <c>main</c> is stored as <c>main.adofai</c>.
/// </summary>
internal sealed class AdofaiChartFileNaming : ChartFileNaming
{
	private const string Extension = ".adofai";
	/// <inheritdoc/>
	public override string GetFileName(string chartName) => $"{chartName}{Extension}";
	/// <inheritdoc/>
	public override bool TryGetChartName(string fileName, out string chartName)
	{
		if (fileName.EndsWith(Extension, StringComparison.OrdinalIgnoreCase))
		{
			chartName = fileName[..^Extension.Length];
			return true;
		}
		chartName = string.Empty;
		return false;
	}
}
/// <summary>
/// Provides the Adofai chart file naming rule.
/// </summary>
internal static class ChartNaming
{
	/// <summary>
	/// The format's chart name ↔ file name mapping.
	/// </summary>
	public static readonly ChartFileNaming Instance = new AdofaiChartFileNaming();
}
