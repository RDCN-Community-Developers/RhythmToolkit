using System.Buffers;
using System.Text;
using System.Text.Json;

namespace RhythmBase.Global.Serialization;

/// <summary>
/// An <see cref="IJsonDataSource"/> backed by a <see cref="JsonDocument"/>.
/// </summary>
public class JsonDocumentDataSource : IJsonDataSource
{
    private readonly JsonDocument jsonDocument;

    /// <summary>
    /// Initializes a new instance of <see cref="JsonDocumentDataSource"/> from the specified document.
    /// </summary>
    /// <param name="jsonDocument">The JSON document to read from.</param>
    public JsonDocumentDataSource(JsonDocument jsonDocument)
    {
        this.jsonDocument = jsonDocument;
    }

    /// <inheritdoc/>
    public ReadOnlySequence<byte> GetSequence()
        => new(Encoding.UTF8.GetBytes(jsonDocument.RootElement.GetRawText()));

    /// <inheritdoc/>
    public ValueTask<ReadOnlySequence<byte>> GetSequenceAsync(CancellationToken cancellationToken = default)
        => new(GetSequence());

    /// <inheritdoc/>
    public long MapToInputPosition(long processedPosition) => -1;
}
