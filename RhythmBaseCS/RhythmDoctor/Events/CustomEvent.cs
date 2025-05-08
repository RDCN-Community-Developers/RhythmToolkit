using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Utils;
namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents a custom event in the rhythm base system.
	/// </summary>
	public class CustomEvent : BaseEvent
	{
		/// <inheritdoc/>
		[JsonIgnore]
		public override EventType Type => EventType.CustomEvent;
		/// <summary>
		/// Gets or sets the actual type of the custom event.
		/// </summary>
		[JsonIgnore]
		public string ActureType
		{
			get => Data["Type".ToLowerCamelCase()]?.ToObject<string>() ?? "";
			init => Data["Type".ToLowerCamelCase()] = value;
		}
		/// <inheritdoc/>
		public override Tabs Tab { get; }
		/// <inheritdoc/>
		public override int Y
		{
			get => (int)(Data["Y".ToLowerCamelCase()] ?? 0);
			set => Data["Y".ToLowerCamelCase()] = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomEvent"/> class.
		/// </summary>
		public CustomEvent()
		{
			Data = [];
			Tab = Tabs.Unknown;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomEvent"/> class with the specified data.
		/// </summary>
		/// <param name="data">The data for the custom event.</param>
		public CustomEvent(JObject data)
		{
			Data = data ?? [];
			Tab = Tabs.Unknown;
			Beat = new RDBeat(Data["bar"]?.ToObject<uint>() ?? 1, Data["beat"]?.ToObject<float>() ?? 1f);
			Tag = Data["tag"]?.ToObject<string>() ?? "";
			Condition = Data["condition"] == null
				? null
				: Condition.Load(Data["condition"]?.ToObject<string>() ?? "");
			Active = Data["active"]?.ToObject<bool>() ?? true;
		}
		/// <inheritdoc/>
		public override string ToString() => $"{Beat} *{ActureType}";
		/// <summary>
		/// Tries to convert the current custom event to a base event.
		/// </summary>
		/// <param name="value">The base event to convert to.</param>
		/// <param name="type">The type of the event.</param>
		/// <returns>True if the conversion was successful; otherwise, false.</returns>
		public virtual bool TryConvert(ref BaseEvent? value, ref EventType? type) => TryConvert(ref value, ref type, new LevelReadOrWriteSettings());
		/// <summary>
		/// Tries to convert the current custom event to a base event with the specified settings.
		/// </summary>
		/// <param name="value">The base event to convert to.</param>
		/// <param name="type">The type of the event.</param>
		/// <param name="settings">The settings for reading or writing the level.</param>
		/// <returns>True if the conversion was successful; otherwise, false.</returns>
		public virtual bool TryConvert(ref BaseEvent? value, ref EventType? type, LevelReadOrWriteSettings settings)
		{
			JsonSerializer serializer = JsonSerializer.Create(_beat.BaseLevel?.GetSerializer(settings));
			Type eventType = EventTypeUtils.ToType(Data["type"]?.ToObject<string>() ?? "");
			bool TryConvert;
			if (eventType == null)
			{
				if (Data["target"] != null)
					value = Data.ToObject<CustomDecorationEvent>(serializer);
				else if (Data["row"] != null)
					value = Data.ToObject<CustomRowEvent>(serializer);
				else
					value = Data.ToObject<CustomEvent>(serializer);
				type = null;
				TryConvert = value is not null;
			}
			else
			{
				value = (BaseEvent?)Data.ToObject(eventType, serializer);
				type = value?.Type;
				TryConvert = true;
			}
			return TryConvert;
		}
		/// <summary>
		/// Gets or sets the data for the custom event.
		/// </summary>
		[JsonIgnore]
		public JObject Data { get; set; }
	}
}
