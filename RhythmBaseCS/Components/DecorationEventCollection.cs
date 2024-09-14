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
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		/// <summary>
		/// Decoration size.
		/// </summary>

		[JsonIgnore]
		public SKSizeI Size
		{
			get
			{
				Asset<ISpriteFile> file = _file;
				object obj = (file != null) ? file.Value.Size : new SKSizeI(32, 31);
				return (obj != null) ? ((SKSizeI)obj) : default;
			}
		}

		/// <summary>
		/// Decoration index.
		/// </summary>

		[JsonProperty("row")]
		public ulong Index
		{
			get
			{
				return checked((ulong)Parent.Decorations.ToList().IndexOf(this));
			}
		}


		[JsonProperty("rooms")]
		public SingleRoom Room { get; set; }

		/// <summary>
		/// The file reference used by the decoration.
		/// </summary>

		[JsonProperty("filename")]
		public string File
		{
			get
			{
				return _file.Name;
			}
		}

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
			Room = new SingleRoom(0);
		}

		/// <param name="room">Decoration room.</param>

		internal DecorationEventCollection(SingleRoom room)
		{
			Room = new SingleRoom(0);
			Room = room;
			_id = Conversions.ToString(GetHashCode());
		}

		/// <summary>
		/// Add an event to decoration.
		/// </summary>
		/// <param name="item">Decoration event.</param>

		public override void Add(BaseDecorationAction item)
		{
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
				this.Room,
				File
			});


		internal DecorationEventCollection Clone() => (DecorationEventCollection)MemberwiseClone();


		private string _id;


		[JsonIgnore]
		internal RDLevel Parent;


		internal Asset<ISpriteFile> _file;
	}
}
