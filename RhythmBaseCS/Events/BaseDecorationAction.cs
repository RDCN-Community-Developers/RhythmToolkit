using System;
using Newtonsoft.Json;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public abstract class BaseDecorationAction : BaseEvent
	{

		[JsonIgnore]
		public DecorationEventCollection Parent => _parent;
		[JsonIgnore]
		public override int Y { get => base.Y; set => base.Y = value; }
		public virtual string Target => (Parent == null) ? "" : Parent.Id;
		/// <summary>
		/// Clone this event and its basic properties. Clone will be added to the level.
		/// </summary>
		/// <typeparam name="TEvent">Type that will be generated.</typeparam>
		/// <returns></returns>
		public new TEvent Clone<TEvent>() where TEvent : BaseDecorationAction, new()
		{
			TEvent Temp = base.Clone<TEvent>();
			Temp._parent = Parent;
			return Temp;
		}
		internal TEvent Clone<TEvent>(DecorationEventCollection decoration) where TEvent : BaseDecorationAction, new()
		{
			TEvent Temp = base.Clone<TEvent>(decoration.Parent);
			Temp._parent = decoration;
			return Temp;
		}
		[JsonIgnore]
		public SingleRoom Room => Parent == null ? SingleRoom.Default : Parent.Room;
		internal DecorationEventCollection _parent;
	}
}
