using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RhythmBase.Components;
using RhythmBase.Exceptions;
using RhythmBase.Extensions;
using RhythmBase.Settings;
using SkiaSharp;

namespace RhythmBase.Assets
{
	/// <summary>
	/// A reference to an asset file.
	/// </summary>

	public class SpriteFile : ISpriteFile
	{
		private bool _isModified = false;
		/// <inheritdoc/>
		[JsonIgnore]
		public bool IsModified
		{
			get => _isModified;
			private set => _isModified = value;
		}
		/// <inheritdoc/>
		[JsonIgnore]
		public string FilePath { get; }
		/// <summary>
		/// The expression names of the sprite file.
		/// </summary>
		[JsonIgnore]
		public IEnumerable<string> Expressions => Clips.Select((i) => i.Name);
		/// <summary>
		/// The name of the file.
		/// </summary>
		[JsonIgnore]
		public string FileName { get; }
		/// <summary>
		/// The area where the sprite is previewed.
		/// </summary>
		[JsonIgnore]
		public SKRect? Preview => new SKRect?((RowPreviewFrame == null) ? default(SKRect) : GetFrame(checked((int)RowPreviewFrame.Value)));
		/// <summary>
		/// The size of the sprite/image
		/// </summary>
		[JsonIgnore]
		public RDSizeNI ImageSize => _imageSize;
		/// <summary>
		/// Base layer
		/// </summary>
		[JsonIgnore]
		public SKBitmap ImageBase { get; set; }
		/// <summary>
		/// Glow layer
		/// </summary>
		[JsonIgnore]
		public SKBitmap ImageGlow { get; set; }
		/// <summary>
		/// Outline layer
		/// </summary>
		[JsonIgnore]
		public SKBitmap ImageOutline { get; set; }
		/// <summary>
		/// Freeze layer
		/// </summary>
		[JsonIgnore]
		public SKBitmap ImageFreeze { get; set; }
		/// <summary>
		/// The name of the sprite.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// [Unknown] The voice of the sprite.
		/// </summary>
		public string? Voice { get; set; }
		/// <summary>
		/// The size of each expression.
		/// </summary>
		public RDSizeNI Size { get; set; }
		/// <summary>
		/// Information of expressions.
		/// </summary>
		public HashSet<Expression> Clips { get; set; } = [];
		/// <summary>
		/// Image offset when the row is previewed.
		/// </summary>
		public RDSizeN? RowPreviewOffset { get; set; }
		/// <summary>
		/// Row preview frame.
		/// </summary>
		public uint? RowPreviewFrame { get; set; }
		/// <summary>
		/// Pivot point offset.
		/// </summary>
		public RDPointN? PivotOffset { get; set; }
		/// <summary>
		/// Image offset in dialog box.
		/// </summary>
		public RDSizeN? PortraitOffset { get; set; }
		/// <summary>
		/// Image clipping in the dialog box.
		/// </summary>
		public RDSizeN? PortraitSize { get; set; }
		/// <summary>
		/// Image scale in the dialog box.
		/// </summary>
		public float? PortraitScale { get; set; }
		public SpriteFile()
		{
		}
		/// <summary>
		/// Create a reference to the file. The contents of the file are not read.
		/// </summary>
		/// <param name="filename">File path.</param>
		public SpriteFile(string filename)
		{
			bool flag = filename.IsNullOrEmpty();
			if (flag)
			{
				throw new ArgumentException("Filename cannot be null.", nameof(filename));
			}
			//this._FileName = filename;
		}
		/// <summary>
		/// Load the file contents into memory.
		/// </summary>
		public static IAssetFile? Load(string path)
		{
			SpriteFile sprite = new();
			string _file = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
			JsonSerializer setting = new();
			bool flag = File.Exists(string.Format("{0}.json", _file));
			string json;
			if (flag)
			{
				json = string.Format("{0}", _file);
			}
			else
			{
				bool flag2 = File.Exists(string.Format("{0}\\{1}.json", _file, Path.GetFileName(_file)));
				if (!flag2)
				{
					throw new FileNotFoundException("Cannot find the json file", _file ?? "");
				}
				json = string.Format("{0}\\{1}", _file, Path.GetFileName(_file));
			}
			JObject obj = setting.Deserialize<JObject>(new JsonTextReader(File.OpenText(string.Format("{0}.json", json))))!;
			string imageBaseFile = string.Format("{0}.png", json);
			string imageGlowFile = string.Format("{0}_glow.png", json);
			string imageOutlineFile = string.Format("{0}_outline.png", json);
			string imageFreezeFile = string.Format("{0}_freeze.png", json);
			if (File.Exists(imageBaseFile))
			{
				sprite.ImageBase = SKBitmap.Decode(imageBaseFile);
				if (File.Exists(imageGlowFile))
					sprite.ImageGlow = SKBitmap.Decode(imageGlowFile);
				if (File.Exists(imageOutlineFile))
					sprite.ImageOutline = SKBitmap.Decode(imageOutlineFile);
				if (File.Exists(imageFreezeFile))
					sprite.ImageFreeze = SKBitmap.Decode(imageFreezeFile);
				JToken? jtoken = obj["Name".ToLowerCamelCase()];
				sprite.Name = jtoken?.ToObject<string>()!;
				JToken? jtoken2 = obj["Voice".ToLowerCamelCase()];
				sprite.Voice = jtoken2?.ToObject<string>();
				sprite.Size = obj[nameof(Size).ToLowerCamelCase()]!.ToObject<RDSizeNI>();
				JToken? jtoken3 = obj["RowPreviewOffset".ToLowerCamelCase()];
				sprite.RowPreviewOffset = (jtoken3 != null) ? new RDSizeN?(jtoken3.ToObject<RDSizeN>()) : null;
				JToken? jtoken4 = obj["RowPreviewFrame".ToLowerCamelCase()];
				sprite.RowPreviewFrame = (jtoken4 != null) ? new uint?(jtoken4.ToObject<uint>()) : null;
				JToken? jtoken5 = obj["PivotOffset".ToLowerCamelCase()];
				sprite.PivotOffset = (jtoken5 != null) ? new RDPointN?(jtoken5.ToObject<RDPointN>()) : null;
				JToken? jtoken6 = obj["PortraitOffset".ToLowerCamelCase()];
				sprite.PortraitOffset = (jtoken6 != null) ? new RDSizeN?(jtoken6.ToObject<RDSizeN>()) : null;
				JToken? jtoken7 = obj["PortraitSize".ToLowerCamelCase()];
				sprite.PortraitSize = (jtoken7 != null) ? new RDSizeN?(jtoken7.ToObject<RDSizeN>()) : null;
				JToken? jtoken8 = obj["PortraitScale".ToLowerCamelCase()];
				sprite.PortraitScale = (jtoken8 != null) ? new float?(jtoken8.ToObject<float>()) : null;
				foreach (JToken clip in obj[nameof(Clips).ToLowerCamelCase()] ?? new JObject())
					sprite.Clips.Add(clip.ToObject<Expression>()!);
				return sprite;
			}
			throw new FileNotFoundException("Cannot find the image file", _file + ".png");
		}
		public void Save() { throw new NotImplementedException(); }
		/// <summary>
		/// Write JSON data to the text stream.
		/// </summary>
		/// <param name="textWriter">Text writer stream.</param>
		public void WriteJson(TextWriter textWriter) => WriteJson(textWriter, new SpriteReadOrWriteSettings());
		/// <summary>
		/// Write JSON data to the text stream.
		/// </summary>
		/// <param name="textWriter">Text writer stream.</param>
		/// <param name="setting">Write settings.</param>
		public void WriteJson(TextWriter textWriter, SpriteReadOrWriteSettings setting)
		{
			JsonSerializerSettings jsonS = new()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				Formatting = Formatting.None
			};
			JsonTextWriter writer = new(textWriter)
			{
				Formatting = setting.Indented ? Formatting.Indented : Formatting.None
			};
			JObject meObj = JObject.FromObject(this);
			meObj.ToLowerCamelCase();
			JArray? clipArray = (JArray?)meObj["Clips".ToLowerCamelCase()];
			Dictionary<string, int> PropertyNameLength = [];
			Dictionary<string, List<string>> propertyValues = [];
			foreach (JToken jtoken in clipArray)
			{
				JObject clip = (JObject)jtoken;
				clip.ToLowerCamelCase();
				foreach (KeyValuePair<string, JToken> pair in clip)
				{
					string stringedValue = (pair.Value.Type == JTokenType.Null) ? string.Empty : JsonConvert.SerializeObject(pair.Value, Formatting.None, jsonS);
					List<string> value = null;
					if (propertyValues.TryGetValue(pair.Key, out value))
						value.Add(stringedValue);
					else
						propertyValues[pair.Key] = [stringedValue];
					if (PropertyNameLength.ContainsKey(pair.Key))
						PropertyNameLength[pair.Key] = Math.Max(PropertyNameLength[pair.Key], stringedValue.Length);
					else
						PropertyNameLength[pair.Key] = stringedValue.Length;
				}
			}
			if (!setting.IgnoreNullValue)
				foreach (KeyValuePair<string, List<string>> pair2 in propertyValues)
					if (pair2.Value.Contains(string.Empty) &&
						pair2.Value.Any(i => i != string.Empty))
						PropertyNameLength[pair2.Key] = Math.Max(PropertyNameLength[pair2.Key], 4);

