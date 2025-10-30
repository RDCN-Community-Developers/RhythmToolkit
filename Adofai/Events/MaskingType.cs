namespace RhythmBase.Adofai.Events
{
	/// <summary>  
	/// Specifies the masking types available for the decoration.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum MaskingType
	{
		/// <summary>  
		/// No masking is applied.  
		/// </summary>  
		None,
		/// <summary>  
		/// Applies a mask to the decoration.  
		/// </summary>  
		Mask,
		/// <summary>  
		/// Makes the decoration visible only inside the mask.  
		/// </summary>  
		VisibleInsideMask,
		/// <summary>  
		/// Makes the decoration visible only outside the mask.  
		/// </summary>  
		VisibleOutsideMask
	}
}
