using System;
namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents the different modes for displaying a background.
	/// </summary>
	public enum BgDisplayModes
	{
		/// <summary>
		/// Scales the background to fit the screen dimensions.
		/// </summary>
		FitToScreen,		/// <summary>
		/// Displays the background without scaling.
		/// </summary>
		Unscaled,		/// <summary>
		/// Tiles the background to fill the screen.
		/// </summary>
		Tiled
	}
}
