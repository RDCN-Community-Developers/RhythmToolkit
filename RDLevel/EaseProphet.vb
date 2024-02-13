Imports System.Diagnostics.Eventing
Imports RDLevel.Objects
Imports RDLevel.Ease
Module EaseProphet
End Module
Interface IEaseProperty
	Property Ease As EaseType
	Property Start As Single
	Property Duration As Single
	ReadOnly Property [End] As Single

End Interface
Class EaseValueProperty
	Implements IEaseProperty

	Public Property Ease As EaseType Implements IEaseProperty.Ease
	Public Property TargetValue As Single
	Public Property Start As Single Implements IEaseProperty.Start
	Public Property Duration As Single Implements IEaseProperty.Duration
	Public ReadOnly Property [End] As Single Implements IEaseProperty.End
		Get
			Return Start + Duration
		End Get
	End Property
	Public Sub New(start As Single, duration As Single, target As Single, Optional ease As EaseType = EaseType.Linear)
		Me.Start = start
		Me.Ease = ease
		Me.TargetValue = target
		Me.Duration = duration
	End Sub
	Public Function Current(startValue As Single, time As Single) As Single
		If time > Duration Then
			Return TargetValue
		End If
		Return RDLevel.Ease.Ease(time / Duration, startValue, TargetValue, Ease)
	End Function
	Public Overrides Function ToString() As String
		Return $"{Duration},{TargetValue}"
	End Function
End Class
Class EasePointProperty
	Implements IEaseProperty
	Public ReadOnly Property X As EaseValueProperty
		Get
			Return If(TargetValue.X, New EaseValueProperty(Start, Duration, TargetValue.X, Ease), Nothing)
		End Get
	End Property
	Public ReadOnly Property Y As EaseValueProperty
		Get
			Return If(TargetValue.Y, New EaseValueProperty(Start, Duration, TargetValue.Y, Ease), Nothing)
		End Get
	End Property
	Public Property Ease As EaseType Implements IEaseProperty.Ease
	Public Property TargetValue As FunctionalPointF
	Public Property Start As Single Implements IEaseProperty.Start
	Public Property Duration As Single Implements IEaseProperty.Duration
	Public ReadOnly Property [End] As Single Implements IEaseProperty.End
		Get
			Return Start + Duration
		End Get
	End Property
	Public Sub New(start As Single, duration As Single, target As FunctionalPointF, Optional ease As EaseType = EaseType.Linear)
		Me.Start = start
		Me.Ease = ease
		Me.TargetValue = target
		Me.Duration = duration
	End Sub
	Public Overrides Function ToString() As String
		Return Ease.ToString
	End Function
End Class
Interface IEaseList
End Interface
Class EaseValueList
	Implements IEaseList
	Public EventList As New List(Of EaseValueProperty)
	Public Function GetData(time As Single) As Single
		Dim L As EaseValueProperty = EventList.LastOrDefault(Function(i) i.Start < time)
		If L Is Nothing Then
			Return 0
		End If
		If L.End < time Then
			Return L.TargetValue
		Else
			Return L.Current(GetData(L.Start), time - L.Start)
		End If
	End Function
End Class
Class EasePointList
	Implements IEaseList
	Private ReadOnly Property XList As EaseValueList
		Get
			Dim N As New EaseValueList With {
				.EventList = EventList.Where(Function(i) i.X IsNot Nothing).Select(Of EaseValueProperty)(Function(i) New EaseValueProperty(i.Start, i.Duration, i.X.TargetValue, i.Ease)).ToList
			}
			Return N
		End Get
	End Property
	Private ReadOnly Property YList As EaseValueList
		Get
			Dim N As New EaseValueList With {
				.EventList = EventList.Where(Function(i) i.Y IsNot Nothing).Select(Of EaseValueProperty)(Function(i) New EaseValueProperty(i.Start, i.Duration, i.Y.TargetValue, i.Ease)).ToList
			}
			Return N
		End Get
	End Property
	Public EventList As New List(Of EasePointProperty)
	Public Function GetData(time As Single) As FunctionalPointF
		Return (XList.GetData(time), YList.GetData(time))
	End Function
	Public Function Copy() As EasePointList
		Return Me.MemberwiseClone
	End Function
End Class
