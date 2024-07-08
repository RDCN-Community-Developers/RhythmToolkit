Imports System.Reflection
Imports System.Runtime.CompilerServices
Namespace Extensions
	Public Module Extensions
		Private Function GetRange(e As RDOrderedEventCollection, index As Index) As (start As Single, [end] As Single)
			Try
				Dim firstEvent = e.First
				Dim lastEvent = e.Last
				Return If(index.IsFromEnd, (
lastEvent.Beat._calculator.BarBeatToBeatOnly(lastEvent.Beat.BarBeat.bar - index.Value, 1),
lastEvent.Beat._calculator.BarBeatToBeatOnly(lastEvent.Beat.BarBeat.bar - index.Value + 1, 1)),
(firstEvent.Beat._calculator.BarBeatToBeatOnly(index.Value, 1),
firstEvent.Beat._calculator.BarBeatToBeatOnly(index.Value + 1, 1)))
			Catch ex As Exception
				Throw New ArgumentOutOfRangeException(NameOf(index))
			End Try
		End Function
		Private Function GetRange(e As RDOrderedEventCollection, range As Range) As (start As Single, [end] As Single)
			Try
				Dim firstEvent = e.First
				Dim lastEvent = e.Last
				Return (If(range.Start.IsFromEnd,
lastEvent.Beat._calculator.BarBeatToBeatOnly(lastEvent.Beat.BarBeat.bar - range.Start.Value, 1),
firstEvent.Beat._calculator.BarBeatToBeatOnly(Math.Max(range.Start.Value, 1), 1)),
If(range.End.IsFromEnd,
lastEvent.Beat._calculator.BarBeatToBeatOnly(lastEvent.Beat.BarBeat.bar - range.End.Value + 1, 1),
firstEvent.Beat._calculator.BarBeatToBeatOnly(range.End.Value + 1, 1)))
			Catch ex As Exception
				Throw New ArgumentOutOfRangeException(NameOf(range))
			End Try
		End Function
		''' <summary>
		''' Null or equal.
		''' </summary>
		''' <param name="e">one item.</param>
		''' <param name="obj">another item.</param>
		''' <returns></returns>
		<Extension> Public Function NullableEquals(e As Single?, obj As Single?) As Boolean
			Return (e.HasValue And obj.HasValue AndAlso e.Value = obj.Value) OrElse (Not e.HasValue AndAlso Not obj.HasValue)
		End Function
		''' <summary>
		''' 
		''' </summary>
		''' <param name="e"></param>
		''' <returns>
		''' <list type="table">
		''' <item>When neither item is empty,<br/>Returns true only if both are equal</item>
		''' <item>when one of the two is empty,<br/>Returns true.</item>
		''' <item>when both are empty,<br/>Returns false.</item>
		''' </list>
		''' </returns>
		<Extension> Public Function IsNullOrEmpty(e As String) As Boolean
			Return e Is Nothing OrElse e.Length = 0
		End Function
		''' <summary>
		''' Make strings follow the Upper Camel Case.
		''' </summary>
		''' <returns>The result.</returns>
		<Extension> Public Function ToUpperCamelCase(e As String) As String
			Dim S = e.ToArray
			S(0) = S(0).ToString.ToUpper
			Return String.Join("", S)
		End Function
		''' <summary>
		''' Make a specific key of a JObject follow the Upper Camel Case.
		''' </summary>
		<Extension> Friend Sub ToUpperCamelCase(e As Newtonsoft.Json.Linq.JObject, key As String)
			Dim token = e(key)
			e.Remove(key)
			e(key.ToUpperCamelCase) = token
		End Sub
		''' <summary>
		''' Make keys of JObject follow the Upper Camel Case.
		''' </summary>
		<Extension> Friend Sub ToUpperCamelCase(e As Newtonsoft.Json.Linq.JObject)
			For Each pair In e.DeepClone.ToObject(Of Newtonsoft.Json.Linq.JObject)
				e.ToUpperCamelCase(pair.Key)
			Next
		End Sub
		''' <summary>
		''' Make strings follow the Lower Camel Case.
		''' </summary>
		''' <returns>The result.</returns>
		<Extension> Public Function ToLowerCamelCase(e As String) As String
			Dim S = e.ToArray
			S(0) = S(0).ToString.ToLower
			Return String.Join("", S)
		End Function
		''' <summary>
		''' Make a specific key of a JObject follow the Lower Camel Case.
		''' </summary>
		<Extension> Friend Sub ToLowerCamelCase(e As Newtonsoft.Json.Linq.JObject, key As String)
			Dim token = e(key)
			e.Remove(key)
			e(key.ToLowerCamelCase) = token
		End Sub
		''' <summary>
		''' Make keys of JObject follow the Lower Camel Case.
		''' </summary>
		<Extension> Friend Sub ToLowerCamelCase(e As Newtonsoft.Json.Linq.JObject)
			For Each pair In e.DeepClone.ToObject(Of Newtonsoft.Json.Linq.JObject)
				e.ToLowerCamelCase(pair.Key)
			Next
		End Sub
		''' <summary>
		''' Convert color format from RGBA to ARGB
		''' </summary>
		<Extension> Public Function RgbaToArgb(Rgba As Int32) As Int32
			Return ((Rgba >> 8) And &HFFFFFF) Or ((Rgba << 24) And &HFF000000)
		End Function
		''' <summary>
		''' Convert color format from ARGB to RGBA
		''' </summary>
		<Extension> Public Function ArgbToRgba(Argb As Int32) As Int32
			Return ((Argb >> 24) And &HFF) Or ((Argb << 8) And &HFFFFFF00)
		End Function
		''' <summary>
		''' Convert <see cref="SkiaSharp.SKPoint"/> to <see cref="RDPointN"/>.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function ToRDPoint(e As SkiaSharp.SKPoint) As RDPointN
			Return New RDPointN(e.X, e.Y)
		End Function
		''' <summary>
		''' Convert <see cref="SkiaSharp.SKPointI"/> to <see cref="RDPointNI"/>.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function ToRDPointI(e As SkiaSharp.SKPointI) As RDPointNI
			Return New RDPointNI(e.X, e.Y)
		End Function
		''' <summary>
		''' Convert <see cref="SkiaSharp.SKSize"/> to <see cref="RDSize"/>.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function ToRDSize(e As SkiaSharp.SKSize) As RDSizeN
			Return New RDSizeN(e.Width, e.Height)
		End Function
		''' <summary>
		''' Convert <see cref="SkiaSharp.SKSizeI"/> to <see cref="RDSizeI"/>.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function ToRDSizeI(e As SkiaSharp.SKSizeI) As RDSizeNI
			Return New RDSizeNI(e.Width, e.Height)
		End Function
		''' <summary>
		''' Convert <see cref="RDPoint"/> to <see cref="SkiaSharp.SKSizeI"/>.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function ToSKPoint(e As RDPointN) As SkiaSharp.SKPoint
			Return New SkiaSharp.SKPoint(e.X, e.Y)
		End Function
		''' <summary>
		''' Convert <see cref="RDPointI"/> to <see cref="SkiaSharp.SKPointI"/>.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function ToSKPointI(e As RDPointNI) As SkiaSharp.SKPointI
			Return New SkiaSharp.SKPointI(e.X, e.Y)
		End Function
		''' <summary>
		''' Convert <see cref="RDSize"/> to <see cref="SkiaSharp.SKSize"/>.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function ToSKSize(e As RDSizeN) As SkiaSharp.SKSize
			Return New SkiaSharp.SKSize(e.Width, e.Height)
		End Function
		''' <summary>
		''' Convert <see cref="RDSizeI"/> to <see cref="SkiaSharp.SKSizeI"/>.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function ToSKSizeI(e As RDSizeNI) As SkiaSharp.SKSizeI
			Return New SkiaSharp.SKSizeI(e.Width, e.Height)
		End Function
		''' <summary>
		''' Calculate the fraction of <paramref name="splitBase"/> equal to the nearest floating point number.
		''' <example>
		''' <code>
		''' 2.236f.FixFraction(4) == 2.25f
		''' float.Pi.FixFraction(5) == 3.2f
		''' float.E.Fixfraction(2) == 2.5f
		''' </code>
		''' </example>
		''' </summary>
		''' <param name="number">The float number.</param>
		''' <param name="splitBase">Indicate what fraction this is.</param>
		''' <returns></returns>
		<Extension> Public Function FixFraction(number As Single, splitBase As UInteger) As Single
			Return Math.Round(number * splitBase) / splitBase
		End Function
