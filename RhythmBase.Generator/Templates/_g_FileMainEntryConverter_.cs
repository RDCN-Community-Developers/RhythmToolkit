using System.Text.Json;

namespace RhythmBase._g_registryId_.Serialization;

/// <summary>
/// Provides entry point methods for deserializing and serializing level files
/// for the <c>_g_registryId_</c> game adapter.
/// </summary>
public class FileMainEntryConverter
{
	private static readonly JsonReaderOptions _readerOptions = new();
	[global::System.Diagnostics.DebuggerHidden]
	[global::System.Diagnostics.StackTraceHidden]
	private static void WrapAndThrow(JsonException ex, RhythmBase.Global.Serialization.IJsonDataSource dataSource, long bytesConsumed)
	{
		long originalPos = dataSource.MapToInputPosition(bytesConsumed);
		if (originalPos >= 0)
			throw new JsonException($"{ex.Message}\n  at original stream byte position ~{originalPos}", ex);
		throw new JsonException($"{ex.Message}\n  at processed byte position {bytesConsumed}", ex);
	}
	/// <summary>
	/// Deserializes a level from the specified data source.
	/// </summary>
	/// <typeparam name="T">The level type to deserialize.</typeparam>
	/// <param name="dataSource">The JSON data source to read from.</param>
	/// <param name="options">The metadata-aware serializer options.</param>
	/// <returns>The deserialized level instance, or a new empty instance if deserialization fails.</returns>
	public static T DeserializeMainEntry<T>(RhythmBase.Global.Serialization.IJsonDataSource dataSource, RhythmBase.Global.Serialization.MetadataJsonSerializerOptions options)
			where T : new()
	{
		var seq = dataSource.GetSequence();
		Utf8JsonReader reader = seq.IsSingleSegment
			? new Utf8JsonReader(seq.First.Span, _readerOptions)
			: new Utf8JsonReader(seq, _readerOptions);
		return RhythmBase._g_registryId_.Serialization.TypeConverterRegistry.Read<T>(ref reader, options) ?? new();
	}
	/// <summary>
	/// Asynchronously deserializes a level from the specified data source.
	/// </summary>
	/// <typeparam name="T">The level type to deserialize.</typeparam>
	/// <param name="dataSource">The JSON data source to read from.</param>
	/// <param name="options">The metadata-aware serializer options.</param>
	/// <param name="cancellationToken">A token to cancel the operation.</param>
	/// <returns>The deserialized level instance, or a new empty instance if deserialization fails.</returns>
	public static async Task<T> DeserializeMainEntryAsync<T>(RhythmBase.Global.Serialization.IJsonDataSource dataSource, RhythmBase.Global.Serialization.MetadataJsonSerializerOptions options, CancellationToken cancellationToken = default)
			where T : new()
	{
		var seq = await dataSource.GetSequenceAsync(cancellationToken);
		Utf8JsonReader reader = seq.IsSingleSegment
			? new Utf8JsonReader(seq.First.Span, _readerOptions)
			: new Utf8JsonReader(seq, _readerOptions);
		return RhythmBase._g_registryId_.Serialization.TypeConverterRegistry.Read<T>(ref reader, options) ?? new();
	}
	/// <summary>
	/// Serializes a level to the specified stream.
	/// </summary>
	/// <typeparam name="T">The level type to serialize.</typeparam>
	/// <param name="mainEntry">The level instance to serialize.</param>
	/// <param name="stream">The output stream to write to.</param>
	/// <param name="options">The metadata-aware serializer options.</param>
	public static void SerializeMainEntry<T>(T mainEntry, Stream stream, RhythmBase.Global.Serialization.MetadataJsonSerializerOptions options)
	{
		using Utf8JsonWriter writer = new(stream, new()
		{
			Indented = options.JsonSerializerOptions.WriteIndented,
			Encoder = options.JsonSerializerOptions.Encoder,
			IndentCharacter = options.JsonSerializerOptions.IndentCharacter,
			IndentSize = options.JsonSerializerOptions.IndentSize,
		});
		RhythmBase._g_registryId_.Serialization.TypeConverterRegistry.Write(writer, mainEntry, options);
		writer.Flush();
	}
}

