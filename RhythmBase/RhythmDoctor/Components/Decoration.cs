using RhythmBase.RhythmDoctor.Events;
namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// A decoration.
	/// </summary>
	public class Decoration : OrderedEventCollection<BaseDecorationAction>
	{
		/// <summary>
		/// Decorated ID.
		/// </summary>
		public string Id
		{
			get => _id;
			set => _id = value;
		}
		/// <summary>
		/// Decoration index.
		/// </summary>
		public int Index => Parent?.Decorations.ToList().IndexOf(this) ?? throw new RhythmBaseException();
		/// <summary>
		/// Room.
		/// </summary>
		public RDSingleRoom Room { get; set; }
		/// <summary>
		/// The file reference used by the decoration.
		/// </summary>
		public string Filename { get; set; } = "";
		/// <summary>
		/// Decoration depth.
		/// </summary>
		public int Depth { get; set; }
		/// <summary>
		/// The filter used for this decoration.
		/// </summary>
		public Filters Filter { get; set; }
		/// <summary>
		/// The initial visibility of this decoration.
		/// </summary>
		public bool Visible { get; set; } = true;
		/// <summary>
		/// Initializes a new instance of the <see cref="Decoration"/> class.
		/// </summary>
		public Decoration()
		{
			Room = new RDSingleRoom(RDRoomIndex.Room1);
			_id = GetHashCode().ToString();
		}
		/// <param name="room">Decoration room.</param>
		internal Decoration(RDSingleRoom room)
		{
			Room = room;
			_id = GetHashCode().ToString();
		}
		/// <summary>
		/// Add an event to decoration.
		/// </summary>
		/// <param name="item">Decoration event.</param>
		public override bool Add(BaseDecorationAction item)
		{
			item._parent?.Remove(item);
			item._parent = this;
			bool success = base.Add(item);
			if (Parent is not null)
				success &= Parent.AddInternal(item);
			return success;
		}

		/// <summary>
		/// Remove an event from decoration.
		/// </summary>
		/// <param name="item">A decoration event.</param>
		public override bool Remove(BaseDecorationAction item)
		{
			return Parent?.RemoveInternal(item) ?? base.Remove(item);
		}
		/// <inheritdoc/>
		public override string ToString() => string.Format("{0}, {1}, {2}, {3}",
			[
				_id,
				Index,
				Room,
				Filename
			]);
		/// <summary>  
		/// Creates a shallow copy of the current <see cref="Decoration"/> instance.  
		/// </summary>  
		/// <returns>A new <see cref="Decoration"/> instance that is a shallow copy of the current instance.</returns>  
		public Decoration Clone()
		{
			var s = (Decoration)MemberwiseClone();
			s.Parent = null;
			return s;
		}
		private string _id = "";
		internal RDLevel? Parent = null;
	}
}
