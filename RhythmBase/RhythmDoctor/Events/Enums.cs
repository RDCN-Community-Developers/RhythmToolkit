using System;
using System.Collections.Generic;
using System.Text;

namespace RhythmBase.RhythmDoctor.Events;

/// <summary>
/// Defines the classic beat patterns.
/// </summary>
[RDJsonEnumSerializable]
public enum ClassicBeatPattern
{
	/// <summary>
	/// No change in the beat pattern.
	/// </summary>
	NoChange,
	/// <summary>
	/// Three beat pattern.
	/// </summary>
	ThreeBeat,
	/// <summary>
	/// Four beat pattern.
	/// </summary>
	FourBeat
}/// <summary>
 /// Represents the freeze burn mode.
 /// </summary>
[RDJsonEnumSerializable]
public enum OneshotType
{
	/// <summary>
	/// A wave freeze burn mode.
	/// </summary>
	Wave,
	/// <summary>
	/// A freeze shot mode.
	/// </summary>
	Freezeshot,
	/// <summary>
	/// A burn shot mode.
	/// </summary>
	Burnshot
}
/// <summary>
/// Represents the type of pulse.
/// </summary>
[RDJsonEnumSerializable]
public enum OneshotPulseShapeType
{
	/// <summary>
	/// A wave pulse.
	/// </summary>
	Wave,
	/// <summary>
	/// A square pulse.
	/// </summary>
	Square,
	/// <summary>
	/// A triangle pulse.
	/// </summary>
	Triangle,
	/// <summary>
	/// A heart-shaped pulse.
	/// </summary>
	Heart
}
/// <summary>
/// Specifies when a hold cue should be triggered for a oneshot beat.
/// </summary>
[RDJsonEnumSerializable]
public enum HoldCue
{
	/// <summary>
	/// Let the system select the most appropriate cue timing automatically.
	/// </summary>
	Auto,
	/// <summary>
	/// Force the hold cue to trigger earlier than the default timing.
	/// </summary>
	Early,
	/// <summary>
	/// Force the hold cue to trigger later than the default timing.
	/// </summary>
	Late,
}
/// <summary>  
/// Defines the strength levels for the BassDrop event.  
/// </summary>  
[RDJsonEnumSerializable]
public enum StrengthType
{
	/// <summary>  
	/// Low strength.  
	/// </summary>  
	Low,
	/// <summary>  
	/// Medium strength.  
	/// </summary>  
	Medium,
	/// <summary>  
	/// High strength.  
	/// </summary>  
	High
}
/// <summary>  
/// Specifies the different types of blend effects available.  
/// </summary>  
[RDJsonEnumSerializable]
public enum BlendType
{
	/// <summary>  
	/// No blend effect.  
	/// </summary>  
	None,
	/// <summary>
	/// Additive blend effect.
	/// </summary>
	Additive,
	/// <summary>
	/// Multiply blend effect.
	/// </summary>
	Multiply,
	/// <summary>
	/// Invert blend effect.
	/// </summary>
	Invert,
}
/// <summary>
/// Specifies the types of borders that can be applied.
/// </summary>
[RDJsonEnumSerializable]
public enum Border
{
	/// <summary>
	/// No border.
	/// </summary>
	None,
	/// <summary>
	/// An outline border.
	/// </summary>
	Outline,
	/// <summary>
	/// A glowing border.
	/// </summary>
	Glow
}
/// <summary>
/// Specifies the execution time options for the method.
/// </summary>
[RDJsonEnumSerializable]
public enum EventExecutionTimeOption
{
	/// <summary>
	/// Execute the method on prebar.
	/// </summary>
	OnPrebar,
	/// <summary>
	/// Execute the method on bar.
	/// </summary>
	OnBar
}
/// <summary>
/// Represents the modes of players.
/// </summary>
[RDJsonEnumSerializable]
public enum PlayingMode
{
	/// <summary>
	/// Single player mode.
	/// </summary>
	OnePlayer,
	/// <summary>
	/// Two players mode.
	/// </summary>
	TwoPlayers,
	/// <summary>
	/// Single player mode or two players mode.
	/// </summary>
	OneOrTwoPlayers,
}
/// <summary>
/// Specifies the different modes for content display.
/// </summary>
[RDJsonEnumSerializable]
public enum ContentMode
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
/// <summary>
/// Enum representing default audio events in the RhythmBase application.
/// </summary>
[RDJsonEnumSerializable]
public enum DefaultAudio
{
	/// <summary>
	/// Base sound for the tutorial house.
	/// </summary>
	sndTutorialHouse_Base,
	/// <summary>
	/// Rest sound for the tutorial house.
	/// </summary>
	sndTutorialHouse_Rest,
	/// <summary>
	/// Amen fill sound for the tutorial house.
	/// </summary>
	sndTutorialHouse_AmenFill,
	/// <summary>
	/// First freeze sound for the tutorial house.
	/// </summary>
	sndTutorialHouse_Freeze1,
	/// <summary>
	/// Second freeze sound for the tutorial house.
	/// </summary>
	sndTutorialHouse_Freeze2,
	/// <summary>
	/// CPU freeze sound for the tutorial house.
	/// </summary>
	sndTutorialHouse_FreezeCPU,
	/// <summary>
	/// First burn sound for the tutorial house.
	/// </summary>
	sndTutorialHouse_Burn1,
	/// <summary>
	/// Second burn sound for the tutorial house.
	/// </summary>
	sndTutorialHouse_Burn2,
	/// <summary>
	/// CPU burn sound for the tutorial house.
	/// </summary>
	sndTutorialHouse_BurnCPU
}
/// <summary>
/// Specifies the possible durations for a flash event.
/// </summary>
[RDJsonEnumSerializable]
public enum DurationType
{
	/// <summary>
	/// A short duration.
	/// </summary>
	Short = 1,
	/// <summary>
	/// A medium duration.
	/// </summary>
	Medium = 2,
	/// <summary>
	/// A long duration.
	/// </summary>
	Long = 4,
}
/// <summary>  
/// Specifies the category of the narration.  
/// </summary>  
[RDJsonEnumSerializable]
public enum NarrationCategory
{
	/// <summary>  
	/// Fallback category, used as a default when no other category applies.  
	/// </summary>  
	Fallback,
	/// <summary>  
	/// Navigation category, used for guiding the user through the interface or level.  
	/// </summary>  
	Navigation,
	/// <summary>  
	/// Instruction category, used for providing instructions or tutorials.  
	/// </summary>  
	Instruction,
	/// <summary>  
	/// Notification category, used for alerts or notifications.  
	/// </summary>  
	Notification,
	/// <summary>  
	/// Dialogue category, used for character or story dialogues.  
	/// </summary>  
	Dialogue,
	/// <summary>  
	/// Description category, used for descriptive text or explanations.  
	/// </summary>  
	Description,
	/// <summary>  
	/// Subtitles category, used for displaying subtitles.  
	/// </summary>  
	Subtitles,
}
/// <summary>
/// Specifies the mode of the text.
/// </summary>
[Flags]
[RDJsonEnumSerializable]
public enum FloatingTextFadeOutMode
{
	/// <summary>
	/// The text will fade out gradually.
	/// </summary>
	FadeOut = 0,
	/// <summary>
	/// The text will hide abruptly.
	/// </summary>
	HideAbruptly = 1
}
/// <summary>
/// Specifies the anchor style of the text.
/// </summary>
[Flags]
public enum FloatingTextAnchorStyle
{
	/// <summary>
	/// The lower anchor style.
	/// </summary>
	Lower = 1,
	/// <summary>
	/// The upper anchor style.
	/// </summary>
	Upper = 2,
	/// <summary>
	/// The left anchor style.
	/// </summary>
	Left = 4,
	/// <summary>
	/// The right anchor style.
	/// </summary>
	Right = 8,
	/// <summary>
	/// The center anchor style.
	/// </summary>
	Center = 0
}
/// <summary>
/// Defines the possible transition types for hiding the row.
/// </summary>
[RDJsonEnumSerializable]
public enum Transition
{
	/// <summary>
	/// Smooth transition.
	/// </summary>
	Smooth,
	/// <summary>
	/// Instant transition.
	/// </summary>
	Instant,
	/// <summary>
	/// Full transition.
	/// </summary>
	Full,
	/// <summary>
	/// Represents a placeholder or default value indicating the absence of a specific option or selection.
	/// </summary>
	None,
}
/// <summary>
/// Defines the possible visibility states of the row.
/// </summary>
[RDJsonEnumSerializable]
public enum ShowTargetType
{
	/// <summary>
	/// Row is visible.
	/// </summary>
	Visible,
	/// <summary>
	/// Row is hidden.
	/// </summary>
	Hidden,
	/// <summary>
	/// Only the character is visible.
	/// </summary>
	OnlyCharacter,
	/// <summary>
	/// Only the row is visible.
	/// </summary>
	OnlyRow
}
/// <summary>
/// Defines the types of masks available.
/// </summary>
[RDJsonEnumSerializable]
public enum RoomMaskType
{
	/// <summary>
	/// Uses an image as the mask.
	/// </summary>
	Image,
	/// <summary>
	/// Uses a room as the mask.
	/// </summary>
	Room,
	/// <summary>
	/// Uses a color as the mask.
	/// </summary>
	Color,
	/// <summary>
	/// No mask is applied.
	/// </summary>
	None
}
/// <summary>
/// Defines the alpha modes available.
/// </summary>
[RDJsonEnumSerializable]
public enum MaskAlphaMode
{
	/// <summary>
	/// Normal alpha mode.
	/// </summary>
	Normal,
	/// <summary>
	/// Inverted alpha mode.
	/// </summary>
	Inverted
}
/// <summary>
/// Specifies the targets for the move row event.
/// </summary>
[RDJsonEnumSerializable]
public enum MoveRowTarget
{
	/// <summary>
	/// Target the whole row.
	/// </summary>
	WholeRow,
	/// <summary>
	/// Target the heart.
	/// </summary>
	Heart,
	/// <summary>
	/// Target the character.
	/// </summary>
	Character
}
/// <summary>
/// Specifies the type of narration information.
/// </summary>
[RDJsonEnumSerializable]
public enum NarrateInfoType
{
	/// <summary>
	/// Indicates a connection event.
	/// </summary>
	Connect,
	/// <summary>
	/// Indicates an update event.
	/// </summary>
	Update,
	/// <summary>
	/// Indicates a disconnection event.
	/// </summary>
	Disconnect,
	/// <summary>
	/// Indicates an online event.
	/// </summary>
	Online,
	/// <summary>
	/// Indicates an offline event.
	/// </summary>
	Offline
}
/// <summary>
/// Specifies the beats to skip during narration.
/// </summary>
[RDJsonEnumSerializable]
public enum NarrateSkipBeat
{
	/// <summary>
	/// Skip beats is on.
	/// </summary>
	On,
	/// <summary>
	/// Custom skip beats.
	/// </summary>
	Custom,
	/// <summary>
	/// Skip beats is off.
	/// </summary>
	Off,
}
/// <summary>
/// Represents the presets.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowDancePreset
{
	/// <summary>
	/// Move preset.
	/// </summary>
	Move,
	/// <summary>
	/// Sway preset.
	/// </summary>
	Sway,
	/// <summary>
	/// Wrap preset.
	/// </summary>
	Wrap,
	/// <summary>
	/// Ellipse preset.
	/// </summary>
	Ellipse,
	/// <summary>
	/// Shake per preset.
	/// </summary>
	ShakePer
}
/// <summary>
/// Represents the same preset behaviors.
/// </summary>
[RDJsonEnumSerializable]
public enum SamePresetBehavior
{
	/// <summary>
	/// Reset behavior.
	/// </summary>
	Reset,
	/// <summary>
	/// Keep behavior.
	/// </summary>
	Keep
}
/// <summary>
/// Represents the references.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowDanceReference
{
	/// <summary>
	/// Center reference.
	/// </summary>
	Center,
	/// <summary>
	/// Edge reference.
	/// </summary>
	Edge
}
/// <summary>
/// Represents the ease types.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowDanceEaseType
{
	/// <summary>
	/// Repeat ease type.
	/// </summary>
	Repeat,
	/// <summary>
	/// Mirror ease type.
	/// </summary>
	Mirror,
}
/// <summary>  
/// Enum representing different rhythm patterns.  
/// </summary>  
public enum Pattern
{
	/// <summary>  
	/// No pattern.  
	/// </summary>  
	None,
	/// <summary>  
	/// Pattern X.  
	/// </summary>  
	X,
	/// <summary>  
	/// Pattern Up.  
	/// </summary>  
	Up,
	/// <summary>  
	/// Pattern Down.  
	/// </summary>  
	Down,
	/// <summary>  
	/// Pattern Banana.  
	/// </summary>  
	Banana,
	/// <summary>  
	/// Pattern Return.  
	/// </summary>  
	Return
}
/// <summary>
/// Represents the hands of a player.
/// </summary>
[RDJsonEnumSerializable]
public enum PlayerHand
{
	/// <summary>
	/// The left hand of the player.
	/// </summary>
	Left,
	/// <summary>
	/// The right hand of the player.
	/// </summary>
	Right,
	/// <summary>
	/// Both hands of the player.
	/// </summary>
	Both,
	/// <summary>
	/// Player 1's hand.
	/// </summary>
	p1,
	/// <summary>
	/// Player 2's hand.
	/// </summary>
	p2
}
/// <summary>
/// Represents the type of player in the game.
/// </summary>
[RDJsonEnumSerializable]
public enum PlayerType
{
	/// <summary>  
	/// Automatically detect the player.  
	/// </summary>  
	AutoDetect,
	/// <summary>
	/// Player 1.
	/// </summary>
	P1,
	/// <summary>
	/// Player 2.
	/// </summary>
	P2,
	/// <summary>
	/// Computer player.
	/// </summary>
	CPU,
	/// <summary>
	/// Both.
	/// </summary>
	BOTH,
	/// <summary>
	/// No change in player type.
	/// </summary>
	NoChange,
}
/// <summary>
/// Defines the types of custom sounds.
/// </summary>
[RDJsonEnumSerializable]
public enum CustomSoundType
{
	/// <summary>
	/// Cue sound type.
	/// </summary>
	CueSound,
	/// <summary>
	/// Music sound type.
	/// </summary>
	MusicSound,
	/// <summary>
	/// Beat sound type.
	/// </summary>
	BeatSound,
	/// <summary>
	/// Hit sound type.
	/// </summary>
	HitSound,
	/// <summary>
	/// Other sound type.
	/// </summary>
	OtherSound
}
/// <summary>
/// Defines the action types for the pulse free time beat.
/// </summary>
[RDJsonEnumSerializable]
public enum PulseAction
{
	/// <summary>
	/// Increment action.
	/// </summary>
	Increment,
	/// <summary>
	/// Decrement action.
	/// </summary>
	Decrement,
	/// <summary>
	/// Custom action.
	/// </summary>
	Custom,
	/// <summary>
	/// Remove action.
	/// </summary>
	Remove
}
/// <summary>
/// Indicates how the window name should be modified by a <see cref="RenameWindow"/> event.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowNameAction
{
	/// <summary>
	/// Replace the current window name with the provided <see cref="RenameWindow.Text"/>.
	/// </summary>
	Set,

	/// <summary>
	/// Append the provided <see cref="RenameWindow.Text"/> to the existing window name.
	/// </summary>
	Append,

	/// <summary>
	/// Reset the window name to its default value. Any provided <see cref="RenameWindow.Text"/>
	/// is ignored when this action is used.
	/// </summary>
	Reset,
}
/// <summary>
/// Specifies the type of row in the rhythm base.
/// </summary>
[RDJsonEnumSerializable]
public enum RowType
{
	/// <summary>
	/// Represents a classic row type.
	/// </summary>
	Classic,
	/// <summary>
	/// Represents a oneshot row type.
	/// </summary>
	Oneshot
}
/// <summary>
/// Represents the sources of the voice.
/// </summary>
[RDJsonEnumSerializable]
public enum SayReadyGetSetGoVoiceSource
{
	/// <summary>
	/// Represents the voice source "Nurse".
	/// </summary>
	Nurse,
	/// <summary>
	/// Represents the voice source "Nurse Tired".
	/// </summary>
	NurseTired,
	/// <summary>
	/// Represents the voice source "Nurse Swing".
	/// </summary>
	NurseSwing,
	/// <summary>
	/// Represents the voice source "Nurse Swing Calm".
	/// </summary>
	NurseSwingCalm,
	/// <summary>
	/// Represents the voice source "Ian Excited".
	/// </summary>
	IanExcited,
	/// <summary>
	/// Represents the voice source "Ian Calm".
	/// </summary>
	IanCalm,
	/// <summary>
	/// Represents the voice source "Ian Slow".
	/// </summary>
	IanSlow,
	/// <summary>
	/// Represents the voice source "None Bottom".
	/// </summary>
	NoneBottom,
	/// <summary>
	/// Represents the voice source "None Top".
	/// </summary>
	NoneTop
}
/// <summary>
/// Represents the phrases that can be said.
/// </summary>
[RDJsonEnumSerializable]
public enum SayReadyGetSetGoWord
{
	/// <summary>
	/// Represents the phrase "Ready, Get Set, Go New".
	/// </summary>
	SayReaDyGetSetGoNew,
	/// <summary>
	/// Represents the phrase "Get Set, Go".
	/// </summary>
	SayGetSetGo,
	/// <summary>
	/// Represents the phrase "Ready, Get Set, One".
	/// </summary>
	SayReaDyGetSetOne,
	/// <summary>
	/// Represents the phrase "Get Set, One".
	/// </summary>
	SayGetSetOne,
	/// <summary>
	/// Represents the phrase "Rea".
	/// </summary>
	JustSayRea,
	/// <summary>
	/// Represents the phrase "Dy".
	/// </summary>
	JustSayDy,
	/// <summary>
	/// Represents the phrase "Get".
	/// </summary>
	JustSayGet,
	/// <summary>
	/// Represents the phrase "Set".
	/// </summary>
	JustSaySet,
	/// <summary>
	/// Represents the phrase "And".
	/// </summary>
	JustSayAnd,
	/// <summary>
	/// Represents the phrase "Go".
	/// </summary>
	JustSayGo,
	/// <summary>
	/// Represents the phrase "Stop".
	/// </summary>
	JustSayStop,
	/// <summary>
	/// Represents the phrase "And Stop".
	/// </summary>
	JustSayAndStop,
	/// <summary>
	/// Represents the phrase "Switch".
	/// </summary>
	SaySwitch,
	/// <summary>
	/// Represents the phrase "Watch".
	/// </summary>
	SayWatch,
	/// <summary>
	/// Represents the phrase "Listen".
	/// </summary>
	SayListen,
	/// <summary>
	/// Represents the count "1".
	/// </summary>
	Count1,
	/// <summary>
	/// Represents the count "2".
	/// </summary>
	Count2,
	/// <summary>
	/// Represents the count "3".
	/// </summary>
	Count3,
	/// <summary>
	/// Represents the count "4".
	/// </summary>
	Count4,
	/// <summary>
	/// Represents the count "5".
	/// </summary>
	Count5,
	/// <summary>
	/// Represents the count "6".
	/// </summary>
	Count6,
	/// <summary>
	/// Represents the count "7".
	/// </summary>
	Count7,
	/// <summary>
	/// Represents the count "8".
	/// </summary>
	Count8,
	/// <summary>
	/// Represents the count "9".
	/// </summary>
	Count9,
	/// <summary>
	/// Represents the count "10".
	/// </summary>
	Count10,
	/// <summary>
	/// Represents the phrase "Ready, Get Set, Go".
	/// </summary>
	SayReadyGetSetGo,
	/// <summary>
	/// Represents the phrase "Ready".
	/// </summary>
	JustSayReady
}
/// <summary>
/// Specifies the types of backgrounds.
/// </summary>
[RDJsonEnumSerializable]
public enum BackgroundType
{
	/// <summary>
	/// Background is a color.
	/// </summary>
	Color,
	/// <summary>
	/// Background is an image.
	/// </summary>
	Image
}
/// <summary>
/// Represents the different voice sources for the counting sound.
/// </summary>
[RDJsonEnumSerializable]
public enum CountingSoundVoiceSource
{
	/// <summary>
	/// Jyi Count
	/// </summary>
	JyiCount,
	/// <summary>
	/// Jyi Count Fast
	/// </summary>
	JyiCountFast,
	/// <summary>
	/// Jyi Count Calm
	/// </summary>
	JyiCountCalm,
	/// <summary>
	/// Jyi Count Tired
	/// </summary>
	JyiCountTired,
	/// <summary>
	/// Jyi Count Very Tired
	/// </summary>
	JyiCountVeryTired,
	/// <summary>
	/// Jyi Count Japanese
	/// </summary>
	JyiCountJapanese,
	/// <summary>
	/// Ian Count
	/// </summary>
	IanCount,
	/// <summary>
	/// Ian Count Fast
	/// </summary>
	IanCountFast,
	/// <summary>
	/// Ian Count Calm
	/// </summary>
	IanCountCalm,
	/// <summary>
	/// Ian Count Slow
	/// </summary>
	IanCountSlow,
	/// <summary>
	/// Ian Count Slower
	/// </summary>
	IanCountSlower,
	/// <summary>
	/// Whistle Count
	/// </summary>
	WhistleCount,
	/// <summary>
	/// Bird Count
	/// </summary>
	BirdCount,
	/// <summary>
	/// Parrot Count
	/// </summary>
	ParrotCount,
	/// <summary>
	/// Owl Count
	/// </summary>
	OwlCount,
	/// <summary>
	/// Oriole Count
	/// </summary>
	OrioleCount,
	/// <summary>
	/// Wren Count
	/// </summary>
	WrenCount,
	/// <summary>
	/// Canary Count
	/// </summary>
	CanaryCount,
	/// <summary>
	/// Spear Count
	/// </summary>
	SpearCount,
	/// <summary>
	/// Jyi Count Legacy
	/// </summary>
	JyiCountLegacy,
	/// <summary>
	/// Jyi Count English
	/// </summary>
	JyiCountEnglish,
	/// <summary>
	/// Ian Count English
	/// </summary>
	IanCountEnglish,
	/// <summary>
	/// Ian Count English Calm
	/// </summary>
	IanCountEnglishCalm,
	/// <summary>
	/// Ian Count English Slow
	/// </summary>
	IanCountEnglishSlow,
	/// <summary>
	/// Ian Count English Fast
	/// </summary>
	IanCountEnglishFast,
	/// <summary>
	/// Custom
	/// </summary>
	Custom
}
/// <summary>
/// Defines the types of intervals.
/// </summary>
[RDJsonEnumSerializable]
public enum HeartExplodeIntervalType
{
	/// <summary>
	/// Interval of one beat after.
	/// </summary>
	OneBeatAfter,
	/// <summary>
	/// Instant interval.
	/// </summary>
	Instant,
	/// <summary>
	/// Gather without ceiling.
	/// </summary>
	GatherNoCeil,
	/// <summary>
	/// Gather and ceiling.
	/// </summary>
	GatherAndCeil
}
/// <summary>  
/// Defines the types of waves.  
/// </summary>  
[RDJsonEnumSerializable]
public enum OneshotWaveShapeType
{
	/// <summary>  
	/// Boom and rush wave.  
	/// </summary>  
	BoomAndRush,
	/// <summary>  
	/// Ball wave.  
	/// </summary>  
	Ball,
	/// <summary>  
	/// Spring wave.  
	/// </summary>  
	Spring,
	/// <summary>  
	/// Spike wave.  
	/// </summary>  
	Spike,
	/// <summary>  
	/// Huge spike wave.  
	/// </summary>  
	SpikeHuge,
	/// <summary>  
	/// Single wave.  
	/// </summary>  
	Single
}
/// <summary>
/// Defines the play styles.
/// </summary>
[RDJsonEnumSerializable]
public enum PlayStyleType
{
	/// <summary>
	/// None.
	/// </summary>
	None,
	/// <summary>
	/// Normal play style.
	/// </summary>
	Normal,
	/// <summary>
	/// Default.
	/// </summary>
	Default,
	/// <summary>
	/// Loop play style.
	/// </summary>
	Loop,
	/// <summary>
	/// Loop only in beat.
	/// </summary>
	BeatLoopOnly,
	/// <summary>
	/// Prolong play style.
	/// </summary>
	Prolong,
	/// <summary>
	/// Prolong one bar play style.
	/// </summary>
	ProlongOneBar,
	/// <summary>
	/// Play on next bar.
	/// </summary>
	OnNextBar,
	/// <summary>
	/// 
	/// </summary>
	ScrubToNext,
	/// <summary>
	/// Immediate play style.
	/// </summary>
	Immediately,
	/// <summary>
	/// Extra immediate play style.
	/// </summary>
	ExtraImmediately,
}
/// <summary>
/// Specifies the style of the synco sound for the SetRowXs event.
/// </summary>
[RDJsonEnumSerializable]
public enum SetRowXsSyncoStyle
{
	/// <summary>
	/// Use the "Chirp" style for the synco sound.
	/// </summary>
	Chirp,
	/// <summary>
	/// Use the "Beep" style for the synco sound.
	/// </summary>
	Beep,
}
/// <summary>  
/// Represents the available themes.  
/// </summary>  
[RDJsonEnumSerializable]
public enum Theme
{
#pragma warning disable CS1591
	None,
	Intimate,
	IntimateSimple,
	InsomniacDay,
	InsomniacNight,
	Matrix,
	NeonMuseum,
	CrossesStraight,
	CrossesFalling,
	CubesFalling,
	CubesFallingNiceBlue,
	OrientalTechno,
	Kaleidoscope,
	PoliticiansRally,
	Rooftop,
	RooftopSummer,
	RooftopAutumn,
	BackAlley,
	Sky,
	NightSky,
	HallOfMirrors,
	CoffeeShop,
	CoffeeShopNight,
	Garden,
	GardenNight,
	TrainDay,
	TrainNight,
	DesertDay,
	DesertNight,
	HospitalWard,
	HospitalWardNight,
	PaigeOffice,
	Basement,
	ColeWardNight,
	ColeWardSunrise,
	BoyWard,
	GirlWard,
	Skyline,
	SkylineBlue,
	FloatingHeart,
	FloatingHeartWithCubes,
	FloatingHeartBroken,
	FloatingHeartBrokenWithCubes,
	ZenGarden,
	Space,
	Vaporwave,
	RollerDisco,
	Stadium,
	StadiumStormy,
	AthleteWard,
	AthleteWardNight,
	ProceduralTree,
	RecordsRoom,
	Airport,
	AbandonedWard,
#pragma warning restore CS1591
}
/// <summary>
/// Enum representing various VFX presets.
/// </summary>
[RDJsonEnumSerializable]
public enum VfxPreset
{
#pragma warning disable CS1591
	SilhouettesOnHBeat,
	Vignette,
	VignetteFlicker,
	ColourfulShockwaves,
	BassDropOnHit,
	ShakeOnHeartBeat,
	ShakeOnHit,
	LightStripVert,
	VHS,
	CutsceneMode,
	HueShift,
	Brightness,
	Contrast,
	Saturation,
	Noise,
	GlitchObstruction,
	Rain,
	Matrix,
	Confetti,
	FallingPetals,
	FallingPetalsInstant,
	FallingPetalsSnow,
	FallingLeaves,
	ConfettiBurst,
	Snow,
	Bloom,
	OrangeBloom,
	BlueBloom,
	HallOfMirrors,
	TileN,
	Sepia,
	CustomScreenScroll,
	JPEG,
	NumbersAbovePulses,
	Mosaic,
	ScreenWaves,
	Funk,
	Grain,
	Blizzard,
	Drawing,
	Aberration,
	Blur,
	RadialBlur,
	Dots,
	Fisheye,
	DisableAll,
	Diamonds,
	Tutorial,
	Balloons,
	GlassShatter,

