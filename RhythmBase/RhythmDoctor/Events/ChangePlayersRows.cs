using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <inheritdoc />
public class ChangePlayersRows : BaseEvent
{
	/// <summary>
	/// Gets or sets the list of players.
	/// </summary>
	[RDJsonConverter(typeof(PlayerTypeGroupConverter))]
	public PlayerTypeGroup Players { get; set; } = new PlayerTypeGroup() { [0] = PlayerType.P1 };
	/// <summary>
	/// Gets or sets the player mode.
	/// </summary>
	public PlayingModes PlayerMode { get; set; } = PlayingModes.OnePlayer;
	/// <summary>
	/// Gets or sets the list of CPU markers.
	/// </summary>
	[RDJsonConverter(typeof(CpuTypeGroupConverter))]
	public CpuTypeGroup CpuMarkers { get; set; } = new CpuTypeGroup() { [0] = CpuType.Otto };
	/// <inheritdoc />
	public override EventType Type => EventType.ChangePlayersRows;

	/// <inheritdoc />
	public override Tab Tab => Tab.Actions;
}
/// <summary>
/// Represents the types of CPUs.
/// </summary>
[RDJsonEnumSerializable]
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
[RDJsonEnumSerializable]
public enum PlayingModes
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
