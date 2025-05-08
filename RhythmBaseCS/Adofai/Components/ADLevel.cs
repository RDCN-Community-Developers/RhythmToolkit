using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Adofai.Converters;
using RhythmBase.Adofai.Events;
using RhythmBase.Adofai.Utils;
using RhythmBase.Global.Exceptions;
using RhythmBase.Global.Settings;

namespace RhythmBase.Adofai.Components
{
	/// <summary>
	/// Adofal level.
	/// </summary>
	public class ADLevel : ADTileCollection
	{
		/// <summary>
		/// Level settings.
		/// </summary>
		public ADSettings Settings { get; set; }
		/// <summary>
		/// Level decoration collection.
		/// </summary>
		public List<ADBaseEvent> Decorations { get; set; }
		/// <summary>
		/// Level file path.
		/// </summary>
		[JsonIgnore]
		public string Path => _path;
		/// <summary>
		/// Level directory path.
		/// </summary>
		[JsonIgnore]
		public string Directory => System.IO.Path.GetDirectoryName(_path);
		/// <summary>
		/// Get all the events of the level.
		/// </summary>
		public override IEnumerable<ADBaseEvent> Events
		{
			get
			{
				foreach (ADBaseEvent tile in base.Events)
					yield return tile;
				foreach (ADBaseEvent tile2 in Decorations)
					yield return tile2;
			}
		}
		/// <summary>
		/// The calculator that comes with the level.
		/// </summary>
		[JsonIgnore]
		public ADBeatCalculator Calculator { get; }
		public ADLevel()
		{
			Settings = new ADSettings();
			Decorations = [];
			Calculator = new ADBeatCalculator(this);
		}
		public ADLevel(IEnumerable<ADTile> items)
		{
			Settings = new ADSettings();
			Decorations = [];
			Calculator = new ADBeatCalculator(this);
			foreach (ADTile tile in items)
				Add(tile);
		}
		/// <summary>
		/// The default level within the game.
		/// </summary>
		public static ADLevel Default => [];
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
			LevelSerializer.Converters.Add(new ADLevelConverter(filepath, settings));
			string extension = System.IO.Path.GetExtension(filepath);
			if (extension != ".adofai")
			{
				throw new RhythmBaseException("File not supported.");
			}
			return LevelSerializer.Deserialize<ADLevel>(new JsonTextReader(File.OpenText(filepath)))!;
		}
		internal string _path;
	}
}
