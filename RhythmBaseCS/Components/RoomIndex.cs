namespace RhythmBase.Components
{
	/// <summary>
	/// Represents the index of a room with various possible values.
	/// </summary>
	[Flags]
	public enum RoomIndex : byte
	{
		/// <summary>
		/// No room selected.
		/// </summary>
		None = 0b0000_0000,

		/// <summary>
		/// Represents Room 1.
		/// </summary>
		Room1 = 0b0000_0001,

		/// <summary>
		/// Represents Room 2.
		/// </summary>
		Room2 = 0b0000_0010,

		/// <summary>
		/// Represents Room 3.
		/// </summary>
		Room3 = 0b0000_0100,

		/// <summary>
		/// Represents Room 4.
		/// </summary>
		Room4 = 0b0000_1000,

		/// <summary>
		/// Represents the top room.
		/// </summary>
		RoomTop = 0b0001_0000,

		/// <summary>
		/// Indicates that the room is not available.
		/// </summary>
		RoomNotAvaliable = byte.MaxValue,
	}
}
