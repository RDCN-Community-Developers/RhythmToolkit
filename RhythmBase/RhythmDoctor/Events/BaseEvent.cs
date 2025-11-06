using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Utils;
using System.Text.Json;

namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// The base class of the event.
	/// All event types inherit directly or indirectly from this.
	/// </summary>
	public abstract class BaseEvent : IBaseEvent
	{
		/// <summary>
		/// The base class of the event.
		/// All event types inherit directly or indirectly from this.
		/// </summary>
		protected BaseEvent()
		{
			_beat = new(1f);
			Active = true;
			Condition = new();
		}
		/// <summary>
		/// Event type.
		/// </summary>
		public abstract EventType Type { get; }
		/// <summary>
		/// Column to which the event belongs.
		/// </summary>
		public abstract Tabs Tab { get; }
		/// <summary>
		/// The beat of the event.
		/// </summary>
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
		/// <summary>
		/// The number of rows this event is on.
		/// </summary>
		public virtual int Y { get; set; }
		/// <summary>
		/// Event tag.
		/// </summary>
		public string Tag { get; set; } = "";
		/// <summary>
		/// Gets or sets a value indicating whether the tag should be executed.
		/// </summary>
		public bool RunTag { get; set; } = false;
		/// <summary>
		/// Event conditions.
		/// </summary>
		public Condition Condition { get; set; }
		/// <summary>
		/// Indicates whether this event is activated.
		/// </summary>
		public bool Active { get; set; }
		/// <summary>
		/// Gets or sets the <see cref="JsonElement"/> associated with the specified property name.
		/// </summary>
		/// <remarks>When setting a value, if the <see cref="JsonElement"/> has a <see
		/// cref="JsonValueKind.Undefined"/>, the property is removed from the collection. Otherwise, the property is added or
		/// updated with the specified value.</remarks>
		/// <param name="propertyName">The name of the property whose value is to be retrieved or set. This parameter is case-sensitive.</param>
		/// <returns></returns>
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
		/// <summary>
		/// Clone this event and its basic properties.
		/// If it is of the same type as the source event, then it will be cloned.
		/// </summary>
		/// <typeparam name="TEvent">Type that will be generated.</typeparam>
		/// <returns>A new instance with the same base properties as the source instance.</returns>
		public virtual TEvent Clone<TEvent>() where TEvent : IBaseEvent, new()
		{
			if (EventTypeUtils.ToEnum<TEvent>() == Type)
			{
				TEvent e = (TEvent)MemberwiseClone();
				((BaseEvent)(object)e)._beat = Beat.WithoutLink();
				return e;
			}
			TEvent temp = new()
			{
				Beat = Beat.WithoutLink(),
				Y = Y,
				Tag = Tag,
				Condition = Condition,
				Active = Active
			};
				temp.Condition = Condition.Clone();
			return temp;
		}
		/// <summary>
		/// Creates a shallow copy of the current <see cref="BaseEvent"/> instance.
		/// </summary>
		/// <remarks>The cloned object will have the same values for all fields as the original object.  Changes to
		/// reference-type fields in the clone will affect the original object, and vice versa.</remarks>
		/// <returns>A new <see cref="BaseEvent"/> object that is a shallow copy of the current instance.</returns>
		public virtual IBaseEvent Clone()
		{
			return (BaseEvent)MemberwiseClone();
		}
		internal virtual TEvent Clone<TEvent>(RDLevel level) where TEvent : IBaseEvent, new()
		{
			TEvent temp = new()
			{
				Beat = Beat.WithoutLink(),
				Y = Y,
				Tag = Tag,
				Condition = Condition,
				Active = Active
			};
			temp.Condition = Condition.Clone();
			return temp;
		}
		/// <inheritdoc/>
		public override string ToString() => $"{Beat} {Type}";
		internal RDBeat _beat;
	}
}
