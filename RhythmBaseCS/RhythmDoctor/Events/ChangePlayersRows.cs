namespace RhythmBase.RhythmDoctor.Events
{
	/// <inheritdoc />
	public class ChangePlayersRows : BaseEvent
	{
		/// <inheritdoc />
		public ChangePlayersRows()
		{
			Players = new List<PlayerType>(16);
			CpuMarkers = new List<CpuType>(16);
			Type = EventType.ChangePlayersRows;
			Tab = Tabs.Actions;
		}
		/// <summary>
		/// Gets or sets the list of players.
		/// </summary>
		public List<PlayerType> Players { get; set; }
		/// <summary>
		/// Gets or sets the player mode.
		/// </summary>
		public PlayerModes PlayerMode { get; set; }
		/// <summary>
		/// Gets or sets the list of CPU markers.
		/// </summary>
		public List<CpuType> CpuMarkers { get; set; }
		/// <inheritdoc />
		public override EventType Type { get; }
		/// <inheritdoc />
		public override Tabs Tab { get; }
		/// <summary>
		/// Represents the types of CPUs.
		/// </summary>
		public enum CpuType
		{
			/// <summary>
			/// No CPU.
			/// </summary>
			None,
			/// <summary>
			/// Otto CPU type.
			/// </summary>
			Otto,
			/// <summary>
			/// Ian CPU type.
			/// </summary>
			Ian,
			/// <summary>
			/// Paige CPU type.
			/// </summary>
			Paige,
			/// <summary>
			/// Edega CPU type.
			/// </summary>
			Edega,
			/// <summary>
			/// Blank CPU type.
			/// </summary>
			BlankCPU,
			/// <summary>
			/// Samurai CPU type.
			/// </summary>
			Samurai
		}
		/// <summary>
		/// Represents the modes of players.
		/// </summary>
		public enum PlayerModes
		{
			/// <summary>
			/// Single player mode.
			/// </summary>
			OnePlayer,
			/// <summary>
			/// Two players mode.
			/// </summary>
			TwoPlayers
		}
	}
}