#Region "RD"
		''' <summary>
		''' Add a range of events.
		''' </summary>
		''' <param name="items"></param>
		<Extension> Public Sub AddRange(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), items As IEnumerable(Of T))
			For Each item In items
				e.Add(item)
			Next
		End Sub
		''' <summary>
		''' Filters a sequence of events based on a predicate.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.eventsBeatOrder.SelectMany(Function(i) i.Value).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events located at a time.
		''' </summary>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), beat As RDBeat) As IEnumerable(Of T)
			Dim value As RDTypedList(Of RDBaseEvent) = Nothing
			If e.eventsBeatOrder.TryGetValue(beat, value) Then
				Return value
			End If
			Return value
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of time.
		''' </summary>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key < endBeat) _
.SkipWhile(Function(i) i.Key < startBeat) _
.SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a bar.
		''' </summary>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), bar As Index) As IEnumerable(Of T)
			Dim rg = GetRange(e, bar)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of beat.
		''' </summary>
		''' <param name="range">Specified beat range.</param>
		''' <returns></returns>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), range As RDRange) As IEnumerable(Of T)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) If(i.Key < range.End, True)) _
.SkipWhile(Function(i) If(i.Key < range.Start, False)) _
.SelectMany(Function(i) i.Value)
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of bar.
		''' </summary>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), bars As Range) As IEnumerable(Of T)
			Dim rg = GetRange(e, bars)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.Where(beat).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified range of beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.Where(startBeat, endBeat).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified range of beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As RDRange) As IEnumerable(Of T)
			Return e.Where(range).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified bar.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), bar As Index) As IEnumerable(Of T)
			Return e.Where(bar).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified range of bar.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), bars As Range) As IEnumerable(Of T)
			Return e.Where(bars).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events in specified event type.
		''' </summary>
		''' <typeparam name="T"></typeparam>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection) As IEnumerable(Of T)
			Dim enums = ConvertToRDEnums(Of T)()
			Return e.eventsBeatOrder _
.Where(Function(i) i.Value._types _
.Any(Function(j) enums.Contains(j))) _
.SelectMany(Function(i) i.Value).OfType(Of T)
		End Function
		''' <summary>
		''' Filters a sequence of events located at a beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, beat As RDBeat) As IEnumerable(Of T)
			Dim value As RDTypedList(Of RDBaseEvent) = Nothing
			If e.eventsBeatOrder.TryGetValue(beat, value) Then
				Return value.OfType(Of T)
			End If
			Return value
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key < endBeat) _
.SkipWhile(Function(i) i.Key < startBeat) _
.SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, bar As Index) As IEnumerable(Of T)
			Dim rg = GetRange(e, bar)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, range As RDRange) As IEnumerable(Of T)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) If(i.Key < range.End, True)) _
.SkipWhile(Function(i) If(i.Key < range.Start, False)) _
.SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, bars As Range) As IEnumerable(Of T)
			Dim rg = GetRange(e, bars)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.OfType(Of T))
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.Where(Of T)().Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.Where(Of T)(beat).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As IEnumerable(Of T)
			Return e.Where(Of T)(startBeat, endBeat).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), range As RDRange) As IEnumerable(Of T)
			Return e.Where(Of T)(range).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), bar As Index) As IEnumerable(Of T)
			Return e.Where(Of T)(bar).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function Where(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), bars As Range) As IEnumerable(Of T)
			Return e.Where(Of T)(bars).Where(predicate)
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a time.
		''' </summary>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(beat)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of time.
		''' </summary>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(startBeat, endBeat)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a bar.
		''' </summary>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), bar As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(bar)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of beat.
		''' </summary>
		''' <param name="range">Specified beat range.</param>
		''' <returns></returns>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of bar.
		''' </summary>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), bars As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(bars)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, beat)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified range of beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, startBeat, endBeat)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified range of beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified bar.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), bar As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, bar)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified range of bar.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), bars As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, bars)))
		End Function
		''' <summary>
		''' Remove a sequence of events in specified event type.
		''' </summary>
		''' <typeparam name="T"></typeparam>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)()))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(beat)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(startBeat, endBeat)))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(range)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, bar As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(bar)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, bars As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(bars)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean)) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate located at a beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), beat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, beat)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), startBeat As RDBeat, endBeat As RDBeat) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, startBeat, endBeat)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, range)))
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), bar As Index) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, bar)))
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function RemoveAll(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), bars As Range) As Integer
			Return e.RemoveRange(New List(Of T)(e.Where(Of T)(predicate, bars)))
		End Function
		''' <summary>
		''' Returns the first element of the collection.
		''' </summary>
		<Extension> Public Function First(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T)) As T
			Return e.eventsBeatOrder.First.Value.First
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function First(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.First(predicate)
		End Function
		''' <summary>
		''' Returns the first element of the collection in specified event type.
		''' </summary>
		<Extension> Public Function First(Of T As RDBaseEvent)(e As RDOrderedEventCollection) As T
			Return e.Where(Of T).First
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition in specified event type.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function First(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).First(predicate)
		End Function
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T)) As T
			Return e.eventsBeatOrder.FirstOrDefault.Value?.FirstOrDefault
		End Function
		''' <summary>
		''' Returns the first element of the collection, or <paramref name="defaultValue"/> if collection contains no elements.
		''' </summary>
		''' <param name="defaultValue">The default value to return if contains no elements.</param>
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), defaultValue As T) As T
			Return e.ConcatAll.FirstOrDefault(defaultValue)
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.FirstOrDefault(predicate)
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.ConcatAll.FirstOrDefault(predicate, defaultValue)
		End Function
		''' <summary>
		''' Returns the first element of the collection in specified event type, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection) As T
			Return e.Where(Of T).FirstOrDefault()
		End Function
		''' <summary>
		''' Returns the first element of the collection in specified event type, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection, defaultValue As T) As T
			Return e.Where(Of T).FirstOrDefault(defaultValue)
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition in specified event type, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).FirstOrDefault(predicate)
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition in specified event type, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function FirstOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.Where(Of T).FirstOrDefault(predicate, defaultValue)
		End Function
		''' <summary>
		''' Returns the last element of the collection.
		''' </summary>
		<Extension> Public Function Last(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T)) As T
			Return e.eventsBeatOrder.Last.Value.Last
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function Last(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.Last(predicate)
		End Function
		''' <summary>
		''' Returns the last element of the collection in specified event type.
		''' </summary>
		<Extension> Public Function Last(Of T As RDBaseEvent)(e As RDOrderedEventCollection) As T
			Return e.Where(Of T).Last
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition in specified event type.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function Last(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).Last(predicate)
		End Function
		''' <summary>
		''' Returns the last element of the collection, or <see langword="null"/> if collection contains no elements.
		''' </summary>
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T)) As T
			Return e.eventsBeatOrder.LastOrDefault.Value?.LastOrDefault()
		End Function
		''' <summary>
		''' Returns the last element of the collection, or <paramref name="defaultValue"/> if collection contains no elements.
		''' </summary>
		''' <param name="defaultValue">The default value to return if contains no elements.</param>
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), defaultValue As T) As T
			Return e.ConcatAll.LastOrDefault(defaultValue)
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As T
			Return e.ConcatAll.LastOrDefault(predicate)
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.ConcatAll.LastOrDefault(predicate, defaultValue)
		End Function
		''' <summary>
		''' Returns the last element of the collection in specified event type, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection) As T
			Return e.Where(Of T).LastOrDefault()
		End Function
		''' <summary>
		''' Returns the last element of the collection in specified event type, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection, defaultValue As T) As T
			Return e.Where(Of T).LastOrDefault(defaultValue)
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition in specified event type, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean)) As T
			Return e.Where(Of T).LastOrDefault(predicate)
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition in specified event type, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function LastOrDefault(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return e.Where(Of T).LastOrDefault(predicate, defaultValue)
		End Function
		''' <summary>
		''' Returns events from a collection as long as it less than or equal to <paramref name="beat"/>.
		''' </summary>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Iterator Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), beat As RDBeat) As IEnumerable(Of T)
			For Each item In e
				If item.Beat <= beat Then
					Yield item
				Else
					Exit For
				End If
			Next
		End Function
		''' <summary>
		''' Returns events from a collection as long as it less than or equal to <paramref name="bar"/>.
		''' </summary>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), bar As Index) As IEnumerable(Of T)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return e.TakeWhile(
				If(bar.IsFromEnd,
				lastEvent.Beat._calculator.BeatOf(lastEvent.Beat.BarBeat.bar - bar.Value + 1, 1),
				firstEvent.Beat._calculator.BeatOf(bar.Value + 1, 1)))
		End Function
		''' <summary>
		''' Returns events from a collection as long as a specified condition is true.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.eventsBeatOrder.SelectMany(Function(i) i.Value).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection as long as a specified condition is true and also less than or equal to <paramref name="beat"/>.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(beat).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection as long as a specified condition is true and also less than or equal to <paramref name="bar"/>.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), predicate As Func(Of T, Boolean), bar As Index) As IEnumerable(Of T)
			Return e.TakeWhile(bar).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection in specified event type as long as it less than or equal to <paramref name="beat"/>.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Iterator Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection, beat As RDBeat) As IEnumerable(Of T)
			For Each item In e.Where(Of T)
				If item.Beat <= beat Then
					Yield item
				Else
					Exit For
				End If
			Next
		End Function
		''' <summary>
		''' Returns events from a collection in specified event type as long as it less than or in <paramref name="bar"/>.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection, bar As Index) As IEnumerable(Of T)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return e.TakeWhile(Of T)(
				If(bar.IsFromEnd,
				lastEvent.Beat._calculator.BeatOf(lastEvent.Beat.BarBeat.bar - bar.Value + 1, 1),
				firstEvent.Beat._calculator.BeatOf(bar.Value + 1, 1)))
		End Function
		''' <summary>
		''' Returns events from a collection in specified event type as long as a specified condition is true.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">Specified condition.</param>
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return e.Where(Of T).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection in specified event type as long as a specified condition is true and its beat less than or equal to <paramref name="beat"/>.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">Specified condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), beat As RDBeat) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(beat).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection in specified event type as long as a specified condition is true and its beat less than or equal to <paramref name="bar"/>.
		''' </summary>
		''' <typeparam name="T">Specified event type.</typeparam>
		''' <param name="predicate">Specified condition.</param>
		''' <param name="bar">Specified beat.</param>
		<Extension> Public Function TakeWhile(Of T As RDBaseEvent)(e As RDOrderedEventCollection, predicate As Func(Of T, Boolean), bar As Index) As IEnumerable(Of T)
			Return e.TakeWhile(Of T)(bar).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Remove a range of events.
		''' </summary>
		''' <param name="items">A range of events.</param>
		''' <returns>The number of events successfully removed.</returns>
		<Extension> Public Function RemoveRange(Of T As RDBaseEvent)(e As RDOrderedEventCollection, items As IEnumerable(Of T)) As Integer
			Dim count As Integer = 0
			For Each item In items
				count += e.Remove(item)
			Next
			Return count
		End Function
		''' <summary>
		''' Remove a range of events.
		''' </summary>
		''' <param name="items">A range of events.</param>
		''' <returns>The number of events successfully removed.</returns>
		<Extension> Public Function RemoveRange(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), items As IEnumerable(Of T)) As Integer
			Dim count As Integer = 0
			For Each item In items
				count += e.Remove(item)
			Next
			Return count
		End Function
		''' <summary>
		''' Determine if <paramref name="item1"/> is in front of <paramref name="item2"/>
		''' </summary>
		''' <returns><list type="table">
		''' <item>If <paramref name="item1"/> is in front of <paramref name="item2"/>, <see langword="true"/></item>
		''' <item>Else, <see langword="false"/></item>
		''' </list></returns>
		<Extension> Public Function IsInFrontOf(e As RDOrderedEventCollection, item1 As RDBaseEvent, item2 As RDBaseEvent) As Boolean
			Return item1.Beat < item2.Beat OrElse
				(item1.Beat.BeatOnly = item2.Beat.BeatOnly AndAlso
				e.eventsBeatOrder(item1.Beat).BeforeThan(item1, item2))
		End Function
		''' <summary>
		''' Determine if <paramref name="item1"/> is after <paramref name="item2"/>
		''' </summary>
		''' <returns><list type="table">
		''' <item>If <paramref name="item1"/> is after <paramref name="item2"/>, <see langword="true"/></item>
		''' <item>Else, <see langword="false"/></item>
		''' </list></returns>
		<Extension> Public Function IsBehind(e As RDOrderedEventCollection, item1 As RDBaseEvent, item2 As RDBaseEvent) As Boolean
			Dim s = item1.Beat.Equals(CObj(item2.Beat))
			Return item1.Beat > item2.Beat OrElse
				(item1.Beat.BeatOnly = item2.Beat.BeatOnly AndAlso
				e.eventsBeatOrder(item1.Beat).BeforeThan(item2, item1))
		End Function
		''' <summary>
		''' Get all the hit of the level.
		''' </summary>
		<Extension> Public Function GetHitBeat(e As RDLevel) As IEnumerable(Of RDHit)
			Dim L As New List(Of RDHit)
			For Each item In e.Rows
				L.AddRange(item.HitBeats)
			Next
			Return L
		End Function
		''' <summary>
		''' Get all the hit event of the level.
		''' </summary>
		<Extension> Public Function GetHitEvents(e As RDLevel) As IEnumerable(Of RDBaseBeat)
			Return e.Where(Of RDBaseBeat)(Function(i) i.IsHitable)
		End Function
		''' <summary>
		''' Get all events with the specified tag.
		''' </summary>
		''' <param name="name">Tag name.</param>
		''' <param name="strict">Indicates whether the label is strictly matched.
		''' If <see langword="true"/>, determine If it contains the specified tag.
		''' If <see langword="false"/>, determine If it Is equal to the specified tag.</param>
		''' <returns>An <see cref="IGrouping"/>, categorized by tag name.</returns>
		<Extension> Public Function GetTaggedEvents(Of T As RDBaseEvent)(e As RDOrderedEventCollection(Of T), name As String, strict As Boolean) As IEnumerable(Of IGrouping(Of String, T))
			If name Is Nothing Then
				Return Nothing
			End If
			If strict Then
				Return e.Where(Function(i) i.Tag = name).GroupBy(Function(i) i.Tag)
			Else
				Return e.Where(Function(i) If(i.Tag?.Contains(name), False)).GroupBy(Function(i) i.Tag)
			End If
		End Function
		''' <summary>
		''' Get all classic beat events and their variants.
		''' </summary>
		<Extension> Private Function ClassicBeats(e As RDRow) As IEnumerable(Of RDBaseBeat)
			Return e.Where(Of RDBaseBeat)(Function(i) i.Type = RDEventType.AddClassicBeat Or
				i.Type = RDEventType.AddFreeTimeBeat Or
				i.Type = RDEventType.PulseFreeTimeBeat)
		End Function
		''' <summary>
		''' Get all oneshot beat events.
		''' </summary>
		<Extension> Private Function OneshotBeats(e As RDRow) As IEnumerable(Of RDBaseBeat)
			Return e.Where(Of RDBaseBeat)(Function(i) i.Type = RDEventType.AddOneshotBeat)
		End Function
		''' <summary>
		''' Get all hits of all beats.
		''' </summary>
		<Extension> Public Function HitBeats(e As RDRow) As IEnumerable(Of RDHit)
			Select Case e.RowType
				Case RDRowType.Classic
					Return e.ClassicBeats().SelectMany(Function(i) i.HitTimes)
				Case RDRowType.Oneshot
					Return e.OneshotBeats().SelectMany(Function(i) i.HitTimes)
				Case Else
					Throw New RhythmBaseException("How?")
			End Select
		End Function
		''' <summary>
		''' Get an instance of the beat associated with the level.
		''' </summary>
		''' <param name="beatOnly">Total number of 1-based beats.</param>
		<Extension> Public Function BeatOf(e As RDLevel, beatOnly As Single) As RDBeat
			Return e.Calculator.BeatOf(beatOnly)
		End Function
		''' <summary>
		''' Get an instance of the beat associated with the level.
		''' </summary>
		''' <param name="bar">The 1-based bar.</param>
		''' <param name="beat">The 1-based beat of the bar.</param>
		<Extension> Public Function BeatOf(e As RDLevel, bar As UInteger, beat As Single) As RDBeat
			Return e.Calculator.BeatOf(bar, beat)
		End Function
		''' <summary>
		''' Get an instance of the beat associated with the level.
		''' </summary>
		''' <param name="timeSpan">Total time span of the beat.</param>
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
		''' <summary>
		''' Get all beats of the row.
		''' </summary>
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
#End Region
#Region "AD"
		''' <summary>
		''' Add a range of events.
		''' </summary>
		<Extension> Public Sub AddRange(e As ADLevel, items As IEnumerable(Of ADTile))
			For Each item In items
				e.Add(item)
			Next
		End Sub
		''' <summary>
		''' Add a range of events.
		''' </summary>
		<Extension> Public Sub AddRange(e As ADTile, items As IEnumerable(Of ADBaseTileEvent))
			For Each item In items
				e.Add(item)
			Next
		End Sub
