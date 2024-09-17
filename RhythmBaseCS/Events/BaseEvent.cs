using Newtonsoft.Json;
using RhythmBase.Components;
using static RhythmBase.Utils.Utils;
namespace RhythmBase.Events
{
	public abstract class BaseEvent : IBaseEvent
	{
		protected BaseEvent()
		{
			_beat = new Beat(1f);
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
		public virtual Beat Beat
		{
			get => _beat;
			set
			{
				if (_beat.BaseLevel == null)
					_beat = value.BaseLevel == null ? value : value.WithoutBinding();
				else
				{
					value = new Beat(_beat.BaseLevel.Calculator, value);
					_beat.BaseLevel.Remove(this);
					value.BaseLevel.Add(this);
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
		public string Tag { get; set; }
		/// <summary>
		/// Event conditions.
		/// </summary>
		[JsonProperty("if", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Condition Condition { get; set; }
		public bool Active { get; set; }
		/// <summary>
		/// Clone this event and its basic properties.
		/// If it is of the same type as the source event, then it will be cloned.
		/// </summary>
		/// <typeparam name="TEvent">Type that will be generated.</typeparam>
		/// <returns></returns>
		public virtual TEvent Clone<TEvent>() where TEvent : IBaseEvent, new()
		{
			if (ConvertToEnum<TEvent>() == Type)
			{
				TEvent e = (TEvent)MemberwiseClone();
				((BaseEvent)(object)e)._beat = Beat.WithoutBinding();
				return e;
			}
			TEvent temp = new()
			{
				Beat = Beat.WithoutBinding(),
				Y = Y,
				Tag = Tag,
				Condition = Condition,
				Active = Active
			};
			if (Condition != null)
				foreach (var item in Condition.ConditionLists)
					temp.Condition.ConditionLists.Add(item);
			return temp;
		}
		internal virtual TEvent Clone<TEvent>(RDLevel level) where TEvent : IBaseEvent, new()
		{
			TEvent temp = new()
			{
				Beat = Beat.WithoutBinding(),
				Y = Y,
				Tag = Tag,
				Condition = Condition,
				Active = Active
			};
			if (Condition != null)
				foreach (var item in Condition.ConditionLists)
					temp.Condition.ConditionLists.Add(item);
			return temp;
		}
		public override string ToString() => string.Format("{0} {1}", Beat, Type);
		internal Beat _beat;
	}
}
