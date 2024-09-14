Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports RhythmBase.Adofai.Events
Imports RhythmBase.Adofai.Components
Namespace Extensions
	Public Module Extensions
		Private Function GetRange(e As OrderedEventCollection, index As Index) As (start As Single, [end] As Single)
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
		Private Function GetRange(e As OrderedEventCollection, range As Range) As (start As Single, [end] As Single)
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
		''' <returns>
		''' <list type="table">
		''' <item>When neither item is empty,<br/>Returns true only if both are equal</item>
		''' <item>when one of the two is empty,<br/>Returns true.</item>
		''' <item>when both are empty,<br/>Returns false.</item>
		''' </list>
		''' </returns>
		<Extension> Public Function NullableEquals(e As Single?, obj As Single?) As Boolean
			Return (e.HasValue And obj.HasValue AndAlso e.Value = obj.Value) OrElse (Not e.HasValue AndAlso Not obj.HasValue)
		End Function
		''' <summary>
		''' 
		''' </summary>
		''' <param name="e"></param>
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
		''' <param name="beat">The float number.</param>
		''' <param name="splitBase">Indicate what fraction this is.</param>
		''' <returns></returns>
		<Extension> Public Function FixFraction(beat As Single, splitBase As UInteger) As Single
			Return Math.Round(beat * splitBase) / splitBase
		End Function
		''' <summary>
		''' Calculate the fraction of <paramref name="splitBase"/> equal to the nearest floating point number.
		''' </summary>
		<Extension> Public Function FixFraction(beat As Beat, splitBase As UInteger) As Beat
			Return New Beat(beat.BeatOnly.FixFraction(splitBase))
		End Function
		''' <summary>
		''' Converting enumeration constants to in-game colors。
		''' </summary>
		''' <returns>The in-game color.</returns>
		<Extension> Public Function ToColor(e As Bookmark.BookmarkColors) As SkiaSharp.SKColor
			Select Case e
				Case Bookmark.BookmarkColors.Blue
					Return New SkiaSharp.SKColor(11, 125, 206)
				Case Bookmark.BookmarkColors.Red
					Return New SkiaSharp.SKColor(219,41,41)
				Case Bookmark.BookmarkColors.Yellow
					Return New SkiaSharp.SKColor(212, 212, 51)
				Case Bookmark.BookmarkColors.Green
					Return New SkiaSharp.SKColor(54, 215, 54)
			End Select
		End Function
#Region "RD"
		''' <summary>
		''' Add a range of events.
		''' </summary>
		''' <param name="items"></param>
		<Extension> Public Sub AddRange(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), items As IEnumerable(Of TEvent))
			For Each item In items
				e.Add(item)
			Next
		End Sub
		''' <summary>
		''' Filters a sequence of events based on a predicate.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean)) As IEnumerable(Of TEvent)
			Return e.eventsBeatOrder.SelectMany(Function(i) i.Value).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events located at a time.
		''' </summary>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), beat As Beat) As IEnumerable(Of TEvent)
			Dim value As TypedEventCollection(Of BaseEvent) = Nothing
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
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), startBeat As Beat, endBeat As Beat) As IEnumerable(Of TEvent)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key < endBeat) _
.SkipWhile(Function(i) i.Key < startBeat) _
.SelectMany(Function(i) i.Value.OfType(Of TEvent))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a bar.
		''' </summary>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), bar As Index) As IEnumerable(Of TEvent)
			Dim rg = GetRange(e, bar)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.OfType(Of TEvent))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of beat.
		''' </summary>
		''' <param name="range">Specified beat range.</param>
		''' <returns></returns>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), range As RDRange) As IEnumerable(Of TEvent)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) If(i.Key < range.End, True)) _
