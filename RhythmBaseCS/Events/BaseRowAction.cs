﻿using System;
using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	public abstract class BaseRowAction : BaseEvent
	{
		[JsonIgnore]
		public RowEventCollection Parent
		{
			get
			{
				return _parent;
			}
			internal set
			{
				if (_parent != null)
				{
					_parent.Remove(this);
					if (value != null)
					{
						value.Add(this);
					}
				}
				_parent = value;
			}
		}

		[JsonIgnore]
		public SingleRoom Room
		{
			get
			{
				bool flag = _parent == null;
				SingleRoom Room;
				if (flag)
				{
					Room = SingleRoom.Default;
				}
				else
				{
					Room = Parent.Rooms;
				}
				return Room;
			}
		}
		/// <inheritdoc/>
		[JsonIgnore]
		public Beat Beat
		{
			get => _beat;
			set
			{
				if (_beat.BaseLevel == null)
					_beat = value.BaseLevel == null ? value : value.WithoutBinding();
				else _beat = new(_beat.BaseLevel.Calculator, value);
			}
		}
		[JsonIgnore]
		[Obsolete("This function is obsolete and may be removed in the next release. Use Index instead.")]
		public int Row { get; }

		[JsonProperty("row", DefaultValueHandling = DefaultValueHandling.Include)]
		public int Index
		{
			get
			{
				RowEventCollection parent = Parent;
				return (int)((parent != null) ? parent.Index : -1);
			}
		}
		/// <summary>
		/// Clone this event and its basic properties. Clone will be added to the level.
		/// </summary>
		/// <typeparam name="TEvent">Type that will be generated.</typeparam>
		/// <returns></returns>
		public new TEvent Clone<TEvent>() where TEvent : BaseRowAction, new()
		{
			TEvent Temp = base.Clone<TEvent>();
			Temp.Parent = Parent;
			return Temp;
		}

		internal TEvent Clone<TEvent>(RowEventCollection row) where TEvent : BaseRowAction, new()
		{
			TEvent Temp = base.Clone<TEvent>(row.Parent);
			Temp.Parent = row;
			return Temp;
		}

		internal RowEventCollection _parent;
	}
}
