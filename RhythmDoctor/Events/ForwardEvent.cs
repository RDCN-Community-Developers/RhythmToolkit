using System.Text.Json;
using System.Text.Json.Serialization;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Utils;
using System.Collections.Generic;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a custom event in the rhythm base system.
	/// </summary>
	[RDJsonObjectNotSerializable]
	public class ForwardEvent : BaseEvent, IForwardEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.ForwardEvent;

		/// <inheritdoc/>
		public override Tabs Tab { get; } = Tabs.Unknown;
		/// <summary>
		/// Gets or sets the actual type of the custom event.
		/// </summary>
		public string ActureType
		{
			get => _extraData.TryGetValue("type", out JsonElement typeElement) && typeElement.ValueKind == JsonValueKind.String ?
					typeElement.GetString() ?? "" : "";
			set => _extraData["type"] = JsonDocument.Parse(JsonSerializer.Serialize(value)).RootElement;
		}
		protected Dictionary<string, JsonElement> ExtraData { get => _extraData; }
		/// <summary>
		/// Initializes a new instance of the <see cref="ForwardEvent"/> class.
		/// </summary>
		public ForwardEvent() { }
		public ForwardEvent(JsonDocument data)
		{
			_extraData = data.Deserialize<Dictionary<string, JsonElement>>() ?? [];
			Beat =
				_extraData.TryGetValue("bar".ToLowerCamelCase(), out JsonElement barElement) && barElement.ValueKind == JsonValueKind.Number ?
				_extraData.TryGetValue("beat".ToLowerCamelCase(), out JsonElement beatElement) && beatElement.ValueKind == JsonValueKind.Number ?
				new(barElement.GetInt32(), beatElement.GetSingle()) : new(barElement.GetInt32(), 1) : default;
			Tag = _extraData.TryGetValue("tag".ToLowerCamelCase(), out JsonElement tagElement) && tagElement.ValueKind == JsonValueKind.String ?
				tagElement.GetString() ?? "" : "";
			Active = (!_extraData.TryGetValue("active".ToLowerCamelCase(), out JsonElement activeElement) || activeElement.ValueKind != JsonValueKind.True) && activeElement.ValueKind != JsonValueKind.False || activeElement.GetBoolean();
			RunTag = (_extraData.TryGetValue("runTag".ToLowerCamelCase(), out JsonElement runTagElement) && runTagElement.ValueKind == JsonValueKind.True || runTagElement.ValueKind == JsonValueKind.False) && runTagElement.GetBoolean();
			Condition = _extraData.TryGetValue("condition".ToLowerCamelCase(), out JsonElement conditionElement) && conditionElement.ValueKind == JsonValueKind.String ?
				Condition.Deserialize(conditionElement.GetString() ?? "") : null;
			Y = _extraData.TryGetValue("y".ToLowerCamelCase(), out JsonElement yElement) && yElement.ValueKind == JsonValueKind.Number ?
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
		public override string ToString() => $"{Beat} *{ActureType}";
	}
}
