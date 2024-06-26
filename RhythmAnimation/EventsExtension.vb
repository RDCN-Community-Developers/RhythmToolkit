Imports RhythmBase.Events
Imports RhythmBase.LevelElements
Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions
Imports System.Text.RegularExpressions
Imports RhythmBase.Expressions
Imports RhythmBase.Components
Imports RhythmBase.Animation
Namespace Extensions
	Public Module EventsExtension
		<Extension>
		Public Function GetAnimation(e As RDMove) As EaseValueGroup(Of RDMove)
			Return New EaseValueGroup(Of RDMove)(e, e.Ease, e.Duration, NameOf(e.Position), NameOf(e.Scale), NameOf(e.Pivot), NameOf(e.Angle))
		End Function
		<Extension>
		Public Function GetAnimation(e As RDMoveRow) As EaseValueGroup(Of RDMoveRow)
			Return New EaseValueGroup(Of RDMoveRow)(e, e.Ease, e.Duration, NameOf(e.RowPosition), NameOf(e.Scale), NameOf(e.Pivot), NameOf(e.Angle))
		End Function

	End Module
End Namespace