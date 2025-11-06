using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
using System.Text.Json;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a custom row event in the rhythm base.
	/// </summary>
	[RDJsonObjectNotSerializable]
	public class ForwardRowEvent : BaseRowAction, IForwardEvent
	{
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type => EventType.ForwardRowEvent;

		/// <summary>
		/// Gets the actual type of the event from the data.
		/// </summary>
		public string ActualType
		{
			get => _extraData.TryGetValue("type", out JsonElement typeElement) && typeElement.ValueKind == JsonValueKind.String ?
					typeElement.GetString() ?? "" : "";
			set => _extraData["type"] = JsonDocument.Parse(JsonSerializer.Serialize(value)).RootElement;
		}

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab => Tabs.Rows;

		/// <summary>
		/// Gets a dictionary containing additional data not explicitly modeled by the class.
		/// </summary>
		/// <remarks>This property provides access to extra data that may be included in the input but is not directly
		/// represented by other properties. It is useful for handling extensible or dynamic data structures.</remarks>
		protected Dictionary<string, JsonElement> ExtraData { get => _extraData; }
		/// <summary>
		/// Initializes a new instance of the <see cref="ForwardRowEvent"/> class.
		/// </summary>
		public ForwardRowEvent()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ForwardRowEvent"/> class with the specified data.
		/// </summary>
		/// <param name="data">The data for the event.</param>
		public ForwardRowEvent(JsonDocument data)
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
			_row = _extraData.TryGetValue("row".ToLowerCamelCase(), out JsonElement rowElement) && rowElement.ValueKind == JsonValueKind.Number ?
				rowElement.GetInt32() : -1;
			_extraData.Remove("bar");
			_extraData.Remove("beat");
			_extraData.Remove("tag");
			_extraData.Remove("active");
			_extraData.Remove("runTag");
			_extraData.Remove("condition");
			_extraData.Remove("row");
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => $"{Beat} *{ActualType}";
	}
}
