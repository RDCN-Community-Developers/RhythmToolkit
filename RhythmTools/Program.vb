Imports RhythmBase
Imports RhythmBase.Objects
Imports RhythmAsset
Imports RhythmBase.Util
Imports RhythmAsset.Sprites
Imports RhythmBase.Events
Module Program
	Sub Main(args As String())
		Dim Testlevel As RDLevel = RDLevel.LoadFile(New IO.FileInfo("E:\Download\Aiobahn feat. KOTOKO - INTERNET YAMERO\main.rdlevel"), New InputSettings.LevelInputSettings With {.SpriteSettings = New SpriteInputSettings With {.PlaceHolder = True}})
		Dim tools As New RDLevelHandler(Testlevel)
		Dim tool1 As New BeatCalculator(Testlevel)
		Dim l = Testlevel.Where(Of CustomFlash)
		Dim r = Testlevel.Where(Of Tint)
		Dim s = Testlevel.Where(Of SetBackgroundColor)
		'tools.MoveBeats(4, 4)
		'Testlevel.RefreshCPBs()
		For Each item In tools.GetLevelMinIntervalTime
			Console.WriteLine(item.Item1.Parent.ToString + " >> " + item.Item2.Parent.ToString)
			Console.WriteLine(tool1.BeatOnly_BarBeat(item.Item1.BeatOnly).ToString + " >> " + tool1.BeatOnly_BarBeat(item.Item2.BeatOnly).ToString)
			Console.WriteLine(item.Item3)
			Console.WriteLine()
		Next
		'tools.CombineToTag("123", FilterCodeCSharp("return Y++==1;"), True)
		'	tools.AddTimer(New FloatingText With {.})
		'tools.SplitRow(False)
		'Dim lvl As New RDLevel
		'Dim Samurai As New Row With {
		'	.Character = "Samurai",
		'	.RowType = RowType.Classic,
		'	.PulseSound = "Kick",
		'	.PulseSoundVolume = 100,
		'	.PulseSoundPitch = 100£¬
		'	.ParentCollection = lvl.Rows
		'}
		'Samurai.Rooms(0) = True
		''For Each item In args.Beats
		'Dim s = Samurai.CreateChildren(Of AddFreeTimeBeat)(2)
		'lvl.Add(s)
		''Next
		'lvl.Rows.Add(Samurai)
		'Testlevel.SaveFile("C:\Users\30698\OneDrive\ÎÄµµ\rdlevels\xnkl\levelEdited.rdlevel", New InputSettings.LevelInputSettings With {.SpriteSettings = New InputSettings.SpriteInputSettings With {.PlaceHolder = True}})
	End Sub
End Module