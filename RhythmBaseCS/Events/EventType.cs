namespace RhythmBase.Events
{
	/// <summary>
	/// Rhythm Doctor event types.
	/// </summary>
	public enum EventType
	{
		/// <summary>
		/// Add a classic beat.
		/// </summary>
		AddClassicBeat,
		/// <summary>
		/// Add a free time beat.
		/// </summary>
		AddFreeTimeBeat,
		/// <summary>
		/// Add a oneshot beat.
		/// </summary>
		AddOneshotBeat,
		/// <summary>
		/// Advance the text.
		/// </summary>
		AdvanceText,
		/// <summary>
		/// Drop the bass.
		/// </summary>
		BassDrop,
		/// <summary>
		/// Call a custom method.
		/// </summary>
		CallCustomMethod,
		/// <summary>
		/// Change the players' rows.
		/// </summary>
		ChangePlayersRows,
		/// <summary>
		/// Add a comment.
		/// </summary>
		Comment,
		/// <summary>
		/// Custom decoration event, from unknown event in the level or user-defined event.
		/// </summary>
		CustomDecorationEvent,
		/// <summary>
		/// Custom event, from unknown event in the level or user-defined event.
		/// </summary>
		CustomEvent,
		/// <summary>
		/// Custom row event, from unknown event in the level or user-defined event.
		/// </summary>
		CustomRowEvent,
		/// <summary>
		/// Custom flash event.
		/// </summary>
		CustomFlash,
		/// <summary>
		/// Fade the room.
		/// </summary>
		FadeRoom,
		/// <summary>
		/// Finish the level.
		/// </summary>
		FinishLevel,
		/// <summary>
		/// Flash the screen.
		/// </summary>
		Flash,
		/// <summary>
		/// Flip the screen.
		/// </summary>
		FlipScreen,
		/// <summary>
		/// Display floating text.
		/// </summary>
		FloatingText,
		/// <summary>
		/// Hide the row.
		/// </summary>
		HideRow,
		/// <summary>
		/// Invert the colors.
		/// </summary>
		InvertColors,
		/// <summary>
		/// Mask the room.
		/// </summary>
		MaskRoom,
		/// <summary>
		/// Move an object.
		/// </summary>
		Move,
		/// <summary>
		/// Move the camera.
		/// </summary>
		MoveCamera,
		/// <summary>
		/// Move the room.
		/// </summary>
		MoveRoom,
		/// <summary>
		/// Move the row.
		/// </summary>
		MoveRow,
		/// <summary>
		/// Narrate row information.
		/// </summary>
		NarrateRowInfo,
		/// <summary>
		/// Start a new window dance.
		/// </summary>
		NewWindowDance,
		/// <summary>
		/// Paint the hands.
		/// </summary>
		PaintHands,
		/// <summary>
		/// Play an animation.
		/// </summary>
		PlayAnimation,
		/// <summary>
		/// Play an expression.
		/// </summary>
		PlayExpression,
		/// <summary>
		/// Play a song.
		/// </summary>
		PlaySong,
		/// <summary>
		/// Play a sound.
		/// </summary>
		PlaySound,
		/// <summary>
		/// Pulse the camera.
		/// </summary>
		PulseCamera,
		/// <summary>
		/// Pulse a free time beat.
		/// </summary>
		PulseFreeTimeBeat,
		/// <summary>
		/// Read the narration.
		/// </summary>
		ReadNarration,
		/// <summary>
		/// Reorder the rooms.
		/// </summary>
		ReorderRooms,
		/// <summary>
		/// Say "Ready, Get Set, Go".
		/// </summary>
		SayReadyGetSetGo,
		/// <summary>
		/// Set the background color.
		/// </summary>
		SetBackgroundColor,
		/// <summary>
		/// Set the beat sound.
		/// </summary>
		SetBeatSound,
		/// <summary>
		/// Set the beats per minute.
		/// </summary>
		SetBeatsPerMinute,
		/// <summary>
		/// Set the clap sounds.
		/// </summary>
		SetClapSounds,
		/// <summary>
		/// Set the counting sound.
		/// </summary>
		SetCountingSound,
		/// <summary>
		/// Set the crotchets per bar.
		/// </summary>
		SetCrotchetsPerBar,
		/// <summary>
		/// Set the foreground.
		/// </summary>
		SetForeground,
		/// <summary>
		/// Set the game sound.
		/// </summary>
		SetGameSound,
		/// <summary>
		/// Set the hand owner.
		/// </summary>
		SetHandOwner,
		/// <summary>
		/// Set the heart explode interval.
		/// </summary>
		SetHeartExplodeInterval,
		/// <summary>
		/// Set the heart explode volume.
		/// </summary>
		SetHeartExplodeVolume,
		/// <summary>
		/// Set the oneshot wave.
		/// </summary>
		SetOneshotWave,
		/// <summary>
		/// Set the play style.
		/// </summary>
		SetPlayStyle,
		/// <summary>
		/// Set the room content mode.
		/// </summary>
		SetRoomContentMode,
		/// <summary>
		/// Set the room perspective.
		/// </summary>
		SetRoomPerspective,
		/// <summary>
		/// Set the row X positions.
		/// </summary>
		SetRowXs,
		/// <summary>
		/// Set the speed.
		/// </summary>
		SetSpeed,
		/// <summary>
		/// Set the theme.
		/// </summary>
		SetTheme,
		/// <summary>
		/// Set the VFX preset.
		/// </summary>
		SetVFXPreset,
		/// <summary>
		/// Set the visibility.
		/// </summary>
		SetVisible,
		/// <summary>
		/// Shake the screen.
		/// </summary>
		ShakeScreen,
		/// <summary>
		/// Show the dialogue.
		/// </summary>
		ShowDialogue,
		/// <summary>
		/// Show the hands.
		/// </summary>
		ShowHands,
		/// <summary>
		/// Show the rooms.
		/// </summary>
		ShowRooms,
		/// <summary>
		/// Show the status sign.
		/// </summary>
		ShowStatusSign,
		/// <summary>
		/// Stutter effect.
		/// </summary>
		Stutter,
		/// <summary>
		/// Tag an action.
		/// </summary>
		TagAction,
		/// <summary>
		/// Text explosion effect.
		/// </summary>
		TextExplosion,
		/// <summary>
		/// Tile effect.
		/// </summary>
		Tile,
		/// <summary>
		/// Tint effect.
		/// </summary>
		Tint,
		/// <summary>
		/// Tint rows effect.
		/// </summary>
		TintRows,
#if DEBUG
#endif
	}
}
