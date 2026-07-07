using System.Buffers;

namespace RhythmBase.Global.Serialization;

/// <summary>
/// An <see cref="IJsonDataSource"/> backed by a pre-loaded <see cref="ReadOnlyMemory{T}"/> of UTF-8 bytes.
/// </summary>
public class ReadOnlyMemoryDataSource : IJsonDataSource
{
    private readonly ReadOnlyMemory<byte> jsonData;

    /// <summary>
    /// Initializes a new instance of <see cref="ReadOnlyMemoryDataSource"/> from the specified byte memory.
    /// </summary>
    /// <param name="jsonData">The UTF-8 encoded JSON data.</param>
    public ReadOnlyMemoryDataSource(ReadOnlyMemory<byte> jsonData)
    {
        this.jsonData = jsonData;
    }

    /// <inheritdoc/>
    public ReadOnlySequence<byte> GetSequence() => new(jsonData);

    /// <inheritdoc/>
    public ValueTask<ReadOnlySequence<byte>> GetSequenceAsync(CancellationToken cancellationToken = default)
        => new(new ReadOnlySequence<byte>(jsonData));

    /// <inheritdoc/>
    public long MapToInputPosition(long processedPosition) => processedPosition;
}
