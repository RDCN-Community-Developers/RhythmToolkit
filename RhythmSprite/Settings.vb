Imports System.Numerics
Namespace Settings
	Public Class SpriteOutputSettings
		Public Enum OutputModes
			HORIZONTAL
			VERTICAL
			PACKED
		End Enum
		Public Sort As Boolean = False
		Public OverWrite As Boolean = False
		Public OutputMode As OutputModes = OutputModes.HORIZONTAL
		Public ExtraFile As Boolean = False
		Public LimitedSize As New Vector2(16384, 16384)
		Public LimitedCount As Vector2?
		Public WithImage As Boolean = False
	End Class
	Public Class SpriteInputSettings
	End Class
End Namespace