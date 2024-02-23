Imports System.Diagnostics.Eventing
Imports RhythmBase.Objects
Imports RhythmBase.Ease
Class EaseValueProperty
	Private ReadOnly variables As Variables
	Public Property Ease As EaseType
	Private ReadOnly targetValue As INumberOrExpression
	Public Function GetTargetValue() As Single
		Return TargetValue.GetValue(variables)
	End Function

	Public ReadOnly Property StartBeat As Single
	Public ReadOnly Property Duration As Single
	Public ReadOnly Property EndBeat As Single
		Get
			Return StartBeat + Duration
		End Get
	End Property
	Public Sub New(startBeat As Single, duration As Single, target As INumberOrExpression, ease As EaseType)
		Me.StartBeat = startBeat
		Me.Ease = ease
		Me.TargetValue = target
		Me.Duration = duration
	End Sub
	Public Function Current(startValue As Single, time As Single) As Single
		If time > Duration Then
			Return TargetValue.GetValue(variables)
		End If
		Return RhythmBase.Ease.Ease(time / Duration, startValue, GetTargetValue(), Ease)
	End Function
	Public Overrides Function ToString() As String
		Return $"{Duration},{TargetValue}"
	End Function
End Class
Class EaseValueList
	Public EventList As New List(Of EaseValueProperty)
	Public Function GetData(time As Single) As Single
		Dim L As EaseValueProperty = EventList.LastOrDefault(Function(i) i.StartBeat < time)
		If L Is Nothing Then
			Return 0
		End If
		If L.EndBeat < time Then
			Return L.GetTargetValue()
		Else
			Return L.Current(GetData(L.StartBeat), time - L.StartBeat)
		End If
	End Function
End Class
