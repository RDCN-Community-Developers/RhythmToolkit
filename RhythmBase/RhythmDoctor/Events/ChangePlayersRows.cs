using RhythmBase.RhythmDoctor.Components;
using RhythmBase.RhythmDoctor.Converters;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Represents an event that changes the configuration of player rows, including player assignments, player mode, and
/// CPU markers.
/// </summary>
public record class ChangePlayersRows : BaseEvent
{
	/// <summary>
	/// Initializes a new instance of the ChangePlayersRows class.
	/// </summary>
	public ChangePlayersRows()
	{
		CpuMarkers[0] = RDCharacters.Otto;
	}
	/// <summary>
	/// Gets or sets the list of players.
	/// </summary>
	[RDJsonConverter(typeof(PlayerTypeGroupConverter))]
	public PlayerTypeGroup Players { get; set; } = new PlayerTypeGroup() { [0] = PlayerType.P1 };
	/// <summary>
	/// Gets or sets the player mode.
	/// </summary>
	public PlayingMode PlayerMode { get; set; } = PlayingMode.OnePlayer;
	/// <summary>
	/// Gets or sets the list of CPU markers.
	/// </summary>
	[RDJsonConverter(typeof(CpuTypeGroupConverter))]
	public RDCharacters[] CpuMarkers { get; set; } = new RDCharacters[16];
	/// <inheritdoc />
	public override EventType Type => EventType.ChangePlayersRows;

	/// <inheritdoc />
	public override Tab Tab => Tab.Actions;
}