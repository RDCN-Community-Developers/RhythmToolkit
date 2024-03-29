Imports RhythmBase.Objects
Imports RhythmBase.Events
Imports System.Drawing
Imports System.Reflection.Metadata
Namespace Animation
	Public Interface IAnimation
		ReadOnly Property Parent As BaseEvent
		ReadOnly Property Start As Single
		ReadOnly Property Duration As Single
		ReadOnly Property [End] As Single
		ReadOnly Property Ease As EaseType
		'Function Current(startValue As IAnimation, beat As Single) As IAnimation
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
		Public Function Current(startValue As Value, beat As Single, variables As Variables) As Value
			If beat > Duration Then
				Return New Value(Parent, Value, EaseType.Linear, beat, 0)
			End If
			Return New Value(
				Parent,
				New Number(RhythmBase.Ease.Ease(beat / Duration, startValue.Value.GetValue(variables), Me.Value.GetValue(variables), Ease)),
				EaseType.Linear,
				beat,
				0
			)
		End Function
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
		Public Function Current(startValue As Color, beat As Single) As Color
			If beat > Duration Then
				Return New Color(Parent, Value, EaseType.Linear, beat, 0)
			End If
			Return New Color(
				Parent,
				New SkiaSharp.SKColor(
					RhythmBase.Ease.Ease(beat / Duration, startValue.Value.Red, Me.Value.Red, Ease),
					RhythmBase.Ease.Ease(beat / Duration, startValue.Value.Green, Me.Value.Green, Ease),
					RhythmBase.Ease.Ease(beat / Duration, startValue.Value.Blue, Me.Value.Blue, Ease),
					RhythmBase.Ease.Ease(beat / Duration, startValue.Value.Alpha, Me.Value.Alpha, Ease)
				),
				EaseType.Linear,
				beat,
				0
			)
		End Function
	End Structure
	Public Structure Pair
		Implements IAnimation
		Public ReadOnly Property Parent As BaseEvent Implements IAnimation.Parent
		Friend ReadOnly Value As NumberOrExpressionPair
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
		Public Function Current(startValue As Pair, beat As Single, variables As Variables) As Pair
			If beat > Duration Then
				Return New Pair(Parent, Value, EaseType.Linear, beat, 0)
			End If
			Return New Pair(
				Parent,
				New NumberOrExpressionPair(
					X?.Current(startValue.X, beat, variables).Value,
					Y?.Current(startValue.Y, beat, variables).Value
				),
				EaseType.Linear,
				beat,
				0
			)
		End Function
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
		Public Function Current(startValue As Movement, beat As Single, variables As Variables) As Movement
			If beat > Duration Then
				Return New Movement(Parent, _Location, _Size, _Pivot, _Angle, EaseType.Linear, beat, 0)
			End If
			Return New Movement(
				Parent,
				Location.Current(startValue.Location, beat, variables).Value,
				Size.Current(startValue.Size, beat, variables).Value,
				Pivot.Current(startValue.Pivot, beat, variables).Value,
				Angle.Current(startValue.Angle, beat, variables).Value,
				EaseType.Linear,
				beat,
				0
			)
		End Function
	End Structure
	Public Structure Gradient
		Implements IAnimation
		Public ReadOnly Property Parent As BaseEvent Implements IAnimation.Parent
		Private ReadOnly _Color1 As SkiaSharp.SKColor
		Private ReadOnly _Color2 As SkiaSharp.SKColor
		Private ReadOnly _Value1 As Single?
		Private ReadOnly _Value2 As Single?
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
		Public Function Current(startValue As Gradient, beat As Single, variables As Variables) As Gradient
			If beat > Duration Then
				Return New Gradient(Parent, _Color1, _Color2, _Value1, _Value2, EaseType.Linear, beat, 0)
			End If
			Return New Gradient(
				Parent,
				Color1.Current(startValue.Color1, beat).Value,
				Color2.Current(startValue.Color2, beat).Value,
				Value1.Current(startValue.Value1, beat, variables).Value.GetValue(variables),
				Value2.Current(startValue.Value2, beat, variables).Value.GetValue(variables),
				EaseType.Linear,
				beat,
				0
			)
		End Function
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
	Public MustInherit Class EaseCalculator(Of T As IAnimation)
		Private ReadOnly list As IReadOnlyCollection(Of T)
		Private ReadOnly variables As Variables
		Private ReadOnly [default] As T
		Public Sub New(list As IReadOnlyCollection(Of T), defaultValue As T)
			Me.list = list
			Me.default = defaultValue
		End Sub
		Public MustOverride Function GetValue(beat As Single) As T
	End Class
	Public Class ValueCalculator
		Inherits EaseCalculator(Of Value)
		Public Sub New(list As IReadOnlyCollection(Of Value), defaultValue As Value)
			MyBase.New(list, defaultValue)
		End Sub
		Public Overrides Function GetValue(beat As Single) As Value
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace