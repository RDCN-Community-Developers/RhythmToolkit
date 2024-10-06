using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// level settings.
	/// </summary>
	public class Settings
	{
		public Settings()
		{
			Version = 60;
			Artist = "";
			Song = "";
			SpecialArtistType = SpecialArtistTypes.None;
			ArtistPermission = "";
			ArtistLinks = "";
			Author = "";
			Difficulty = DifficultyLevel.Easy;
			SeizureWarning = false;
			PreviewImage = "";
			SyringeIcon = "";
			PreviewSong = "";
			Description = "";
			Tags = "";
			Separate2PLevelFilename = "";
			CanBePlayedOn = LevelPlayedMode.OnePlayerOnly;
			FirstBeatBehavior = FirstBeatBehaviors.RunNormally;
			MultiplayerAppearance = MultiplayerAppearances.HorizontalStrips;
			LevelVolume = 1f;
			RankMaxMistakes = new LimitedList<int>(4U, 20);
			RankDescription = new LimitedList<string>(6U, "");
		}
		/// <summary>
		/// The version number of the level.
		/// The minimum level version number supported by this library is 55.
		/// </summary>
		public int Version { get; set; }
		/// <summary>
		/// Song artist.
		/// </summary>
		public string Artist { get; set; }
		/// <summary>
		/// Song name.
		/// </summary>
		public string Song { get; set; }
		/// <summary>
		/// Special artlist type
		/// </summary>
		public SpecialArtistTypes SpecialArtistType { get; set; }
		/// <summary>
		/// File path for proof of artist's permission.
		/// </summary>
		public string ArtistPermission { get; set; }
		/// <summary>
		/// Artlist links.
		/// </summary>
		public string ArtistLinks { get; set; }
		/// <summary>
		/// Level author.
		/// </summary>
		public string Author { get; set; }
		/// <summary>
		/// Level difficulty.
		/// </summary>
		public DifficultyLevel Difficulty { get; set; }
		/// <summary>
		/// Show seizure warning.
		/// </summary>
		public bool SeizureWarning { get; set; }
		/// <summary>
		/// Preview image file path.
		/// </summary>
		public string PreviewImage { get; set; }
		/// <summary>
		/// Syringe packaging image file path.
		/// </summary>
		public string SyringeIcon { get; set; }
		/// <summary>
		/// The file path of the music used for previewing.
		/// </summary>
		public string PreviewSong { get; set; }
		/// <summary>
		/// Start time of preview music.
		/// </summary>
		[JsonConverter(typeof(SecondConverter))]
		public TimeSpan PreviewSongStartTime { get; set; }
		/// <summary>
		/// Duration of preview music.
		/// </summary>
		[JsonConverter(typeof(SecondConverter))]
		public TimeSpan PreviewSongDuration { get; set; }
		/// <summary>
		/// Hue offset or grayscale of the level name on the syringe.
		/// </summary>
		[JsonProperty("songNameHue")]
		public float SongNameHueOrGrayscale { get; set; }
		/// <summary>
		/// Whether grayscale is enabled.
		/// </summary>
		public bool SongLabelGrayscale { get; set; }
		/// <summary>
		/// Level Description.
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// Level labels.
		/// </summary>
		public string Tags { get; set; }
		/// <summary>
		/// Separate two-player level file paths.
		/// It Is uncertain if this attribute Is still being used.
		/// </summary>
		public string Separate2PLevelFilename { get; set; }
		/// <summary>
		/// Level play mode.
		/// </summary>
		public LevelPlayedMode CanBePlayedOn { get; set; }
		/// <summary>
		/// Behavior of the first beat of the level.
		/// </summary>
		public FirstBeatBehaviors FirstBeatBehavior { get; set; }

		public MultiplayerAppearances MultiplayerAppearance { get; set; }
		/// <summary>
		/// A percentage value indicating the total volume of the level.
		/// </summary>
		public float LevelVolume { get; set; }
		/// <summary>
		/// Maximum number of mistakes per rank
		/// </summary>
		public LimitedList<int> RankMaxMistakes { get; set; }
		/// <summary>
		/// Description of each rank
		/// </summary>
		public LimitedList<string> RankDescription { get; set; }
		/// <summary>
		/// Mods enabled for the level.
		/// </summary>
		/// oldBassDrop
		/// startImmediately
		/// classicHitParticles
		/// adaptRowsToRoomHeight
		/// noSmartJudgment
		/// smoothShake
		/// rotateShake
		/// wobblyLines
		/// bombBeats
		/// noDoublePulse
		/// invisibleCharacters
		/// gentleBassDrop
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Mods { get; set; }
		/// <summary>
		/// Difficulty level of the level.
		/// </summary>
		public enum DifficultyLevel
		{
			Easy,
			Medium,
			Tough,
			VeryTough
		}
		/// <summary>
		/// Play mode of the level.
		/// </summary>
		public enum LevelPlayedMode
		{
			OnePlayerOnly,
			TwoPlayerOnly,
			BothModes
		}
		/// <summary>
		/// Behavior of the first beat of the level.
		/// </summary>
		public enum FirstBeatBehaviors
		{
			RunNormally,
			RunEventsOnPrebar
		}

		public enum MultiplayerAppearances
		{
			HorizontalStrips,
			Nothing
		}
	}
}
