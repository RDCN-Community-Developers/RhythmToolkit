Imports System.Runtime.CompilerServices
Imports NAudio.CoreAudioApi
Namespace Extensions
	Public Module Extension
		Private Function GetRange(e As OrderedEventCollection, index As Index) As (start As Single, [end] As Single)
			Try
				Dim firstEvent = e.First
				Dim lastEvent = e.Last
				Return If(index.IsFromEnd, (
lastEvent.Beat._calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value, 1),
lastEvent.Beat._calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value + 1, 1)),
(firstEvent.Beat._calculator.BarBeat_BeatOnly(index.Value, 1),
firstEvent.Beat._calculator.BarBeat_BeatOnly(index.Value + 1, 1)))
			Catch ex As Exception
				Throw New ArgumentOutOfRangeException(NameOf(index))
			End Try
		End Function
		Private Function GetRange(e As OrderedEventCollection, range As Range) As (start As Single, [end] As Single)
			Try
				Dim firstEvent = e.First
				Dim lastEvent = e.Last
				Return (If(range.Start.IsFromEnd,
lastEvent.Beat._calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - range.Start.Value, 1),
firstEvent.Beat._calculator.BarBeat_BeatOnly(Math.Max(range.Start.Value, 1), 1)),
If(range.End.IsFromEnd,
lastEvent.Beat._calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - range.End.Value + 1, 1),
firstEvent.Beat._calculator.BarBeat_BeatOnly(range.End.Value + 1, 1)))
			Catch ex As Exception
				Throw New ArgumentOutOfRangeException(NameOf(range))
			End Try
		End Function
		<Extension> Public Function NullableEquals(e As Single?, obj As Single?) As Boolean
			Return (e.HasValue And obj.HasValue AndAlso e.Value = obj.Value) OrElse (Not e.HasValue AndAlso Not obj.HasValue)
		End Function
		<Extension> Public Function IsNullOrEmpty(e As String) As Boolean
			Return e Is Nothing OrElse e.Length = 0
		End Function
		<Extension> Public Function ToUpperCamelCase(e As String) As String
			Dim S = e.ToArray
			S(0) = S(0).ToString.ToUpper
			Return String.Join("", S)
		End Function
		<Extension> Friend Sub ToUpperCamelCase(e As Newtonsoft.Json.Linq.JObject, key As String)
			Dim token = e(key)
			e.Remove(key)
			e(key.ToUpperCamelCase) = token
		End Sub
		<Extension> Friend Sub ToUpperCamelCase(e As Newtonsoft.Json.Linq.JObject)
			For Each pair In e.DeepClone.ToObject(Of Newtonsoft.Json.Linq.JObject)
				e.ToUpperCamelCase(pair.Key)
			Next
		End Sub
		<Extension> Public Function ToLowerCamelCase(e As String) As String
			Dim S = e.ToArray
			S(0) = S(0).ToString.ToLower
			Return String.Join("", S)
		End Function
		<Extension> Friend Sub ToLowerCamelCase(e As Newtonsoft.Json.Linq.JObject, key As String)
			Dim token = e(key)
			e.Remove(key)
			e(key.ToLowerCamelCase) = token
		End Sub
		<Extension> Friend Sub ToLowerCamelCase(e As Newtonsoft.Json.Linq.JObject)
			For Each pair In e.DeepClone.ToObject(Of Newtonsoft.Json.Linq.JObject)
				e.ToLowerCamelCase(pair.Key)
			Next
		End Sub
		<Extension> Public Function RgbaToArgb(Rgba As Int32) As Int32
			Return ((Rgba >> 8) And &HFFFFFF) Or ((Rgba << 24) And &HFF000000)
		End Function
		<Extension> Public Function ArgbToRgba(Argb As Int32) As Int32
			Return ((Argb >> 24) And &HFF) Or ((Argb << 8) And &HFFFFFF00)
		End Function
		<Extension> Public Function ToRDPoint(e As SkiaSharp.SKPoint) As RDPointN
			Return New RDPointN(e.X, e.Y)
		End Function
		<Extension> Public Function ToRDPointI(e As SkiaSharp.SKPointI) As RDPointNI
			Return New RDPointNI(e.X, e.Y)
		End Function
		<Extension> Public Function ToRDSize(e As SkiaSharp.SKSize) As RDSizeN
			Return New RDSizeN(e.Width, e.Height)
		End Function
		<Extension> Public Function ToRDSizeI(e As SkiaSharp.SKSizeI) As RDSizeNI
			Return New RDSizeNI(e.Width, e.Height)
		End Function
		<Extension> Public Function ToSKPoint(e As RDPointN) As SkiaSharp.SKPoint
			Return New SkiaSharp.SKPoint(e.X, e.Y)
		End Function
		<Extension> Public Function ToSKPointI(e As RDPointNI) As SkiaSharp.SKPointI
			Return New SkiaSharp.SKPointI(e.X, e.Y)
		End Function
		<Extension> Public Function ToSKSize(e As RDSizeN) As SkiaSharp.SKSize
			Return New SkiaSharp.SKSize(e.Width, e.Height)
		End Function
		<Extension> Public Function ToSKSizeI(e As RDSizeNI) As SkiaSharp.SKSizeI
			Return New SkiaSharp.SKSizeI(e.Width, e.Height)
		End Function
		<Extension> Public Function FixFraction(number As Single, splitBase As UInteger) As Single
			Return Math.Round(number * splitBase) / splitBase
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.EventsBeatOrder.SelectMany(Function(i) i.Value.list).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), beat As RDBeat) As IEnumerable(Of T)
			Dim value As TypedList(Of RDBaseEvent) = Nothing
			If e.EventsBeatOrder.TryGetValue(beat, value) Then
				Return value.list
			End If
			Return value
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.EventsBeatOrder _
.TakeWhile(Function(i) i.Key < endBeat) _
.SkipWhile(Function(i) i.Key < startBeat) _
.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), index As Index) As IEnumerable(Of T)
			Dim rg = GetRange(e, index)
			Return e.EventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), range As RDRange) As IEnumerable(Of T)
			Return e.EventsBeatOrder _
