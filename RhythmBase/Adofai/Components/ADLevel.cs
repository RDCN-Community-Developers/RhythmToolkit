using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Extensions;
using RhythmBase.Adofai.Utils;
using RhythmBase.RhythmDoctor.Components;
using System.IO.Compression;
using System.Text.Json;

namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Adofal level.
	/// </summary>
	public class ADLevel : TileCollection, IDisposable
	{
		/// <summary>
		/// Level settings.
		/// </summary>
		public Settings Settings { get; set; }
		/// <summary>
		/// Level decoration collection.
		/// </summary>
		public List<IBaseEvent> Decorations { get; set; }
		/// <summary>
		/// Level file path.
		/// </summary>
		public string Filepath => _path;
		/// <summary>
		/// Level directory path.
		/// </summary>
		public string Directory => Path.GetDirectoryName(_path) ?? "";
		/// <summary>
		/// Get all the events of the level.
		/// </summary>
		public override IEnumerable<IBaseEvent> Events
		{
			get
			{
				foreach (IBaseEvent tile in base.Events)
					yield return tile;
				foreach (IBaseEvent tile2 in Decorations)
					yield return tile2;
			}
		}
		/// <summary>
		/// The calculator that comes with the level.
		/// </summary>
		public ADBeatCalculator Calculator { get; }
		/// <summary>  
		/// Initializes a new instance of the <see cref="ADLevel"/> class.  
		/// </summary>  
		public ADLevel()
		{
			Settings = new Settings();
			Decorations = [];
			Calculator = new ADBeatCalculator(this);
			End.Parent = this;
		}
		/// <summary>  
		/// Initializes a new instance of the <see cref="ADLevel"/> class with a collection of tiles.  
		/// </summary>  
		/// <param name="items">The collection of tiles to initialize the level with.</param>  
		public ADLevel(IEnumerable<Tile> items)
		{
			Settings = new Settings();
			Decorations = [];
			Calculator = new ADBeatCalculator(this);
			End.Parent = this;
			this.InsertRange(0, items);
		}
		/// <summary>
		/// Gets the default level, which consists of 10 repeated tiles.
		/// </summary>
		public static ADLevel Default
		{
			get
			{
				ADLevel level = [.. new Tile().Repeat(10)];
				level.Settings = new()
				{
					Version = GlobalSettings.DefaultVersionAdofai,
				};
				return level;
			}
		}

		/// <summary>
		/// Reads a level from a file.
		/// Supports .adofai and .zip file extensions.
		/// </summary>
		/// <param name="filepath">The path to the level file.</param>
		/// <param name="settings">Optional settings for reading the level.</param>
		/// <returns>An <see cref="ADLevel"/> instance loaded from the file.</returns>
		/// <exception cref="RhythmBaseException">Thrown if the file type is not supported or extraction fails.</exception>
		public static ADLevel FromFile(string filepath, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			string extension = Path.GetExtension(filepath);
			ADLevel? level;
			if (extension != ".zip")
			{
				if (extension != ".adofai")
					throw new RhythmBaseException("File not supported.");
				using FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
				level = FromStream(stream, settings);
				level._path = Path.GetFullPath(filepath);
				return level;
			}
			DirectoryInfo tempDirectory = new(Path.Combine(GlobalSettings.CachePath, "RhythmBaseTemp_" + Path.GetRandomFileName()));
			tempDirectory.Create();
			try
			{
#if NET8_0_OR_GREATER
				using Stream stream = File.OpenRead(filepath);
				ZipFile.ExtractToDirectory(stream, tempDirectory.FullName, overwriteFiles: true);
#elif NETSTANDARD2_0_OR_GREATER
				ZipFile.ExtractToDirectory(filepath, tempDirectory.FullName);
#endif
				string? adlevelPath = null;
				foreach (FileInfo file in tempDirectory.GetFiles())
				{
					if (file.Extension == ".adofai")
					{
						adlevelPath = file.FullName;
						break;
					}
				}
				if (adlevelPath == null)
					throw new RhythmBaseException("No Adofai file has been found.");
				level = FromFile(adlevelPath, settings);
				level.isZip = true;
				return level;
			}
			catch (Exception ex2)
			{
				tempDirectory.Delete(true);
				throw new RhythmBaseException("Cannot extract the file.", ex2);
			}
		}
		/// <summary>
		/// Asynchronously reads a level from a file.
		/// Supports .adofai and .zip file extensions.
		/// </summary>
		/// <param name="filepath">The path to the level file.</param>
		/// <param name="settings">Optional settings for reading the level.</param>
		/// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the loaded <see cref="ADLevel"/> instance.</returns>
		/// <exception cref="RhythmBaseException">Thrown if the file type is not supported or extraction fails.</exception>
		public static async Task<ADLevel> FromFileAsync(string filepath, LevelReadOrWriteSettings? settings = null, CancellationToken cancellationToken = default)
		{
			settings ??= new();
			string extension = Path.GetExtension(filepath);
			ADLevel? level;
			if (extension != ".zip")
			{
				if (extension != ".adofai")
					throw new RhythmBaseException("File not supported.");
				using FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
				level = await FromStreamAsync(stream, settings);
				level._path = Path.GetFullPath(filepath);
				return level;
			}
			DirectoryInfo tempDirectory = new(Path.Combine(Path.GetTempPath(), "RhythmBaseTemp_" + Path.GetRandomFileName()));
			tempDirectory.Create();
			try
			{
#if NET8_0_OR_GREATER
				using Stream stream = File.OpenRead(filepath);
				ZipFile.ExtractToDirectory(stream, tempDirectory.FullName, overwriteFiles: true);
#elif NETSTANDARD2_0_OR_GREATER
				ZipFile.ExtractToDirectory(filepath, tempDirectory.FullName);
#endif
				string? adlevelPath = null;
				foreach (FileInfo file in tempDirectory.GetFiles())
				{
					if (file.Extension == ".adofai")
					{
						adlevelPath = file.FullName;
						break;
					}
				}
				if (adlevelPath == null)
					throw new RhythmBaseException("No Adofai file has been found.");
				level = await FromFileAsync(adlevelPath, settings);
				level.isZip = true;
				return level;
			}
			catch (Exception ex2)
			{
				tempDirectory.Delete(true);
				throw new RhythmBaseException("Cannot extract the file.", ex2);
			}
		}

		/// <summary>
		/// Reads an <see cref="ADLevel"/> instance from a stream containing JSON data.
		/// </summary>
		/// <param name="adlevelStream">The input stream containing the level data in JSON format.</param>
		/// <param name="settings">Optional settings for reading the level. If <c>null</c>, default settings are used.</param>
		/// <returns>
		/// An <see cref="ADLevel"/> instance loaded from the stream. If deserialization fails, returns a new empty <see cref="ADLevel"/>.
		/// </returns>
		public static ADLevel FromStream(Stream adlevelStream, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			ADLevel? level;
			settings.OnBeforeReading();
			using EscapeSpecialCharacterStream stream = new(adlevelStream);
			level = JsonSerializer.Deserialize<ADLevel>(stream, options);
			settings.OnAfterReading();
			return level ?? [];
		}

		/// <summary>
		/// Asynchronously reads an <see cref="ADLevel"/> instance from a stream containing JSON data.
		/// </summary>
		/// <param name="stream">The input stream containing the level data in JSON format.</param>
		/// <param name="settings">Optional settings for reading the level. If <c>null</c>, default settings are used.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. The task result contains the loaded <see cref="ADLevel"/> instance.
		/// </returns>
		public static async Task<ADLevel> FromStreamAsync(Stream stream, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			ADLevel? level;
			settings.OnBeforeReading();
			level = await JsonSerializer.DeserializeAsync<ADLevel>(stream, options);
			settings.OnAfterReading();
			return level ?? [];
		}
		/// <summary>
		/// Reads a level from a JSON string.
		/// </summary>
		/// <param name="json">The JSON string containing the level data.</param>
		/// <param name="settings">Optional settings for reading the level.</param>
		/// <returns>An <see cref="ADLevel"/> instance loaded from the JSON string.</returns>
		public static ADLevel FromJsonString(string json, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			ADLevel? level;
			settings.OnBeforeWriting();
			level = JsonSerializer.Deserialize<ADLevel>(json, options);
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
		/// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
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
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(filepath, settings);
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
		/// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
		public async void SaveToFileAsync(string filepath, LevelReadOrWriteSettings? settings = null, CancellationToken cancellationToken = default)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(filepath, settings);
			settings.OnBeforeWriting();
			using (FileStream stream = File.Open(filepath, FileMode.OpenOrCreate, FileAccess.Write))
			{
				await JsonSerializer.SerializeAsync(stream, this, options, cancellationToken);
			}
			settings.OnAfterWriting();
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
			return json;
		}
		/// <summary>  
		/// Adds a tile to the level.  
		/// Sets the parent of the tile to this level before adding it to the base collection.  
		/// </summary>  
		/// <param name="item">The tile to add to the level.</param>  
		public override void Add(Tile item) => Insert(Count, item);
		/// <summary>  
		/// Inserts a tile into the level at the specified index.  
		/// Sets the parent of the tile to this level before inserting it into the base collection.  
		/// </summary>  
		/// <param name="index">The zero-based index at which the tile should be inserted.</param>  
		/// <param name="item">The tile to insert into the level.</param>  
		public override void Insert(int index, Tile item)
		{
			item.Parent = this;
			base.Insert(index, item);
		}
		/// <summary>
		/// Removes the tile at the specified index from the collection.
		/// </summary>
		/// <remarks>After removal, the tile's <see cref="Tile.Parent"/>, <see cref="Tile.Previous"/>, and <see
		/// cref="Tile.Next"/> properties are set to <see langword="null"/>.</remarks>
		/// <param name="index">The zero-based index of the tile to remove.</param>
		public void RemoveAt(int index)
		{
			Tile tile = this[index];
			tile.Parent = null!;
			tile.Previous = null;
			tile.Next = null!;
			tileOrder.RemoveAt(index);
		}
		/// <summary>  
		/// Removes all tiles from the level.  
		/// Sets the parent of each tile to null before clearing the base collection.  
		/// </summary>  
		public override void Clear()
		{
			foreach (Tile item in tileOrder)
			{
				item.Parent = null;
			}
			base.Clear();
		}
		/// <inheritdoc/>
		public void Dispose()
		{
			if (isZip)
			{
				System.IO.Directory.Delete(Directory, true);
			}
			GC.SuppressFinalize(this);
		}

		/// <inheritdoc/>
		public override string ToString() => $"\"{Settings.Song}\" Count = {Count}";
		internal string _path = string.Empty;
		private bool isZip = false;
		//private bool isExtracted = false;
	}
}