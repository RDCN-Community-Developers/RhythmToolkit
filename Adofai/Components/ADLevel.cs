using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Converters;
using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Extensions;
using RhythmBase.Adofai.Utils;
using RhythmBase.Global.Exceptions;
using RhythmBase.Global.Settings;
using System.Text;

namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Adofal level.
	/// </summary>
	public class ADLevel : TileCollection
	{
		/// <summary>
		/// Level settings.
		/// </summary>
		public ADSettings Settings { get; set; }
		/// <summary>
		/// Level decoration collection.
		/// </summary>
		public List<BaseEvent> Decorations { get; set; }
		/// <summary>
		/// Level file path.
		/// </summary>
		[JsonIgnore]
		public string Path => _path;
		/// <summary>
		/// Level directory path.
		/// </summary>
		[JsonIgnore]
		public string Directory => System.IO.Path.GetDirectoryName(_path) ?? "";
		/// <summary>
		/// Get all the events of the level.
		/// </summary>
		public override IEnumerable<BaseEvent> Events
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
		[JsonIgnore]
		public ADBeatCalculator Calculator { get; }
		/// <summary>  
		/// Initializes a new instance of the <see cref="ADLevel"/> class.  
		/// </summary>  
		public ADLevel()
		{
			Settings = new ADSettings();
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
			Settings = new ADSettings();
			Decorations = [];
			Calculator = new ADBeatCalculator(this);
			End.Parent = this;
			this.InsertRange(0, items);
		}
		/// <summary>
		/// The default level within the game.
		/// </summary>
		public static ADLevel Default => [[], [], [], [], [], [], [], [], [], []];
		/// <summary>
		/// Read from file as level.
		/// Use default input settings.
		/// Supports .rdlevel, .rdzip, .zip file extension.
		/// </summary>
		/// <param name="filepath">File path.</param>
		/// <returns>An instance of a level that reads from a file.</returns>
		public static ADLevel Read(string filepath) => Read(filepath, new LevelReadOrWriteSettings());
		/// <summary>
		/// Read from file as level.
		/// Supports .rdlevel, .rdzip, .zip file extension.
		/// </summary>
		/// <param name="filepath">File path.</param>
		/// <param name="settings">Input settings.</param>
		/// <returns>An instance of a level that reads from a file.</returns>
		public static ADLevel Read(string filepath, LevelReadOrWriteSettings settings)
		{
			JsonSerializer LevelSerializer = new();
			LevelSerializer.Converters.Add(new LevelConverter(filepath, settings));
			string extension = System.IO.Path.GetExtension(filepath);
			if (extension != ".adofai")
			{
				throw new RhythmBaseException("File not supported.");
			}
			return LevelSerializer.Deserialize<ADLevel>(new JsonTextReader(File.OpenText(filepath)))!;
		}
		private JsonSerializer Serializer(LevelReadOrWriteSettings settings) => new()
		{
			Converters = { new LevelConverter(_path, settings) }
		};
		/// <summary>
		/// Save the level.
		/// Use default output settings.
		/// </summary>
		/// <param name="filepath">File path.</param>
		/// <exception cref="T:RhythmBase.Exceptions.OverwriteNotAllowedException">Overwriting is disabled by the settings and a file with the same name already exists.</exception>
		public void Write(string filepath) => Write(filepath, new LevelReadOrWriteSettings());
		/// <summary>
		/// Save the level.
		/// </summary>
		/// <param name="filepath">File path.</param>
		/// <param name="settings">Output settings.</param>
		/// <exception cref="T:RhythmBase.Exceptions.OverwriteNotAllowedException">Overwriting is disabled by the settings and a file with the same name already exists.</exception>
		public void Write(string filepath, LevelReadOrWriteSettings settings)
		{
			using StreamWriter file = File.CreateText(filepath);
			Write(file, settings);
		}
		/// <summary>
		/// Save the level to a text writer.
		/// Use default output settings.
		/// </summary>
		/// <param name="stream">The text writer to write the level to.</param>
		public void Write(TextWriter stream) => Write(stream, new LevelReadOrWriteSettings());
		/// <summary>
		/// Save the level to a text writer.
		/// </summary>
		/// <param name="stream">The text writer to write the level to.</param>
		/// <param name="settings">The settings for writing the level.</param>
		public void Write(TextWriter stream, LevelReadOrWriteSettings settings)
		{
			using JsonTextWriter writer = new(stream);
			settings.OnBeforeWriting();
			Serializer(settings).Serialize(writer, this);
			settings.OnAfterWriting();
		}
		/// <summary>
		/// Save the level to a stream.
		/// Use default output settings.
		/// </summary>
		/// <param name="stream">The stream to write the level to.</param>
#if NET7_0_OR_GREATER
		public void Write(Stream stream) => Write(new StreamWriter(stream, leaveOpen: true), new LevelReadOrWriteSettings());
#else
		public void Write(Stream stream) => Write(new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true), new LevelReadOrWriteSettings());
#endif
		/// <summary>
		/// Save the level to a stream.
		/// </summary>
		/// <param name="stream">The stream to write the level to.</param>
		/// <param name="settings">The settings for writing the level.</param>
		public void Write(Stream stream, LevelReadOrWriteSettings settings) => Write(new StreamWriter(stream), settings);
		/// <summary>
		/// Convert to JObject type.
		/// </summary>
		/// <returns>A JObject type that stores all the data for the level.</returns>
		public JObject ToJObject() => ToJObject(new LevelReadOrWriteSettings());
		/// <summary>
		/// Convert to JObject type.
		/// </summary>
		/// <returns>A JObject type that stores all the data for the level.</returns>
		public JObject ToJObject(LevelReadOrWriteSettings settings) => JObject.FromObject(this, new JsonSerializer
		{
			Converters = { new LevelConverter(Path, settings) }
		});
		/// <summary>
		/// Convert to a string that can be read by the game.
		/// Use default output settings.
		/// </summary>
		/// <returns>Level string.</returns>
		public string ToADLevelJson() => ToADLevelJson(new LevelReadOrWriteSettings());
		/// <summary>
		/// Convert to a string that can be read by the game.
		/// </summary>
		/// <param name="settings">Output settings.</param>
		/// <returns>Level string.</returns>
		public string ToADLevelJson(LevelReadOrWriteSettings settings)
		{
			StringWriter file = new();
			Write(file, settings);
			file.Close();
			return file.ToString();
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
		internal string _path = string.Empty;
	}
}