using System;
namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Specifies the reference point for decoration placement in the game.
	/// </summary>
	public enum DecorationRelativeTo
	{
		/// <summary>
		/// Decoration is positioned relative to the tile.
		/// </summary>
		Tile,
		/// <summary>
		/// Decoration is positioned relative to the global coordinate system.
		/// </summary>
		Global,
		/// <summary>
		/// Decoration is positioned relative to the red planet.
		/// </summary>
		RedPlanet,
		/// <summary>
		/// Decoration is positioned relative to the blue planet.
		/// </summary>
		BluePlanet,		/// <summary>
		/// Decoration is positioned relative to the green planet.
		/// </summary>
		GreenPlanet,		/// <summary>
		/// Decoration is positioned relative to the camera.
		/// </summary>
		Camera,		/// <summary>
		/// Decoration is positioned relative to the camera's aspect ratio.
		/// </summary>
		CameraAspect,		/// <summary>
		/// Decoration is positioned relative to the last known position.
		/// </summary>
		LastPosition
	}
}
