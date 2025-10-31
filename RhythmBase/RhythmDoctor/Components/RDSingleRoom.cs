using RhythmBase.RhythmDoctor.Converters;
using System.Text.Json.Serialization;
namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Represents a single room that can be applied to one room only.
	/// </summary>
	[JsonConverter(typeof(SingleRoomConverter))]
	public struct RDSingleRoom(RDRoomIndex index) : IEquatable<RDSingleRoom>
	{
		/// <summary>
		/// Gets a value indicating whether it can be used in the top room.
		/// </summary>
		public bool EnableTop { get; }
		/// <summary>
		/// Gets or sets the applied room.
		/// </summary>
		public RDRoomIndex Room
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
					if (_data == (RDRoomIndex)(1 << i))
						return (byte)i;
				}
				return byte.MaxValue;
			}
			set => _data = (RDRoomIndex)(1 << value);
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override readonly string ToString() => string.Format("[{0}]", _data);
		/// <summary>
		/// Gets the default single room which represents room 0.
		/// </summary>
		public static RDSingleRoom Default => new((RDRoomIndex)255);
		/// <summary>
		/// Initializes a new instance of the <see cref="RDSingleRoom"/> struct with the specified room index.
		/// </summary>
		/// <param name="room">The room index.</param>
		public RDSingleRoom(byte room) : this((RDRoomIndex)(1 << room)) { }

		/// <inheritdoc/>
		public static bool operator ==(RDSingleRoom R1, RDSingleRoom R2) => R1._data == R2._data;
		/// <inheritdoc/>
		public static bool operator !=(RDSingleRoom R1, RDSingleRoom R2) => R1._data != R2._data;
		/// <inheritdoc/>
		public static implicit operator RDSingleRoom(RDRoomIndex room) => new(room);
		/// <inheritdoc/>
		public override readonly bool Equals(object? obj) => obj is RDSingleRoom e && Equals(e);
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly int GetHashCode()
		{
			int hash = 17;
			hash = hash * 31 + _data.GetHashCode();
			return hash;
		}
#else
		public override readonly int GetHashCode() => HashCode.Combine(_data);
#endif
		/// <inheritdoc/>
		public readonly bool Equals(RDSingleRoom other) => _data == other._data;
		private RDRoomIndex _data = index;
	}
}
