Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Collections.ObjectModel
'' <summary>
'' 工具类
'' </summary>
Namespace Utils
	''' <summary>
	''' Beat calculator.
	''' </summary>
	Public Class BeatCalculator
		Friend ReadOnly Collection As RDLevel
		Private _BPMList As List(Of BaseBeatsPerMinute)
		Private _CPBList As List(Of SetCrotchetsPerBar)
		Friend Sub New(level As RDLevel)
			Collection = level
			Refresh()
		End Sub
		''' <summary>
		''' Refresh the cache.
		''' </summary>
		Public Sub Refresh()
			_BPMList = Collection.Where(Of BaseBeatsPerMinute).ToList
			_CPBList = Collection.Where(Of SetCrotchetsPerBar).ToList
		End Sub
		''' <summary>
		''' Convert beat data.
		''' </summary>
		''' <param name="bar">The 1-based bar.</param>
		''' <param name="beat">The 1-based beat of the bar.</param>
		''' <returns>Total 1-based beats.</returns>
		Public Function BarBeatToBeatOnly(bar As UInteger, beat As Single) As Single
			Return BarBeatToBeatOnly(bar, beat, _CPBList)
		End Function
		''' <summary>
		''' Convert beat data.
		''' </summary>
		''' <param name="bar">The 1-based bar.</param>
		''' <param name="beat">The 1-based beat of the bar.</param>
		''' <returns>Total time span.</returns>
		Public Function BarBeatToTimeSpan(bar As UInteger, beat As Single) As TimeSpan
			Return BeatOnlyToTimeSpan(BarBeatToBeatOnly(bar, beat))
		End Function
		''' <summary>
		''' Convert beat data.
		''' </summary>
		''' <param name="beat">Total 1-based beats.</param>
		''' <returns>The 1-based bar and the 1-based beat of bar.</returns>
		Public Function BeatOnlyToBarBeat(beat As Single) As (bar As UInteger, beat As Single)
			Return BeatOnlyToBarBeat(beat, _CPBList)
		End Function
		''' <summary>
		''' Convert beat data.
		''' </summary>
		''' <param name="beat">Total 1-based beats.</param>
		''' <returns>Total time span.</returns>
		Public Function BeatOnlyToTimeSpan(beat As Single) As TimeSpan
			Return BeatOnlyToTimeSpan(beat, _BPMList)
		End Function
		''' <summary>
		''' Convert beat data.
		''' </summary>
		''' <param name="timeSpan">Total time span.</param>
		''' <returns>Total 1-based beats.</returns>
		Public Function TimeSpanToBeatOnly(timeSpan As TimeSpan) As Single
			Return TimeSpanToBeatOnly(timeSpan, _BPMList)
		End Function
		''' <summary>
		''' Convert beat data.
		''' </summary>
		''' <param name="timeSpan">Total time span.</param>
		''' <returns>The 1-based bar and the 1-based beat of bar.</returns>
		Public Function TimeSpanToBarBeat(timeSpan As TimeSpan) As (bar As UInteger, beat As Single)
			Return BeatOnlyToBarBeat(TimeSpanToBeatOnly(timeSpan))
		End Function
		Private Shared Function BarBeatToBeatOnly(bar As UInteger, beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As Single
			Dim foreCPB As (BeatOnly As Single, Bar As UInteger, CPB As UInteger) = (1, 1, 8)
			Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Beat.BarBeat.bar < bar)
			If LastCPB IsNot Nothing Then
				foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar)
			End If
			Dim result = foreCPB.BeatOnly + (bar - foreCPB.Bar) * foreCPB.CPB + beat - 1
			Return result
		End Function
		Private Shared Function BeatOnlyToBarBeat(beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As (bar As UInteger, beat As Single)
			Dim foreCPB As (BeatOnly As Single, Bar As UInteger, CPB As UInteger) = (1, 1, 8)
			Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Beat.BeatOnly < beat)
			If LastCPB IsNot Nothing Then
				foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar)
			End If
			Dim result = (CUInt(foreCPB.Bar + Math.Floor((beat - foreCPB.BeatOnly) / foreCPB.CPB)),
				(beat - foreCPB.BeatOnly) Mod foreCPB.CPB + 1)
			Return result
		End Function
		Private Shared Function BeatOnlyToTimeSpan(beatOnly As Single, BPMCollection As IEnumerable(Of BaseBeatsPerMinute)) As TimeSpan
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
		Private Shared Function TimeSpanToBeatOnly(timeSpan As TimeSpan, BPMCollection As IEnumerable(Of BaseBeatsPerMinute)) As Single
			Dim fore As (BeatOnly As Single, BPM As Single) = (1, 100)
			Dim foreBPM = BPMCollection.FirstOrDefault()
			If foreBPM IsNot Nothing Then
				fore = (foreBPM.Beat.BeatOnly, foreBPM.BeatsPerMinute)
			End If
			Dim beatOnly As Single = 1
			For Each item As BaseBeatsPerMinute In BPMCollection
				If timeSpan > BeatOnlyToTimeSpan(item.Beat.BeatOnly, BPMCollection) Then
					beatOnly +=
