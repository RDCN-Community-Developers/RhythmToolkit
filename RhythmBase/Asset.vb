Imports System.Data
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json
Imports SkiaSharp
Imports RhythmBase.Extensions.Extension
Namespace Assets
	Public Class RDSprite
		Private _file As String
		<JsonIgnore> Public Property FilePath As String
			Get
				Return _file
			End Get
			Friend Set(value As String)
				_file = value
			End Set
		End Property
		<JsonIgnore> Public ReadOnly Property Expressions As IEnumerable(Of String)
			Get
				Return Clips.Select(Function(i) i.Name)
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property FileName As String
			Get
				If IsSprite Then
					Return IO.Path.GetFileNameWithoutExtension(_file)
				Else
					Return IO.Path.GetFileName(_file)
				End If
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property Preview As SKRect?
			Get
				Return If(RowPreviewFrame Is Nothing, New SKRect, GetFrame(RowPreviewFrame))
			End Get
		End Property
		<JsonIgnore> Public Property ImageSize As RDSizeNI
		<JsonIgnore> Public Property Image_Base As SKBitmap
		<JsonIgnore> Public Property Image_Glow As SKBitmap
		<JsonIgnore> Public Property Image_Outline As SKBitmap
		<JsonIgnore> Public Property Image_Freeze As SKBitmap
		Public Property Name As String
#If DEBUG Then
		Public Property Voice As String
#End If
		Public Property Size As RDSizeNI
		Public Property Clips As New HashSet(Of Clip)
		Public Property RowPreviewOffset As RDSizeN?
		Public Property RowPreviewFrame As UInteger?
		Public Property PivotOffset As RDPointN?
		Public Property PortraitOffset As RDSizeN?
		Public Property PortraitSize As RDSizeN?
		Public Property PortraitScale As Single?
		<JsonIgnore> Public ReadOnly Property IsSprite As Boolean
			Get
				Return Path.GetExtension(_file) = String.Empty
			End Get
		End Property
		Public Class Clip
			Friend parent As RDSprite
			Public Property Name As String
			Public Property Frames As List(Of UInteger) 'nullable
			Public Property LoopStart As Integer? 'nullable
			<JsonConverter(GetType(Newtonsoft.Json.Converters.StringEnumConverter))>
			Public Property [Loop] As LoopOption
			Public Property Fps As Single
			Public Property PivotOffset As RDPointN? 'nullable
			Public Property PortraitOffset As RDSizeN? 'nullable
			Public Property PortraitScale As Single? 'nullable
			Public Property PortraitSize As RDSizeN? 'nullable
			Public Function GetFrameRects() As SKRectI()
				Return Frames.Select(Function(i) parent.GetFrame(i)).ToArray
			End Function
			Public Overrides Function ToString() As String
				Return Name
			End Function
		End Class
		Public Sub New()
		End Sub
		Public Sub New(filename As String)
			If filename.IsNullOrEmpty Then
				Throw New ArgumentException("Filename cannot be null.", NameOf(filename))
			End If
			_file = filename
		End Sub
		Public Sub Load()
			If IsSprite Then

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

				obj = setting.Deserialize(New JsonTextReader(File.OpenText($"{json}.json")))

				Dim imageBaseFile = $"{json}.png"
				Dim imageGlowFile = $"{json}_glow.png"
				Dim imageOutlineFile = $"{json}_outline.png"
				Dim imageFreezeFile = $"{json}_freeze.png"

				If File.Exists(imageBaseFile) Then
					Image_Base = SKBitmap.Decode(imageBaseFile)
				Else
					Throw New FileNotFoundException($"Cannot find the image file", _file + ".png")
				End If
				If File.Exists(imageGlowFile) Then
					Image_Glow = SKBitmap.Decode(imageGlowFile)
				End If
				If File.Exists(imageOutlineFile) Then
					Image_Outline = SKBitmap.Decode(imageOutlineFile)
				End If
				If File.Exists(imageFreezeFile) Then
					Image_Freeze = SKBitmap.Decode(imageFreezeFile)
				End If
				Name = obj(NameOf(Name).ToLowerCamelCase)?.ToObject(Of String)
