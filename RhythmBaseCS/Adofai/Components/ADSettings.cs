using System;
using System.Collections.Generic;
using RhythmBase.Adofai.Events;
using RhythmBase.Components;
using RhythmBase.Components.Easing;
namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Represents the settings for an ADOFAI (A Dance of Fire and Ice) level.
	/// </summary>
	public class ADSettings
	{		/// <summary>
		/// Gets or sets the version of the level.
		/// </summary>
		public int Version { get; set; }		/// <summary>
		/// Gets or sets the name of the artist of the song.
		/// </summary>
		public string Artist { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the special type of the artist.
		/// </summary>
		public SpecialArtistTypes SpecialArtistType { get; set; }		/// <summary>
		/// Gets or sets the permission details for the artist's work.
		/// </summary>
		public string ArtistPermission { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the name of the song.
		/// </summary>
		public string Song { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the author of the level.
		/// </summary>
		public string Author { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets a value indicating whether the countdown time is separate.
		/// </summary>
		public bool SeparateCountdownTime { get; set; }		/// <summary>
		/// Gets or sets the preview image for the level.
		/// </summary>
		public string PreviewImage { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the preview icon for the level.
		/// </summary>
		public string PreviewIcon { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the color of the preview icon.
		/// </summary>
		public RDColor PreviewIconColor { get; set; }		/// <summary>
		/// Gets or sets the start time of the preview song in seconds.
		/// </summary>
		public float PreviewSongStart { get; set; }		/// <summary>
		/// Gets or sets the duration of the preview song in seconds.
		/// </summary>
		public float PreviewSongDuration { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether the level has a seizure warning.
		/// </summary>
		public bool SeizureWarning { get; set; }		/// <summary>
		/// Gets or sets the description of the level.
		/// </summary>
		public string LevelDesc { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the tags associated with the level.
		/// </summary>
		public string LevelTags { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the links to the artist's work or profile.
		/// </summary>
		public string ArtistLinks { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the difficulty level of the level.
		/// </summary>
		public int Difficulty { get; set; }		/// <summary>
		/// Gets or sets the list of required mods for the level.
		/// </summary>
		public List<string> RequiredMods { get; set; } = [];		/// <summary>
		/// Gets or sets the filename of the song.
		/// </summary>
		public string SongFilename { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the beats per minute (BPM) of the song.
		/// </summary>
		public float Bpm { get; set; }		/// <summary>
		/// Gets or sets the volume of the song.
		/// </summary>
		public float Volume { get; set; }		/// <summary>
		/// Gets or sets the offset of the song in seconds.
		/// </summary>
		public float Offset { get; set; }		/// <summary>
		/// Gets or sets the pitch of the song.
		/// </summary>
		public float Pitch { get; set; }		/// <summary>
		/// Gets or sets the hitsound used in the level.
		/// </summary>
		public string Hitsound { get; set; } = "Kick";		/// <summary>
		/// Gets or sets the volume of the hitsound.
		/// </summary>
		public float HitsoundVolume { get; set; }		/// <summary>
		/// Gets or sets the number of countdown ticks before the level starts.
		/// </summary>
		public float CountdownTicks { get; set; }		/// <summary>
		/// Gets or sets the type of track color used in the level.
		/// </summary>
		public TrackColorType TrackColorType { get; set; }		/// <summary>
		/// Gets or sets the primary track color.
		/// </summary>
		public RDColor TrackColor { get; set; }		/// <summary>
		/// Gets or sets the secondary track color.
		/// </summary>
		public RDColor SecondaryTrackColor { get; set; }		/// <summary>
		/// Gets or sets the duration of the track color animation in seconds.
		/// </summary>
		public float TrackColorAnimDuration { get; set; }		/// <summary>
		/// Gets or sets the type of track color pulse.
		/// </summary>
		public ADTrackColorPulses TrackColorPulse { get; set; }		/// <summary>
		/// Gets or sets the length of the track pulse.
		/// </summary>
		public float TrackPulseLength { get; set; }		/// <summary>
		/// Gets or sets the style of the track.
		/// </summary>
		public TrackStyle TrackStyle { get; set; }		/// <summary>
		/// Gets or sets the type of track animation.
		/// </summary>
		public TrackAnimationTypes TrackAnimation { get; set; }		/// <summary>
		/// Gets or sets the number of beats ahead for the track.
		/// </summary>
		public int BeatsAhead { get; set; }		/// <summary>
		/// Gets or sets the type of track disappear animation.
		/// </summary>
		public ADTrackDisappearAnimationTypes TrackDisappearAnimation { get; set; }		/// <summary>
		/// Gets or sets the number of beats behind for the track.
		/// </summary>
		public int BeatsBehind { get; set; }		/// <summary>
		/// Gets or sets the background color of the level.
		/// </summary>
		public RDColor BackgroundColor { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether to show the default background if no image is provided.
		/// </summary>
		public bool ShowDefaultBGIfNoImage { get; set; }		/// <summary>
		/// Gets or sets the background image for the level.
		/// </summary>
		public string BgImage { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the color of the background image.
		/// </summary>
		public RDColor BgImageColor { get; set; }		/// <summary>
		/// Gets or sets the parallax effect for the background.
		/// </summary>
		public RDPointI Parallax { get; set; }		/// <summary>
		/// Gets or sets the display mode of the background.
		/// </summary>
		public BgDisplayModes BgDisplayMode { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether the rotation is locked.
		/// </summary>
		public bool LockRot { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether the background loops.
		/// </summary>
		public bool LoopBG { get; set; }		/// <summary>
		/// Gets or sets the scaling ratio for the background.
		/// </summary>
		public float ScalingRatio { get; set; }		/// <summary>
		/// Gets or sets the reference point for the camera.
		/// </summary>
		public ADCameraRelativeTo RelativeTo { get; set; }		/// <summary>
		/// Gets or sets the position of the camera.
		/// </summary>
		public RDPointI Position { get; set; }		/// <summary>
		/// Gets or sets the rotation of the camera.
		/// </summary>
		public float Rotation { get; set; }		/// <summary>
		/// Gets or sets the zoom level of the camera.
		/// </summary>
		public float Zoom { get; set; }		/// <summary>
		/// Gets or sets the background video for the level.
		/// </summary>
		public string BgVideo { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets a value indicating whether the video loops.
		/// </summary>
		public bool LoopVideo { get; set; }		/// <summary>
		/// Gets or sets the offset of the video in milliseconds.
		/// </summary>
		public int VidOffset { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether floor icons have outlines.
		/// </summary>
		public bool FloorIconOutlines { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether the camera sticks to floors.
		/// </summary>
		public bool StickToFloors { get; set; }		/// <summary>
		/// Gets or sets the easing type for the planet animation.
		/// </summary>
		public EaseType PlanetEase { get; set; }		/// <summary>
		/// Gets or sets the number of parts for the planet easing.
		/// </summary>
		public int PlanetEaseParts { get; set; }		/// <summary>
		/// Gets or sets the behavior of the planet easing parts.
		/// </summary>
		public ADEasePartBehaviors PlanetEasePartBehavior { get; set; }		/// <summary>
		/// Gets or sets the default text color.
		/// </summary>
		public RDColor DefaultTextColor { get; set; }		/// <summary>
		/// Gets or sets the default text shadow color.
		/// </summary>
		public RDColor DefaultTextShadowColor { get; set; }		/// <summary>
		/// Gets or sets the congratulatory text displayed at the end of the level.
		/// </summary>
		public string CongratsText { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets the text displayed for a perfect score.
		/// </summary>
		public string PerfectText { get; set; } = string.Empty;		/// <summary>
		/// Gets or sets a value indicating whether legacy flash effects are enabled.
		/// </summary>
		public bool LegacyFlash { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether the camera uses legacy relative-to settings.
		/// </summary>
		public bool LegacyCamRelativeTo { get; set; }		/// <summary>
		/// Gets or sets a value indicating whether legacy sprite tiles are used.
		/// </summary>
		public bool LegacySpriteTiles { get; set; }
	}
}
