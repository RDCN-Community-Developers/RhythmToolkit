using RhythmBase.Global.Serialization;
using RhythmBase.RhythmDoctor.Serialization;
using System.Text;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Components;

partial class Chart
{
	#region file
	/// <summary>
	/// Loads a single chart from a <c>.rdlevel</c> or <c>.json</c> file.
	/// </summary>
	public static Chart FromFile(string filepath, LevelReadSettings? settings = null)
		=> FromFileAsync(filepath, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously loads a single chart from a <c>.rdlevel</c> or <c>.json</c> file.
	/// </summary>
	public static async Task<Chart> FromFileAsync(string filepath, LevelReadSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelReadSettings();
		string extension = Path.GetExtension(filepath);
		if (extension is not ".rdlevel" and not ".json")
		{
			if (extension is ".rdzip" or ".zip")
				throw new NotSupportedException($"File type '{extension}' is not supported. Use {nameof(Level.FromZipAsync)} instead.");
			throw new NotSupportedException($"File type '{extension}' is not supported.");
		}
		using FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForRead(settings);
		options.DirectoryName = new FileInfo(filepath).Directory?.FullName;
		Chart chart = await FileMainEntryConverter.DeserializeMainEntryAsync<Chart>(new StreamDataSource(stream), options, cancellationToken);
		chart.Filepath = chart.ResolvedPath = Path.GetFullPath(filepath);
		if (ChartNaming.Instance.TryGetChartName(Path.GetFileName(filepath), out string name))
			chart.Name = name;
		return chart;
	}
	/// <summary>
	/// Saves this chart to a single <c>.rdlevel</c> or <c>.json</c> file.
	/// </summary>
	public void SaveToFile(string filepath, LevelWriteSettings? settings = null)
		=> SaveToFileAsync(filepath, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously saves this chart to a single <c>.rdlevel</c> or <c>.json</c> file.
	/// </summary>
	public async Task SaveToFileAsync(string filepath, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelWriteSettings();
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings);
		options.DirectoryName = new FileInfo(filepath).Directory?.FullName;
		DirectoryInfo directory = new FileInfo(filepath).Directory ?? new("");
		if (!directory.Exists)
			directory.Create();
		using FileStream stream = File.Open(filepath, FileMode.OpenOrCreate, FileAccess.Write);
		stream.SetLength(0);
		await Task.Run(() => FileMainEntryConverter.SerializeMainEntry(this, stream, options), cancellationToken);
	}
	#endregion
	#region stream
	/// <summary>
	/// Loads a single chart from a stream.
	/// </summary>
	public static Chart FromStream(Stream rdlevelStream, LevelReadSettings? settings = null)
		=> FromStreamAsync(rdlevelStream, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously loads a single chart from a stream.
	/// </summary>
	public static async Task<Chart> FromStreamAsync(Stream rdlevelStream, LevelReadSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelReadSettings();
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForRead(settings);
		return await FileMainEntryConverter.DeserializeMainEntryAsync<Chart>(new StreamDataSource(rdlevelStream), options, cancellationToken);
	}
	/// <summary>
	/// Saves this chart to the specified stream.
	/// </summary>
	public void SaveToStream(Stream stream, LevelWriteSettings? settings = null)
		=> SaveToStreamAsync(stream, settings).GetAwaiter().GetResult();
	/// <summary>
	/// Asynchronously saves this chart to the specified stream.
	/// </summary>
	public Task SaveToStreamAsync(Stream stream, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default)
	{
		settings ??= new LevelWriteSettings();
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings);
		FileMainEntryConverter.SerializeMainEntry(this, stream, options);
		return Task.CompletedTask;
	}
	#endregion
	#region json
	/// <summary>
	/// Loads a single chart from a JSON string.
	/// </summary>
	public static Chart FromJsonString(string json, LevelReadSettings? settings = null)
	{
		settings ??= new LevelReadSettings();
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForRead(settings);
		return FileMainEntryConverter.DeserializeMainEntry<Chart>(new ReadOnlyMemoryDataSource(new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(json))), options);
	}
	/// <summary>
	/// Loads a single chart from a <see cref="JsonDocument"/>.
	/// </summary>
	public static Chart FromJsonDocument(JsonDocument jsonDocument, LevelReadSettings? settings = null)
	{
		settings ??= new LevelReadSettings();
		MetadataJsonSerializerOptions options = JsonSerializerOptionsUtils.GetJsonSerializerOptionsForRead(settings);
		return FileMainEntryConverter.DeserializeMainEntry<Chart>(new JsonDocumentDataSource(jsonDocument), options);
	}
	/// <summary>
	/// Serializes this chart to a JSON string.
	/// </summary>
	public string ToJsonString(LevelWriteSettings? settings = null)
	{
		settings ??= new LevelWriteSettings();
		using MemoryStream stream = new();
		FileMainEntryConverter.SerializeMainEntry(this, stream, JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings));
		stream.Seek(0, SeekOrigin.Begin);
		return Encoding.UTF8.GetString(stream.ToArray());
	}
	/// <summary>
	/// Serializes this chart to a <see cref="JsonDocument"/>.
	/// </summary>
	public JsonDocument ToJsonDocument(LevelWriteSettings? settings = null)
	{
		settings ??= new LevelWriteSettings();
		using MemoryStream stream = new();
		FileMainEntryConverter.SerializeMainEntry(this, stream, JsonSerializerOptionsUtils.GetJsonSerializerOptionsForWrite(settings));
		stream.Seek(0, SeekOrigin.Begin);
		return JsonDocument.Parse(stream);
	}
	#endregion
}
