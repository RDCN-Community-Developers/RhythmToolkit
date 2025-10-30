namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Specifies the fail hitbox types available for the decoration.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum FailHitboxTypes
	{
		/// <summary>  
		/// A rectangular fail hitbox.  
		/// </summary>  
		Box,
		/// <summary>  
		/// A circular fail hitbox.  
		/// </summary>  
		Circle,
		/// <summary>  
		/// A capsule-shaped fail hitbox.  
		/// </summary>  
		Capsule
	}
}
