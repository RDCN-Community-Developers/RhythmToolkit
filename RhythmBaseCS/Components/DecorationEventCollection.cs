﻿using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Events;
using RhythmBase.Exceptions;
namespace RhythmBase.Components
{
	/// <summary>
	/// A decoration.
	/// </summary>
	[JsonObject]
	public class DecorationEventCollection : OrderedEventCollection<BaseDecorationAction>
	{
		/// <summary>
		/// Decorated ID.
		/// </summary>
		[JsonProperty("id")]
		public string Id
		{
			get => _id;
			set => _id = value;
		}
		/// <summary>
		/// Decoration index.
		/// </summary>
		[JsonProperty("row")]
		public int Index => Parent?.Decorations.ToList().IndexOf(this) ?? throw new RhythmBaseException();

		/// <summary>
		/// Room.
		/// </summary>
		[JsonProperty("rooms")]
		public RDSingleRoom Room { get; set; }
		/// <summary>
		/// The file reference used by the decoration.
		/// </summary>
		[JsonProperty("filename")]
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
		public bool Visible { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DecorationEventCollection"/> class.
		/// </summary>
		public DecorationEventCollection()
		{
			Room = new RDSingleRoom(RDRoomIndex.Room1);
		}
		/// <param name="room">Decoration room.</param>
		internal DecorationEventCollection(RDSingleRoom room)
		{
			Room = room;
			_id = GetHashCode().ToString();
		}
		/// <summary>
		/// Add an event to decoration.
		/// </summary>
		/// <param name="item">Decoration event.</param>
		public override void Add(BaseDecorationAction item)
		{
			item._parent?.Remove(item);
			item._parent = this;
			Parent?.Add(item);
		}
		internal void AddSafely(BaseDecorationAction item) => base.Add(item);
		/// <summary>
		/// Remove an event from decoration.
		/// </summary>
		/// <param name="item">A decoration event.</param>
		public override bool Remove(BaseDecorationAction item) => Parent?.Remove(item) ?? throw new RhythmBaseException();
		internal bool RemoveSafely(BaseDecorationAction item) => base.Remove(item);
		/// <inheritdoc/>
		public override string ToString() => string.Format("{0}, {1}, {2}, {3}",
			[
				_id,
				Index,
				Room,
				Filename
			]);
		internal DecorationEventCollection Clone() => (DecorationEventCollection)MemberwiseClone();
		private string _id = "";
		[JsonIgnore]
		internal RDLevel? Parent = null;
	}
}
