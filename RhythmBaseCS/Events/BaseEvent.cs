using Newtonsoft.Json;
using RhythmBase.Components;
using RhythmBase.Utils;
namespace RhythmBase.Events
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
			_beat = new RDBeat(1f);
			Active = true;
		}
		/// <summary>
		/// Event type.
		/// </summary>
		[JsonIgnore]
		public abstract EventType Type { get; }
		/// <summary>
		/// Column to which the event belongs.
		/// </summary>
		[JsonIgnore]
		public abstract Tabs Tab { get; }
		/// <summary>
		/// The beat of the event.
		/// </summary>
		[JsonIgnore]
		public virtual RDBeat Beat
		{
			get => _beat;
			set
			{
				if (_beat.BaseLevel == null)
					_beat = value.BaseLevel == null ? value : value.WithoutLink();
				else
				{
					value = new RDBeat(_beat.BaseLevel.Calculator, value);
					_beat.BaseLevel.Remove(this);
					value.BaseLevel?.Add(this);
					_beat = value;
				}
			}
		}
		/// <summary>
		/// The number of rows this event is on.
		/// </summary>
		public virtual int Y { get; set; }
		/// <summary>
		/// Event tag.
		/// </summary>
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Tag { get; set; } = "";
		/// <summary>
		/// Event conditions.
		/// </summary>
		[JsonProperty("if", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Condition? Condition { get; set; }
		/// <summary>
		/// Indicates whether this event is activated.
		/// </summary>
		public bool Active { get; set; }
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
			if (Condition != null)
				temp.Condition = new()
				{
					ConditionLists = [.. Condition.ConditionLists]
				};
			return temp;
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
			if (Condition != null)
				temp.Condition = new()
				{
					ConditionLists = [.. Condition.ConditionLists]
				};
			return temp;
		}
		/// <inheritdoc/>
		public override string ToString() => $"{Beat} {Type}";
		internal RDBeat _beat;
	}
}
