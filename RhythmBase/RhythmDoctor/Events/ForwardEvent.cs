using System.Text.Json;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Components;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents a custom event that forwards data and metadata, allowing for dynamic event types and extensible
/// properties.
/// </summary>
[RDJsonObjectNotSerializable]
public class ForwardEvent : BaseEvent, IForwardEvent
{
	/// <inheritdoc/>
	public override EventType Type => EventType.ForwardEvent;

	/// <inheritdoc/>
	public override Tab Tab => Tab.Unknown;

	///<inheritdoc/>
	public string ActualType
	{
		get => _extraData.TryGetValue("type", out JsonElement typeElement) && typeElement.ValueKind == JsonValueKind.String ?
				typeElement.GetString() ?? "" : "";
		set => _extraData["type"] = JsonDocument.Parse(JsonSerializer.Serialize(value)).RootElement;
	}
	/// <summary>
	/// Gets the collection of additional data associated with the object.
	/// </summary>
	protected Dictionary<string, JsonElement> ExtraData { get => _extraData; }
	/// <summary>
	/// Initializes a new instance of the <see cref="ForwardEvent"/> class.
	/// </summary>
	public ForwardEvent() { }
	/// <summary>
	/// Initializes a new instance of the <see cref="ForwardEvent"/> class using the specified JSON document.
	/// </summary>
	public ForwardEvent(JsonDocument data)
	{
		_extraData = data.Deserialize<Dictionary<string, JsonElement>>() ?? [];
		base.Beat =
			_extraData.TryGetValue("bar".ToLowerCamelCase(), out JsonElement barElement) && barElement.ValueKind == JsonValueKind.Number ?
			_extraData.TryGetValue("beat".ToLowerCamelCase(), out JsonElement beatElement) && beatElement.ValueKind == JsonValueKind.Number ?
			new(barElement.GetInt32(), beatElement.GetSingle()) : new(barElement.GetInt32(), 1) : default;
		Tag = _extraData.TryGetValue("tag".ToLowerCamelCase(), out JsonElement tagElement) && tagElement.ValueKind == JsonValueKind.String ?
			tagElement.GetString() ?? "" : "";
		Active = (!_extraData.TryGetValue("active".ToLowerCamelCase(), out JsonElement activeElement) || activeElement.ValueKind != JsonValueKind.True) && activeElement.ValueKind != JsonValueKind.False || activeElement.GetBoolean();
		RunTag = (_extraData.TryGetValue("runTag".ToLowerCamelCase(), out JsonElement runTagElement) && runTagElement.ValueKind == JsonValueKind.True || runTagElement.ValueKind == JsonValueKind.False) && runTagElement.GetBoolean();
		Condition = _extraData.TryGetValue("condition".ToLowerCamelCase(), out JsonElement conditionElement) && conditionElement.ValueKind == JsonValueKind.String ?
			Condition.Deserialize(conditionElement.GetString() ?? "") : new();
		base.Y = _extraData.TryGetValue("y".ToLowerCamelCase(), out JsonElement yElement) && yElement.ValueKind == JsonValueKind.Number ?
			yElement.GetInt32() : 0;
		_extraData["type"] = data.RootElement.GetProperty("type");
		_extraData.Remove("bar");
		_extraData.Remove("beat");
		_extraData.Remove("tag");
		_extraData.Remove("active");
		_extraData.Remove("runTag");
		_extraData.Remove("condition");
		_extraData.Remove("y");
	}
	/// <inheritdoc/>
	public override string ToString() => $"{Beat} *{ActualType}";
}
