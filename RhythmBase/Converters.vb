Imports System.Reflection
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports SkiaSharp
Namespace Converters
#Region "RD"
	Friend Class RDLevelConverter
		Inherits JsonConverter(Of RDLevel)
		Private ReadOnly fileLocation As String
		Private ReadOnly settings As LeveReadOrWriteSettings
		Public Sub New(location As String, settings As LeveReadOrWriteSettings)
			fileLocation = location
			Me.settings = settings
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDLevel, serializer As JsonSerializer)

			Dim AllInOneSerializer As New JsonSerializerSettings With {
				.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver,
				.Formatting = Formatting.None
				}
			With AllInOneSerializer.Converters
				.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				.Add(New CharacterConverter(fileLocation, value.Assets))
				.Add(New AssetConverter(fileLocation, value.Assets, settings))
				.Add(New BookmarkConverter(New RDBeatCalculator(value)))
				.Add(New ColorConverter)
				.Add(New PanelColorConverter(value.ColorPalette))
				.Add(New ConditionConverter(value.Conditionals))
				.Add(New CustomEventConverter(value, settings))
				.Add(New TagActionConverter(value, settings))
				.Add(New BaseRDEventConverter(Of RDBaseEvent)(value, settings))
			End With

			With writer
				.Formatting = If(settings.Indented, Formatting.Indented, Formatting.None)
				.WriteStartObject()

				.WritePropertyName("settings")
				.WriteRawValue(JsonConvert.SerializeObject(value.Settings, Formatting.Indented, AllInOneSerializer))

				.WritePropertyName("rows")
				.WriteStartArray()
				For Each item In value.Rows
					.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer))
				Next
				.WriteEndArray()

				.WritePropertyName("decorations")
				.WriteStartArray()
				For Each item In value.Decorations
					.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer))
				Next
				.WriteEndArray()

				.WritePropertyName("events")
				.WriteStartArray()
				For Each item In value
					.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer))
				Next
				.WriteEndArray()

				.WritePropertyName("conditionals")
				.WriteStartArray()
				For Each item In value.Conditionals
					.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer))
				Next
				.WriteEndArray()

				.WritePropertyName("bookmarks")
				.WriteStartArray()
				For Each item In value.Bookmarks
					.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer))
				Next
				.WriteEndArray()

				.WritePropertyName("colorPalette")
				.WriteStartArray()
				For Each item In value.ColorPalette
					.WriteRawValue(JsonConvert.SerializeObject(item, Formatting.None, AllInOneSerializer))
				Next
				.WriteEndArray()

				.WriteEndObject()
			End With
			writer.Close()

		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDLevel, hasExistingValue As Boolean, serializer As JsonSerializer) As RDLevel
			Dim assetsCollection As New HashSet(Of RDSprite)
			Dim SettingsSerializer As New JsonSerializer
			With SettingsSerializer.Converters
			End With
			Dim RowsSerializer As New JsonSerializer
			With RowsSerializer.Converters
				.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				.Add(New CharacterConverter(fileLocation, assetsCollection))
			End With
			Dim DecorationsSerializer As New JsonSerializer
			With DecorationsSerializer.Converters
				.Add(New AssetConverter(fileLocation, assetsCollection, settings))
			End With
			Dim ConditionalsSerializer As New JsonSerializer
			With ConditionalsSerializer.Converters
				.Add(New ConditionalConverter)
			End With
			Dim ColorPaletteSerializer As New JsonSerializer
			With ColorPaletteSerializer.Converters
				.Add(New ColorConverter)
			End With

			Dim outLevel As New RDLevel With {._path = fileLocation}
			Dim JEvents As JArray
			Dim JBookmarks As JArray

			While reader.Read
				Dim name = reader.Value
				reader.Read()
				Select Case name
					Case "settings"
						Dim Jobj = JObject.Load(reader)
						Dim Mods = Jobj("mods")
						If Mods?.Type = JTokenType.String Then
							Jobj("mods") = New JArray(Mods)
						End If
						outLevel.Settings = Jobj.ToObject(Of RDSettings)(SettingsSerializer)
					Case "rows"
						Dim Jarr = JArray.Load(reader)
						outLevel._Rows.AddRange(Jarr.ToObject(Of List(Of RDRow))(RowsSerializer))
						For Each item In outLevel.Rows
							item.Parent = outLevel
						Next
					Case "decorations"
						Dim Jarr = JArray.Load(reader)
						outLevel._Decorations.AddRange(Jarr.ToObject(Of List(Of RDDecoration))(DecorationsSerializer))
						For Each item In outLevel.Decorations
							item.Parent = outLevel
						Next
					Case "conditionals"
						Dim Jarr = JArray.Load(reader)
						outLevel.Conditionals.AddRange(Jarr.ToObject(Of List(Of RDBaseConditional))(ConditionalsSerializer))
						For Each item In outLevel.Conditionals
							item.ParentCollection = outLevel.Conditionals
						Next
					Case "colorPalette"
						Dim Jarr = JArray.Load(reader)
						For Each item In Jarr.ToObject(Of SKColor())(ColorPaletteSerializer)
							outLevel.ColorPalette.Add(item)
						Next
					Case "events"
						JEvents = JArray.Load(reader)
					Case "bookmarks"
						JBookmarks = JArray.Load(reader)
					Case Else
				End Select
			End While
			reader.Close()

			Dim itemToThrow As JObject

			Try
