using System;
namespace RhythmBase.Components
{
	/// <summary>
	/// Room index.
	/// </summary>
	[Flags]
	public enum RoomIndex
	{
		None = 0,
		Room1 = 1,
		Room2 = 2,
		Room3 = 4,
		Room4 = 8,
		RoomTop = 16,
		RoomNotAvaliable = 127
	}
}
