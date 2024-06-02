Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports RhythmBase.Events
Imports RhythmBase.Extensions
Imports RhythmBase.LevelElements
Imports RhythmBase.Components
Imports RhythmBase.Expressions
Imports System.Runtime.InteropServices.JavaScript.JSType
'' <summary>
'' 工具类
'' </summary>
Namespace Utils
	Public Module [Global]
		Public ReadOnly RowTypes As IEnumerable(Of EventType) = ConvertToEnums(Of BaseRowAction)()
		Public ReadOnly DecorationTypes As IEnumerable(Of EventType) = ConvertToEnums(Of BaseDecorationAction)()
	End Module
	Public Class BeatCalculator
		Private ReadOnly Collection As RDLevel
		Public Sub New(CPBCollection As IEnumerable(Of SetCrotchetsPerBar), BPMCollection As IEnumerable(Of BaseBeatsPerMinute))
			Collection = New RDLevel
			Collection.AddRange(CPBCollection)
			Collection.AddRange(BPMCollection)
			Initialize()
		End Sub
		Public Sub New(level As RDLevel)
			Collection = level
			Initialize()
		End Sub
		Public Shared Sub Initialize(CPBs As IEnumerable(Of SetCrotchetsPerBar))
			For Each item In CPBs
				item.Bar = item.Beat.BarBeat.bar
			Next
		End Sub
		Public Sub Initialize()
			Dim l = Collection.Where(Of SetCrotchetsPerBar)
			If l.Any Then
				l(0).Bar = l(0).Beat.BarBeat.bar
				Dim CPB = l(0).CrotchetsPerBar
				For i = 1 To l.Count - 1
					l(i).Bar = l(i).Beat.BarBeat.bar
				Next
			End If
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
				foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar)
			End If
			Dim result = foreCPB.BeatOnly + (bar - foreCPB.Bar) * foreCPB.CPB + beat - 1
			Return result
		End Function
		Public Shared Function BeatOnly_BarBeat(beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As (bar As UInteger, beat As Single)
			Dim foreCPB As (BeatOnly As Single, Bar As UInteger, CPB As UInteger) = (1, 1, 8)
			Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Beat < beat)
			If LastCPB IsNot Nothing Then
				foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar)
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
		Public Function BeatsPerMinuteOf(beat As RDBeat) As Single
			Return If(Collection.LastOrDefault(Of BaseBeatsPerMinute)(Function(i) i.Beat < beat)?.BeatsPerMinute, 100)
		End Function
		Public Function CrotchetsPerBarOf(beat As RDBeat) As Single
			Return If(Collection.LastOrDefault(Of SetCrotchetsPerBar)(Function(i) i.Beat < beat)?.CrotchetsPerBar, 8)
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
		Private Function GetRange(e As OrderedEventCollection, index As Index) As (start As Single, [end] As Single)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return If(index.IsFromEnd, (
				lastEvent.ParentLevel.Calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value, 1),
				lastEvent.ParentLevel.Calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value + 1, 1)),
				(firstEvent.ParentLevel.Calculator.BarBeat_BeatOnly(index.Value, 1),
				firstEvent.ParentLevel.Calculator.BarBeat_BeatOnly(index.Value + 1, 1)))
		End Function
		Private Function GetRange(e As OrderedEventCollection, range As Range) As (start As Single, [end] As Single)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return (If(range.Start.IsFromEnd,
				lastEvent.ParentLevel.Calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - range.Start.Value, 1),
				firstEvent.ParentLevel.Calculator.BarBeat_BeatOnly(range.Start.Value, 1)),
				If(range.End.IsFromEnd,
				lastEvent.ParentLevel.Calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - range.End.Value + 1, 1),
				firstEvent.ParentLevel.Calculator.BarBeat_BeatOnly(range.End.Value + 1, 1)))
		End Function
		<Extension> Public Function UpperCamelCase(e As String) As String
			Dim S = e.ToArray
			S(0) = S(0).ToString.ToUpper
			Return String.Join("", S)
		End Function
		<Extension> Public Function LowerCamelCase(e As String) As String
			Dim S = e.ToArray
			S(0) = S(0).ToString.ToLower
			Return String.Join("", S)
		End Function
		<Extension> Public Function RgbaToArgb(Rgba As Int32) As Int32
			Return ((Rgba >> 8) And &HFFFFFF) Or ((Rgba << 24) And &HFF000000)
		End Function
		<Extension> Public Function ArgbToRgba(Argb As Int32) As Int32
			Return ((Argb >> 24) And &HFF) Or ((Argb << 8) And &HFFFFFF00)
		End Function
		<Extension> Public Function FixFraction(number As Single, splitBase As UInteger) As Single
			Return Math.Round(number * splitBase) / splitBase
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.EventsBeatOrder.SelectMany(Function(i) i.Value).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), beat As Single) As IEnumerable(Of T)
			Dim value As List(Of BaseEvent) = Nothing
			If e.EventsBeatOrder.TryGetValue(beat, value) Then
				Return value
			End If
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), startBeat As Single, endBeat As Single) As IEnumerable(Of T)
			Return e.EventsBeatOrder.TakeWhile(Function(i) i.Key < endBeat).SkipWhile(Function(i) i.Key < startBeat).SelectMany(Function(i) i.Value)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), beat As RDBeat) As IEnumerable(Of T)
			Dim value As List(Of BaseEvent) = Nothing
			If e.EventsBeatOrder.TryGetValue(beat.BeatOnly, value) Then
				Return value
			End If
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.Where(startBeat.BeatOnly, endBeat.BeatOnly)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), index As Index) As IEnumerable(Of T)
			Dim rg = GetRange(e, index)
			Return e.Where(rg.start, rg.end)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), range As RDRange) As IEnumerable(Of T)
			Return e.EventsBeatOrder.TakeWhile(Function(i) If(i.Key < range.End?.BeatOnly, True)).SkipWhile(Function(i) If(i.Key < range.Start?.BeatOnly, False)).SelectMany(Function(i) i.Value)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), range As Range) As IEnumerable(Of T)
			Dim rg = GetRange(e, range)
			Return e.Where(rg.start, rg.end)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As Single) As IEnumerable(Of T)
			Return e.Where(beat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), startBeat As Single, endBeat As Single) As IEnumerable(Of T)
			Return e.Where(startBeat, endBeat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.Where(beat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.Where(startBeat, endBeat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As RDRange) As IEnumerable(Of T)
			Return e.Where(range).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), index As Index) As IEnumerable(Of T)
			Return e.Where(index).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As Range) As IEnumerable(Of T)
			Return e.Where(range).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection) As IEnumerable(Of T)
			Return e.OfType(Of T)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, beat As Single) As IEnumerable(Of T)
			Dim value As List(Of BaseEvent) = Nothing
			If e.EventsBeatOrder.TryGetValue(beat, value) Then
				Return value.OfType(Of T)
			End If
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, startBeat As Single, endBeat As Single) As IEnumerable(Of T)
			Return e.EventsBeatOrder.TakeWhile(Function(i) i.Key < endBeat).SkipWhile(Function(i) i.Key < startBeat).SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, beat As RDBeat) As IEnumerable(Of T)
			Dim value As List(Of BaseEvent) = Nothing
			If e.EventsBeatOrder.TryGetValue(beat.BeatOnly, value) Then
				Return value.OfType(Of T)
			End If
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.Where(Of T)(startBeat.BeatOnly, endBeat.BeatOnly)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, index As Index) As IEnumerable(Of T)
			Dim rg = GetRange(e, index)
			Return e.Where(Of T)(rg.start, rg.end)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, range As RDRange) As IEnumerable(Of T)
			Return e.EventsBeatOrder.TakeWhile(Function(i) If(i.Key < range.End?.BeatOnly, True)).SkipWhile(Function(i) If(i.Key < range.Start?.BeatOnly, False)).SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, range As Range) As IEnumerable(Of T)
			Dim rg = GetRange(e, range)
			Return e.Where(Of T)(rg.start, rg.end)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.Where(Of T)().Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As Single) As IEnumerable(Of T)
			Return e.Where(Of T)(beat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), startBeat As Single, endBeat As Single) As IEnumerable(Of T)
			Return e.Where(Of T)(startBeat, endBeat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.Where(Of T)(beat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.Where(Of T)(startBeat, endBeat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), range As RDRange) As IEnumerable(Of T)
			Return e.Where(Of T)(range).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), index As Index) As IEnumerable(Of T)
			Return e.Where(Of T)(index).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), range As Range) As IEnumerable(Of T)
			Return e.Where(Of T)(range).Where(predicate)
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), beat As Single) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), startBeat As Single, endBeat As Single) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), index As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(index)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), range As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As Single) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), startBeat As Single, endBeat As Single) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), index As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, index)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)()))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, beat As Single) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, startBeat As Single, endBeat As Single) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, index As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(index)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, range As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As Single) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), startBeat As Single, endBeat As Single) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), index As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, index)))
		End Function
		<Extension> Public Function RemoveAll(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), range As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		<Extension> Public Function First(Of T As BaseEvent)(e As OrderedEventCollection(Of T)) As T
			Return e.EventsBeatOrder.First.Value.First
		End Function
		<Extension> Public Function First(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.First(predicate)
		End Function
		<Extension> Public Function First(Of T As BaseEvent)(e As OrderedEventCollection) As T
			Return e.Where(Of T).First
		End Function
		<Extension> Public Function First(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).First(predicate)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As BaseEvent)(e As OrderedEventCollection(Of T)) As T
			Return e.EventsBeatOrder.FirstOrDefault.Value?.FirstOrDefault
		End Function
		<Extension> Public Function FirstOrDefault(Of T As BaseEvent)(e As OrderedEventCollection(Of T), defaultValue As T) As T
			Return e.ConcatAll.FirstOrDefault(defaultValue)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.FirstOrDefault(predicate)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.ConcatAll.FirstOrDefault(predicate, defaultValue)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As BaseEvent)(e As OrderedEventCollection) As T
			Return e.Where(Of T).FirstOrDefault()
		End Function
		<Extension> Public Function FirstOrDefault(Of T As BaseEvent)(e As OrderedEventCollection, defaultValue As T) As T
			Return e.Where(Of T).FirstOrDefault(defaultValue)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).FirstOrDefault(predicate)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.Where(Of T).FirstOrDefault(predicate, defaultValue)
		End Function
		<Extension> Public Function Last(Of T As BaseEvent)(e As OrderedEventCollection(Of T)) As T
			Return e.EventsBeatOrder.Last.Value.Last
		End Function
		<Extension> Public Function Last(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.Last(predicate)
		End Function
		<Extension> Public Function Last(Of T As BaseEvent)(e As OrderedEventCollection) As T
			Return e.Where(Of T).Last
		End Function
		<Extension> Public Function Last(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).Last(predicate)
		End Function
		<Extension> Public Function LastOrDefault(Of T As BaseEvent)(e As OrderedEventCollection(Of T)) As T
			Return e.EventsBeatOrder.LastOrDefault.Value?.LastOrDefault()
		End Function
		<Extension> Public Function LastOrDefault(Of T As BaseEvent)(e As OrderedEventCollection(Of T), defaultValue As T) As T
			Return e.ConcatAll.LastOrDefault(defaultValue)
		End Function
		<Extension> Public Function LastOrDefault(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.LastOrDefault(predicate)
		End Function
		<Extension> Public Function LastOrDefault(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.ConcatAll.LastOrDefault(predicate, defaultValue)
		End Function
		<Extension> Public Function LastOrDefault(Of T As BaseEvent)(e As OrderedEventCollection) As T
			Return e.Where(Of T).LastOrDefault()
		End Function
		<Extension> Public Function LastOrDefault(Of T As BaseEvent)(e As OrderedEventCollection, defaultValue As T) As T
			Return e.Where(Of T).LastOrDefault(defaultValue)
		End Function
		<Extension> Public Function LastOrDefault(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).LastOrDefault(predicate)
		End Function
		<Extension> Public Function LastOrDefault(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.Where(Of T).LastOrDefault(predicate, defaultValue)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), beat As Single) As IEnumerable(Of T)
			Return e.EventsBeatOrder.TakeWhile(Function(i) i.Key < beat).SelectMany(Function(i) i.Value)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(beat.BeatOnly)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), index As Index) As IEnumerable(Of T)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return e.TakeWhile(
If(index.IsFromEnd,
lastEvent.ParentLevel.Calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value + 1, 1),
firstEvent.ParentLevel.Calculator.BarBeat_BeatOnly(index.Value + 1, 1)))
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.EventsBeatOrder.SelectMany(Function(i) i.Value).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As Single) As IEnumerable(Of T)
			Return e.TakeWhile(beat).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(beat).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), index As Index) As IEnumerable(Of T)
			Return e.TakeWhile(index).TakeWhile(predicate)
		End Function
		<Extension> Public  Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, beat As Single) As IEnumerable(Of T)
			Return e.EventsBeatOrder.TakeWhile(Function(i) i.Key < beat).SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(beat.BeatOnly)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, index As Index) As IEnumerable(Of T)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return e.TakeWhile(Of T)(
If(index.IsFromEnd,
lastEvent.ParentLevel.Calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value + 1, 1),
firstEvent.ParentLevel.Calculator.BarBeat_BeatOnly(index.Value + 1, 1)))
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.EventsBeatOrder.SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As Single) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(beat).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(beat).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), index As Index) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(index).TakeWhile(predicate)
		End Function
		<Extension> Public Function RemoveRange(Of T As BaseEvent)(e As OrderedEventCollection, items As IEnumerable(Of T)) As Integer
			Dim count As Integer = 0
			For Each item In items
				count += e.Remove(item)
			Next
			Return count
		End Function
		<Extension> Public Function RemoveRange(Of T As BaseEvent)(e As OrderedEventCollection(Of T), items As IEnumerable(Of T)) As Integer
			Dim count As Integer = 0
			For Each item In items
				count += e.Remove(item)
			Next
			Return count
		End Function
		<Extension> Public Function IsInFrontOf(Of T As BaseEvent)(e As OrderedEventCollection(Of T), item1 As T, item2 As T) As Boolean
			Return item1.ParentLevel Is e AndAlso item2.ParentLevel Is e AndAlso
