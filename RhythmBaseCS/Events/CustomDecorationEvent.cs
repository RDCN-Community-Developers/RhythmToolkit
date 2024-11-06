using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Components;
using RhythmBase.Exceptions;
using RhythmBase.Extensions;
using RhythmBase.Settings;
namespace RhythmBase.Events
{
	/// <inheritdoc />
	public class CustomDecorationEvent : BaseDecorationAction
	{
		/// <inheritdoc />
		public override EventType Type { get; }

		/// <summary>
		/// Gets the actual type of the decoration event.
		/// </summary>
		[JsonIgnore]
		public string ActureType
		{
			get
			{
				return Data["Type".ToLowerCamelCase()]?.ToString() ?? "";
			}
		}

		/// <inheritdoc />
		public override Tabs Tab { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomDecorationEvent"/> class.
		/// </summary>
		public CustomDecorationEvent()
		{
			Data = [];
			Type = EventType.CustomDecorationEvent;
			Tab = Tabs.Decorations;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomDecorationEvent"/> class with the specified data.
		/// </summary>
		/// <param name="data">The JSON data for the event.</param>
		public CustomDecorationEvent(JObject data)
		{
			Data = data ?? [];
			Type = EventType.CustomDecorationEvent;
			Tab = Tabs.Decorations;
			Beat = new RDBeat(Data["bar"]?.ToObject<uint>() ?? 1, (Data["beat"]?.ToObject<float>() ?? 1f));
			Tag = Data["tag"]?.ToObject<string>() ?? "";
			Condition = Data["condition"] != null
				? Data["condition"] is null ? null : Condition.Load(Data["condition"]!.ToObject<string>() ?? "")
				: null;
			Active = Data["active"]?.ToObject<bool>() ?? true;
		}

		/// <inheritdoc />
		public override string ToString() => $"{Beat} *{ActureType}";

		/// <inheritdoc />
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type) => TryConvert(ref value, ref type, new LevelReadOrWriteSettings());

		/// <inheritdoc />
		public virtual bool TryConvert(ref BaseEvent value, ref EventType? type, LevelReadOrWriteSettings settings) => TryConvert(ref value, ref type, settings);

		/// <summary>
		/// Explicit conversion from <see cref="CustomEvent"/> to <see cref="CustomDecorationEvent"/>.
		/// </summary>
		/// <param name="e">The <see cref="CustomEvent"/> instance.</param>
		/// <exception cref="RhythmBaseException">Thrown when the row field is missing from the data.</exception>
		public static explicit operator CustomDecorationEvent(CustomEvent e)
		{
			return e.Data["row"] != null
				? new CustomDecorationEvent(e.Data)
				: throw new RhythmBaseException("The row field is missing from the field contained in this object.");
		}

		/// <summary>
		/// Gets or sets the JSON data for the event.
		/// </summary>
		public JObject Data;
	}
}