.SkipWhile(Function(i) If(i.Key < range.Start, False)) _
.SelectMany(Function(i) i.Value)
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of bar.
		''' </summary>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), bars As Range) As IEnumerable(Of TEvent)
			Dim rg = GetRange(e, bars)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.OfType(Of TEvent))
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), beat As Beat) As IEnumerable(Of TEvent)
			Return e.Where(beat).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified range of beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), startBeat As Beat, endBeat As Beat) As IEnumerable(Of TEvent)
			Return e.Where(startBeat, endBeat).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified range of beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), range As RDRange) As IEnumerable(Of TEvent)
			Return e.Where(range).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified bar.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), bar As Index) As IEnumerable(Of TEvent)
			Return e.Where(bar).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate in specified range of bar.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), bars As Range) As IEnumerable(Of TEvent)
			Return e.Where(bars).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events in specified event type.
		''' </summary>
		''' <typeparam name="TEvent"></typeparam>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection) As IEnumerable(Of TEvent)
			Dim enums = ConvertToEnums(Of TEvent)()
			Return e.eventsBeatOrder _
.Where(Function(i) i.Value._types _
.Any(Function(j) enums.Contains(j))) _
.SelectMany(Function(i) i.Value).OfType(Of TEvent)
		End Function
		''' <summary>
		''' Filters a sequence of events located at a beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, beat As Beat) As IEnumerable(Of TEvent)
			Dim value As TypedEventCollection(Of BaseEvent) = Nothing
			If e.eventsBeatOrder.TryGetValue(beat, value) Then
				Return value.OfType(Of TEvent)
			End If
			Return value
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, startBeat As Beat, endBeat As Beat) As IEnumerable(Of TEvent)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key < endBeat) _
.SkipWhile(Function(i) i.Key < startBeat) _
.SelectMany(Function(i) i.Value.OfType(Of TEvent))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, bar As Index) As IEnumerable(Of TEvent)
			Dim rg = GetRange(e, bar)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.OfType(Of TEvent))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, range As RDRange) As IEnumerable(Of TEvent)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) If(i.Key < range.End, True)) _
.SkipWhile(Function(i) If(i.Key < range.Start, False)) _
.SelectMany(Function(i) i.Value.OfType(Of TEvent))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, bars As Range) As IEnumerable(Of TEvent)
			Dim rg = GetRange(e, bars)
			Return e.eventsBeatOrder _
.TakeWhile(Function(i) i.Key.BeatOnly < rg.end) _
.SkipWhile(Function(i) i.Key.BeatOnly < rg.start) _
.SelectMany(Function(i) i.Value.OfType(Of TEvent))
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean)) As IEnumerable(Of TEvent)
			Return e.Where(Of TEvent)().Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), beat As Beat) As IEnumerable(Of TEvent)
			Return e.Where(Of TEvent)(beat).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), startBeat As Beat, endBeat As Beat) As IEnumerable(Of TEvent)
			Return e.Where(Of TEvent)(startBeat, endBeat).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), range As RDRange) As IEnumerable(Of TEvent)
			Return e.Where(Of TEvent)(range).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), bar As Index) As IEnumerable(Of TEvent)
			Return e.Where(Of TEvent)(bar).Where(predicate)
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function Where(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), bars As Range) As IEnumerable(Of TEvent)
			Return e.Where(Of TEvent)(bars).Where(predicate)
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean)) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a time.
		''' </summary>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), beat As Beat) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(beat)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of time.
		''' </summary>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), startBeat As Beat, endBeat As Beat) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(startBeat, endBeat)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a bar.
		''' </summary>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), bar As Index) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(bar)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of beat.
		''' </summary>
		''' <param name="range">Specified beat range.</param>
		''' <returns></returns>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(range)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of bar.
		''' </summary>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), bars As Range) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(bars)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), beat As Beat) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, beat)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified range of beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), startBeat As Beat, endBeat As Beat) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, startBeat, endBeat)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified range of beat.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, range)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified bar.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), bar As Index) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, bar)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate in specified range of bar.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), bars As Range) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, bars)))
		End Function
		''' <summary>
		''' Remove a sequence of events in specified event type.
		''' </summary>
		''' <typeparam name="TEvent"></typeparam>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)()))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, beat As Beat) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(beat)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, startBeat As Beat, endBeat As Beat) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(startBeat, endBeat)))
		End Function
		''' <summary>
		''' Filters a sequence of events located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, range As RDRange) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(range)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, bar As Index) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(bar)))
		End Function
		''' <summary>
		''' Remove a sequence of events located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, bars As Range) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(bars)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean)) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate located at a beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), beat As Beat) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, beat)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="startBeat">Specified start beat.</param>
		''' <param name="endBeat">Specified end beat.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), startBeat As Beat, endBeat As Beat) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, startBeat, endBeat)))
		End Function
		''' <summary>
		''' Remove a sequence of events based on a predicate located at a range of beat in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="range">Specified beat range.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), range As RDRange) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, range)))
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), bar As Index) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, bar)))
		End Function
		''' <summary>
		''' Filters a sequence of events based on a predicate located at a range of bar in specified event type.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bars">Specified bar range.</param>
		<Extension> Public Function RemoveAll(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), bars As Range) As Integer
			Return e.RemoveRange(New List(Of TEvent)(e.Where(Of TEvent)(predicate, bars)))
		End Function
		''' <summary>
		''' Returns the first element of the collection.
		''' </summary>
		<Extension> Public Function First(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent)) As TEvent
			Return e.eventsBeatOrder.First.Value.First
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function First(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean)) As TEvent
			Return e.ConcatAll.First(predicate)
		End Function
		''' <summary>
		''' Returns the first element of the collection in specified event type.
		''' </summary>
		<Extension> Public Function First(Of TEvent As BaseEvent)(e As OrderedEventCollection) As TEvent
			Return e.Where(Of TEvent).First
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition in specified event type.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function First(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean)) As TEvent
			Return e.Where(Of TEvent).First(predicate)
		End Function
		<Extension> Public Function FirstOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent)) As TEvent
			Return e.eventsBeatOrder.FirstOrDefault.Value?.FirstOrDefault
		End Function
		''' <summary>
		''' Returns the first element of the collection, or <paramref name="defaultValue"/> if collection contains no elements.
		''' </summary>
		''' <param name="defaultValue">The default value to return if contains no elements.</param>
		<Extension> Public Function FirstOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), defaultValue As TEvent) As TEvent
			Return e.ConcatAll.FirstOrDefault(defaultValue)
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function FirstOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean)) As TEvent
			Return e.ConcatAll.FirstOrDefault(predicate)
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function FirstOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), defaultValue As TEvent) As TEvent
			Return e.ConcatAll.FirstOrDefault(predicate, defaultValue)
		End Function
		''' <summary>
		''' Returns the first element of the collection in specified event type, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		<Extension> Public Function FirstOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection) As TEvent
			Return e.Where(Of TEvent).FirstOrDefault()
		End Function
		''' <summary>
		''' Returns the first element of the collection in specified event type, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function FirstOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection, defaultValue As TEvent) As TEvent
			Return e.Where(Of TEvent).FirstOrDefault(defaultValue)
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition in specified event type, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function FirstOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean)) As TEvent
			Return e.Where(Of TEvent).FirstOrDefault(predicate)
		End Function
		''' <summary>
		''' Returns the first element of the collection that satisfies a specified condition in specified event type, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function FirstOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), defaultValue As TEvent) As TEvent
			Return e.Where(Of TEvent).FirstOrDefault(predicate, defaultValue)
		End Function
		''' <summary>
		''' Returns the last element of the collection.
		''' </summary>
		<Extension> Public Function Last(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent)) As TEvent
			Return e.eventsBeatOrder.Last.Value.Last
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function Last(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean)) As TEvent
			Return e.ConcatAll.Last(predicate)
		End Function
		''' <summary>
		''' Returns the last element of the collection in specified event type.
		''' </summary>
		<Extension> Public Function Last(Of TEvent As BaseEvent)(e As OrderedEventCollection) As TEvent
			Return e.Where(Of TEvent).Last
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition in specified event type.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function Last(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean)) As TEvent
			Return e.Where(Of TEvent).Last(predicate)
		End Function
		''' <summary>
		''' Returns the last element of the collection, or <see langword="null"/> if collection contains no elements.
		''' </summary>
		<Extension> Public Function LastOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent)) As TEvent
			Return e.eventsBeatOrder.LastOrDefault.Value?.LastOrDefault()
		End Function
		''' <summary>
		''' Returns the last element of the collection, or <paramref name="defaultValue"/> if collection contains no elements.
		''' </summary>
		''' <param name="defaultValue">The default value to return if contains no elements.</param>
		<Extension> Public Function LastOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), defaultValue As TEvent) As TEvent
			Return e.ConcatAll.LastOrDefault(defaultValue)
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function LastOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean)) As TEvent
			Return e.ConcatAll.LastOrDefault(predicate)
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function LastOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), defaultValue As TEvent) As TEvent
			Return e.ConcatAll.LastOrDefault(predicate, defaultValue)
		End Function
		''' <summary>
		''' Returns the last element of the collection in specified event type, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		<Extension> Public Function LastOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection) As TEvent
			Return e.Where(Of TEvent).LastOrDefault()
		End Function
		''' <summary>
		''' Returns the last element of the collection in specified event type, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function LastOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection, defaultValue As TEvent) As TEvent
			Return e.Where(Of TEvent).LastOrDefault(defaultValue)
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition in specified event type, or <see langword="null"/> if matches no elements.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		<Extension> Public Function LastOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean)) As TEvent
			Return e.Where(Of TEvent).LastOrDefault(predicate)
		End Function
		''' <summary>
		''' Returns the last element of the collection that satisfies a specified condition in specified event type, or <paramref name="defaultValue"/> if matches no elements.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="defaultValue">The default value to return if matches no elements.</param>
		<Extension> Public Function LastOrDefault(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), defaultValue As TEvent) As TEvent
			Return e.Where(Of TEvent).LastOrDefault(predicate, defaultValue)
		End Function
		''' <summary>
		''' Returns events from a collection as long as it less than or equal to <paramref name="beat"/>.
		''' </summary>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Iterator Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), beat As Beat) As IEnumerable(Of TEvent)
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
		<Extension> Public Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), bar As Index) As IEnumerable(Of TEvent)
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
		<Extension> Public Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean)) As IEnumerable(Of TEvent)
			Return e.eventsBeatOrder.SelectMany(Function(i) i.Value).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection as long as a specified condition is true and also less than or equal to <paramref name="beat"/>.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), beat As Beat) As IEnumerable(Of TEvent)
			Return e.TakeWhile(beat).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection as long as a specified condition is true and also less than or equal to <paramref name="bar"/>.
		''' </summary>
		''' <param name="predicate">A function to test each event for a condition.</param>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), predicate As Func(Of TEvent, Boolean), bar As Index) As IEnumerable(Of TEvent)
			Return e.TakeWhile(bar).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection in specified event type as long as it less than or equal to <paramref name="beat"/>.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Iterator Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection, beat As Beat) As IEnumerable(Of TEvent)
			For Each item In e.Where(Of TEvent)
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
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="bar">Specified bar.</param>
		<Extension> Public Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection, bar As Index) As IEnumerable(Of TEvent)
			Dim firstEvent = e.First
			Dim lastEvent = e.Last
			Return e.TakeWhile(Of TEvent)(
				If(bar.IsFromEnd,
				lastEvent.Beat._calculator.BeatOf(lastEvent.Beat.BarBeat.bar - bar.Value + 1, 1),
				firstEvent.Beat._calculator.BeatOf(bar.Value + 1, 1)))
		End Function
		''' <summary>
		''' Returns events from a collection in specified event type as long as a specified condition is true.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">Specified condition.</param>
		<Extension> Public Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean)) As IEnumerable(Of TEvent)
			Return e.Where(Of TEvent).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection in specified event type as long as a specified condition is true and its beat less than or equal to <paramref name="beat"/>.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">Specified condition.</param>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), beat As Beat) As IEnumerable(Of TEvent)
			Return e.TakeWhile(Of TEvent)(beat).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Returns events from a collection in specified event type as long as a specified condition is true and its beat less than or equal to <paramref name="bar"/>.
		''' </summary>
		''' <typeparam name="TEvent">Specified event type.</typeparam>
		''' <param name="predicate">Specified condition.</param>
		''' <param name="bar">Specified beat.</param>
		<Extension> Public Function TakeWhile(Of TEvent As BaseEvent)(e As OrderedEventCollection, predicate As Func(Of TEvent, Boolean), bar As Index) As IEnumerable(Of TEvent)
			Return e.TakeWhile(Of TEvent)(bar).TakeWhile(predicate)
		End Function
		''' <summary>
		''' Remove a range of events.
		''' </summary>
		''' <param name="items">A range of events.</param>
		''' <returns>The number of events successfully removed.</returns>
		<Extension> Public Function RemoveRange(Of TEvent As BaseEvent)(e As OrderedEventCollection, items As IEnumerable(Of TEvent)) As Integer
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
		<Extension> Public Function RemoveRange(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), items As IEnumerable(Of TEvent)) As Integer
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
		<Extension> Public Function IsInFrontOf(e As OrderedEventCollection, item1 As BaseEvent, item2 As BaseEvent) As Boolean
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
		<Extension> Public Function IsBehind(e As OrderedEventCollection, item1 As BaseEvent, item2 As BaseEvent) As Boolean
			Dim s = item1.Beat.Equals(CObj(item2.Beat))
			Return item1.Beat > item2.Beat OrElse
				(item1.Beat.BeatOnly = item2.Beat.BeatOnly AndAlso
				e.eventsBeatOrder(item1.Beat).BeforeThan(item2, item1))
		End Function
		''' <summary>
		''' Get all the hit of the level.
		''' </summary>
		<Extension> Public Function GetHitBeat(e As RDLevel) As IEnumerable(Of Hit)
			Dim L As New List(Of Hit)
			For Each item In e.Rows
				L.AddRange(item.HitBeats)
			Next
			Return L
		End Function
		''' <summary>
		''' Get all the hit event of the level.
		''' </summary>
		<Extension> Public Function GetHitEvents(e As RDLevel) As IEnumerable(Of BaseBeat)
			Return e.Where(Of BaseBeat)(Function(i) i.IsHitable)
		End Function
		''' <summary>
		''' Get all events with the specified tag.
		''' </summary>
		''' <param name="name">Tag name.</param>
		''' <param name="strict">Indicates whether the label is strictly matched.
		''' If <see langword="true"/>, determine If it contains the specified tag.
		''' If <see langword="false"/>, determine If it Is equal to the specified tag.</param>
		''' <returns>An <see cref="IGrouping"/>, categorized by tag name.</returns>
		<Extension> Public Function GetTaggedEvents(Of TEvent As BaseEvent)(e As OrderedEventCollection(Of TEvent), name As String, strict As Boolean) As IEnumerable(Of IGrouping(Of String, TEvent))
			If name Is Nothing Then
				Return Nothing
			End If
			If strict Then
				Return e.Where(Function(i) i.Tag = name) _
					.GroupBy(Function(i) i.Tag)
			Else
				Return e.Where(Function(i) If(i.Tag?.Contains(name), False)) _
					.GroupBy(Function(i) i.Tag)
			End If
		End Function
		''' <summary>
		''' Get all classic beat events and their variants.
		''' </summary>
		<Extension> Private Function ClassicBeats(e As RowEventCollection) As IEnumerable(Of BaseBeat)
			Return e.Where(Of BaseBeat)(Function(i) i.Type = EventType.AddClassicBeat Or
				i.Type = EventType.AddFreeTimeBeat Or
				i.Type = EventType.PulseFreeTimeBeat)
		End Function
		''' <summary>
		''' Get all oneshot beat events.
		''' </summary>
		<Extension> Private Function OneshotBeats(e As RowEventCollection) As IEnumerable(Of BaseBeat)
			Return e.Where(Of BaseBeat)(Function(i) i.Type = EventType.AddOneshotBeat)
		End Function
		''' <summary>
		''' Get all hits of all beats.
		''' </summary>
		<Extension> Public Function HitBeats(e As RowEventCollection) As IEnumerable(Of Hit)
			Select Case e.RowType
				Case RowType.Classic
					Return e.ClassicBeats().SelectMany(Function(i) i.HitTimes)
				Case RowType.Oneshot
					Return e.OneshotBeats().SelectMany(Function(i) i.HitTimes)
				Case Else
					Throw New RhythmBaseException("How?")
			End Select
		End Function
		''' <summary>
		''' Get an instance of the beat associated with the level.
		''' </summary>
		''' <param name="beatOnly">Total number of 1-based beats.</param>
		<Extension> Public Function BeatOf(e As RDLevel, beatOnly As Single) As Beat
			Return e.Calculator.BeatOf(beatOnly)
		End Function
		''' <summary>
		''' Get an instance of the beat associated with the level.
		''' </summary>
		''' <param name="bar">The 1-based bar.</param>
		''' <param name="beat">The 1-based beat of the bar.</param>
		<Extension> Public Function BeatOf(e As RDLevel, bar As UInteger, beat As Single) As Beat
			Return e.Calculator.BeatOf(bar, beat)
		End Function
		''' <summary>
		''' Get an instance of the beat associated with the level.
		''' </summary>
		''' <param name="timeSpan">Total time span of the beat.</param>
		<Extension> Public Function BeatOf(e As RDLevel, timeSpan As TimeSpan) As Beat
			Return e.Calculator.BeatOf(timeSpan)
		End Function
