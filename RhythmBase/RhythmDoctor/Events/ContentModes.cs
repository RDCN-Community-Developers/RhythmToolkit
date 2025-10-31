namespace RhythmBase.RhythmDoctor.Events
{
	/// <summary>
	/// Specifies the different modes for content display.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum ContentModes
	{
		/// <summary>
		/// Scales the content to fill the available space.
		/// </summary>
		ScaleToFill,
		/// <summary>
		/// Scales the content to fit within the available space while maintaining the aspect ratio.
		/// </summary>
		AspectFit,
		/// <summary>
		/// Scales the content to fill the available space while maintaining the aspect ratio.
		/// </summary>
		AspectFill,
		/// <summary>
		/// Centers the content within the available space without scaling.
		/// </summary>
		Center,
		/// <summary>
		/// Tiles the content to fill the available space.
		/// </summary>
		Tiled,
		/// <summary>
		/// 
		/// </summary>
		Real,
	}
}
