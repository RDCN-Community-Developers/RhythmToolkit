Imports System.Runtime.CompilerServices
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
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.EventsBeatOrder.SelectMany(Function(i) i.Value.list).Where(predicate)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), beat As RDBeat) As IEnumerable(Of T)
			Dim value As TypedList(Of BaseEvent) = Nothing
			If e.EventsBeatOrder.TryGetValue(beat, value) Then
				Return value.list
			End If
			Return value
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.EventsBeatOrder _
				.TakeWhile(Function(i) i.Key < endBeat) _
				.SkipWhile(Function(i) i.Key < startBeat) _
				.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), index As Index) As IEnumerable(Of T)
			Dim rg = GetRange(e, index)
			Return e.EventsBeatOrder _
				.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
				.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
				.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), range As RDRange) As IEnumerable(Of T)
			Return e.EventsBeatOrder _
				.TakeWhile(Function(i) If(i.Key < range.End, True)) _
				.SkipWhile(Function(i) If(i.Key < range.Start, False)) _
				.SelectMany(Function(i) i.Value.list)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), range As Range) As IEnumerable(Of T)
			Dim rg = GetRange(e, range)
			Return e.EventsBeatOrder _
				.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
				.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
				.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As Single) As IEnumerable(Of T)
			Return e.Where(beat).Where(predicate)
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
			Dim enums = ConvertToEnums(Of T)()
			Return e.EventsBeatOrder _
				.Where(Function(i) i.Value._types _
					.Any(Function(j) enums.Contains(j))) _
				.SelectMany(Function(i) i.Value.list).OfType(Of T)
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, beat As RDBeat) As IEnumerable(Of T)
			Dim value As TypedList(Of BaseEvent) = Nothing
			If e.EventsBeatOrder.TryGetValue(beat, value) Then
				Return value.list.OfType(Of T)
			End If
			Return value
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.EventsBeatOrder _
				.TakeWhile(Function(i) i.Key < endBeat) _
				.SkipWhile(Function(i) i.Key < startBeat) _
				.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, index As Index) As IEnumerable(Of T)
			Dim rg = GetRange(e, index)
			Return e.EventsBeatOrder _
				.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
				.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
				.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, range As RDRange) As IEnumerable(Of T)
			Return e.EventsBeatOrder _
				.TakeWhile(Function(i) If(i.Key < range.End, True)) _
				.SkipWhile(Function(i) If(i.Key < range.Start, False)) _
				.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, range As Range) As IEnumerable(Of T)
			Dim rg = GetRange(e, range)
			Return e.EventsBeatOrder _
				.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
				.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
				.SelectMany(Function(i) i.Value.list.OfType(Of T))
		End Function
		<Extension> Public Function Where(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.Where(Of T)().Where(predicate)
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
			Return e.EventsBeatOrder.First.Value.list.First
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
			Return e.EventsBeatOrder.FirstOrDefault.Value?.list.FirstOrDefault
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
			Return e.EventsBeatOrder.Last.Value.list.Last
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
			Return e.EventsBeatOrder.LastOrDefault.Value?.list.LastOrDefault()
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
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(beat.BeatOnly)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), index As Index) As IEnumerable(Of T)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return e.TakeWhile(
If(index.IsFromEnd,
lastEvent.Beat._calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value + 1, 1),
firstEvent.Beat._calculator.BarBeat_BeatOnly(index.Value + 1, 1)))
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.EventsBeatOrder.SelectMany(Function(i) i.Value.list).TakeWhile(predicate)
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
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(beat.BeatOnly)
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, index As Index) As IEnumerable(Of T)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return e.TakeWhile(Of T)(
If(index.IsFromEnd,
lastEvent.Beat._calculator.BarBeat_BeatOnly(lastEvent.Beat.BarBeat.bar - index.Value + 1, 1),
firstEvent.Beat._calculator.BarBeat_BeatOnly(index.Value + 1, 1)))
		End Function
		<Extension> Public Function TakeWhile(Of T As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.EventsBeatOrder.SelectMany(Function(i) i.Value.list.OfType(Of T)).Where(predicate)
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
			Return item1.Beat < item2.Beat OrElse
				(item1.Beat.BeatOnly = item2.Beat.BeatOnly AndAlso
				e.EventsBeatOrder(item1.Beat).list.IndexOf(item1) < e.EventsBeatOrder(item2.Beat).list.IndexOf(item2))
		End Function
		<Extension> Public Function IsBehind(Of T As BaseEvent)(e As OrderedEventCollection(Of T), item1 As T, item2 As T) As Boolean
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
		<Extension> Private Function ClassicBeats(e As Row) As IEnumerable(Of BaseBeat)
			Return e.Where(Of BaseBeat)(Function(i) (i.Type = EventType.AddClassicBeat Or
								i.Type = EventType.AddFreeTimeBeat Or
								i.Type = EventType.PulseFreeTimeBeat) AndAlso
								i.Hitable)
		End Function
		<Extension> Private Function OneshotBeats(e As Row) As IEnumerable(Of BaseBeat)
			Return e.Where(Of BaseBeat)(Function(i) i.Type = EventType.AddOneshotBeat AndAlso
								i.Hitable)
		End Function
		<Extension> Public Function HitBeats(e As Row) As IEnumerable(Of Hit)
			Select Case e.RowType
				Case RowType.Classic
					Return e.ClassicBeats().SelectMany(Function(i) i.HitTimes)
				Case RowType.Oneshot
					Return e.OneshotBeats().SelectMany(Function(i) i.HitTimes)
				Case Else
					Throw New Exceptions.RhythmBaseException("How?")
			End Select
		End Function
#If DEBUG Then
		<Extension> Public Function GetRowBeatStatus(e As Row) As SortedDictionary(Of Single, Integer())
			Dim L As New SortedDictionary(Of Single, Integer())
			Select Case e.RowType
				Case RowType.Classic
					Dim value As Integer() = New Integer(6) {}
					L.Add(0, value)
					For Each beat In e
						Select Case beat.Type
							Case EventType.AddClassicBeat
								Dim trueBeat = CType(beat, AddClassicBeat)
								For i = 0 To 6
									Dim statusArray As Integer() = If(L(beat.Beat.BeatOnly), New Integer(6) {})
									statusArray(i) += 1
									L(beat.Beat.BeatOnly) = statusArray
								Next
							Case EventType.AddFreeTimeBeat
							Case EventType.PulseFreeTimeBeat
							Case EventType.SetRowXs
						End Select
					Next
				Case RowType.Oneshot
				Case Else
					Throw New RhythmBaseException("How")
			End Select
			Return L
		End Function
#End If
		<Extension> Public Function Beats(e As Row) As IEnumerable(Of BaseBeat)
			Select Case e.RowType
				Case RowType.Classic
					Return e.ClassicBeats()
				Case RowType.Oneshot
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
		<Extension> Public Function IsInFrontOf(e As BaseEvent, item As BaseEvent) As Boolean
			Return e.Beat.baseLevel.IsInFrontOf(e, item)
		End Function
		<Extension> Public Function IsBehind(e As BaseEvent, item As BaseEvent) As Boolean
			Return e.Beat.baseLevel.IsBehind(e, item)
		End Function
		<Extension> Public Function Before(Of T As BaseEvent)(e As T) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(e.Beat.baseLevel.DefaultBeat, e.Beat)
		End Function
		<Extension> Public Function Before(Of T As BaseEvent)(e As BaseEvent) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(e.Beat.baseLevel.DefaultBeat, e.Beat)
		End Function
		<Extension> Public Function After(Of T As BaseEvent)(e As T) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(Function(i) i.Beat > e.Beat)
		End Function
		<Extension> Public Function After(Of T As BaseEvent)(e As BaseEvent) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(Function(i) i.Beat > e.Beat)
		End Function
		<Extension> Public Function Front(Of T As BaseEvent)(e As T) As T
			Return e.Before.Last
		End Function
		<Extension> Public Function Front(Of T As BaseEvent)(e As BaseEvent) As T
			Return e.Before(Of T).Last
		End Function
		<Extension> Public Function FrontOrDefault(Of T As BaseEvent)(e As T) As T
			Return e.Before.LastOrDefault
		End Function
		<Extension> Public Function FrontOrDefault(Of T As BaseEvent)(e As BaseEvent) As T
			Return e.Before(Of T).LastOrDefault
		End Function
		<Extension> Public Function [Next](Of T As BaseEvent)(e As T) As T
			Return e.After().First
		End Function
		<Extension> Public Function [Next](Of T As BaseEvent)(e As BaseEvent) As T
			Return e.After(Of T).First
		End Function
		<Extension> Public Function NextOrDefault(Of T As BaseEvent)(e As T) As T
			Return e.After().FirstOrDefault
		End Function
		<Extension> Public Function NextOrDefault(Of T As BaseEvent)(e As BaseEvent) As T
			Return e.After(Of T).FirstOrDefault
		End Function



		<Extension> Public Sub SetScoreboardLights(e As CallCustomMethod, Mode As Boolean, Text As String)
			e.MethodName = FunctionCalling(NameOf(SetScoreboardLights), Mode, Text)
		End Sub
		<Extension> Public Sub InvisibleChars(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(InvisibleChars), value)
		End Sub
		<Extension> Public Sub InvisibleHeart(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(InvisibleHeart), value)
		End Sub
		<Extension> Public Sub NoHitFlashBorder(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(NoHitFlashBorder), value)
		End Sub
		<Extension> Public Sub NoHitStrips(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(NoHitStrips), value)
		End Sub
		<Extension> Public Sub SetOneshotType(e As CallCustomMethod, rowID As Integer, wavetype As ShockWaveType)
			e.MethodName = FunctionCalling(NameOf(SetOneshotType), rowID, wavetype)
		End Sub
		<Extension> Public Sub WobblyLines(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(WobblyLines), value)
		End Sub
		<Extension> Public Sub TrueCameraMove(e As Comment, RoomID As Integer, p As RDPoint, AnimationDuration As Single, Ease As EaseType)
			e.Text = $"()=>{NameOf(TrueCameraMove).ToLowerCamelCase}({RoomID},{p.X},{p.Y},{AnimationDuration},{Ease})"
		End Sub
		<Extension> Public Sub Create(e As Comment, particleName As Particle, p As RDPoint)
			e.Text = $"()=>{NameOf(Create).ToLowerCamelCase}(CustomParticles/{particleName},{p.X},{p.Y})"
		End Sub
		<Extension> Public Sub ShockwaveSizeMultiplier(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(ShockwaveSizeMultiplier), value)
		End Sub
		<Extension> Public Sub ShockwaveDistortionMultiplier(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(ShockwaveDistortionMultiplier), value)
		End Sub
		<Extension> Public Sub ShockwaveDurationMultiplier(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(ShockwaveDurationMultiplier), value)
		End Sub
		<Extension> Public Sub Shockwave(e As Comment, type As ShockWaveType, value As Single)
			e.Text = $"()=>{NameOf(Shockwave).ToLowerCamelCase}({type},{value})"
		End Sub
		<Extension> Public Sub MistakeOrHeal(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHeal), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealP1(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealP1), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealP2(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealP2), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealSilent(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealSilent), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealP1Silent(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealP1Silent), damageOrHeal)
		End Sub
		<Extension> Public Sub MistakeOrHealP2Silent(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = FunctionCalling(NameOf(MistakeOrHealP2Silent), damageOrHeal)
		End Sub
		<Extension> Public Sub SetMistakeWeight(e As CallCustomMethod, rowID As Integer, weight As Single)
			e.MethodName = FunctionCalling(NameOf(SetMistakeWeight), rowID, weight)
		End Sub
		<Extension> Public Sub DamageHeart(e As CallCustomMethod, rowID As Integer, damage As Single)
			e.MethodName = FunctionCalling(NameOf(DamageHeart), rowID, damage)
		End Sub
		<Extension> Public Sub HealHeart(e As CallCustomMethod, rowID As Integer, damage As Single)
			e.MethodName = FunctionCalling(NameOf(HealHeart), rowID, damage)
		End Sub
		<Extension> Public Sub WavyRowsAmplitude(e As CallCustomMethod, roomID As Byte, amplitude As Single)
			e.MethodName = RoomPropertyAssignment(roomID, NameOf(WavyRowsAmplitude), amplitude)
		End Sub
		<Extension> Public Sub WavyRowsAmplitude(e As Comment, roomID As Byte, amplitude As Single, duration As Single)
			e.Text = $"()=>{NameOf(WavyRowsAmplitude).ToLowerCamelCase}({roomID},{amplitude},{duration})"
		End Sub
		<Extension> Public Sub WavyRowsFrequency(e As CallCustomMethod, roomID As Byte, frequency As Single)
			e.MethodName = RoomPropertyAssignment(roomID, NameOf(WavyRowsFrequency), frequency)
		End Sub
		<Extension> Public Sub SetShakeIntensityOnHit(e As CallCustomMethod, roomID As Byte, number As Integer, strength As Integer)
			e.MethodName = RoomFunctionCalling(roomID, NameOf(SetShakeIntensityOnHit), number, strength)
		End Sub
		<Extension> Public Sub ShowPlayerHand(e As CallCustomMethod, roomID As Byte, isPlayer1 As Boolean, isShortArm As Boolean, isInstant As Boolean)
			e.MethodName = FunctionCalling(NameOf(ShowPlayerHand), roomID, isPlayer1, isShortArm, isInstant)
		End Sub
		<Extension> Public Sub TintHandsWithInts(e As CallCustomMethod, roomID As Byte, R As Single, G As Single, B As Single, A As Single)
			e.MethodName = FunctionCalling(NameOf(TintHandsWithInts), roomID, R, G, B, A)
		End Sub
		<Extension> Public Sub SetHandsBorderColor(e As CallCustomMethod, roomID As Byte, R As Single, G As Single, B As Single, A As Single, style As Integer)
			e.MethodName = FunctionCalling(NameOf(SetHandsBorderColor), roomID, R, G, B, A, style)
		End Sub
		<Extension> Public Sub SetAllHandsBorderColor(e As CallCustomMethod, R As Single, G As Single, B As Single, A As Single, style As Integer)
			e.MethodName = FunctionCalling(NameOf(SetAllHandsBorderColor), R, G, B, A, style)
		End Sub
		<Extension> Public Sub SetHandToP1(e As CallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = FunctionCalling(NameOf(SetHandToP1), room, rightHand)
		End Sub
		<Extension> Public Sub SetHandToP2(e As CallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = FunctionCalling(NameOf(SetHandToP2), room, rightHand)
		End Sub
		<Extension> Public Sub SetHandToIan(e As CallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = FunctionCalling(NameOf(SetHandToIan), room, rightHand)
		End Sub
		<Extension> Public Sub SetHandToPaige(e As CallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = FunctionCalling(NameOf(SetHandToPaige), room, rightHand)
		End Sub
		<Extension> Public Sub SetShadowRow(e As CallCustomMethod, mimickerRowID As Integer, mimickedRowID As Integer)
			e.MethodName = FunctionCalling(NameOf(SetShadowRow), mimickerRowID, mimickedRowID)
		End Sub
		<Extension> Public Sub UnsetShadowRow(e As CallCustomMethod, mimickerRowID As Integer, mimickedRowID As Integer)
			e.MethodName = FunctionCalling(NameOf(UnsetShadowRow), mimickerRowID, mimickedRowID)
		End Sub
		<Extension> Public Sub ShakeCam(e As CallCustomMethod, number As Integer, strength As Integer, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(ShakeCam), number, strength, roomID)
		End Sub
		<Extension> Public Sub StopShakeCam(e As CallCustomMethod, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(StopShakeCam), roomID)
		End Sub
		<Extension> Public Sub ShakeCamSmooth(e As CallCustomMethod, duration As Integer, strength As Integer, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(ShakeCamSmooth),duration,strength,roomID)
		End Sub
		<Extension> Public Sub ShakeCamRotate(e As CallCustomMethod, duration As Integer, strength As Integer, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(ShakeCamRotate), duration, strength, roomID)
		End Sub
		<Extension> Public Sub SetKaleidoscopeColor(e As CallCustomMethod, roomID As Integer, R1 As Single, G1 As Single, B1 As Single, R2 As Single, G2 As Single, B2 As Single)
			e.MethodName = FunctionCalling(NameOf(SetKaleidoscopeColor), roomID, R1, G1, B1, R2, G2, B2)
		End Sub
		<Extension> Public Sub SyncKaleidoscopes(e As CallCustomMethod, targetRoomID As Integer, otherRoomID As Integer)
			e.MethodName = FunctionCalling(NameOf(SyncKaleidoscopes), targetRoomID, otherRoomID)
		End Sub
		<Extension> Public Sub SetVignetteAlpha(e As CallCustomMethod, alpha As Single, roomID As Integer)
			e.MethodName = VfxFunctionCalling(NameOf(SetVignetteAlpha), alpha, roomID)
		End Sub
		<Extension> Public Sub NoOneshotShadows(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(NoOneshotShadows), value)
		End Sub
		<Extension> Public Sub EnableRowReflections(e As CallCustomMethod, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(EnableRowReflections), roomID)
		End Sub
		<Extension> Public Sub DisableRowReflections(e As CallCustomMethod, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(DisableRowReflections), roomID)
		End Sub
		<Extension> Public Sub ChangeCharacter(e As CallCustomMethod, Name As String, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(ChangeCharacter), Name, roomID)
		End Sub
		<Extension> Public Sub ChangeCharacter(e As CallCustomMethod, Name As Characters, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(ChangeCharacter), Name, roomID)
		End Sub
		<Extension> Public Sub ChangeCharacterSmooth(e As CallCustomMethod, Name As String, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(ChangeCharacterSmooth), Name, roomID)
		End Sub
		<Extension> Public Sub ChangeCharacterSmooth(e As CallCustomMethod, Name As Characters, roomID As Integer)
			e.MethodName = FunctionCalling(NameOf(ChangeCharacterSmooth), Name, roomID)
		End Sub
		<Extension> Public Sub SmoothShake(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(SmoothShake), value)
		End Sub
		<Extension> Public Sub RotateShake(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(RotateShake), value)
		End Sub
		<Extension> Public Sub DisableRowChangeWarningFlashes(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(DisableRowChangeWarningFlashes), value)
		End Sub
		<Extension> Public Sub StatusSignWidth(e As CallCustomMethod, value As Single)
			e.MethodName = PropertyAssignment(NameOf(StatusSignWidth), value)
		End Sub
		<Extension> Public Sub SkippableRankScreen(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(SkippableRankScreen), value)
		End Sub
		<Extension> Public Sub MissesToCrackHeart(e As CallCustomMethod, value As Integer)
			e.MethodName = PropertyAssignment(NameOf(MissesToCrackHeart), value)
		End Sub
		<Extension> Public Sub SkipRankText(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(SkipRankText), value)
		End Sub
		<Extension> Public Sub AlternativeMatrix(e As CallCustomMethod, value As Boolean)
			e.MethodName = PropertyAssignment(NameOf(AlternativeMatrix), value)
		End Sub
		<Extension> Public Sub ToggleSingleRowReflections(e As CallCustomMethod, room As Byte, row As Byte, value As Boolean)
			e.MethodName = FunctionCalling(NameOf(ToggleSingleRowReflections), room, row, value)
		End Sub
		<Extension> Public Sub SetScrollSpeed(e As CallCustomMethod, roomID As Byte, speed As Single, duration As Single, ease As EaseType)
			e.MethodName = RoomFunctionCalling(roomID, NameOf(SetScrollSpeed), speed, duration, ease)
		End Sub
		<Extension> Public Sub SetScrollOffset(e As CallCustomMethod, roomID As Byte, cameraOffset As Single, duration As Single, ease As EaseType)
			e.MethodName = RoomFunctionCalling(roomID, NameOf(SetScrollOffset), cameraOffset, duration, ease)
		End Sub
		<Extension> Public Sub DarkenedRollerdisco(e As CallCustomMethod, roomID As Byte, value As Single)
			e.MethodName = RoomFunctionCalling(roomID, NameOf(DarkenedRollerdisco), value)
		End Sub
		<Extension> Public Sub CurrentSongVol(e As CallCustomMethod, targetVolume As Single, fadeTimeSeconds As Single)
			e.MethodName = FunctionCalling(NameOf(CurrentSongVol), targetVolume, fadeTimeSeconds)
		End Sub
		<Extension> Public Sub PreviousSongVol(e As CallCustomMethod, targetVolume As Single, fadeTimeSeconds As Single)
			e.MethodName = FunctionCalling(NameOf(PreviousSongVol), targetVolume, fadeTimeSeconds)
		End Sub
		<Extension> Public Sub EditTree(e As CallCustomMethod, room As Byte, [property] As String, value As Single, beats As Single, ease As EaseType)
			e.MethodName = RoomFunctionCalling(room, NameOf(EditTree), [property], value, beats, ease)
		End Sub
		<Extension> Public Function EditTree(e As CallCustomMethod, room As Byte, treeProperties As ProceduralTree, beats As Single, ease As EaseType) As IEnumerable(Of CallCustomMethod)
			Dim L As New List(Of CallCustomMethod)
			For Each p In GetType(ProceduralTree).GetFields
				If p.GetValue(treeProperties) IsNot Nothing Then
					Dim T As CallCustomMethod = e.Clone(Of CallCustomMethod)
					T.EditTree(room, p.Name, p.GetValue(treeProperties), beats, ease)
					L.Add(T)
				End If
			Next
			Return L
		End Function
		<Extension> Public Sub EditTreeColor(e As CallCustomMethod, room As Byte, location As Boolean, color As String, beats As Single, ease As EaseType)
			e.MethodName = RoomFunctionCalling(room, NameOf(EditTreeColor), location, color, beats, ease)
		End Sub


		'<Extension> Public Sub MoveToPosition(e As Move, point As RDPoint)

		'End Sub
		<Extension> Public Function TextOnly(e As ShowDialogue) As String
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
		<Extension> Public Sub MovePositionMaintainVisual(e As Move, target As RDPointE)
			If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric Then
				Exit Sub
			End If
			e.Position = target
			e.Pivot = (e.VisualPosition() - New RDSizeE(target)).Rotate(e.Angle.Value.NumericValue)
		End Sub
		<Extension> Public Sub MovePositionMaintainVisual(e As MoveRoom, target As RDSizeE)
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric Then
				Exit Sub
			End If
			e.RoomPosition = target
			e.Pivot = (e.VisualPosition() - New RDSizeE(target)).Rotate(e.Angle.Value.NumericValue)
		End Sub
		<Extension> Public Function VisualPosition(e As Move) As RDPointE
			If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric OrElse e.Scale Is Nothing Then
				Return New RDPointE
			End If
			Dim previousPosition As RDPointE = e.Position
			Dim previousPivot As New RDPointE(
				e.Pivot?.X * e.Scale?.X * e.Parent.Size.Width / 100,
				e.Pivot?.Y * e.Scale?.Y * e.Parent.Size.Height / 100)
			Return previousPosition + New RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue))
		End Function
		<Extension> Public Function VisualPosition(e As MoveRoom) As RDPointE
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
				Return New RDPointE
			End If
			Dim previousPosition As RDPointE = e.RoomPosition
			Dim previousPivot As New RDPointE(e.Pivot?.X * e.Scale?.X, e.Pivot?.Y * e.Scale?.Y)
			Return previousPosition + New RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue))
		End Function
	End Module
End Namespace