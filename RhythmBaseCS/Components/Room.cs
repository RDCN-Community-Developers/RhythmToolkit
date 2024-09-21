using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Runtime.CompilerServices;
namespace RhythmBase.Components
{
	/// <summary>
	/// Indicates that can be applied to multiple rooms.
	/// </summary>
	[JsonConverter(typeof(RoomConverter))]
	public struct Room
	{
		/// <summary>
		/// Can be applied to top room.
		/// </summary>
		public bool EnableTop { get; }
		/// <summary>
		/// Whether the specified room is enabled.
		/// </summary>
		/// <param name="Index"></param>
		[IndexerName("Room")]
		public bool this[byte Index]
		{
			readonly get => _data.HasFlag(Enum.Parse<RoomIndex>(Conversions.ToString(1 << Index)));
			set
			{
				if (!(Index >= 4 && !EnableTop))
					_data = value ? (_data | (RoomIndex)(1 << (int)Index)) : (_data & (RoomIndex)(1 << (int)Index));
			}
		}
		/// <summary>
		/// List of enabled rooms.
		/// </summary>
		public readonly List<byte> Rooms
		{
			get
			{
				RoomIndex indexes = _data;
				return Enumerable
					.Range(0, 5)
					.Where(x => indexes.HasFlag((RoomIndex)(1 << x)))
					.Select(x => (byte)x)
					.ToList();
			}
		}
		public override string ToString() => string.Format("[{0}]", string.Join(",", Rooms));
		/// <summary>
		/// Returns an instance with only room 1 enabled.
		/// </summary>
		/// <returns>An instance with only room 1 enabled.</returns>
		public static Room Default() => new(false, [])
		{
			_data = RoomIndex.Room1
		};
		public Room(bool enableTop) => EnableTop = enableTop;
		public Room(bool enableTop, params byte[] rooms)
		{
			this = default;
			EnableTop = enableTop;
			int num = rooms.Length;
			if (num != 0)
				if (num != 1)
					foreach (byte item in rooms)
						this[item] = true;
				else
					this[rooms.Single()] = true;
			else
				_data = RoomIndex.RoomNotAvaliable;
		}
		/// <summary>
		/// Check if the room is included.
		/// </summary>
		/// <param name="rooms">Rooms inspected.</param>
		/// <returns></returns>
		public readonly bool Contains(Room rooms)
		{
			if (_data == RoomIndex.RoomNotAvaliable)
				return false;
			else
			{

				for (int i = 0; i < 5; i++)
				{
					if (this[(byte)i] != rooms[(byte)i])
						break;
					if (i > 4)
						return true;
				}
				return false;
			}
		}
		public static bool operator ==(Room R1, Room R2) => R1._data == R2._data;
		public static bool operator !=(Room R1, Room R2) => !(R1 == R2);

		public static implicit operator Room(SingleRoom room) => new(room.EnableTop, [0, ((byte)room.Room)]);

		public static explicit operator SingleRoom(Room room) =>
			room.Rooms.Count == 1
				? new SingleRoom(room.Rooms.Single())
				: throw new Exceptions.RhythmBaseException();
		public override readonly bool Equals(object obj) => this == ((obj != null) ? ((Room)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(_data);

		private RoomIndex _data;
	}
}