(
BeatOnlyToTimeSpan(item.Beat.BeatOnly, BPMCollection) -
BeatOnlyToTimeSpan(fore.BeatOnly, BPMCollection)
).TotalMinutes * fore.BPM
					foreBPM = item
				Else
					Exit For
				End If
			Next
			beatOnly += (
timeSpan - BeatOnlyToTimeSpan(fore.BeatOnly, BPMCollection)
).TotalMinutes * fore.BPM
			Return beatOnly
		End Function
		''' <summary>
		''' Creates a beat instance.
		''' </summary>
		Public Function BeatOf(beatOnly As Single) As Beat
			Return New Beat(Me, beatOnly)
		End Function
		''' <summary>
		''' Creates a beat instance.
		''' </summary>
		Public Function BeatOf(bar As UInteger, beat As Single) As Beat
			Return New Beat(Me, bar, beat)
		End Function
		''' <summary>
		''' Creates a beat instance.
		''' </summary>
		Public Function BeatOf(timeSpan As TimeSpan) As Beat
			Return New Beat(Me, timeSpan)
		End Function
		''' <summary>
		''' Calculate the BPM of the moment in which the beat is.
		''' </summary>
		Public Function BeatsPerMinuteOf(beat As Beat) As Single
			Return If(_BPMList.LastOrDefault(Function(i) i.Beat < beat)?.BeatsPerMinute, 100)
		End Function
		''' <summary>
		''' Calculate the CPB of the moment in which the beat is.
		''' </summary>
		Public Function CrotchetsPerBarOf(beat As Beat) As Single
			Return If(_CPBList.LastOrDefault(Function(i) i.Beat < beat)?.CrotchetsPerBar, 8)
		End Function
	End Class
	Public Module Utils
		Private ReadOnly EventTypes As ObjectModel.ReadOnlyCollection(Of Type) =
			GetType(BaseEvent).Assembly.GetTypes() _
				.Where(Function(i) i.IsAssignableTo(GetType(BaseEvent))).ToList.AsReadOnly
		''' <summary>
		''' A dictionary that records the correspondence of event types inheriting from <see cref="BaseEvent"/> to <see cref="EventType"/>.
		''' </summary>
		Public ReadOnly EventTypeToEnums As ObjectModel.ReadOnlyDictionary(Of Type, EventType()) =
			EventTypes.ToDictionary(Function(i) i,
						  Function(i) EventTypes _
							  .Where(Function(j) (j = i OrElse j.IsAssignableTo(i)) AndAlso Not j.IsAbstract) _
							  .Select(Function(j) ConvertToEnum(j)) _
							  .ToArray).AsReadOnly
		''' <summary>
		''' A dictionary that records the correspondence of <see cref="EventType"/> to event types inheriting from <see cref="BaseEvent"/>.
		''' </summary>
		Public ReadOnly EnumToEventType As ObjectModel.ReadOnlyDictionary(Of EventType, Type) =
			[Enum].GetValues(Of EventType).ToDictionary(Function(i) i, Function(i) i.ConvertToType).AsReadOnly
		''' <summary>
		''' Event types that inherit from <see cref="BaseRowAction"/>.
		''' </summary>
		Public ReadOnly RowTypes As ObjectModel.ReadOnlyCollection(Of EventType) =
			ConvertToEnums(Of BaseRowAction)().AsReadOnly
		''' <summary>
		''' Event types that inherit from <see cref="BaseDecorationAction"/>
		''' </summary>
		Public ReadOnly DecorationTypes As ObjectModel.ReadOnlyCollection(Of EventType) =
			ConvertToEnums(Of BaseDecorationAction)().AsReadOnly
		Public ReadOnly RDScreenSize As New RDSizeNI(352, 198)
		Public ReadOnly EventTypeEnumsForGameplay As New ReadOnlyCollection(Of EventType)({
			EventType.HideRow,
			EventType.ChangePlayersRows,
			EventType.FinishLevel,
			EventType.ShowHands,
			EventType.SetHandOwner,
			EventType.SetPlayStyle
		})
		Public ReadOnly EventTypeEnumsForEnvironment As New ReadOnlyCollection(Of EventType)({
			EventType.SetTheme,
			EventType.SetBackgroundColor,
			EventType.SetForeground,
			EventType.SetSpeed,
			EventType.Flash,
			EventType.CustomFlash
		})
		Public ReadOnly EventTypeEnumsForRowFX As New ReadOnlyCollection(Of EventType)({
			EventType.HideRow,
			EventType.MoveRow,
			EventType.PlayExpression,
			EventType.TintRows
		})
		Public ReadOnly EventTypeEnumsForCameraFX As New ReadOnlyCollection(Of EventType)({
			EventType.MoveCamera,
			EventType.ShakeScreen,
			EventType.FlipScreen,
			EventType.PulseCamera
		})
		Public ReadOnly EventTypeEnumsForVisualFX As New ReadOnlyCollection(Of EventType)({
			EventType.SetVFXPreset,
			EventType.SetSpeed,
			EventType.Flash,
			EventType.CustomFlash,
			EventType.BassDrop,
			EventType.InvertColors,
			EventType.Stutter,
			EventType.PaintHands,
			EventType.NewWindowDance
		})
		Public ReadOnly EventTypeEnumsForText As New ReadOnlyCollection(Of EventType)({
			EventType.TextExplosion,
			EventType.ShowDialogue,
			EventType.ShowStatusSign,
			EventType.FloatingText,
			EventType.AdvanceText
		})
		Public ReadOnly EventTypeEnumsForUtility As New ReadOnlyCollection(Of EventType)({
			EventType.Comment,
			EventType.TagAction,
			EventType.CallCustomMethod
		})
		''' <summary>
		''' Converts percentage point to pixel point with default screen size (352 * 198).
		''' </summary>
		Public Function PercentToPixel(point As PointE) As PointE
			Return PercentToPixel(point, RDScreenSize)
		End Function
		''' <summary>
		''' Converts percentage point to pixel point with specified size.
		''' </summary>
		''' <param name="size">Specified size.</param>
		Public Function PercentToPixel(point As PointE, size As RDSizeE) As PointE
			Return New PointE(point.X * size.Width / 100, point.Y * size.Height / 100)
		End Function
		''' <summary>
		''' Converts pixel point to percentage point with default screen size (352 * 198).
		''' </summary>
		Public Function PixelToPercent(point As (X As Single?, Y As Single?)) As (X As Single?, Y As Single?)
			Return PixelToPercent(point, (352, 198))
		End Function
		''' <summary>
		''' Converts pixel point to percentage point with specified size.
		''' </summary>
		''' <param name="size">Specified size.</param>
		Public Function PixelToPercent(point As (X As Single?, Y As Single?), size As (X As Single, Y As Single)) As (X As Single?, Y As Single?)
			Return (point.X * 100 / size.X, point.Y * 100 / size.Y)
		End Function
		''' <summary>
		''' Converts Xs patterns to string form.
		''' </summary>
		''' <param name="list">String pattern.</param>
		''' <returns></returns>
		Public Function GetPatternString(list As LimitedList(Of Patterns)) As String
			Dim out = ""
			For Each item In list
				Select Case item
					Case Patterns.X
						out += "x"
					Case Patterns.Up
						out += "u"
					Case Patterns.Down
						out += "d"
					Case Patterns.Banana
						out += "b"
					Case Patterns.Return
						out += "r"
					Case Patterns.None
						out += "-"
				End Select
			Next
			Return out
		End Function
		Public Function DegreeToRadius(degree As Single) As Single
			Return Single.Pi * degree / 180
		End Function
		Public Function RadiusToDegree(radius As Single) As Single
			Return radius * 180 / Single.Pi
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ConvertToEnum(type As Type) As EventType
			If EventTypeToEnums Is Nothing Then
				Dim result As EventType
				Dim name = type.Name
				If [Enum].TryParse(name, result) Then
					Return result
				End If
				Throw New IllegalEventTypeException(type, "Unable to find a matching EventType.")
			Else
				Try
					Return EventTypeToEnums(type).Single
				Catch ex As Exception
					Throw New IllegalEventTypeException(type, "Multiple matching EventTypes were found. Please check if the type is an abstract class type.", New ArgumentException("Multiple matching EventTypes were found. Please check if the type is an abstract class type.", NameOf(type)))
				End Try
			End If
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ConvertToEnum(Of T As {BaseEvent, New})() As EventType
			Return ConvertToEnum(GetType(T))
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ConvertToEnums(Of T As BaseEvent)() As EventType()
			Try
				Return EventTypeToEnums(GetType(T))
			Catch ex As Exception
				Throw New IllegalEventTypeException(GetType(T), "This exception is not expected. Please contact the developer to handle this exception.", ex)
			End Try
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ConvertToType(type As String) As Type
			Dim result As EventType
			If [Enum].TryParse(type, result) Then
				Return result.ConvertToType()
			End If
			Return EventType.CustomEvent.ConvertToType
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		<Extension> Public Function ConvertToType(type As EventType) As Type
			If EnumToEventType Is Nothing Then
				Dim result = System.Type.GetType($"{GetType(BaseEvent).Namespace}.{type}")
				If result Is Nothing Then
					Throw New RhythmBaseException($"Illegal Type: {type}.")
				End If
				Return result
			Else
				Try
					Return EnumToEventType(type)
				Catch ex As Exception
					Throw New IllegalEventTypeException(type, "This value does not exist in the EventType enumeration.")
				End Try
			End If
		End Function
		<Extension> Public Function GetSerializer(rdlevel As RDLevel, settings As LevelReadOrWriteSettings) As JsonSerializerSettings
			Dim EventsSerializer As New JsonSerializerSettings With {
						.ContractResolver = New RDContractResolver
					}
			With EventsSerializer.Converters
				.Add(New PanelColorConverter(rdlevel.ColorPalette))
				.Add(New ColorConverter)
				.Add(New ConditionalConverter)
				.Add(New CharacterConverter(rdlevel.Path, rdlevel.Assets))
				.Add(New AssetConverter(rdlevel.Path, rdlevel.Assets, settings))
				.Add(New ConditionConverter(rdlevel.Conditionals))
				.Add(New TagActionConverter(rdlevel, settings))
				.Add(New CustomDecorationEventConverter(rdlevel, settings))
				.Add(New CustomRowEventConverter(rdlevel, settings))
				.Add(New CustomEventConverter(rdlevel, settings))
				.Add(New BaseRowActionConverter(Of BaseRowAction)(rdlevel, settings))
				.Add(New BaseDecorationActionConverter(Of BaseDecorationAction)(rdlevel, settings))
				.Add(New BaseEventConverter(Of BaseEvent)(rdlevel, settings))
				.Add(New BookmarkConverter(rdlevel.Calculator))
				.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
			End With
			Return EventsSerializer
		End Function
	End Module
	''' <summary>
	''' Provides methods for creating and reading property names.
	''' </summary>
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