.TakeWhile(Function(i) If(i.Key < range.End, True)) _
.SkipWhile(Function(i) If(i.Key < range.Start, False)) _
.SelectMany(Function(i) i.Value.list)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), range As Range) As IEnumerable(Of T)
			Dim rg = GetRange(e, range)
			Return e.EventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As Single) As IEnumerable(Of T)
			Return e.Where(beat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.Where(beat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.Where(startBeat, endBeat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As RDRange) As IEnumerable(Of T)
			Return e.Where(range).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), index As Index) As IEnumerable(Of T)
			Return e.Where(index).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As Range) As IEnumerable(Of T)
			Return e.Where(range).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection) As IEnumerable(Of T)
			Dim enums = ConvertToRDEnums(Of T)()
			Return e.EventsBeatOrder _
.Where(Function(i) i.Value._types _
.Any(Function(j) enums.Contains(j))) _
.SelectMany(Function(i) i.Value.list).OfType(Of T)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, beat As RDBeat) As IEnumerable(Of T)
			Dim value As TypedList(Of RDBaseEvent) = Nothing
			If e.EventsBeatOrder.TryGetValue(beat, value) Then
				Return value.list.OfType(Of T)
			End If
			Return value
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.EventsBeatOrder _
.TakeWhile(Function(i) i.Key < endBeat) _
.SkipWhile(Function(i) i.Key < startBeat) _
.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, index As Index) As IEnumerable(Of T)
			Dim rg = GetRange(e, index)
			Return e.EventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, range As RDRange) As IEnumerable(Of T)
			Return e.EventsBeatOrder _
.TakeWhile(Function(i) If(i.Key < range.End, True)) _
.SkipWhile(Function(i) If(i.Key < range.Start, False)) _
.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, range As Range) As IEnumerable(Of T)
			Dim rg = GetRange(e, range)
			Return e.EventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.Where(Of T)().Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.Where(Of T)(beat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.Where(Of T)(startBeat, endBeat).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), range As RDRange) As IEnumerable(Of T)
			Return e.Where(Of T)(range).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), index As Index) As IEnumerable(Of T)
			Return e.Where(Of T)(index).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), range As Range) As IEnumerable(Of T)
			Return e.Where(Of T)(range).Where(predicate)
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), beat As Single) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), index As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(index)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), range As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), index As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, index)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)()))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, index As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(index)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, range As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, beat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, startBeat, endBeat)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), index As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, index)))
		End Function
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), range As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		<Extension> Public Function First(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T)) As T
			Return e.EventsBeatOrder.First.Value.list.First
		End Function
		<Extension> Public Function First(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.First(predicate)
		End Function
		<Extension> Public Function First(Of T As RDBaseEvent)(e As OrderedEventCollection) As T
			Return e.Where(Of T).First
		End Function
		<Extension> Public Function First(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).First(predicate)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T)) As T
			Return e.EventsBeatOrder.FirstOrDefault.Value?.list.FirstOrDefault
		End Function
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), defaultValue As T) As T
			Return e.ConcatAll.FirstOrDefault(defaultValue)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.FirstOrDefault(predicate)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.ConcatAll.FirstOrDefault(predicate, defaultValue)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection) As T
			Return e.Where(Of T).FirstOrDefault()
		End Function
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection, defaultValue As T) As T
			Return e.Where(Of T).FirstOrDefault(defaultValue)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).FirstOrDefault(predicate)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.Where(Of T).FirstOrDefault(predicate, defaultValue)
		End Function
		<Extension> Public Function Last(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T)) As T
			Return e.EventsBeatOrder.Last.Value.list.Last
		End Function
		<Extension> Public Function Last(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.Last(predicate)
		End Function
		<Extension> Public Function Last(Of T As RDBaseEvent)(e As OrderedEventCollection) As T
			Return e.Where(Of T).Last
		End Function
		<Extension> Public Function Last(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).Last(predicate)
		End Function
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T)) As T
			Return e.EventsBeatOrder.LastOrDefault.Value?.list.LastOrDefault()
		End Function
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), defaultValue As T) As T
			Return e.ConcatAll.LastOrDefault(defaultValue)
		End Function
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.LastOrDefault(predicate)
		End Function
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.ConcatAll.LastOrDefault(predicate, defaultValue)
		End Function
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection) As T
			Return e.Where(Of T).LastOrDefault()
		End Function
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection, defaultValue As T) As T
			Return e.Where(Of T).LastOrDefault(defaultValue)
		End Function
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).LastOrDefault(predicate)
		End Function
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.Where(Of T).LastOrDefault(predicate, defaultValue)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(beat.BeatOnly)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), index As Index) As IEnumerable(Of T)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return e.TakeWhile(
If(index.IsFromEnd,
lastEvent.Beat._calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value + 1, 1),
firstEvent.Beat._calculator.BarBeat_BeatOnly(index.Value + 1, 1)))
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.EventsBeatOrder.SelectMany(Function(i) i.Value.list).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As Single) As IEnumerable(Of T)
			Return e.TakeWhile(beat).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(beat).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), index As Index) As IEnumerable(Of T)
			Return e.TakeWhile(index).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection, beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(beat.BeatOnly)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection, index As Index) As IEnumerable(Of T)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return e.TakeWhile(Of T)(
If(index.IsFromEnd,
lastEvent.Beat._calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value + 1, 1),
firstEvent.Beat._calculator.BarBeat_BeatOnly(index.Value + 1, 1)))
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.EventsBeatOrder.SelectMany(Function(i) i.Value.list.OfType(Of T)).Where(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As Single) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(beat).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(beat).TakeWhile(predicate)
		End Function
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean), index As Index) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(index).TakeWhile(predicate)
		End Function
		<Extension> Public Function RemoveRange(Of T As RDBaseEvent)(e As OrderedEventCollection, items As IEnumerable(Of T)) As Integer
			Dim count As Integer = 0
			For Each item In items
				count += e.Remove(item)
			Next
			Return count
		End Function
		<Extension> Public Function RemoveRange(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), items As IEnumerable(Of T)) As Integer
			Dim count As Integer = 0
			For Each item In items
				count += e.Remove(item)
			Next
			Return count
		End Function
		<Extension> Public Function IsInFrontOf(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), item1 As T, item2 As T) As Boolean
			Return item1.Beat < item2.Beat OrElse
