Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
'' <summary>
'' 工具类
'' </summary>
Namespace Utils
	Public Module [Global]

		Private ReadOnly RDETypes As ObjectModel.ReadOnlyCollection(Of Type) =
			GetType(RDBaseEvent).Assembly.GetTypes() _
				.Where(Function(i) i.IsAssignableTo(GetType(RDBaseEvent))).ToList.AsReadOnly
		''' <summary>
		''' A dictionary that records the correspondence of <see cref="RDEventType"/> to event types inheriting from <see cref="RDBaseEvent"/>.
		''' </summary>
		Public ReadOnly RDETypesToEnum As ObjectModel.ReadOnlyDictionary(Of Type, RDEventType()) =
			RDETypes.ToDictionary(Function(i) i,
						  Function(i) RDETypes _
							  .Where(Function(j) (j = i OrElse j.IsAssignableTo(i)) AndAlso Not j.IsAbstract) _
							  .Select(Function(j) RDConvertToEnum(j)) _
							  .ToArray).AsReadOnly
		''' <summary>
		''' A dictionary that records the correspondence of event types inheriting from <see cref="RDBaseEvent"/> to <see cref="RDEventType"/>.
		''' </summary>
		Public ReadOnly RDEnumToEType As ObjectModel.ReadOnlyDictionary(Of RDEventType, Type) =
			[Enum].GetValues(Of RDEventType).ToDictionary(Function(i) i, Function(i) i.ConvertToType).AsReadOnly

		Private ReadOnly ADETypes As ObjectModel.ReadOnlyCollection(Of Type) =
			GetType(ADBaseEvent).Assembly.GetTypes() _
				.Where(Function(i) i.IsAssignableTo(GetType(ADBaseEvent))).ToList.AsReadOnly
		''' <summary>
		''' A dictionary that records the correspondence of <see cref="ADEventType"/> to event types inheriting from <see cref="ADBaseEvent"/>.
		''' </summary>
		Public ReadOnly ADETypesToEnum As ObjectModel.ReadOnlyDictionary(Of Type, ADEventType()) =
			ADETypes.ToDictionary(Function(i) i,
						  Function(i) ADETypes _
							  .Where(Function(j) (j = i OrElse j.IsAssignableTo(i)) AndAlso Not j.IsAbstract) _
							  .Select(Function(j) ADConvertToEnum(j)) _
							  .ToArray).AsReadOnly
		''' <summary>
		''' A dictionary that records the correspondence of event types inheriting from <see cref="ADBaseEvent"/> to <see cref="ADEventType"/>.
		''' </summary>
		Public ReadOnly ADEnumToEType As ObjectModel.ReadOnlyDictionary(Of ADEventType, Type) =
			[Enum].GetValues(Of ADEventType).ToDictionary(Function(i) i, Function(i) i.ConvertToType).AsReadOnly

		''' <summary>
		''' Event types that inherit from <see cref="RDBaseRowAction"/>.
		''' </summary>
		Public ReadOnly RowTypes As ObjectModel.ReadOnlyCollection(Of RDEventType) =
			ConvertToRDEnums(Of RDBaseRowAction)().AsReadOnly
		''' <summary>
		''' Event types that inherit from <see cref="RDBaseDecorationAction"/>
		''' </summary>
		Public ReadOnly DecorationTypes As ObjectModel.ReadOnlyCollection(Of RDEventType) =
			ConvertToRDEnums(Of RDBaseDecorationAction)().AsReadOnly
	End Module
	''' <summary>
	''' Beat calculator.
	''' </summary>
	Public Class RDBeatCalculator
		Friend ReadOnly Collection As RDLevel
		Private _BPMList As List(Of RDBaseBeatsPerMinute)
		Private _CPBList As List(Of RDSetCrotchetsPerBar)
		Friend Sub New(level As RDLevel)
			Collection = level
			Refresh()
		End Sub
		''' <summary>
		''' Refresh the cache.
		''' </summary>
		Public Sub Refresh()
			_BPMList = Collection.Where(Of RDBaseBeatsPerMinute).ToList
			_CPBList = Collection.Where(Of RDSetCrotchetsPerBar).ToList
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
		Private Shared Function BarBeatToBeatOnly(bar As UInteger, beat As Single, Collection As IEnumerable(Of RDSetCrotchetsPerBar)) As Single
			Dim foreCPB As (BeatOnly As Single, Bar As UInteger, CPB As UInteger) = (1, 1, 8)
			Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Beat.BarBeat.bar < bar)
			If LastCPB IsNot Nothing Then
				foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar)
			End If
			Dim result = foreCPB.BeatOnly + (bar - foreCPB.Bar) * foreCPB.CPB + beat - 1
			Return result
		End Function
		Private Shared Function BeatOnlyToBarBeat(beat As Single, Collection As IEnumerable(Of RDSetCrotchetsPerBar)) As (bar As UInteger, beat As Single)
			Dim foreCPB As (BeatOnly As Single, Bar As UInteger, CPB As UInteger) = (1, 1, 8)
			Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Beat.BeatOnly < beat)
			If LastCPB IsNot Nothing Then
				foreCPB = (LastCPB.Beat.BeatOnly, LastCPB.Beat.BarBeat.bar, LastCPB.CrotchetsPerBar)
			End If
			Dim result = (CUInt(foreCPB.Bar + Math.Floor((beat - foreCPB.BeatOnly) / foreCPB.CPB)),
				(beat - foreCPB.BeatOnly) Mod foreCPB.CPB + 1)
			Return result
		End Function
		Private Shared Function BeatOnlyToTimeSpan(beatOnly As Single, BPMCollection As IEnumerable(Of RDBaseBeatsPerMinute)) As TimeSpan
			Dim fore As (BeatOnly As Single, BPM As Single) = (1, 100)
			Dim foreBPM = BPMCollection.FirstOrDefault()
			If foreBPM IsNot Nothing Then
				fore = (foreBPM.Beat.BeatOnly, foreBPM.BeatsPerMinute)
			End If
			Dim resultMinute As Single = 0
			For Each item As RDBaseBeatsPerMinute In BPMCollection
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
		Private Shared Function TimeSpanToBeatOnly(timeSpan As TimeSpan, BPMCollection As IEnumerable(Of RDBaseBeatsPerMinute)) As Single
			Dim fore As (BeatOnly As Single, BPM As Single) = (1, 100)
			Dim foreBPM = BPMCollection.FirstOrDefault()
			If foreBPM IsNot Nothing Then
				fore = (foreBPM.Beat.BeatOnly, foreBPM.BeatsPerMinute)
			End If
			Dim beatOnly As Single = 1
			For Each item As RDBaseBeatsPerMinute In BPMCollection
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
		Public Function BeatOf(beatOnly As Single) As RDBeat
			Return New RDBeat(Me, beatOnly)
		End Function
		''' <summary>
		''' Creates a beat instance.
		''' </summary>
		Public Function BeatOf(bar As UInteger, beat As Single) As RDBeat
			Return New RDBeat(Me, bar, beat)
		End Function
		''' <summary>
		''' Creates a beat instance.
		''' </summary>
		Public Function BeatOf(timeSpan As TimeSpan) As RDBeat
			Return New RDBeat(Me, timeSpan)
		End Function
		''' <summary>
		''' Calculate the BPM of the moment in which the beat is.
		''' </summary>
		Public Function BeatsPerMinuteOf(beat As RDBeat) As Single
			Return If(_BPMList.LastOrDefault(Function(i) i.Beat < beat)?.BeatsPerMinute, 100)
		End Function
		''' <summary>
		''' Calculate the CPB of the moment in which the beat is.
		''' </summary>
		Public Function CrotchetsPerBarOf(beat As RDBeat) As Single
			Return If(_CPBList.LastOrDefault(Function(i) i.Beat < beat)?.CrotchetsPerBar, 8)
		End Function
	End Class
	''' <summary>
	''' Beat Calculator.
	''' </summary>
	Public Class ADBeatCalculator
		Friend Collection As ADLevel
		Friend Sub New(level As ADLevel)
			Collection = level
		End Sub
	End Class
	Public Module Utils
		''' <summary>
		''' Converts percentage point to pixel point with default screen size (352 * 198).
		''' </summary>
		Public Function PercentToPixel(point As (X As Single?, Y As Single?)) As (X As Single?, Y As Single?)
			Return PercentToPixel(point, (352, 198))
		End Function
		''' <summary>
		''' Converts percentage point to pixel point with specified size.
		''' </summary>
		''' <param name="size">Specified size.</param>
		Public Function PercentToPixel(point As (X As Single?, Y As Single?), size As (X As Single, Y As Single)) As (X As Single?, Y As Single?)
			Return (point.X * size.X / 100, point.Y * size.Y / 100)
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
	End Module
	Public Module TypeConvert
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function RDConvertToEnum(type As Type) As RDEventType
			If RDETypesToEnum Is Nothing Then
				Dim result As RDEventType
				If type.Name.StartsWith("RD") Then
					Dim name = type.Name.Substring(2)
					If [Enum].TryParse(name, result) Then
						Return result
					End If
				End If
				Throw New IllegalEventTypeException(type, "Unable to find a matching EventType.")
			Else
				Try
					Return RDETypesToEnum(type).Single
				Catch ex As Exception
					Throw New IllegalEventTypeException(type, "Multiple matching EventTypes were found. Please check if the type is an abstract class type.", New ArgumentException("Multiple matching EventTypes were found. Please check if the type is an abstract class type.", NameOf(type)))
				End Try
			End If
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ADConvertToEnum(type As Type) As ADEventType
			If ADETypesToEnum Is Nothing Then
				Dim result As ADEventType
				If type.Name.StartsWith("AD") Then
					Dim name = type.Name.Substring(2)
					If [Enum].TryParse(name, result) Then
						Return result
					End If
				End If
				Throw New IllegalEventTypeException(type, "Unable to find a matching EventType.")
			Else
				Try
					Return ADETypesToEnum(type).Single
				Catch ex As Exception
					Throw New IllegalEventTypeException(type, "Multiple matching EventTypes were found. Please check if the type is an abstract class type.", New ArgumentException("Multiple matching EventTypes were found. Please check if the type is an abstract class type.", NameOf(type)))
				End Try
			End If
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ConvertToRDEnum(Of T As {RDBaseEvent, New})() As RDEventType
			Return RDConvertToEnum(GetType(T))
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ConvertToADEnum(Of T As {ADBaseEvent, New})() As ADEventType
			Return ADConvertToEnum(GetType(T))
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ConvertToRDEnums(Of T As RDBaseEvent)() As RDEventType()
			Try
				Return RDETypesToEnum(GetType(T))
			Catch ex As Exception
				Throw New IllegalEventTypeException(GetType(T), "This exception is not expected. Please contact the developer to handle this exception.", ex)
			End Try
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ConvertToADEnums(Of T As RDBaseEvent)() As ADEventType()
			Try
				Return ADETypesToEnum(GetType(T))
			Catch ex As Exception
				Throw New IllegalEventTypeException(GetType(T), "This exception is not expected. Please contact the developer to handle this exception.")
			End Try
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function RDConvertToType(type As String) As Type
			Dim result As RDEventType
			If [Enum].TryParse(type, result) Then
				Return result.ConvertToType()
			End If
			Return RDEventType.CustomEvent.ConvertToType
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		Public Function ADConvertToType(type As String) As Type
			Dim result As ADEventType
			If [Enum].TryParse(type, result) Then
				Return result.ConvertToType()
			End If
			Return ADEventType.CustomEvent.ConvertToType
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		<Extension> Public Function ConvertToType(type As RDEventType) As Type
			If RDEnumToEType Is Nothing Then
				Dim result = System.Type.GetType($"{GetType(RDBaseEvent).Namespace}.RD{type}")
				If result Is Nothing Then
					Throw New RhythmBaseException($"Illegal Type: {type}.")
				End If
				Return result
			Else
				Try
					Return RDEnumToEType(type)
				Catch ex As Exception
					Throw New IllegalEventTypeException(type, "This value does not exist in the EventType enumeration.")
				End Try
			End If
		End Function
		''' <summary>
		''' Conversion between types and enumerations.
		''' </summary>
		<Extension> Public Function ConvertToType(type As ADEventType) As Type
			If ADEnumToEType Is Nothing Then
				Dim result = System.Type.GetType($"{GetType(ADBaseEvent).Namespace}.AD{type}")
				If result Is Nothing Then
					Throw New RhythmBaseException($"Illegal Type: {type}.")
				End If
				Return result
			Else
				Try
					Return ADEnumToEType(type)
				Catch ex As Exception
					Throw New IllegalEventTypeException(type, "This value does not exist in the EventType enumeration.")
				End Try
			End If
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