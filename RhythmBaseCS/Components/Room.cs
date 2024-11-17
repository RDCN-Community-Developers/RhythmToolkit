using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Numerics;
using System.Runtime.CompilerServices;
namespace RhythmBase.Components
{
	/// <summary>
	/// Represents a room that can be applied to multiple rooms.
	/// </summary>
	[JsonConverter(typeof(RoomConverter))]
	public struct Room : IEqualityOperators<Room, Room, bool>, IEquatable<Room>
	{
		/// <summary>
		/// Indicates if the top room can be applied.
		/// </summary>
		public bool EnableTop { get; }

		/// <summary>
		/// Gets or sets whether the specified room is enabled.
		/// </summary>
		/// <param name="Index">The index of the room.</param>
		/// <returns>True if the room is enabled; otherwise, false.</returns>
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
		/// Gets the list of enabled rooms.
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

		/// <inheritdoc/>
		public override readonly string ToString() => string.Format("[{0}]", string.Join(",", Rooms));

		/// <summary>
		/// Returns an instance with only room 1 enabled.
		/// </summary>
		/// <returns>An instance with only room 1 enabled.</returns>
		public static Room Default() => new(false, [])
		{
			_data = RoomIndex.Room1
		};

		/// <summary>
		/// Initializes a new instance of the <see cref="Room"/> struct.
		/// </summary>
		/// <param name="enableTop">Indicates if the top room can be applied.</param>
		public Room(bool enableTop) => EnableTop = enableTop;

		/// <summary>
		/// Initializes a new instance of the <see cref="Room"/> struct with specified rooms.
		/// </summary>
		/// <param name="enableTop">Indicates if the top room can be applied.</param>
		/// <param name="rooms">The rooms to be enabled.</param>
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
		/// Checks if the specified rooms are included.
		/// </summary>
		/// <param name="rooms">The rooms to check.</param>
		/// <returns>True if the rooms are included; otherwise, false.</returns>
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
		/// <summary>
		/// Checks if the specified room is included.
		/// </summary>
		/// <param name="room">The room to check.</param>
		/// <returns>True if the room is included; otherwise, false.</returns>
		public readonly bool Contains(RoomIndex room)
		{
			return _data.HasFlag(room);
		}

		/// <inheritdoc/>
		public static bool operator ==(Room R1, Room R2) => R1._data == R2._data;

		/// <inheritdoc/>
		public static bool operator !=(Room R1, Room R2) => !(R1 == R2);

		/// <summary>
		/// Implicitly converts a SingleRoom to a Room.
		/// </summary>
		/// <param name="room">The SingleRoom instance to convert.</param>
		/// <returns>A Room instance.</returns>
		public static implicit operator Room(SingleRoom room) => new(room.EnableTop, [0, ((byte)room.Room)]);

		/// <summary>
		/// Explicitly converts a Room to a SingleRoom.
		/// </summary>
		/// <param name="room">The Room instance to convert.</param>
		/// <returns>A SingleRoom instance.</returns>
		/// <exception cref="Exceptions.RhythmBaseException">Thrown when the Room contains more than one room.</exception>
		public static explicit operator SingleRoom(Room room) =>
			room.Rooms.Count == 1
				? new SingleRoom(room.Rooms.Single())
				: throw new Exceptions.RhythmBaseException();

		/// <inheritdoc/>
		public override readonly bool Equals(object? obj) => obj is Room e && Equals(e);

		/// <inheritdoc/>
		public override readonly int GetHashCode() => HashCode.Combine(_data);

		/// <inheritdoc/>
		public readonly bool Equals(Room other) => this == other;

		private RoomIndex _data;
	}
}
