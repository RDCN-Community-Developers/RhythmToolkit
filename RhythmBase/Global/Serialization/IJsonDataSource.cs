using System.Buffers;
using System.Text.Json;

namespace RhythmBase.Global.Serialization;

/// <summary>
/// Abstracts the source of JSON data for level deserialization, allowing callers to provide
/// a <see cref="Stream"/>, <see cref="JsonDocument"/>, or raw <see cref="ReadOnlyMemory{T}"/> of bytes.
/// </summary>
public interface IJsonDataSource
{
    /// <summary>
    /// Asynchronously obtains the JSON data as a <see cref="ReadOnlySequence{T}"/> of UTF-8 bytes.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The JSON data as a sequence of UTF-8 byte segments.</returns>
    ValueTask<ReadOnlySequence<byte>> GetSequenceAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Synchronously obtains the JSON data as a <see cref="ReadOnlySequence{T}"/> of UTF-8 bytes.
    /// </summary>
    /// <returns>The JSON data as a sequence of UTF-8 byte segments.</returns>
    ReadOnlySequence<byte> GetSequence();
    /// <summary>
    /// Maps a byte position in the processed (compacted) JSON data back to an approximate
    /// byte position in the original source stream. Returns <c>-1</c> if mapping is unavailable.
    /// </summary>
    /// <param name="processedPosition">The byte position from <see cref="Utf8JsonReader.BytesConsumed"/> or <see cref="Utf8JsonReader.TokenStartIndex"/>.</param>
    /// <returns>The approximate byte position in the original stream, or <c>-1</c> if unavailable.</returns>
    long MapToInputPosition(long processedPosition);
}