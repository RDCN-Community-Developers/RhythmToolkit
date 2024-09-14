using System;
using System.Collections.Generic;

namespace RhythmBase.Events
{

	public class ChangePlayersRows : BaseEvent
	{

		public ChangePlayersRows()
		{
			Players = new List<PlayerType>(16);
			CpuMarkers = new List<CpuType>(16);
			Type = EventType.ChangePlayersRows;
			Tab = Tabs.Actions;
		}


		public List<PlayerType> Players { get; set; }


		public PlayerModes PlayerMode { get; set; }


		public List<CpuType> CpuMarkers { get; set; }


		public override EventType Type { get; }


		public override Tabs Tab { get; }


		public enum CpuType
		{

			None,

			Otto,

			Ian,

			Paige,

			Edega,

			BlankCPU,

			Samurai
		}


		public enum PlayerModes
		{

			OnePlayer,

			TwoPlayers
		}
	}
}