#If DEBUG Then
				Voice = obj(NameOf(Voice).ToLowerCamelCase)?.ToObject(Of String)
#End If
				Size = obj(NameOf(Size).ToLowerCamelCase).ToObject(Of RDSizeNI)
				RowPreviewOffset = obj(NameOf(RowPreviewOffset).ToLowerCamelCase)?.ToObject(Of RDSizeN)
				RowPreviewFrame = obj(NameOf(RowPreviewFrame).ToLowerCamelCase)?.ToObject(Of UInteger)
				PivotOffset = obj(NameOf(PivotOffset).ToLowerCamelCase)?.ToObject(Of RDPointN)
				PortraitOffset = obj(NameOf(PortraitOffset).ToLowerCamelCase)?.ToObject(Of RDSizeN)
				PortraitSize = obj(NameOf(PortraitSize).ToLowerCamelCase)?.ToObject(Of RDSizeN)
				PortraitScale = obj(NameOf(PortraitScale).ToLowerCamelCase)?.ToObject(Of Single)
				For Each clip In obj(NameOf(Clips).ToLowerCamelCase)
					Clips.Add(clip.ToObject(Of Clip))
				Next
			Else
				If File.Exists(_file) Then
					Dim imgFile As SKBitmap
					Try
						imgFile = SKBitmap.Decode(_file)
						Image_Base = imgFile
					Catch ex As Exception
					End Try
				Else
					Throw New FileNotFoundException($"Cannot find the image file", _file)
				End If
			End If
		End Sub
		Public Sub WriteJson(textWriter As TextWriter)
			WriteJson(textWriter, New SpriteOutputSettings)
		End Sub
		Public Sub WriteJson(textWriter As TextWriter, setting As SpriteOutputSettings)
			Dim jsonS As New JsonSerializerSettings With {
				.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver,
				.NullValueHandling = NullValueHandling.Ignore,
				.Formatting = Formatting.None
				}
			Dim writer = New JsonTextWriter(textWriter) With {.Formatting = If(setting.Indented, Formatting.Indented, Formatting.None)}
			Dim meObj = Linq.JObject.FromObject(Me)
			meObj.ToLowerCamelCase
			Dim clipArray As Linq.JArray = meObj(NameOf(Clips).ToLowerCamelCase)

			Dim PropertyNameLength As New Dictionary(Of String, Integer)

			Dim propertyValues As New Dictionary(Of String, List(Of String))

			For Each clip As Linq.JObject In clipArray
				clip.ToLowerCamelCase
				For Each pair In clip
					Dim stringedValue = If(pair.Value.Type = Linq.JTokenType.Null, String.Empty, JsonConvert.SerializeObject(pair.Value, Formatting.None, jsonS))
					If propertyValues.ContainsKey(pair.Key) Then
						propertyValues(pair.Key).Add(stringedValue)
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
		Private Function GetFrame(index As UInteger) As SKRectI
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
		Friend Function ShouldSerializeRowPreviewFrame() As Boolean
			Return RowPreviewFrame IsNot Nothing
		End Function
		Friend Function ShouldSerializeRowPreviewOffset() As Boolean
			Return RowPreviewFrame IsNot Nothing
		End Function
		Public Function AddBlankClip(name As String) As Clip
			Dim C = Clips.FirstOrDefault(New Clip With {.Name = name})
			Clips.Add(C)
			Return C
		End Function
		Public Function AddBlankClipsForCharacter() As IEnumerable(Of Clip)
			Return From n In {"neutral", "happy", "barely", "missed"}
				   Select AddBlankClip(n)
		End Function
		Public Function AddBlankClipsForDecoration() As IEnumerable(Of Clip)
			Return From n In {"neutral"}
				   Select AddBlankClip(n)
		End Function
		Public Sub Save(path As String)
			Save(path, New SpriteOutputSettings)
		End Sub
		Public Sub Save(path As String, settings As SpriteOutputSettings)

			Dim file = New FileInfo(path)
			Dim WithoutExtension As String = IO.Path.Combine(file.Directory.FullName, IO.Path.GetFileNameWithoutExtension(file.Name))
			If (IO.File.Exists(WithoutExtension + ".json") OrElse
				(settings.WithImage AndAlso IO.File.Exists(WithoutExtension + ".png"))
				) And Not settings.OverWrite Then
				Throw New OverwriteNotAllowedException(path, GetType(LevelOutputSettings))
			End If

			If settings.WithImage Then
				Image_Base.Save(WithoutExtension + ".png")
				Image_Glow?.Save(WithoutExtension + "_glow.png")
				Image_Outline?.Save(WithoutExtension + "_outline.png")
				Image_Freeze?.Save(WithoutExtension + "_freeze.png")
			End If

			WriteJson(New FileInfo(WithoutExtension + ".json").CreateText, settings)

		End Sub
		Public Overrides Function ToString() As String
			Return $"{ If(IsSprite, ".json", Path.GetExtension(FilePath))}, {If(Name.IsNullOrEmpty, FileName, Name)}"
		End Function
	End Class
	Public Structure RDCharacter
		Public ReadOnly IsCustom As Boolean
		Public ReadOnly Character? As Characters
		Public ReadOnly CustomCharacter As RDSprite
		Public Sub New(character As Characters)
			IsCustom = False
			Me.Character = character
		End Sub
		Public Sub New(character As RDSprite)
			IsCustom = True
			CustomCharacter = character
		End Sub
		Public Overrides Function ToString() As String
			Return If(IsCustom, CustomCharacter.FileName, Character)
		End Function
	End Structure
	Public Class RDAudio
		Private ReadOnly _file As String
		Public ReadOnly Property FilePath As String
			Get
				Return _file
			End Get
		End Property
		Public ReadOnly Property IsFile As Boolean
		Public Sub New(name As String)
			_file = name
			IsFile = {".mp3", ".wav", ".ogg", ".aif", ".aiff"}.Contains(IO.Path.GetExtension(name))
		End Sub
	End Class
	Public Enum LoopOption
		no
		yes
		onBeat
	End Enum
End Namespace
Namespace Extensions
	Public Module ImageExtension
		Public Function LoadImage(path As String) As SKBitmap
			Using stream = New IO.FileInfo(path).OpenRead
				Return SKBitmap.Decode(stream)
			End Using
		End Function
		<Extension>
		Public Sub Save(image As SKBitmap, path As String)
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
		<Extension>
		Public Function Copy(image As SKBitmap, rect As SKRectI) As SKBitmap
			Dim result As New SKBitmap(rect.Width, rect.Height)
			Dim canvas As New SKCanvas(result)
			canvas.DrawBitmap(image, rect, New SKRectI(0, 0, rect.Width, rect.Height))
			Return result
		End Function
		<Extension>
		Public Function OutLine(image As SKBitmap) As SKBitmap
			Dim img = image.Copy
			For x = 0 To img.Width - 1
				For y = 0 To img.Height - 1
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
		<Extension>
		Public Function OutGlow(image As SKBitmap) As SKBitmap
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
		'<Extension> Public Sub DrawFrame(e As SKCanvas, frame As Assets.RDSprite.Frame, p As SKPoint, [event] As Events.Tint)
		'    Select Case [event].Border
		'        Case Events.Borders.Glow
		'            e.DrawBitmap(frame.Glow, p, New SKPaint With {.Color = [event].BorderColor.Value})
		'        Case Events.Borders.Outline
		'            e.DrawBitmap(frame.OutLine, p, New SKPaint With {.Color = [event].BorderColor.Value})
		'        Case Events.Borders.None
		'    End Select
		'    e.DrawBitmap(frame.Base, p)
		'End Sub
	End Module
End Namespace