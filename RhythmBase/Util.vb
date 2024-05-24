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
        Private ReadOnly Collection As OrderedEventCollection(Of BaseEvent)
        Public Sub New(CPBCollection As IEnumerable(Of SetCrotchetsPerBar), BPMCollection As IEnumerable(Of BaseBeatsPerMinute))
            Collection = New OrderedEventCollection(Of BaseEvent)
            Collection.AddRange(CPBCollection)
            Collection.AddRange(BPMCollection)
            Initialize(Collection.Where(Of SetCrotchetsPerBar))
        End Sub
        Public Sub New(level As RDLevel)
            Collection = level
            Initialize(Collection.Where(Of SetCrotchetsPerBar))
        End Sub
        Public Shared Sub Initialize(CPBs As IEnumerable(Of SetCrotchetsPerBar))
            For Each item In CPBs
                item.Bar = BeatOnly_BarBeat(item.Beat.BeatOnly, CPBs).bar
            Next
        End Sub
        Public Function BarBeat_BeatOnly(bar As UInteger, beat As Single) As Single
            Return BarBeat_BeatOnly(bar, beat, Collection.Where(Of SetCrotchetsPerBar))
        End Function
        Public Function BarBeat_Time(bar As UInteger, beat As Single) As TimeSpan
            Return BeatOnly_Time(BarBeat_BeatOnly(bar, beat))
        End Function
        Public Function BeatOnly_BarBeat(beat As Single) As (bar As UInteger, beat As Single)
            Return BeatOnly_BarBeat(beat, Collection.Where(Of SetCrotchetsPerBar))
        End Function
        Public Function BeatOnly_Time(beatOnly As Single) As TimeSpan
            Return BeatOnly_Time(beatOnly, Collection.Where(Of BaseBeatsPerMinute))
        End Function
        Public Function Time_BeatOnly(timeSpan As TimeSpan) As Single
            Return Time_BeatOnly(timeSpan, Collection.Where(Of BaseBeatsPerMinute))
        End Function
        Public Function Time_BarBeat(timeSpan As TimeSpan) As (bar As UInteger, beat As Single)
            Return BeatOnly_BarBeat(Time_BeatOnly(timeSpan))
        End Function
        Public Function IntervalTime(beat1 As Single, beat2 As Single) As TimeSpan
            Return BeatOnly_Time(beat1) - BeatOnly_Time(beat2)
        End Function
        Public Function IntervalTime(event1 As BaseEvent, event2 As BaseEvent) As TimeSpan
            Return IntervalTime(event1.Beat.BeatOnly, event2.Beat.BeatOnly)
        End Function
        Public Shared Function BarBeat_BeatOnly(bar As UInteger, beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As Single
            Dim foreCPB As (BeatOnly As Single, Bar As UInteger, CPB As UInteger) = (1, 1, 8)
            Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Bar < bar)
            If LastCPB IsNot Nothing Then
                foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Bar, LastCPB.CrotchetsPerBar)
            End If
            Dim result = foreCPB.BeatOnly + (bar - foreCPB.Bar) * foreCPB.CPB + beat - 1
            Return result
        End Function
        Public Shared Function BeatOnly_BarBeat(beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As (bar As UInteger, beat As Single)
            Dim foreCPB As (BeatOnly As Single, Bar As UInteger, CPB As UInteger) = (1, 1, 8)
            Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Beat < beat)
            If LastCPB IsNot Nothing Then
                foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Bar, LastCPB.CrotchetsPerBar)
            End If
            Dim result = (foreCPB.Bar + Math.Floor((beat - foreCPB.BeatOnly) / foreCPB.CPB),
                (beat - foreCPB.BeatOnly) Mod foreCPB.CPB + 1)

            Return result
        End Function
        Private Shared Function BeatOnly_Time(beatOnly As Single, BPMCollection As IEnumerable(Of BaseBeatsPerMinute)) As TimeSpan
            Dim fore As (BeatOnly As Single, BPM As Single) = (1, 100)
            Dim foreBPM = BPMCollection.FirstOrDefault()
            If foreBPM IsNot Nothing Then
                fore = (foreBPM.Beat.BeatOnly, foreBPM.BeatsPerMinute)
            End If
            Dim resultMinute As Single = 0
            For Each item As BaseBeatsPerMinute In BPMCollection
                If beatOnly > item.Beat.BeatOnly Then
                    resultMinute += (item.Beat.BeatOnly - fore.BeatOnly) / fore.BPM
                    fore = (item.Beat.BeatOnly, item.BeatsPerMinute)
                Else
                    Exit For
                End If
            Next
            resultMinute += (beatOnly - foreBPM.Beat.BeatOnly) / foreBPM.BeatsPerMinute
            Return TimeSpan.FromMinutes(resultMinute)
        End Function
        Private Shared Function Time_BeatOnly(timeSpan As TimeSpan, BPMCollection As IEnumerable(Of BaseBeatsPerMinute)) As Single
            Dim fore As (BeatOnly As Single, BPM As Single) = (1, 100)
            Dim foreBPM = BPMCollection.FirstOrDefault()
            If foreBPM IsNot Nothing Then
                fore = (foreBPM.Beat.BeatOnly, foreBPM.BeatsPerMinute)
            End If
            Dim beatOnly As Single = 1
            For Each item As BaseBeatsPerMinute In BPMCollection
                If timeSpan > BeatOnly_Time(item.Beat.BeatOnly, BPMCollection) Then
                    beatOnly +=
                        (
                            BeatOnly_Time(item.Beat.BeatOnly, BPMCollection) -
                            BeatOnly_Time(fore.BeatOnly, BPMCollection)
                        ).TotalMinutes * fore.BPM
                    foreBPM = item
                Else
                    Exit For
                End If
            Next
            beatOnly += (
                            timeSpan - BeatOnly_Time(fore.BeatOnly, BPMCollection)
                        ).TotalMinutes * foreBPM.BeatsPerMinute
            Return beatOnly
        End Function
        Public Function BeatOf(beatOnly As Single) As RDBeat
            Return New RDBeat(Me, beatOnly)
        End Function
        Public Function BeatOf(bar As UInteger, beat As Single) As RDBeat
            Return New RDBeat(Me, bar, beat)
        End Function
        Public Function BeatOf(timeSpan As TimeSpan) As RDBeat
            Return New RDBeat(Me, timeSpan)
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