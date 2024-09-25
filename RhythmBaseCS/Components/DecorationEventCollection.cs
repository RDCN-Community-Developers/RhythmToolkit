using System;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Assets;
using RhythmBase.Events;
using SkiaSharp;
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
		/// Decoration size.
		/// </summary>
		[JsonIgnore]
		public RDSizeI Size
		{
			get
			{
				Asset<ISpriteFile> file = _file;
				RDSizeI obj = (file != null) ? file.Value.Size : new RDSizeI(32, 31);
				return obj;
			}
		}
		/// <summary>
		/// Decoration index.
		/// </summary>
		[JsonProperty("row")]
		public int Index => Parent.Decorations.ToList().IndexOf(this);

		[JsonProperty("rooms")]
		public SingleRoom Room { get; set; }
		/// <summary>
		/// The file reference used by the decoration.
		/// </summary>
		[JsonProperty("filename")]
		public string File
		{
			get => _file.Name;
			set
			{
				_file ??= new();
				_file.Name = value;
			}
		}
		[JsonIgnore]
		public Asset<ISpriteFile> Sprite { get => _file; set => _file = value; }
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

		public DecorationEventCollection()
		{
			Room = new SingleRoom(RoomIndex.Room1);
		}
		/// <param name="room">Decoration room.</param>
		internal DecorationEventCollection(SingleRoom room)
		{
			Room = room;
			_id = Conversions.ToString(GetHashCode());
		}
		/// <summary>
		/// Add an event to decoration.
		/// </summary>
		/// <param name="item">Decoration event.</param>
		public override void Add(BaseDecorationAction item)
		{
			item._parent?.Remove(item);
			item._parent = this;
			Parent.Add(item);
		}
		internal void AddSafely(BaseDecorationAction item) => base.Add(item);
		/// <summary>
		/// Remove an event from decoration.
		/// </summary>
		/// <param name="item">A decoration event.</param>
		public override bool Remove(BaseDecorationAction item) => Parent.Remove(item);
		internal bool RemoveSafely(BaseDecorationAction item) => base.Remove(item);
		public override string ToString() => string.Format("{0}, {1}, {2}, {3}", new object[]
			{
				_id,
				Index,
				Room,
				File
			});
		internal DecorationEventCollection Clone() => (DecorationEventCollection)MemberwiseClone();
		private string _id;
		[JsonIgnore]
		internal RDLevel Parent;
		internal Asset<ISpriteFile> _file = new();
	}
}
