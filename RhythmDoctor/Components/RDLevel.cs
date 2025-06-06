﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RhythmBase.Adofai.Extensions;
using RhythmBase.Global.Components;
using RhythmBase.Global.Exceptions;
using RhythmBase.Global.Settings;
using RhythmBase.RhythmDoctor.Converters;
using RhythmBase.RhythmDoctor.Events;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.RhythmDoctor.Utils;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Reflection;
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
		/// Create a decoration and add it to the level.
		/// </summary>
		/// <param name="room">The room where this decoration is in.</param>
		/// <param name="sprite">The sprite referenced by this decoration.</param>
		/// <returns>Decoration that created and added to the level.</returns>
		[Obsolete("Use `new Decoration()` instead. This method will be removed in the future.")]
#if NETSTANDARD
		public Decoration CreateDecoration(RDSingleRoom room, string sprite)
#else
		public Decoration CreateDecoration(RDSingleRoom room, [NotNull] string sprite)
#endif
		{
			Decoration temp = new(room)
			{
				Parent = this,
				Filename = sprite
			};
			Decorations.Add(temp);
			return temp;
		}
		/// <summary>
		/// Clone the decoration and add it to the level.
		/// </summary>
		/// <param name="decoration">Decoration that was copied.</param>
		/// <returns></returns>
		[Obsolete("Use `Decoration.Clone()` instead. This method will be removed in the future.")]
		public Decoration CloneDecoration(Decoration decoration)
		{
			Decoration temp = decoration.Clone();
			Decorations.Add(temp);
			return temp;
		}
		/// <summary>
		/// Remove the decoration from the level.
		/// </summary>
		/// <param name="decoration">The decoration to be removed.</param>
		/// <returns></returns>
		[Obsolete("Use `RDLevel.Decorations.Remove()` instead. This method will be removed in the future.")]
		public bool RemoveDecoration(Decoration decoration)
		{
			if (Decorations.Contains(decoration))
			{
				this.RemoveRange(decoration);
				return Decorations.Remove(decoration);
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
		[Obsolete("Use `new Row()` instead. This method will be removed in the future.")]
		public Row CreateRow(RDSingleRoom room, RDCharacter character)
		{
			Row temp = new()
			{
				Character = character,
				Rooms = room,
				Parent = this
			};
			temp.Parent = this;
			Rows.Add(temp);
			return temp;
		}
		/// <summary>
		/// Remove the row from the level.
		/// </summary>
		/// <param name="row">The row to be removed.</param>
		/// <returns></returns>
		[Obsolete("Use `RDLevel.Rows.Remove()` instead. This method will be removed in the future.")]
		public bool RemoveRow(Row row)
		{
			if (Rows.Contains(row))
			{
				this.RemoveRange(row);
				return Rows.Remove(row);
			}
			else
				return false;
		}
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
			RDLevel? Read;
			if (extension != ".rdzip" && extension != ".zip")
			{
				if (extension != ".rdlevel")
					throw new RhythmBaseException("File not supported.");
				settings.OnBeforeReading();
				Read = LevelSerializer.Deserialize<RDLevel>(new JsonTextReader(File.OpenText(filepath)));
				settings.OnAfterReading();
			}
			else
				Read = ReadFromZip(filepath, settings);
			return Read ?? [];
		}
		/// <summary>
		/// Reads an RDLevel from a TextReader with the specified filepath and settings.
		/// </summary>
		/// <param name="reader">The TextReader to read from.</param>
		/// <param name="filepath">The filepath of the RDLevel.</param>
		/// <param name="settings">The settings to use for reading the RDLevel.</param>
		/// <returns>The deserialized RDLevel object.</returns>
		/// <exception cref="RhythmBaseException">Thrown when the file cannot be read.</exception>
		public static RDLevel Read(TextReader reader, string filepath, LevelReadOrWriteSettings settings)
		{
			JsonSerializer LevelSerializer = new();
			LevelSerializer.Converters.Add(new RDLevelConverter(filepath, settings));
			RDLevel Read;
			Read = LevelSerializer.Deserialize<RDLevel>(new JsonTextReader(reader)) ?? throw new RhythmBaseException("Cannot read the file.");
			return Read;
		}
		/// <summary>
		/// Reads an RDLevel from a TextReader with the specified settings.
		/// </summary>
		/// <param name="reader">The TextReader to read from.</param>
		/// <param name="settings">The settings to use for reading the RDLevel.</param>
		/// <returns>The deserialized RDLevel object.</returns>
		/// <exception cref="RhythmBaseException">Thrown when the file cannot be read.</exception>
		public static RDLevel Read(TextReader reader, LevelReadOrWriteSettings settings) => Read(reader, "", settings);
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
		public static RDLevel ReadFromZip(string filepath, LevelReadOrWriteSettings settings)
#if !NET7_0_OR_GREATER
		{
			DirectoryInfo tempDirectory = new(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "RhythmBaseTemp_" + System.IO.Path.GetRandomFileName()));
			tempDirectory.Create();
			RDLevel ReadFromZip;
			try
			{
				ZipFile.ExtractToDirectory(filepath, tempDirectory.FullName);
				ReadFromZip = Read(tempDirectory.GetFiles().Single(i => i.Extension == ".rdlevel").FullName, settings);
				ReadFromZip.isZip = true;
			}
			catch (InvalidOperationException ex)
			{
				tempDirectory.Delete(true);
				throw new RhythmBaseException("More than one RDLevel file has been found.", ex);
			}
			catch (Exception ex2)
			{
				tempDirectory.Delete(true);
				throw new RhythmBaseException("Cannot extract the file.", ex2);
			}
			return ReadFromZip;
		}
