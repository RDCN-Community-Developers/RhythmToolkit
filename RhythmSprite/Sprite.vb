Imports System.Globalization
Imports System.IO
Imports System.Numerics
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RhythmBase.Assets
Imports RhythmBase.Exceptions
Imports RhythmBase.Settings
Imports RhythmBase.Utils
Imports SkiaSharp
Imports RhythmBase.Components
#Disable Warning CA1416
Namespace Converters
    Friend Class SpriteConverter
        Inherits JsonConverter(Of Sprite)
        Private tempImageList As HashSet(Of SKBitmap)
        Public Sub New(imageList As HashSet(Of SKBitmap))
            tempImageList = imageList
        End Sub
        Public Overrides Sub WriteJson(writer As JsonWriter, value As Sprite, serializer As JsonSerializer)
            Dim Setting As New JsonSerializerSettings With {
                .ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver,
                .Formatting = Formatting.Indented
            }
            With Setting.Converters
                .Add(New RDPointConverter)
                .Add(New ClipListConverter(tempImageList))
            End With
            Dim rowPreviewFrame As UInteger = value.Images.ToList.IndexOf(value.Preview)
            value.RowPreviewFrame = Nothing
            Dim Jobj = JObject.FromObject(value)
            If value.ShouldSerializeRowPreviewFrame Then
                Jobj("rowPreviewFrame") = rowPreviewFrame
            End If
            writer.WriteRawValue(Jobj)
        End Sub
        Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Sprite, hasExistingValue As Boolean, serializer As JsonSerializer) As Sprite
            Dim Setting As New JsonSerializer With {
                    .ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
            }
            With Setting.Converters
                .Add(New RDPointConverter)
                .Add(New ClipListConverter(tempImageList))
            End With
            Dim JObj = JObject.Load(reader).ToObject(Of JObject)
            Dim index As UInteger?
            index = JObj("rowPreviewFrame")?.ToObject(Of UInteger?)
            JObj.Remove("rowPreviewFrame")
            Dim result = JObj.ToObject(Of Sprite)(Setting)
            result._size = JObj("size").ToObject(Of RDPoint)(Setting)
            result.RowPreviewFrame = If(index, result.Images.ToList(index), Nothing)
            Return result
        End Function
    End Class
    Friend Class RDPointConverter
        Inherits JsonConverter(Of RDPoint)
        Public Overrides Sub WriteJson(writer As JsonWriter, value As RDPoint, serializer As JsonSerializer)
            writer.WriteRawValue($"[{value.Width},{value.Height}]")
        End Sub

        Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDPoint, hasExistingValue As Boolean, serializer As JsonSerializer) As RDPoint
            Dim J = JArray.Load(reader)
            Return New RDPoint(J(0), J(1))
        End Function
    End Class
    Class ClipListConverter
        Inherits JsonConverter(Of HashSet(Of Sprite.Clip))
        Private Shared Function CamelCase(s As String) As String
            Return String.Concat(s(0).ToString.ToLower, s.AsSpan(1))
        End Function
        Private tempImageList As List(Of SKBitmap)
        Public Sub New(imageList As HashSet(Of SKBitmap))
            tempImageList = imageList.ToList
        End Sub
        Public Overrides Sub WriteJson(writer As JsonWriter, value As HashSet(Of Sprite.Clip), serializer As JsonSerializer)
            Dim Setting As New JsonSerializerSettings With {
                    .Formatting = Formatting.None,
                    .ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
                }
            With Setting.Converters
                .Add(New RDPointConverter)
                .Add(New Newtonsoft.Json.Converters.StringEnumConverter)
            End With

            If value.Count <> 0 Then
                Dim Properties() As PropertyInfo = GetType(Sprite.Clip).GetProperties
                Dim A As Dictionary(Of PropertyInfo, UShort) = GetType(Sprite.Clip).GetProperties.ToDictionary(Of PropertyInfo, UShort)(Function(i) i, Function(i) value.Max(Function(j) JsonConvert.SerializeObject(i.GetValue(j), Setting).ToString.Length))

                With writer
                    .WriteStartArray()
                    For Each item In value
                        .WriteStartObject()
                        writer.Formatting = Formatting.None
                        For Each p In Properties
                            .WritePropertyName(CamelCase(p.Name))
                            Dim M As Object
                            Dim WriteSpace As Boolean = True
                            If p.Name = "Frames" Then
                                M = CType(p.GetValue(item), List(Of SKBitmap)).Select(Of UInteger)(Function(i) tempImageList.IndexOf(i))
                                WriteSpace = False
                            Else
                                M = p.GetValue(item)
                            End If
                            .WriteRawValue(JsonConvert.SerializeObject(M, Setting))
                            Dim obj As Object = If(p.PropertyType.IsValueType, Activator.CreateInstance(p.PropertyType), Nothing)
                            If WriteSpace Then
                                For i = 1 To A(p) - JsonConvert.SerializeObject(p.GetValue(item), Setting).ToString.Length
                                    .WriteWhitespace(" ")
                                Next
                            End If
                        Next
                        .WriteEndObject()
                        writer.Formatting = Formatting.Indented
                    Next
                    .WriteEndArray()
                End With

            End If
        End Sub
        Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As HashSet(Of Sprite.Clip), hasExistingValue As Boolean, serializer As JsonSerializer) As HashSet(Of Sprite.Clip)
            Dim Setting As New JsonSerializer With {
                    .ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
                }
            With Setting.Converters
                .Add(New RDPointConverter)
                .Add(New Newtonsoft.Json.Converters.StringEnumConverter)
            End With
            Dim Arr As JArray = JToken.ReadFrom(reader)
            Dim Output As New HashSet(Of Sprite.Clip)
            For Each item As Object In Arr
                Dim L As List(Of UInteger) = CType(item("frames"), JArray).ToObject(Of List(Of UInteger))
                Dim ClipImageList As New List(Of SKBitmap)
                For Each I In L
                    If I < tempImageList.Count Then
                        ClipImageList.Add(tempImageList(I))
                    Else
                        ClipImageList.Add(Nothing)
                    End If
                Next
                item("frames") = Nothing
                Dim C As Sprite.Clip = CType(item, JToken).ToObject(Of Sprite.Clip)(Setting)
                C.Frames = ClipImageList
                Output.Add(C)
            Next
            Return Output
        End Function
    End Class
