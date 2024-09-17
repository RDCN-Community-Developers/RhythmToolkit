Imports RhythmBase.Events
Imports RhythmBase.Components
Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions
Imports System.Text.RegularExpressions
Imports RhythmBase.Expressions
Imports RhythmBase.Components
Imports RhythmBase.Animation
Namespace Extensions
	Public Module EventsExtension
		<Extension>
		Public Function GetAnimation(e As Move) As EaseValueGroup(Of Move)
			Return New EaseValueGroup(Of Move)(e, e.Ease, e.Duration, NameOf(e.Position), NameOf(e.Scale), NameOf(e.Pivot), NameOf(e.Angle))
		End Function
		<Extension>
		Public Function GetAnimation(e As MoveRow) As EaseValueGroup(Of MoveRow)
			Return New EaseValueGroup(Of MoveRow)(e, e.Ease, e.Duration, NameOf(e.RowPosition), NameOf(e.Scale), NameOf(e.Pivot), NameOf(e.Angle))
		End Function
	End Module
End Namespace