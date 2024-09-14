using System;

namespace RhythmBase.Components
{

	public struct ClassicBeatStatus
	{

		public StatusType Status;


		public ushort BeatCount;


		public enum StatusType
		{

			Unset = -1,

			None,

			Synco,

			Beat_Open,

			Beat_Flash,

			Beat_Double_Flash,

			Beat_Triple_Flash,

			Beat_Close,

			X_Open,

			X_Flash,

			X_Close,

			X_Synco_Open,

			X_Synco_Flash,

			X_Synco_Close,

			Up_Open,

			Up_Close,

			Down_Open,

			Down_Close,

			Swing_Left,

			Swing_Right,

			Swing_Bounce,

			Held_Start,

			Held_End
		}
	}
}