End Namespace
Namespace Exceptions
    Class SpriteException
        Inherits Exception
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub
    End Class
    Class FileExtensionMismatchException
        Inherits SpriteException
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub
    End Class
    Class OverwriteNotAllowedException
        Inherits SpriteException
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub
    End Class
End Namespace
Namespace Utils
    Public Enum LoopOption
        no
        yes
        onBeat
    End Enum
    Public Enum ImageInputOption
        HORIZONTAL
        VERTICAL
    End Enum
    Public Module ImageUtils
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
                    canvas.DrawBitmap(output, New RDPoint, paint)
                End Using
                Return output
            End Using
        End Function
    End Module
End Namespace
Namespace Extensions
    Public Module AssetExtension
        '<Extension>
        'Public Function Read(p As Quote) As ISprite
        '    Dim result As ISprite
        '    If Sprite.CanRead(p.FilePath) Then
        '        result = Sprite.FromPath(p.FilePath)
        '    ElseIf Image.CanRead(p.FilePath) Then
        '        result = Image.FromPath(p.FilePath)
        '    Else
        '        result = Nothing
        '    End If
        '    Return result
        'End Function
    End Module
End Namespace
Namespace Assets
    Public Class Sprite
        Implements ISprite
        Dim _File As String
        <JsonIgnore>
        Public ReadOnly Property FilePath As String Implements ISprite.FilePath
            Get
                Return _File
            End Get
        End Property
        <JsonIgnore>
        Public ReadOnly Property Expressions As IEnumerable(Of String) Implements ISprite.Expressions
            Get
                Return Clips.Select(Function(i) i.Name)
            End Get
        End Property
        <JsonIgnore>
        Public ReadOnly Property Name As String Implements ISprite.Name
            Get
                Return IO.Path.GetFileNameWithoutExtension(_File)
            End Get
        End Property
        <JsonIgnore>
        Public ReadOnly Property Preview As SKBitmap Implements ISprite.Preview
            Get
                Return If(RowPreviewFrame, New SKBitmap)
            End Get
        End Property
        <JsonIgnore>
        Public Property Images As New HashSet(Of SKBitmap)
        <JsonIgnore>
        Public Property Images_Freeze As New SKBitmap
        <JsonIgnore>
        Public Property Images_Glow As HashSet(Of SKBitmap)
        <JsonIgnore>
        Public Property Images_Outline As New HashSet(Of SKBitmap)
        <JsonProperty("size")>
        Friend Property _size As RDPoint
        Public ReadOnly Property Size As RDPoint Implements ISprite.Size
            Get
                Return _size
            End Get
        End Property
        Public Property RowPreviewFrame As SKBitmap
        Public Property RowPreviewOffset As RDPoint
        Public Property Clips As New HashSet(Of Clip)
        Private Shared ReadOnly stringArray As String() = {"neutral", "happy", "barely", "missed"}
        Public Class Clip
            Public Property Name As String
            Public Property [Loop] As LoopOption
            Public Property Fps As Integer
            Public Property LoopStart As Integer
            Public Property PortraitOffset As RDPoint
            Public Property PortraitScale As Integer
            Public Property PortraitSize As RDPoint
            Public Property Frames As New List(Of SKBitmap)
            Public Overrides Function ToString() As String
                Return Name
            End Function
        End Class
        Friend Function ShouldSerializeRowPreviewFrame() As Boolean
            Return RowPreviewFrame IsNot Nothing
        End Function
        Friend Function ShouldSerializeRowPreviewOffset() As Boolean
            Return RowPreviewFrame IsNot Nothing
        End Function
        Public Shared Function CanRead(path As FileInfo) As Boolean
            Dim file = path
            Dim WithoutExtension As String = IO.Path.Combine(file.Directory.FullName, IO.Path.GetFileNameWithoutExtension(file.Name))
            Dim fileExtension = file.Extension
            Dim jsonFile As New IO.FileInfo(WithoutExtension + ".json")
            If Not jsonFile.Exists Then
                Return False
            End If
            Dim imageFile As New IO.FileInfo(WithoutExtension + ".png")
            If Not imageFile.Exists Then
                Return False
            End If
            Return True
        End Function
        Public Shared Function FromPath(path As FileInfo) As Sprite
            Dim file = path
            Dim WithoutExtension As String = IO.Path.Combine(file.Directory.FullName, IO.Path.GetFileNameWithoutExtension(file.Name))
            Dim fileExtension = file.Extension
            Dim jsonFile = WithoutExtension + ".json"
            If Not IO.File.Exists(jsonFile) Then
                Throw New Exceptions.FileExtensionMismatchException($"Imported file should have a '.json' extension but found '{file.Extension}'.")
            End If
            Dim imageFile As New IO.FileInfo(WithoutExtension + ".png")
            If Not imageFile.Exists Then
                Throw New IO.FileNotFoundException($"Could not find file at path '{imageFile.FullName}'. Please check whether the file exists and that you have adequate permissions to access it.")
            End If
            Dim TempImages As New HashSet(Of SKBitmap)
            Dim Setting As New JsonSerializerSettings
            With Setting.Converters
                .Add(New Converters.SpriteConverter(TempImages))
            End With
            Dim ReadedSize As JToken = JsonConvert.DeserializeObject(Of JObject)(IO.File.ReadAllText(jsonFile))("size")
            Using img As SKBitmap = LoadImage(WithoutExtension + ".png")
                Dim size As New RDPoint(ReadedSize(0), ReadedSize(1))
                TempImages = SplitImage(img, size)
            End Using
            Dim Temp = JsonConvert.DeserializeObject(Of Sprite)(IO.File.ReadAllText(jsonFile), Setting)
            Temp._File = jsonFile
            Temp.Images = TempImages
            Return Temp
        End Function
        Private Shared Function SplitImage(img As SKBitmap, size As RDPoint, Optional inputMode As ImageInputOption = ImageInputOption.HORIZONTAL) As HashSet(Of SKBitmap)
            Dim Transparent As New SKBitmap(CInt(size.Width), CInt(size.Height))
            Dim L As New HashSet(Of SKBitmap)
            Dim countSize As New RDPoint(img.Width \ size.Width, img.Height \ size.Height)
            Select Case inputMode
                Case ImageInputOption.HORIZONTAL
                    For i = 0 To countSize.Width * countSize.Height - 1
                        Dim Area = New SKRect((i Mod countSize.Width) * size.Width, (i \ countSize.Width) * size.Height, size.Width, size.Height)
                        Dim tmpimg As New SKBitmap(size.Width, size.Height)
                        Using g As New SKCanvas(tmpimg)
                            g.DrawBitmap(img, Area)
                        End Using
                        If TotallyTransparentImage(tmpimg) Then
                            L.Add(Transparent)
                        Else
                            L.Add(tmpimg)
                        End If
                    Next
                Case ImageInputOption.VERTICAL
                    For i = 0 To countSize.Width * countSize.Height - 1
                        Dim Area = New SKRect((i \ countSize.Height) * size.Width, (i Mod countSize.Height) * size.Height, size.Width, size.Height)
                        Dim tmpimg As New SKBitmap(size.Width, size.Height)
                        Using g As New SKCanvas(tmpimg)
                            g.DrawBitmap(img, Area)
                        End Using
                        If TotallyTransparentImage(tmpimg) Then
                            L.Add(Transparent)
                        Else
                            L.Add(tmpimg)
                        End If
                    Next
            End Select
            Return L
        End Function
        Private Shared Function TotallyTransparentImage(img As SKBitmap) As Boolean
            For x = 0 To img.Width - 1
                For y = 0 To img.Height - 1
                    If img.GetPixel(x, y).Alpha > 0 Then
                        Return False
                    End If
                Next
            Next
            Return True
        End Function
        Public Shared Function FromImage(img As SKBitmap, size As RDPoint, Optional inputMode As ImageInputOption = ImageInputOption.HORIZONTAL) As Sprite
            Dim Output As New Sprite With {
            ._size = size,
            .Images = SplitImage(img, size, inputMode)
        }
            Return Output
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
        Public Sub WriteJson(path As FileInfo)
            WriteJson(path, New SpriteOutputSettings)
        End Sub
        Public Sub WriteJson(path As FileInfo, settings As SpriteOutputSettings)

            Dim file = path
            Dim WithoutExtension As String = IO.Path.Combine(file.Directory.FullName, IO.Path.GetFileNameWithoutExtension(file.Name))
            If (
            IO.File.Exists(WithoutExtension + ".json") OrElse
            (settings.WithImage AndAlso IO.File.Exists(WithoutExtension + ".png"))
            ) And Not settings.OverWrite Then
                Throw New OverwriteNotAllowedException($"Cannot save file '{path}' because overwriting is disabled by user configuration and a file with the same name already exists.")
            End If

            If settings.Sort Then
                Dim SortedClips = Clips.OrderBy(Function(i) i.Name, StringComparer.Create(CultureInfo.CurrentCulture, True)).ToList
                For Each expressionName In stringArray.Reverse
                    Dim expression = SortedClips.FirstOrDefault(Function(i) i.Name = expressionName)
                    If expression IsNot Nothing Then
                        SortedClips.Remove(expression)
                        SortedClips.Insert(0, expression)
                    End If
                Next
                Dim TempList As New List(Of SKBitmap)
                For Each item In SortedClips
                    TempList.AddRange(item.Frames)
                Next
                TempList.AddRange(Images)
                Images = TempList.Distinct()
            End If

            If settings.WithImage Then
                Dim MaxCountSize As New RDPoint(Math.Min(If(settings.LimitedCount, New RDPoint(16384, 0)).X, settings.LimitedSize.X \ Size.Width),
                                            Math.Min(If(settings.LimitedCount, New RDPoint(0, 16384)).Y, settings.LimitedSize.Y \ Size.Height))
                Select Case settings.OutputMode
                    Case SpriteOutputSettings.OutputModes.HORIZONTAL
                        Dim png As New SKBitmap(CInt(Math.Min(Images.Count, MaxCountSize.X) * Size.Width),
                                          CInt(Math.Ceiling(Images.Count / MaxCountSize.X) * Size.Height))
                        Dim g As New SKCanvas(png)
                        For i = 0 To Images.Count - 1
                            Dim index = Images
                            g.DrawBitmap(Images(i), (i Mod MaxCountSize.X) * Size.Width, (i \ MaxCountSize.X) * Size.Height)
                        Next
                        png.Save(WithoutExtension + ".png")
                    Case SpriteOutputSettings.OutputModes.VERTICAL
                        Dim png As New SKBitmap(CInt(Math.Ceiling(Images.Count / MaxCountSize.Y) * Size.Width),
                                          CInt(Math.Min(Images.Count, MaxCountSize.Y) * Size.Height))
                        Dim g As New SKCanvas(png)
                        For i = 0 To Images.Count - 1
                            Dim index = Images
                            g.DrawBitmap(Images(i), (i \ MaxCountSize.Y) * Size.Width, (i Mod MaxCountSize.Y) * Size.Height)
                        Next
                        png.Save(WithoutExtension + ".png")
                    Case SpriteOutputSettings.OutputModes.PACKED
                        Dim BestCountSize As New RDPoint
                        Do
                            If BestCountSize.X * Size.Width < BestCountSize.Y * Size.Height Then
                                BestCountSize.X += 1
                            ElseIf BestCountSize.X * Size.Width > BestCountSize.Y * Size.Height Then
                                BestCountSize.Y += 1
                            Else
                                BestCountSize.X += 1
                                BestCountSize.Y += 1
                            End If
                        Loop Until BestCountsize.x * BestCountSize.Y >= Images.Count - 1 Or BestCountsize.x = MaxCountsize.x Or BestCountSize.Y = MaxCountSize.Y
                        Dim png As New SKBitmap(CInt(Math.Min(Images.Count, BestCountSize.X) * Size.Width),
                                          CInt(Math.Ceiling(Images.Count / BestCountSize.X) * Size.Height))
                        Dim g As New SKCanvas(png)
                        For i = 0 To Images.Count - 1
                            Dim index = Images
                            g.DrawBitmap(Images(i), (i Mod BestCountSize.X) * Size.Width, (i \ BestCountSize.X) * Size.Height)
                        Next
                        png.Save(WithoutExtension + ".png")
                End Select
            End If

            Dim JSetting As New JsonSerializerSettings
            With JSetting.Converters
                .Add(New Converters.SpriteConverter(Me.Images))
            End With
            IO.File.WriteAllText(WithoutExtension + ".json", JsonConvert.SerializeObject(Me, JSetting))

        End Sub
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    Public Class Image
        Implements ISprite
        Private _File As String
        Public Image As SKBitmap
        Public ReadOnly Property FilePath As String Implements ISprite.FilePath
            Get
                Return _File
            End Get
        End Property
        Public ReadOnly Property Name As String Implements ISprite.Name
            Get
                Return New IO.FileInfo(_File).Name
            End Get
        End Property
        <JsonIgnore>
        Public ReadOnly Property Expressions As IEnumerable(Of String) Implements ISprite.Expressions
            Get
                Return New List(Of String)
            End Get
        End Property
        Public ReadOnly Property Size As RDPoint Implements ISprite.Size
            Get
                Return New RDPoint(Image.Width, Image.Height)
            End Get
        End Property
        <JsonIgnore>
        Public ReadOnly Property Preview As SKBitmap Implements ISprite.Preview
            Get
                Return Image
            End Get
        End Property
        Public Shared Function CanRead(path As FileInfo) As Boolean
            Dim file = path
            If Not (file.Extension = ".png" OrElse
            file.Extension = ".jpg" OrElse
            file.Extension = ".jpeg" OrElse
            file.Extension = ".gif") Then
                Return False
            End If
            Return True
        End Function
        Public Shared Function FromPath(path As String) As Image
            Return New Image With {._File = path, .Image = LoadImage(path)}
        End Function
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace