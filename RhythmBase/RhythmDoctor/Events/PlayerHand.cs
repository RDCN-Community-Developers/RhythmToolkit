namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Represents the hands of a player.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum PlayerHand
	{
		/// <summary>
		/// The left hand of the player.
		/// </summary>
		Left,
		/// <summary>
		/// The right hand of the player.
		/// </summary>
		Right,
		/// <summary>
		/// Both hands of the player.
		/// </summary>
		Both,
		/// <summary>
		/// Player 1's hand.
		/// </summary>
		p1,
		/// <summary>
		/// Player 2's hand.
		/// </summary>
		p2
	}
}
