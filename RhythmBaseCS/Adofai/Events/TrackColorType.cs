using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the different types of track colors available in the application.
	/// </summary>
	public enum TrackColorType
	{
		/// <summary>
		/// A single solid color for the track.
		/// </summary>
		Single,		/// <summary>
		/// Alternating stripes of colors for the track.
		/// </summary>
		Stripes,		/// <summary>
		/// A glowing effect for the track.
		/// </summary>
		Glow,		/// <summary>
		/// A blinking effect for the track.
		/// </summary>
		Blink,		/// <summary>
		/// A switching effect between different colors for the track.
		/// </summary>
		Switch,		/// <summary>
		/// A rainbow gradient effect for the track.
		/// </summary>
		Rainbow,		/// <summary>
		/// A color effect based on the volume of the track.
		/// </summary>
		Volume
	}
}
