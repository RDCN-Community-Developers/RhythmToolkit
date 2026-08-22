using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace RhythmBase.Global.Components;


/// <summary>
/// Represents a chart (format-agnostic). A chart is one playable beatmap; how it is stored
/// (a single file, several documents, ...) is defined by the format-specific interfaces such as
/// <see cref="IJsonChart{TSelf, TBeat}"/>.
/// </summary>
public interface IChart
{
	/// <summary>
	/// Gets or sets the name of the chart (e.g. <c>easy</c> from <c>chart-easy.json</c>).
	/// </summary>
	public string Name { get; set; }
}
/// <summary>
/// Represents a chart (format-agnostic). A chart is one playable beatmap; how it is stored
/// (a single file, several documents, ...) is defined by the format-specific interfaces such as
/// <see cref="IJsonChart{TSelf, TBeat}"/>.
/// </summary>
/// <typeparam name="TSelf">The concrete chart type itself.</typeparam>
/// <typeparam name="TBeat">The type of beat used for timing calculations.</typeparam>
public interface IChart<TSelf, TBeat> : IChart
	where TSelf : IChart<TSelf, TBeat>
	where TBeat : struct, ITickTime<TBeat>
{
}
/// <summary>
/// Defines a chart format that is fully represented as JSON and can be saved to and loaded from a
/// single file or stream.
/// </summary>
/// <typeparam name="TSelf">The concrete chart type itself.</typeparam>
/// <typeparam name="TBeat">The type of beat used for timing calculations.</typeparam>
public interface IJsonChart<TSelf, TBeat> : IChart<TSelf, TBeat>
	where TSelf : IJsonChart<TSelf, TBeat>
	where TBeat : struct, ITickTime<TBeat>
{
#if NET8_0_OR_GREATER
	/// <summary>
	/// Deserializes an <typeparamref name="TSelf"/> object from the specified stream using the provided settings.
	/// </summary>
	/// <param name="stream">The stream containing the serialized <typeparamref name="TSelf"/> data. The stream must be readable and positioned at the start of
	/// the data.</param>
	/// <param name="settings">Optional settings that control the deserialization process. If not specified, default settings are used.</param>
	/// <returns>An <typeparamref name="TSelf"/> object representing the deserialized data. Returns an empty <typeparamref name="TSelf"/> instance if the stream contains
	/// no data or deserialization results in null.</returns>
	static abstract TSelf FromStream(Stream stream, LevelReadSettings? settings = null);
	/// <summary>
	/// Asynchronously reads a chart from a stream.
	/// </summary>
	/// <param name="stream">The stream containing the chart data.</param>
	/// <param name="settings">Optional settings for reading the chart.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>A <see cref="Task{T}"/> representing the asynchronous operation, with a <typeparamref name="TSelf"/> instance loaded from the stream.</returns>
	static abstract Task<TSelf> FromStreamAsync(Stream stream, LevelReadSettings? settings = null, CancellationToken cancellationToken = default);
	/// <summary>
	/// Creates a <typeparamref name="TSelf"/> instance by reading data from the specified file.
	/// </summary>
	/// <param name="filepath">The path to the file to read.</param>
	/// <param name="settings">Optional settings that control how the chart is read. If not provided, default settings are used.</param>
	/// <returns>A <typeparamref name="TSelf"/> instance representing the data read from the file.</returns>
	/// <exception cref="NotSupportedException">Thrown if the file format is not supported.</exception>
	static abstract TSelf FromFile(string filepath, LevelReadSettings? settings = null);
	/// <summary>
	/// Asynchronously loads a <typeparamref name="TSelf"/> instance from a file.
	/// </summary>
	/// <param name="filepath">The path to the file to load.</param>
	/// <param name="settings">Optional settings that control how the chart is read. If <see langword="null"/>, default settings are used.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the loaded <typeparamref name="TSelf"/> instance.</returns>
	/// <exception cref="NotSupportedException">Thrown if the file format is unsupported.</exception>
	static abstract Task<TSelf> FromFileAsync(string filepath, LevelReadSettings? settings = null, CancellationToken cancellationToken = default);
#endif
	/// <summary>
	/// Saves the current chart to a file in JSON format.
	/// </summary>
	/// <param name="filepath">The file path where the chart will be saved.</param>
	/// <param name="settings">Optional settings for writing the chart. If null, default settings are used.</param>
	void SaveToFile(string filepath, LevelWriteSettings? settings = null);
	/// <summary>
	/// Asynchronously saves the current chart to a file in JSON format.
	/// </summary>
	/// <param name="filepath">The file path where the chart will be saved.</param>
	/// <param name="settings">Optional settings for writing the chart. If null, default settings are used.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	Task SaveToFileAsync(string filepath, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default);
	/// <summary>
	/// Saves the current chart to the specified stream in JSON format.
	/// </summary>
	/// <param name="stream">The stream to which the chart will be saved.</param>
	/// <param name="settings">Optional settings for writing the chart. If null, default settings are used.</param>
	void SaveToStream(Stream stream, LevelWriteSettings? settings = null);
	/// <summary>
	/// Asynchronously saves the current chart to the specified stream in JSON format.
	/// </summary>
	/// <param name="stream">The stream to which the chart will be saved.</param>
	/// <param name="settings">Optional settings for writing the chart. If null, default settings are used.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	Task SaveToStreamAsync(Stream stream, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default);
#if NET8_0_OR_GREATER
	/// <summary>
	/// Deserializes a JSON document into a <typeparamref name="TSelf"/> instance using the specified settings.
	/// </summary>
	/// <param name="jsonDocument">The JSON document to deserialize. This parameter cannot be null.</param>
	/// <param name="settings">Optional settings that control the deserialization process. If not specified, default settings are used.</param>
	/// <returns>A <typeparamref name="TSelf"/> instance representing the deserialized data.</returns>
	static abstract TSelf FromJsonDocument(JsonDocument jsonDocument, LevelReadSettings? settings = null);
	/// <summary>
	/// Reads a chart from a JSON string.
	/// </summary>
	/// <param name="json">The JSON string containing the chart data.</param>
	/// <param name="settings">Optional settings for reading the chart.</param>
	/// <returns>A <typeparamref name="TSelf"/> instance loaded from the JSON string.</returns>
	static abstract TSelf FromJsonString(string json, LevelReadSettings? settings = null);
#endif
	/// <summary>
	/// Converts the current chart instance into a <see cref="JsonDocument"/>.
	/// </summary>
	/// <param name="settings">Optional settings to control the JSON serialization process. If <see langword="null"/>, default settings are used.</param>
	/// <returns>A <see cref="JsonDocument"/> representing the serialized chart data.</returns>
	JsonDocument ToJsonDocument(LevelWriteSettings? settings = null);
	/// <summary>
	/// Serializes the current chart instance into a JSON formatted string.
	/// </summary>
	/// <param name="settings">Optional settings to control the JSON serialization process. If <see langword="null"/>, default settings are used.</param>
	/// <returns>A string containing the JSON representation of the chart.</returns>
	string ToJsonString(LevelWriteSettings? settings = null);
}
/// <summary>
/// Marker interface for all level types.
/// </summary>
public interface ILevel { }
/// <summary>
/// Defines the base, format-agnostic contract for a level. A level is a container of charts;
/// its chart file I/O is provided by format-specific interfaces such as
/// <see cref="IJsonChart{TSelf, TBeat}"/>, while directory and archive storage are format-agnostic
/// transports provided here and by <see cref="IArchiveLevel{TSelf}"/>.
/// </summary>
/// <typeparam name="TSelf">The concrete level type itself.</typeparam>
/// <typeparam name="TChart">The concrete chart type stored in this level.</typeparam>
public interface ILevel<TSelf, TChart> : IDisposable, ILevel
		where TSelf : ILevel<TSelf, TChart>
		where TChart : IChart
{
	/// <summary>
	/// The original file path as provided (e.g., archive path or direct file path).
	/// </summary>
	public string? Filepath { get; }
	/// <summary>
	/// Gets the charts contained in this level, keyed by chart name. Adding or renaming entries keeps
	/// <see cref="IChart.Name"/> in sync with the dictionary key.
	/// </summary>
	public ChartDictionary<TChart> Charts { get; }
	/// <summary>
	/// Saves the current level to the specified directory.
	/// </summary>
	/// <param name="directoryPath">The path to the directory where the level will be saved. The directory will be created if it does not exist.</param>
	/// <param name="settings">Optional settings for writing the level. If null, default settings are used.</param>
	void SaveToDirectory(string directoryPath, LevelWriteSettings? settings = null);
	/// <summary>
	/// Asynchronously saves the current level to the specified directory.
	/// </summary>
	/// <param name="directoryPath">The path to the directory where the level will be saved. The directory will be created if it does not exist.</param>
	/// <param name="settings">Optional settings for writing the level. If null, default settings are used.</param>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	Task SaveToDirectoryAsync(string directoryPath, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default);
#if NET8_0_OR_GREATER
	/// <summary>
	/// The default level within the game.
	/// </summary>
	public static abstract TSelf Default { get; }
	/// <summary>
	/// Deserializes a <typeparamref name="TSelf"/> object from the specified directory using the provided settings.
	/// </summary>
	/// <param name="directoryPath">The path to the directory containing the level data. The directory must contain all necessary files for deserialization.</param>
	/// <param name="settings">Optional settings that control the deserialization process. If not specified, default settings are used.</param>
	/// <returns>A <typeparamref name="TSelf"/> object representing the deserialized data.</returns>
	static abstract TSelf FromDirectory(string directoryPath, LevelReadSettings? settings = null);
	/// <summary>
	/// Asynchronously reads a level from a directory.
	/// </summary>
	/// <param name="directoryPath">The path to the directory containing the level data. The directory must contain all necessary files for deserialization.</param>
	/// <param name="settings">Optional settings that control the deserialization process. If not specified, default settings are used.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the loaded <typeparamref name="TSelf"/> instance.</returns>
	static abstract Task<TSelf> FromDirectoryAsync(string directoryPath, LevelReadSettings? settings = null, CancellationToken cancellationToken = default);
#endif
}
/// <summary>
/// Defines a level format that can be saved and loaded as a ZIP archive containing the level data
/// and associated resources.
/// </summary>
public interface IArchiveLevel<TSelf, TChart> : ILevel<TSelf, TChart>
where TSelf : ILevel<TSelf, TChart>
where TChart : IChart
{
	/// <summary>     
	/// The resolved file path for reading. Points to the extracted temporary file entry if the source is an archive; otherwise identical to <c>Filepath</c>.
	/// Null if the level was not loaded from a file or archive.
	/// </summary>
	string? ResolvedPath { get; }
	/// <summary>
	/// The directory containing the resolved file. Points to the temporary extraction directory if the source is an archive; otherwise the directory of <c>Filepath</c>.
	/// Null if the level was not loaded from a file or archive.
	/// </summary>
	string? ResolvedDirectory { get; }
#if NET8_0_OR_GREATER
	/// <summary>
	/// Creates a new instance of the type from the contents of the specified zip file.
	/// </summary>
	/// <remarks>The zip file must be in a valid format expected by the implementation. This method
	/// may throw exceptions if the file does not exist, is inaccessible, or is not in the correct format.</remarks>
	/// <param name="filepath">The path to the zip file to read. The file must exist and be accessible.</param>
	/// <param name="settings">Optional settings that control how the zip file is read. If not specified, default settings are used.</param>
	/// <returns>An instance of the type represented by TSelf, initialized from the data in the specified zip file.</returns>
	[MemberNotNull(nameof(ResolvedPath))]
	static abstract TSelf FromZip(string filepath, LevelReadSettings? settings = null);
	/// <summary>
	/// Asynchronously creates an instance of the implementing type from a ZIP file at the specified path.
	/// </summary>
	/// <param name="filepath">The path to the ZIP file to read. This parameter cannot be null or empty.</param>
	/// <param name="settings">Optional settings that control how the ZIP file is read. If null, default settings are used.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is a non-cancelable token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the created instance of the
	/// implementing type.</returns>
	[MemberNotNull(nameof(ResolvedDirectory))]
	static abstract Task<TSelf> FromZipAsync(string filepath, LevelReadSettings? settings = null, CancellationToken cancellationToken = default);
	/// <summary>
	/// Asynchronously creates a new instance of the type from the contents of the specified zip stream.
	/// </summary>
	/// <param name="stream">The stream containing the zip file data.</param>
	/// <param name="settings">Optional settings that control how the zip stream is read. If not specified, default settings are used.</param>
	/// <returns>A task representing the asynchronous operation, with an instance of the type initialized from the zip stream.</returns>
	static abstract Task<TSelf> FromZip(Stream stream, LevelReadSettings? settings = null);
	/// <summary>
	/// Asynchronously creates an instance of the implementing type from a ZIP stream.
	/// </summary>
	/// <param name="stream">The stream containing the zip file data.</param>
	/// <param name="settings">Optional settings that control how the zip stream is read. If not specified, default settings are used.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is a non-cancelable token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the created instance of the implementing type.</returns>
	static abstract Task<TSelf> FromZipAsync(Stream stream, LevelReadSettings? settings = null, CancellationToken cancellationToken = default);
#endif
	/// <summary>
	/// Saves the current level data to a file in packed (ZIP) format at the specified path. (This method is not fully implemented yet.)
	/// </summary>
	/// <param name="filepath">The path of the file to create and write the packed level data to. Must not be null or empty.</param>
	/// <param name="settings">Optional settings that control how the level data is written. If null, default settings are used.</param>
	void SaveToZip(string filepath, LevelWriteSettings? settings = null);
	/// <summary>
	/// Asynchronously saves the current level and its associated assets to a ZIP archive at the specified file path.
	/// </summary>
	/// <param name="filepath">The full path to the ZIP file to create. If the file exists, it will be overwritten.</param>
	/// <param name="settings">Optional settings that control how the level and its assets are serialized. If null, default settings are used.</param>
	/// <param name="cancellationToken">A cancellation token that can be used to cancel the save operation.</param>
	Task SaveToZipAsync(string filepath, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default);
	/// <summary>
	/// Saves the current level and its associated assets to a ZIP archive written to the provided stream.
	/// </summary>
	/// <param name="stream">The stream to write the ZIP archive to.</param>
	/// <param name="settings">Optional settings that control how the level and its assets are serialized. If null, default settings are used.</param>
	void SaveToZip(Stream stream, LevelWriteSettings? settings = null);
	/// <summary>
	/// Asynchronously saves the current level and its associated assets to a ZIP archive written to the provided stream.
	/// </summary>
	/// <param name="stream">The stream to write the ZIP archive to.</param>
	/// <param name="settings">Optional settings that control how the level and its assets are serialized. If null, default settings are used.</param>
	/// <param name="cancellationToken">A cancellation token that can be used to cancel the save operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task SaveToZipAsync(Stream stream, LevelWriteSettings? settings = null, CancellationToken cancellationToken = default);
}
