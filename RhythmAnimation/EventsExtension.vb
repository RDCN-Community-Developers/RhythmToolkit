Imports RhythmBase.Events
Imports RhythmBase.LevelElements
Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions
Imports System.Text.RegularExpressions
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
		<Extension>
		Public Function GetExpValue(e As Exp, variables As Variables) As Object
			Dim paramererExpression = Expression.Parameter(GetType(Variables), "variable")
			Dim compiledFunction = Expressions.GetExpressionTree(e.value, paramererExpression)
			Return compiledFunction(variables)
		End Function
		<Extension>
		Public Function GetValue(e As INumOrExp, variables As Variables) As Object
			If e.GetType = GetType(Num) Then
				Return CType(e, Num).value
			Else
				Return CType(e, Exp).GetExpValue(variables)
			End If
		End Function
	End Module
End Namespace