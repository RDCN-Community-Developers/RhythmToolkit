using RhythmBase.RhythmDoctor.Events;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Represents a fixed-size collection of 16 player types, accessible by index.
	/// </summary>
	/// <remarks>This structure provides indexed access to a collection of player types, where the valid index range
	/// is 0 to 15.  Attempting to access an index outside this range will result in an <see
	/// cref="IndexOutOfRangeException"/>.</remarks>
	public struct PlayerTypeGroup()
	{
		/// <summary>
		/// Gets or sets the <see cref="PlayerType"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the player. Must be in the range 0 to 15, inclusive.</param>
		/// <returns></returns>
		/// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="index"/> is less than 0 or greater than 15.</exception>
		public readonly PlayerType this[int index]
		{
			get => index is >= 0 and < 16 ? _players[index] : throw new IndexOutOfRangeException(nameof(index));
			set => _players[index] = index is >= 0 and < 16 ? value : throw new IndexOutOfRangeException(nameof(index));
		}
		private readonly PlayerType[] _players = new PlayerType[16];
	}
}
