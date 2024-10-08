Imports RhythmBase.Components
Imports RhythmBase.Events
Imports RhythmBase.Extensions
Namespace Animation
    Public Class EaseValueGroup(Of TEvent As BaseEvent)
        Public ReadOnly Property Parent As BaseEvent
        Public ReadOnly Values As New Dictionary(Of String, Expression)
        Public ReadOnly Property Ease As EaseType
        Public ReadOnly Property Start As Single
        Public ReadOnly Property [End] As Single
            Get
                Return Start + Duration
            End Get
        End Property
        Public ReadOnly Property Duration As Single
        Public Sub New(parent As TEvent, ease As EaseType, duration As Single, propertyNames As IEnumerable(Of String))
            Dim type = GetType(TEvent)
            For Each name In propertyNames
                'Dim value = type.GetProperty(name).GetValue(parent)
                'If value IsNot Nothing Then
                '	If Num.CanCast(value) Then
                '		Values.Add(name, New Num(value))
                '	Else
                '		Try
                '			Values.Add(name, New ExpTemp(value))
                '		Catch
                '			Throw New Exceptions.ExpressionException($"Illegal value: {type}.{name} = {value}")
                '		End Try
                '	End If
                'End If
            Next
            Me.Parent = parent
            Me.Ease = ease
            Me.Start = parent.Beat.BeatOnly
            Me.Duration = duration
        End Sub
        Public Sub New(parent As TEvent, ease As EaseType, start As Single, duration As Single, ParamArray propertyNames() As String)
            Me.New(parent, ease, duration, propertyNames.ToList)
        End Sub
        Public Sub RefreshValue(variables As Variables)
            For Each pair In Values
                'Values(pair.Key) = New Num(pair.Value.GetValue(variables))
            Next
        End Sub
        Public Sub RefreshValue(realtimeVariables As Func(Of Single, Variables))
            RefreshValue(realtimeVariables(Parent.Beat.BeatOnly))
        End Sub
        Public Function GetValues(StartValues As IDictionary(Of String, Single), beat As Single, variables As Variables) As IDictionary(Of String, Single)
            For Each pair In Values
                StartValues(pair.Key) = Ease.Calculate((beat - Start) / Duration, StartValues(pair.Key), pair.Value.Expression(variables))
            Next
            Return StartValues
        End Function
    End Class
    Public Class EaseValueCalculator(Of TEvent As BaseEvent)
        Public ReadOnly Values As IEnumerable(Of EaseValueGroup(Of TEvent))
        Public Sub New(valueList As IEnumerable(Of EaseValueGroup(Of TEvent)))
            Me.Values = valueList.OrderBy(Function(i) i.Start)
        End Sub
        Public Function GetValue(beat As Single, realtimeVariables As Func(Of Single, Variables), defaultValueGroup As EaseValueGroup(Of TEvent)) As IDictionary(Of String, Single)
            Dim List As New LinkedList(Of EaseValueGroup(Of TEvent))(Values.Where(Function(i) i.Start < beat))
            If Not List.Any Then
                Return defaultValueGroup.Values.ToDictionary(Function(i) i.Key, Function(i) i.Value.Expression(realtimeVariables(0)))
            End If
            Dim p = List.Last
            While p.Previous IsNot Nothing AndAlso p.Previous.Value.End > p.Value.Start
                p = p.Previous
            End While
            Dim startValueGroup As EaseValueGroup(Of TEvent)
            If p.Previous Is Nothing Then
                startValueGroup = defaultValueGroup
            Else
                startValueGroup = p.Previous.Value
                For Each pair In p.Previous.Value.Values
                    If Not startValueGroup.Values.ContainsKey(pair.Key) Then
                        startValueGroup.Values.Add(pair.Key, pair.Value)
                    End If
                Next
            End If
            Dim runtimeValues As New Dictionary(Of String, Single)
            While p.Next IsNot Nothing
                runtimeValues = p.Value.GetValues(runtimeValues, p.Next.Value.Start, realtimeVariables(p.Value.Start))
                p = p.Next
            End While
            Return runtimeValues
        End Function
    End Class
End Namespace