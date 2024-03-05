Imports RhythmBase.Objects
Imports RhythmBase.Events
Namespace Animation
	Public Interface IAnimation
		ReadOnly Property Parent As BaseEvent
		ReadOnly Property Start As Single
		ReadOnly Property Duration As Single
		ReadOnly Property [End] As Single
		ReadOnly Property Ease As EaseType
	End Interface
	Public Structure Value
		Implements IAnimation
		Public ReadOnly Property Parent As BaseEvent Implements IAnimation.Parent
		Public ReadOnly Value As INumberOrExpression
		Public ReadOnly Property Ease As EaseType Implements IAnimation.Ease
		Public ReadOnly Property Start As Single Implements IAnimation.Start
		Public ReadOnly Property Duration As Single Implements IAnimation.Duration
		Public ReadOnly Property [End] As Single Implements IAnimation.End
			Get
				Return Start + Duration
			End Get
		End Property
		Friend Sub New(parent As BaseEvent, value As INumberOrExpression, ease As EaseType, start As Single, duration As Single)
			Me.Parent = parent
			Me.Value = value
			Me.Ease = ease
			Me.Start = start
			Me.Duration = duration
		End Sub
	End Structure
	Public Structure Color
		Implements IAnimation
		Public ReadOnly Property Parent As BaseEvent Implements IAnimation.Parent
		Public ReadOnly Value As SkiaSharp.SKColor
		Public ReadOnly Property Start As Single Implements IAnimation.Start
		Public ReadOnly Property Duration As Single Implements IAnimation.Duration
		Public ReadOnly Property [End] As Single Implements IAnimation.End
			Get
				Return Start + Duration
			End Get
		End Property
		Public ReadOnly Property Ease As EaseType Implements IAnimation.Ease
		Friend Sub New(parent As BaseEvent, value As SkiaSharp.SKColor, ease As EaseType, start As Single, duration As Single)
			Me.Parent = parent
			Me.Value = value
			Me.Ease = ease
			Me.Start = start
			Me.Duration = duration
		End Sub
	End Structure
	Public Structure Pair
		Implements IAnimation
		Public ReadOnly Property Parent As BaseEvent Implements IAnimation.Parent
		Private ReadOnly Value As NumberOrExpressionPair
		Public ReadOnly Property Ease As EaseType Implements IAnimation.Ease
		Public ReadOnly Property Start As Single Implements IAnimation.Start
		Public ReadOnly Property Duration As Single Implements IAnimation.Duration
		Public ReadOnly Property [End] As Single Implements IAnimation.End
			Get
				Return Start + Duration
			End Get
		End Property
		Public ReadOnly Property X As Value?
			Get
				Return If(Value.X Is Nothing, Nothing, New Value(Parent, Value.X, Ease, Start, Duration))
			End Get
		End Property
		Public ReadOnly Property Y As Value?
			Get
				Return If(Value.Y Is Nothing, Nothing, New Value(Parent, Value.X, Ease, Start, Duration))
			End Get
		End Property
		Friend Sub New(parent As BaseEvent, value As NumberOrExpressionPair, ease As EaseType, start As Single, duration As Single)
			Me.Parent = parent
			Me.Value = value
			Me.Ease = ease
			Me.Start = start
			Me.Duration = duration
		End Sub
	End Structure
	Public Structure Movement
		Implements IAnimation
		Public ReadOnly Property Parent As BaseEvent Implements IAnimation.Parent
		Private ReadOnly _Location As NumberOrExpressionPair?
		Private ReadOnly _Size As NumberOrExpressionPair?
		Private ReadOnly _Pivot As NumberOrExpressionPair?
		Private ReadOnly _Angle As INumberOrExpression
		Public ReadOnly Property Location As Pair
			Get
				Return New Pair(Parent, _Location, Ease, Start, Duration)
			End Get
		End Property
		Public ReadOnly Property Size As Pair
			Get
				Return New Pair(Parent, _Size, Ease, Start, Duration)
			End Get
		End Property
		Public ReadOnly Property Pivot As Pair
			Get
				Return New Pair(Parent, _Pivot, Ease, Start, Duration)
			End Get
		End Property
		Public ReadOnly Property Angle As Value
			Get
				Return New Value(Parent, _Angle, Ease, Start, Duration)
			End Get
		End Property
		Public ReadOnly Property Ease As EaseType Implements IAnimation.Ease
		Public ReadOnly Property Start As Single Implements IAnimation.Start
		Public ReadOnly Property Duration As Single Implements IAnimation.Duration
		Public ReadOnly Property [End] As Single Implements IAnimation.End
			Get
				Return Start + Duration
			End Get
		End Property
		Friend Sub New(parent As BaseEvent, location As NumberOrExpressionPair?, size As NumberOrExpressionPair?, pivot As NumberOrExpressionPair?, angle As INumberOrExpression, ease As EaseType, start As Single, duration As Single)
			Me.Parent = parent
			Me._Location = location
			Me._Size = size
			Me._Pivot = pivot
			Me._Angle = angle
			Me.Ease = ease
			Me.Start = start
			Me.Duration = duration
		End Sub
	End Structure
	Public Structure Gradient
		Implements IAnimation
		Public ReadOnly Property Parent As BaseEvent Implements IAnimation.Parent
		Private _Color1 As SkiaSharp.SKColor
		Private _Color2 As SkiaSharp.SKColor
		Private _Value1 As Single?
		Private _Value2 As Single?
		Public ReadOnly Property Color1 As Color
			Get
				Return New Color(Parent, _Color1, Ease, Start, Duration)
			End Get
		End Property
		Public ReadOnly Property Color2 As Color
			Get
				Return New Color(Parent, _Color2, Ease, Start, Duration)
			End Get
		End Property
		Public ReadOnly Property Value1 As Value
			Get
				Return New Value(Parent, New Number(_Value1), Ease, Start, Duration)
			End Get
		End Property
		Public ReadOnly Property Value2 As Value
			Get
				Return New Value(Parent, New Number(_Value1), Ease, Start, Duration)
			End Get
		End Property
		Public ReadOnly Property Start As Single Implements IAnimation.Start
		Public ReadOnly Property Duration As Single Implements IAnimation.Duration
		Public ReadOnly Property [End] As Single Implements IAnimation.End
			Get
				Return Start + Duration
			End Get
		End Property
		Public ReadOnly Property Ease As EaseType Implements IAnimation.Ease
		Friend Sub New(parent As BaseEvent, color1 As SkiaSharp.SKColor?, color2 As SkiaSharp.SKColor?, value1 As Single?, value2 As Single, ease As EaseType, start As Single, duration As Single)
			Me.Parent = parent
			Me._Color1 = color1
			Me._Color2 = color2
			Me._Value1 = value1
			Me._Value2 = value2
			Me.Ease = ease
			Me.Start = start
			Me.Duration = duration
		End Sub
	End Structure
	Public Structure [Object]
		Implements IAnimation
		Public ReadOnly Property Parent As BaseEvent Implements IAnimation.Parent
		Public Value As Object
		Public ReadOnly Property Start As Single Implements IAnimation.Start
		Public ReadOnly Property Duration As Single Implements IAnimation.Duration
		Public ReadOnly Property [End] As Single Implements IAnimation.End
			Get
				Return Start + Duration
			End Get
		End Property
		Public ReadOnly Property Ease As EaseType Implements IAnimation.Ease
		Public Sub New(parent As BaseEvent, value As Object, ease As EaseType, start As Single, duration As Single)
			Me.Parent = parent
			Me.Value = value
			Me.Ease = ease
			Me.Start = start
			Me.Duration = duration
		End Sub
	End Structure
	Class EaseValueProperty
		Private ReadOnly variables As Variables
		Public Property Ease As EaseType
		Private ReadOnly targetValue As INumberOrExpression
		Public Function GetTargetValue() As Single
			Return targetValue.GetValue(variables)
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
			Me.targetValue = target
			Me.Duration = duration
		End Sub
		Public Function Current(startValue As Single, time As Single) As Single
			If time > Duration Then
				Return targetValue.GetValue(variables)
			End If
			Return RhythmBase.Ease.Ease(time / Duration, startValue, GetTargetValue(), Ease)
		End Function
		Public Overrides Function ToString() As String
			Return $"{Duration},{targetValue}"
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
End Namespace