item1.Beat < item2.Beat OrElse
(item1.Beat.BeatOnly = item2.Beat.BeatOnly AndAlso
e.EventsBeatOrder(item1.Beat.BeatOnly).IndexOf(item1) < e.EventsBeatOrder(item2.Beat.BeatOnly).IndexOf(item2))
		End Function
		<Extension> Public Function IsBehind(Of T As BaseEvent)(e As OrderedEventCollection(Of T), item1 As T, item2 As T) As Boolean
			Return item1.ParentLevel Is e AndAlso item2.ParentLevel Is e AndAlso
item1.Beat > item2.Beat OrElse
(item1.Beat.BeatOnly = item2.Beat.BeatOnly AndAlso
e.EventsBeatOrder(item1.Beat.BeatOnly).IndexOf(item1) > e.EventsBeatOrder(item2.Beat.BeatOnly).IndexOf(item2))
		End Function
		<Extension> Public Function GetHitBeat(e As RDLevel) As IEnumerable(Of Hit)
			Dim L As New List(Of Hit)
			For Each item In e.Rows
				L.AddRange(item.HitBeats)
			Next
			Return L
		End Function
		<Extension> Public Function GetHitEvents(e As RDLevel) As IEnumerable(Of BaseBeat)
			Return e.Where(Of BaseBeat)(Function(i) i.Hitable)
		End Function
		<Extension> Public Function GetTaggedEvents(Of T As BaseEvent)(e As OrderedEventCollection(Of T), name As String, direct As Boolean) As IEnumerable(Of IGrouping(Of String, T))
			If name Is Nothing Then
				Return Nothing
			End If
			If direct Then
				Return e.Where(Function(i) i.Tag = name).GroupBy(Function(i) i.Tag)
			Else
				Return e.Where(Function(i) If(i.Tag?.Contains(name), False)).GroupBy(Function(i) i.Tag)
			End If
		End Function
	End Module
	Public Module EventsExtension
		<Extension> Public Function IsInFrontOf(e As BaseEvent, item As BaseEvent) As Boolean
			Return e.ParentLevel Is item.ParentLevel AndAlso e.ParentLevel.IsInFrontOf(e, item)
		End Function
		<Extension> Public Function IsBehind(e As BaseEvent, item As BaseEvent) As Boolean
			Return e.ParentLevel Is item.ParentLevel AndAlso e.ParentLevel.IsBehind(e, item)
		End Function
		<Extension>
		Public Sub MovePositionMaintainVisual(e As Move, target As RDPointTemp)
			If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
				Exit Sub
			End If
			e.Position = target
			e.Pivot = (e.VisualPosition() - target).Rotate(-e.Angle.TryGetValue())
		End Sub
		<Extension>
		Public Sub MovePositionMaintainVisual(e As MoveRoom, target As RDPointTemp)
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
				Exit Sub
			End If
			e.RoomPosition = target
			e.Pivot = (e.VisualPosition() - target).Rotate(-e.Angle.TryGetValue())
		End Sub
		<Extension>
		Public Function VisualPosition(e As Move) As RDPointTemp
			If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse e.Scale Is Nothing Then
				Return New RDPointTemp
			End If
			Dim previousPosition As RDPointTemp = e.Position
			Dim previousPivot As RDPointTemp = (CType(e.Pivot, RDPointTemp) * e.Scale * e.Parent.Size) / (100, 100)
			Return previousPosition + previousPivot.Rotate(e.Angle.TryGetValue())
		End Function
		<Extension>
		Public Function VisualPosition(e As MoveRoom) As RDPointTemp
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
				Return New RDPointTemp
			End If
			Dim previousPosition As RDPointTemp = e.RoomPosition
			Dim previousPivot As RDPointTemp = CType(e.Pivot, RDPointTemp) * e.Scale
			Return previousPosition + previousPivot.Rotate(e.Angle.TryGetValue())
		End Function
		<Extension>
		Public Function GetExpValue(e As ExpTemp, variables As Variables) As Object
			Dim compiledFunction = GetFunctionalExpression(Of Single)(e.value)
			Return compiledFunction(variables)
		End Function
		<Extension>
		Public Function GetValue(e As INumOrExp, variables As Variables) As Object
			If e.GetType = GetType(Num) Then
				Return CType(e, Num).value
			Else
				Return CType(e, ExpTemp).GetExpValue(variables)
			End If
		End Function
	End Module
End Namespace