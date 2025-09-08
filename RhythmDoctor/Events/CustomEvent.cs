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
	public class CustomEvent : BaseEvent, ICustomEvent
	{
		/// <inheritdoc/>
		public override EventType Type => EventType.CustomEvent;

		/// <inheritdoc/>
		public override Tabs Tab { get; } = Tabs.Unknown;

		/// <inheritdoc/>
		public override int Y
		{
			get => _y;
			set => _y = value;
		}

		/// <summary>
		/// Gets or sets the actual type of the custom event.
		/// </summary>
		public string ActureType
		{
			get => _type ?? "";
			set => _type = value;
		}

		// 常用字段直接属性化
		public int Bar { get; set; } = 1;
		public float BeatValue { get; set; } = 1f;
		public string Tag { get; set; } = "";
		public bool Active { get; set; } = true;
		public string? ConditionRaw { get; set; }
		[JsonIgnore]
		public Condition? Condition
		{
			get => string.IsNullOrEmpty(ConditionRaw) ? null : Condition.Load(ConditionRaw);
			set => ConditionRaw = value?.ToString();
		}

		// 其余动态字段
		[JsonExtensionData]
		public Dictionary<string, JsonElement> ExtraData { get => _extraData ??= []; set => _extraData = value; }

		// 内部字段
		private string? _type;
		private int _y;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomEvent"/> class.
		/// </summary>
		public CustomEvent() { }
		/// <inheritdoc/>
		public override string ToString() => $"{Bar}:{BeatValue} *{ActureType}";

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
			// 只用 ExtraData 作为动态数据源
			var json = JsonSerializer.Serialize(this);
			Type? eventType = EventTypeUtils.ToType(_type ?? "");
			bool TryConvertResult;
			if (eventType == null)
			{
				if (ExtraData.ContainsKey("target"))
					value = JsonSerializer.Deserialize<CustomDecorationEvent>(json);
				else if (ExtraData.ContainsKey("row"))
					value = JsonSerializer.Deserialize<CustomRowEvent>(json);
				else
					value = JsonSerializer.Deserialize<CustomEvent>(json);
				type = null;
				TryConvertResult = value is not null;
			}
			else
			{
				value = (BaseEvent?)JsonSerializer.Deserialize(json, eventType);
				type = value?.Type;
				TryConvertResult = value is not null;
			}
			return TryConvertResult;
		}
	}

	//// 高性能自定义序列化器
	//public class CustomEventConverter : JsonConverter<ICustomEvent>
	//{
	//}
}
