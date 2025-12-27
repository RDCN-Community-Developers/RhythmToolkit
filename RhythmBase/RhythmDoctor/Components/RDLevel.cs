using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Utils;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// Rhythm Doctor level.
	/// </summary>
	public class RDLevel : OrderedEventCollection<IBaseEvent>, IDisposable
	{
		/// <inheritdoc/>
		public override int Count => base.Count;
		/// <summary>
		/// The calculator that comes with the level.
		/// </summary>
		[JsonIgnore]
		public BeatCalculator Calculator { get; }
		/// <summary>
		/// Level Settings.
		/// </summary>
		public Settings Settings { get; set; }
		/// <summary>
		/// Level tile collection.
		/// </summary>
		public RowCollection Rows { get; }
		/// <summary>
		/// Level decoration collection.
		/// </summary>
		public DecorationCollection Decorations { get; }
		/// <summary>
		/// Level condition collection.
		/// </summary>
		public ConditionalCollection Conditionals { get; }
		/// <summary>
		/// Level bookmark collection.
		/// </summary>
		public List<Bookmark> Bookmarks { get; }
		/// <summary>
		/// Level colorPalette collection.
		/// </summary>
		public RDColor[] ColorPalette
		{
			get => colorPalette;
			internal set => colorPalette = value.Length == 21 ? value : throw new RhythmBaseException();
		}
		/// <summary>
		/// Gets the file path of the source file associated with the current operation.
		/// </summary>
		public string SourceFilePath { get; internal set; } = string.Empty;
		/// <summary>
		/// Level file path.
		/// </summary>
		[JsonIgnore]
		public string? Filepath { get; internal set; }
		/// <summary>
		/// Level directory path.
		/// </summary>
		[JsonIgnore]
		public string? Directory => Path.GetDirectoryName(Filepath);
		/// <summary>
		/// Default beats with levels.
		/// The beat is 1.
		/// </summary>
		[JsonIgnore]
		public RDBeat DefaultBeat => Calculator.BeatOf(1f);
		/// <summary>
		/// Initializes a new instance of the <see cref="RDLevel"/> class.
		/// </summary>
		public RDLevel()
		{
			Variables = new RDVariables();
			Calculator = new BeatCalculator(this);
			Settings = new Settings();
			Conditionals = [];
			Bookmarks = [];
			ColorPalette = new RDColor[21];
			Rows = new(this);
			Decorations = new(this);
			calculator = Calculator;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="RDLevel"/> class with the specified items.
		/// </summary>
		/// <param name="items">The items to add to the level.</param>
		public RDLevel(IEnumerable<IBaseEvent> items) : this()
		{
			foreach (IBaseEvent item in items)
				Add(item);
		}
		/// <summary>
		/// The default level within the game.
		/// </summary>
		public static RDLevel Default
		{
			get
			{
				RDLevel rdlevel = [];
				rdlevel.ColorPalette =
				[
					RDColor.Black,
					RDColor.White,
					new(0xFF7F7F7Fu),
					new(0xFFC3C3C3u),
					new(0xFF880015u),
					new(0xFFB97A57u),
					RDColor.Red,
					new(0xFFFFAEC9u),
					new(0xFFFF7F27u),
					new(0xFFFFC90Eu),
					new(0xFFFFF200u),
					new(0xFFEFE4B0u),
					new(0xFF22B14Cu),
					new(0xFFB5E61Du),
					new(0xFF00A2E8u),
					new(0xFF99D9EAu),
					new(0xFF3F48CCu),
					new(0xFF7092BEu),
					new(0xFFA349A4u),
					new(0xFFC8BFE7u),
					new(0x00000000u)
				];
				rdlevel.Settings.RankMaxMistakes = [
					20,
					15,
					10,
					5
				];
				rdlevel.Settings.RankDescription = [
					"Better call 911, now!",
					"Ugh, you can do better",
					"Not bad I guess...",
					"We make a good team!",
					"You are really good!",
					"Wow! That's awesome!!"
				];
				PlaySong playsong = new();
				SetTheme settheme = new();
				playsong.Song = new RDAudio() { Filename = "sndOrientalTechno" };
				settheme.Preset = Theme.OrientalTechno;
				rdlevel.AddRange([playsong, settheme]);
				Row samurai = new() { Rooms = RDRoomIndex.Room1, Character = RDCharacters.Samurai };
				rdlevel.Rows.Add(samurai);
				samurai.Sound.Filename = "Shaker";
				samurai.Add(new AddClassicBeat());
				return rdlevel;
			}
		}
		/// <summary>
		/// Creates an <see cref="RDLevel"/> instance by reading data from the specified file.
		/// </summary>
		/// <remarks>This method supports both plain level files (<c>.rdlevel</c>, <c>.json</c>) and compressed
		/// archives (<c>.rdzip</c>, <c>.zip</c>). If the file is a compressed archive, it is extracted to a temporary
		/// directory to locate the level file within the archive.</remarks>
		/// <param name="filepath">The path to the file to read. The file must have one of the following extensions: <c>.rdlevel</c>, <c>.json</c>,
		/// <c>.rdzip</c>, or <c>.zip</c>.</param>
		/// <param name="settings">Optional settings that control how the level is read. If not provided, default settings are used.</param>
		/// <returns>An <see cref="RDLevel"/> instance representing the data read from the file.</returns>
		/// <exception cref="RhythmBaseException">Thrown if the file format is not supported, if no level file is found in the archive, or if an error occurs during
		/// file extraction.</exception>
		public static RDLevel FromFile(string filepath, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			string extension = Path.GetExtension(filepath);
			RDLevel? level;
			if (extension is not ".rdzip" and not ".zip")
			{
				if (extension is not ".rdlevel" and not ".json")
					throw new RhythmBaseException("File not supported.");
				using FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
				level = FromStream(stream, Path.GetDirectoryName(Path.GetFullPath(filepath)) ?? "", settings);
				level.Filepath = level.SourceFilePath = Path.GetFullPath(filepath);
				return level;
			}
			switch (settings.ZipFileProcessMethod)
			{
				case ZipFileProcessMethod.AllFiles:
					DirectoryInfo tempDirectory = new(Path.Combine(GlobalSettings.CachePath, "RhythmBaseTemp_Zip_" + Path.GetRandomFileName()));
					tempDirectory.Create();
					try
					{
#if NET8_0_OR_GREATER
						using Stream stream = File.OpenRead(filepath);
						ZipFile.ExtractToDirectory(stream, tempDirectory.FullName, overwriteFiles: true);
#elif NETSTANDARD2_0_OR_GREATER
						ZipFile.ExtractToDirectory(filepath, tempDirectory.FullName);
#endif
						string? rdlevelPath = null;
						foreach (FileInfo? file in tempDirectory.GetFiles())
						{
							if (file.Extension == ".rdlevel")
							{
								rdlevelPath = file.FullName;
								break;
							}
						}
						if (rdlevelPath == null)
							throw new RhythmBaseException("No RDLevel file has been found.");
						level = FromFile(rdlevelPath, settings);
						level.SourceFilePath = Path.GetFullPath(filepath);
						level.Filepath = Path.GetFullPath(rdlevelPath);
						level.isZip = true;
						level.isExtracted = true;
					}
					catch (Exception ex2)
					{
						tempDirectory.Delete(true);
						throw new RhythmBaseException("Cannot extract the file.", ex2);
					}
					break;
				case ZipFileProcessMethod.LevelFileOnly:
					try
					{
						using FileStream zipStream = new(filepath, FileMode.Open, FileAccess.Read);
						using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);
						ZipArchiveEntry? entry = archive.GetEntry("main.rdlevel") ?? throw new RhythmBaseException("Cannot find the level file.");
						using Stream stream = entry.Open();
						level = FromStream(stream, settings);
						level.SourceFilePath = Path.GetFullPath(filepath);
						level.isZip = true;
						level.isExtracted = false;
					}
					catch (Exception ex2)
					{
						throw new RhythmBaseException("Cannot extract the file.", ex2);
					}
					break;
				default:
					throw new RhythmBaseException($"{settings.ZipFileProcessMethod} is not supported.");
			}
			return level;
		}
		/// <summary>
		/// Asynchronously loads an <see cref="RDLevel"/> instance from a file.
		/// </summary>
		/// <remarks>If the file is a compressed archive (<c>.rdzip</c> or <c>.zip</c>), the method extracts its
		/// contents to a temporary directory and searches for a file with the <c>.rdlevel</c> extension. If no such file is
		/// found, an exception is thrown. The temporary directory is automatically cleaned up after the operation
		/// completes.</remarks>
		/// <param name="filepath">The path to the file to load. The file must have one of the supported extensions: <c>.rdlevel</c>, <c>.json</c>,
		/// <c>.rdzip</c>, or <c>.zip</c>.</param>
		/// <param name="settings">Optional settings that control how the level is read. If <see langword="null"/>, default settings are used.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the loaded <see cref="RDLevel"/>
		/// instance.</returns>
		/// <exception cref="RhythmBaseException">Thrown if the file format is unsupported, if no <c>.rdlevel</c> file is found in a compressed archive, or if an
		/// error occurs during extraction.</exception>
		public static async Task<RDLevel> FromFileAsync(string filepath, LevelReadOrWriteSettings? settings = null, CancellationToken cancellationToken = default)
		{
			settings ??= new();
			string extension = Path.GetExtension(filepath);
			RDLevel? level;
			if (extension is not ".rdzip" and not ".zip")
			{
				if (extension is not ".rdlevel" and not ".json")
					throw new RhythmBaseException("File not supported.");
				using FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
				level = await FromStreamAsync(stream, settings, cancellationToken);
				level.Filepath = level.SourceFilePath = Path.GetFullPath(filepath);
			}
			switch (settings.ZipFileProcessMethod)
			{
				case ZipFileProcessMethod.AllFiles:
					DirectoryInfo tempDirectory = new(Path.Combine(GlobalSettings.CachePath, "RhythmBaseTemp_Zip_" + Path.GetRandomFileName()));
					tempDirectory.Create();
					try
					{
#if NET8_0_OR_GREATER
						using Stream stream = File.OpenRead(filepath);
						ZipFile.ExtractToDirectory(stream, tempDirectory.FullName, overwriteFiles: true);
#elif NETSTANDARD2_0_OR_GREATER
						ZipFile.ExtractToDirectory(filepath, tempDirectory.FullName);
#endif
						string? rdlevelPath = null;
						foreach (FileInfo? file in tempDirectory.GetFiles())
						{
							if (file.Name == "main.rdlevel")
							{
								rdlevelPath = file.FullName;
								break;
							}
						}
						if (rdlevelPath == null)
							throw new RhythmBaseException("No RDLevel file has been found.");
						level = await FromFileAsync(rdlevelPath, settings, cancellationToken);
						level.SourceFilePath = Path.GetFullPath(filepath);
						level.Filepath = Path.GetFullPath(rdlevelPath);
						level.isZip = true;
						level.isExtracted = true;
					}
					catch (Exception ex2)
					{
						tempDirectory.Delete(true);
						throw new RhythmBaseException("Cannot extract the file.", ex2);
					}
					break;
				case ZipFileProcessMethod.LevelFileOnly:
					try
					{
						using FileStream zipStream = new(filepath, FileMode.Open, FileAccess.Read);
						using ZipArchive archive = new(zipStream, ZipArchiveMode.Read);
						ZipArchiveEntry? entry = archive.GetEntry("main.rdlevel") ?? throw new RhythmBaseException("Cannot find the level file.");
						using Stream stream = entry.Open();
						level = await FromStreamAsync(stream, settings, cancellationToken);
						level.SourceFilePath = Path.GetFullPath(filepath);
						level.isZip = true;
						level.isExtracted = false;
					}
					catch (Exception ex2)
					{
						throw new RhythmBaseException("Cannot extract the file.", ex2);
					}
					break;
				default:
					throw new RhythmBaseException(extension + " is not supported.");
			}
			return level;
		}
		/// <summary>
		/// Reads a level from a stream.
		/// </summary>
		/// <param name="rdlevelStream">The stream containing the level data.</param>
		/// <param name="settings">Optional settings for reading the level.</param>
		/// <returns>An <see cref="RDLevel"/> instance loaded from the stream.</returns>
		public static RDLevel FromStream(Stream rdlevelStream, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			RDLevel? level;
			settings.OnBeforeReading();
			using EscapeSpecialCharacterStream stream = new(rdlevelStream);
			level = JsonSerializer.Deserialize<RDLevel>(stream, options);
			settings.OnAfterReading();
			return level ?? [];
		}
		private static RDLevel FromStream(Stream rdlevelStream, string dirPath, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(dirPath, settings);
			RDLevel? level;
			settings.OnBeforeReading();
			using EscapeSpecialCharacterStream stream = new(rdlevelStream);
			level = JsonSerializer.Deserialize<RDLevel>(stream, options);
			settings.OnAfterReading();
			return level ?? [];
		}
		/// <summary>
		/// Asynchronously reads a level from a stream.
		/// </summary>
		/// <param name="rdlevelStream">The stream containing the level data.</param>
		/// <param name="settings">Optional settings for reading the level.</param>
		/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
		/// <returns>A <see cref="Task{RDLevel}"/> representing the asynchronous operation, with an <see cref="RDLevel"/> instance loaded from the stream.</returns>
		public static async Task<RDLevel> FromStreamAsync(Stream rdlevelStream, LevelReadOrWriteSettings? settings = null, CancellationToken cancellationToken = default)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			RDLevel? level;
			settings.OnBeforeReading();
			using EscapeSpecialCharacterStream stream = new(rdlevelStream);
			level = await JsonSerializer.DeserializeAsync<RDLevel>(stream, options, cancellationToken);
			settings.OnAfterReading();
			return level ?? [];
		}
		/// <summary>
		/// Reads a level from a JSON string.
		/// </summary>
		/// <param name="json">The JSON string containing the level data.</param>
		/// <param name="settings">Optional settings for reading the level.</param>
		/// <returns>An <see cref="RDLevel"/> instance loaded from the JSON string.</returns>
		public static RDLevel FromJsonString(string json, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			RDLevel? level;
			settings.OnBeforeWriting();
			level = JsonSerializer.Deserialize<RDLevel>(json, options);
			settings.OnAfterWriting();
			return level ?? [];
		}
		/// <summary>
		/// Saves the current level to the specified stream in JSON format.
		/// </summary>
		/// <param name="stream">The stream to which the level will be saved.</param>
		/// <param name="settings">Optional settings for writing the level. If null, default settings are used.</param>
		public void SaveToStream(Stream stream, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			settings.OnBeforeWriting();
			JsonSerializer.Serialize(stream, this, options);
			settings.OnAfterWriting();
		}
		/// <summary>
		/// Asynchronously saves the current level to the specified stream in JSON format.
		/// </summary>
		/// <param name="stream">The stream to which the level will be saved.</param>
		/// <param name="settings">Optional settings for writing the level. If null, default settings are used.</param>
		/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
		public async void SaveToStreamAsync(Stream stream, LevelReadOrWriteSettings? settings = null, CancellationToken cancellationToken = default)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			settings.OnBeforeWriting();
			await JsonSerializer.SerializeAsync(stream, this, options, cancellationToken);
			settings.OnAfterWriting();
		}
		/// <summary>
		/// Saves the current level to a file in JSON format.
		/// </summary>
		/// <param name="filepath">The file path where the level will be saved.</param>
		/// <param name="settings">Optional settings for writing the level. If null, default settings are used.</param>
		public void SaveToFile(string filepath, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(Path.GetDirectoryName(Filepath) ?? "", settings);
			DirectoryInfo directory = new FileInfo(filepath).Directory ?? new("");
			if (!directory.Exists)
				directory.Create();
			settings.OnBeforeWriting();
			using (FileStream stream = File.Open(filepath, FileMode.OpenOrCreate, FileAccess.Write))
			{
				stream.SetLength(0);
				JsonSerializer.Serialize(stream, this, options);
			}
			settings.OnAfterWriting();
		}
		/// <summary>
		/// Asynchronously saves the current level to a file in JSON format.
		/// </summary>
		/// <param name="filepath">The file path where the level will be saved.</param>
		/// <param name="settings">Optional settings for writing the level. If null, default settings are used.</param>
		/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
		public async void SaveToFileAsync(string filepath, LevelReadOrWriteSettings? settings = null, CancellationToken cancellationToken = default)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(Path.GetDirectoryName(filepath) ?? "", settings);
			DirectoryInfo directory = new FileInfo(filepath).Directory ?? new("");
			if (!directory.Exists)
				directory.Create();
			settings.OnBeforeWriting();
			using (FileStream stream = File.Open(filepath, FileMode.OpenOrCreate, FileAccess.Write))
			{
				stream.SetLength(0);
				await JsonSerializer.SerializeAsync(stream, this, options, cancellationToken);
			}
			settings.OnAfterWriting();
		}
		/// <summary>
		/// Saves the current level data to a file in packed (ZIP) format at the specified path. (This method is not fully implemented yet.)
		/// </summary>
		/// <param name="filepath">The path of the file to create and write the packed level data to. Must not be null or empty.</param>
		/// <param name="settings">Optional settings that control how the level data is written. If null, default settings are used.</param>
		public void SaveToZip(string filepath, LevelReadOrWriteSettings? settings = null)
		{
			if (string.IsNullOrEmpty(this.Directory))
				throw new NotImplementedException();
			settings ??= new();
			settings.FileReferences.Clear();
			bool loadAssets = settings.LoadAssets;
			settings.LoadAssets = true;
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(Path.GetDirectoryName(Filepath) ?? "", settings);
			DirectoryInfo directory = new FileInfo(filepath).Directory ?? new("");
			if (!directory.Exists)
				directory.Create();
			settings.OnBeforeWriting();
			using Stream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
			ZipArchive archive = new(stream, ZipArchiveMode.Create);
			ZipArchiveEntry entry = archive.CreateEntry("main.rdlevel");
			using (Stream rdlevelStream = entry.Open())
			{
				JsonSerializer.Serialize(rdlevelStream, this, options);
			}
			foreach (var file in settings.FileReferences)
			{
				archive.CreateEntryFromFile(Path.Combine(Directory, file.Path), Path.GetFileName(file.Path));
			}
			archive.Dispose();
			settings.OnAfterWriting();
			settings.LoadAssets = loadAssets;
		}
		/// <summary>
		/// Asynchronously saves the current level and its associated assets to a ZIP archive at the specified file path.
		/// </summary>
		/// <remarks>The resulting ZIP archive will contain the main level data as a JSON file and any referenced
		/// asset files. This method is asynchronous but returns void; exceptions will be thrown on the calling thread if the
		/// operation fails.</remarks>
		/// <param name="filepath">The full path to the ZIP file to create. If the file exists, it will be overwritten.</param>
		/// <param name="settings">Optional settings that control how the level and its assets are serialized. If null, default settings are used.</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the save operation.</param>
		/// <exception cref="NotImplementedException">Thrown if the level's directory is not set.</exception>
		public async void SaveToZipAsync(string filepath, LevelReadOrWriteSettings? settings = null, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrEmpty(this.Directory))
				throw new NotImplementedException();
			settings ??= new();
			settings.FileReferences.Clear();
			bool loadAssets = settings.LoadAssets;
			settings.LoadAssets = true;
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(Path.GetDirectoryName(Filepath) ?? "", settings);
			DirectoryInfo directory = new FileInfo(filepath).Directory ?? new("");
			if (!directory.Exists)
				directory.Create();
			settings.OnBeforeWriting();
			using Stream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
			ZipArchive archive = new(stream, ZipArchiveMode.Create);
			ZipArchiveEntry entry = archive.CreateEntry("main.rdlevel");
			using (Stream rdlevelStream = entry.Open())
			{
				await JsonSerializer.SerializeAsync(rdlevelStream, this, options, cancellationToken);
			}
			foreach (var file in settings.FileReferences)
			{
				archive.CreateEntryFromFile(Path.Combine(Directory, file.Path), Path.GetFileName(file.Path));
			}
			archive.Dispose();
			settings.OnAfterWriting();
			settings.LoadAssets = loadAssets;
		}
		/// <summary>
		/// Serializes the current level to a JSON string.
		/// </summary>
		/// <param name="settings">Optional settings for writing the level. If null, default settings are used.</param>
		/// <returns>A JSON string representing the current level.</returns>
		public string ToJsonString(LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			string json;
			settings.OnBeforeWriting();
			json = JsonSerializer.Serialize(this, options);
			settings.OnAfterWriting();
			return json;
		}
		/// <summary>
		/// Serializes the current level to a <see cref="JsonDocument"/>.
		/// </summary>
		/// <param name="settings">
		/// Optional settings for writing the level. If <c>null</c>, default settings are used.
		/// </param>
		/// <returns>
		/// A <see cref="JsonDocument"/> representing the current level in JSON format.
		/// </returns>
		public JsonDocument ToJsonDocument(LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			string json;
			settings.OnBeforeWriting();
			json = JsonSerializer.Serialize(this, options);
			settings.OnAfterWriting();
			return JsonDocument.Parse(json);
		}
		#region obsolete
		/// <summary>
		/// Read from file as level.
		/// Supports .rdlevel, .rdzip, .zip file extension.
		/// </summary>
		/// <param name="filepath">File path.</param>
		/// <param name="settings">Input settings.</param>
		/// <exception cref="T:RhythmBase.Exceptions.VersionTooLowException">The minimum level version number supported by this library is 54.</exception>
		/// <exception cref="T:RhythmBase.Exceptions.ConvertingException"></exception>
		/// <exception cref="T:RhythmBase.Exceptions.RhythmBaseException">File not supported.</exception>
		/// <returns>An instance of a level that reads from a file.</returns>
		[Obsolete("Use FromFile instead.", false)]
		public static RDLevel Read(string filepath, LevelReadOrWriteSettings? settings = null) => FromFile(filepath, settings);
		/// <summary>
		/// Read from a zip file as a level.
		/// </summary>
		/// <param name="filepath">The path to the zip file.</param>
		/// <returns>An instance of RDLevel that reads from a zip file.</returns>
		[Obsolete("Use FromFile instead.", true)]
		public static RDLevel ReadFromZip(string filepath) => throw new NotImplementedException();
		/// <summary>
		/// Read from a zip file as a level with specific settings.
		/// </summary>
		/// <param name="filepath">The path to the zip file.</param>
		/// <param name="settings">The settings for reading the level.</param>
		/// <returns>An instance of RDLevel that reads from a zip file with specific settings.</returns>
		[Obsolete("Use FromFile instead.", true)]
		public static RDLevel ReadFromZip(string filepath, LevelReadOrWriteSettings settings) => throw new NotImplementedException();
		/// <summary>
		/// Read from a zip file as a level.
		/// </summary>
		/// <param name="stream">The stream of the zip file.</param>
		/// <returns>An instance of RDLevel that reads from a zip file.</returns>
		[Obsolete("This method is not implemented. Use FromFile instead.", true)]
		public static RDLevel ReadFromZip(Stream stream) => throw new NotImplementedException();
		/// <summary>
		/// Read from a zip file as a level with specific settings.
		/// </summary>
		/// <param name="stream">The stream of the zip file.</param>
		/// <param name="settings">The settings for reading the level.</param>
		/// <returns>An instance of RDLevel that reads from a zip file with specific settings.</returns>
		[Obsolete("This method is not implemented. Use FromFile instead.", true)]
		public static RDLevel ReadFromZip(Stream stream, LevelReadOrWriteSettings settings) => throw new NotImplementedException();
		/// <summary>
		/// Save the level.
		/// Use default output settings.
		/// </summary>
		/// <param name="filepath">File path.</param>
		/// <exception cref="T:RhythmBase.Exceptions.OverwriteNotAllowedException">Overwriting is disabled by the settings and a file with the same name already exists.</exception>
		[Obsolete("Use SaveToFile instead.", false)]
		public void Write(string filepath) => SaveToFile(filepath);
		/// <summary>
		/// Save the level.
		/// </summary>
		/// <param name="filepath">File path.</param>
		/// <param name="settings">Output settings.</param>
		/// <exception cref="T:RhythmBase.Exceptions.OverwriteNotAllowedException">Overwriting is disabled by the settings and a file with the same name already exists.</exception>
		[Obsolete("Use SaveToFile instead.", false)]
		public void Write(string filepath, LevelReadOrWriteSettings settings) => SaveToFile(filepath, settings);
		/// <summary>
		/// Save the level to a text writer.
		/// Use default output settings.
		/// </summary>
		/// <param name="stream">The text writer to write the level to.</param>
		[Obsolete("Use SaveToStream instead.", false)]
		public void Write(TextWriter stream) => throw new NotImplementedException();
		/// <summary>
		/// Save the level to a text writer.
		/// </summary>
		/// <param name="stream">The text writer to write the level to.</param>
		/// <param name="settings">The settings for writing the level.</param>
		[Obsolete("Use SaveToStream instead.", false)]
		public void Write(TextWriter stream, LevelReadOrWriteSettings settings) => throw new NotImplementedException();
		/// <summary>
		/// Save the level to a stream.
		/// Use default output settings.
		/// </summary>
		/// <param name = "stream" > The stream to write the level to.</param>
		[Obsolete("Use SaveToStream instead.", false)]
		public void Write(Stream stream) => throw new NotImplementedException();
		/// <summary>
		/// Save the level to a stream.
		/// </summary>
		/// <param name = "stream" > The stream to write the level to.</param>
		/// <param name = "settings" > The settings for writing the level.</param>
		[Obsolete("Use SaveToStream instead.", false)]
		public void Write(Stream stream, LevelReadOrWriteSettings settings) => throw new NotImplementedException();
		/// <summary>
		/// Convert to JObject type.
		/// </summary>
		/// <returns>A JObject type that stores all the data for the level.</returns>
		[Obsolete("Use ToJsonDocument instead.", true)]
		public void ToJObject() => throw new NotImplementedException();
		/// <summary>
		/// Convert to JObject type.
		/// </summary>
		/// <returns>A JObject type that stores all the data for the level.</returns>
		[Obsolete("Use ToJsonDocument instead.", true)]
		public void ToJObject(LevelReadOrWriteSettings settings) => throw new NotImplementedException();
		/// <summary>
		/// Convert to a string that can be read by the game.
		/// Use default output settings.
		/// </summary>
		/// <returns>Level string.</returns>
		[Obsolete("Use ToJson instead.", true)]
		public string ToRDLevelJson() => throw new NotImplementedException();
		/// <summary>
		/// Convert to a string that can be read by the game.
		/// </summary>
		/// <param name = "settings" > Output settings.</param>
		/// <returns>Level string.</returns>
		[Obsolete("Use ToJson instead.", true)]
		public string ToRDLevelJson(LevelReadOrWriteSettings settings) => throw new NotImplementedException();
		#endregion
		/// <summary>
		/// Adds an event to the level.
		/// </summary>
		/// <param name="item">The event to be added.</param>
		public override bool Add(IBaseEvent item) => Add(item, true);
		/// <summary>
		/// Adds an event to the level, with an option to keep the event's position.
		/// </summary>
		/// <param name="item">The event to be added.</param>
		/// <param name="keepPos">Whether to keep the event's position (default is true).</param>
		public bool Add(IBaseEvent item, bool keepPos = true)
		{
			bool success = true;
			// Set the default beat calculator
			((BaseEvent)item)._beat._calculator = Calculator;
			// Some events can only be at the beginning of a bar
			(_, float beat) = ((BaseEvent)item)._beat;
			if (item is IBarBeginningEvent e && beat != 1f)
				throw new IllegalBeatException(e);
			// Update the beat's associated level
			((BaseEvent)item)._beat.ResetCache();
			if (item is Comment comment && comment.Parent == null)
				// Comment events may or may not be in the decoration section
				success &= base.Add(item);
			else if (item is TintRows tintRows && tintRows.Parent == null)
				success &= base.Add(item);
			else if (item is BaseRowAction rowAction)
				// Add to the corresponding row
				success &= AddInternal(rowAction);
			else if (item is BaseDecorationAction decoAction)
				// Add to the corresponding decoration
				success &= AddInternal(decoAction);
			// BPM and CPB
			else if (item is SetCrotchetsPerBar setCrochetsPerBar)
				success &= AddSetCrotchetsPerBarInternal(setCrochetsPerBar, keepPos);
			else if (item is BaseBeatsPerMinute baseBeatsPerMinute)
				success &= AddBaseBeatsPerMinuteInternal(baseBeatsPerMinute);
			// Other events
			else
				success &= base.Add(item);
			if (item is FloatingText floatingText)
				_floatingTexts.Add(floatingText);
			return success;
		}
		/// <summary>
		/// Determines whether the level contains the specified event.
		/// </summary>
		/// <param name="item">The event to check for.</param>
		/// <returns>True if the event is contained in the level; otherwise, false.</returns>
		public override bool Contains(IBaseEvent item) => EventTypeUtils.RowTypes.Contains(item.Type)
			&& Rows.Any((i) => i.Contains(item)) || EventTypeUtils.DecorationTypes.Contains(item.Type) && Decorations.Any((i) => i.Contains(item)) || base.Contains(item);
		/// <summary>
		/// Removes an event from the level.
		/// </summary>
		/// <param name="item">The event to be removed.</param>
		/// <returns>True if the event was successfully removed; otherwise, false.</returns>
		public override bool Remove(IBaseEvent item) => Remove(item, true);
		/// <summary>
		/// Removes an event from the level, with an option to keep the event's position.
		/// </summary>
		/// <param name="item">The event to be removed.</param>
		/// <param name="keepPos">Whether to keep the event's position (default is true).</param>
		/// <returns>True if the event was successfully removed; otherwise, false.</returns>
		public bool Remove(IBaseEvent item, bool keepPos)
		{
			bool Remove;
			if (item is BaseDecorationAction decoAction)
			{
				RemoveInternal(decoAction);
				((BaseEvent)item)._beat._calculator = null;
				Remove = true;
			}
			else if (item is BaseRowAction rowAction)
			{
				RemoveInternal(rowAction);
				((BaseEvent)item)._beat._calculator = null;
				Remove = true;
			}
			else if (Contains(item))
			{
				if (item.Type == EventType.SetCrotchetsPerBar)
					Remove = RemoveSetCrotchetsPerBarInternal((SetCrotchetsPerBar)item, keepPos);
				else if (EventTypeUtils.ToEnums<BaseBeatsPerMinute>().Contains(item.Type))
					Remove = RemoveBaseBeatsPerMinuteInternal((BaseBeatsPerMinute)item);
				else
				{
					bool result = base.Remove(item);
					((BaseEvent)item)._beat._calculator = null;
					Remove = result;
				}
			}
			else
				Remove = false;
			if (item is FloatingText floatingText)
				_floatingTexts.Remove(floatingText);
			return Remove;
		}
		internal bool AddInternal(BaseDecorationAction item)
		{
			item._beat._calculator = Calculator;
			if (base.Contains(item)) return false;
			Decoration? parent = item.Parent ?? Decorations[item._decoId];
			if (parent == null) Decorations._unhandledRowEvents.Add(item);
			else ((OrderedEventCollection)parent).Add(item);
			base.Add(item);
			return true;
		}
		internal bool AddInternal(BaseRowAction item)
		{
			item._beat._calculator = Calculator;
			if (base.Contains(item)) return false;
			Row? parent = item.Parent ?? (item.Index < Rows.Count ? Rows[item.Index] : null);
			if (parent == null) Rows._unhandledRowEvents.Add(item);
			else { ((OrderedEventCollection)parent).Add(item); item._parent = parent; }
			base.Add(item);
			return true;
		}
		internal bool RemoveInternal(BaseDecorationAction item)
		{
			Decoration? parent = item.Parent ?? Decorations[item._decoId];
			if (parent == null) Decorations._unhandledRowEvents.Remove(item);
			else { ((OrderedEventCollection)parent).Remove(item); item._parent = parent; }
			return base.Remove(item);
		}
		internal bool RemoveInternal(BaseRowAction item)
		{
			Row? parent = item.Parent ?? ((item.Index >= 0 && item.Index < Rows.Count) ? Rows[item.Index] : null);
			if (parent == null) Rows._unhandledRowEvents.Remove(item);
			else ((OrderedEventCollection)parent).Remove(item);
			return base.Remove(item);
		}
		private bool AddSetCrotchetsPerBarInternal(SetCrotchetsPerBar item, bool keepCpb = true)
		{
			bool result;
			if (keepCpb)
			{
				SetCrotchetsPerBar? nxt = item.NextOrDefault();
				//更新拍号
				//RefreshCPBs(item._beat);
				//添加事件
				result = base.Add(item);
				if (nxt != null)
				{
					SetCrotchetsPerBar? frt = item.FrontOrDefault();
					//更新计算器
					Calculator.Refresh();
					BaseEvent? nxtE = item.After<BaseEvent>().FirstOrDefault((i) => i is IBarBeginningEvent &&
						i.Type != EventType.SetCrotchetsPerBar &&
						i._beat < nxt._beat);
					float interval = (nxtE != null ? nxtE._beat.BeatOnly : nxt._beat.BeatOnly) - item._beat.BeatOnly;
					float c = interval % item.CrotchetsPerBar;
					if (c > 0f)
					{
						c = c < 2f ? c + item.CrotchetsPerBar : c;
						result &= base.Add(new SetCrotchetsPerBar
						{
							_beat = item._beat + interval - c,
							_crotchetsPerBar = checked((int)Math.Round((double)unchecked(c - 1f)))
						});
					}
					else if (nxt.CrotchetsPerBar == item.CrotchetsPerBar)
						base.Remove(nxt);
					if (nxtE != null)
						result &= base.Add(new SetCrotchetsPerBar
						{
							_beat = nxtE._beat,
							_crotchetsPerBar = frt?.CrotchetsPerBar ?? 8 - 1
						});
				}
			}
			else
			{
				//RefreshCPBs(item._beat);
				result = base.Add(item);
			}
			// 更新计算器
			Calculator.Refresh();
			return result;
		}
		private bool RemoveSetCrotchetsPerBarInternal(SetCrotchetsPerBar item, bool keepCpb = true)
		{
			if (keepCpb)
			{
				SetCrotchetsPerBar? nxt = item.NextOrDefault();
				if (nxt != null)
				{
					SetCrotchetsPerBar? frt = item.FrontOrDefault();
					BaseEvent? nxtE = item.After<BaseEvent>().FirstOrDefault((i) => i is IBarBeginningEvent &&
						i.Type != EventType.SetCrotchetsPerBar &&
						i._beat < nxt._beat);
					int cpb = item.CrotchetsPerBar;
					int interval = (int)((nxtE ?? nxt)._beat.BeatOnly - item._beat.BeatOnly);
					int c = interval % frt?.CrotchetsPerBar ?? 8;
					if (c > 0)
					{
						c = c < 2 ? c + item.CrotchetsPerBar : c;
						if (c == nxt.CrotchetsPerBar)
							base.Remove(nxt);
						base.Add(new SetCrotchetsPerBar()
						{
							_beat = item._beat + interval - c,
							_crotchetsPerBar = (c - 1)
						});
					}
					else
					{
						if (nxtE != null && nxt.CrotchetsPerBar == (frt?.CrotchetsPerBar ?? 8))
						{
							base.Remove(nxt);
						}
					}
					if (nxtE != null)
						base.Add(new SetCrotchetsPerBar
						{
							_beat = nxtE._beat,
							_crotchetsPerBar = frt != null ? frt.CrotchetsPerBar : 8 - 1
						});
					Calculator.Refresh();
				}
				//更新计算器
				Calculator.Refresh();
				bool result = base.Remove(item);
				item._beat._calculator = null;
				Calculator.Refresh();
				return result;
			}
			else
			{
				bool result = base.Remove(item);
				item._beat._calculator = null;
				Calculator.Refresh();
				return result;
			}
		}
		private bool AddBaseBeatsPerMinuteInternal(BaseBeatsPerMinute item)
		{
			//RefreshBPMs(item.Beat);
			bool result = base.Add(item);
			Calculator.Refresh();
			return result;
		}
		private bool RemoveBaseBeatsPerMinuteInternal(BaseBeatsPerMinute item)
		{
			bool result = base.Remove(item);
			Calculator.Refresh();
			//RefreshBPMs(item.Beat);
			item._beat._calculator = null;
			return result;
		}
		private void RefreshBPMs(RDBeat start)
		{
			foreach (KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>> item in eventsBeatOrder)
				item.Key.ResetBPM();
			foreach (IBaseEvent? item in this.Where(i => i.Beat > start))
				item.Beat.ResetBPM();
			foreach (Bookmark? item in Bookmarks)
				item.Beat.ResetBPM();
		}
		private void RefreshCPBs(RDBeat start)
		{
			foreach (KeyValuePair<RDBeat, TypedEventCollection<IBaseEvent>> item in eventsBeatOrder)
				item.Key.ResetCPB();
			foreach (IBaseEvent? item in this.Where(i => i.Beat > start))
				item.Beat.ResetCPB();
			foreach (Bookmark? item in Bookmarks)
				item.Beat.ResetCPB();
		}
		/// <inheritdoc/>
		public void Dispose()
		{
			if (isZip && isExtracted && !string.IsNullOrEmpty(Directory))
			{
				System.IO.Directory.Delete(Directory, true);
			}
			GC.SuppressFinalize(this);
		}
		/// <inheritdoc/>
		public override string ToString() => $"\"{Settings.Song}\" Count = {Count}";
		private bool isZip = false;
		private bool isExtracted = false;
		private RDColor[] colorPalette = new RDColor[21];
		internal List<FloatingText> _floatingTexts = [];
		/// <summary>
		/// Variables.
		/// </summary>
		[JsonIgnore]
		public readonly RDVariables Variables;
	}
}