#End Region
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




		''' <summary>
		''' Check if another event is in front of itself, including events of the same beat but executed before itself.
		''' </summary>
		<Extension> Public Function IsInFrontOf(e As RDBaseEvent, item As RDBaseEvent) As Boolean
			Return e.Beat.baseLevel.IsInFrontOf(e, item)
		End Function
		''' <summary>
		''' Check if another event is after itself, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function IsBehind(e As RDBaseEvent, item As RDBaseEvent) As Boolean
			Return e.Beat.baseLevel.IsBehind(e, item)
		End Function
		''' <summary>
		''' Returns all previous events of the same type, including events of the same beat but executed before itself.
		''' </summary>
		<Extension> Public Function Before(Of T As RDBaseEvent)(e As T) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(e.Beat.baseLevel.DefaultBeat, e.Beat)
		End Function
		''' <summary>
		''' Returns all previous events of the specified type, including events of the same beat but executed before itself.
		''' </summary>
		<Extension> Public Function Before(Of T As RDBaseEvent)(e As RDBaseEvent) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(e.Beat.baseLevel.DefaultBeat, e.Beat)
		End Function
		''' <summary>
		''' Returns all events of the same type that follow, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function After(Of T As RDBaseEvent)(e As T) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(Function(i) i.Beat > e.Beat)
		End Function
		''' <summary>
		''' Returns all events of the specified type that follow, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function After(Of T As RDBaseEvent)(e As RDBaseEvent) As IEnumerable(Of T)
			Return e.Beat.baseLevel.Where(Of T)(Function(i) i.Beat > e.Beat)
		End Function
		''' <summary>
		''' Returns the previous event of the same type, including events of the same beat but executed before itself.
		''' </summary>
		<Extension> Public Function Front(Of T As RDBaseEvent)(e As T) As T
			Return e.Before.Last
		End Function
		''' <summary>
		''' Returns the previous event of the specified type, including events of the same beat but executed before itself.
		''' </summary>
		<Extension> Public Function Front(Of T As RDBaseEvent)(e As RDBaseEvent) As T
			Return e.Before(Of T).Last
		End Function
		''' <summary>
		''' Returns the previous event of the same type, including events of the same beat but executed before itself. Returns <see langword="null"/> if it does not exist.
		''' </summary>
		<Extension> Public Function FrontOrDefault(Of T As RDBaseEvent)(e As T) As T
			Return e.Before.LastOrDefault
		End Function
		''' <summary>
		''' Returns the previous event of the specified type, including events of the same beat but executed before itself. Returns <see langword="null"/> if it does not exist.
		''' </summary>
		<Extension> Public Function FrontOrDefault(Of T As RDBaseEvent)(e As RDBaseEvent) As T
			Return e.Before(Of T).LastOrDefault
		End Function
		''' <summary>
		''' Returns the next event of the same type, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function [Next](Of T As RDBaseEvent)(e As T) As T
			Return e.After().First
		End Function
		''' <summary>
		''' Returns the next event of the specified type, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function [Next](Of T As RDBaseEvent)(e As RDBaseEvent) As T
			Return e.After(Of T).First
		End Function
		''' <summary>
		''' Returns the next event of the same type, including events of the same beat but executed after itself. Returns <see langword="null"/> if it does not exist.
		''' </summary>
		<Extension> Public Function NextOrDefault(Of T As RDBaseEvent)(e As T) As T
			Return e.After().FirstOrDefault
		End Function
		''' <summary>
		''' Returns the next event of the specified type, including events of the same beat but executed after itself. Returns <see langword="null"/> if it does not exist.
		''' </summary>
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





		<Extension> Public Function DurationOffset(beat As RDBeat, duration As Single) As RDBeat
			Dim setBPM = beat.baseLevel.First(Of RDSetBeatsPerMinute)(Function(i) i.Beat > beat)
			If beat.BarBeat.bar = setBPM.Beat.BarBeat.bar Then
				Return beat + duration
			Else
				Return beat + TimeSpan.FromMinutes(duration / beat.BPM)
			End If
		End Function
		''' <summary>
		''' Shallow copy.
		''' </summary>
		<Extension> Public Function MemberwiseClone(Of T As RDBaseEvent)(e As T) As T
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
		End Function
		<Extension> Friend Function MClone(e As Object) As Object
			If e Is Nothing Then
				Return Nothing
			End If
			Dim type As Type = e.GetType
			Dim copy = Activator.CreateInstance(type)
			Dim properties() As PropertyInfo = type.GetProperties(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
			For Each p In properties
				If p.CanWrite Then
					p.SetValue(copy, p.GetValue(e))
				End If
			Next
			Return copy
		End Function
		''' <summary>
		''' Get current player of the beat event.
		''' </summary>
		''' <param name="e"></param>
		''' <returns></returns>
		<Extension> Public Function Player(e As RDBaseBeat) As RDPlayerType
			Return If(
			e.Beat.baseLevel.LastOrDefault(Of RDChangePlayersRows)(Function(i) i.Active AndAlso i.Players(e.Index) <> RDPlayerType.NoChange)?.Players(e.Index),
			e.Parent.Player)
		End Function
		''' <summary>
		''' Get the pulse sound effect of row beat event.
		''' </summary>
		''' <returns>The sound effect of row beat event.</returns>
		<Extension> Public Function BeatSound(e As RDBaseBeat) As Components.RDAudio
			Return If(
				e.Parent.LastOrDefault(Of RDSetBeatSound)(Function(i) i.Beat < e.Beat AndAlso i.Active)?.Sound,
				e.Parent.Sound)
		End Function
		''' <summary>
		''' Get the hit sound effect of row beat event.
		''' </summary>
		''' <returns>The sound effect of row beat event.</returns>
		<Extension> Public Function HitSound(e As RDBaseBeat) As Components.RDAudio
			Dim DefaultAudio = New Components.RDAudio With {.Filename = "sndClapHit", .Offset = TimeSpan.Zero, .Pan = 100, .Pitch = 100, .Volume = 100}
			Select Case e.Player
				Case RDPlayerType.P1
					Return If(
						e.Beat.baseLevel.LastOrDefault(Of RDSetClapSounds)(Function(i) i.Active AndAlso i.P1Sound IsNot Nothing)?.P1Sound,
						DefaultAudio)
				Case RDPlayerType.P2
					Return If(
						e.Beat.baseLevel.LastOrDefault(Of RDSetClapSounds)(Function(i) i.Active AndAlso i.P2Sound IsNot Nothing)?.P2Sound,
						DefaultAudio)
				Case RDPlayerType.CPU
					Return If(
						e.Beat.baseLevel.LastOrDefault(Of RDSetClapSounds)(Function(i) i.Active AndAlso i.CpuSound IsNot Nothing)?.CpuSound,
						DefaultAudio)
				Case Else
					Return Nothing
			End Select
		End Function
		''' <summary>
		''' Get the special tag of the tag event.
		''' </summary>
		''' <returns>special tags.</returns>
		<Extension> Public Function SpetialTags(e As RDTagAction) As RDTagAction.SpecialTag()
			Return [Enum].GetValues(Of RDTagAction.SpecialTag).Where(Function(i) e.ActionTag.Contains($"[{i}]"))
		End Function
		''' <summary>
		''' Convert beat pattern to string.
		''' </summary>
		''' <returns>The pattern string.</returns>
		<Extension> Public Function Pattern(e As RDAddClassicBeat) As String
			Return Utils.GetPatternString(e.RowXs)
		End Function
		''' <summary>
		''' Get the actual beat pattern.
		''' </summary>
		''' <returns>The actual beat pattern.</returns>
		<Extension> Public Function RowXs(e As RDAddClassicBeat) As LimitedList(Of Patterns)
			If e.SetXs Is Nothing Then
				Dim X = e.Parent.LastOrDefault(Of RDSetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New RDSetRowXs)
				Return X.Pattern
			Else
				Dim T As New LimitedList(Of Patterns)(6, Patterns.None)
				Select Case e.SetXs
					Case RDAddClassicBeat.ClassicBeatPatterns.ThreeBeat
						T(1) = Patterns.X
						T(2) = Patterns.X
						T(4) = Patterns.X
						T(5) = Patterns.X
					Case RDAddClassicBeat.ClassicBeatPatterns.FourBeat
						T(1) = Patterns.X
						T(3) = Patterns.X
						T(5) = Patterns.X
					Case Else
						Throw New RhythmBaseException("How?")
				End Select
				Return T
			End If
		End Function
		''' <summary>
		''' Get the total length of the oneshot.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function Length(e As RDAddOneshotBeat) As Single
			Return e.Tick * e.Loops + e.Interval * e.Loops - 1
		End Function
		''' <summary>
		''' Get the total length of the classic beat.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function Length(e As RDAddClassicBeat) As Single
			Dim SyncoSwing = e.Parent.LastOrDefault(Of RDSetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New RDSetRowXs).SyncoSwing
			Return e.Tick * 6 - If(SyncoSwing = 0, 0.5, SyncoSwing) * e.Tick
		End Function
		''' <summary>
		''' Check if it can be hit by player.
		''' </summary>
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
		''' <summary>
		''' Check if it can be hit by player.
		''' </summary>
		<Extension> Public Function IsHitable(e As RDAddFreeTimeBeat) As Boolean
			Return e.Pulse = 6
		End Function
		''' <summary>
		''' Check if it can be hit by player.
		''' </summary>
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
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As RDAddClassicBeat) As IEnumerable(Of RDHit)
			Return New List(Of RDHit) From {New RDHit(e, e.GetBeat(6), e.Hold)}.AsEnumerable
		End Function
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As RDAddOneshotBeat) As IEnumerable(Of RDHit)
			Dim L As New List(Of RDHit)
			For i As UInteger = 0 To e.Loops
				For j As SByte = 0 To e.Subdivisions - 1
					L.Add(New RDHit(e, New RDBeat(e._beat._calculator, e._beat.BeatOnly + i * e.Interval + e.Tick + e.Delay + j * (e.Tick / e.Subdivisions)), 0))
				Next
			Next
			Return L.AsEnumerable
		End Function
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As RDAddFreeTimeBeat) As IEnumerable(Of RDHit)
			If e.Pulse = 6 Then
				Return New List(Of RDHit) From {New RDHit(e, e.Beat, e.Hold)}.AsEnumerable
			End If
			Return New List(Of RDHit)
		End Function
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As RDPulseFreeTimeBeat) As IEnumerable(Of RDHit)
			If e.IsHitable Then
				Return New List(Of RDHit) From {New RDHit(e, e.Beat, e.Hold)}
			End If
			Return New List(Of RDHit)
		End Function
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As RDBaseBeat) As IEnumerable(Of RDHit)
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
					Return Array.Empty(Of RDHit).AsEnumerable
			End Select
		End Function
		''' <summary>
		''' Returns the pulse beat of the specified 0-based index.
		''' </summary>
		''' <exception cref="RhythmBaseException">THIS IS 7TH BEAT GAMES!</exception>
		<Extension> Public Function GetBeat(e As RDAddClassicBeat, index As Byte) As RDBeat
			Dim x = e.Parent.LastOrDefault(Of RDSetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New RDSetRowXs)
			Dim Synco As Single
			If 0 <= x.SyncoBeat AndAlso x.SyncoBeat < index Then
				Synco = If(x.SyncoSwing = 0, 0.5, x.SyncoSwing)
			Else
				Synco = 0
			End If
			If index >= 7 Then
				Throw New RhythmBaseException("THIS IS 7TH BEAT GAMES!")
			End If
			Return e.Beat.DurationOffset(e.Tick * (index - Synco))
		End Function
		''' <summary>
		''' Converts Xs patterns to string form.
		''' </summary>
		<Extension> Public Function GetPatternString(e As RDSetRowXs) As String
			Return Utils.GetPatternString(e.Pattern)
		End Function
		''' <summary>
		''' Creates a new <see cref="RDAdvanceText"/> subordinate to <see cref="RDFloatingText"/> at the specified beat. The new event created will be attempted to be added to the <see cref="RDFloatingText"/>'s source level.
		''' </summary>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function CreateAdvanceText(e As RDFloatingText, beat As RDBeat) As RDAdvanceText
			Dim A As New RDAdvanceText With {.Parent = e, .Beat = beat.WithoutBinding}
			e.Children.Add(A)
			Return A
		End Function
		''' <summary>
		''' Get the sequence of <see cref="RDPulseFreeTimeBeat"/> belonging to this <see cref="RDAddFreeTimeBeat"/>, return all of the <see cref="RDPulseFreeTimeBeat"/> from the time the pulse was created to the time it was removed or hit.
		''' </summary>
		<Extension> Public Function GetPulses(e As RDAddFreeTimeBeat) As IEnumerable(Of RDPulseFreeTimeBeat)
			Dim Result As New List(Of RDPulseFreeTimeBeat)
			Dim pulse As Byte = e.Pulse
			For Each item In e.Parent.Where(Of RDPulseFreeTimeBeat)(Function(i) i.Active AndAlso e.IsInFrontOf(i))
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
		''' <summary>
		''' Generate split event instances.
		''' </summary>
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
		''' <summary>
		''' Generate split event instances.
		''' </summary>
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
		''' <summary>
		''' Generate split event instances. Follow the most recently activated Xs.
		''' </summary>
		<Extension> Public Function Split(e As RDAddClassicBeat) As IEnumerable(Of RDBaseBeat)
			Dim x = e.Parent.LastOrDefault(Of RDSetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New RDSetRowXs)
			Return e.Split(x)
		End Function
		''' <summary>
		''' Generate split event instances.
		''' </summary>
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
		''' <summary>
		''' Getting controlled events.
		''' </summary>
		<Extension> Public Function ControllingEvents(e As RDTagAction) As IEnumerable(Of IGrouping(Of String, RDBaseEvent))
			Return e.Beat.baseLevel.GetTaggedEvents(e.ActionTag, e.Action.HasFlag(RDTagAction.Actions.All))
		End Function
		''' <summary>
		''' Remove auxiliary symbols.
		''' </summary>
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
		''' <summary>
		''' Specifies the position of the image. This method changes both the pivot and the angle to keep the image visually in its original position.
		''' </summary>
		''' <param name="target">Specified position. </param>
		<Extension> Public Sub MovePositionMaintainVisual(e As RDMove, target As RDPointE)
			If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric Then
				Exit Sub
			End If
			e.Position = target
			e.Pivot = (e.VisualPosition() - New RDSizeE(target)).Rotate(e.Angle.Value.NumericValue)
		End Sub
		''' <summary>
		''' Specifies the position of the image. This method changes both the pivot and the angle to keep the image visually in its original position.
		''' </summary>
		''' <param name="target">Specified position. </param>
		<Extension> Public Sub MovePositionMaintainVisual(e As RDMoveRoom, target As RDSizeE)
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric Then
				Exit Sub
			End If
			e.RoomPosition = target
			e.Pivot = (e.VisualPosition() - New RDSizeE(target)).Rotate(e.Angle.Value.NumericValue)
		End Sub
		''' <summary>
		''' The visual position of the lower left corner of the image.
		''' </summary>
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
		''' <summary>
		''' The visual position of the lower left corner of the image.
		''' </summary>
		<Extension> Public Function VisualPosition(e As RDMoveRoom) As RDPointE
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
				Return New RDPointE
			End If
			Dim previousPosition As RDPointE = e.RoomPosition
			Dim previousPivot As New RDPointE(e.Pivot?.X * e.Scale?.Width, e.Pivot?.Y * e.Scale?.Height)
			Return previousPosition + New RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue))
		End Function
	End Module
End Namespace