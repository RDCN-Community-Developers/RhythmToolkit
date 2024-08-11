Imports System.Data
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports NAudio
Imports NAudio.Wave
Imports NAudio.Vorbis
Imports RhythmBase.Extensions.Extensions
Imports SkiaSharp
Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports RhythmBase.Events.ShowDialogue
Imports System.Drawing
Imports System.Runtime.InteropServices.JavaScript.JSType
Imports System.Security
Namespace Assets
	Public Class AssetManager
		Private baseLevel As RDLevel
		Private data As Dictionary(Of TypeNameIndex, IAssetFile)
		Private Structure TypeNameIndex
			Implements IEquatable(Of TypeNameIndex)
			Public Type As Type
			Public Name As String
			Public Sub New(type As Type, name As String)
				Me.Type = type
				Me.Name = name
			End Sub
			Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
				Return obj.GetType = GetType(TypeNameIndex) Or Equals(CType(obj, TypeNameIndex))
			End Function
			Public Overloads Function Equals(other As TypeNameIndex) As Boolean Implements IEquatable(Of TypeNameIndex).Equals
				Return Name = other.Name AndAlso Name = other.Name
			End Function
			Public Overrides Function GetHashCode() As Integer
				Return HashCode.Combine(Type, Name)
			End Function
		End Structure
		Default Public Property Item(type As Type, name As String)
			Get
				Return data(New TypeNameIndex(type, name))
			End Get
			Set(value)
				data(New TypeNameIndex(type, name)) = value
			End Set
		End Property
		Default Public Property Item(index As Asset(Of IAssetFile))
			Get
				Return Item(index.Type, index.Name)
			End Get
			Set(value)
				Item(index.Type, index.Name) = value
			End Set
		End Property
		Public Function Create(Of TAsset As IAssetFile)(name As String)
			Return New Asset(Of TAsset)(Me) With {.Name = name}
		End Function
		Public Function Create(Of TAsset As IAssetFile)(name As String, asset As IAssetFile)
			Return New Asset(Of TAsset)(Me) With {.Name = name, .Value = asset}
		End Function
		Public Sub New(level As RDLevel)
			baseLevel = level
		End Sub
	End Class
	Public Interface IAssetFile
		ReadOnly Property Name As String
		Sub Load(directory As String)
	End Interface
	Public Interface ISpriteFile
		Inherits IAssetFile
		ReadOnly Property Size As RDSizeNI
	End Interface
	Public Class Asset(Of TAssetFile As IAssetFile)
		Private cache As TAssetFile
		Private _name As String
		Private _connected As Boolean
		Public ReadOnly Property Type
			Get
				Return GetType(TAssetFile)
			End Get
		End Property
		Public Property Name As String
			Get
				Return _name
			End Get
			Set(value As String)
				If _name <> value Then
					cache = Nothing
				End If
				_name = value
			End Set
		End Property
		Public Property Value As TAssetFile
			Get
				If Manager IsNot Nothing Then
					cache = Manager(Type, Name)
				End If
				Return cache
			End Get
			Set(value As TAssetFile)
				If Manager IsNot Nothing Then
					Manager(Type, Name) = value
				Else
					cache = value
				End If
			End Set
		End Property

		Private Property Manager As AssetManager

		Friend Sub New(manager As AssetManager)
			Me.Manager = manager
			Me._connected = True
		End Sub
		Friend Sub New()
		End Sub
	End Class
	''' <summary>
	''' A reference to an asset file.
	''' </summary>
	Public Class SpriteFile
		Implements ISpriteFile
		Private _imageSize As RDSizeNI
		''' <summary>
		''' The expression names of the sprite file.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property Expressions As IEnumerable(Of String)
			Get
				Return Clips.Select(Function(i) i.Name)
			End Get
		End Property
		''' <summary>
		''' The name of the file.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property FileName As String Implements IAssetFile.Name
		''' <summary>
		''' The area where the sprite is previewed.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property Preview As SKRect?
			Get
				Return If(RowPreviewFrame Is Nothing, New SKRect, GetFrame(RowPreviewFrame))
			End Get
		End Property
		''' <summary>
		''' The size of the sprite/image
		''' </summary>
		<JsonIgnore> Public ReadOnly Property ImageSize As RDSizeNI
			Get
				Return _imageSize
			End Get
		End Property
		''' <summary>
		''' Base layer
		''' </summary>
		<JsonIgnore> Public Property ImageBase As SKBitmap
		''' <summary>
		''' Glow layer
		''' </summary>
		<JsonIgnore> Public Property ImageGlow As SKBitmap
		''' <summary>
		''' Outline layer
		''' </summary>
		<JsonIgnore> Public Property ImageOutline As SKBitmap
		''' <summary>
		''' Freeze layer
		''' </summary>
		<JsonIgnore> Public Property ImageFreeze As SKBitmap
		''' <summary>
		''' The name of the sprite.
		''' </summary>
		Public Property Name As String
#If DEBUG Then
		''' <summary>
		''' [Unknown] The voice of the sprite.
		''' </summary>
		Public Property Voice As String
#End If
		''' <summary>
		''' The size of each expression.
		''' </summary>
		Public Property Size As RDSizeNI Implements ISpriteFile.Size
		''' <summary>
		''' Information of expressions.
		''' </summary>
		Public Property Clips As HashSet(Of Expression)
		''' <summary>
		''' Image offset when the row is previewed.
		''' </summary>
		Public Property RowPreviewOffset As RDSizeN?
		''' <summary>
		''' Row preview frame.
		''' </summary>
		Public Property RowPreviewFrame As UInteger?
		''' <summary>
		''' Pivot point offset.
		''' </summary>
		Public Property PivotOffset As RDPointN?
		''' <summary>
		''' Image offset in dialog box.
		''' </summary>
		Public Property PortraitOffset As RDSizeN?
		''' <summary>
		''' Image clipping in the dialog box.
		''' </summary>
		Public Property PortraitSize As RDSizeN?
		''' <summary>
		''' Image scale in the dialog box.
		''' </summary>
		Public Property PortraitScale As Single?
		''' <summary>
		''' An expression.
		''' </summary>
		Public Class Expression
			Friend parent As SpriteFile
			''' <summary>
			''' Expression name.
			''' </summary>
			Public Property Name As String
			''' <summary>
			''' The list of frame indexes for expression.
			''' </summary>
			Public Property Frames As List(Of UInteger) 'nullable
			''' <summary>
			''' The start frame of the cycle for the expression.
			''' </summary>
			Public Property LoopStart As Integer? 'nullable
			''' <summary>
			''' The way the expression loops.
			''' </summary>
			<JsonConverter(GetType(Newtonsoft.Json.Converters.StringEnumConverter))> Public Property [Loop] As LoopOption
			''' <summary>
			''' The frame rate of the emoticon when <c>loop == yes</c>.
			''' </summary>
			Public Property Fps As Single
			''' <summary>
			''' Pivot point offset.
			''' </summary>
			Public Property PivotOffset As RDPointN? 'nullable
			''' <summary>
			''' Image offset in dialog box.
			''' </summary>
			Public Property PortraitOffset As RDSizeN? 'nullable
			''' <summary>
			''' Image scale in the dialog box.
			''' </summary>
			Public Property PortraitScale As Single? 'nullable
			''' <summary>
			''' Image clipping in the dialog box.
			''' </summary>
			Public Property PortraitSize As RDSizeN? 'nullable
			''' <summary>
			''' Get the cropped area on the image for each frame of this expression.
			''' </summary>
			''' <returns>An array of rectangles indicating each crop area.</returns>
			Public Function GetFrameRects() As SKRectI()
				Return Frames.Select(Function(i) parent.GetFrame(i)).ToArray
			End Function
			Public Overrides Function ToString() As String
				Return Name
			End Function
		End Class
		Public Sub New()
		End Sub
		''' <summary>
		''' Create a reference to the file. The contents of the file are not read.
		''' </summary>
		''' <param name="filename">File path.</param>
		Public Sub New(filename As String)
			If filename.IsNullOrEmpty Then
				Throw New ArgumentException("Filename cannot be null.", NameOf(filename))
			End If
			Me.FileName = filename
		End Sub
		''' <summary>
		''' Load the file contents into memory.
		''' </summary>
		Public Sub Load(directory As String) Implements IAssetFile.Load
			Dim _file = Path.Combine(directory, FileName)

			Dim setting As New JsonSerializer

			Dim json As String
			Dim obj As Linq.JObject

			If File.Exists($"{_file}.json") Then
				json = $"{_file}"
			ElseIf File.Exists($"{_file}\{Path.GetFileName(_file)}.json") Then
				json = $"{_file}\{Path.GetFileName(_file)}"
			Else
				Throw New FileNotFoundException($"Cannot find the json file", _file + "")
			End If

			obj = setting.Deserialize(Of JObject)(New JsonTextReader(File.OpenText($"{json}.json")))

			Dim imageBaseFile = $"{json}.png"
			Dim imageGlowFile = $"{json}_glow.png"
			Dim imageOutlineFile = $"{json}_outline.png"
			Dim imageFreezeFile = $"{json}_freeze.png"

			If File.Exists(imageBaseFile) Then
				_ImageBase = SKBitmap.Decode(imageBaseFile)
			Else
				Throw New FileNotFoundException($"Cannot find the image file", _file + ".png")
			End If
			If File.Exists(imageGlowFile) Then
				_ImageGlow = SKBitmap.Decode(imageGlowFile)
			End If
			If File.Exists(imageOutlineFile) Then
				_ImageOutline = SKBitmap.Decode(imageOutlineFile)
			End If
			If File.Exists(imageFreezeFile) Then
				_ImageFreeze = SKBitmap.Decode(imageFreezeFile)
			End If
			_Name = obj(NameOf(Name).ToLowerCamelCase)?.ToObject(Of String)
#If DEBUG Then
			_Voice = obj(NameOf(Voice).ToLowerCamelCase)?.ToObject(Of String)
#End If
			_Size = obj(NameOf(Size).ToLowerCamelCase).ToObject(Of RDSizeNI)
			_RowPreviewOffset = obj(NameOf(RowPreviewOffset).ToLowerCamelCase)?.ToObject(Of RDSizeN)
			_RowPreviewFrame = obj(NameOf(RowPreviewFrame).ToLowerCamelCase)?.ToObject(Of UInteger)
			_PivotOffset = obj(NameOf(PivotOffset).ToLowerCamelCase)?.ToObject(Of RDPointN)
			_PortraitOffset = obj(NameOf(PortraitOffset).ToLowerCamelCase)?.ToObject(Of RDSizeN)
			_PortraitSize = obj(NameOf(PortraitSize).ToLowerCamelCase)?.ToObject(Of RDSizeN)
			_PortraitScale = obj(NameOf(PortraitScale).ToLowerCamelCase)?.ToObject(Of Single)
			For Each clip In obj(NameOf(Clips).ToLowerCamelCase)
				_Clips.Add(clip.ToObject(Of Expression))
			Next
		End Sub
		''' <summary>
		''' Write JSON data to the text stream.
		''' </summary>
		''' <param name="textWriter">Text writer stream.</param>
		Public Sub WriteJson(textWriter As TextWriter)
			WriteJson(textWriter, New SpriteReadOrWriteSettings)
		End Sub
		''' <summary>
		''' Write JSON data to the text stream.
		''' </summary>
		''' <param name="textWriter">Text writer stream.</param>
		''' <param name="setting">Write settings.</param>
		Public Sub WriteJson(textWriter As TextWriter, setting As SpriteReadOrWriteSettings)
			Dim jsonS As New JsonSerializerSettings With {
				.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver,
				.NullValueHandling = NullValueHandling.Ignore,
				.Formatting = Formatting.None
				}
			Dim writer = New JsonTextWriter(textWriter) With {.Formatting = If(setting.Indented, Formatting.Indented, Formatting.None)}
			Dim meObj = JObject.FromObject(Me)
			meObj.ToLowerCamelCase
			Dim clipArray As JArray = meObj(NameOf(Clips).ToLowerCamelCase)

			Dim PropertyNameLength As New Dictionary(Of String, Integer)

			Dim propertyValues As New Dictionary(Of String, List(Of String))

			For Each clip As JObject In clipArray
				clip.ToLowerCamelCase
				For Each pair In clip
					Dim stringedValue = If(pair.Value.Type = Linq.JTokenType.Null, String.Empty, JsonConvert.SerializeObject(pair.Value, Formatting.None, jsonS))

					Dim value As List(Of String) = Nothing

					If propertyValues.TryGetValue(pair.Key, value) Then
						value.Add(stringedValue)
					Else
						propertyValues(pair.Key) = New List(Of String) From {stringedValue}
					End If
					If PropertyNameLength.ContainsKey(pair.Key) Then
						PropertyNameLength(pair.Key) = Math.Max(PropertyNameLength(pair.Key), stringedValue.Length)
					Else
						PropertyNameLength(pair.Key) = stringedValue.Length
					End If
				Next
			Next

			If Not setting.IgnoreNullValue Then
				For Each pair In propertyValues
					If pair.Value.Contains(String.Empty) AndAlso pair.Value.Any(Function(i) i <> String.Empty) Then
						PropertyNameLength(pair.Key) = Math.Max(PropertyNameLength(pair.Key), 4)
					End If
				Next
			End If

			meObj.Remove(NameOf(Clips).ToLowerCamelCase)

			With writer
				.WriteStartObject()
				For Each pair In meObj
					If Not pair.Value.Type = Linq.JTokenType.Null Then
						.WritePropertyName(pair.Key)
						.WriteRawValue(JsonConvert.SerializeObject(pair.Value, Formatting.None, jsonS))
					End If
				Next

				.WritePropertyName(NameOf(Clips).ToLowerCamelCase)
				.WriteStartArray()
				For i = 0 To clipArray.Count - 1
					.WriteStartObject()
					writer.Formatting = Formatting.None
					For Each pair In propertyValues
						If PropertyNameLength(pair.Key) > 0 Then
							If setting.IgnoreNullValue Then
								If pair.Value(i).IsNullOrEmpty Then
									If (setting.Indented) Then
										.WriteWhitespace(String.Empty.PadRight(pair.Key.Length + PropertyNameLength(pair.Key) + 4))
									End If
								Else
									.WritePropertyName(pair.Key)
									.WriteRawValue(pair.Value(i).PadRight(If(setting.Indented, PropertyNameLength(pair.Key), 0)))
								End If
							Else
								.WritePropertyName(pair.Key)
								If pair.Value(i).IsNullOrEmpty Then
									.WriteRawValue(JsonConvert.Null.PadRight(If(setting.Indented, PropertyNameLength(pair.Key), 0)))
								Else
									.WriteRawValue(pair.Value(i).PadRight(If(setting.Indented, PropertyNameLength(pair.Key), 0)))
								End If

							End If
						End If
					Next
					.WriteEndObject()
					writer.Formatting = If(setting.Indented, Formatting.Indented, Formatting.None)
				Next
				.WriteEndArray()

				.WriteEndObject()
			End With
			textWriter.Flush()
		End Sub
		''' <summary>
		''' Gets the frame crop area.
		''' </summary>
		''' <param name="index">Frame index.</param>
		''' <returns>A rectangular area that indicates the cropping area.</returns>
		Public Function GetFrame(index As Integer) As SKRectI
			If index < 0 Then
				Throw New OverflowException
			End If
			Return GetFrameRect(index, ImageSize.ToSKSizeI, Size.ToSKSizeI)
		End Function
		Private Shared Function GetFrameRect(index As UInteger, source As SKSizeI, size As SKSizeI) As SKRectI
			Dim column As UInteger = (source.Width \ size.Width)
			Dim leftTop As New SKPointI(
				(index Mod column) * size.Width,
				(index \ column) * size.Height)
			Return New SKRectI(leftTop.X,
							   leftTop.Y,
							   leftTop.X + size.Width,
							   leftTop.Y + size.Height)
		End Function
		''' <summary>
		''' Add a blank expression.
		''' </summary>
		''' <param name="name">Expression name.</param>
		''' <returns>Added expression. Further changes can be made on top of this.</returns>
		Public Function AddBlankExpression(name As String) As Expression
			Dim C = Clips.FirstOrDefault(New Expression With {.Name = name})
			Clips.Add(C)
			Return C
		End Function
		''' <summary>
		''' Add a blank emoticon to the creation of character assets.
		''' </summary>
		''' <returns>Added expressions. Further changes can be made on top of these.</returns>
		Public Function AddBlankExpressionsForCharacter() As IEnumerable(Of Expression)
			Return From n In {"neutral", "happy", "barely", "missed"}
				   Select AddBlankExpression(n)
		End Function
		''' <summary>
		''' Add a blank emoticon to the creation of sprite assets.
		''' </summary>
		''' <returns>Added expression. Further changes can be made on top of this.</returns>
		Public Function AddBlankExpressionForDecoration() As IEnumerable(Of Expression)
			Return From n In {"neutral"}
				   Select AddBlankExpression(n)
		End Function
		''' <summary>
		''' Save the file.
		''' </summary>
		''' <param name="path">the file path.</param>
		''' <exception cref="OverwriteNotAllowedException">The save path is the same as the reference path.</exception>
		Public Sub Save(path As String)
			Save(path, New SpriteReadOrWriteSettings)
		End Sub
		''' <summary>
		''' Save the file.
		''' </summary>
		''' <param name="path">the file path.</param>
		''' <param name="settings">save settings.</param>
		''' <exception cref="OverwriteNotAllowedException">The save path is the same as the reference path.</exception>
		Public Sub Save(path As String, settings As SpriteReadOrWriteSettings)

			Dim file = New FileInfo(path)
			Dim WithoutExtension As String = IO.Path.Combine(file.Directory.FullName, IO.Path.GetFileNameWithoutExtension(file.Name))
			If (IO.File.Exists(WithoutExtension + ".json") OrElse
				(settings.WithImage AndAlso IO.File.Exists(WithoutExtension + ".png"))
				) And Not settings.OverWrite Then
				Throw New OverwriteNotAllowedException(path, GetType(LevelReadOrWriteSettings))
			End If

			If settings.WithImage Then
				ImageBase.Save(WithoutExtension + ".png")
				ImageGlow?.Save(WithoutExtension + "_glow.png")
				ImageOutline?.Save(WithoutExtension + "_outline.png")
				ImageFreeze?.Save(WithoutExtension + "_freeze.png")
			End If

			WriteJson(New FileInfo(WithoutExtension + ".json").CreateText, settings)

		End Sub
		Public Overrides Function ToString() As String
			Return $"{If(Name.IsNullOrEmpty, FileName, Name)}"
		End Function
	End Class
	Public Class ImageFile
		Implements ISpriteFile
		Private ReadOnly _filename As String
		Public Property Image As SKBitmap
		Public ReadOnly Property Size As RDSizeNI Implements ISpriteFile.Size
		Public ReadOnly Property Name As String Implements IAssetFile.Name
			Get
				Return _filename
			End Get
		End Property
		Public Sub New(filename As String)
			_filename = filename
		End Sub
		''' <summary>
		''' Load the file contents into memory.
		''' </summary>
		Public Sub Load(directory As String) Implements IAssetFile.Load
			Dim path = IO.Path.Combine(directory, Name)
			If IO.Path.Exists(path) Then
				Dim imgFile As SKBitmap
				Try
					imgFile = SKBitmap.Decode(path)
					Image = imgFile
				Catch ex As Exception
				End Try
			Else
				Throw New FileNotFoundException($"Cannot find the image file", path)
			End If
		End Sub
	End Class
	''' <summary>
	''' A Character.
	''' </summary>
	Public Class RDCharacter

		''' <summary>
		''' Whether  in-game character or customized character(sprite).
		''' </summary>
		Public ReadOnly Property IsCustom As Boolean
		''' <summary>
		''' In-game character.
		''' <br/>
		''' If using a customized character, this value will be empty
		''' </summary>
		Public ReadOnly Property Character As Characters?
		''' <summary>
		''' Customized character(sprite).
		''' <br/>
		''' If using an in-game character, this value will be empty
		''' </summary>
		Public ReadOnly Property CustomCharacter As Asset(Of SpriteFile)
		''' <summary>
		''' Construct an in-game character.
		''' </summary>
		''' <param name="character">Character type.</param>
		Public Sub New(level As RDLevel, character As Characters)
			IsCustom = False
			Me.Character = character
		End Sub
		''' <summary>
		''' Construct a customized character.
		''' </summary>
		''' <param name="character">A sprite.</param>
		Public Sub New(level As RDLevel, character As String)
			IsCustom = True
			CustomCharacter = New Asset(Of SpriteFile)(level.Manager) With {.Name = character}
		End Sub
		Public Overrides Function ToString() As String
			Return If(IsCustom, CustomCharacter.Name, Character)
		End Function
	End Class
	Public Class AudioFile
		Implements IAssetFile
		Private ReadOnly _file As String
		Private _stream As WaveStream
		Public ReadOnly Property Name As String Implements IAssetFile.Name
		Public Sub New(name As String)
			_file = name
		End Sub
		Public Sub Load(directory As String) Implements IAssetFile.Load
			Dim path = IO.Path.Combine(directory, Name)
			If IO.Path.Exists(path) Then
				Select Case IO.Path.GetExtension(Name)
					Case ".ogg"
						_stream = New VorbisWaveReader(path)
					Case ".mp3"
						_stream = New Mp3FileReader(path)
					Case ".wav", ".wave"
						_stream = New WaveFileReader(path)
					Case ".aiff", ".aif"
						_stream = New AiffFileReader(path)
					Case Else
						_stream = Stream.Null
				End Select
			End If
		End Sub
		Public Overrides Function ToString() As String
			Return Name
		End Function
	End Class
	Public Class BuiltInAudio
		Implements IAssetFile
		Private _name As String
		Public ReadOnly Property Name As String Implements IAssetFile.Name
			Get
				Return _name
			End Get
		End Property
		Public Sub Load(directory As String) Implements IAssetFile.Load
			Return
		End Sub
		Public Sub New(name As String)
			_name = name
		End Sub
	End Class
	''' <summary>
	''' Audio.
	''' </summary>
	Public Class Audio
		<JsonIgnore> Public Property AudioFile As New Asset(Of AudioFile)
		''' <summary>
		''' File name.
		''' </summary>
		<JsonProperty("filename")> Public Property Name As String
			Get
				Return AudioFile?.Name
			End Get
			Set(value As String)
				AudioFile.Name = value
			End Set
		End Property
		''' <summary>
		''' Audio volume.
		''' </summary>
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> <DefaultValue(100)> Public Property Volume As Integer = 100
		''' <summary>
		''' Audio Pitch.
		''' </summary>
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> <DefaultValue(100)> Public Property Pitch As Integer = 100
		''' <summary>
		''' Audio Pan.
		''' </summary>
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> Public Property Pan As Integer = 0
		''' <summary>
		''' Audio Offset.
		''' </summary>
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> <JsonConverter(GetType(TimeConverter))> Public Property Offset As TimeSpan = TimeSpan.Zero
		<JsonIgnore> Public ReadOnly Property IsFile As Boolean
			Get
				IsFile = {".mp3", ".wav", ".ogg", ".aif", ".aiff"}.Contains(Path.GetExtension(Name))
			End Get
		End Property
		Public Overrides Function ToString() As String
			Return Name
		End Function
	End Class
	Public Enum LoopOption
		no
		yes
		onBeat
	End Enum
