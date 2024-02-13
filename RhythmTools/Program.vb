Imports System
Imports RhythmTools.Tools
Imports RhythmBase
Imports RhythmBase.Objects
Module Program
	Sub Main(args As String())
		Dim Testlevel As Objects.RDLevel = RDLevel.LoadFile("C:\Users\30698\OneDrive\ÎÄµµ\rdlevels\xnkl\level.rdlevel", New InputSettings.LevelInputSettings With {.SpriteSettings = New InputSettings.SpriteInputSettings With {.PlaceHolder = True}})
		Dim tools As New RDLevelHandler(Testlevel)
		'tools.CombineToTag("123", FilterCodeCSharp("return Y++==1;"), True)
		'	tools.AddTimer(New FloatingText With {.})
		'tools.SplitRow(False)
		Testlevel.SaveFile("C:\Users\30698\OneDrive\ÎÄµµ\rdlevels\xnkl\levelEdited.rdlevel", New InputSettings.LevelInputSettings With {.SpriteSettings = New InputSettings.SpriteInputSettings With {.PlaceHolder = True}})
	End Sub
End Module