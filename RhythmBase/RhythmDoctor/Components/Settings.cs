using System.Text.Json;
namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>  
	/// Level settings.  
	/// </summary>  
	public class Settings
	{
		private int[] rankMaxMistakes = new int[4];
		private string[] rankDescription = ["", "", "", "", "", ""];
		/// <summary>  
		/// Level settings.  
		/// </summary>  
		public Settings()
		{
			Version = 61;
			Artist = "";
			Song = "";
			SpecialArtistType = SpecialArtistTypes.None;
			ArtistPermission = "";
			ArtistLinks = "";
			Author = "";
			Difficulty = DifficultyLevel.Medium;
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
		}
		/// <summary>  
		/// The version number of the level.  
		/// The minimum level version number supported by this library is 55.  
		/// </summary>  
		public int Version { get; set; } = GlobalSettings.DefaultVersionRhythmDoctor;
		/// <summary>  
		/// Song artist.  
		/// </summary>  
		public string Artist { get; set; }
		/// <summary>  
		/// Song name.  
		/// </summary>  
		public string Song { get; set; }
		/// <summary>  
		/// Special artist type.  
		/// </summary>  
		public SpecialArtistTypes SpecialArtistType { get; set; }
		/// <summary>  
		/// File path for proof of artist's permission.  
		/// </summary>  
		public string ArtistPermission { get; set; }
		/// <summary>  
		/// Artist links.  
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
		public TimeSpan PreviewSongStartTime { get; set; }
		/// <summary>  
		/// Duration of preview music.  
		/// </summary>  
		public TimeSpan PreviewSongDuration { get; set; }
		/// <summary>  
		/// Hue offset or grayscale of the level name on the syringe.  
		/// </summary>  
		public float SongNameHueOrGrayscale { get; set; }
		/// <summary>  
		/// Whether grayscale is enabled.  
		/// </summary>  
		public bool SongLabelGrayscale { get; set; }
		/// <summary>  
		/// Level description.  
		/// </summary>  
		public string Description { get; set; }
		/// <summary>  
		/// Level tags.  
		/// </summary>  
		public string Tags { get; set; }
		/// <summary>  
		/// Separate two-player level file paths.  
		/// It is uncertain if this attribute is still being used.  
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
		/// <summary>  
		/// Appearance of the level in multiplayer mode.  
		/// </summary>  
		public MultiplayerAppearances MultiplayerAppearance { get; set; }
		/// <summary>  
		/// A percentage value indicating the total volume of the level.  
		/// </summary>  
		public float LevelVolume { get; set; }
		/// <summary>  
		/// Maximum number of mistakes per rank.  
		/// </summary>  
		public int[] RankMaxMistakes
		{
			get => rankMaxMistakes;
			set => rankMaxMistakes = value.Length == 4 ? value : throw new RhythmBaseException();
		}
		/// <summary>  
		/// Description of each rank.  
		/// </summary>  
		public string[] RankDescription
		{
			get => rankDescription;
			set => rankDescription = value.Length == 6 ? value : throw new RhythmBaseException();
		}
		/// <summary>  
		/// Mods enabled for the level.  
		/// </summary>  
		public List<string> Mods { get; set; } = [];
		/// <summary>
		/// Represents a collection of additional data as key-value pairs, where the keys are strings and the values are JSON
		/// elements.
		/// </summary>
		/// <remarks>This dictionary can be used to store extra information that is not explicitly defined in the
		/// primary data model. The keys must be unique, and the values are represented as <see
		/// cref="System.Text.Json.JsonElement"/> objects, allowing for flexible data storage.</remarks>
		public Dictionary<string, JsonElement> ExtraData = [];
	}
	/// <summary>  
	/// Difficulty level of the level.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum DifficultyLevel
	{
		/// <summary>  
		/// Easy difficulty.  
		/// </summary>  
		Easy,
		/// <summary>  
		/// Medium difficulty.  
		/// </summary>  
		Medium,
		/// <summary>  
		/// Tough difficulty.  
		/// </summary>  
		Tough,
		/// <summary>  
		/// Very tough difficulty.  
		/// </summary>  
		VeryTough
	}
	/// <summary>  
	/// Play mode of the level.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum LevelPlayedMode
	{
		/// <summary>  
		/// Can be played by one player only.  
		/// </summary>  
		OnePlayerOnly,
		/// <summary>  
		/// Can be played by two players only.  
		/// </summary>  
		TwoPlayerOnly,
		/// <summary>  
		/// Can be played in both one-player and two-player modes.  
		/// </summary>  
		BothModes
	}
	/// <summary>  
	/// Behavior of the first beat of the level.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum FirstBeatBehaviors
	{
		/// <summary>  
		/// Run normally.  
		/// </summary>  
		RunNormally,
		/// <summary>  
		/// Run events on prebar.  
		/// </summary>  
		RunEventsOnPrebar
	}
	/// <summary>  
	/// Appearance of the level in multiplayer mode.  
	/// </summary>  
	[RDJsonEnumSerializable]
	public enum MultiplayerAppearances
	{
		/// <summary>  
		/// Horizontal strips appearance.  
		/// </summary>  
		HorizontalStrips,
		/// <summary>  
		/// No special appearance.  
		/// </summary>  
		Nothing
	}
}
