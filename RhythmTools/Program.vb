Imports System
Imports RhythmTools.Tools
Imports RhythmBase
Imports RhythmBase.Objects
Module Program
	Sub Main(args As String())
		Dim Testlevel As RDLevel = RDLevel.LoadFile("C:\Users\30698\OneDrive\ÎÄµµ\rdlevels\xnkl\level.rdlevel", New InputSettings.LevelInputSettings With {.SpriteSettings = New InputSettings.SpriteInputSettings With {.PlaceHolder = True}})
		Dim tools As New RDLevelHandler(Testlevel)
		Dim l = Testlevel.Where(Of CustomFlash)
		Dim r = Testlevel.Where(Of Tint)
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
		Testlevel.SaveFile("C:\Users\30698\OneDrive\ÎÄµµ\rdlevels\xnkl\levelEdited.rdlevel", New InputSettings.LevelInputSettings With {.SpriteSettings = New InputSettings.SpriteInputSettings With {.PlaceHolder = True}})
	End Sub
End Module