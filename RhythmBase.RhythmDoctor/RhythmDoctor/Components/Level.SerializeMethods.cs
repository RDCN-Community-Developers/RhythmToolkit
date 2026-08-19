using RhythmBase.Global.Serialization;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Serialization;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Components;

partial class Level
{
	#region file
	/// <summary>
	/// Loads a level from a single chart file (<c>.rdlevel</c> or <c>.json</c>).
	/// </summary>
	public static Level FromFile(string filepath, LevelReadSettings? settings = null)
		=> FromFileAsync(filepath, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously loads a level from a single chart file (<c>.rdlevel</c> or <c>.json</c>).
	/// </summary>
	public static async Task<Level> FromFileAsync(string filepath, LevelReadSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelReadSettings();
		string extension = Path.GetExtension(filepath);
		if (extension is not ".rdlevel" and not ".json")
		{
			if (extension is ".rdzip" or ".zip")
				throw new NotSupportedException($"File type '{extension}' is not supported. Use {nameof(FromZipAsync)} instead.");
			throw new NotSupportedException($"File type '{extension}' is not supported.");
		}
		Chart main = await LoadChartFileAsync(filepath, settings, cancellationToken);
		Level level = new(main)
		{
			Filepath = main.Filepath,
			ResolvedPath = main.ResolvedPath,
		};
		if (settings.LoadReferencedCharts)
			LoadReferencedCharts(level, settings);
		return level;
	}
	/// <summary>
	/// Saves the main chart of this level to a single chart file. When the level contains other charts,
	/// they are written as sibling <c>.rdlevel</c> files next to <paramref name="filepath"/>.
	/// </summary>
	public void SaveToFile(string filepath, LevelWriteSettings? settings = null)
		=> SaveToFileAsync(filepath, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously saves the main chart of this level to a single chart file. When the level contains
	/// other charts, they are written as sibling <c>.rdlevel</c> files next to <paramref name="filepath"/>.
	/// </summary>
	public async Task SaveToFileAsync(string filepath, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelWriteSettings();
		await Task.Run(() => SerializeChartFile(MainChart, filepath, settings), cancellationToken);
		string directoryPath = Path.GetDirectoryName(Path.GetFullPath(filepath)) ?? "";
		if (string.IsNullOrEmpty(directoryPath))
			return;
		foreach (var pair in _charts)
		{
			if (pair.Key == DefaultChartName)
				continue;
			await Task.Run(() => SerializeChartFile(pair.Value, Path.Combine(directoryPath, pair.Key), settings), cancellationToken);
		}
	}
	#endregion
	#region directory
	/// <summary>
	/// Loads a level from a directory containing <c>.rdlevel</c> chart files. The <c>main.rdlevel</c>
	/// file is loaded as the main chart and all other <c>.rdlevel</c> files are loaded as additional charts.
	/// </summary>
	public static Level FromDirectory(string directoryPath, LevelReadSettings? settings = null)
		=> FromDirectoryAsync(directoryPath, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously loads a level from a directory containing <c>.rdlevel</c> chart files.
	/// </summary>
	public static async Task<Level> FromDirectoryAsync(string directoryPath, LevelReadSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelReadSettings();
		string mainPath = Path.Combine(directoryPath, DefaultChartName);
		if (!File.Exists(mainPath))
			throw new FileNotFoundException($"The main chart '{mainPath}' was not found.");
		Chart main = await LoadChartFileAsync(mainPath, settings, cancellationToken);
		Dictionary<string, Chart> extras = [];
		foreach (string file in Directory.GetFiles(directoryPath, "*.rdlevel"))
		{
			if (Path.GetFileName(file) == DefaultChartName)
				continue;
			Chart extra = await LoadChartFileAsync(file, settings, cancellationToken);
			extras[Path.GetFileName(file)] = extra;
		}
		Level level = new(main, extras)
		{
			Filepath = main.Filepath,
			ResolvedPath = main.ResolvedPath,
		};
		if (settings.LoadReferencedCharts)
			LoadReferencedCharts(level, settings);
		return level;
	}
	/// <summary>
	/// Saves all charts contained in this level to the specified directory.
	/// </summary>
	public void SaveToDirectory(string directoryPath, LevelWriteSettings? settings = null)
		=> SaveToDirectoryAsync(directoryPath, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously saves all charts contained in this level to the specified directory.
	/// </summary>
	public async Task SaveToDirectoryAsync(string directoryPath, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelWriteSettings();
		Directory.CreateDirectory(directoryPath);
		foreach (var pair in _charts)
			await Task.Run(() => SerializeChartFile(pair.Value, Path.Combine(directoryPath, pair.Key), settings), cancellationToken);
	}
	#endregion
	#region stream
	/// <summary>
	/// Loads a level from a chart stream.
	/// </summary>
	public static Level FromStream(Stream rdlevelStream, LevelReadSettings? settings = null)
		=> FromStreamAsync(rdlevelStream, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously loads a level from a chart stream.
	/// </summary>
	public static async Task<Level> FromStreamAsync(Stream rdlevelStream, LevelReadSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelReadSettings();
		Chart chart = await ParseChartStreamAsync(rdlevelStream, settings, cancellationToken);
		return new Level(chart);
	}
	/// <summary>
	/// Saves the main chart of this level to the specified stream.
	/// </summary>
	public void SaveToStream(Stream stream, LevelWriteSettings? settings = null)
		=> SaveToStreamAsync(stream, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously saves the main chart of this level to the specified stream.
	/// </summary>
	public Task SaveToStreamAsync(Stream stream, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelWriteSettings();
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings);
		FileMainEntryConverter.SerializeMainEntry(MainChart, stream, options);
		return Task.CompletedTask;
	}
	#endregion
	#region zip
	/// <summary>
	/// Loads a level from a ZIP archive containing the main chart and its assets.
	/// </summary>
	public static Level FromZip(string filepath, LevelReadSettings? settings = null) => FromZipAsync(filepath, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously loads a level from a ZIP archive containing the main chart and its assets.
	/// </summary>
	public static async Task<Level> FromZipAsync(string filepath, LevelReadSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelReadSettings();
		string extension = Path.GetExtension(filepath);
		Chart main;
		Dictionary<string, Chart> extras = [];
		if (extension is not ".rdzip" and not ".zip")
		{
			if (extension is ".rdlevel" or ".json")
				throw new NotSupportedException($"File type '{extension}' is not supported. Use {nameof(FromFileAsync)} instead.");
			throw new NotSupportedException($"File type '{extension}' is not supported.");
		}
		switch (settings.ZipProcessingMode)
		{
			case ZipProcessingMode.AllEntries:
				DirectoryInfo tempDirectory = new(Path.Combine(GlobalSettings.CachePath, "RhythmBaseTemp_Zip_" + Path.GetRandomFileName()));
				tempDirectory.Create();
				try
				{
#if NET8_0_OR_GREATER
					using Stream stream = File.OpenRead(filepath);
					ZipFile.ExtractToDirectory(stream, tempDirectory.FullName, overwriteFiles: true);
#elif NETSTANDARD2_0_OR_GREATER
					ZipFile.ExtractToDirectory(filepath, tempDirectory.FullName);
#endif
					string? rdlevelPath = null;
					foreach (FileInfo? file in tempDirectory.GetFiles())
					{
						if (file.Name == "main.rdlevel")
						{
							rdlevelPath = file.FullName;
							break;
						}
					}
					if (rdlevelPath == null)
						throw new FileNotFoundException("No RDLevel file has been found.");
					main = await LoadChartFileAsync(rdlevelPath, settings, cancellationToken);
					main.ResolvedPath = Path.GetFullPath(rdlevelPath);
					main.Filepath = Path.GetFullPath(filepath);
					main.isZip = true;
					main.isExtracted = true;
					foreach (FileInfo file in tempDirectory.GetFiles("*.rdlevel"))
					{
						if (file.Name == "main.rdlevel")
							continue;
						Chart extra = await LoadChartFileAsync(file.FullName, settings, cancellationToken);
						extras[file.Name] = extra;
					}
				}
				catch (Exception)
				{
					tempDirectory.Delete(true);
					throw;
				}
				break;
			case ZipProcessingMode.RootEntriesOnly:
				using (FileStream zipStream = new(filepath, FileMode.Open, FileAccess.Read))
				{
					using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);
					ZipArchiveEntry entry = archive.GetEntry("main.rdlevel") ?? throw new FileNotFoundException("Cannot find the level file.");
					using Stream entryStream = entry.Open();
					main = await ParseChartStreamAsync(entryStream, settings, cancellationToken);
				}
				main.Filepath = Path.GetFullPath(filepath);
				main.isZip = true;
				main.isExtracted = false;
				break;
			default:
				throw new NotSupportedException(extension + " is not supported.");
		}
		Level level = new(main, extras)
		{
			Filepath = main.Filepath,
			ResolvedPath = main.ResolvedPath,
		};
		if (settings.LoadReferencedCharts)
			LoadReferencedCharts(level, settings);
		return level;
	}
	/// <summary>
	/// Asynchronously loads a level from a ZIP stream containing the main chart.
	/// </summary>
	public static Task<Level> FromZip(Stream zipStream, LevelReadSettings? settings = null)
		=> FromZipAsync(zipStream, settings);
	/// <summary>
	/// Asynchronously loads a level from a ZIP stream containing the main chart.
	/// </summary>
	public static async Task<Level> FromZipAsync(Stream zipStream, LevelReadSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelReadSettings();
		Chart main;
		using (ZipArchive archive = new(zipStream, ZipArchiveMode.Read))
		{
			ZipArchiveEntry entry = archive.GetEntry("main.rdlevel") ?? throw new FileNotFoundException("Cannot find the level file.");
			using Stream stream = entry.Open();
			main = await ParseChartStreamAsync(stream, settings, cancellationToken);
		}
		main.isZip = true;
		main.isExtracted = false;
		return new Level(main);
	}
	/// <summary>
	/// Saves all charts contained in this level to a ZIP archive at the specified path. Referenced
	/// charts' assets are additionally packed when <see cref="LevelWriteSettings.PackReferencedCharts"/> is enabled.
	/// </summary>
	public void SaveToZip(string filepath, LevelWriteSettings? settings = null)
		=> SaveToZipAsync(filepath, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously saves the level to a ZIP archive at the specified path.
	/// </summary>
	public async Task SaveToZipAsync(string filepath, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelWriteSettings();
		DirectoryInfo directory = new FileInfo(filepath).Directory ?? new("");
		if (!directory.Exists)
			directory.Create();
		using FileStream stream = new(filepath, FileMode.Create, FileAccess.Write);
		await SaveToZipAsync(stream, settings, cancellationToken);
	}
	/// <summary>
	/// Saves the level to the specified ZIP stream.
	/// </summary>
	public void SaveToZip(Stream zipStream, LevelWriteSettings? settings = null)
		=> SaveToZipAsync(zipStream, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously saves the level to the specified ZIP stream. All charts contained in the level are
	/// written as entries. Referenced charts' assets are additionally packed only when
	/// <see cref="LevelWriteSettings.PackReferencedCharts"/> is enabled.
	/// </summary>
	public async Task SaveToZipAsync(Stream zipStream, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelWriteSettings();
		using ZipArchive archive = new(zipStream, ZipArchiveMode.Create, leaveOpen: true);
		foreach (var pair in _charts)
		{
			string name = pair.Key;
			Chart chart = pair.Value;
			bool packAssets = chart == MainChart || settings.PackReferencedCharts;
			string directory = ResolveChartDirectory(chart, settings);
			HashSet<FileReference> fileReferences = [];
			void referenceDelegate(object? sender, FileReferenceArgs args) => fileReferences.Add(args.Reference);
			bool loadAssets = settings.LoadAssets;
			if (packAssets)
			{
				settings.FileReferenceEncountered += referenceDelegate;
				settings.LoadAssets = true;
			}
			ZipArchiveEntry entry = archive.CreateEntry(name);
			using (Stream chartStream = entry.Open())
			{
				MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings);
				options.DirectoryName = directory;
				await Task.Run(() => FileMainEntryConverter.SerializeMainEntry(chart, chartStream, options), cancellationToken);
			}
			if (packAssets)
			{
				settings.FileReferenceEncountered -= referenceDelegate;
				settings.LoadAssets = loadAssets;
				foreach (var file in fileReferences)
				{
					if (string.IsNullOrEmpty(directory))
						continue;
					string fullPath = Path.Combine(directory, file.Path);
					if (!File.Exists(fullPath))
						throw new FileNotFoundException($"Referenced file '{file.Path}' not found in the resolved directory.");
					archive.CreateEntryFromFile(fullPath, Path.GetFileName(file.Path));
				}
			}
		}
	}
	#endregion
	#region json
	/// <summary>
	/// Loads a level from a JSON string.
	/// </summary>
	public static Level FromJsonString(string json, LevelReadSettings? settings = null)
	{
		settings ??= new LevelReadSettings();
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForRead(settings);
		Chart chart = FileMainEntryConverter.DeserializeMainEntry<Chart>(new ReadOnlyMemoryDataSource(new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(json))), options);
		return new Level(chart);
	}
	/// <summary>
	/// Loads a level from a <see cref="JsonDocument"/>.
	/// </summary>
	public static Level FromJsonDocument(JsonDocument jsonDocument, LevelReadSettings? settings = null)
	{
		settings ??= new LevelReadSettings();
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForRead(settings);
		Chart chart = FileMainEntryConverter.DeserializeMainEntry<Chart>(new JsonDocumentDataSource(jsonDocument), options);
		return new Level(chart);
	}
	/// <summary>
	/// Serializes the main chart of this level to a JSON string.
	/// </summary>
	public string ToJsonString(LevelWriteSettings? settings = null)
	{
		settings ??= new LevelWriteSettings();
		string json;
		using (MemoryStream stream = new())
		{
			FileMainEntryConverter.SerializeMainEntry(MainChart, stream, JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings));
			stream.Seek(0, SeekOrigin.Begin);
			json = Encoding.UTF8.GetString(stream.ToArray());
		}
		return json;
	}
	/// <summary>
	/// Serializes the main chart of this level to a <see cref="JsonDocument"/>.
	/// </summary>
	public JsonDocument ToJsonDocument(LevelWriteSettings? settings = null)
	{
		settings ??= new LevelWriteSettings();
		string json;
		using (MemoryStream stream = new())
		{
			FileMainEntryConverter.SerializeMainEntry(MainChart, stream, JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings));
			stream.Seek(0, SeekOrigin.Begin);
			json = Encoding.UTF8.GetString(stream.ToArray());
		}
		return JsonDocument.Parse(json);
	}
	#endregion
	/// <summary>
	/// Resolves the directory used to locate a chart's referenced assets, giving precedence to
	/// <paramref name="settings"/>.<see cref="LevelWriteSettings.ResolvedDirectory"/> over the chart's
	/// own <see cref="Chart.ResolvedDirectory"/>.
	/// </summary>
	private static string ResolveChartDirectory(Chart chart, LevelWriteSettings settings) =>
		!string.IsNullOrWhiteSpace(settings.ResolvedDirectory)
		? settings.ResolvedDirectory
		: chart.ResolvedDirectory;
	/// <summary>
	/// Recursively resolves charts referenced by <see cref="GoToLevel"/> events into the level's chart
	/// collection. Each referenced file is loaded only once and shared by all <see cref="GoToLevel"/>
	/// events, which makes cyclic references between charts safe.
	/// </summary>
	private static void LoadReferencedCharts(Level level, LevelReadSettings settings)
	{
		Queue<Chart> pending = new();
		pending.Enqueue(level.MainChart);
		while (pending.Count > 0)
		{
			Chart chart = pending.Dequeue();
			foreach (GoToLevel goTo in chart.OfType<GoToLevel>())
			{
				if (goTo.Chart.IsEmpty)
					continue;
				string key = Path.GetFileName(goTo.Chart.Path);
				if (string.IsNullOrEmpty(key))
					continue;
				if (level.TryGetChart(key, out Chart? existing))
				{
					goTo.ResolvedLevel = existing;
					continue;
				}
				string baseDir = string.IsNullOrEmpty(chart.ResolvedDirectory) ? Directory.GetCurrentDirectory() : chart.ResolvedDirectory;
				string fullPath = Path.GetFullPath(goTo.Chart.Path, baseDir);
				if (!File.Exists(fullPath))
					continue;
				Chart refChart = LoadChartFileAsync(fullPath, settings, CancellationToken.None).GetAwaiter().GetResult();
				level.RegisterChart(key, refChart);
				goTo.ResolvedLevel = refChart;
				pending.Enqueue(refChart);
			}
		}
	}
	/// <summary>
	/// Deserializes a single chart from the specified file and assigns its file paths.
	/// </summary>
	private static async Task<Chart> LoadChartFileAsync(string filepath, LevelReadSettings settings, CancellationToken cancellationToken)
	{
		using FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
		Chart chart = await ParseChartStreamAsync(stream, settings, cancellationToken);
		chart.Filepath = chart.ResolvedPath = Path.GetFullPath(filepath);
		return chart;
	}
	/// <summary>
	/// Deserializes a single chart from the specified stream.
	/// </summary>
	private static async Task<Chart> ParseChartStreamAsync(Stream stream, LevelReadSettings settings, CancellationToken cancellationToken)
	{
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForRead(settings);
		options.DirectoryName = stream is FileStream fileStream ? Path.GetDirectoryName(fileStream.Name) : null;
		return await FileMainEntryConverter.DeserializeMainEntryAsync<Chart>(new StreamDataSource(stream), options, cancellationToken);
	}
	/// <summary>
	/// Serializes a single chart to the specified file.
	/// </summary>
	private static void SerializeChartFile(Chart chart, string filepath, LevelWriteSettings settings)
	{
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings);
		options.DirectoryName = new FileInfo(filepath).Directory?.FullName;
		DirectoryInfo directory = new FileInfo(filepath).Directory ?? new("");
		if (!directory.Exists)
			directory.Create();
		using FileStream stream = File.Open(filepath, FileMode.OpenOrCreate, FileAccess.Write);
		stream.SetLength(0);
		FileMainEntryConverter.SerializeMainEntry(chart, stream, options);
	}
}