(item1.Beat.BeatOnly = item2.Beat.BeatOnly AndAlso
e.EventsBeatOrder(item1.Beat).list.IndexOf(item1) < e.EventsBeatOrder(item2.Beat).list.IndexOf(item2))
		End Function
		<Extension> Public Function IsBehind(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), item1 As T, item2 As T) As Boolean
			Dim s = item1.Beat.Equals(CObj(item2.Beat))
			Return item1.Beat > item2.Beat OrElse
(item1.Beat.BeatOnly = item2.Beat.BeatOnly AndAlso
e.EventsBeatOrder(item1.Beat).list.IndexOf(item1) > e.EventsBeatOrder(item2.Beat).list.IndexOf(item2))
		End Function
		<Extension> Public Function GetHitBeat(e As RDLevel) As IEnumerable(Of Hit)
			Dim L As New List(Of Hit)
			For Each item In e.Rows
				L.AddRange(item.HitBeats)
			Next
			Return L
		End Function
		<Extension> Public Function GetHitEvents(e As RDLevel) As IEnumerable(Of RDBaseBeat)
			Return e.Where(Of RDBaseBeat)(Function(i) i.IsHitable)
		End Function
		<Extension> Public Function GetTaggedEvents(Of T As RDBaseEvent)(e As OrderedEventCollection(Of T), name As String, direct As Boolean) As IEnumerable(Of IGrouping(Of String, T))
			If name Is Nothing Then
				Return Nothing
			End If
			If direct Then
				Return e.Where(Function(i) i.Tag = name).GroupBy(Function(i) i.Tag)
			Else
				Return e.Where(Function(i) If(i.Tag?.Contains(name), False)).GroupBy(Function(i) i.Tag)
			End If
		End Function
		<Extension> Public Function ControllingEvents(e As RDLevel, ParamArray tag As RDTagAction.SpecialTag()) As IEnumerable(Of IGrouping(Of String, RDBaseEvent))
			Return e.GetTaggedEvents("[" + tag.ToString + "]", False)
		End Function
		<Extension> Public Function ControllingEventsLeftAligned(e As RDLevel, ParamArray tag As RDTagAction.SpecialTag()) As IEnumerable(Of IGrouping(Of String, RDBaseEvent))
			Dim L = e.ControllingEvents(tag)
			For Each pair In L
				Dim start = pair(0).Beat
				For Each item In pair
					item.Beat -= start.BeatOnly
				Next
			Next
			Return L
		End Function
		<Extension> Private Function ClassicBeats(e As RDRow) As IEnumerable(Of RDBaseBeat)
			Return e.Where(Of RDBaseBeat)(Function(i) i.Type = RDEventType.AddClassicBeat Or
				i.Type = RDEventType.AddFreeTimeBeat Or
				i.Type = RDEventType.PulseFreeTimeBeat)
		End Function
		<Extension> Private Function OneshotBeats(e As RDRow) As IEnumerable(Of RDBaseBeat)
			Return e.Where(Of RDBaseBeat)(Function(i) i.Type = RDEventType.AddOneshotBeat)
		End Function
		<Extension> Public Function HitBeats(e As RDRow) As IEnumerable(Of Hit)
			Select Case e.RowType
				Case RDRowType.Classic
					Return e.ClassicBeats().SelectMany(Function(i) i.HitTimes)
				Case RDRowType.Oneshot
					Return e.OneshotBeats().SelectMany(Function(i) i.HitTimes)
				Case Else
					Throw New RhythmBaseException("How?")
			End Select
		End Function
		<Extension> Public Function BeatOf(e As RDLevel, beatOnly As Single) As RDBeat
			Return e.Calculator.BeatOf(beatOnly)
		End Function
		<Extension> Public Function BeatOf(e As RDLevel, bar As UInteger, beat As Single) As RDBeat
			Return e.Calculator.BeatOf(bar, beat)
		End Function
		<Extension> Public Function BeatOf(e As RDLevel, timeSpan As TimeSpan) As RDBeat
			Return e.Calculator.BeatOf(timeSpan)
		End Function
#If DEBUG Then
		<Extension> Public Function GetRowBeatStatus(e As RDRow) As SortedDictionary(Of Single, Integer())
			Dim L As New SortedDictionary(Of Single, Integer())
			Select Case e.RowType
				Case RDRowType.Classic
					Dim value As Integer() = New Integer(6) {}
					L.Add(0, value)
					For Each beat In e
						Select Case beat.Type
							Case RDEventType.AddClassicBeat
								Dim trueBeat = CType(beat, RDAddClassicBeat)
								For i = 0 To 6
									Dim statusArray As Integer() = If(L(beat.Beat.BeatOnly), New Integer(6) {})
									statusArray(i) += 1
									L(beat.Beat.BeatOnly) = statusArray
								Next
							Case RDEventType.AddFreeTimeBeat
							Case RDEventType.PulseFreeTimeBeat
							Case RDEventType.SetRowXs
						End Select
					Next
				Case RDRowType.Oneshot
				Case Else
					Throw New RhythmBaseException("How")
			End Select
			Return L
		End Function