			meObj.Remove("Clips".ToLowerCamelCase());
			JsonTextWriter jsonTextWriter = writer;
			jsonTextWriter.WriteStartObject();
			foreach (KeyValuePair<string, JToken> pair3 in meObj)
			{
				bool flag5 = pair3.Value.Type != JTokenType.Null;
				if (flag5)
				{
					jsonTextWriter.WritePropertyName(pair3.Key);
					jsonTextWriter.WriteRawValue(JsonConvert.SerializeObject(pair3.Value, Formatting.None, jsonS));
				}
			}
			jsonTextWriter.WritePropertyName("Clips".ToLowerCamelCase());
			jsonTextWriter.WriteStartArray();
			checked
			{
				int num = clipArray.Count - 1;
				for (int j = 0; j <= num; j++)
				{
					jsonTextWriter.WriteStartObject();
					writer.Formatting = Formatting.None;
					foreach (KeyValuePair<string, List<string>> pair4 in propertyValues)
					{
						bool flag6 = PropertyNameLength[pair4.Key] > 0;
						if (flag6)
						{
							bool ignoreNullValue = setting.IgnoreNullValue;
							if (ignoreNullValue)
							{
								bool flag7 = pair4.Value[j].IsNullOrEmpty();
								if (flag7)
								{
									bool indented = setting.Indented;
									if (indented)
									{
										jsonTextWriter.WriteWhitespace(string.Empty.PadRight(pair4.Key.Length + PropertyNameLength[pair4.Key] + 4));
									}
								}
								else
								{
									jsonTextWriter.WritePropertyName(pair4.Key);
									jsonTextWriter.WriteRawValue(pair4.Value[j].PadRight(setting.Indented ? PropertyNameLength[pair4.Key] : 0));
								}
							}
							else
							{
								jsonTextWriter.WritePropertyName(pair4.Key);
								bool flag8 = pair4.Value[j].IsNullOrEmpty();
								if (flag8)
								{
									jsonTextWriter.WriteRawValue(JsonConvert.Null.PadRight(setting.Indented ? PropertyNameLength[pair4.Key] : 0));
								}
								else
								{
									jsonTextWriter.WriteRawValue(pair4.Value[j].PadRight(setting.Indented ? PropertyNameLength[pair4.Key] : 0));
								}
							}
						}
					}
					jsonTextWriter.WriteEndObject();
					writer.Formatting = setting.Indented ? Formatting.Indented : Formatting.None;
				}
				jsonTextWriter.WriteEndArray();
				jsonTextWriter.WriteEndObject();
				jsonTextWriter = null;
				textWriter.Flush();
			}
		}
		/// <summary>
		/// Gets the frame crop area.
		/// </summary>
		/// <param name="index">Frame index.</param>
		/// <returns>A rectangular area that indicates the cropping area.</returns>
		public SKRectI GetFrame(int index)
		{
			bool flag = index < 0;
			if (flag)
			{
				throw new OverflowException();
			}
			return GetFrameRect(checked((uint)index), ImageSize.ToSKSizeI(), Size.ToSKSizeI());
		}
		private static SKRectI GetFrameRect(uint index, SKSizeI source, SKSizeI size)
		{
			int column = source.Width / size.Width;
			SKPointI leftTop = new(
				(int)(index % column * size.Width),
				(int)(index / column * size.Height));
			return new(leftTop.X,
					   leftTop.Y,
					   leftTop.X + size.Width,
					   leftTop.Y + size.Height);
		}
		/// <summary>
		/// Add a blank expression.
		/// </summary>
		/// <param name="name">Expression name.</param>
		/// <returns>Added expression. Further changes can be made on top of this.</returns>
		public Expression AddBlankExpression(string name)
		{
			Expression C = Clips.FirstOrDefault(new Expression
			{
				Name = name
			});
			Clips.Add(C);
			return C;
		}
		/// <summary>
		/// Add a blank emoticon to the creation of character assets.
		/// </summary>
		/// <returns>Added expressions. Further changes can be made on top of these.</returns>
		public IEnumerable<Expression> AddBlankExpressionsForCharacter() => from n in characterExpressionNames select AddBlankExpression(n);
		/// <summary>
		/// Add a blank emoticon to the creation of sprite assets.
		/// </summary>
		/// <returns>Added expression. Further changes can be made on top of this.</returns>
		public IEnumerable<Expression> AddBlankExpressionForDecoration() => [AddBlankExpression("neutral")];
		/// <summary>
		/// Save the file.
		/// </summary>
		/// <param name="path">the file path.</param>
		/// <exception cref="T:RhythmBase.Exceptions.OverwriteNotAllowedException">The save path is the same as the reference path.</exception>
		public void Save(string path) => Save(path, new SpriteReadOrWriteSettings());
		/// <summary>
		/// Save the file.
		/// </summary>
		/// <param name="path">the file path.</param>
		/// <param name="settings">save settings.</param>
		/// <exception cref="T:RhythmBase.Exceptions.OverwriteNotAllowedException">The save path is the same as the reference path.</exception>
		public void Save(string path, SpriteReadOrWriteSettings settings)
		{
			FileInfo file = new(path);
			string WithoutExtension = Path.Combine(file.Directory.FullName, Path.GetFileNameWithoutExtension(file.Name));
			bool flag = (File.Exists(WithoutExtension + ".json") || (settings.WithImage && File.Exists(WithoutExtension + ".png"))) & !settings.OverWrite;
			if (flag)
			{
				throw new OverwriteNotAllowedException(path, typeof(LevelReadOrWriteSettings));
			}
			bool withImage = settings.WithImage;
			if (withImage)
			{
				ImageBase.Save(WithoutExtension + ".png");
				SKBitmap imageGlow = ImageGlow;
				if (imageGlow != null)
				{
					imageGlow.Save(WithoutExtension + "_glow.png");
				}
				SKBitmap imageOutline = ImageOutline;
				if (imageOutline != null)
				{
					imageOutline.Save(WithoutExtension + "_outline.png");
				}
				SKBitmap imageFreeze = ImageFreeze;
				if (imageFreeze != null)
				{
					imageFreeze.Save(WithoutExtension + "_freeze.png");
				}
			}
			WriteJson(new FileInfo(WithoutExtension + ".json").CreateText(), settings);
		}
		public override string ToString() => Name.IsNullOrEmpty() ? FileName : Name;
		private RDSizeNI _imageSize;
		private static readonly string[] characterExpressionNames =
			[
				"neutral",
				"happy",
				"barely",
				"missed"
			];
		/// <summary>
		/// An expression.
		/// </summary>
		public class Expression
		{
			/// <summary>
			/// Expression name.
			/// </summary>
			public string Name { get; set; }
			/// <summary>
			/// The list of frame indexes for expression.
			/// </summary>
			public List<uint> Frames { get; set; }
			/// <summary>
			/// The start frame of the cycle for the expression.
			/// </summary>
			public int? LoopStart { get; set; }
			/// <summary>
			/// The way the expression loops.
			/// </summary>
			[JsonConverter(typeof(StringEnumConverter))]
			public LoopOption Loop { get; set; }
			/// <summary>
			/// The frame rate of the emoticon when <c>loop == yes</c>.
			/// </summary>
			public float Fps { get; set; }
			/// <summary>
			/// Pivot point offset.
			/// </summary>
			public RDPointN? PivotOffset { get; set; }
			/// <summary>
			/// Image offset in dialog box.
			/// </summary>
			public RDSizeN? PortraitOffset { get; set; }
			/// <summary>
			/// Image scale in the dialog box.
			/// </summary>
			public float? PortraitScale { get; set; }
			/// <summary>
			/// Image clipping in the dialog box.
			/// </summary>
			public RDSizeN? PortraitSize { get; set; }
			/// <summary>
			/// Get the cropped area on the image for each frame of this expression.
			/// </summary>
			/// <returns>An array of rectangles indicating each crop area.</returns>
			public SKRectI[] GetFrameRects() => (from i in Frames
												 select parent.GetFrame(checked((int)i))).ToArray();
			public override string ToString() => Name;
			internal SpriteFile parent;
		}
	}
}
