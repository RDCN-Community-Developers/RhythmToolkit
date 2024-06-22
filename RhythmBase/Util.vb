Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
'' <summary>
'' 工具类
'' </summary>
Namespace Utils
    Public Module [Global]
        Private ReadOnly ETypes As List(Of Type) =
            GetType(BaseEvent).Assembly.GetTypes() _
                .Where(Function(i) i.IsAssignableTo(GetType(BaseEvent))).ToList
        Public ReadOnly TypesToEnum As Dictionary(Of Type, EventType()) =
            ETypes.ToDictionary(Function(i) i,
                          Function(i) ETypes _
                              .Where(Function(j) (j = i OrElse j.IsAssignableTo(i)) AndAlso Not j.IsAbstract) _
                              .Select(Function(j) ConvertToEnum(j)) _
                              .ToArray)
        Public ReadOnly EnumToType As Dictionary(Of EventType, Type) =
            [Enum].GetValues(Of EventType).ToDictionary(Function(i) i, Function(i) i.ConvertToType)
        Public ReadOnly RowTypes As List(Of EventType) =
            ConvertToEnums(Of BaseRowAction)().ToList
        Public ReadOnly DecorationTypes As List(Of EventType) =
            ConvertToEnums(Of BaseDecorationAction)().ToList
    End Module
    Public Class BeatCalculator
        Friend ReadOnly Collection As RDLevel
        Private _BPMList As List(Of BaseBeatsPerMinute)
        Private _CPBList As List(Of SetCrotchetsPerBar)
        Public Sub New(CPBCollection As IEnumerable(Of SetCrotchetsPerBar), BPMCollection As IEnumerable(Of BaseBeatsPerMinute))
            _BPMList = BPMCollection.OrderBy(Function(i) i.Beat).ToList
            _CPBList = CPBCollection.OrderBy(Function(i) i.Beat).ToList
            'Initialize()
        End Sub
        Public Sub New(level As RDLevel)
            Collection = level
            _BPMList = level.Where(Of BaseBeatsPerMinute).ToList
            _CPBList = level.Where(Of SetCrotchetsPerBar).ToList
            'Initialize()
        End Sub
        Public Shared Sub Initialize(CPBs As IEnumerable(Of SetCrotchetsPerBar))
            'For Each item In CPBs
            '    item.Bar = item.Beat.BarBeat.bar
            'Next
        End Sub
        Public Sub Refresh()
            _BPMList = Collection.Where(Of BaseBeatsPerMinute).ToList
            _CPBList = Collection.Where(Of SetCrotchetsPerBar).ToList
            'If _CPBList.Count <> 0 Then
            '    _CPBList(0).Bar = _CPBList(0).Beat.BarBeat.bar
            '    Dim CPB = _CPBList(0).CrotchetsPerBar
            '    For Each item In _CPBList.Skip(1)
            '        item.Bar = item.Beat.BarBeat.bar
            '    Next
            'End If
        End Sub
        Public Function BarBeat_BeatOnly(bar As UInteger, beat As Single) As Single
            Return BarBeat_BeatOnly(bar, beat, _CPBList)
        End Function
        Public Function BarBeat_Time(bar As UInteger, beat As Single) As TimeSpan
            Return BeatOnly_Time(BarBeat_BeatOnly(bar, beat))
        End Function
        Public Function BeatOnly_BarBeat(beat As Single) As (bar As UInteger, beat As Single)
            Return BeatOnly_BarBeat(beat, _CPBList)
        End Function
        Public Function BeatOnly_Time(beatOnly As Single) As TimeSpan
            Return BeatOnly_Time(beatOnly, _BPMList)
        End Function
        Public Function Time_BeatOnly(timeSpan As TimeSpan) As Single
            Return Time_BeatOnly(timeSpan, _BPMList)
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
            Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Beat.BarBeat.bar < bar)
            If LastCPB IsNot Nothing Then
                foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar)
            End If
            Dim result = foreCPB.BeatOnly + (bar - foreCPB.Bar) * foreCPB.CPB + beat - 1
            Return result
        End Function
        Public Shared Function BeatOnly_BarBeat(beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As (bar As UInteger, beat As Single)
            Dim foreCPB As (BeatOnly As Single, Bar As UInteger, CPB As UInteger) = (1, 1, 8)
            Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Beat.BeatOnly < beat)
            If LastCPB IsNot Nothing Then
                foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar)
            End If
            Dim result = (CUInt(foreCPB.Bar + Math.Floor((beat - foreCPB.BeatOnly) / foreCPB.CPB)),
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
            resultMinute += (beatOnly - fore.BeatOnly) / fore.BPM
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
).TotalMinutes * fore.BPM
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
        Public Function BeatsPerMinuteOf(beat As RDBeat) As Single
            Return If(_BPMList.LastOrDefault(Function(i) i.Beat < beat)?.BeatsPerMinute, 100)
        End Function
        Public Function CrotchetsPerBarOf(beat As RDBeat) As Single
            Return If(_CPBList.LastOrDefault(Function(i) i.Beat < beat)?.CrotchetsPerBar, 8)
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
            If TypesToEnum Is Nothing Then
                Dim result As EventType
                If [Enum].TryParse(type.Name, result) Then
                    Return result
                End If
                Throw New IllegalEventTypeException(type, "Unable to find a matching EventType.")
            Else
                Try
                    Return TypesToEnum(type).Single
                Catch ex As Exception
                    Throw New IllegalEventTypeException(type, "Multiple matching EventTypes were found. Please check if the type is an abstract class type.", New ArgumentException("Multiple matching EventTypes were found. Please check if the type is an abstract class type.", NameOf(type)))
                End Try
            End If
        End Function
        Public Function ConvertToEnum(Of T As {BaseEvent, New})() As EventType
            Return ConvertToEnum(GetType(T))
        End Function
        Public Function ConvertToEnums(Of T As BaseEvent)() As EventType()
            Try
                Return TypesToEnum(GetType(T))
            Catch ex As Exception
                Throw New IllegalEventTypeException(GetType(T), "This exception is not expected. Please contact the developer to handle this exception.")
            End Try
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
            If EnumToType Is Nothing Then
                Dim result = System.Type.GetType($"{GetType(BaseEvent).Namespace}.{type}")
                If result Is Nothing Then
                    Throw New RhythmBaseException($"Illegal Type: {type}.")
                End If
                Return result
            Else
                Try
                    Return EnumToType(type)
                Catch ex As Exception
                    Throw New IllegalEventTypeException(type, "This value does not exist in the EventType enumeration.")
                End Try
            End If
        End Function
    End Module
    Public Class TranaslationManager
        Private ReadOnly jsonpath As IO.FileInfo
        Private ReadOnly values As JObject
        Public Sub New(filepath As IO.FileInfo)
            jsonpath = filepath
            If jsonpath.Exists Then
            Else
                jsonpath.Directory.Create()
                Using stream = New IO.StreamWriter(jsonpath.Create())
                    stream.Write("{}")
                End Using
            End If

            Using Stream = New IO.StreamReader(jsonpath.OpenRead)
                values = JsonConvert.DeserializeObject(Stream.ReadToEnd)
            End Using
        End Sub
        Public Function GetValue(p As MemberInfo, value As String) As String
            Dim current As JObject = values
            Dim keys = GetPath(p)

            For i = 0 To keys.Length - 2
                Dim j As JToken = Nothing
                If Not current.TryGetValue(keys(i), j) Then
                    current(keys(i)) = New JObject
                    current = current(keys(i))
                Else
                    current = j
                End If
            Next
            If Not current.ContainsKey(keys.Last) OrElse current(keys.Last) Is Nothing Then
                current(keys.Last) = value
                Save()
                Return value
            Else
                Return current(keys.Last).ToString
            End If
        End Function
        Public Function GetValue(p As MemberInfo)
            Return GetValue(p, GetPath(p).Last)
        End Function
        Private Shared Function GetPath(p As MemberInfo) As String()
            Return {p.DeclaringType.Namespace, p.DeclaringType.Name, p.Name}
        End Function
        Private Sub Save()
            Using Stream As New IO.StreamWriter(jsonpath.OpenWrite)
                Stream.Write(values.ToString)
            End Using
        End Sub
    End Class
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

End Namespace