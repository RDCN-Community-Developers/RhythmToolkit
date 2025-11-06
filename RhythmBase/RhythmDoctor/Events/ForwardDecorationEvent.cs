using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
using System.Text.Json;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <inheritdoc />
	[RDJsonObjectNotSerializable]
	public class ForwardDecorationEvent : BaseDecorationAction, IForwardEvent
	{
		/// <inheritdoc />
		public override EventType Type => EventType.ForwardDecorationEvent;

		/// <summary>
		/// Gets the actual type of the decoration event.
		/// </summary>
		public string ActualType
		{
			get => _extraData.TryGetValue("type", out JsonElement typeElement) && typeElement.ValueKind == JsonValueKind.String ?
				typeElement.GetString() ?? "" : "";
			set => _extraData["type"] = JsonSerializer.SerializeToElement(value);
		}
		/// <inheritdoc />
		public override Tabs Tab => Tabs.Decorations;

		/// <summary>
		/// Gets a dictionary containing additional data not explicitly modeled by the class.
		/// </summary>
		/// <remarks>This property provides access to extra data that may be included in the source but is not
		/// directly represented by other properties. The dictionary is read-only and reflects the internal state of the extra
		/// data.</remarks>
		protected Dictionary<string, JsonElement> ExtraData => _extraData;
		/// <summary>
		/// Initializes a new instance of the <see cref="ForwardDecorationEvent"/> class.
		/// </summary>
		public ForwardDecorationEvent() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="ForwardDecorationEvent"/> class with the specified data.
		/// </summary>
		/// <param name="data">The JSON data for the event.</param>
		public ForwardDecorationEvent(JsonDocument data)
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
				Condition.Deserialize( conditionElement.GetString() ?? "") : new();
			_decoId = _extraData.TryGetValue("target".ToLowerCamelCase(), out JsonElement targetElement) && targetElement.ValueKind == JsonValueKind.String ?
				targetElement.GetString() ?? "" : "";
			_extraData.Remove("bar");
			_extraData.Remove("beat");
			_extraData.Remove("tag");
			_extraData.Remove("active");
			_extraData.Remove("runTag");
			_extraData.Remove("condition");
			_extraData.Remove("target");
		}
		/// <inheritdoc />
		public override string ToString() => $"{Beat} *{ActualType}";
		/// <inheritdoc />
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type) => TryConvert(ref value, ref type, new LevelReadOrWriteSettings());
		/// <inheritdoc />
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type, LevelReadOrWriteSettings settings) => TryConvert(ref value, ref type, settings);
	}
}
