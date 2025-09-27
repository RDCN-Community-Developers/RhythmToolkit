namespace RhythmBase.Adofai.Events
{
	/// <summary>
	/// Represents the types of events available in the Adofai editor.
	/// </summary>
	[RDJsonEnumSerializable]
	public enum EventType

	{
		/// <summary>
		/// Adds a decoration to the level.
		/// </summary>
		/// <summary>  
		/// Adds a decoration to the level.  
		/// </summary>  
		AddDecoration,
		/// <summary>  
		/// Adds a particle effect to the level.  
		/// </summary>  
		AddParticle,
		/// <summary>
		/// Adds an object to the level.
		/// </summary>
		AddObject,
		/// <summary>
		/// Adds text to the level.
		/// </summary>
		AddText,
		/// <summary>
		/// Animates a track in the level.
		/// </summary>
		AnimateTrack,
		/// <summary>
		/// Enables autoplay for specific tiles.
		/// </summary>
		AutoPlayTiles,
		/// <summary>
		/// Adjusts the bloom effect in the level.
		/// </summary>
		Bloom,
		/// <summary>
		/// Adds a bookmark to the level.
		/// </summary>
		Bookmark,
		/// <summary>
		/// Creates a checkpoint in the level.
		/// </summary>
		Checkpoint,
		/// <summary>
		/// Changes the color of a track.
		/// </summary>
		ColorTrack,
		/// <summary>
		/// Sets a custom background for the level.
		/// </summary>
		CustomBackground,
		/// <summary>
		/// Defines a custom event in the level.
		/// </summary>
		ForwardEvent,
		/// <summary>
		/// Defines a custom tile event in the level.
		/// </summary>
		ForwardTileEvent,
		/// <summary>
		/// Adds a comment in the editor.
		/// </summary>
		EditorComment,
		/// <summary>  
		/// Emits a particle effect in the level.  
		/// </summary>  
		EmitParticle,
		/// <summary>
		/// Creates a flash effect in the level.
		/// </summary>
		Flash,
		/// <summary>
		/// Enables free roam mode.
		/// </summary>
		FreeRoam,
		/// <summary>
		/// Removes free roam mode.
		/// </summary>
		FreeRoamRemove,
		/// <summary>
		/// Adds a twirl effect in free roam mode.
		/// </summary>
		FreeRoamTwirl,
		/// <summary>
		/// Applies a hall of mirrors effect.
		/// </summary>
		HallOfMirrors,
		/// <summary>
		/// Hides an object or element in the level.
		/// </summary>
		Hide,
		/// <summary>
		/// Holds an action or event.
		/// </summary>
		Hold,
		/// <summary>
		/// Moves the camera to a specific position.
		/// </summary>
		MoveCamera,
		/// <summary>
		/// Moves decorations in the level.
		/// </summary>
		MoveDecorations,
		/// <summary>
		/// Moves a track to a specific position.
		/// </summary>
		MoveTrack,
		/// <summary>
		/// Enables multi-planet mode.
		/// </summary>
		MultiPlanet,
		/// <summary>
		/// Pauses the level.
		/// </summary>
		Pause,
		/// <summary>
		/// Plays a sound in the level.
		/// </summary>
		PlaySound,
		/// <summary>
		/// Sets the position of a track.
		/// </summary>
		PositionTrack,
		/// <summary>
		/// Recolors a track in the level.
		/// </summary>
		RecolorTrack,
		/// <summary>
		/// Repeats specific events in the level.
		/// </summary>
		RepeatEvents,
		/// <summary>
		/// Scales the margin of the level.
		/// </summary>
		ScaleMargin,
		/// <summary>
		/// Scales the planets in the level.
		/// </summary>
		ScalePlanets,
		/// <summary>
		/// Scales the radius of the level.
		/// </summary>
		ScaleRadius,
		/// <summary>
		/// Scrolls the screen in the level.
		/// </summary>
		ScreenScroll,
		/// <summary>
		/// Sets the screen tile effect.
		/// </summary>
		ScreenTile,
		/// <summary>
		/// Sets the background of the level.
		/// </summary>
		SetBackground,
		/// <summary>
		/// Sets conditional events in the level.
		/// </summary>
		SetConditionalEvents,
		/// <summary>
		/// Sets the default text for the level.
		/// </summary>
		SetDefaultText,
		/// <summary>
		/// Applies a filter effect in the level.
		/// </summary>
		SetFilter,
		/// <summary>
		/// Sets an advanced filter effect in the level.
		/// </summary>
		SetFilterAdvanced,
		/// <summary>
		/// Sets the frame rate for the level.
		/// </summary>
		SetFrameRate,
		/// <summary>
		/// Sets the hitsound for the level.
		/// </summary>
		SetHitsound,
		/// <summary>
		/// Sets the hold sound for the level.
		/// </summary>
		SetHoldSound,
		/// <summary>
		/// Sets the input event for the level.
		/// </summary>
		SetInputEvent,
		/// <summary>
		/// Sets an object in the level.
		/// </summary>
		SetObject,
		/// <summary>
		/// Sets the particle for the level.
		/// </summary>		SetParticle,
		/// <summary>
		/// Sets the rotation of a planet.
		/// </summary>
		SetPlanetRotation,
		/// <summary>
		/// Sets the speed of the level.
		/// </summary>
		SetSpeed,
		/// <summary>
		/// Sets text in the level.
		/// </summary>
		SetText,
		/// <summary>
		/// Creates a screen shake effect.
		/// </summary>
		ShakeScreen,
		/// <summary>
		/// Adds a twirl effect to the level.
		/// </summary>
		Twirl
	}
}
