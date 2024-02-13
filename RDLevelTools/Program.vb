Imports System
Imports RDLevelTools.Tools
Imports RDLevel
Imports RDLevel.Objects
Module Program
	Sub Main(args As String())
		Dim Testlevel As Objects.RDLevel = Objects.LoadFile("C:\Users\30698\OneDrive\ÎÄµµ\rdlevels\FindersX Remix\FindersY.rdlevel")
		Dim tools As New RDLevelHandler(Testlevel)
		'tools.CombineToTag("123", FilterCodeCSharp("return Y++==1;"), True)
		tools.PressOnEveryBeat()
		Testlevel.SaveFile("C:\Users\30698\OneDrive\ÎÄµµ\rdlevels\FindersX Remix\FindersYEdited.rdlevel")
	End Sub
End Module
