using System;
using Newtonsoft.Json;
using RhythmBase.Components;

namespace RhythmBase.Events
{

	public abstract class BaseDecorationAction : BaseEvent
	{

		[JsonIgnore]
		public DecorationEventCollection Parent
		{
			get
			{
				return _parent;
			}
		}


		public virtual string Target
		{
			get
			{
				return (Parent == null) ? "" : Parent.Id;
			}
		}

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
		public SingleRoom Room
		{
			get
			{
				bool flag = Parent == null;
				SingleRoom Room;
				if (flag)
				{
					Room = SingleRoom.Default;
				}
				else
				{
					Room = Parent.Room;
				}
				return Room;
			}
		}


		internal DecorationEventCollection _parent;
	}
}