#End If
		<Extension> Public Function Beats(e As RDRow) As IEnumerable(Of RDBaseBeat)
			Select Case e.RowType
				Case RDRowType.Classic
					Return e.ClassicBeats()
				Case RDRowType.Oneshot
					Return e.OneshotBeats()
				Case Else
					Throw New RhythmBaseException("How?")
			End Select
		End Function
	End Module
	Public Module EventsExtension
		Public Enum Wavetype
			BoomAndRush
			Spring
			Spike
			SpikeHuge
			Ball
			[Single]
		End Enum
		Public Enum ShockWaveType
			size
			distortion
			duration
		End Enum
		Public Enum Particle
			HitExplosion
			leveleventexplosion
		End Enum
		Public Structure ProceduralTree
			Public brachedPerlteration As Single?
			Public branchesPerDivision As Single?
			Public iterationsPerSecond As Single?
			Public thickness As Single?
			Public targetLength As Single?
			Public maxDeviation As Single?
			Public angle As Single?
			Public camAngle As Single?
			Public camDistance As Single?
			Public camDegreesPerSecond As Single?
			Public camSpeed As Single?
			Public pulseIntensity As Single?
			Public pulseRate As Single?
			Public pulseWavelength As Single?
		End Structure
		Private Function PropertyAssignment(propertyName As String, value As Boolean) As String
			Return $"{propertyName.ToLowerCamelCase} = {value}"
		End Function
		Private Function RoomPropertyAssignment(room As Byte, propertyName As String, value As Boolean) As String
			Return $"room[{room}].{propertyName.ToLowerCamelCase} = {value}"
		End Function
		Private Function FunctionCalling(functionName As String, ParamArray params() As Object) As String
			Return FunctionCalling(functionName, params.AsEnumerable)
		End Function
		Private Function FunctionCalling(functionName As String, params As IEnumerable(Of Object)) As String
			Dim out = functionName
			Dim paramStrings As New List(Of String)
			For Each param In params
				If param.GetType = GetType(String) OrElse param.GetType.IsEnum Then
					paramStrings.Add($"str:{param}")
				Else
					paramStrings.Add(param.ToString)
				End If
			Next
			Return $"{out}({String.Join(","c, paramStrings)})"
		End Function
		Private Function RoomFunctionCalling(room As Byte, functionName As String, ParamArray params() As Object) As String
			Return $"room[{room}].{FunctionCalling(functionName, params.AsEnumerable)}"
		End Function
		Private Function VfxFunctionCalling(functionName As String, ParamArray params() As Object) As String
			Return $"vfx.{FunctionCalling(functionName, params.AsEnumerable)}"
		End Function
		<Extension> Public Function IsInFrontOf(e As RDBaseEvent, item As RDBaseEvent) As Boolean
			Return e.Beat.baseLevel.IsInFrontOf(e, item)
		End Function
		<Extension> Public Function IsBehind(e As RDBaseEvent, item As RDBaseEvent) As Boolean
			Return e.Beat.baseLevel.IsBehind(e, item)
		End Function
		<Extension> Public Function Before(Of T As RDBaseEvent)(e As T) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(e.Beat.baseLevel.DefaultBeat, e.Beat)
		End Function
		<Extension> Public Function Before(Of T As RDBaseEvent)(e As RDBaseEvent) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(e.Beat.baseLevel.DefaultBeat, e.Beat)
		End Function
		<Extension> Public Function After(Of T As RDBaseEvent)(e As T) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(Function(i) i.Beat > e.Beat)
		End Function
		<Extension> Public Function After(Of T As RDBaseEvent)(e As RDBaseEvent) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(Function(i) i.Beat > e.Beat)
		End Function
		<Extension> Public Function Front(Of T As RDBaseEvent)(e As T) As T
			Return e.Before.Last
		End Function
		<Extension> Public Function Front(Of T As RDBaseEvent)(e As RDBaseEvent) As T
			Return e.Before(Of T).Last
		End Function
		<Extension> Public Function FrontOrDefault(Of T As RDBaseEvent)(e As T) As T
			Return e.Before.LastOrDefault
		End Function
		<Extension> Public Function FrontOrDefault(Of T As RDBaseEvent)(e As RDBaseEvent) As T
			Return e.Before(Of T).LastOrDefault
		End Function
		<Extension> Public Function [Next](Of T As RDBaseEvent)(e As T) As T
			Return e.After().First
		End Function
		<Extension> Public Function [Next](Of T As RDBaseEvent)(e As RDBaseEvent) As T
			Return e.After(Of T).First
		End Function
		<Extension> Public Function NextOrDefault(Of T As RDBaseEvent)(e As T) As T
			Return e.After().FirstOrDefault
		End Function
		<Extension> Public Function NextOrDefault(Of T As RDBaseEvent)(e As RDBaseEvent) As T
			Return e.After(Of T).FirstOrDefault
		End Function
		<Extension> Public Sub SetScoreboardLights(e As RDCallCustomMethod, Mode As Boolean, Text As String)
			e.MethodName = FunctionCalling(NameOf(SetScoreboardLights), Mode, Text)
		End Sub
		<Extension> Public Sub InvisibleChars(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(InvisibleChars), value)
		End Sub
		<Extension> Public Sub InvisibleHeart(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(InvisibleHeart), value)
		End Sub
		<Extension> Public Sub NoHitFlashBorder(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(NoHitFlashBorder), value)
		End Sub
		<Extension> Public Sub NoHitStrips(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(NoHitStrips), value)
		End Sub
		<Extension> Public Sub SetOneshotType(e As RDCallCustomMethod, rowID As Integer, wavetype As ShockWaveType)
			e.MethodName = FunctionCalling(NameOf(SetOneshotType), rowID, wavetype)
		End Sub
		<Extension> Public Sub WobblyLines(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(WobblyLines), value)
		End Sub
		<Extension> Public Sub TrueCameraMove(e As RDComment, RoomID As Integer, p As RDPoint, AnimationDuration As Single, Ease As EaseType)
			e.Text = $"()=>{NameOf(TrueCameraMove).ToLowerCamelCase}({RoomID},{p.X},{p.Y},{AnimationDuration},{Ease})"
		End Sub
		<Extension> Public Sub Create(e As RDComment, particleName As Particle, p As RDPoint)
			e.Text = $"()=>{NameOf(Create).ToLowerCamelCase}(CustomParticles/{particleName},{p.X},{p.Y})"
		End Sub
		<Extension> Public Sub ShockwaveSizeMultiplier(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(ShockwaveSizeMultiplier), value)
		End Sub
		<Extension> Public Sub ShockwaveDistortionMultiplier(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(ShockwaveDistortionMultiplier), value)
		End Sub
		<Extension> Public Sub ShockwaveDurationMultiplier(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(ShockwaveDurationMultiplier), value)
		End Sub
		<Extension> Public Sub Shockwave(e As RDComment, type As ShockWaveType, value As Single)
			e.Text = $"()=>{NameOf(Shockwave).ToLowerCamelCase}({type},{value})"
		End Sub
		<Extension> Public Sub MistakeOrHeal(e As RDCallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHeal), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealP1(e As RDCallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealP1), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealP2(e As RDCallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealP2), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealSilent(e As RDCallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealSilent), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealP1Silent(e As RDCallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealP1Silent), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealP2Silent(e As RDCallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealP2Silent), damageOrHeal)
		End Sub
		<Extension> Public Sub SetMistakeWeight(e As RDCallCustomMethod, rowID As Integer, weight As Single)
			e.MethodName = FunctionCalling(NameOf(SetMistakeWeight), rowID, weight)
		End Sub
		<Extension> Public Sub DamageHeart(e As RDCallCustomMethod, rowID As Integer, damage As Single)
			e.MethodName = FunctionCalling(NameOf(DamageHeart), rowID, damage)
		End Sub
		<Extension> Public Sub HealHeart(e As RDCallCustomMethod, rowID As Integer, damage As Single)
			e.MethodName = FunctionCalling(NameOf(HealHeart), rowID, damage)
		End Sub
		<Extension> Public Sub WavyRowsAmplitude(e As RDCallCustomMethod, roomID As Byte, amplitude As Single)
			e.MethodName = RoomPropertyAssignment(roomID, NameOf(WavyRowsAmplitude), amplitude)
		End Sub
		<Extension> Public Sub WavyRowsAmplitude(e As RDComment, roomID As Byte, amplitude As Single, duration As Single)
			e.Text = $"()=>{NameOf(WavyRowsAmplitude).ToLowerCamelCase}({roomID},{amplitude},{duration})"
		End Sub
		<Extension> Public Sub WavyRowsFrequency(e As RDCallCustomMethod, roomID As Byte, frequency As Single)
			e.MethodName = RoomPropertyAssignment(roomID, NameOf(WavyRowsFrequency), frequency)
		End Sub
		<Extension> Public Sub SetShakeIntensityOnHit(e As RDCallCustomMethod, roomID As Byte, number As Integer, strength As Integer)
			e.MethodName = RoomFunctionCalling(roomID, NameOf(SetShakeIntensityOnHit), number, strength)
		End Sub
		<Extension> Public Sub ShowPlayerHand(e As RDCallCustomMethod, roomID As Byte, isPlayer1 As Boolean, isShortArm As Boolean, isInstant As Boolean)
			e.MethodName = FunctionCalling(NameOf(ShowPlayerHand), roomID, isPlayer1, isShortArm, isInstant)
		End Sub
		<Extension> Public Sub TintHandsWithInts(e As RDCallCustomMethod, roomID As Byte, R As Single, G As Single, B As Single, A As Single)
			e.MethodName = FunctionCalling(NameOf(TintHandsWithInts), roomID, R, G, B, A)
		End Sub
		<Extension> Public Sub SetHandsBorderColor(e As RDCallCustomMethod, roomID As Byte, R As Single, G As Single, B As Single, A As Single, style As Integer)
			e.MethodName = FunctionCalling(NameOf(SetHandsBorderColor), roomID, R, G, B, A, style)
		End Sub
		<Extension> Public Sub SetAllHandsBorderColor(e As RDCallCustomMethod, R As Single, G As Single, B As Single, A As Single, style As Integer)
			e.MethodName = FunctionCalling(NameOf(SetAllHandsBorderColor), R, G, B, A, style)
		End Sub
		<Extension> Public Sub SetHandToP1(e As RDCallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = FunctionCalling(NameOf(SetHandToP1), room, rightHand)
		End Sub
		<Extension> Public Sub SetHandToP2(e As RDCallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = FunctionCalling(NameOf(SetHandToP2), room, rightHand)
		End Sub
		<Extension> Public Sub SetHandToIan(e As RDCallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = FunctionCalling(NameOf(SetHandToIan), room, rightHand)
		End Sub
		<Extension> Public Sub SetHandToPaige(e As RDCallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = FunctionCalling(NameOf(SetHandToPaige), room, rightHand)
		End Sub
		<Extension> Public Sub SetShadowRow(e As RDCallCustomMethod, mimickerRowID As Integer, mimickedRowID As Integer)
			e.MethodName = FunctionCalling(NameOf(SetShadowRow), mimickerRowID, mimickedRowID)
		End Sub
		<Extension> Public Sub UnsetShadowRow(e As RDCallCustomMethod, mimickerRowID As Integer, mimickedRowID As Integer)
			e.MethodName = FunctionCalling(NameOf(UnsetShadowRow), mimickerRowID, mimickedRowID)
		End Sub
		<Extension> Public Sub ShakeCam(e As RDCallCustomMethod, number As Integer, strength As Integer, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(ShakeCam), number, strength, roomID)
		End Sub
		<Extension> Public Sub StopShakeCam(e As RDCallCustomMethod, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(StopShakeCam), roomID)
		End Sub
		<Extension> Public Sub ShakeCamSmooth(e As RDCallCustomMethod, duration As Integer, strength As Integer, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(ShakeCamSmooth), duration, strength, roomID)
		End Sub
		<Extension> Public Sub ShakeCamRotate(e As RDCallCustomMethod, duration As Integer, strength As Integer, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(ShakeCamRotate), duration, strength, roomID)
		End Sub
		<Extension> Public Sub SetKaleidoscopeColor(e As RDCallCustomMethod, roomID As Integer, R1 As Single, G1 As Single, B1 As Single, R2 As Single, G2 As Single, B2 As Single)
			e.MethodName = FunctionCalling(NameOf(SetKaleidoscopeColor), roomID, R1, G1, B1, R2, G2, B2)
		End Sub
		<Extension> Public Sub SyncKaleidoscopes(e As RDCallCustomMethod, targetRoomID As Integer, otherRoomID As Integer)
			e.MethodName = FunctionCalling(NameOf(SyncKaleidoscopes), targetRoomID, otherRoomID)
		End Sub
		<Extension> Public Sub SetVignetteAlpha(e As RDCallCustomMethod, alpha As Single, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(SetVignetteAlpha), alpha, roomID)
		End Sub
		<Extension> Public Sub NoOneshotShadows(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(NoOneshotShadows), value)
		End Sub
		<Extension> Public Sub EnableRowReflections(e As RDCallCustomMethod, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(EnableRowReflections), roomID)
		End Sub
		<Extension> Public Sub DisableRowReflections(e As RDCallCustomMethod, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(DisableRowReflections), roomID)
		End Sub
		<Extension> Public Sub ChangeCharacter(e As RDCallCustomMethod, Name As String, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(ChangeCharacter), Name, roomID)
		End Sub
		<Extension> Public Sub ChangeCharacter(e As RDCallCustomMethod, Name As Characters, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(ChangeCharacter), Name, roomID)
		End Sub
		<Extension> Public Sub ChangeCharacterSmooth(e As RDCallCustomMethod, Name As String, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(ChangeCharacterSmooth), Name, roomID)
		End Sub
		<Extension> Public Sub ChangeCharacterSmooth(e As RDCallCustomMethod, Name As Characters, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(ChangeCharacterSmooth), Name, roomID)
		End Sub
		<Extension> Public Sub SmoothShake(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(SmoothShake), value)
		End Sub
		<Extension> Public Sub RotateShake(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(RotateShake), value)
		End Sub
		<Extension> Public Sub DisableRowChangeWarningFlashes(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(DisableRowChangeWarningFlashes), value)
		End Sub
		<Extension> Public Sub StatusSignWidth(e As RDCallCustomMethod, value As Single)
			e.MethodName = PropertyAssignment(NameOf(StatusSignWidth), value)
		End Sub
		<Extension> Public Sub SkippableRankScreen(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(SkippableRankScreen), value)
		End Sub
		<Extension> Public Sub MissesToCrackHeart(e As RDCallCustomMethod, value As Integer)
			e.MethodName = PropertyAssignment(NameOf(MissesToCrackHeart), value)
		End Sub
		<Extension> Public Sub SkipRankText(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(SkipRankText), value)
		End Sub
		<Extension> Public Sub AlternativeMatrix(e As RDCallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(AlternativeMatrix), value)
		End Sub
		<Extension> Public Sub ToggleSingleRowReflections(e As RDCallCustomMethod, room As Byte, row As Byte, value As Boolean)
			e.MethodName = FunctionCalling(NameOf(ToggleSingleRowReflections), room, row, value)
		End Sub
		<Extension> Public Sub SetScrollSpeed(e As RDCallCustomMethod, roomID As Byte, speed As Single, duration As Single, ease As EaseType)
			e.MethodName = RoomFunctionCalling(roomID, NameOf(SetScrollSpeed), speed, duration, ease)
		End Sub
		<Extension> Public Sub SetScrollOffset(e As RDCallCustomMethod, roomID As Byte, cameraOffset As Single, duration As Single, ease As EaseType)
			e.MethodName = RoomFunctionCalling(roomID, NameOf(SetScrollOffset), cameraOffset, duration, ease)
		End Sub
		<Extension> Public Sub DarkenedRollerdisco(e As RDCallCustomMethod, roomID As Byte, value As Single)
			e.MethodName = RoomFunctionCalling(roomID, NameOf(DarkenedRollerdisco), value)
		End Sub
		<Extension> Public Sub CurrentSongVol(e As RDCallCustomMethod, targetVolume As Single, fadeTimeSeconds As Single)
			e.MethodName = FunctionCalling(NameOf(CurrentSongVol), targetVolume, fadeTimeSeconds)
		End Sub
		<Extension> Public Sub PreviousSongVol(e As RDCallCustomMethod, targetVolume As Single, fadeTimeSeconds As Single)
			e.MethodName = FunctionCalling(NameOf(PreviousSongVol), targetVolume, fadeTimeSeconds)
		End Sub
		<Extension> Public Sub EditTree(e As RDCallCustomMethod, room As Byte, [property] As String, value As Single, beats As Single, ease As EaseType)
			e.MethodName = RoomFunctionCalling(room, NameOf(EditTree), [property], value, beats, ease)
		End Sub
		<Extension> Public Function EditTree(e As RDCallCustomMethod, room As Byte, treeProperties As ProceduralTree, beats As Single, ease As EaseType) As IEnumerable(Of RDCallCustomMethod)
			Dim L As New List(Of RDCallCustomMethod)
			For Each p In GetType(ProceduralTree).GetFields
				If p.GetValue(treeProperties) IsNot Nothing Then
					Dim T As RDCallCustomMethod = e.Clone(Of RDCallCustomMethod)
					T.EditTree(room, p.Name, p.GetValue(treeProperties), beats, ease)
					L.Add(T)
				End If
			Next
			Return L
		End Function
		<Extension> Public Sub EditTreeColor(e As RDCallCustomMethod, room As Byte, location As Boolean, color As String, beats As Single, ease As EaseType)
			e.MethodName = RoomFunctionCalling(room, NameOf(EditTreeColor), location, color, beats, ease)
		End Sub




		<Extension> Public Function SpetialTags(e As RDTagAction) As RDTagAction.SpecialTag()
			Return [Enum].GetValues(GetType(RDTagAction.SpecialTag)).Cast(Of RDTagAction.SpecialTag).Where(Function(i) e.ActionTag.Contains($"[{i}]"))
		End Function
		<Extension> Public Function Length(e As RDAddOneshotBeat) As Single
			Return e.Tick * e.Loops + e.Interval * e.Loops - 1
		End Function
		<Extension> Public Function Length(e As RDAddClassicBeat) As Single
			Dim SyncoSwing = e.Parent.LastOrDefault(Of RDSetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New RDSetRowXs).SyncoSwing
			Return e.Tick * 6 - If(SyncoSwing = 0, 0.5, SyncoSwing) * e.Tick
		End Function
		<Extension> Public Function IsHitable(e As RDPulseFreeTimeBeat) As Boolean
			Dim PulseIndexMin = 6
			Dim PulseIndexMax = 6
			For Each item In e.Parent.Where(Of RDBaseBeat)(Function(i) e.IsBehind(i)).Reverse
				Select Case item.Type
					Case RDEventType.AddFreeTimeBeat
						Dim Temp = CType(item, RDAddFreeTimeBeat)
						If PulseIndexMin <= Temp.Pulse And Temp.Pulse <= PulseIndexMax Then
							Return True
						End If
					Case RDEventType.PulseFreeTimeBeat
						Dim Temp = CType(item, RDPulseFreeTimeBeat)
						Select Case Temp.Action
							Case RDPulseFreeTimeBeat.ActionType.Increment
								If PulseIndexMin > 0 Then
									PulseIndexMin -= 1
								End If
								If PulseIndexMax > 0 Then
									PulseIndexMax -= 1
								Else
									Return False
								End If
							Case RDPulseFreeTimeBeat.ActionType.Decrement
								If PulseIndexMin > 0 Then
									PulseIndexMin += 1
								End If
								If PulseIndexMax < 6 Then
									PulseIndexMax += 1
								Else
									Return False
								End If
							Case RDPulseFreeTimeBeat.ActionType.Custom
								If PulseIndexMin <= Temp.CustomPulse And Temp.CustomPulse <= PulseIndexMax Then
									PulseIndexMin = 0
									PulseIndexMax = 5
								Else
									Return False
								End If
							Case RDPulseFreeTimeBeat.ActionType.Remove
								Return False
						End Select
						If PulseIndexMin > PulseIndexMax Then
							Return False
						End If
				End Select
			Next
			Return False
		End Function
		<Extension> Public Function IsHitable(e As RDAddFreeTimeBeat) As Boolean
			Return e.Pulse = 6
		End Function
		<Extension> Public Function IsHitable(e As RDBaseBeat) As Boolean
			Select Case e.Type
				Case RDEventType.AddClassicBeat, RDEventType.AddOneshotBeat
					Return True
				Case RDEventType.AddFreeTimeBeat
					Return CType(e, RDAddFreeTimeBeat).IsHitable
				Case RDEventType.PulseFreeTimeBeat
					Return CType(e, RDPulseFreeTimeBeat).IsHitable
				Case Else
					Return False
			End Select
		End Function
		<Extension> Public Function HitTimes(e As RDAddClassicBeat) As IEnumerable(Of Hit)
			Return New List(Of Hit) From {New Hit(e, e.GetBeat(6), e.Hold)}.AsEnumerable
		End Function
		<Extension> Public Function HitTimes(e As RDAddOneshotBeat) As IEnumerable(Of Hit)
			Dim L As New List(Of Hit)
			For i As UInteger = 0 To e.Loops
				For j As SByte = 0 To e.Subdivisions - 1
					L.Add(New Hit(e, New RDBeat(e._beat._calculator, e._beat.BeatOnly + i * e.Interval + e.Tick + e.Delay + j * (e.Tick / e.Subdivisions)), 0))
				Next
			Next
			Return L.AsEnumerable
		End Function
		<Extension> Public Function HitTimes(e As RDAddFreeTimeBeat) As IEnumerable(Of Hit)
			If e.Pulse = 6 Then
				Return New List(Of Hit) From {New Hit(e, e.Beat, e.Hold)}.AsEnumerable
			End If
			Return New List(Of Hit)
		End Function
		<Extension> Public Function HitTimes(e As RDPulseFreeTimeBeat) As IEnumerable(Of Hit)
			If e.IsHitable Then
				Return New List(Of Hit) From {New Hit(e, e.Beat, e.Hold)}
			End If
			Return New List(Of Hit)
		End Function
		<Extension> Public Function HitTimes(e As RDBaseBeat) As IEnumerable(Of Hit)
			Select Case e.Type
				Case RDEventType.AddClassicBeat
					Return CType(e, RDAddClassicBeat).HitTimes
				Case RDEventType.AddFreeTimeBeat
					Return CType(e, RDAddFreeTimeBeat).HitTimes
				Case RDEventType.AddOneshotBeat
					Return CType(e, RDAddOneshotBeat).HitTimes
				Case RDEventType.PulseFreeTimeBeat
					Return CType(e, RDPulseFreeTimeBeat).HitTimes
				Case Else
					Return Array.Empty(Of Hit).AsEnumerable
			End Select
		End Function
		<Extension> Public Function GetBeat(e As RDAddClassicBeat, index As Byte) As RDBeat
			Dim x = e.Parent.LastOrDefault(Of RDSetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New RDSetRowXs)
			Dim Synco As Single
			If x.SyncoBeat >= 0 Then
				Synco = If(x.SyncoSwing = 0, 0.5, x.SyncoSwing)
			Else
				Synco = 0
			End If
			If index >= 7 Then
				Throw New RhythmBaseException("THIS IS 7TH BEAT GAMES!")
			End If
			Return e.Beat + e.Tick * 6 - e.Tick * Synco
		End Function
		<Extension> Public Function GetPatternString(e As RDSetRowXs) As String
			Return Utils.GetPatternString(e.Pattern)
		End Function
		<Extension> Public Function CreateAdvanceText(e As RDFloatingText, beat As RDBeat) As RDAdvanceText
			Dim A As New RDAdvanceText With {.Parent = e, .Beat = beat}
			e.Children.Add(A)
			Return A
		End Function
		<Extension> Public Function GetPulses(e As RDAddFreeTimeBeat) As IEnumerable(Of RDPulseFreeTimeBeat)
			Dim Result As New List(Of RDPulseFreeTimeBeat)
			Dim pulse As Byte = e.Pulse
			For Each item In e.Parent.Where(Of RDPulseFreeTimeBeat)(Function(i) i.Active AndAlso i.Beat > e.Beat)
				Select Case item.Action
					Case RDPulseFreeTimeBeat.ActionType.Increment
						pulse += 1
						Result.Add(item)
					Case RDPulseFreeTimeBeat.ActionType.Decrement
						pulse = If(pulse > 1, pulse - 1, 0)
						Result.Add(item)
					Case RDPulseFreeTimeBeat.ActionType.Custom
						pulse = item.CustomPulse
						Result.Add(item)
					Case RDPulseFreeTimeBeat.ActionType.Remove
						Result.Add(item)
						Exit For
				End Select
				If pulse = 6 Then
					Exit For
				End If
			Next
			Return Result
		End Function
		<Extension> Private Function SplitCopy(e As RDSayReadyGetSetGo, extraBeat As Single, word As RDSayReadyGetSetGo.Words) As RDSayReadyGetSetGo
			Dim Temp = e.Clone(Of RDSayReadyGetSetGo)
			Temp.Beat += extraBeat
			Temp.PhraseToSay = word
			Temp.Volume = e.Volume
			Return Temp
		End Function
		<Extension> Public Function Split(e As RDSayReadyGetSetGo) As IEnumerable(Of RDSayReadyGetSetGo)
			If e.Splitable Then
				Select Case e.PhraseToSay
					Case RDSayReadyGetSetGo.Words.SayReaDyGetSetGoNew
						Return New List(Of RDSayReadyGetSetGo) From {
						e.SplitCopy(0, RDSayReadyGetSetGo.Words.JustSayRea),
						e.SplitCopy(e.Tick, RDSayReadyGetSetGo.Words.JustSayDy),
						e.SplitCopy(e.Tick * 2, RDSayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick * 3, RDSayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 4, RDSayReadyGetSetGo.Words.JustSayGo)}
					Case RDSayReadyGetSetGo.Words.SayGetSetGo
						Return New List(Of RDSayReadyGetSetGo) From {
						e.SplitCopy(0, RDSayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick, RDSayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 2, RDSayReadyGetSetGo.Words.JustSayGo)}
					Case RDSayReadyGetSetGo.Words.SayReaDyGetSetOne
						Return New List(Of RDSayReadyGetSetGo) From {
						e.SplitCopy(0, RDSayReadyGetSetGo.Words.JustSayRea),
						e.SplitCopy(e.Tick, RDSayReadyGetSetGo.Words.JustSayDy),
						e.SplitCopy(e.Tick * 2, RDSayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick * 3, RDSayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 4, RDSayReadyGetSetGo.Words.Count1)}
					Case RDSayReadyGetSetGo.Words.SayGetSetOne
						Return New List(Of RDSayReadyGetSetGo) From {
						e.SplitCopy(0, RDSayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick, RDSayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 2, RDSayReadyGetSetGo.Words.Count1)}
					Case RDSayReadyGetSetGo.Words.SayReadyGetSetGo
						Return New List(Of RDSayReadyGetSetGo) From {
						e.SplitCopy(0, RDSayReadyGetSetGo.Words.JustSayReady),
						e.SplitCopy(e.Tick * 2, RDSayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick * 3, RDSayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 4, RDSayReadyGetSetGo.Words.JustSayGo)}
					Case Else
				End Select
			End If
			Return New List(Of RDSayReadyGetSetGo) From {e}.AsEnumerable
		End Function
		<Extension> Public Function Split(e As RDAddOneshotBeat) As IEnumerable(Of RDAddOneshotBeat)
			Dim L As New List(Of RDAddOneshotBeat)
			For i As UInteger = 0 To e.Loops
				Dim T = e.Clone(Of RDAddOneshotBeat)
				T.FreezeBurnMode = e.FreezeBurnMode
				T.Delay = e.Delay
				T.PulseType = e.PulseType
				T.Subdivisions = e.Subdivisions
				T.SubdivSound = e.SubdivSound
				T.Tick = e.Tick
				T.Loops = 0
				T.Interval = 0
				T.Beat = New RDBeat(e._beat._calculator, e.Beat.BeatOnly + i * e.Interval)
				L.Add(T)
			Next
			Return L.AsEnumerable
		End Function
		<Extension> Public Function Split(e As RDAddClassicBeat) As IEnumerable(Of RDBaseBeat)
			Dim x = e.Parent.LastOrDefault(Of RDSetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New RDSetRowXs)
			Return e.Split(x)
		End Function
		<Extension> Public Function Split(e As RDAddClassicBeat, Xs As RDSetRowXs) As IEnumerable(Of RDBaseBeat)
			Dim L As New List(Of RDBaseBeat)
			Dim Head As RDAddFreeTimeBeat = e.Clone(Of RDAddFreeTimeBeat)()
			Head.Pulse = 0
			Head.Hold = e.Hold
			L.Add(Head)
			Dim tempBeat = e.Beat
			For i = 1 To 6
				If i < 6 AndAlso Xs.Pattern(i) = Patterns.X Then
					Continue For
				End If
				Dim Pulse As RDPulseFreeTimeBeat = e.Clone(Of RDPulseFreeTimeBeat)()
				Pulse.Beat += e.Tick * i
				If i >= Xs.SyncoBeat Then
					Pulse.Beat -= Xs.SyncoSwing
				End If
				If i Mod 2 = 1 Then
					Pulse.Beat += e.Tick - If(e.Swing = 0, e.Tick, e.Swing)
				End If
				Pulse.Hold = e.Hold
				Pulse.Action = RDPulseFreeTimeBeat.ActionType.Increment
				L.Add(Pulse)
			Next
			Return L.AsEnumerable
		End Function
		<Extension> Public Function ControllingEvents(e As RDTagAction) As IEnumerable(Of IGrouping(Of String, RDBaseEvent))
			Return e.Beat.baseLevel.GetTaggedEvents(e.ActionTag, e.Action.HasFlag(RDTagAction.Actions.All))
		End Function
		<Extension> Public Function ControllingEventsLeftAligned(e As RDTagAction) As IEnumerable(Of IGrouping(Of String, RDBaseEvent))
			Dim L = e.Beat.baseLevel.ControllingEvents
			For Each pair In L
				Dim start = pair(0).Beat
				For Each item In pair
					item.Beat -= start.BeatOnly
				Next
			Next
			Return L
		End Function
		<Extension> Public Function TextOnly(e As RDShowDialogue) As String
			Dim result = e.Text
			For Each item In {
"shake",
"shakeRadius=\d+",
"wave",
"waveHeight=\d+",
"waveSpeed=\d+",
"swirl",
"swirlRadius=\d+",
"swirlSpeed=\d+",
"static"
}
				result = Text.RegularExpressions.Regex.Replace(result, $"\[{item}\]", "")
			Next
			Return result
		End Function
		<Extension> Public Sub MovePositionMaintainVisual(e As RDMove, target As RDPointE)
			If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric Then
				Exit Sub
			End If
			e.Position = target
			e.Pivot = (e.VisualPosition() - New RDSizeE(target)).Rotate(e.Angle.Value.NumericValue)
		End Sub
		<Extension> Public Sub MovePositionMaintainVisual(e As RDMoveRoom, target As RDSizeE)
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric Then
				Exit Sub
			End If
			e.RoomPosition = target
			e.Pivot = (e.VisualPosition() - New RDSizeE(target)).Rotate(e.Angle.Value.NumericValue)
		End Sub
		<Extension> Public Function VisualPosition(e As RDMove) As RDPointE
			If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric OrElse e.Scale Is Nothing Then
				Return New RDPointE
			End If
			Dim previousPosition As RDPointE = e.Position
			Dim previousPivot As New RDPointE(
e.Pivot?.X * e.Scale?.X * e.Parent.Size.Width / 100,
e.Pivot?.Y * e.Scale?.Y * e.Parent.Size.Height / 100)
			Return previousPosition + New RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue))
		End Function
		<Extension> Public Function VisualPosition(e As RDMoveRoom) As RDPointE
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
				Return New RDPointE
			End If
			Dim previousPosition As RDPointE = e.RoomPosition
			Dim previousPivot As New RDPointE(e.Pivot?.X * e.Scale?.X, e.Pivot?.Y * e.Scale?.Y)
			Return previousPosition + New RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue))
		End Function
	End Module
End Namespace