using System;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// Can only be applied to one room.
	/// </summary>
	[JsonConverter(typeof(RoomConverter))]
	public struct SingleRoom
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
			readonly get
			{
				return _data;
			}
			set
			{
				_data = value;
			}
		}
		/// <summary>
		/// Applied room indexes.
		/// </summary>
		public byte Value
		{
			readonly get
			{
				int i = 0;
				checked
				{
					for (; ; )
					{
						if (_data == Enum.Parse<RoomIndex>(Conversions.ToString(1 << i)))
						{
							break;
						}
						i++;
						if (i > 4)
						{
							goto Block_2;
						}
					}
					return (byte)i;
				Block_2:
					return byte.MaxValue;
				}
			}
			set
			{
				_data = (RoomIndex)(1 << (int)value);
			}
		}

		public override readonly string ToString() => string.Format("[{0}]", _data);
		/// <summary>
		/// Represents room 0.
		/// </summary>
		public static SingleRoom Default
		{
			get
			{
				return new SingleRoom
				{
					_data = (RoomIndex)255
				};
			}
		}

		public SingleRoom(byte room)
		{
			this = default;
			_data = (RoomIndex)(1 << (int)room);
		}

		public static bool operator ==(SingleRoom R1, SingleRoom R2) => R1._data == R2._data;

		public static bool operator !=(SingleRoom R1, SingleRoom R2) => !(R1 == R2);

		public override readonly bool Equals(object obj) => this == ((obj != null) ? ((SingleRoom)obj) : default);

		public override readonly int GetHashCode() => HashCode.Combine(_data);

		private RoomIndex _data;
	}
}
