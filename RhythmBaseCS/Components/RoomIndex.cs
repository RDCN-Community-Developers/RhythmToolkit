namespace RhythmBase.Components
{
	/// <summary>
	/// Room index.
	/// </summary>
	[Flags]
	public enum RoomIndex : byte
	{
		None =             0b0000_0000,
		Room1 =            0b0000_0001,
		Room2 =            0b0000_0010,
		Room3 =            0b0000_0100,
		Room4 =            0b0000_1000,
		RoomTop =          0b0001_0000,
		RoomNotAvaliable = byte.MaxValue,
	}
}
