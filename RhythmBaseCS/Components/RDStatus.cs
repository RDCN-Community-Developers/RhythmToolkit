namespace RhythmBase.Components
{
	/// <summary>
	/// Record the status of RDLevel moment
	/// </summary>
	public readonly record struct RDStatus
	{
		/// <summary>
		/// Gets the beat information.
		/// </summary>
		public RDBeat Beat { get; init; }

		/// <summary>
		/// Gets the room status information.
		/// </summary>
		public RoomStatus RoomStatus { get; init; }
	}

	/// <summary>
	/// Represents the status of a room.
	/// </summary>
	public readonly record struct RoomStatus
	{
		/// <summary>
		/// Gets the beat information.
		/// </summary>
		public RDBeat Beat { get; init; }
	}
}
