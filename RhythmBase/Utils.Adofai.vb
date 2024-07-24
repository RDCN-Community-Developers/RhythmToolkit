Imports System.Runtime.CompilerServices
Imports RhythmBase.Adofai.Components
Imports RhythmBase.Adofai.Converters
Imports RhythmBase.Adofai.Events
Imports Newtonsoft.Json

Namespace Adofai
	Namespace Utils
		Public Module Utils
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
			Public Function ConvertToADEnum(Of T As {ADBaseEvent, New})() As ADEventType
				Return ADConvertToEnum(GetType(T))
			End Function
			''' <summary>
			''' Conversion between types and enumerations.
			''' </summary>
			Public Function ConvertToADEnums(Of T As BaseEvent)() As ADEventType()
				Try
					Return ADETypesToEnum(GetType(T))
				Catch ex As Exception
					Throw New IllegalEventTypeException(GetType(T), "This exception is not expected. Please contact the developer to handle this exception.")
				End Try
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
			<Extension> Public Function GetSerializer(adlevel As ADLevel, settings As LevelReadOrWriteSettings) As JsonSerializer
				Dim AllInOneSerializer As New JsonSerializer
				With AllInOneSerializer.Converters
					.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
					.Add(New ColorConverter)
					.Add(New ADTileConverter(adlevel))
					.Add(New ADCustomTileEventConverter(adlevel, settings))
					.Add(New ADCustomEventConverter(adlevel, settings))
					.Add(New ADBaseTileEventConverter(Of ADBaseTileEvent)(adlevel, settings))
					.Add(New ADBaseEventConverter(Of ADBaseEvent)(adlevel, settings))
				End With
				Return AllInOneSerializer
			End Function
		End Module
		''' <summary>
		''' Beat Calculator.
		''' </summary>
		Public Class ADBeatCalculator
			Friend Collection As ADLevel
			Private _DefaultBpm As Single
			Private _MidSpins As List(Of ADTile)
			Private _SetSpeeds As List(Of ADSetSpeed)
			Private _Twirls As List(Of ADTwirl)
			Private _Pauses As List(Of ADPause)
			Private _Holds As List(Of ADHold)
			Private _Freeroams As List(Of ADFreeRoam)
			Friend Sub New(level As ADLevel)
				Collection = level
				Refresh()
			End Sub
			Private Sub Refresh()
				_DefaultBpm = Collection.Settings.Bpm
				_MidSpins = Collection.Where(Function(i) i.IsMidSpin).ToList
				_SetSpeeds = Collection.EventsWhere(Of ADSetSpeed).ToList
				_Twirls = Collection.EventsWhere(Of ADTwirl).ToList
				_Pauses = Collection.EventsWhere(Of ADPause).ToList
				_Holds = Collection.EventsWhere(Of ADHold).ToList
				_Freeroams = Collection.EventsWhere(Of ADFreeRoam).ToList
			End Sub
		End Class
	End Namespace
End Namespace
