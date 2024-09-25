using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// Can only be applied to one room.
	/// </summary>
	[JsonConverter(typeof(RoomConverter))]
	public struct SingleRoom(RoomIndex index)
	{
		/// <summary>
		/// Whether it can be used in the top room.
		/// </summary>
		public bool EnableTop { get; }
		/// <summary>
		/// Applied rooms.
		/// </summary>
		public RoomIndex Room
		{
			readonly get => _data;
			set => _data = value;
		}
		/// <summary>
		/// Applied room indexes.
		/// </summary>
		public byte Value
		{
			readonly get
			{
				for (int i = 0; i < 5; i++)
				{
					if (_data == (RoomIndex)(1 << i))
						return (byte)i;
				}
				return byte.MaxValue;
			}
			set => _data = (RoomIndex)(1 << value);
		}

		public override readonly string ToString() => string.Format("[{0}]", _data);
		/// <summary>
		/// Represents room 0.
		/// </summary>
		public static SingleRoom Default => new((RoomIndex)255);
		public SingleRoom(byte room):this( (RoomIndex)(1 << (int)room)) { }
		public static bool operator ==(SingleRoom R1, SingleRoom R2) => R1._data == R2._data;
		public static bool operator !=(SingleRoom R1, SingleRoom R2) => !(R1 == R2);
		public override readonly bool Equals(object obj) => this == ((obj != null) ? ((SingleRoom)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(_data);
		private RoomIndex _data = index;
	}
}
