using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Components;
using System.Text.Json;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <inheritdoc />
	[RDJsonObjectNotSerializable]
	public class CustomDecorationEvent : BaseDecorationAction, ICustomEvent
	{
		/// <inheritdoc />
		public override EventType Type { get; }
		/// <summary>
		/// Gets the actual type of the decoration event.
		/// </summary>
		public string ActureType
		{
			get
			{
				return ExtraData["Type".ToLowerCamelCase()]?.ToString() ?? "";
			}
			set
			{
				ExtraData["Type".ToLowerCamelCase()] = value;
			}
		}
		/// <inheritdoc />
		public override Tabs Tab { get; }
		public int Bar { get ; set; }
		public float BeatValue { get ; set ; }
		public string? ConditionRaw { get; set; }
		Dictionary<string, JsonElement> ICustomEvent.ExtraData { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomDecorationEvent"/> class.
		/// </summary>
		public CustomDecorationEvent()
		{
			ExtraData = [];
			Type = EventType.CustomDecorationEvent;
			Tab = Tabs.Decorations;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomDecorationEvent"/> class with the specified data.
		/// </summary>
		/// <param name="data">The JSON data for the event.</param>
		public CustomDecorationEvent(JObject data)
		{
			ExtraData = data ?? [];
			Type = EventType.CustomDecorationEvent;
			Tab = Tabs.Decorations;
			Beat = new RDBeat(ExtraData["bar"]?.ToObject<int>() ?? 1, ExtraData["beat"]?.ToObject<float>() ?? 1f);
			Tag = ExtraData["tag"]?.ToObject<string>() ?? "";
			Condition = ExtraData["condition"] != null
				? ExtraData["condition"] is null ? null : Condition.Load(ExtraData["condition"]!.ToObject<string>() ?? "")
				: null;
			Active = ExtraData["active"]?.ToObject<bool>() ?? true;
		}
		/// <inheritdoc />
		public override string ToString() => $"{Beat} *{ActureType}";
		/// <inheritdoc />
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type) => TryConvert(ref value, ref type, new LevelReadOrWriteSettings());
		/// <inheritdoc />
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type, LevelReadOrWriteSettings settings) => TryConvert(ref value, ref type, settings);
		///// <summary>
		///// Explicit conversion from <see cref="CustomEvent"/> to <see cref="CustomDecorationEvent"/>.
		///// </summary>
		///// <param name="e">The <see cref="CustomEvent"/> instance.</param>
		///// <exception cref="RhythmBaseException">Thrown when the row field is missing from the data.</exception>
		//public static explicit operator CustomDecorationEvent(CustomEvent e)
		//{
		//	return e.ExtraData["row"] != null
		//		? new CustomDecorationEvent(e.ExtraData)
		//		: throw new RhythmBaseException("The row field is missing from the field contained in this object.");
		//}
		/// <summary>
		/// Gets or sets the JSON data for the event.
		/// </summary>
		public JObject ExtraData;
	}
}
