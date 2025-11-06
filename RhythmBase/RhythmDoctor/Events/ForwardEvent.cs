using System.Text.Json;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Components;

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
		public override Tabs Tab => Tabs.Unknown;

		/// <summary>
		/// Gets or sets the actual type of the custom event.
		/// </summary>
		public string ActualType
		{
			get => _extraData.TryGetValue("type", out JsonElement typeElement) && typeElement.ValueKind == JsonValueKind.String ?
					typeElement.GetString() ?? "" : "";
			set => _extraData["type"] = JsonDocument.Parse(JsonSerializer.Serialize(value)).RootElement;
		}
		/// <summary>
		/// Gets the collection of additional data associated with the object.
		/// </summary>
		/// <remarks>The <see cref="ExtraData"/> property is typically used to store and retrieve dynamic or
		/// unstructured data  that may be included in the object. Callers should ensure that the keys and values are handled
		/// appropriately  based on the expected data format.</remarks>
		protected Dictionary<string, JsonElement> ExtraData { get => _extraData; }
		/// <summary>
		/// Initializes a new instance of the <see cref="ForwardEvent"/> class.
		/// </summary>
		public ForwardEvent() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="ForwardEvent"/> class using the specified JSON document.
		/// </summary>
		/// <remarks>This constructor deserializes the provided JSON document into a dictionary of key-value pairs and
		/// extracts specific properties to initialize the instance. The following properties are expected: <list
		/// type="bullet"> <item> <description><c>bar</c> (number): Represents the bar value. Used in combination with
		/// <c>beat</c> to calculate the beat.</description> </item> <item> <description><c>beat</c> (number): Represents the
		/// beat value. Defaults to 1 if not provided.</description> </item> <item> <description><c>tag</c> (string):
		/// Represents the tag associated with the event. Defaults to an empty string if not provided.</description> </item>
		/// <item> <description><c>active</c> (boolean): Indicates whether the event is active. Defaults to <see
		/// langword="false"/> if not provided.</description> </item> <item> <description><c>runTag</c> (boolean): Indicates
		/// whether the run tag is enabled. Defaults to <see langword="false"/> if not provided.</description> </item> <item>
		/// <description><c>condition</c> (string): Represents a serialized condition. Deserialized into a <c>Condition</c>
		/// object if provided.</description> </item> <item> <description><c>y</c> (number): Represents the Y-coordinate
		/// value. Defaults to 0 if not provided.</description> </item> </list> Any unrecognized properties in the JSON
		/// document are retained in the internal dictionary for additional processing.</remarks>
		/// <param name="data">A <see cref="JsonDocument"/> containing the event data. The document must include specific properties such as
		/// "bar", "beat", "tag", "active", "runTag", "condition", and "y" to populate the corresponding fields.</param>
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
}