#If DEBUG Then
		<Extension> Public Function GetRowBeatStatus(e As RowEventCollection) As SortedDictionary(Of Single, Integer())
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
		''' <summary>
		''' Get all beats of the row.
		''' </summary>
		<Extension> Public Function Beats(e As RowEventCollection) As IEnumerable(Of BaseBeat)
			Select Case e.RowType
				Case RowType.Classic
					Return e.ClassicBeats()
				Case RowType.Oneshot
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
		<Extension> Public Function Where(e As ADTileCollection, predicate As Func(Of ADTile, Boolean)) As IEnumerable(Of ADTile)
			Return e.AsEnumerable.Where(predicate)
		End Function
		<Extension> Public Function EventsWhere(e As ADTileCollection, predicate As Func(Of ADBaseEvent, Boolean)) As IEnumerable(Of ADBaseEvent)
			Return e.Events.Where(predicate)
		End Function
		<Extension> Public Function EventsWhere(Of TEvent As ADBaseEvent)(e As ADTileCollection) As IEnumerable(Of TEvent)
			Return e.Events.OfType(Of TEvent)
		End Function
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
		<Extension> Public Function IsInFrontOf(e As BaseEvent, item As BaseEvent) As Boolean
			Return e.Beat.baseLevel.IsInFrontOf(e, item)
		End Function
		''' <summary>
		''' Check if another event is after itself, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function IsBehind(e As BaseEvent, item As BaseEvent) As Boolean
			Return e.Beat.baseLevel.IsBehind(e, item)
		End Function
		''' <summary>
		''' Returns all previous events of the same type, including events of the same beat but executed before itself.
		''' </summary>
		<Extension> Public Function Before(Of TEvent As BaseEvent)(e As TEvent) As IEnumerable(Of TEvent)
			Return e.Beat.baseLevel.Where(Of TEvent)(e.Beat.baseLevel.DefaultBeat, e.Beat)
		End Function
		''' <summary>
		''' Returns all previous events of the specified type, including events of the same beat but executed before itself.
		''' </summary>
		<Extension> Public Function Before(Of TEvent As BaseEvent)(e As BaseEvent) As IEnumerable(Of TEvent)
			Return e.Beat.baseLevel.Where(Of TEvent)(e.Beat.baseLevel.DefaultBeat, e.Beat)
		End Function
		''' <summary>
		''' Returns all events of the same type that follow, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function After(Of TEvent As BaseEvent)(e As TEvent) As IEnumerable(Of TEvent)
			Return e.Beat.baseLevel.Where(Of TEvent)(Function(i) i.Beat > e.Beat)
		End Function
		''' <summary>
		''' Returns all events of the specified type that follow, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function After(Of TEvent As BaseEvent)(e As BaseEvent) As IEnumerable(Of TEvent)
			Return e.Beat.baseLevel.Where(Of TEvent)(Function(i) i.Beat > e.Beat)
		End Function
		''' <summary>
		''' Returns the previous event of the same type, including events of the same beat but executed before itself.
		''' </summary>
		<Extension> Public Function Front(Of TEvent As BaseEvent)(e As TEvent) As TEvent
			Return e.Before.Last
		End Function
		''' <summary>
		''' Returns the previous event of the specified type, including events of the same beat but executed before itself.
		''' </summary>
		<Extension> Public Function Front(Of TEvent As BaseEvent)(e As BaseEvent) As TEvent
			Return e.Before(Of TEvent).Last
		End Function
		''' <summary>
		''' Returns the previous event of the same type, including events of the same beat but executed before itself. Returns <see langword="null"/> if it does not exist.
		''' </summary>
		<Extension> Public Function FrontOrDefault(Of TEvent As BaseEvent)(e As TEvent) As TEvent
			Return e.Before.LastOrDefault
		End Function
		''' <summary>
		''' Returns the previous event of the specified type, including events of the same beat but executed before itself. Returns <see langword="null"/> if it does not exist.
		''' </summary>
		<Extension> Public Function FrontOrDefault(Of TEvent As BaseEvent)(e As BaseEvent) As TEvent
			Return e.Before(Of TEvent).LastOrDefault
		End Function
		''' <summary>
		''' Returns the next event of the same type, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function [Next](Of TEvent As BaseEvent)(e As TEvent) As TEvent
			Return e.After().First
		End Function
		''' <summary>
		''' Returns the next event of the specified type, including events of the same beat but executed after itself.
		''' </summary>
		<Extension> Public Function [Next](Of TEvent As BaseEvent)(e As BaseEvent) As TEvent
			Return e.After(Of TEvent).First
		End Function
		''' <summary>
		''' Returns the next event of the same type, including events of the same beat but executed after itself. Returns <see langword="null"/> if it does not exist.
		''' </summary>
		<Extension> Public Function NextOrDefault(Of TEvent As BaseEvent)(e As TEvent) As TEvent
			Return e.After().FirstOrDefault
		End Function
		''' <summary>
		''' Returns the next event of the specified type, including events of the same beat but executed after itself. Returns <see langword="null"/> if it does not exist.
		''' </summary>
		<Extension> Public Function NextOrDefault(Of TEvent As BaseEvent)(e As BaseEvent) As TEvent
			Return e.After(Of TEvent).FirstOrDefault
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
			e.MethodName = VfxFunctionCalling(NameOf(ShakeCamSmooth), duration, strength, roomID)
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





		<Extension> Public Function DurationOffset(beat As Beat, duration As Single) As Beat
			Dim setBPM = beat.baseLevel.First(Of SetBeatsPerMinute)(Function(i) i.Beat > beat)
			If beat.BarBeat.bar = setBPM.Beat.BarBeat.bar Then
				Return beat + duration
			Else
				Return beat + TimeSpan.FromMinutes(duration / beat.BPM)
			End If
		End Function
		''' <summary>
		''' Shallow copy.
		''' </summary>
		<Extension> Public Function MemberwiseClone(Of TEvent As BaseEvent)(e As TEvent) As TEvent
			If e Is Nothing Then
				Return Nothing
			End If
			Dim type As Type = GetType(TEvent)
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
		<Extension> Public Function Player(e As BaseBeat) As PlayerType
			Return If(
			e.Beat.baseLevel.LastOrDefault(Of ChangePlayersRows)(Function(i) i.Active AndAlso i.Players(e.Index) <> PlayerType.NoChange)?.Players(e.Index),
			e.Parent.Player)
		End Function
		''' <summary>
		''' Get the pulse sound effect of row beat event.
		''' </summary>
		''' <returns>The sound effect of row beat event.</returns>
		<Extension> Public Function BeatSound(e As BaseBeat) As Audio
			Return If(
				e.Parent.LastOrDefault(Of SetBeatSound)(Function(i) i.Beat < e.Beat AndAlso i.Active)?.Sound,
				e.Parent.Sound)
		End Function
		''' <summary>
		''' Get the hit sound effect of row beat event.
		''' </summary>
		''' <returns>The sound effect of row beat event.</returns>
		<Extension> Public Function HitSound(e As BaseBeat) As Audio
			Dim DefaultAudio = New Audio With {.AudioFile = e.Beat.baseLevel.Manager.Create(Of AudioFile)("sndClapHit"), .Offset = TimeSpan.Zero, .Pan = 100, .Pitch = 100, .Volume = 100}
			Select Case e.Player
				Case PlayerType.P1
					Return If(
						e.Beat.baseLevel.LastOrDefault(Of SetClapSounds)(Function(i) i.Active AndAlso i.P1Sound IsNot Nothing)?.P1Sound,
						DefaultAudio)
				Case PlayerType.P2
					Return If(
						e.Beat.baseLevel.LastOrDefault(Of SetClapSounds)(Function(i) i.Active AndAlso i.P2Sound IsNot Nothing)?.P2Sound,
						DefaultAudio)
				Case PlayerType.CPU
					Return If(
						e.Beat.baseLevel.LastOrDefault(Of SetClapSounds)(Function(i) i.Active AndAlso i.CpuSound IsNot Nothing)?.CpuSound,
						DefaultAudio)
				Case Else
					Return Nothing
			End Select
		End Function
		''' <summary>
		''' Get the special tag of the tag event.
		''' </summary>
		''' <returns>special tags.</returns>
		<Extension> Public Function SpetialTags(e As TagAction) As TagAction.SpecialTag()
			Return [Enum].GetValues(Of TagAction.SpecialTag).Where(Function(i) e.ActionTag.Contains($"[{i}]"))
		End Function
		''' <summary>
		''' Convert beat pattern to string.
		''' </summary>
		''' <returns>The pattern string.</returns>
		<Extension> Public Function Pattern(e As AddClassicBeat) As String
			Return Utils.GetPatternString(e.RowXs)
		End Function
		''' <summary>
		''' Get the actual beat pattern.
		''' </summary>
		''' <returns>The actual beat pattern.</returns>
		<Extension> Public Function RowXs(e As AddClassicBeat) As LimitedList(Of Patterns)
			If e.SetXs Is Nothing Then
				Dim X = e.Parent.LastOrDefault(Of SetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New SetRowXs)
				Return X.Pattern
			Else
				Dim T As New LimitedList(Of Patterns)(6, Patterns.None)
				Select Case e.SetXs
					Case AddClassicBeat.ClassicBeatPatterns.ThreeBeat
						T(1) = Patterns.X
						T(2) = Patterns.X
						T(4) = Patterns.X
						T(5) = Patterns.X
					Case AddClassicBeat.ClassicBeatPatterns.FourBeat
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
		<Extension> Public Function Length(e As AddOneshotBeat) As Single
			Return e.Tick * e.Loops + e.Interval * e.Loops - 1
		End Function
		''' <summary>
		''' Get the total length of the classic beat.
		''' </summary>
		''' <returns></returns>
		<Extension> Public Function Length(e As AddClassicBeat) As Single
			Dim SyncoSwing = e.Parent.LastOrDefault(Of SetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New SetRowXs).SyncoSwing
			Return e.Tick * 6 - If(SyncoSwing = 0, 0.5, SyncoSwing) * e.Tick
		End Function
		''' <summary>
		''' Check if it can be hit by player.
		''' </summary>
		<Extension> Public Function IsHitable(e As PulseFreeTimeBeat) As Boolean
			Dim PulseIndexMin = 6
			Dim PulseIndexMax = 6
			For Each item In e.Parent.Where(Of BaseBeat)(Function(i) e.IsBehind(i), New RDRange(e.Beat, Nothing)).Reverse
				Select Case item.Type
					Case EventType.AddFreeTimeBeat
						Dim Temp = CType(item, AddFreeTimeBeat)
						If PulseIndexMin <= Temp.Pulse And Temp.Pulse <= PulseIndexMax Then
							Return True
						End If
					Case EventType.PulseFreeTimeBeat
						Dim Temp = CType(item, PulseFreeTimeBeat)
						Select Case Temp.Action
							Case PulseFreeTimeBeat.ActionType.Increment
								If PulseIndexMin > 0 Then
									PulseIndexMin -= 1
								End If
								If PulseIndexMax > 0 Then
									PulseIndexMax -= 1
								Else
									Return False
								End If
							Case PulseFreeTimeBeat.ActionType.Decrement
								If PulseIndexMin > 0 Then
									PulseIndexMin += 1
								End If
								If PulseIndexMax < 6 Then
									PulseIndexMax += 1
								Else
									Return False
								End If
							Case PulseFreeTimeBeat.ActionType.Custom
								If PulseIndexMin <= Temp.CustomPulse And Temp.CustomPulse <= PulseIndexMax Then
									PulseIndexMin = 0
									PulseIndexMax = 5
								Else
									Return False
								End If
							Case PulseFreeTimeBeat.ActionType.Remove
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
		<Extension> Public Function IsHitable(e As AddFreeTimeBeat) As Boolean
			Return e.Pulse = 6
		End Function
		''' <summary>
		''' Check if it can be hit by player.
		''' </summary>
		<Extension> Public Function IsHitable(e As BaseBeat) As Boolean
			Select Case e.Type
				Case EventType.AddClassicBeat, EventType.AddOneshotBeat
					Return True
				Case EventType.AddFreeTimeBeat
					Return CType(e, AddFreeTimeBeat).IsHitable
				Case EventType.PulseFreeTimeBeat
					Return CType(e, PulseFreeTimeBeat).IsHitable
				Case Else
					Return False
			End Select
		End Function
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As AddClassicBeat) As IEnumerable(Of Hit)
			Return New List(Of Hit) From {New Hit(e, e.GetBeat(6), e.Hold)}.AsEnumerable
		End Function
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As AddOneshotBeat) As IEnumerable(Of Hit)
			Dim L As New List(Of Hit)
			For i As UInteger = 0 To e.Loops
				For j As SByte = 0 To e.Subdivisions - 1
					L.Add(New Hit(e, New Beat(e._beat._calculator, e._beat.BeatOnly + i * e.Interval + e.Tick + e.Delay + j * (e.Tick / e.Subdivisions)), 0))
				Next
			Next
			Return L.AsEnumerable
		End Function
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As AddFreeTimeBeat) As IEnumerable(Of Hit)
			If e.Pulse = 6 Then
				Return New List(Of Hit) From {New Hit(e, e.Beat, e.Hold)}.AsEnumerable
			End If
			Return New List(Of Hit)
		End Function
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As PulseFreeTimeBeat) As IEnumerable(Of Hit)
			If e.IsHitable Then
				Return New List(Of Hit) From {New Hit(e, e.Beat, e.Hold)}
			End If
			Return New List(Of Hit)
		End Function
		''' <summary>
		''' Get all hits.
		''' </summary>
		<Extension> Public Function HitTimes(e As BaseBeat) As IEnumerable(Of Hit)
			Select Case e.Type
				Case EventType.AddClassicBeat
					Return CType(e, AddClassicBeat).HitTimes
				Case EventType.AddFreeTimeBeat
					Return CType(e, AddFreeTimeBeat).HitTimes
				Case EventType.AddOneshotBeat
					Return CType(e, AddOneshotBeat).HitTimes
				Case EventType.PulseFreeTimeBeat
					Return CType(e, PulseFreeTimeBeat).HitTimes
				Case Else
					Return Array.Empty(Of Hit).AsEnumerable
			End Select
		End Function
		''' <summary>
		''' Returns the pulse beat of the specified 0-based index.
		''' </summary>
		''' <exception cref="RhythmBaseException">THIS IS 7TH BEAT GAMES!</exception>
		<Extension> Public Function GetBeat(e As AddClassicBeat, index As Byte) As Beat
			Dim x = e.Parent.LastOrDefault(Of SetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New SetRowXs)
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
		<Extension> Public Function GetPatternString(e As SetRowXs) As String
			Return Utils.GetPatternString(e.Pattern)
		End Function
		''' <summary>
		''' Creates a new <see cref="AdvanceText"/> subordinate to <see cref="FloatingText"/> at the specified beat. The new event created will be attempted to be added to the <see cref="FloatingText"/>'s source level.
		''' </summary>
		''' <param name="beat">Specified beat.</param>
		<Extension> Public Function CreateAdvanceText(e As FloatingText, beat As Beat) As AdvanceText
			Dim A As New AdvanceText With {.Parent = e, .Beat = beat.WithoutBinding}
			e.Children.Add(A)
			Return A
		End Function
		''' <summary>
		''' Get the sequence of <see cref="PulseFreeTimeBeat"/> belonging to this <see cref="AddFreeTimeBeat"/>, return all of the <see cref="PulseFreeTimeBeat"/> from the time the pulse was created to the time it was removed or hit.
		''' </summary>
		<Extension> Public Function GetPulses(e As AddFreeTimeBeat) As IEnumerable(Of PulseFreeTimeBeat)
			Dim Result As New List(Of PulseFreeTimeBeat)
			Dim pulse As Byte = e.Pulse
			For Each item In e.Parent.Where(Of PulseFreeTimeBeat)(Function(i) i.Active AndAlso e.IsInFrontOf(i))
				Select Case item.Action
					Case PulseFreeTimeBeat.ActionType.Increment
						pulse += 1
						Result.Add(item)
					Case PulseFreeTimeBeat.ActionType.Decrement
						pulse = If(pulse > 1, pulse - 1, 0)
						Result.Add(item)
					Case PulseFreeTimeBeat.ActionType.Custom
						pulse = item.CustomPulse
						Result.Add(item)
					Case PulseFreeTimeBeat.ActionType.Remove
						Result.Add(item)
						Exit For
				End Select
				If pulse = 6 Then
					Exit For
				End If
			Next
			Return Result
		End Function
		<Extension> Private Function SplitCopy(e As SayReadyGetSetGo, extraBeat As Single, word As SayReadyGetSetGo.Words) As SayReadyGetSetGo
			Dim Temp = e.Clone(Of SayReadyGetSetGo)
			Temp.Beat += extraBeat
			Temp.PhraseToSay = word
			Temp.Volume = e.Volume
			Return Temp
		End Function
		''' <summary>
		''' Generate split event instances.
		''' </summary>
		<Extension> Public Function Split(e As SayReadyGetSetGo) As IEnumerable(Of SayReadyGetSetGo)
			If e.Splitable Then
				Select Case e.PhraseToSay
					Case SayReadyGetSetGo.Words.SayReaDyGetSetGoNew
						Return New List(Of SayReadyGetSetGo) From {
						e.SplitCopy(0, SayReadyGetSetGo.Words.JustSayRea),
						e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSayDy),
						e.SplitCopy(e.Tick * 2, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick * 3, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 4, SayReadyGetSetGo.Words.JustSayGo)}
					Case SayReadyGetSetGo.Words.SayGetSetGo
						Return New List(Of SayReadyGetSetGo) From {
						e.SplitCopy(0, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 2, SayReadyGetSetGo.Words.JustSayGo)}
					Case SayReadyGetSetGo.Words.SayReaDyGetSetOne
						Return New List(Of SayReadyGetSetGo) From {
						e.SplitCopy(0, SayReadyGetSetGo.Words.JustSayRea),
						e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSayDy),
						e.SplitCopy(e.Tick * 2, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick * 3, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 4, SayReadyGetSetGo.Words.Count1)}
					Case SayReadyGetSetGo.Words.SayGetSetOne
						Return New List(Of SayReadyGetSetGo) From {
						e.SplitCopy(0, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 2, SayReadyGetSetGo.Words.Count1)}
					Case SayReadyGetSetGo.Words.SayReadyGetSetGo
						Return New List(Of SayReadyGetSetGo) From {
						e.SplitCopy(0, SayReadyGetSetGo.Words.JustSayReady),
						e.SplitCopy(e.Tick * 2, SayReadyGetSetGo.Words.JustSayGet),
						e.SplitCopy(e.Tick * 3, SayReadyGetSetGo.Words.JustSaySet),
						e.SplitCopy(e.Tick * 4, SayReadyGetSetGo.Words.JustSayGo)}
					Case Else
				End Select
			End If
			Return New List(Of SayReadyGetSetGo) From {e}.AsEnumerable
		End Function
		''' <summary>
		''' Generate split event instances.
		''' </summary>
		<Extension> Public Function Split(e As AddOneshotBeat) As IEnumerable(Of AddOneshotBeat)
			Dim L As New List(Of AddOneshotBeat)
			For i As UInteger = 0 To e.Loops
				Dim T = e.Clone(Of AddOneshotBeat)
				T.FreezeBurnMode = e.FreezeBurnMode
				T.Delay = e.Delay
				T.PulseType = e.PulseType
				T.Subdivisions = e.Subdivisions
				T.SubdivSound = e.SubdivSound
				T.Tick = e.Tick
				T.Loops = 0
				T.Interval = 0
				T.Beat = New Beat(e._beat._calculator, e.Beat.BeatOnly + i * e.Interval)
				L.Add(T)
			Next
			Return L.AsEnumerable
		End Function
		''' <summary>
		''' Generate split event instances. Follow the most recently activated Xs.
		''' </summary>
		<Extension> Public Function Split(e As AddClassicBeat) As IEnumerable(Of BaseBeat)
			Dim x = e.Parent.LastOrDefault(Of SetRowXs)(Function(i) i.Active AndAlso e.IsBehind(i), New SetRowXs)
			Return e.Split(x)
		End Function
		''' <summary>
		''' Generate split event instances.
		''' </summary>
		<Extension> Public Function Split(e As AddClassicBeat, Xs As SetRowXs) As IEnumerable(Of BaseBeat)
			Dim L As New List(Of BaseBeat)
			Dim Head As AddFreeTimeBeat = e.Clone(Of AddFreeTimeBeat)()
			Head.Pulse = 0
			Head.Hold = e.Hold
			L.Add(Head)
			Dim tempBeat = e.Beat
			For i = 1 To 6
				If i < 6 AndAlso Xs.Pattern(i) = Patterns.X Then
					Continue For
				End If
				Dim Pulse As PulseFreeTimeBeat = e.Clone(Of PulseFreeTimeBeat)()
				Pulse.Beat += e.Tick * i
				If i >= Xs.SyncoBeat Then
					Pulse.Beat -= Xs.SyncoSwing
				End If
				If i Mod 2 = 1 Then
					Pulse.Beat += e.Tick - If(e.Swing = 0, e.Tick, e.Swing)
				End If
				Pulse.Hold = e.Hold
				Pulse.Action = PulseFreeTimeBeat.ActionType.Increment
				L.Add(Pulse)
			Next
			Return L.AsEnumerable
		End Function
		''' <summary>
		''' Getting controlled events.
		''' </summary>
		<Extension> Public Function ControllingEvents(e As TagAction) As IEnumerable(Of IGrouping(Of String, BaseEvent))
			Return e.Beat.baseLevel.GetTaggedEvents(e.ActionTag, e.Action.HasFlag(TagAction.Actions.All))
		End Function
		''' <summary>
		''' Remove auxiliary symbols.
		''' </summary>
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
		''' <summary>
		''' Specifies the position of the image. This method changes both the pivot and the angle to keep the image visually in its original position.
		''' </summary>
		''' <param name="target">Specified position. </param>
		<Extension> Public Sub MovePositionMaintainVisual(e As Move, target As PointE)
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
		<Extension> Public Sub MovePositionMaintainVisual(e As MoveRoom, target As RDSizeE)
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric Then
				Exit Sub
			End If
			e.RoomPosition = target
			e.Pivot = (e.VisualPosition() - New RDSizeE(target)).Rotate(e.Angle.Value.NumericValue)
		End Sub
		''' <summary>
		''' The visual position of the lower left corner of the image.
		''' </summary>
		<Extension> Public Function VisualPosition(e As Move) As PointE
			If e.Position Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing OrElse Not e.Angle.Value.IsNumeric OrElse e.Scale Is Nothing Then
				Return New PointE
			End If
			Dim previousPosition As PointE = e.Position
			Dim previousPivot As New PointE(
				e.Pivot?.X * e.Scale?.X * e.Parent.Size.Width / 100,
				e.Pivot?.Y * e.Scale?.Y * e.Parent.Size.Height / 100)
			Return previousPosition + New RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue))
		End Function
		''' <summary>
		''' The visual position of the lower left corner of the image.
		''' </summary>
		<Extension> Public Function VisualPosition(e As MoveRoom) As PointE
			If e.RoomPosition Is Nothing OrElse e.Pivot Is Nothing OrElse e.Angle Is Nothing Then
				Return New PointE
			End If
			Dim previousPosition As PointE = e.RoomPosition
			Dim previousPivot As New PointE(e.Pivot?.X * e.Scale?.Width, e.Pivot?.Y * e.Scale?.Height)
			Return previousPosition + New RDSizeE(previousPivot.Rotate(e.Angle.Value.NumericValue))
		End Function
	End Module
End Namespace