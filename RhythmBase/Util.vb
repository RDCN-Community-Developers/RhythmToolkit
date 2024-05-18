Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports RhythmBase.Events
Imports RhythmBase.Extensions
Imports RhythmBase.LevelElements
Imports RhythmBase.Components
''' <summary>
''' 工具类
''' </summary>
Namespace Utils
    Public Module [Global]
        Public ReadOnly RowTypes As IEnumerable(Of EventType) = ConvertToEnums(Of BaseRowAction)()
        Public ReadOnly DecorationTypes As IEnumerable(Of EventType) = ConvertToEnums(Of BaseDecorationAction)()
    End Module
    Public Class BeatCalculator
        Private CPBs As IEnumerable(Of SetCrotchetsPerBar)
        Private BPMs As IEnumerable(Of BaseBeatsPerMinute)
        Public Sub New(CPBCollection As IEnumerable(Of SetCrotchetsPerBar), BPMCollection As IEnumerable(Of BaseBeatsPerMinute))
            Initialize(CPBCollection, BPMCollection)
        End Sub
        Public Sub New(level As RDLevel)
            Initialize(level.Where(Of SetCrotchetsPerBar), level.Where(Of BaseBeatsPerMinute))
        End Sub
        Private Sub Initialize(CPBs As IEnumerable(Of SetCrotchetsPerBar), BPMs As IEnumerable(Of BaseBeatsPerMinute))
            Me.CPBs = CPBs '.OrderBy(Function(i) i.BeatOnly)
            Me.BPMs = BPMs '.OrderBy(Function(i) i.BeatOnly)
            Initialize(CPBs)
        End Sub
        Public Shared Sub Initialize(CPBs As IEnumerable(Of SetCrotchetsPerBar))
            For Each item In CPBs
                item.Bar = BeatOnly_BarBeat(item.BeatOnly, CPBs).bar
            Next
        End Sub
        Public Function BarBeat_BeatOnly(bar As UInteger, beat As Single) As Single
            Return BarBeat_BeatOnly(bar, beat, CPBs)
        End Function
        Public Function BarBeat_Time(bar As UInteger, beat As Single) As TimeSpan
            Return BeatOnly_Time(BarBeat_BeatOnly(bar, beat))
        End Function
        Public Function BeatOnly_BarBeat(beat As Single) As (bar As UInteger, beat As Single)
            Return BeatOnly_BarBeat(beat, CPBs)
        End Function
        Public Function BeatOnly_Time(beatOnly As Single) As TimeSpan
            Return BeatOnly_Time(beatOnly, BPMs)
        End Function
        Public Function Time_BeatOnly(timeSpan As TimeSpan) As Single
            Return Time_BeatOnly(timeSpan, BPMs)
        End Function
        Public Function Time_BarBeat(timeSpan As TimeSpan) As (bar As UInteger, beat As Single)
            Return BeatOnly_BarBeat(Time_BeatOnly(timeSpan))
        End Function
        Public Function IntervalTime(beat1 As Single, beat2 As Single) As TimeSpan
            Return BeatOnly_Time(beat1) - BeatOnly_Time(beat2)
        End Function
        Public Function IntervalTime(event1 As BaseEvent, event2 As BaseEvent) As TimeSpan
            Return IntervalTime(event1.BeatOnly, event2.BeatOnly)
        End Function
        Public Shared Function BarBeat_BeatOnly(bar As UInteger, beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As Single
            Dim foreCPB As New SetCrotchetsPerBar(1, 0, 8, 1)
            Dim result As Single = 0
            Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Bar < bar, foreCPB)
            result = LastCPB.BeatOnly + (bar - LastCPB.Bar) * LastCPB.CrotchetsPerBar + beat - 1
            Return result
        End Function
        Public Shared Function BeatOnly_BarBeat(beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As (bar As UInteger, beat As Single)
            Dim foreCPB As New SetCrotchetsPerBar(1, 0, 8, 1)
            Dim result As (bar As UInteger, beat As Single) = (1, 1)

            Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.BeatOnly < beat, foreCPB)

            result.bar = LastCPB.Bar + Math.Floor((beat - LastCPB.BeatOnly) / LastCPB.CrotchetsPerBar)
            result.beat = (beat - LastCPB.BeatOnly) Mod LastCPB.CrotchetsPerBar + 1

            Return result
        End Function
        Private Shared Function BeatOnly_Time(beatOnly As Single, BPMCollection As IEnumerable(Of BaseBeatsPerMinute)) As TimeSpan
            Dim foreBPM As BaseBeatsPerMinute = New SetBeatsPerMinute(
                1,
                BPMCollection.FirstOrDefault(Function(i)
                                                 Return i.Active AndAlso i.Type = EventType.PlaySong
                                             End Function, New SetBeatsPerMinute(1, 100, 0)).BeatsPerMinute,
                0)
            Dim resultMinute As Single = 0
            For Each item As BaseBeatsPerMinute In BPMCollection
                If beatOnly > item.BeatOnly Then
                    resultMinute += (item.BeatOnly - foreBPM.BeatOnly) / foreBPM.BeatsPerMinute
                    foreBPM = item
                Else
                    Exit For
                End If
            Next
            resultMinute += (beatOnly - foreBPM.BeatOnly) / foreBPM.BeatsPerMinute
            Return TimeSpan.FromMinutes(resultMinute)
        End Function
        Private Shared Function Time_BeatOnly(timeSpan As TimeSpan, BPMCollection As IEnumerable(Of BaseBeatsPerMinute)) As Single
            Dim foreBPM As BaseBeatsPerMinute = New SetBeatsPerMinute(1, BPMCollection.FirstOrDefault(Function(i) i.Active AndAlso i.Type = EventType.PlaySong, New SetBeatsPerMinute(1, 100, 0)).BeatsPerMinute, 0)
            Dim beatOnly As Single = 1
            For Each item As BaseBeatsPerMinute In BPMCollection
                If timeSpan > BeatOnly_Time(item.BeatOnly, BPMCollection) Then
                    beatOnly +=
                        (
                            BeatOnly_Time(item.BeatOnly, BPMCollection) -
                            BeatOnly_Time(foreBPM.BeatOnly, BPMCollection)
                        ).TotalMinutes * foreBPM.BeatsPerMinute
                    foreBPM = item
                Else
                    Exit For
                End If
            Next
            beatOnly += (
                            timeSpan - BeatOnly_Time(foreBPM.BeatOnly, BPMCollection)
                        ).TotalMinutes * foreBPM.BeatsPerMinute
            Return beatOnly
        End Function
    End Class
    Public Module Others
        Public Function PercentToPixel(point As (X As Single?, Y As Single?)) As (X As Single?, Y As Single?)
            Return PercentToPixel(point, (352, 198))
        End Function
        Public Function PercentToPixel(point As (X As Single?, Y As Single?), size As (X As Single, Y As Single)) As (X As Single?, Y As Single?)
            Return (point.X * size.X / 100, point.Y * size.Y / 100)
        End Function
        Public Function PixelToPercent(point As (X As Single?, Y As Single?)) As (X As Single?, Y As Single?)
            Return PixelToPercent(point, (352, 198))
        End Function
        Public Function PixelToPercent(point As (X As Single?, Y As Single?), size As (X As Single, Y As Single)) As (X As Single?, Y As Single?)
            Return (point.X * 100 / size.X, point.Y * 100 / size.Y)
        End Function
        Public Function Clone(Of T)(e As T) As T
            If e Is Nothing Then
                Return Nothing
            End If
            Dim type As Type = GetType(T)
            Dim copy = Activator.CreateInstance(type)

            Dim properties() As PropertyInfo = type.GetProperties(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
            For Each p In properties
                If p.CanWrite Then
                    p.SetValue(copy, p.GetValue(e))
                End If
            Next
            Return copy
            Return Clone(e)
        End Function
    End Module
    Public Module TypeConvert
        Public Function ConvertToEnum(type As Type) As EventType
            Dim result As EventType
            If [Enum].TryParse(type.Name, result) Then
                Return result
            End If
            Throw New Exceptions.RhythmBaseException($"Illegal Type: {type.Name}.")
        End Function
        Public Function ConvertToEnum(Of T As {BaseEvent, New})() As EventType
            Dim result As EventType
            If [Enum].TryParse(GetType(T).Name, result) Then
                Return result
            End If
            Throw New Exceptions.RhythmBaseException($"Illegal Type: {GetType(T).Name}.")
        End Function
        Public Function ConvertToEnums(Of T As BaseEvent)() As EventType()
            Dim types = Assembly.GetAssembly(GetType(T)).GetTypes()
            Return types.Where(Function(i) i.Namespace = GetType(T).Namespace AndAlso
                                        (i = GetType(T) OrElse i.IsSubclassOf(GetType(T))) AndAlso
                                        Not i.IsAbstract).Select(Function(i) ConvertToEnum(i)).ToArray
        End Function
        Public Function ConvertToType(type As String) As Type
            Dim result As EventType
            If [Enum].TryParse(type, result) Then
                Return result.ConvertToType()
            End If
            Return EventType.CustomEvent.ConvertToType
        End Function
        <Extension>
        Public Function ConvertToType(type As EventType) As Type
            Dim result = System.Type.GetType($"{GetType(BaseEvent).Namespace}.{type}")
            If result Is Nothing Then
                Throw New Exceptions.RhythmBaseException($"Illegal Type: {type}.")
            End If
            Return result
        End Function
    End Module
End Namespace
Namespace Exceptions
    Public Class ModulesException
        Inherits RhythmBaseException
        Sub New()
            MyBase.New()
        End Sub
        Sub New(Message As String)
            MyBase.New(Message)
        End Sub
    End Class
End Namespace
Namespace Extensions
    Public Module Extension
        <Extension>
        Public Function UpperCamelCase(e As String) As String
            Dim S = e.ToArray
            S(0) = S(0).ToString.ToUpper
            Return String.Join("", S)
        End Function
        <Extension>
        Public Function LowerCamelCase(e As String) As String
            Dim S = e.ToArray
            S(0) = S(0).ToString.ToLower
            Return String.Join("", S)
        End Function
        <Extension>
        Public Function RgbaToArgb(Rgba As Int32) As Int32
            Return ((Rgba >> 8) And &HFFFFFF) Or ((Rgba << 24) And &HFF000000)
        End Function
        <Extension>
        Public Function ArgbToRgba(Argb As Int32) As Int32
            Return ((Argb >> 24) And &HFF) Or ((Argb << 8) And &HFFFFFF00)
        End Function
        <Extension>
        Public Function FixFraction(number As Single, splitBase As UInteger) As Single
            Return Math.Round(number * splitBase) / splitBase
        End Function
    End Module
    Public Module EventsExtension
        <Extension>
        Public Sub MovePositionMaintainVisual(e As Move, target As RDPoint)
            If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
                Exit Sub
            End If
            e.Position = target
            e.Pivot = (e.VisualPosition() - target).Rotate(-e.Angle.TryGetValue())
        End Sub
        <Extension>
        Public Sub MovePositionMaintainVisual(e As MoveRoom, target As RDPoint)
            If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
                Exit Sub
            End If
            e.RoomPosition = target
            e.Pivot = (e.VisualPosition() - target).Rotate(-e.Angle.TryGetValue())
        End Sub
        <Extension>
        Public Function VisualPosition(e As Move) As RDPoint
            If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse e.Scale Is Nothing Then
                Return New RDPoint
            End If
            Dim previousPosition As RDPoint = e.Position
            Dim previousPivot As RDPoint = (CType(e.Pivot, RDPoint) * e.Scale * e.Parent.Size) / (100, 100)
            Return previousPosition + previousPivot.Rotate(e.Angle.TryGetValue())
        End Function
        <Extension>
        Public Function VisualPosition(e As MoveRoom) As RDPoint
            If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
                Return New RDPoint
            End If
            Dim previousPosition As RDPoint = e.RoomPosition
            Dim previousPivot As RDPoint = CType(e.Pivot, RDPoint) * e.Scale
            Return previousPosition + previousPivot.Rotate(e.Angle.TryGetValue())
        End Function
    End Module
End Namespace