#else
			=> ReadFromZip(File.OpenRead(filepath), settings);
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
			DirectoryInfo tempDirectory = new(System.IO.Path.Combine(System.IO.Path.GetTempPath(), "RhythmBaseTemp_" + System.IO.Path.GetRandomFileName()));
			tempDirectory.Create();
			RDLevel ReadFromZip;
			try
			{
				ZipFile.ExtractToDirectory(stream, tempDirectory.FullName);
				ReadFromZip = Read(tempDirectory.GetFiles().Single(i => i.Extension == ".rdlevel").FullName, settings);
				ReadFromZip.isZip = true;
			}
			catch (InvalidOperationException ex)
			{
				tempDirectory.Delete(true);
				throw new RhythmBaseException("More than one RDLevel file has been found.", ex);
			}
			catch (Exception ex2)
			{
				tempDirectory.Delete(true);
				throw new RhythmBaseException("Cannot extract the file.", ex2);
			}
			return ReadFromZip;
		}
#endif
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
		public void Write(Stream stream) => Write(new StreamWriter(stream, System.Text.Encoding.UTF8, 1024, leaveOpen: true), new LevelReadOrWriteSettings());
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
		public override void Add(IBaseEvent item) => Add(item, true);
		public void Add(IBaseEvent item, bool keepPos = true)
		{
			//添加默认节拍
			((BaseEvent)item)._beat._calculator = Calculator;
			//部分事件只能在小节的开头
			if (item is IBarBeginningEvent @event && ((BaseEvent)item)._beat.BarBeat.beat != 1f)
				throw new IllegalBeatException(@event);
			//更改节拍的关联关卡
			((BaseEvent)item)._beat._calculator = Calculator;
			((BaseEvent)item)._beat.ResetCache();
			//重置调色板关联
			if (item is IColorEvent colorEvent)
				foreach (PropertyInfo info in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
					if (info.PropertyType == typeof(PaletteColor))
						typeof(PaletteColor).GetField(nameof(PaletteColor.parent), BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(info.GetValue(colorEvent), ColorPalette);
			if (item is Comment comment && comment.Parent == null)
				//注释事件可能在精灵板块，也可能不在
				base.Add(item);
			else if (item is TintRows tintRows && tintRows.Parent == null)
				base.Add(item);
			else if (item is BaseRowAction rowAction)
			{
				//添加至对应轨道
				Row? parent = rowAction.Parent ?? (rowAction.Index < Rows.Count ? Rows[rowAction.Index] : null);
				if (parent == null) Rows._unhandledRowEvents.Add(rowAction);
				else parent.AddInternal(rowAction);
				base.Add(item);
			}
			else if (item is BaseDecorationAction decoAction)
			{
				//添加至对应精灵
				Decoration? parent = decoAction.Parent ?? Decorations[decoAction._decoId];
				if (parent == null) Decorations._unhandledRowEvents.Add(decoAction);
				else parent.AddInternal(decoAction);
				base.Add(item);
			}
			//BPM 和 CPB
			else if (item is SetCrotchetsPerBar setCrochetsPerBar)
				AddSetCrotchetsPerBar(setCrochetsPerBar, keepPos);
			else if (item is BaseBeatsPerMinute baseBeatsPerMinute)
				AddBaseBeatsPerMinute(baseBeatsPerMinute);
			// 其他
			else
				base.Add(item);
			if (item is FloatingText floatingText)
				_floatingTexts.Add(floatingText);
		}
		/// <summary>
		/// Determine if the level contains this event.
		/// </summary>
		/// <param name="item">Event.</param>
		/// <returns></returns>
		public override bool Contains(IBaseEvent item) => EventTypeUtils.RowTypes.Contains(item.Type)
			&& Rows.Any((i) => i.Contains(item)) || EventTypeUtils.DecorationTypes.Contains(item.Type) && Decorations.Any((i) => i.Contains(item)) || base.Contains(item);
		/// <summary>
		/// Remove event from the level.
		/// </summary>
		/// <param name="item">Event to be removed.</param>
		/// <returns></returns>
		public override bool Remove(IBaseEvent item) => Remove(item, true);
		public bool Remove(IBaseEvent item, bool keepPos)
		{
			bool Remove;
			if (EventTypeUtils.RowTypes.Contains(item.Type)
				&& Rows.Any((i) => i.RemoveInternal((BaseRowAction)item)))
			{
				base.Remove(item);
				((BaseEvent)item)._beat._calculator = null;
				Remove = true;
			}
			else if (EventTypeUtils.DecorationTypes.Contains(item.Type)
					&& Decorations.Any((i) => i.RemoveInternal((BaseDecorationAction)item)))
			{
				base.Remove(item);
				((BaseEvent)item)._beat._calculator = null;
				Remove = true;
			}
			else if (Contains(item))
			{
				if (item.Type == EventType.SetCrotchetsPerBar)
					Remove = RemoveSetCrotchetsPerBar((SetCrotchetsPerBar)item, keepPos);
				else if (EventTypeUtils.ToEnums<BaseBeatsPerMinute>().Contains(item.Type))
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
			if (item is FloatingText floatingText)
				_floatingTexts.Remove(floatingText);
			return Remove;
		}
		/// <summary>
		/// Gets the status of the level at the specified beat.
		/// </summary>
		/// <param name="beat">The beat at which to get the status.</param>
		/// <returns>A new instance of <see cref="RDStatus"/> representing the status at the specified beat.</returns>
		internal RDStatus GetStatus(RDBeat beat)
		{
			return new()
			{
				Beat = beat,
				RoomStatus = [
					..new RDRoomIndex[] {
						RDRoomIndex.Room1,
						RDRoomIndex.Room2,
						RDRoomIndex.Room3,
						RDRoomIndex.Room4,
						RDRoomIndex.RoomTop,
					}.Select(room=>
						new RoomStatus(){
							Beat = beat,
							RunningVFXs =[.. this.Where<SetVFXPreset>(vfx=>vfx.Rooms.Contains(room)&& vfx.VFXDuration().Contains(beat), new RDRange(null,beat))],
							Background = this.Where<SetBackgroundColor>(j=>j.Rooms.Contains(room),new RDRange(null,beat)).LastOrDefault(),
							BassDrop = this.Where<BassDrop>(j=>j.Rooms.Contains(room),new RDRange(null,beat)).LastOrDefault(),
							Flash = this.Where<Flash>(j=>j.Rooms.Contains(room),new RDRange(null,beat)).LastOrDefault(),
							Flip = this.Where<FlipScreen>(j=>j.Rooms.Contains(room),new RDRange(null,beat)).LastOrDefault(),
							Foreground = this.Where<SetForeground>(j=>j.Rooms.Contains(room),new RDRange(null,beat)).LastOrDefault(),
							Shake = this.Where<ShakeScreen>(j=>j.Rooms.Contains(room),new RDRange(null,beat)).LastOrDefault(),
							Stutter = this.Where<Stutter>(j=>j.Rooms.Contains(room),new RDRange(null,beat)).LastOrDefault(),
							Theme = this.Where<SetTheme>(j=>j.Rooms.Contains(room),new RDRange(null,beat)).LastOrDefault()?.Preset ?? Themes.None,
						}
					)
				],
				RowStatus = [..Rows.Select(i=>new RowStatus(){
					Beat = beat,
					ParentRow = i,
					PlayerType = this.LastOrDefault<ChangePlayersRows>(j => j.Players[i.Index] != PlayerType.NoChange)?.Players[i.Index]??i.Player,
					Sound = this.LastOrDefault<SetBeatSound>()?.Sound??i.Sound
				})
				]
			};
		}
		/// <summary>  
		/// Extracts all group events from the level and adds their individual events to the level.  
		/// </summary>  
		/// <remarks>  
		/// This method iterates through all group events in the level, adds their individual events to the level,  
		/// and then removes the original group events.  
		/// </remarks>  
		public void ExtractGroups()
		{
			IEnumerable<Group> groups = this.Where<Group>();
			foreach (var group in groups)
				this.AddRange(group);
			this.RemoveRange(groups);
		}
		private void AddSetCrotchetsPerBar(SetCrotchetsPerBar item, bool keepCpb = true)
		{
			if (keepCpb)
			{
				SetCrotchetsPerBar? frt = item.FrontOrDefault();
				SetCrotchetsPerBar? nxt = item.NextOrDefault();
				//更新拍号
				//RefreshCPBs(item._beat);
				//添加事件
				base.Add(item);
				//更新计算器
				Calculator.Refresh();
				if (nxt != null)
				{
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
		private bool RemoveSetCrotchetsPerBar(SetCrotchetsPerBar item, bool keepCpb = true)
		{
			if (keepCpb)
			{
				SetCrotchetsPerBar? frt = item.FrontOrDefault();
				SetCrotchetsPerBar? nxt = item.NextOrDefault();
				if (nxt != null)
				{
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
		private void AddBaseBeatsPerMinute(BaseBeatsPerMinute item)
		{
			//RefreshBPMs(item.Beat);
			base.Add(item);
			Calculator.Refresh();
		}
		private bool RemoveBaseBeatsPerMinute(BaseBeatsPerMinute item)
		{
			bool result = base.Remove(item);
			Calculator.Refresh();
			//RefreshBPMs(item.Beat);
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
		internal string _path;
		private bool isZip = false;
		private RDColor[] colorPalette = new RDColor[21];
		internal List<FloatingText> _floatingTexts = [];
		/// <summary>
		/// Variables.
		/// </summary>
		[JsonIgnore]
		public readonly RDVariables Variables;
	}
}
