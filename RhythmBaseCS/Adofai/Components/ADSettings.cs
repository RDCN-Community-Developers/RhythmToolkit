using System;
using System.Collections.Generic;
using RhythmBase.Adofai.Events;
using RhythmBase.Components;
using SkiaSharp;

namespace RhythmBase.Adofai.Components
{

	public class ADSettings
	{

		public ADSettings()
		{
			RequiredMods = [];
		}


		public int Version { get; set; }


		public string Artist { get; set; }


		public SpecialArtistTypes SpecialArtistType { get; set; }


		public string ArtistPermission { get; set; }


		public string Song { get; set; }


		public string Author { get; set; }


		public bool SeparateCountdownTime { get; set; }


		public string PreviewImage { get; set; }


		public string PreviewIcon { get; set; }


		public SKColor PreviewIconColor { get; set; }


		public float PreviewSongStart { get; set; }


		public float PreviewSongDuration { get; set; }


		public bool SeizureWarning { get; set; }


		public string LevelDesc { get; set; }


		public string LevelTags { get; set; }


		public string ArtistLinks { get; set; }


		public int Difficulty { get; set; }


		public List<string> RequiredMods { get; set; }


		public string SongFilename { get; set; }


		public float Bpm { get; set; }


		public float Volume { get; set; }


		public float Offset { get; set; }


		public float Pitch { get; set; }


		public string Hitsound { get; set; }


		public float HitsoundVolume { get; set; }


		public float CountdownTicks { get; set; }


		public ADTrackColorTypes TrackColorType { get; set; }


		public SKColor TrackColor { get; set; }


		public SKColor SecondaryTrackColor { get; set; }


		public float TrackColorAnimDuration { get; set; }


		public ADTrackColorPulses TrackColorPulse { get; set; }


		public float TrackPulseLength { get; set; }


		public ADTrackStyles TrackStyle { get; set; }


		public ADTrackAnimationTypes TrackAnimation { get; set; }


		public int BeatsAhead { get; set; }


		public ADTrackDisappearAnimationTypes TrackDisappearAnimation { get; set; }


		public int BeatsBehind { get; set; }


		public SKColor BackgroundColor { get; set; }


		public bool ShowDefaultBGIfNoImage { get; set; }


		public string BgImage { get; set; }


		public SKColor BgImageColor { get; set; }


		public RDPointI Parallax { get; set; }


		public BgDisplayModes BgDisplayMode { get; set; }


		public bool LockRot { get; set; }


		public bool LoopBG { get; set; }


		public float ScalingRatio { get; set; }


		public ADCameraRelativeTo RelativeTo { get; set; }


		public RDPointI Position { get; set; }


		public float Rotation { get; set; }


		public float Zoom { get; set; }


		public string BgVideo { get; set; }


		public bool LoopVideo { get; set; }


		public int VidOffset { get; set; }


		public bool FloorIconOutlines { get; set; }


		public bool StickToFloors { get; set; }


		public Ease.EaseType PlanetEase { get; set; }


		public int PlanetEaseParts { get; set; }


		public ADEasePartBehaviors PlanetEasePartBehavior { get; set; }


		public SKColor DefaultTextColor { get; set; }


		public SKColor DefaultTextShadowColor { get; set; }


		public string CongratsText { get; set; }


		public string PerfectText { get; set; }


		public bool LegacyFlash { get; set; }


		public bool LegacyCamRelativeTo { get; set; }


		public bool LegacySpriteTiles { get; set; }
	}
}
