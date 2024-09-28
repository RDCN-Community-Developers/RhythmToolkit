using System;
namespace RhythmBase.Events
{
	/// <summary>
	/// Rhythm Doctor event types.
	/// </summary>
	public enum EventType
	{
		AddClassicBeat,
		AddFreeTimeBeat,
		AddOneshotBeat,
		AdvanceText,
		BassDrop,
		CallCustomMethod,
		ChangePlayersRows,
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
		CustomFlash,
		FadeRoom,
		FinishLevel,
		Flash,
		FlipScreen,
		FloatingText,
		HideRow,
		InvertColors,
		MaskRoom,
		Move,
		MoveCamera,
		MoveRoom,
		MoveRow,
		NarrateRowInfo,
		NewWindowDance,
		PaintHands,
		PlayAnimation,
		PlayExpression,
		PlaySong,
		PlaySound,
		PulseCamera,
		PulseFreeTimeBeat,
		ReadNarration,
		ReorderRooms,
		SayReadyGetSetGo,
		SetBackgroundColor,
		SetBeatSound,
		SetBeatsPerMinute,
		SetClapSounds,
		SetCountingSound,
		SetCrotchetsPerBar,
		SetForeground,
		SetGameSound,
		SetHandOwner,
		SetHeartExplodeInterval,
		SetHeartExplodeVolume,
		SetOneshotWave,
		SetPlayStyle,
		SetRoomContentMode,
		SetRoomPerspective,
		SetRowXs,
		SetSpeed,
		SetTheme,
		SetVFXPreset,
		SetVisible,
		ShakeScreen,
		ShowDialogue,
		ShowHands,
		ShowRooms,
		ShowStatusSign,
		Stutter,
		TagAction,
		TextExplosion,
		Tile,
		Tint,
		TintRows,
#if DEBUG
#endif
	}
}