#Disable Warning BC42104
				Dim EventsSerializer As New JsonSerializer With {
						.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
					}
				With EventsSerializer.Converters
					.Add(New PanelColorConverter(outLevel.ColorPalette))
					.Add(New AssetConverter(outLevel.Path, outLevel.Assets, settings))
					.Add(New ConditionConverter(outLevel.Conditionals))
					.Add(New TagActionConverter(outLevel, settings))
					.Add(New CustomDecorationEventConverter(outLevel, settings))
					.Add(New CustomRowEventConverter(outLevel, settings))
					.Add(New CustomEventConverter(outLevel, settings))
					.Add(New BaseRowActionConverter(Of RDBaseRowAction)(outLevel, settings))
					.Add(New BaseDecorationActionConverter(Of RDBaseDecorationAction)(outLevel, settings))
					.Add(New BaseRDEventConverter(Of RDBaseEvent)(outLevel, settings))
					.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				End With

				Dim FloatingTextCollection As New List(Of ([event] As RDFloatingText, id As Integer))
				Dim AdvanceTextCollection As New List(Of ([event] As RDAdvanceText, id As Integer))

				Dim calculator As New RDBeatCalculator(outLevel)


				For Each item In JEvents
					itemToThrow = item
					Dim eventType = RDConvertToType(item("type"))
					If eventType Is Nothing Then
						Dim TempEvent As RDBaseEvent
						If item("target") IsNot Nothing Then
							TempEvent = item.ToObject(Of RDCustomDecorationEvent)(EventsSerializer)
						ElseIf item("row") IsNot Nothing Then
							TempEvent = item.ToObject(Of RDCustomRowEvent)(EventsSerializer)
						Else
							TempEvent = item.ToObject(Of RDCustomEvent)(EventsSerializer)
						End If
						outLevel.Add(TempEvent)
					Else
						Dim TempEvent As RDBaseEvent = item.ToObject(eventType, EventsSerializer)
						If TempEvent IsNot Nothing Then
							If TempEvent.Type <> RDEventType.CustomEvent Then
								Select Case TempEvent.Type
									'浮动文字事件记录
									Case RDEventType.FloatingText
										FloatingTextCollection.Add((CType(TempEvent, RDFloatingText), item("id")))

									Case RDEventType.AdvanceText
										AdvanceTextCollection.Add((CType(TempEvent, RDAdvanceText), item("id")))

								End Select
							End If
							outLevel.Add(TempEvent)
						End If
					End If
				Next
				'浮动文字事件关联
				For Each AdvancePair In AdvanceTextCollection
					Dim Parent = FloatingTextCollection.First(Function(i) i.id = AdvancePair.id).event
					Parent.Children.Add(AdvancePair.event)
					AdvancePair.event.Parent = Parent
				Next
				Dim BookmarksSerializer As New JsonSerializer
				With BookmarksSerializer.Converters
					.Add(New BookmarkConverter(New RDBeatCalculator(outLevel)))
				End With
				outLevel.Bookmarks.AddRange(JBookmarks.ToObject(Of List(Of RDBookmark))(BookmarksSerializer))
				Return outLevel
			Catch ex As Exception
				If outLevel.Settings.Version < 55 Then
					Throw New VersionTooLowException(outLevel.Settings.Version, ex)
				Else
					Throw New ConvertingException(ex)
				End If
#Enable Warning BC42104
			End Try
		End Function
	End Class
	Friend Class BookmarkConverter
		Inherits JsonConverter(Of RDBookmark)
		Private ReadOnly calculator As RDBeatCalculator
		Public Sub New(calculator As RDBeatCalculator)
			Me.calculator = calculator
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDBookmark, serializer As JsonSerializer)
			Dim beat = value.Beat.BarBeat
			With writer
				.WriteStartObject()
				.WritePropertyName("bar")
				.WriteValue(beat.bar)
				.WritePropertyName("beat")
				.WriteValue(beat.beat)
				.WritePropertyName("color")
				.WriteValue(value.Color)
				.WriteEndObject()
			End With
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDBookmark, hasExistingValue As Boolean, serializer As JsonSerializer) As RDBookmark
			Dim jobj = JToken.ReadFrom(reader)
			Return New RDBookmark With {
				.Beat = calculator.BeatOf(jobj("bar").ToObject(Of UInteger), jobj("beat").ToObject(Of Single)),
				.Color = [Enum].Parse(Of RDBookmark.BookmarkColors)(jobj("color"))
			}
		End Function
	End Class
	Friend Class CharacterConverter
		Inherits JsonConverter(Of RDCharacter)
		Private ReadOnly fileLocation As String
		Private ReadOnly assets As HashSet(Of RDSprite)
		Public Sub New(location As String, assets As HashSet(Of RDSprite))
			fileLocation = location
			Me.assets = assets
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDCharacter, serializer As JsonSerializer)
			If value.IsCustom Then
				writer.WriteValue($"custom:{value.CustomCharacter.FileName}")
			Else
				writer.WriteValue(value.Character.ToString)
			End If
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDCharacter, hasExistingValue As Boolean, serializer As JsonSerializer) As RDCharacter
			Dim value = JToken.ReadFrom(reader).ToObject(Of String)
			If value.StartsWith("custom:") Then
				Dim name = value.Substring(7)
				Return New RDCharacter(If(assets.SingleOrDefault(Function(i) i.FileName = name), New RDSprite($"{IO.Path.GetDirectoryName(fileLocation)}\{name}")))
			Else
				Return New RDCharacter([Enum].Parse(Of Characters)(value))
			End If
		End Function
	End Class
	Friend Class AnchorStyleConverter
		Inherits JsonConverter(Of RDFloatingText.AnchorStyle)
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDFloatingText.AnchorStyle, serializer As JsonSerializer)
			Dim v1 = If(value And &B11, "Middle", [Enum].Parse(Of RDFloatingText.AnchorStyle)(value And &B11).ToString)
			writer.WriteValue(If(value And &B11, [Enum].Parse(Of RDFloatingText.AnchorStyle)(value And &B11).ToString, "Middle") +
								  [Enum].Parse(Of RDFloatingText.AnchorStyle)(value And &B1100).ToString)
		End Sub

		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDFloatingText.AnchorStyle, hasExistingValue As Boolean, serializer As JsonSerializer) As RDFloatingText.AnchorStyle
			Dim JString As String = JToken.ReadFrom(reader).ToObject(Of String)
			Dim match = Regex.Match(JString, "([A-Z][a-z]+)([A-Z][a-z]+)")
			Dim middle As Boolean = False
			Dim center As Boolean = False
			Dim result As New RDFloatingText.AnchorStyle
			Select Case match.Groups(1).Value
				Case "Upper"
					result = result Or RDFloatingText.AnchorStyle.Upper
				Case "Lower"
					result = result Or RDFloatingText.AnchorStyle.Lower
				Case Else
					middle = True
			End Select
			Select Case match.Groups(2).Value
				Case "Left"
					result = result Or RDFloatingText.AnchorStyle.Left
				Case "Right"
					result = result Or RDFloatingText.AnchorStyle.Right
				Case Else
					center = True
			End Select
			If center And middle Then
				result = RDFloatingText.AnchorStyle.Center
			End If
			Return result
		End Function
	End Class
	Friend Class BaseRDEventConverter(Of T As RDBaseEvent)
		Inherits JsonConverter(Of T)
		Protected ReadOnly level As RDLevel
		Protected ReadOnly settings As LeveReadOrWriteSettings
		Private _canread As Boolean = True
		Private _canwrite As Boolean = True
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
		Public Sub New(level As RDLevel, inputSettings As LeveReadOrWriteSettings)
			Me.level = level
			Me.settings = inputSettings
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As T, serializer As JsonSerializer)
			serializer.Formatting = Formatting.None
			With writer
				.WriteRawValue(JsonConvert.SerializeObject(SetSerializedObject(value, serializer)))
			End With
			serializer.Formatting = Formatting.Indented
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As T, hasExistingValue As Boolean, serializer As JsonSerializer) As T
			Return GetDeserializedObject(JToken.ReadFrom(reader), objectType, existingValue, hasExistingValue, serializer)
		End Function
		Public Overridable Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As T, hasExistingValue As Boolean, serializer As JsonSerializer) As T

			Dim SubClassType As Type = RDConvertToType(jobj("type").ToObject(Of String))

			If SubClassType Is Nothing Then
				If jobj("target") IsNot Nothing Then
					SubClassType = GetType(RDCustomDecorationEvent)
				ElseIf jobj("row") IsNot Nothing Then
					SubClassType = GetType(RDCustomRowEvent)
				Else
					SubClassType = GetType(RDCustomEvent)
				End If
			End If
			_canread = False
			existingValue = jobj.ToObject(SubClassType, serializer)
			_canread = True
			existingValue._beat = level.Calculator.BeatOf(UInteger.Parse(jobj("bar")), Single.Parse(If(jobj("beat"), 1)))
			Return existingValue
		End Function
		Public Overridable Function SetSerializedObject(value As T, serializer As JsonSerializer) As JObject
			_canwrite = False
			Dim JObj = JObject.FromObject(value, serializer)
			_canwrite = True

			JObj.Remove("type")

			Dim b = value.Beat.BarBeat
			Dim s = JObj.First
			s.AddBeforeSelf(New JProperty("bar", b.bar))
			s.AddBeforeSelf(New JProperty("beat", b.beat))
			s.AddBeforeSelf(New JProperty("type", value.Type.ToString))
			Return JObj
		End Function
	End Class
	Friend Class CustomEventConverter
		Inherits BaseRDEventConverter(Of RDCustomEvent)
		Public Sub New(level As RDLevel, inputSettings As LeveReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As RDCustomEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As RDCustomEvent
			Dim result = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			result.Data = jobj
			Return result
		End Function
		Public Overrides Function SetSerializedObject(value As RDCustomEvent, serializer As JsonSerializer) As JObject
			Dim jobj = MyBase.SetSerializedObject(value, serializer)
			Dim data = value.Data.DeepClone
			For Each item In jobj
				data(item.Key) = item.Value
			Next
			Return data
		End Function
	End Class
	Friend Class CustomRowEventConverter
		Inherits BaseRowActionConverter(Of RDCustomRowEvent)
		Public Sub New(level As RDLevel, inputSettings As LeveReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As RDCustomRowEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As RDCustomRowEvent
			Dim result = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			result.Data = jobj
			Return result
		End Function
		Public Overrides Function SetSerializedObject(value As RDCustomRowEvent, serializer As JsonSerializer) As JObject
			Dim jobj = MyBase.SetSerializedObject(value, serializer)
			Dim data = value.Data.DeepClone
			For Each item In jobj
				data(item.Key) = item.Value
			Next
			Return data
		End Function
	End Class
	Friend Class CustomDecorationEventConverter
		Inherits BaseDecorationActionConverter(Of RDCustomDecorationEvent)
		Public Sub New(level As RDLevel, inputSettings As LeveReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As RDCustomDecorationEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As RDCustomDecorationEvent
			Dim result = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			result.Data = jobj
			Return result
		End Function
		Public Overrides Function SetSerializedObject(value As RDCustomDecorationEvent, serializer As JsonSerializer) As JObject
			Dim jobj = MyBase.SetSerializedObject(value, serializer)
			Dim data = value.Data.DeepClone
			For Each item In jobj
				data(item.Key) = item.Value
			Next
			Return data
		End Function
	End Class
	Friend Class BaseRowActionConverter(Of T As RDBaseRowAction)
		Inherits BaseRDEventConverter(Of T)
		Public Sub New(level As RDLevel, inputSettings As LeveReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As T, hasExistingValue As Boolean, serializer As JsonSerializer) As T
			Dim obj = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			Try
				Dim rowId = jobj("row").ToObject(Of Short)
				If rowId = -1 Then
					If obj.Type <> RDEventType.TintRows Then
						If settings.UnreadableEventAction = ActionOnUnreadableEvents.Store Then
							settings.UnreadableEvents.Add(jobj)
							Return Nothing
						Else
							Throw New ConvertingException($"Cannot find the row ""{jobj("target")}"" at {obj}")
						End If
					End If
				Else
					Dim Parent = level._Rows(rowId)
					obj._parent = Parent
				End If
			Catch e As Exception
				Throw New ConvertingException($"Cannot find the row {jobj("row")} at {obj}")
			End Try
			Return obj
		End Function
	End Class
	Friend Class BaseDecorationActionConverter(Of T As RDBaseDecorationAction)
		Inherits BaseRDEventConverter(Of T)
		Public Sub New(level As RDLevel, inputSettings As LeveReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As T, hasExistingValue As Boolean, serializer As JsonSerializer) As T
			Dim obj = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)

			Dim decoId As String = jobj("target")?.ToObject(Of String)
			Dim Parent = level._Decorations.FirstOrDefault(Function(i) i.Id = decoId)
			obj._parent = Parent
			If Parent Is Nothing AndAlso decoId = String.Empty AndAlso obj.Type <> RDEventType.Comment Then
				If settings.UnreadableEventAction = ActionOnUnreadableEvents.Store Then
					settings.UnreadableEvents.Add(jobj)
					Return Nothing
				Else
					Throw New ConvertingException($"Cannot find the decoration ""{jobj("target")}"" at {obj}")
				End If
			End If
			Return obj
		End Function
	End Class
	Friend Class AssetConverter
		Inherits JsonConverter(Of RDSprite)
		Private ReadOnly fileLocation As String
		Private ReadOnly assets As HashSet(Of RDSprite)
		Private ReadOnly settings As LeveReadOrWriteSettings
		Public Sub New(location As String, assets As HashSet(Of RDSprite), settings As LeveReadOrWriteSettings)
			fileLocation = location
			Me.assets = assets
			Me.settings = settings
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDSprite, serializer As JsonSerializer)
			writer.WriteValue(value.FileName)
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDSprite, hasExistingValue As Boolean, serializer As JsonSerializer) As RDSprite
			Dim Json = JToken.ReadFrom(reader).ToObject(Of String)
			Dim assetName = Json
			Dim result As RDSprite
			If assets.Any(Function(i) i.FileName = assetName) Then
				result = assets.Single(Function(i) i.FileName = assetName)
			ElseIf Json = String.Empty Then
				Return Nothing
			Else
				Dim file = IO.Path.GetDirectoryName(fileLocation) + "\" + Json
				result = New RDSprite(file)
				If Me.settings.PreloadAssets Then
					result.Reload()
				End If
				assets.Add(result)
			End If
			Return result
		End Function
	End Class
	Friend Class RoomConverter
		Inherits JsonConverter
		Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
			Select Case value.GetType
				Case GetType(RDRoom)
					writer.WriteRawValue($"[{String.Join(",", CType(value, RDRoom).Rooms)}]")
				Case GetType(RDSingleRoom)
					writer.WriteRawValue($"[{ CType(value, RDSingleRoom).Value}]")
				Case Else
					Throw New NotImplementedException
			End Select
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
			Dim J = JArray.Load(reader).ToObject(Of Byte())
			Select Case objectType
				Case GetType(RDRoom)
					For Each item In J
						existingValue(item) = True
					Next
					Return existingValue
				Case GetType(RDSingleRoom)
					Return New RDSingleRoom(J.Single)
				Case Else
					Throw New NotImplementedException
			End Select
		End Function
		Public Overrides Function CanConvert(objectType As Type) As Boolean
			Return objectType = GetType(RDRoom) OrElse objectType = GetType(RDSingleRoom)
		End Function
	End Class
	Friend Class TagActionConverter
		Inherits BaseRDEventConverter(Of RDTagAction)
		Public Sub New(level As RDLevel, inputSettings As LeveReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function SetSerializedObject(value As RDTagAction, serializer As JsonSerializer) As JObject
			Dim jobj = MyBase.SetSerializedObject(value, serializer)
			If value.Tag Is Nothing Then
				jobj.Remove("tag")
			Else
				jobj("tag") = value.Tag
			End If
			jobj("Tag") = value.ActionTag
			Return jobj
		End Function
	End Class
	Friend Class PatternConverter
		Inherits JsonConverter(Of LimitedList(Of Patterns))
		Public Overrides Sub WriteJson(writer As JsonWriter, value As LimitedList(Of Patterns), serializer As JsonSerializer)
			Dim out = ""
			For Each item In value
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
			writer.WriteValue(out)
		End Sub

		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As LimitedList(Of Patterns), hasExistingValue As Boolean, serializer As JsonSerializer) As LimitedList(Of Patterns)
			For Each c In JToken.ReadFrom(reader).ToObject(Of String)
				Select Case c
					Case "x"c
						existingValue.Add(Patterns.X)
					Case "u"c
						existingValue.Add(Patterns.Up)
					Case "d"c
						existingValue.Add(Patterns.Down)
					Case "b"c
						existingValue.Add(Patterns.Banana)
					Case "r"c
						existingValue.Add(Patterns.Return)
					Case "-"c
						existingValue.Add(Patterns.None)
				End Select
			Next
			Return existingValue
		End Function
	End Class
	Friend Class ConditionalConverter
		Inherits JsonConverter(Of RDBaseConditional)
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDBaseConditional, serializer As JsonSerializer)
			Dim S As New JsonSerializerSettings
			S.Converters.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
			S.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
			writer.WriteRawValue(JsonConvert.SerializeObject(value, S))
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDBaseConditional, hasExistingValue As Boolean, serializer As JsonSerializer) As RDBaseConditional
			Dim J = JObject.Load(reader)
			Dim ConditionalsType As Type = GetType(RDBaseConditional)
			Dim SubClassType As Type = Type.GetType($"{GetType(RDBaseConditional).Namespace}.{NameOf(Conditions)}.{J("type")}")
			Dim Conditional As RDBaseConditional = J.ToObject(SubClassType)
			Return Conditional
		End Function
	End Class
	Friend Class ConditionConverter
		Inherits JsonConverter(Of RDCondition)
		Private ReadOnly conditionals As List(Of RDBaseConditional)
		Public Sub New(Conditionals As List(Of RDBaseConditional))
			Me.conditionals = Conditionals
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDCondition, serializer As JsonSerializer)
			writer.WriteValue(value.Serialize)
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDCondition, hasExistingValue As Boolean, serializer As JsonSerializer) As RDCondition
			Dim J = JToken.Load(reader).ToObject(Of String)

			Dim Value As New RDCondition()

			Dim ConditionIds = Regex.Matches(J, "~?\d+(?=[&d])")
			For Each match As Match In ConditionIds
				Dim vs = Val("~2")
				Dim Parent = conditionals.Where(Function(i) i.Id = CInt(Regex.Match(match.Value, "\d+").Value)).First
				Value.ConditionLists.Add((match.Value(0) <> "~"c, Parent))
			Next
			Value.Duration = Regex.Match(J, "(?<=d)[\d\.]+").Value
			Return Value
		End Function
	End Class
#End Region

#Region "AD"
	Friend Class ADLevelConverter
		Inherits JsonConverter(Of ADLevel)
		Private ReadOnly fileLocation As String
		Private ReadOnly settings As LeveReadOrWriteSettings
		Public Sub New(location As String, settings As LeveReadOrWriteSettings)
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

			Dim AllInOneSerializer As New JsonSerializer
			With AllInOneSerializer.Converters
				.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				.Add(New ColorConverter)
				.Add(New ADTileConverter(outLevel))
				.Add(New ADCustomTileEventConverter(outLevel, settings))
				.Add(New ADCustomEventConverter(outLevel, settings))
				.Add(New ADBaseTileEventConverter(Of ADBaseTileEvent)(outLevel, settings))
				.Add(New ADBaseEventConverter(Of ADBaseEvent)(outLevel, settings))
			End With


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
	Friend Class ADBaseEventConverter(Of T As ADBaseEvent)
		Inherits JsonConverter(Of T)
		Protected ReadOnly level As ADLevel
		Protected ReadOnly settings As LeveReadOrWriteSettings
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
		Public Sub New(level As ADLevel, inputSettings As LeveReadOrWriteSettings)
			Me.level = level
			Me.settings = inputSettings
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As T, serializer As JsonSerializer)
			Throw New NotImplementedException()
		End Sub

		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As T, hasExistingValue As Boolean, serializer As JsonSerializer) As T
			Return GetDeserializedObject(JToken.ReadFrom(reader), objectType, existingValue, hasExistingValue, serializer)
		End Function
		Public Overridable Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As T, hasExistingValue As Boolean, serializer As JsonSerializer) As T

			Dim SubClassType As Type = ADConvertToType(jobj("eventType").ToObject(Of String))

			_canread = False
			existingValue = If(SubClassType IsNot Nothing,
				jobj.ToObject(SubClassType, serializer),
				jobj.ToObject(Of ADCustomEvent)(serializer))
			_canread = True

			Return existingValue
		End Function
		Public Overridable Function SetSerializedObject(value As T, serializer As JsonSerializer) As JObject
			_canwrite = False
			Dim JObj = JObject.FromObject(value, serializer)
			_canwrite = True

			JObj.Remove("type")

			Dim s = JObj.First
			s.AddBeforeSelf(New JProperty("eventType", value.Type.ToString))
			Return JObj
		End Function
	End Class
	Friend Class ADBaseTileEventConverter(Of T As ADBaseTileEvent)
		Inherits ADBaseEventConverter(Of T)
		Public Sub New(level As ADLevel, inputSettings As LeveReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As T, hasExistingValue As Boolean, serializer As JsonSerializer) As T
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
		Public Overrides Function SetSerializedObject(value As T, serializer As JsonSerializer) As JObject
			Dim jobj = MyBase.SetSerializedObject(value, serializer)

			Dim s = jobj.First
			s.AddBeforeSelf(New JProperty("floor", level.eventsOrder.IndexOf(value.Parent)))

			Return jobj
		End Function
	End Class
	Friend Class ADCustomEventConverter
		Inherits ADBaseEventConverter(Of ADCustomEvent)
		Public Sub New(level As ADLevel, settings As LeveReadOrWriteSettings)
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
		Public Sub New(level As ADLevel, settings As LeveReadOrWriteSettings)
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
#End Region

