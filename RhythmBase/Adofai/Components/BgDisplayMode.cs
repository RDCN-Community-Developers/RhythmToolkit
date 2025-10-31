namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents the different modes for displaying a background.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum BgDisplayMode
	{
		/// <summary>
		/// Scales the background to fit the screen dimensions.
		/// </summary>
		FitToScreen,
		/// <summary>
		/// Displays the background without scaling.
		/// </summary>
		Unscaled,
		/// <summary>
		/// Tiles the background to fill the screen.
		/// </summary>
		Tiled
	}
}
