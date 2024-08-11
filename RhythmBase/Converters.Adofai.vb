Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RhythmBase.Adofai.Utils
Imports RhythmBase.Adofai.Events
Imports RhythmBase.Adofai.Components
Namespace Adofai
	Namespace Converters
		Friend Class ADLevelConverter
			Inherits JsonConverter(Of ADLevel)
			Private ReadOnly fileLocation As String
			Private ReadOnly settings As LevelReadOrWriteSettings
			Public Sub New(location As String, settings As LevelReadOrWriteSettings)
				fileLocation = location
				Me.settings = settings
			End Sub
			Public Overrides Sub WriteJson(writer As JsonWriter, value As ADLevel, serializer As JsonSerializer)

				Dim AllInOneSerializer As New JsonSerializerSettings With {
				.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver,
				.Formatting = Formatting.None
				}
				With AllInOneSerializer.Converters
					.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
					.Add(New ColorConverter)
				End With
				With writer
					.Formatting = If(settings.Indented, Formatting.Indented, Formatting.None)
					.WriteStartObject()

					.WritePropertyName("angleData")
					.WriteStartArray()
					For Each item In value
						.WriteRawValue(JsonConvert.SerializeObject(item.Angle, Formatting.None))
					Next
					.WriteEndArray()

					.WritePropertyName("settings")
					.WriteRawValue(JsonConvert.SerializeObject(value.Settings, Formatting.Indented, AllInOneSerializer))

					.WritePropertyName("actions")
					.WriteStartArray()
					For Each item In value
						.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer))
					Next
					.WriteEndArray()

					.WritePropertyName("decorations")
					.WriteStartArray()
					For Each item In value.Decorations
						.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer))
					Next
					.WriteEndArray()

					.WriteEndObject()
				End With
				writer.Close()
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As ADLevel, hasExistingValue As Boolean, serializer As JsonSerializer) As ADLevel
				Dim outLevel As New ADLevel With {._path = fileLocation}
				Dim JActions As JArray
				Dim JDecorations As JArray

				Dim AllInOneSerializer = outLevel.GetSerializer(settings)

				While reader.Read
					Dim name = reader.Value
					reader.Read()
					Select Case name
						Case "settings"
							Dim jobj = JObject.Load(reader)
							outLevel.Settings = jobj.ToObject(Of ADSettings)(AllInOneSerializer)
						Case "angleData"
							Dim jobj = JArray.Load(reader)
							outLevel.AddRange(jobj.ToObject(Of List(Of ADTile))(AllInOneSerializer))
						Case "actions"
							JActions = JArray.Load(reader)
						Case "decorations"
							JDecorations = JArray.Load(reader)
						Case Else

					End Select
				End While
				reader.Close()

#Disable Warning BC42104
				JActions.ToObject(Of List(Of ADBaseTileEvent))(AllInOneSerializer)
				outLevel.Decorations.AddRange(JDecorations.ToObject(Of List(Of ADBaseEvent))(AllInOneSerializer))
