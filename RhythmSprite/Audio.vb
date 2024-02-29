Imports NAudio.Wave
Imports NAudio.Utils
Namespace Converters

End Namespace
Namespace Exceptions

End Namespace
Namespace Audio
	Public Interface IAudio
		ReadOnly Property FileInfo As IO.FileInfo
		ReadOnly Property Name As String
		ReadOnly Property Preview As Object
	End Interface

End Namespace