End Namespace
Namespace Extensions
	Public Module ImageExtension
		Public Function FromFile(path As String) As SKBitmap
			Using stream = New IO.FileInfo(path).OpenRead
				Return SKBitmap.Decode(stream)
			End Using
		End Function
		''' <summary>
		''' Save the image to a file path.
		''' </summary>
		''' <param name="image"></param>
		''' <param name="path"></param>
		<Extension> Public Sub Save(image As SKBitmap, path As String)
			Using stream = New IO.FileInfo(path).OpenWrite
				image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream)
			End Using
		End Sub
		Public Function TryDecode(ByRef image As SKBitmap, path As String) As Boolean
			Try
				image = SKBitmap.Decode(path)
			Catch ex As Exception
				Return False
			End Try
			Return True
		End Function
		<Extension> Public Function Copy(image As SKBitmap, rect As SKRectI) As SKBitmap
			Dim result As New SKBitmap(rect.Width, rect.Height)
			Dim canvas As New SKCanvas(result)
			canvas.DrawBitmap(image, rect, New SKRectI(0, 0, rect.Width, rect.Height))
			Return result
		End Function
#If DEBUG Then
		<Extension> Public Function OutLine(image As SKBitmap) As SKBitmap
			Dim img = image.Copy
			For x As Integer = 0 To img.Width - 1
				For y As Integer = 0 To img.Height - 1
					If image.GetPixel(x, y).Alpha = 0 AndAlso
(image.GetPixel(Math.Max(0, x - 1), y).Alpha = 255 OrElse
image.GetPixel(Math.Min(x + 1, img.Width - 1), y).Alpha = 255 OrElse
image.GetPixel(x, Math.Max(0, y - 1)).Alpha = 255 OrElse
						image.GetPixel(x, Math.Min(y + 1, img.Width - 1)).Alpha = 255) Then
						img.SetPixel(x, y, New SKColor(&HFF, &HFF, &HFF, &HFF))
					Else
						img.SetPixel(x, y, New SKColor)
					End If
				Next
			Next
			Return img
		End Function
		<Extension> Public Function OutGlow(image As SKBitmap) As SKBitmap
			'Dim output As SKBitmap
			'Using surface = SKSurface.Create(New SKImageInfo(image.Width, image.Height))
			'    Dim canvas = surface.Canvas
			'    canvas.Clear()
			'    Using blurFilter = SKImageFilter.CreateBlur(10, 10)
			'        Dim paint = New SKPaint With {.ImageFilter = blurFilter, .BlendMode = SKBlendMode.SrcOver}
			'        canvas.DrawBitmap(image, New SKPoint, paint)
			'        output = SKBitmap.FromImage(surface.Snapshot())
			'    End Using
			'End Using

			Dim shadowFilter = SKImageFilter.CreateDropShadow(0, 0, 10, 10, SKColors.White)
			Dim paint = New SKPaint With {.ImageFilter = shadowFilter}
			Using output As New SKBitmap(image.Width, image.Height)
				Using canvas As New SKCanvas(output)
					canvas.DrawBitmap(output, New SKPoint, paint)
				End Using
				Return output
			End Using
		End Function
#End If
	End Module
End Namespace