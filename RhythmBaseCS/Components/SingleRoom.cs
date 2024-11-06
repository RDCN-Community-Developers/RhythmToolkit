using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// Represents a single room that can be applied to one room only.
	/// </summary>
	[JsonConverter(typeof(RoomConverter))]
	public struct SingleRoom(RoomIndex index) : IEquatable<SingleRoom>
	{
		/// <summary>
		/// Gets a value indicating whether it can be used in the top room.
		/// </summary>
		public bool EnableTop { get; }

		/// <summary>
		/// Gets or sets the applied room.
		/// </summary>
		public RoomIndex Room
		{
			readonly get => _data;
			set => _data = value;
		}

		/// <summary>
		/// Gets or sets the applied room index as a byte.
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

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override readonly string ToString() => string.Format("[{0}]", _data);

		/// <summary>
		/// Gets the default single room which represents room 0.
		/// </summary>
		public static SingleRoom Default => new((RoomIndex)255);

		/// <summary>
		/// Initializes a new instance of the <see cref="SingleRoom"/> struct with the specified room index.
		/// </summary>
		/// <param name="room">The room index.</param>
		public SingleRoom(byte room) : this((RoomIndex)(1 << (int)room)) { }

		/// <summary>
		/// Determines whether two specified instances of <see cref="SingleRoom"/> are equal.
		/// </summary>
		/// <param name="R1">The first <see cref="SingleRoom"/> to compare.</param>
		/// <param name="R2">The second <see cref="SingleRoom"/> to compare.</param>
		/// <returns>true if the two <see cref="SingleRoom"/> instances are equal; otherwise, false.</returns>
		public static bool operator ==(SingleRoom R1, SingleRoom R2) => R1._data == R2._data;

		/// <summary>
		/// Determines whether two specified instances of <see cref="SingleRoom"/> are not equal.
		/// </summary>
		/// <param name="R1">The first <see cref="SingleRoom"/> to compare.</param>
		/// <param name="R2">The second <see cref="SingleRoom"/> to compare.</param>
		/// <returns>true if the two <see cref="SingleRoom"/> instances are not equal; otherwise, false.</returns>
		public static bool operator !=(SingleRoom R1, SingleRoom R2) => R1._data != R2._data;

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
		public override readonly bool Equals(object? obj) => obj is SingleRoom e && Equals(e);

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override readonly int GetHashCode() => HashCode.Combine(_data);

		/// <summary>
		/// Determines whether the specified <see cref="SingleRoom"/> is equal to the current <see cref="SingleRoom"/>.
		/// </summary>
		/// <param name="other">The <see cref="SingleRoom"/> to compare with the current <see cref="SingleRoom"/>.</param>
		/// <returns>true if the specified <see cref="SingleRoom"/> is equal to the current <see cref="SingleRoom"/>; otherwise, false.</returns>
		public readonly bool Equals(SingleRoom other) => _data == other._data;

		private RoomIndex _data = index;
	}
}
