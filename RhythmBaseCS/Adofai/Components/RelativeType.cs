namespace RhythmBase.Adofai.Components
{
	/// <summary>  
	/// Specifies the relative type of a tile reference.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum RelativeType
	{
		/// <summary>  
		/// Refers to the current tile.  
		/// </summary>  
		ThisTile,
		/// <summary>  
		/// Refers to the starting tile.  
		/// </summary>  
		Start,
		/// <summary>  
		/// Refers to the ending tile.  
		/// </summary>  
		End,
	}
}
