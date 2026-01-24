using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Utils;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// The base class of the event.
/// All event types inherit directly or indirectly from this.
/// </summary>
public abstract record class BaseEvent : IBaseEvent
{
	///<inheritdoc/>
	public abstract EventType Type { get; }
	///<inheritdoc/>
	public abstract Tab Tab { get; }
	///<inheritdoc/>
	public virtual RDBeat Beat
	{
		get => _beat;
		set
		{
			if (!value.IsEmpty && _beat == value)
				return;
			BeatCalculator? c = _beat.BaseLevel?.Calculator;
			_beat.BaseLevel?.Remove(this);
			_beat = c == null ?
				value.WithoutLink() :
				new(c,value);
			_beat.BaseLevel?.Add(this);
		}
	}
	///<inheritdoc/>
	public virtual int Y { get; set; }
	///<inheritdoc/>
	public string Tag { get; set; } = "";
	///<inheritdoc/>
	public bool RunTag { get; set; } = false;
	///<inheritdoc/>
	public Condition Condition { get; set; } = new();
	///<inheritdoc/>
	public bool Active { get; set; } = true;
	///<inheritdoc/>
	public JsonElement this[string propertyName]
	{
		get => _extraData.TryGetValue(propertyName, out JsonElement value) ? value : default;
		set
		{
			if (value.ValueKind == JsonValueKind.Undefined)
				_extraData.Remove(propertyName);
			else
				_extraData[propertyName] = value;
		}
	}
	internal Dictionary<string, JsonElement> _extraData = [];
	/////<inheritdoc/>
	//public virtual TEvent Clone<TEvent>() where TEvent : IBaseEvent, new()
	//{
	//	if (EventTypeUtils.ToEnum<TEvent>() == Type)
	//	{
	//		TEvent e = (TEvent)MemberwiseClone();
	//		((BaseEvent)(object)e)._beat = Beat.WithoutLink();
	//		return e;
	//	}
	//	TEvent temp = new()
	//	{
	//		Beat = Beat.WithoutLink(),
	//		Y = Y,
	//		Tag = Tag,
	//		Condition = Condition,
	//		Active = Active
	//	};
	//		temp.Condition = Condition.Clone();
	//	return temp;
	//}
	/////<inheritdoc/>
	//public virtual IBaseEvent Clone()
	//{
	//	return (BaseEvent)MemberwiseClone();
	//}
	//internal virtual TEvent Clone<TEvent>(RDLevel level) where TEvent : IBaseEvent, new()
	//{
	//	TEvent temp = new()
	//	{
	//		Beat = Beat.WithoutLink(),
	//		Y = Y,
	//		Tag = Tag,
	//		Condition = Condition,
	//		Active = Active
	//	};
	//	temp.Condition = Condition.Clone();
	//	return temp;
	//}
	/// <inheritdoc/>
	public override string ToString() => $"{Beat} {Type}";
	internal RDBeat _beat = new(1f);
}
