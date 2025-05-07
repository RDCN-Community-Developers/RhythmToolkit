using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Specifies the reference point for the camera in the game.
	/// </summary>
	public enum ADCameraRelativeTo
	{
		/// <summary>
		/// The camera is relative to the player.
		/// </summary>
		Player,		/// <summary>
		/// The camera is relative to the tile.
		/// </summary>
		Tile,		/// <summary>
		/// The camera is relative to a global position.
		/// </summary>
		Global,		/// <summary>
		/// The camera is relative to the last position.
		/// </summary>
		LastPosition,		/// <summary>
		/// The camera is relative to the last position without considering rotation.
		/// </summary>
		LastPositionNoRotation
	}
}
