using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components
{
	public struct PlayerTypeGroup()
	{
		public readonly PlayerType this[int index]
		{
			get => index is >= 0 and < 16 ? _players[index] : throw new IndexOutOfRangeException(nameof(index));
			set => _players[index] = index is >= 0 and < 16 ? value : throw new IndexOutOfRangeException(nameof(index));
		}
		private readonly PlayerType[] _players = new PlayerType[16];
	}
}
