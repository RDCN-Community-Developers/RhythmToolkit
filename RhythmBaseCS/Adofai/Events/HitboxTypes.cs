namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Specifies the hitbox types available for the decoration.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum HitboxTypes
	{
		/// <summary>  
		/// No hitbox is applied.  
		/// </summary>  
		None,
		/// <summary>  
		/// A hitbox that causes the player to fail when touched.  
		/// </summary>  
		Kill,
		/// <summary>  
		/// A hitbox that triggers an event when touched.  
		/// </summary>  
		Event
	}
}
