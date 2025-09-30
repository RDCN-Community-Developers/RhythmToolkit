using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Extensions;
using RhythmBase.Adofai.Utils;
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
				foreach (BaseEvent tile in base.Events)
					yield return tile;
				foreach (BaseEvent tile2 in Decorations)
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
		/// The default level within the game.
		/// </summary>
		public static ADLevel Default => [.. new Tile().Repeat(10)];
		/// <summary>
		/// Read from file as level.
		/// Supports .adofai, .zip file extension.
		/// </summary>
		/// <param name="filepath">File path.</param>
		/// <param name="settings">Input settings.</param>
		/// <returns>An instance of a level that reads from a file.</returns>
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
			switch (settings.CompressionMode)
			{
				case Global.Settings.CompressionMode.EntirePackage:
					DirectoryInfo tempDirectory = new(Path.Combine(Path.GetTempPath(), "RhythmBaseTemp_" + Path.GetRandomFileName()));
					tempDirectory.Create();
					try
					{
#if NET8_0_OR_GREATER
					using Stream stream = File.OpenRead(filepath);
					// Use async extraction if available for better performance
					ZipFile.ExtractToDirectory(stream, tempDirectory.FullName, overwriteFiles: true);
#elif NETSTANDARD2_0_OR_GREATER
						ZipFile.ExtractToDirectory(filepath, tempDirectory.FullName);
#endif
						// Avoid LINQ Single for performance, use a simple loop
						string? adlevelPath = null;
						foreach (var file in tempDirectory.GetFiles())
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
				case Global.Settings.CompressionMode.No:
					using (var zip = ZipFile.OpenRead(filepath))
					{
						foreach (var entry in zip.Entries)
						{
							if (entry.FullName.EndsWith(".adofai", StringComparison.OrdinalIgnoreCase))
							{
								using var entryStream = entry.Open();
								level = FromStream(entryStream, settings);
								level._path = Path.GetFullPath(filepath);
								level.isZip = true;
								return level;
							}
						}
						throw new RhythmBaseException("No Adofai file has been found.");
					}
				default:
					throw new RhythmBaseException("Unsupported compression mode.");
			}
		}
		public static ADLevel FromStream(Stream stream, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(settings);
			ADLevel? level;
			settings.OnBeforeReading();
			level = JsonSerializer.Deserialize<ADLevel>(stream, options);
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
				JsonSerializer.Serialize(stream, this, options);
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
			settings.OnAfterWriting();
			return json;
		}
		/// <summary>  
		/// Adds a tile to the level.  
		/// Sets the parent of the tile to this level before adding it to the base collection.  
		/// </summary>  
		/// <param name="item">The tile to add to the level.</param>  
		public override void Add(ITile item) => Insert(Count, item);
		/// <summary>  
		/// Inserts a tile into the level at the specified index.  
		/// Sets the parent of the tile to this level before inserting it into the base collection.  
		/// </summary>  
		/// <param name="index">The zero-based index at which the tile should be inserted.</param>  
		/// <param name="item">The tile to insert into the level.</param>  
		public override void Insert(int index, ITile item)
		{
			item.Parent = this;
			base.Insert(index, item);
		}
		public void RemoveAt(int index)
		{
			ITile tile = this[index];
			tile.Parent = null;
			tile.Previous = null;
			tile.Next = null;
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
		private bool isExtracted = false;
	}
}