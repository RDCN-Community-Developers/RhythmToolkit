Imports RhythmAsset.Converters
Imports RhythmAsset.Sprites
#Disable Warning ca1416
Module Test
	Sub Main(args As String())

	End Sub
	Sub SubMain2(args As String())
		Dim Sets As New SpriteOutputSettings With {
			.OutputMode = SpriteOutputSettings.OutputModes.PACKED,
			.Sort = True,
			.OverWrite = True,
			.WithImage = True
		}
		Dim Names As String() = {"Yindoubai", "Qiulufan", "Yueyue", "DownWhite"}
		Dim Actions As String() = {"Default", "SingingSeparately", "Singing", "MicrophoneStand", "MicrophoneOnly", "Microphone", ""}
		Dim A = Sprite.FromImage(LoadImage(New IO.FileInfo("C:\Users\30698\OneDrive\ÎÄµµ\rdlevels\Characters\Yindoubai.png")), New Numerics.Vector2(32, 56), ImageInputOption.VERTICAL)
		For i = 0 To A.Images.Count - 1 Step 8
			If (i / 8 \ 4) >= 4 Then
				Continue For
			End If
			A.Clips.Add(New Sprite.Clip With {
				.Name = $"{Names(i / 8 Mod 4).PadRight(Names.Max(Function(j) j.Length), "_")}_{Actions(i / 8 \ 4)}",
				.Fps = 0,
				.Frames = {
					A.Images(i + 1),
					A.Images(i + 2),
					A.Images(i + 3),
					A.Images(i + 4),
					A.Images(i + 4),
					A.Images(i + 4),
					A.Images(i + 4),
					A.Images(i + 5),
					A.Images(i + 6),
					A.Images(i + 7),
					A.Images(i),
					A.Images(i),
					A.Images(i),
					A.Images(i)
				}.ToList,
				.[Loop] = LoopOption.onBeat,
				.LoopStart = 0,
				.PortraitOffset = New Numerics.Vector2(0, 0),
				.PortraitScale = 2,
				.PortraitSize = New Numerics.Vector2(2, 2)
			})
		Next
		A.AddBlankClip("neutral")
		A.AddBlankClip("happy")
		A.AddBlankClip("barely")
		A.AddBlankClip("missed")
		A.WriteJson(New IO.FileInfo("C:\Users\30698\OneDrive\ÎÄµµ\rdlevels\Characters\Output.png"), Sets)
	End Sub
	Sub SubMain1(args As String())
		Dim Setting As New Newtonsoft.Json.JsonSerializer
		With Setting.Converters
			.Add(New Vector2Converter)
			.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
		End With
		Console.WriteLine($"File: {args(0)}")
		Dim GlobalSettings As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(IO.File.ReadAllText(args(0)))
		Dim sets As SpriteOutputSettings = GlobalSettings("settings").ToObject(Of SpriteOutputSettings)(Setting)
		Dim A = Sprite.FromPath(New IO.FileInfo(GlobalSettings("readFile")))
		If GlobalSettings("write") Then
			A.WriteJson(New IO.FileInfo(GlobalSettings("writeFile")))
		End If
		Console.WriteLine("Done.")

	End Sub
End Module