#Region "Others"

	Friend Class LimitedListConverter
		Inherits JsonConverter(Of LimitedList)
		Public Overrides Sub WriteJson(writer As JsonWriter, value As LimitedList, serializer As JsonSerializer)
			writer.WriteStartArray()
			For Each item In value
				serializer.Serialize(writer, item)
				'writer.WriteRawValue(JsonConvert.SerializeObject(item, serializer))
			Next
			writer.WriteEndArray()
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As LimitedList, hasExistingValue As Boolean, serializer As JsonSerializer) As LimitedList
			Dim J = JArray.Load(reader)
			If existingValue Is Nothing Then
				existingValue = New LimitedList(Of Object)(J.Count)
			End If
			existingValue.Clear()
			For Each item In J
				existingValue.Add(item.ToObject(Of Object)(serializer))
			Next
			Return existingValue
		End Function
	End Class
	Friend Class RDPointsConverter
		Inherits JsonConverter
		Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
			With writer
				.WriteStartArray()
				Select Case value.GetType
					Case GetType(RDPointNI), GetType(RDPointNI?),
						 GetType(RDPointN), GetType(RDPointN?),
						 GetType(RDPointI), GetType(RDPointI?),
						 GetType(RDPoint), GetType(RDPoint?)
						.WriteValue(value.X)
						.WriteValue(value.Y)
					Case GetType(RDPointE), GetType(RDPointE?)
						Dim temp = CType(value, RDPointE)
						If temp.X.HasValue Then
							.WriteValue(If(temp.X.Value.IsNumeric, temp.X.Value.NumericValue, temp.X.Value.ExpressionValue))
						Else
							.WriteNull()
						End If
						If temp.Y.HasValue Then
							.WriteValue(If(temp.Y.Value.IsNumeric, temp.Y.Value.NumericValue, temp.Y.Value.ExpressionValue))
						Else
							.WriteNull()
						End If
					Case GetType(RDSizeNI), GetType(RDSizeNI?),
						 GetType(RDSizeN), GetType(RDSizeN?),
						 GetType(RDSizeI), GetType(RDSizeI?),
						 GetType(RDSize), GetType(RDSize?)
						.WriteValue(value.Width)
						.WriteValue(value.Height)
					Case GetType(RDSizeE), GetType(RDSizeE?)
						Dim temp = CType(value, RDSizeE)
						If temp.Width.HasValue Then
							.WriteValue(If(temp.Width.Value.IsNumeric, temp.Width.Value.NumericValue, temp.Width.Value.ExpressionValue))
						Else
							.WriteNull()
						End If
						If temp.Height.HasValue Then
							.WriteValue(If(temp.Height.Value.IsNumeric, temp.Height.Value.NumericValue, temp.Height.Value.ExpressionValue))
						Else
							.WriteNull()
						End If
					Case Else
						Throw New NotImplementedException
				End Select
				.WriteEndArray()
			End With
		End Sub

		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
			Dim ja = JToken.ReadFrom(reader)
			With reader
				Select Case objectType
					Case GetType(RDPointNI), GetType(RDPointNI?)
						Return New RDPointNI(ja(0).ToObject(Of Integer), ja(1).ToObject(Of Integer))
					Case GetType(RDPointN), GetType(RDPointN?)
						Return New RDPointN(ja(0).ToObject(Of Single), ja(1).ToObject(Of Single))
					Case GetType(RDPointI), GetType(RDPointI?)
						Return New RDPointI(ja(0).ToObject(Of Integer?), ja(1).ToObject(Of Integer?))
					Case GetType(RDPoint), GetType(RDPoint?)
						Return New RDPoint(ja(0).ToObject(Of Single?), ja(1).ToObject(Of Single?))
					Case GetType(RDPointE), GetType(RDPointE?)
						Return New RDPointE(
							If(ja(0).ToString.IsNullOrEmpty, Nothing, ja(0).ToObject(Of RDExpression)),
							If(ja(1).ToString.IsNullOrEmpty, Nothing, ja(1).ToObject(Of RDExpression)))
					Case GetType(RDSizeNI), GetType(RDSizeNI?)
						Return New RDSizeNI(ja(0).ToObject(Of Integer), ja(1).ToObject(Of Integer))
					Case GetType(RDSizeN), GetType(RDSizeN?)
						Return New RDSizeN(ja(0).ToObject(Of Single), ja(1).ToObject(Of Single))
					Case GetType(RDSizeI), GetType(RDSizeI?)
						Return New RDSizeI(ja(0).ToObject(Of Integer?), ja(1).ToObject(Of Integer?))
					Case GetType(RDSize), GetType(RDSize?)
						Return New RDSize(ja(0).ToObject(Of Single?), ja(1).ToObject(Of Single?))
					Case GetType(RDSizeE), GetType(RDSizeE?)
						Return New RDSizeE(
							If(ja(0).ToString.IsNullOrEmpty, Nothing, ja(0).ToObject(Of RDExpression)),
							If(ja(1).ToString.IsNullOrEmpty, Nothing, ja(1).ToObject(Of RDExpression)))
					Case Else
				End Select
			End With
			Throw New NotImplementedException()
		End Function
		Public Overrides Function CanConvert(objectType As Type) As Boolean
			Throw New NotImplementedException()
		End Function
	End Class
	Friend Class RDExpressionConverter
		Inherits JsonConverter(Of RDExpression)
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDExpression, serializer As JsonSerializer)
			With writer
				If value.IsNumeric Then
					.WriteRawValue(value.NumericValue)
				ElseIf value.ExpressionValue.IsNullOrEmpty Then
					.WriteNull()
				Else
					.WriteValue($"{{{value.ExpressionValue}}}")
				End If
			End With
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDExpression, hasExistingValue As Boolean, serializer As JsonSerializer) As RDExpression
			Dim js = JToken.ReadFrom(reader).ToObject(Of String)
			Return New RDExpression(js.TrimStart("{"c).TrimEnd("}"c))
		End Function
	End Class
	Friend Class PanelColorConverter
		Inherits JsonConverter(Of RDPaletteColor)
		Private ReadOnly parent As LimitedList(Of SKColor)
		Friend Sub New(list As LimitedList(Of SKColor))
			parent = list
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDPaletteColor, serializer As JsonSerializer)
			If value.EnablePanel Then
				writer.WriteValue($"pal{value.PaletteIndex}")
			Else
				Dim s = value.Value.ToString.Replace("#", "")
				Dim alpha = s.Substring(0, 2)
				Dim rgb = s.Substring(2)
				If value.EnableAlpha Then
					writer.WriteValue(rgb + alpha)
				Else
					writer.WriteValue(rgb)
				End If
			End If
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDPaletteColor, hasExistingValue As Boolean, serializer As JsonSerializer) As RDPaletteColor
			Dim JString = JToken.Load(reader).Value(Of String)
			Dim reg = Regex.Match(JString, "pal(\d+)")
			existingValue.parent = parent
			If reg.Success Then
				existingValue.PaletteIndex = reg.Groups(1).Value
			Else
				Dim s = JString.Replace("#", "")
				Dim alpha As String = ""
				If s.Length > 6 Then
					alpha = s.Substring(6)
				End If
				Dim rgb = s.Substring(0, 6)
				If s.Length > 6 Then
					existingValue.Color = SKColor.Parse(alpha + rgb)
				Else
					existingValue.Color = SKColor.Parse(rgb)
				End If
			End If
			Return existingValue
		End Function
	End Class
	Friend Class ColorConverter
		Inherits JsonConverter(Of SKColor)
		Public Overrides Sub WriteJson(writer As JsonWriter, value As SKColor, serializer As JsonSerializer)
			Dim JString = value.ToString
			Dim Reg = Regex.Match(JString, "([0-9A-Fa-f]{2})?([0-9A-Fa-f]{6})")
			writer.WriteValue(Reg.Groups(1).Value + Reg.Groups(2).Value)
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As SKColor, hasExistingValue As Boolean, serializer As JsonSerializer) As SKColor
			Dim JString = JToken.Load(reader).Value(Of String)
			Dim Reg = Regex.Match(JString, "([0-9A-Fa-f]{6})([0-9A-Fa-f]{2})?")
			Return SKColor.Parse(Reg.Groups(1).Value + Reg.Groups(2).Value)
		End Function
	End Class
	Friend Class TimeConverter
		Inherits JsonConverter(Of TimeSpan)
		Public Enum TimeType
			Hour
			Minute
			Second
			MiliSecond
			Microsecond
		End Enum
		Private _timeType
		Public Sub New()
			_timeType = TimeType.MiliSecond
		End Sub
		Public Sub New(type As TimeType)
			_timeType = type
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As TimeSpan, serializer As JsonSerializer)
			With writer
				Select Case _timeType
					Case TimeType.Hour
						.WriteValue(value.TotalHours)
					Case TimeType.Minute
						.WriteValue(value.TotalMinutes)
					Case TimeType.Second
						.WriteValue(value.TotalSeconds)
					Case TimeType.MiliSecond
						.WriteValue(value.TotalMilliseconds)
					Case TimeType.Microsecond
						.WriteValue(value.TotalMicroseconds)
				End Select
			End With
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As TimeSpan, hasExistingValue As Boolean, serializer As JsonSerializer) As TimeSpan
			Dim value = JToken.ReadFrom(reader).ToObject(Of Single)
			Select Case _timeType
				Case TimeType.Hour
					Return TimeSpan.FromHours(value)
				Case TimeType.Minute
					Return TimeSpan.FromMinutes(value)
				Case TimeType.Second
					Return TimeSpan.FromSeconds(value)
				Case TimeType.MiliSecond
					Return TimeSpan.FromMilliseconds(value)
				Case TimeType.Microsecond
					Return TimeSpan.FromMicroseconds(value)
			End Select
		End Function
	End Class
	Friend Class SecondConverter
		Inherits TimeConverter
		Public Sub New()
			MyBase.New(TimeType.Second)
		End Sub
	End Class
#End Region

End Namespace