	WavyRows,
	Tile2,
	Tile3,
	Tile4,
	ScreenScroll,
	ScreenScrollX,
	ScreenScrollSansVHS,
	ScreenScrollXSansVHS,
	RowGlowWhite,
	RowOutline,
	RowShadow,
	RowAllWhite,
	RowSilhouetteGlow,
	RowPlain,
	BlackAndWhite,
	Blackout,
	MiawMiaw,
#pragma warning restore CS1591
}
/// <summary>  
/// Specifies the available modes for displaying content in the window.  
/// </summary>  
[RDJsonEnumSerializable]
public enum WindowContentMode
{
	/// <summary>  
	/// Show all rooms in this window.  
	/// </summary>  
	OnTop,
	/// <summary>
	/// Show one room in this window.
	/// </summary>
	Room,
}
/// <summary>
/// Represents the type of shake effect used in Rhythm Doctor events.
/// </summary>
[RDJsonEnumSerializable]
public enum ShakeType
{
	/// <summary>
	/// Standard shake effect.
	/// </summary>
	Normal,
	/// <summary>
	/// Smooth shake effect.
	/// </summary>
	Smooth,
	/// <summary>
	/// Rotational shake effect.
	/// </summary>
	Rotate,
	/// <summary>
	/// Bass drop shake effect.
	/// </summary>
	BassDrop,
}
/// <summary>
/// Specifies the sides where the dialogue panel can be shown.
/// </summary>
[RDJsonEnumSerializable]
public enum DialogueSide
{
	/// <summary>
	/// The bottom side.
	/// </summary>
	Bottom,
	/// <summary>
	/// The top side.
	/// </summary>
	Top
}
/// <summary>
/// Specifies the sides where the portrait can be shown.
/// </summary>
[RDJsonEnumSerializable]
public enum DialoguePortraitSide
{
	/// <summary>
	/// The left side.
	/// </summary>
	Left,
	/// <summary>
	/// The right side.
	/// </summary>
	Right
}
/// <summary>
/// Defines the possible actions for the event.
/// </summary>
[RDJsonEnumSerializable]
public enum ShowHandsAction
{
	/// <summary>
	/// Show the hands.
	/// </summary>
	Show,
	/// <summary>
	/// Hide the hands.
	/// </summary>
	Hide,
	/// <summary>
	/// Raise the hands.
	/// </summary>
	Raise,
	/// <summary>
	/// Lower the hands.
	/// </summary>
	Lower
}
/// <summary>
/// Defines the possible extents for the action.
/// </summary>
[RDJsonEnumSerializable]
public enum ShowHandsExtent
{
	/// <summary>
	/// Full extent.
	/// </summary>
	Full,
	/// <summary>
	/// Short extent.
	/// </summary>
	Short
}
/// <summary>
/// Specifies the display mode for subdivision rows.
/// </summary>
[RDJsonEnumSerializable]
public enum ShowSubdivisionsRowsMode
{
	/// <summary>
	/// Mini mode for displaying subdivision rows.
	/// </summary>
	Mini,
	/// <summary>
	/// Normal mode for displaying subdivision rows.
	/// </summary>
	Normal,
}
/// <summary>
/// Enumerates the possible spinning actions that can be applied to rows.
/// </summary>
[RDJsonEnumSerializable]
public enum SpiningAction
{
	/// <summary>
	/// Connect the current row to another row (use <see cref="SpinningRows.ToRow"/> to indicate the target).
	/// </summary>
	Connect,

	/// <summary>
	/// Disconnect the current row from any connected row.
	/// </summary>
	Disconnect,

	/// <summary>
	/// Rotate the row by a specified angle over a duration with optional easing.
	/// </summary>
	Rotate,

	/// <summary>
	/// Apply a continuous (constant speed) rotation to the row.
	/// </summary>
	ConstantRotation,

	/// <summary>
	/// Apply a wavy rotational motion using amplitude and frequency parameters.
	/// </summary>
	WavyRotation,

	/// <summary>
	/// Merge rows together with optional visual effects.
	/// </summary>
	Merge,

	/// <summary>
	/// Split a row into multiple parts using rotational animation.
	/// </summary>
	Split,
}
/// <summary>
/// Defines the possible actions for the stutter event.
/// </summary>
[RDJsonEnumSerializable]
public enum StutterAction
{
	/// <summary>
	/// Add action.
	/// </summary>
	Add,
	/// <summary>
	/// Cancel action.
	/// </summary>
	Cancel
}
/// <summary>  
/// Specifies the different tabs available in the RhythmBase application.  
/// </summary>  
public enum Tab
{
	/// <summary>  
	/// Represents the Sounds tab.  
	/// </summary>  
	Sounds,
	/// <summary>  
	/// Represents the Rows tab.  
	/// </summary>  
	Rows,
	/// <summary>  
	/// Represents the Actions tab.  
	/// </summary>  
	Actions,
	/// <summary>  
	/// Represents the Decorations tab.  
	/// </summary>  
	Decorations,
	/// <summary>  
	/// Represents the Rooms tab.  
	/// </summary>  
	Rooms,
	/// <summary>  
	/// Represents the Windows tab.  
	/// </summary>  
	Windows,
	/// <summary>  
	/// Represents an unknown tab.  
	/// </summary>  
	Unknown
}
/// <summary>
/// Defines the possible actions for a tag.
/// </summary>
[Flags]
public enum ActionTagAction
{
	/// <summary>
	/// Represents the run action.
	/// </summary>
	Run = 2,
	/// <summary>
	/// Represents all actions.
	/// </summary>
	All = 1,
	/// <summary>
	/// Represents the enable action.
	/// </summary>
	Enable = 6,
	/// <summary>
	/// Represents the disable action.
	/// </summary>
	Disable = 4
}
/// <summary>
/// Defines special tags for the action.
/// </summary>
public enum SpecialTag
{
#pragma warning disable CS1591
	onHit,
	onMiss,
	onHeldPressHit,
	onHeldReleaseHit,
	onHeldPressMiss,
	onHeldReleaseMiss,
	row0,
	row1,
	row2,
	row3,
	row4,
	row5,
	row6,
	row7,
	row8,
	row9,
	row10,
	row11,
	row12,
	row13,
	row14,
	row15
#pragma warning restore CS1591
}
/// <summary>
/// Specifies the direction of the text explosion.
/// </summary>
[RDJsonEnumSerializable]
public enum TextExplosionDirection
{
	/// <summary>
	/// The text explodes to the left.
	/// </summary>
	Left,
	/// <summary>
	/// The text explodes to the right.
	/// </summary>
	Right
}
/// <summary>
/// Specifies the mode of the text explosion.
/// </summary>
[RDJsonEnumSerializable]
public enum TextExplosionMode
{
	/// <summary>
	/// The text explosion uses one color.
	/// </summary>
	OneColor,
	/// <summary>
	/// The text explosion uses random colors.
	/// </summary>
	Random
}
/// <summary>
/// Represents the types of tiling that can be applied.
/// </summary>
[RDJsonEnumSerializable]
public enum TilingType
{
	/// <summary>
	/// Tiling type where the content scrolls.
	/// </summary>
	Scroll,
	/// <summary>
	/// Tiling type where the content pulses.
	/// </summary>
	Pulse
}
/// <summary>
/// Specifies the row effects.
/// </summary>
[RDJsonEnumSerializable]
public enum TintRowEffect
{
	/// <summary>
	/// No effect.
	/// </summary>
	None,
	/// <summary>
	/// Electric effect.
	/// </summary>
	Electric,
	/// <summary>
	/// Smoke effect.
	/// </summary>
	Smoke
}
/// <summary>
/// Specifies how the pivot is interpreted when applying a window resize.
/// </summary>
[RDJsonEnumSerializable]
public enum PivotMode
{
	/// <summary>
	/// Use the default pivot behavior. Typically uses the pivot value directly without special anchoring.
	/// </summary>
	Default,

