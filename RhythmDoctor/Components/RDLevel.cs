using RhythmBase.Adofai.Extensions;
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
		public List<BaseConditional> Conditionals { get; }
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
		/// Level file path.
		/// </summary>
		[JsonIgnore]
		public string? Filepath => _path;
		/// <summary>
		/// Level directory path.
		/// </summary>
		[JsonIgnore]
		public string? Directory => Path.GetDirectoryName(_path);
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
			_path = "";
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
				settheme.Preset = Themes.OrientalTechno;
				rdlevel.AddRange([playsong, settheme]);
				Row samurai = new() { Rooms = RDRoomIndex.Room1, Character = RDCharacters.Samurai };
				rdlevel.Rows.Add(samurai);
				samurai.Sound.Filename = "Shaker";
				samurai.Add(new AddClassicBeat());
				return rdlevel;
			}
		}
		/// <summary>
		/// Reads a level from a file.
		/// Supports .rdlevel, .rdzip, and .zip file extensions.
		/// </summary>
		/// <param name="filepath">The path to the file to read.</param>
		/// <param name="settings">Optional settings for reading the level.</param>
		/// <exception cref="T:RhythmBase.Exceptions.VersionTooLowException">Thrown if the level version is below the minimum supported (54).</exception>
		/// <exception cref="T:RhythmBase.Exceptions.ConvertingException">Thrown if a conversion error occurs.</exception>
		/// <exception cref="T:RhythmBase.Exceptions.RhythmBaseException">Thrown if the file is not supported or cannot be extracted.</exception>
		/// <returns>An <see cref="RDLevel"/> instance loaded from the specified file.</returns>
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
				level = FromStream(stream, settings);
				level._path = Path.GetFullPath(filepath);
				return level;
			}
			DirectoryInfo tempDirectory = new(Path.Combine(Path.GetTempPath(), "RhythmBaseTemp_Zip_" + Path.GetRandomFileName()));
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
				string? rdlevelPath = null;
				foreach (var file in tempDirectory.GetFiles())
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
				level.isZip = true;
				level.isExtracted = true;
				return level;
			}
			catch (Exception ex2)
			{
				tempDirectory.Delete(true);
				throw new RhythmBaseException("Cannot extract the file.", ex2);
			}
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
			using EscapeNewLineStream stream = new(rdlevelStream);
			level = JsonSerializer.Deserialize<RDLevel>(stream, options);
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
		/// Saves the current level to a file in JSON format.
		/// </summary>
		/// <param name="filepath">The file path where the level will be saved.</param>
		/// <param name="settings">Optional settings for writing the level. If null, default settings are used.</param>
		public void SaveToFile(string filepath, LevelReadOrWriteSettings? settings = null)
		{
			settings ??= new();
			JsonSerializerOptions options = Utils.Utils.GetJsonSerializerOptions(filepath, settings);
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
		public override void Add(IBaseEvent item) => Add(item, true);
		/// <summary>
		/// Adds an event to the level, with an option to keep the event's position.
		/// </summary>
		/// <param name="item">The event to be added.</param>
		/// <param name="keepPos">Whether to keep the event's position (default is true).</param>
		public void Add(IBaseEvent item, bool keepPos = true)
		{
			// Set the default beat calculator
			((BaseEvent)item)._beat._calculator = Calculator;
			// Some events can only be at the beginning of a bar
			if (item is IBarBeginningEvent e && ((BaseEvent)item)._beat.BarBeat.beat != 1f)
				throw new IllegalBeatException(e);
			// Update the beat's associated level
			((BaseEvent)item)._beat.ResetCache();
			if (item is Comment comment && comment.Parent == null)
				// Comment events may or may not be in the decoration section
				base.Add(item);
			else if (item is TintRows tintRows && tintRows.Parent == null)
				base.Add(item);
			else if (item is BaseRowAction rowAction)
			{
				// Add to the corresponding row
				AddInternal(rowAction);
			}
			else if (item is BaseDecorationAction decoAction)
			{
				// Add to the corresponding decoration
				AddInternal(decoAction);
			}
			// BPM and CPB
			else if (item is SetCrotchetsPerBar setCrochetsPerBar)
				AddSetCrotchetsPerBarInternal(setCrochetsPerBar, keepPos);
			else if (item is BaseBeatsPerMinute baseBeatsPerMinute)
				AddBaseBeatsPerMinuteInternal(baseBeatsPerMinute);
			// Other events
			else
				base.Add(item);
			if (item is FloatingText floatingText)
				_floatingTexts.Add(floatingText);
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
		internal void AddInternal(BaseDecorationAction item)
		{
			Decoration? parent = item.Parent ?? Decorations[item._decoId];
			if (parent == null) Decorations._unhandledRowEvents.Add(item);
			if (base.Contains(item))
				return;
			item._beat._calculator = Calculator;
			base.Add(item);
		}
		internal void AddInternal(BaseRowAction item)
		{
			Row? parent = item.Parent ?? (item.Index < Rows.Count ? Rows[item.Index] : null);
			if (parent == null) Rows._unhandledRowEvents.Add(item);
			if (base.Contains(item))
				return;
			item._beat._calculator = Calculator;
			base.Add(item);
		}
		internal bool RemoveInternal(BaseDecorationAction item)
		{
			Decoration? parent = item.Parent ?? Decorations[item._decoId];
			if (parent == null) Decorations._unhandledRowEvents.Remove(item);
			else ((OrderedEventCollection)parent).Remove(item);
			return base.Remove(item);
		}
		internal bool RemoveInternal(BaseRowAction item)
		{
			Row? parent = item.Parent ?? (item.Index < Rows.Count ? Rows[item.Index] : null);
			if (parent == null) Rows._unhandledRowEvents.Remove(item);
			else ((OrderedEventCollection)parent).Remove(item);
			return base.Remove(item);
		}
		private void AddSetCrotchetsPerBarInternal(SetCrotchetsPerBar item, bool keepCpb = true)
		{
			if (keepCpb)
			{
				SetCrotchetsPerBar? nxt = item.NextOrDefault();
				//更新拍号
				//RefreshCPBs(item._beat);
				//添加事件
				base.Add(item);
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
						base.Add(new SetCrotchetsPerBar
						{
							_beat = item._beat + interval - c,
							_crotchetsPerBar = checked((int)Math.Round((double)unchecked(c - 1f)))
						});
					}
					else if (nxt.CrotchetsPerBar == item.CrotchetsPerBar)
						base.Remove(nxt);
					if (nxtE != null)
						base.Add(new SetCrotchetsPerBar
						{
							_beat = nxtE._beat,
							_crotchetsPerBar = frt?.CrotchetsPerBar ?? 8 - 1
						});
				}
			}
			else
			{
				//RefreshCPBs(item._beat);
				base.Add(item);
			}
			// 更新计算器
			Calculator.Refresh();
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
		private void AddBaseBeatsPerMinuteInternal(BaseBeatsPerMinute item)
		{
			//RefreshBPMs(item.Beat);
			base.Add(item);
			Calculator.Refresh();
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
			foreach (var item in eventsBeatOrder)
				item.Key.ResetBPM();
			foreach (var item in this.Where(i => i.Beat > start))
				item.Beat.ResetBPM();
			foreach (var item in Bookmarks)
				item.Beat.ResetBPM();
		}
		private void RefreshCPBs(RDBeat start)
		{
			foreach (var item in eventsBeatOrder)
				item.Key.ResetCPB();
			foreach (var item in this.Where(i => i.Beat > start))
				item.Beat.ResetCPB();
			foreach (var item in Bookmarks)
				item.Beat.ResetCPB();
		}
		/// <inheritdoc/>
		public void Dispose()
		{
			if (isZip && isExtracted)
			{
				System.IO.Directory.Delete(Directory, true);
			}
			GC.SuppressFinalize(this);
		}
		/// <inheritdoc/>
		public override string ToString() => $"\"{Settings.Song}\" Count = {Count}";
		internal string? _path;
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
