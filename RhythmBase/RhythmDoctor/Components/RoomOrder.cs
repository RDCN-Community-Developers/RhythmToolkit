using RhythmBase.RhythmDoctor.Converters;
using System.Text.Json.Serialization;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Represents a unique order of rooms identified by their IDs.
	/// </summary>
	[JsonConverter(typeof(RoomOrderConverter))]
	public struct RoomOrder
	{
		private byte _id;

		/// <summary>
		/// Initializes a new instance of the <see cref="RoomOrder"/> struct with the specified room IDs.
		/// </summary>
		/// <param name="tgt1">The ID of the first room.</param>
		/// <param name="tgt2">The ID of the second room.</param>
		/// <param name="tgt3">The ID of the third room.</param>
		/// <param name="tgt4">The ID of the fourth room.</param>
		/// <exception cref="ArgumentException">Thrown when room IDs are not unique.</exception>
		public RoomOrder(byte tgt1, byte tgt2, byte tgt3, byte tgt4)
		{
			if (tgt1 == tgt2 || tgt1 == tgt3 || tgt1 == tgt4 ||
				tgt2 == tgt3 || tgt2 == tgt4 ||
				tgt3 == tgt4)
				throw new ArgumentException("Room IDs must be unique.");
			_id = (byte)(tgt1 * 6
				+ (tgt2 > tgt3 ? 2 : 0) + (tgt2 > tgt4 ? 2 : 0)
				+ (tgt3 > tgt4 ? 1 : 0));
		}

		/// <summary>
		/// Gets the order of room IDs as an array of bytes.
		/// </summary>
		public readonly byte[] Order
		{
			get
			{
#if NET7_0_OR_GREATER
				(byte id1, byte rm1) = byte.DivRem(_id, 6);
				(byte id2, byte rm2) = byte.DivRem(rm1, 2);
#else
				byte id1 = (byte)Math.DivRem(_id, 6, out int rm1);
				byte id2 = (byte)Math.DivRem(rm1, 2, out int rm2);
#endif
				List<byte> l = [0, 1, 2, 3];
				byte o1 = l[id1];
				l.RemoveAt(id1);
				byte o2 = l[id2];
				l.RemoveAt(id2);
				byte o3 = l[rm2];
				l.RemoveAt(rm2);
				return [o1, o2, o3, l[0]];
			}
		}

		/// <summary>
		/// Gets the room ID at the specified index in the order.
		/// </summary>
		/// <param name="index">The index of the room ID to retrieve (0 to 3).</param>
		/// <returns>The room ID at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
		public readonly byte this[int index]
		{
			get
			{
				if (index < 0 || index > 3)
					throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and 3.");
				return Order[index];
			}
		}

		/// <summary>
		/// Returns a string representation of the room order.
		/// </summary>
		/// <returns>A string representing the room order.</returns>
		public readonly override string ToString()
		{
			return $"RoomOrder: {string.Join(", ", Order)}";
		}

		/// <summary>
		/// Implicitly converts a <see cref="RoomOrder"/> to a byte array.
		/// </summary>
		/// <param name="order">The <see cref="RoomOrder"/> to convert.</param>
		public static implicit operator byte[](RoomOrder order)
		{
			return order.Order;
		}

		/// <summary>
		/// Implicitly converts a byte array to a <see cref="RoomOrder"/>.
		/// </summary>
		/// <param name="order">The byte array to convert.</param>
		/// <returns>A <see cref="RoomOrder"/> instance.</returns>
		/// <exception cref="ArgumentException">Thrown when the byte array length is not 4.</exception>
		public static implicit operator RoomOrder(byte[] order)
		{
			if (order.Length != 4)
				throw new ArgumentException("Order must be an array of 4 bytes.");
			return new RoomOrder(order[0], order[1], order[2], order[3]);
		}
	}
}
