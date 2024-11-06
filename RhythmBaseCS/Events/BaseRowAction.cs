using Newtonsoft.Json;
using RhythmBase.Components;
namespace RhythmBase.Events
{
	/// <summary>
	/// Represents a base action for a row event.
	/// </summary>
	public abstract class BaseRowAction : BaseEvent
	{
		/// <summary>
		/// Gets or sets the parent row event collection.
		/// </summary>
		[JsonIgnore]
		public RowEventCollection? Parent
		{
			get => _parent;
			internal set
			{
				if (_parent != null)
				{
					_parent.Remove(this);
					value?.Add(this);
				}
				_parent = value;
			}
		}

		/// <summary>
		/// Gets the room associated with this action.
		/// </summary>
		[JsonIgnore]
		public SingleRoom Room => _parent?.Rooms ?? SingleRoom.Default;

		/// <inheritdoc/>
		/// <summary>
		/// Gets or sets the beat associated with this action.
		/// </summary>
		[JsonIgnore]
		public override RDBeat Beat
		{
			get => _beat;
			set
			{
				_beat = _beat.BaseLevel == null
					? value.BaseLevel == null
						? value
						: value.WithoutBinding()
					: new(_beat.BaseLevel.Calculator, value);
			}
		}

		/// <summary>
		/// Gets the row index. This function is obsolete and may be removed in the next release. Use Index instead.
		/// </summary>
		[JsonIgnore]
		[Obsolete("This function is obsolete and may be removed in the next release. Use Index instead.")]
		public int Row { get; }

		/// <summary>
		/// Gets the index of the row in the parent collection.
		/// </summary>
		[JsonProperty("row", DefaultValueHandling = DefaultValueHandling.Include)]
		public int Index => Parent?.Index ?? -1;

		/// <summary>
		/// Clones this event and its basic properties. Clone will be added to the level.
		/// </summary>
		/// <typeparam name="TEvent">Type that will be generated.</typeparam>
		/// <returns>A new instance of <typeparamref name="TEvent"/>.</returns>
		public new TEvent Clone<TEvent>() where TEvent : BaseRowAction, new()
		{
			TEvent Temp = base.Clone<TEvent>();
			Temp.Parent = Parent;
			return Temp;
		}

		/// <summary>
		/// Clones this event and assigns it to a specified row event collection.
		/// </summary>
		/// <typeparam name="TEvent">Type that will be generated.</typeparam>
		/// <param name="row">The row event collection to assign the clone to.</param>
		/// <returns>A new instance of <typeparamref name="TEvent"/>.</returns>
		internal TEvent Clone<TEvent>(RowEventCollection row) where TEvent : BaseRowAction, new()
		{
			TEvent Temp = base.Clone<TEvent>(row.Parent ?? throw new RhythmBase.Exceptions.RhythmBaseException());
			Temp.Parent = row;
			return Temp;
		}

		internal RowEventCollection? _parent;
	}
}