#Enable Warning BC42104
				Return outLevel
			End Function
		End Class
		Friend Class ADTileConverter
			Inherits JsonConverter(Of ADTile)
			Private ReadOnly level As ADLevel
			Public Sub New(level As ADLevel)
				Me.level = level
			End Sub
			Public Overrides Sub WriteJson(writer As JsonWriter, value As ADTile, serializer As JsonSerializer)
				Throw New NotImplementedException
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As ADTile, hasExistingValue As Boolean, serializer As JsonSerializer) As ADTile
				Return New ADTile() With {.Angle = JValue.Load(reader).ToObject(Of Single), .Parent = level}
			End Function
		End Class
		Friend Class ADBaseEventConverter(Of TEvent As ADBaseEvent)
			Inherits JsonConverter(Of TEvent)
			Protected ReadOnly level As ADLevel
			Protected ReadOnly settings As LevelReadOrWriteSettings
			Protected _canread As Boolean = True
			Protected _canwrite As Boolean = True
			Public Overrides ReadOnly Property CanRead As Boolean
				Get
					Return _canread
				End Get
			End Property
			Public Overrides ReadOnly Property CanWrite As Boolean
				Get
					Return _canwrite
				End Get
			End Property
			Public Sub New(level As ADLevel, inputSettings As LevelReadOrWriteSettings)
				Me.level = level
				Me.settings = inputSettings
			End Sub
			Public Overrides Sub WriteJson(writer As JsonWriter, value As TEvent, serializer As JsonSerializer)
				Throw New NotImplementedException()
			End Sub

			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As TEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As TEvent
				Return GetDeserializedObject(JToken.ReadFrom(reader), objectType, existingValue, hasExistingValue, serializer)
			End Function
			Public Overridable Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As TEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As TEvent

				Dim SubClassType As Type = ADConvertToType(jobj("eventType").ToObject(Of String))

				_canread = False
				existingValue = If(SubClassType IsNot Nothing,
				jobj.ToObject(SubClassType, serializer),
				jobj.ToObject(Of ADCustomEvent)(serializer))
				_canread = True

				Return existingValue
			End Function
			Public Overridable Function SetSerializedObject(value As TEvent, serializer As JsonSerializer) As JObject
				_canwrite = False
				Dim JObj = JObject.FromObject(value, serializer)
				_canwrite = True

				JObj.Remove("type")

				Dim s = JObj.First
				s.AddBeforeSelf(New JProperty("eventType", value.Type.ToString))
				Return JObj
			End Function
		End Class
		Friend Class ADBaseTileEventConverter(Of TEvent As ADBaseTileEvent)
			Inherits ADBaseEventConverter(Of TEvent)
			Public Sub New(level As ADLevel, inputSettings As LevelReadOrWriteSettings)
				MyBase.New(level, inputSettings)
			End Sub
			Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As TEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As TEvent
				Dim parentIndex = jobj("floor")?.ToObject(Of Integer)

				_canread = False
				If ADConvertToType(jobj("eventType").ToObject(Of String)) = GetType(ADCustomEvent) Then
					existingValue = CType((New ADCustomTileEventConverter(level, settings)).GetDeserializedObject(jobj, objectType, Nothing, hasExistingValue, serializer), ADBaseTileEvent)
				Else
					jobj.Remove("floor")
					existingValue = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
				End If
				_canread = True

				If parentIndex.HasValue Then
					existingValue.Parent = level(parentIndex.Value)
					existingValue.Parent.Add(existingValue)
				End If
				Return existingValue
			End Function
			Public Overrides Function SetSerializedObject(value As TEvent, serializer As JsonSerializer) As JObject
				Dim jobj = MyBase.SetSerializedObject(value, serializer)

				Dim s = jobj.First
				s.AddBeforeSelf(New JProperty("floor", level.tileOrder.IndexOf(value.Parent)))

				Return jobj
			End Function
		End Class
		Friend Class ADCustomEventConverter
			Inherits ADBaseEventConverter(Of ADCustomEvent)
			Public Sub New(level As ADLevel, settings As LevelReadOrWriteSettings)
				MyBase.New(level, settings)
			End Sub
			Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As ADCustomEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As ADCustomEvent
				Dim result As New ADCustomEvent With {
				.Data = jobj
			}
				Return result
			End Function
		End Class
		Friend Class ADCustomTileEventConverter
			Inherits ADBaseTileEventConverter(Of ADCustomTileEvent)
			Public Sub New(level As ADLevel, settings As LevelReadOrWriteSettings)
				MyBase.New(level, settings)
			End Sub
			Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As ADCustomTileEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As ADCustomTileEvent
				Dim result As New ADCustomTileEvent With {
				.Parent = level(jobj("floor").ToObject(Of Integer)),
				.Data = jobj
			}
				Return result
			End Function
		End Class
	End Namespace
End Namespace
