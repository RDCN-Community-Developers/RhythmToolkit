using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Converters;
using RhythmBase.Events;
using RhythmBase.Exceptions;
using RhythmBase.Extensions;
using RhythmBase.Settings;
using RhythmBase.Utils;

using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
namespace RhythmBase.Components
{
	/// <summary>
	/// Rhythm Doctor level.
	/// </summary>
	public class RDLevel : OrderedEventCollection<IBaseEvent>
	{
		/// <summary>
		/// The calculator that comes with the level.
		/// </summary>
		[JsonIgnore]
		public BeatCalculator Calculator { get; }
		/// <summary>
		/// Level Settings.
		/// </summary>
		public Settings Settings { get; set; }
		internal List<RowEventCollection> ModifiableRows { get; } = new List<RowEventCollection>(16);
		internal List<DecorationEventCollection> ModifiableDecorations { get; } = [];
		/// <summary>
		/// Level tile collection.
		/// </summary>
		public IReadOnlyList<RowEventCollection> Rows => ModifiableRows.AsReadOnly();
		/// <summary>
		/// Level decoration collection.
		/// </summary>
		public IReadOnlyList<DecorationEventCollection> Decorations => ModifiableDecorations.AsReadOnly();
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
		public RDColor[] ColorPalette { get; }
		/// <summary>
		/// Level file path.
		/// </summary>
		[JsonIgnore]
		public string Path => _path;
		/// <summary>
		/// Level directory path.
		/// </summary>
		[JsonIgnore]
		public string Directory => System.IO.Path.GetDirectoryName(_path)!;
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
			Variables = new Variables();
			Calculator = new BeatCalculator(this);
			Settings = new Settings();
			Conditionals = [];
			Bookmarks = [];
			ColorPalette = new RDColor[21];
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
				PlaySong playsong = new();
				SetTheme settheme = new();
				playsong.Song = new RDAudio() { Filename = "sndOrientalTechno" };
				settheme.Preset = SetTheme.Theme.OrientalTechno;
				rdlevel.AddRange([playsong, settheme]);
				RowEventCollection samurai = rdlevel.CreateRow(new SingleRoom(RoomIndex.Room1), Characters.Samurai);
				samurai.Sound.Filename = "Shaker";
				samurai.Add(new AddClassicBeat());
				return rdlevel;
			}
		}
		/// <summary>
		/// Create a decoration and add it to the level.
		/// </summary>
		/// <param name="room">The room where this decoration is in.</param>
		/// <param name="sprite">The sprite referenced by this decoration.</param>
		/// <returns>Decoration that created and added to the level.</returns>
		public DecorationEventCollection CreateDecoration(SingleRoom room, [NotNull] string sprite)
		{
			DecorationEventCollection temp = new(room)
			{
				Parent = this,
				Filename = sprite
			};
			ModifiableDecorations.Add(temp);
			return temp;
		}
		/// <summary>
		/// Clone the decoration and add it to the level.
		/// </summary>
		/// <param name="decoration">Decoration that was copied.</param>
		/// <returns></returns>
		public DecorationEventCollection CloneDecoration(DecorationEventCollection decoration)
		{
			DecorationEventCollection temp = decoration.Clone();
			ModifiableDecorations.Add(temp);
			return temp;
		}
		/// <summary>
		/// Remove the decoration from the level.
		/// </summary>
		/// <param name="decoration">The decoration to be removed.</param>
		/// <returns></returns>
		public bool RemoveDecoration(DecorationEventCollection decoration)
		{
			if (Decorations.Contains(decoration))
			{
				this.RemoveRange(decoration);
				return ModifiableDecorations.Remove(decoration);
			}
			else
				return false;
		}
		/// <summary>
		/// Create a row and add it to the level.
		/// </summary>
		/// <param name="room">The room where this row is in.</param>
		/// <param name="character">The character used by this row.</param>
		/// <returns>Row that created and added to the level.</returns>
		public RowEventCollection CreateRow(SingleRoom room, RDCharacter character)
		{
			RowEventCollection temp = new()
			{
				Character = character,
				Rooms = room,
				Parent = this
			};
			temp.Parent = this;
			ModifiableRows.Add(temp);
			return temp;
		}
		/// <summary>
		/// Remove the row from the level.
		/// </summary>
		/// <param name="row">The row to be removed.</param>
		/// <returns></returns>
		public bool RemoveRow(RowEventCollection row) => Rows.Contains(row) && ModifiableRows.Remove(row);
		/// <summary>
		/// Read from file as level.
		/// Use default input settings.
		/// Supports .rdlevel, .rdzip, .zip file extension.
		/// </summary>
		/// <param name="filepath">File path.</param>
		/// <exception cref="T:RhythmBase.Exceptions.VersionTooLowException">The minimum level version number supported by this library is 54.</exception>
		/// <exception cref="T:RhythmBase.Exceptions.ConvertingException"></exception>
		/// <exception cref="T:RhythmBase.Exceptions.RhythmBaseException">File not supported.</exception>
		/// <returns>An instance of a level that reads from a file.</returns>
		public static RDLevel Read(string filepath) => Read(filepath, new LevelReadOrWriteSettings());
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
		public static RDLevel Read(string filepath, LevelReadOrWriteSettings settings)
		{
			JsonSerializer LevelSerializer = new();
			LevelSerializer.Converters.Add(new RDLevelConverter(filepath, settings));
			string extension = System.IO.Path.GetExtension(filepath);
			RDLevel Read;
			if ((extension != ".rdzip") && (extension != ".zip"))
			{
				if (extension != ".rdlevel")
					throw new RhythmBaseException("File not supported.");
				Read = LevelSerializer.Deserialize<RDLevel>(new JsonTextReader(File.OpenText(filepath))) ?? throw new RhythmBaseException("Cannot read the file.");
			}
			else
				Read = ReadFromZip(filepath, settings);
			return Read;
		}
		/// <summary>
		/// Read from a zip file as a level.
		/// </summary>
		/// <param name="filepath">The path to the zip file.</param>
		/// <returns>An instance of RDLevel that reads from a zip file.</returns>
		public static RDLevel ReadFromZip(string filepath) => ReadFromZip(filepath, new LevelReadOrWriteSettings());

		/// <summary>
		/// Read from a zip file as a level with specific settings.
		/// </summary>
		/// <param name="filepath">The path to the zip file.</param>
		/// <param name="settings">The settings for reading the level.</param>
		/// <returns>An instance of RDLevel that reads from a zip file with specific settings.</returns>
		public static RDLevel ReadFromZip(string filepath, LevelReadOrWriteSettings settings) => ReadFromZip(File.OpenRead(filepath), settings);
		/// <summary>
		/// Read from a zip file as a level.
		/// </summary>
		/// <param name="stream">The stream of the zip file.</param>
		/// <returns>An instance of RDLevel that reads from a zip file.</returns>
		public static RDLevel ReadFromZip(Stream stream) => ReadFromZip(stream, new LevelReadOrWriteSettings());

		/// <summary>
		/// Read from a zip file as a level with specific settings.
		/// </summary>
		/// <param name="stream">The stream of the zip file.</param>
		/// <param name="settings">The settings for reading the level.</param>
		/// <returns>An instance of RDLevel that reads from a zip file with specific settings.</returns>
		public static RDLevel ReadFromZip(Stream stream, LevelReadOrWriteSettings settings)
		{
			DirectoryInfo tempDirectory = new(System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName()));
			tempDirectory.Create();
			RDLevel ReadFromZip;
			try
			{
				ZipFile.ExtractToDirectory(stream, tempDirectory.FullName);
				ReadFromZip = Read(tempDirectory.GetFiles().Single(i => i.Extension == ".rdlevel").FullName, settings);
			}
			catch (InvalidOperationException ex)
			{
				throw new RhythmBaseException("Found more than one rdlevel file.", ex);
			}
			catch (Exception ex2)
			{
				throw new RhythmBaseException("Cannot extract the file.", ex2);
			}
			return ReadFromZip;
		}
		private JsonSerializer Serializer(LevelReadOrWriteSettings settings) => new()
		{
			Converters = { new RDLevelConverter(_path, settings) }
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
			if (_path.IsNullOrEmpty() &&
				System.IO.Path.GetFullPath(_path) == System.IO.Path.GetFullPath(filepath))
				throw new OverwriteNotAllowedException(_path, typeof(LevelReadOrWriteSettings));
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
			Serializer(settings).Serialize(writer, this);
		}

		/// <summary>
		/// Save the level to a stream.
		/// Use default output settings.
		/// </summary>
		/// <param name="stream">The stream to write the level to.</param>
		public void Write(Stream stream) => Write(new StreamWriter(stream), new LevelReadOrWriteSettings());

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
			Converters = { new RDLevelConverter(Path, settings) }
		});
		/// <summary>
		/// Convert to a string that can be read by the game.
		/// Use default output settings.
		/// </summary>
		/// <returns>Level string.</returns>
		public string ToRDLevelJson() => ToRDLevelJson(new LevelReadOrWriteSettings());
		/// <summary>
		/// Convert to a string that can be read by the game.
		/// </summary>
		/// <param name="settings">Output settings.</param>
		/// <returns>Level string.</returns>
		public string ToRDLevelJson(LevelReadOrWriteSettings settings)
		{
			StringWriter file = new();
			Write(file, settings);
			file.Close();
			return file.ToString();
		}
		/// <summary>
		/// Add event to the level.
		/// </summary>
		/// <param name="item">Event to be added.</param>
		/// <exception cref="T:RhythmBase.Exceptions.RhythmBaseException"></exception>
		public override void Add(IBaseEvent item)
		{
			//添加默认节拍
			if (((BaseEvent)item)._beat.IsEmpty)
				((BaseEvent)item)._beat._calculator = Calculator;
			//部分事件只能在小节的开头
			if (item is IBarBeginningEvent @event && ((BaseEvent)item)._beat.BarBeat.beat != 1f)
				throw new IllegalBeatException(@event);
			//更改节拍的关联关卡
			((BaseEvent)item)._beat._calculator = Calculator;
			((BaseEvent)item)._beat.ResetCache();
			if (item.Type == EventType.Comment && ((Comment)item).Parent == null)
				//注释事件可能在精灵板块，也可能不在
				base.Add(item);
			else if (item.Type == EventType.TintRows && ((TintRows)item).Parent == null)
				//轨道染色事件可能是为所有轨道染色
				base.Add(item);
			else if (Utils.EventTypeUtils.RowTypes.Contains(item.Type))
			{
				BaseRowAction rowAction = (BaseRowAction)item;
				if (rowAction.Parent == null)
					throw new UnreadableEventException("The Parent property of this event should not be null. Call RowEventCollection.Add() instead.", item);
				//添加至对应轨道
				rowAction.Parent.AddSafely((BaseRowAction)item);
				base.Add(item);
			}
			else if (Utils.EventTypeUtils.DecorationTypes.Contains(item.Type))
			{
				BaseDecorationAction decoAction = (BaseDecorationAction)item;
				if (decoAction.Parent == null)
					throw new UnreadableEventException("The Parent property of this event should not be null. Call DecorationEventCollection.Add() instead.", item);
				//添加至对应精灵
				decoAction.Parent.AddSafely((BaseDecorationAction)item);
				base.Add(item);
			}
			//BPM 和 CPB
			else if (item.Type == EventType.SetCrotchetsPerBar)
				AddSetCrotchetsPerBar((SetCrotchetsPerBar)item);
			else if (Utils.EventTypeUtils.ToEnums<BaseBeatsPerMinute>().Contains(item.Type))
				AddBaseBeatsPerMinute((BaseBeatsPerMinute)item);
			// 其他
			else
				base.Add(item);
		}
		/// <summary>
		/// Determine if the level contains this event.
		/// </summary>
		/// <param name="item">Event.</param>
		/// <returns></returns>
		public override bool Contains(IBaseEvent item) => (Utils.EventTypeUtils.RowTypes.Contains(item.Type)
			&& Rows.Any((RowEventCollection i) => i.Contains(item))) || (Utils.EventTypeUtils.DecorationTypes.Contains(item.Type) && Decorations.Any((DecorationEventCollection i) => i.Contains(item))) || base.Contains(item);
		/// <summary>
		/// Remove event from the level.
		/// </summary>
		/// <param name="item">Event to be removed.</param>
		/// <returns></returns>
		public override bool Remove(IBaseEvent item)
		{
			bool Remove;
			if (Utils.EventTypeUtils.RowTypes.Contains(item.Type)
				&& Rows.Any((RowEventCollection i) => i.RemoveSafely((BaseRowAction)item)))
			{
				base.Remove(item);
				((BaseEvent)item)._beat._calculator = null;
				Remove = true;
			}
			else if (Utils.EventTypeUtils.DecorationTypes.Contains(item.Type)
					&& Decorations.Any((DecorationEventCollection i) => i.RemoveSafely((BaseDecorationAction)item)))
			{
				base.Remove(item);
				((BaseEvent)item)._beat._calculator = null;
				Remove = true;
			}
			else if (Contains(item))
			{
				if (item.Type == EventType.SetCrotchetsPerBar)
					Remove = RemoveSetCrotchetsPerBar((SetCrotchetsPerBar)item);
				else if (Utils.EventTypeUtils.ToEnums<BaseBeatsPerMinute>().Contains(item.Type))
					Remove = RemoveBaseBeatsPerMinute((BaseBeatsPerMinute)item);
				else
				{
					bool result = base.Remove(item);
					((BaseEvent)item)._beat._calculator = null;
					Remove = result;
				}
			}
			else
				Remove = false;
			return Remove;
		}
		private void AddSetCrotchetsPerBar(SetCrotchetsPerBar item)
		{
			SetCrotchetsPerBar? frt = item.FrontOrDefault();
			SetCrotchetsPerBar? nxt = item.NextOrDefault();
			//更新拍号
			RefreshCPBs(item._beat);
			//添加事件
			base.Add(item);
			//更新计算器
			Calculator.Refresh();
			if (nxt != null)
			{
				BaseEvent? nxtE = item.After<BaseEvent>().FirstOrDefault((BaseEvent i) => i is IBarBeginningEvent &&
					i.Type != EventType.SetCrotchetsPerBar &&
					i._beat < nxt._beat);
				float interval = ((nxtE != null) ? nxtE._beat.BeatOnly : nxt._beat.BeatOnly) - item._beat.BeatOnly;
				float c = interval % item.CrotchetsPerBar;
				if (c > 0f)
				{
					c = (c < 2f) ? (c + item.CrotchetsPerBar) : c;
					base.Add(new SetCrotchetsPerBar
					{
						_beat = item._beat + interval - c,
						_crotchetsPerBar = checked((uint)Math.Round((double)unchecked(c - 1f)))
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
			// 更新计算器
			Calculator.Refresh();
		}
		private bool RemoveSetCrotchetsPerBar(SetCrotchetsPerBar item)
		{
			SetCrotchetsPerBar? frt = item.FrontOrDefault();
			SetCrotchetsPerBar? nxt = item.NextOrDefault();
			if (nxt != null)
			{
				BaseEvent? nxtE = item.After<BaseEvent>().FirstOrDefault((BaseEvent i) => i is IBarBeginningEvent &&
					i.Type != EventType.SetCrotchetsPerBar &&
					i._beat < nxt._beat);
				uint cpb = item.CrotchetsPerBar;
				int interval = (int)((nxtE ?? nxt)._beat.BeatOnly - item._beat.BeatOnly);
				long c = interval % frt?.CrotchetsPerBar ?? 8;
				if (c > 0)
				{
					c = c < 2 ? c + item.CrotchetsPerBar : c;
					if (c == nxt.CrotchetsPerBar)
						base.Remove(nxt);
					base.Add(new SetCrotchetsPerBar()
					{
						_beat = item._beat + interval - c,
						_crotchetsPerBar = (uint)(c - 1)
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
						_crotchetsPerBar = ((frt != null) ? frt.CrotchetsPerBar : 8u - 1u)
					});
				Calculator.Refresh();
			}
			//更新计算器
			Calculator.Refresh();
			bool result = base.Remove(item);
			RefreshCPBs(item.Beat);
			item._beat._calculator = null;
			Calculator.Refresh();
			return result;
		}
		private void AddBaseBeatsPerMinute(BaseBeatsPerMinute item)
		{
			RefreshBPMs(item.Beat);
			base.Add(item);
			Calculator.Refresh();
		}
		private bool RemoveBaseBeatsPerMinute(BaseBeatsPerMinute item)
		{
			bool result = base.Remove(item);
			Calculator.Refresh();
			RefreshBPMs(item.Beat);
			item._beat._calculator = null;
			return result;
		}
		private void RefreshBPMs(RDBeat start)
		{
			foreach (var item in eventsBeatOrder.Keys)
				item.ResetBPM();
			foreach (var item in this.Where(i => i.Beat > start))
				item.Beat.ResetBPM();
			foreach (var item in Bookmarks)
				item.Beat.ResetBPM();
		}
		private void RefreshCPBs(RDBeat start)
		{
			foreach (var item in eventsBeatOrder.Keys)
				item.ResetCPB();
			foreach (var item in this.Where(i => i.Beat > start))
				item.Beat.ResetCPB();
			foreach (var item in Bookmarks)
				item.Beat.ResetCPB();
		}
		/// <inheritdoc/>
		public override string ToString() => string.Format("\"{0}\" Count = {1}", Settings.Song, Count);
		internal string _path;
		/// <summary>
		/// Variables.
		/// </summary>
		[JsonIgnore]
		public readonly Variables Variables;
	}
}
