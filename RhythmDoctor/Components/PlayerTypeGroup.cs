using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components
{
	public struct PlayerTypeGroup()
	{
		public readonly PlayerType this[int index]
		{
			get => _players[index];
			set => _players[index] = value;
		}
		private readonly PlayerType[] _players = new PlayerType[16];
	}
}
