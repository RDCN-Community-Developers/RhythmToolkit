using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Extensions;
using System.Text.Json;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a custom row event in the rhythm base.
	/// </summary>
	[RDJsonObjectNotSerializable]
	public class CustomRowEvent : BaseRowAction, ICustomEvent
	{
		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public override EventType Type { get; }
		/// <summary>
		/// Gets the actual type of the event from the data.
		/// </summary>
		public string ActureType
		{
			get => ExtraData["Type".ToLowerCamelCase()]?.ToString() ?? "";
			set => ExtraData["Type".ToLowerCamelCase()] = value;
		}

		/// <summary>
		/// Gets the tab associated with the event.
		/// </summary>
		public override Tabs Tab { get; }
		public int Bar { get; set; }
		public float BeatValue { get; set; }
		public string? ConditionRaw { get; set; }
		Dictionary<string, JsonElement> ICustomEvent.ExtraData { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomRowEvent"/> class.
		/// </summary>
		public CustomRowEvent()
		{
			ExtraData = [];
			Type = EventType.CustomRowEvent;
			Tab = Tabs.Rows;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomRowEvent"/> class with the specified data.
		/// </summary>
		/// <param name="data">The data for the event.</param>
		public CustomRowEvent(JObject data)
		{
			Type = EventType.CustomRowEvent;
			Tab = Tabs.Rows;
			ExtraData = data;
			Beat = new RDBeat(ExtraData["bar"]?.ToObject<int>() ?? 1, ExtraData["beat"]?.ToObject<float>() ?? 1f);
			Tag = ExtraData["tag"]?.ToObject<string>() ?? "";
			Condition = ExtraData["condition"] == null
				? null
				: Condition.Load(ExtraData["condition"]?.ToObject<string>() ?? "");
			Active = ExtraData["active"]?.ToObject<bool>() ?? true;
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => $"{Beat} *{ActureType}";
		/// <summary>
		/// Tries to convert the current event to a base event.
		/// </summary>
		/// <param name="value">The base event to convert to.</param>
		/// <param name="type">The type of the event.</param>
		/// <returns>true if the conversion was successful; otherwise, false.</returns>
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type) => TryConvert(ref value, ref type, new LevelReadOrWriteSettings());
		/// <summary>
		/// Tries to convert the current event to a base event with the specified settings.
		/// </summary>
		/// <param name="value">The base event to convert to.</param>
		/// <param name="type">The type of the event.</param>
		/// <param name="settings">The settings for the conversion.</param>
		/// <returns>true if the conversion was successful; otherwise, false.</returns>
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type, LevelReadOrWriteSettings settings) => TryConvert(ref value, ref type, settings);
		///// <summary>
		///// Implicitly converts a <see cref="CustomRowEvent"/> to a <see cref="CustomEvent"/>.
		///// </summary>
		///// <param name="e">The custom row event to convert.</param>
		//public static implicit operator CustomEvent(CustomRowEvent e) => new(e.ExtraData);
		///// <summary>
		///// Explicitly converts a <see cref="CustomEvent"/> to a <see cref="CustomRowEvent"/>.
		///// </summary>
		///// <param name="e">The custom event to convert.</param>
		///// <returns>The converted custom row event.</returns>
		///// <exception cref="RhythmBaseException">Thrown when the row field is missing from the data.</exception>
		//public static explicit operator CustomRowEvent(CustomEvent e)
		//{
		//	return e.ExtraData["row"] != null
		//		? new CustomRowEvent(e.Data)
		//		: throw new RhythmBaseException("The row field is missing from the field contained in this object.");
		//}
		/// <summary>
		/// Gets or sets the data for the event.
		/// </summary>
		public JObject ExtraData;
	}
}
