Imports System.IO
Imports System.Numerics
Imports Newtonsoft.Json
Imports SkiaSharp
Imports RhythmBase.Components
Imports Newtonsoft.Json.Linq
Imports System.Data
Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports RhythmBase.Exceptions
Imports RhythmBase.Extensions
Namespace Assets
	Public Class Sprite
		Private _File As String
		<JsonIgnore> Public Property FilePath As String
			Get
				Return _File
			End Get
			Friend Set(value As String)
				_File = value
			End Set
		End Property
		<JsonIgnore> Public ReadOnly Property Expressions As IEnumerable(Of String)
			Get
				Return Clips.Select(Function(i) i.Name)
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property Name As String
			Get
				If IsSprite Then
					Return IO.Path.GetFileNameWithoutExtension(_File)
				Else
					Return IO.Path.GetFileName(_File)
				End If
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property Preview As SKRectI?
			Get
				Return If(RowPreviewFrame Is Nothing, New SKRectI, GetFrame(RowPreviewFrame))
			End Get
		End Property
		<JsonIgnore> Public Property Frames As Frame
		<JsonIgnore> Public Property Freeze As New SKBitmap

		<JsonIgnore> Private _isSprite As Boolean
		Public Property Size As SKSizeI
		Public Property RowPreviewFrame As UInteger?
		Public Property RowPreviewOffset As SKSizeI
		Public Property Clips As New HashSet(Of Clip)

		Public Property IsSprite As Boolean
			Get
				Return _isSprite
			End Get
			Friend Set(value As Boolean)
				_isSprite = value
			End Set
		End Property
		Public Class Frame
			Public ReadOnly Property Size As SKSizeI
				Get
					Return Base.Info.Size
				End Get
			End Property
			Public Base As SKBitmap
			Public Glow As SKBitmap
			Public OutLine As SKBitmap
			Public Sub New(size As SKSizeI)
				Me.Base = New SKBitmap(size.Width, size.Height)
			End Sub
			Public Sub New(filePath As String)
				filePath = $"{ IO.Path.GetDirectoryName(filePath)}\{ IO.Path.GetFileNameWithoutExtension(filePath)}"
				Base = SKBitmap.Decode(filePath + ".png")
				Glow = SKBitmap.Decode(filePath + "_glow.png")
				OutLine = SKBitmap.Decode(filePath + "_outline.png")
			End Sub
		End Class
		Public Sub New()
		End Sub
		Public Shared Function LoadFile(filename As String) As Sprite
			If IO.Path.GetExtension(filename) = String.Empty Then
				Dim setting As New JsonSerializerSettings
				setting.Converters.Add(New Converters.SpriteConverter With {.FilePath = filename})
				Dim result = JsonConvert.DeserializeObject(Of Sprite)(IO.File.ReadAllText(filename + ".json"), setting)
				result.IsSprite = True
				Return result
			Else
				Dim imgFile = SKBitmap.Decode(filename)
				Return New Sprite With {.FilePath = filename, .Frames = New Frame(filename), .Size = imgFile.Info.Size, .RowPreviewFrame = 0, ._isSprite = False}
			End If
		End Function
		Private Function GetFrame(index As UInteger) As SKRectI
			Return GetFrameRect(index, Frames.Size, Size)
		End Function
		Private Function GetFrameRect(index As UInteger, source As SKSizeI, size As SKSizeI) As SKRectI
			Dim column As UInteger = (source.Width \ size.Width)
			Dim leftTop As New SKPointI(
				(index Mod column) * size.Width,
				(index \ column) * size.Height)
			Return New SKRectI(leftTop.X,
							   leftTop.Y,
							   leftTop.X + size.Width,
							   leftTop.Y + size.Height)
		End Function
		Public Class Clip
			Friend parent As Sprite
			Public Property Name As String
			Public Property [Loop] As LoopOption
			Public Property Fps As Integer
			Public Property LoopStart As Integer
			Public Property PortraitOffset As SKSizeI
			Public Property PortraitScale As Integer
			Public Property PortraitSize As SKSizeI
			Public Property Frames As List(Of UInteger)
			Public Function GetFrameRect() As SKRectI()
				Return Frames.Select(Function(i) parent.GetFrame(i)).ToArray
			End Function
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
			WriteJson(path, New Settings.SpriteOutputSettings)
		End Sub
		Public Sub WriteJson(path As FileInfo, settings As Settings.SpriteOutputSettings)

			Dim file = path
			Dim WithoutExtension As String = IO.Path.Combine(file.Directory.FullName, IO.Path.GetFileNameWithoutExtension(file.Name))
			If (
				IO.File.Exists(WithoutExtension + ".json") OrElse
				(settings.WithImage AndAlso IO.File.Exists(WithoutExtension + ".png"))
				) And Not settings.OverWrite Then
				Throw New OverwriteNotAllowedException($"Cannot save file '{path}' because overwriting is disabled by user configuration and a file with the same name already exists.")
			End If

			If settings.WithImage Then
				Frames.Base.Save(WithoutExtension + ".png")
				Frames.Glow?.Save(WithoutExtension + "_glow.png")
				Frames.OutLine?.Save(WithoutExtension + "_outline.png")
				Freeze?.Save(WithoutExtension + "_freeze.png")
			End If

			IO.File.WriteAllText(WithoutExtension + ".json", JsonConvert.SerializeObject(Me))

		End Sub
		Public Overrides Function ToString() As String
			Return $"{If(IsSprite, ".json", IO.Path.GetExtension(FilePath))}, {Name}"
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
					canvas.DrawBitmap(output, New RDPoint, paint)
				End Using
				Return output
			End Using
		End Function
		<Extension> Public Sub DrawFrame(e As SKCanvas, frame As Assets.Sprite.Frame, p As SKPoint, [event] As Events.Tint)
			Select Case [event].Border
				Case Events.Borders.Glow
					e.DrawBitmap(frame.Glow, p, New SKPaint With {.Color = [event].BorderColor.Value})
				Case Events.Borders.Outline
					e.DrawBitmap(frame.OutLine, p, New SKPaint With {.Color = [event].BorderColor.Value})
				Case Events.Borders.None
			End Select
			e.DrawBitmap(frame.Base, p)
		End Sub
	End Module
End Namespace