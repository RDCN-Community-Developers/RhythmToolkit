using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Converters;
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
			get => _data.HasFlag(Enum.Parse<RoomIndex>(Conversions.ToString(1 << Index)));
			set
			{
				if (!(Index >= 4 && !EnableTop))
				{
					_data = value ? (_data | (RoomIndex)(1 << (int)Index)) : (_data & (RoomIndex)(1 << (int)Index));
				}
			}
		}
		/// <summary>
		/// List of enabled rooms.
		/// </summary>
		public List<byte> Rooms
		{
			get
			{
				List<byte> L = [];
				int i = 0;
				checked
				{
					do
					{
						if (_data.HasFlag(Enum.Parse<RoomIndex>(Conversions.ToString(1 << i))))
						{
							L.Add((byte)i);
						}
						i++;
					}
					while (i <= 4);
					return L;
				}
			}
		}

		public override string ToString() => string.Format("[{0}]", string.Join(",", Rooms));
		/// <summary>
		/// Returns an instance with only room 1 enabled.
		/// </summary>
		/// <returns>An instance with only room 1 enabled.</returns>
		public static Room Default() => new(false, Array.Empty<byte>())
		{
			_data = RoomIndex.Room1
		};

		public Room(bool enableTop)
		{
			this = default;
			this.EnableTop = enableTop;
		}

		public Room(bool enableTop, params byte[] rooms)
		{
			this = default;
			this.EnableTop = enableTop;
			int num = rooms.Length;
			if (num != 0)
			{
				if (num != 1)
				{
					foreach (byte item in rooms)
					{
						this[item] = true;
					}
				}
				else
				{
					this[rooms.Single()] = true;
				}
			}
			else
			{
				_data = RoomIndex.RoomNotAvaliable;
			}
		}
		/// <summary>
		/// Check if the room is included.
		/// </summary>
		/// <param name="rooms">Rooms inspected.</param>
		/// <returns></returns>
		public bool Contains(Room rooms)
		{
			bool flag = _data == RoomIndex.RoomNotAvaliable;
			checked
			{
				bool Contains;
				if (flag)
				{
					Contains = false;
				}
				else
				{
					int i = 0;
					for (; ; )
					{
						if (this[(byte)i] != rooms[(byte)i])
						{
							break;
						}
						i++;
						if (i > 4)
						{
							goto Block_3;
						}
					}
					return false;
				Block_3:
					Contains = true;
				}
				return Contains;
			}
		}

		public static bool operator ==(Room R1, Room R2) => R1._data == R2._data;

		public static bool operator !=(Room R1, Room R2) => !(R1 == R2);

		public static implicit operator Room(SingleRoom room)
		{
			Room result = new(room.EnableTop,
			[
				0,
				checked((byte)room.Room)
			]);
			return result;
		}

		public static explicit operator SingleRoom(Room room)
		{
			SingleRoom result = default;
			if (room.Rooms.Count == 1)
			{
				result = new SingleRoom(room.Rooms.Single());
			}
			return result;
		}

		public override bool Equals(object obj) => this == ((obj != null) ? ((Room)obj) : default);

		public override int GetHashCode() => HashCode.Combine(_data);

		private RoomIndex _data;
	}
}