	/// <summary>
	/// Treat the pivot as an edge anchor. Pivot operations will align content relative to the specified window edge.
	/// </summary>
	AnchorEdge,
}

/// <summary>
/// Describes which edge of the window the content should be anchored to when resizing.
/// </summary>
[RDJsonEnumSerializable]
public enum WindowAnchorType
{
	/// <summary>
	/// No anchoring. Content is not anchored to any specific edge.
	/// </summary>
	None,

	/// <summary>
	/// Anchor to the left edge of the window.
	/// </summary>
	LeftEdge,

	/// <summary>
	/// Anchor to the right edge of the window.
	/// </summary>
	RightEdge,

	/// <summary>
	/// Anchor to the bottom edge of the window.
	/// </summary>
	BottomEdge,

	/// <summary>
	/// Anchor to the top edge of the window.
	/// </summary>
	TopEdge,
}
/// <summary>
/// Specifies how content is scaled or positioned within a container when zooming is applied.
/// </summary>
[RDJsonEnumSerializable]
public enum ZoomMode
{
	/// <summary>
	/// Gets or sets the fill brush used to paint the interior of the shape.
	/// </summary>
	Fill,
	/// <summary>
	/// Specifies how content is resized to fit within a given space.
	/// </summary>
	Fit,
	/// <summary>
	/// Represents the absence of a value or a default state.
	/// </summary>
	None,
}
/// <summary>
/// Specifies the type of render filter to be used.
/// </summary>
/// 
[RDJsonEnumSerializable]
public enum Filter
{
	/// <summary>
	/// Nearest neighbor filtering.
	/// </summary>
	NearestNeighbor,
	/// <summary>
	/// Bilinear filtering.
	/// </summary>
	BiliNear
}
/// <summary>  
/// Defines the types of sounds.  
/// </summary>  
[RDJsonEnumSerializable]
public enum SoundType
{
#pragma warning disable CS1591
	ClapSoundP1Classic,
	ClapSoundP2Classic,
	ClapSoundP1Oneshot,
	ClapSoundP2Oneshot,
	SmallMistake,
	BigMistake,
	Hand1PopSound,
	Hand2PopSound,
	HeartExplosion,
	HeartExplosion2,
	HeartExplosion3,
	ClapSoundHoldLongEnd,
	ClapSoundHoldLongStart,
	ClapSoundHoldShortEnd,
	ClapSoundHoldShortStart,
	PulseSoundHoldStart,
	PulseSoundHoldShortEnd,
	PulseSoundHoldEnd,
	PulseSoundHoldStartAlt,
	PulseSoundHoldShortEndAlt,
	PulseSoundHoldEndAlt,
	ClapSoundCPUClassic,
	ClapSoundCPUOneshot,
	ClapSoundHoldLongEndP2,
	ClapSoundHoldLongStartP2,
	ClapSoundHoldShortEndP2,
	ClapSoundHoldShortStartP2,
	PulseSoundHoldStartP2,
	PulseSoundHoldShortEndP2,
	PulseSoundHoldEndP2,
	PulseSoundHoldStartAltP2,
	PulseSoundHoldShortEndAltP2,
	PulseSoundHoldEndAltP2,
	FreezeshotSoundCueLow,
	FreezeshotSoundCueHigh,
	FreezeshotSoundRiser,
	FreezeshotSoundCymbal,
	BurnshotSoundCueLow,
	BurnshotSoundCueHigh,
	BurnshotSoundRiser,
	BurnshotSoundCymbal,
	ClapSoundHold,
	PulseSoundHold,
	ClapSoundHoldP2,
	PulseSoundHoldP2,
	FreezeshotSound,
	BurnshotSound,
	Skipshot,
	HoldshotSound,
	HoldshotSoundCue,
	HoldshotSoundClapStart,
	HoldshotSoundClapLongEnd,
	HoldshotSoundClapShortEnd,
#pragma warning restore CS1591
}
/// <summary>
/// Specifies the available font rendering styles for Rhythm Doctor text elements.
/// </summary>
[RDJsonEnumSerializable]
public enum RDFontType
{
	/// <summary>
	/// Uses the default project font, typically optimized for general UI text.
	/// </summary>
	Default,
	/// <summary>
	/// Renders text with pixel-perfect precision, ideal for retro aesthetics.
	/// </summary>
	Pixel,
	/// <summary>
	/// Utilizes vector-based rendering to keep text crisp at any scale.
	/// </summary>
	Vector,
	/// <summary>
	/// Applies a Flash-inspired font style for legacy content compatibility.
	/// </summary>
	Flash
}
[RDJsonEnumSerializable]
public enum LayerType
{
	Dialogue,
	ForegroundParticles,
	Foreground,
	Default,
	Background,
	BackgroundParticles,
}