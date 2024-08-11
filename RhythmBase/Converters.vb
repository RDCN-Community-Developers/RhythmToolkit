﻿Imports System.Reflection
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Serialization
Imports SkiaSharp
Namespace Converters
#Region "RD"
	Friend Class RDLevelConverter
		Inherits JsonConverter(Of RDLevel)
		Private ReadOnly fileLocation As String
		Private ReadOnly settings As LevelReadOrWriteSettings
		Public Sub New(location As String, settings As LevelReadOrWriteSettings)
			fileLocation = location
			Me.settings = settings
		End Sub
		Public Sub New(settings As LevelReadOrWriteSettings)
			Me.settings = settings
			Me.settings.PreloadAssets = False
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDLevel, serializer As JsonSerializer)

			Dim AllInOneSerializer = value.GetSerializer(settings)
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
				For Each item In If(settings.InactiveEventsHandling = InactiveEventsHandling.Retain, value.Where(Function(i) i.Active), value)
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
			Dim assetsCollection As New HashSet(Of SpriteFile)
			Dim outLevel As New RDLevel With {._path = fileLocation}
			Dim AllInOneSerializer = JsonSerializer.Create(outLevel.GetSerializer(settings))
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
						outLevel.Settings = Jobj.ToObject(Of Components.Settings)(AllInOneSerializer)
					Case "rows"
						Dim Jarr = JArray.Load(reader)
						outLevel._Rows.AddRange(Jarr.ToObject(Of List(Of RowEventCollection))(AllInOneSerializer))
						For Each item In outLevel.Rows
							item.Parent = outLevel
						Next
					Case "decorations"
						Dim Jarr = JArray.Load(reader)
						outLevel._Decorations.AddRange(Jarr.ToObject(Of List(Of DecorationEventCollection))(AllInOneSerializer))
						For Each item In outLevel.Decorations
							item.Parent = outLevel
						Next
					Case "conditionals"
						Dim Jarr = JArray.Load(reader)
						outLevel.Conditionals.AddRange(Jarr.ToObject(Of List(Of BaseConditional))(AllInOneSerializer))
						For Each item In outLevel.Conditionals
							item.ParentCollection = outLevel.Conditionals
						Next
					Case "colorPalette"
						Dim Jarr = JArray.Load(reader)
						For Each item In Jarr.ToObject(Of SKColor())(AllInOneSerializer)
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


			Try
#Disable Warning BC42104

				Dim FloatingTextCollection As New List(Of ([event] As FloatingText, id As Integer))
				Dim AdvanceTextCollection As New List(Of ([event] As AdvanceText, id As Integer))

				'Dim calculator As New RDBeatCalculator(outLevel)


				For Each item In JEvents
					If settings.InactiveEventsHandling AndAlso item("active")?.Value(Of Boolean) = True Then
						Continue For
					End If
					Dim eventType = ConvertToType(item("type"))
					If eventType Is Nothing Then
						Dim TempEvent As BaseEvent
						If item("target") IsNot Nothing Then
							TempEvent = item.ToObject(Of CustomDecorationEvent)(AllInOneSerializer)
						ElseIf item("row") IsNot Nothing Then
							TempEvent = item.ToObject(Of CustomRowEvent)(AllInOneSerializer)
						Else
							TempEvent = item.ToObject(Of CustomEvent)(AllInOneSerializer)
						End If
						If settings.InactiveEventsHandling = InactiveEventsHandling.Store AndAlso TempEvent.Active = False Then
							settings.InactiveEvents.Add(TempEvent)
						Else
							outLevel.Add(TempEvent)
						End If
					Else
						Dim TempEvent As BaseEvent = item.ToObject(eventType, AllInOneSerializer)
						If TempEvent IsNot Nothing Then
							If TempEvent.Type <> Events.EventType.CustomEvent Then
								Select Case TempEvent.Type
									'浮动文字事件记录
									Case Events.EventType.FloatingText
										FloatingTextCollection.Add((CType(TempEvent, FloatingText), item("id")))

									Case Events.EventType.AdvanceText
										AdvanceTextCollection.Add((CType(TempEvent, AdvanceText), item("id")))

								End Select
							End If
							If settings.InactiveEventsHandling = InactiveEventsHandling.Store AndAlso TempEvent.Active = False Then
								settings.InactiveEvents.Add(TempEvent)
							Else
								outLevel.Add(TempEvent)
							End If
						End If
					End If
				Next
				'浮动文字事件关联
				For Each AdvancePair In AdvanceTextCollection
					Dim Parent = FloatingTextCollection.First(Function(i) i.id = AdvancePair.id).event
					Parent.Children.Add(AdvancePair.event)
					AdvancePair.event.Parent = Parent
				Next
				outLevel.Bookmarks.AddRange(JBookmarks.ToObject(Of List(Of Bookmark))(AllInOneSerializer))
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
		Inherits JsonConverter(Of Bookmark)
		Private ReadOnly calculator As BeatCalculator
		Public Sub New(calculator As BeatCalculator)
			Me.calculator = calculator
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As Bookmark, serializer As JsonSerializer)
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
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Bookmark, hasExistingValue As Boolean, serializer As JsonSerializer) As Bookmark
			Dim jobj = JToken.ReadFrom(reader)
			Return New Bookmark With {
				.Beat = calculator.BeatOf(jobj("bar").ToObject(Of UInteger), jobj("beat").ToObject(Of Single)),
				.Color = [Enum].Parse(Of Bookmark.BookmarkColors)(jobj("color"))
			}
		End Function
	End Class
	Friend Class CharacterConverter
		Inherits JsonConverter(Of RDCharacter)
		Private level As RDLevel
		Private ReadOnly assets As HashSet(Of SpriteFile)
		Public Sub New(level As RDLevel, assets As HashSet(Of SpriteFile))
			Me.level = level
			Me.assets = assets
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDCharacter, serializer As JsonSerializer)
			If value.IsCustom Then
				writer.WriteValue($"custom:{value.CustomCharacter.Name}")
			Else
				writer.WriteValue(value.Character.ToString)
			End If
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDCharacter, hasExistingValue As Boolean, serializer As JsonSerializer) As RDCharacter
			Dim value = JToken.ReadFrom(reader).ToObject(Of String)
			If value.StartsWith("custom:") Then
				Dim name = value.Substring(7)
				Return New RDCharacter(level, name)
			Else
				Return New RDCharacter(level, [Enum].Parse(Of Characters)(value))
			End If
		End Function
	End Class
	Friend Class AnchorStyleConverter
		Inherits JsonConverter(Of FloatingText.AnchorStyle)
		Public Overrides Sub WriteJson(writer As JsonWriter, value As FloatingText.AnchorStyle, serializer As JsonSerializer)
			Dim v1 = If(value And &B11, "Middle", [Enum].Parse(Of FloatingText.AnchorStyle)(value And &B11).ToString)
			writer.WriteValue(If(value And &B11, [Enum].Parse(Of FloatingText.AnchorStyle)(value And &B11).ToString, "Middle") +
								  [Enum].Parse(Of FloatingText.AnchorStyle)(value And &B1100).ToString)
		End Sub

		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As FloatingText.AnchorStyle, hasExistingValue As Boolean, serializer As JsonSerializer) As FloatingText.AnchorStyle
			Dim JString As String = JToken.ReadFrom(reader).ToObject(Of String)
			Dim match = Regex.Match(JString, "([A-Z][a-z]+)([A-Z][a-z]+)")
			Dim middle As Boolean = False
			Dim center As Boolean = False
			Dim result As New FloatingText.AnchorStyle
			Select Case match.Groups(1).Value
				Case "Upper"
					result = result Or FloatingText.AnchorStyle.Upper
				Case "Lower"
					result = result Or FloatingText.AnchorStyle.Lower
				Case Else
					middle = True
			End Select
			Select Case match.Groups(2).Value
				Case "Left"
					result = result Or FloatingText.AnchorStyle.Left
				Case "Right"
					result = result Or FloatingText.AnchorStyle.Right
				Case Else
					center = True
			End Select
			If center And middle Then
				result = FloatingText.AnchorStyle.Center
			End If
			Return result
		End Function
	End Class
	Friend Class BaseEventConverter(Of TEvent As BaseEvent)
		Inherits JsonConverter(Of TEvent)
		Protected ReadOnly level As RDLevel
		Protected ReadOnly settings As LevelReadOrWriteSettings
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
		Public Sub New(level As RDLevel, inputSettings As LevelReadOrWriteSettings)
			Me.level = level
			Me.settings = inputSettings
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As TEvent, serializer As JsonSerializer)
			serializer.Formatting = Formatting.None
			With writer
				.WriteRawValue(JsonConvert.SerializeObject(SetSerializedObject(value, serializer)))
			End With
			serializer.Formatting = Formatting.Indented
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As TEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As TEvent
			Return GetDeserializedObject(JToken.ReadFrom(reader), objectType, existingValue, hasExistingValue, serializer)
		End Function
		Public Overridable Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As TEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As TEvent

			Dim SubClassType As Type = ConvertToType(jobj("type").ToObject(Of String))

			If SubClassType Is Nothing Then
				If jobj("target") IsNot Nothing Then
					SubClassType = GetType(CustomDecorationEvent)
				ElseIf jobj("row") IsNot Nothing Then
					SubClassType = GetType(CustomRowEvent)
				Else
					SubClassType = GetType(CustomEvent)
				End If
			End If
			_canread = False
			existingValue = jobj.ToObject(SubClassType, serializer)
			_canread = True
			existingValue._beat = level.Calculator.BeatOf(UInteger.Parse(jobj("bar")), Single.Parse(If(jobj("beat"), 1)))
			Return existingValue
		End Function
		Public Overridable Function SetSerializedObject(value As TEvent, serializer As JsonSerializer) As JObject
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
		Inherits BaseEventConverter(Of CustomEvent)
		Public Sub New(level As RDLevel, inputSettings As LevelReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As CustomEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As CustomEvent
			Dim result = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			result.Data = jobj
			Return result
		End Function
		Public Overrides Function SetSerializedObject(value As CustomEvent, serializer As JsonSerializer) As JObject
			Dim jobj = MyBase.SetSerializedObject(value, serializer)
			Dim data = CType(value.Data.DeepClone, JObject)
			For Each item In data
				jobj(item.Key) = item.Value
			Next
			Return jobj
		End Function
	End Class
	Friend Class CustomRowEventConverter
		Inherits BaseRowActionConverter(Of CustomRowEvent)
		Public Sub New(level As RDLevel, inputSettings As LevelReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As CustomRowEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As CustomRowEvent
			Dim result = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			result.Data = jobj
			Return result
		End Function
		Public Overrides Function SetSerializedObject(value As CustomRowEvent, serializer As JsonSerializer) As JObject
			Dim jobj = MyBase.SetSerializedObject(value, serializer)
			Dim data = value.Data.DeepClone
			For Each item In jobj
				data(item.Key) = item.Value
			Next
			Return data
		End Function
	End Class
	Friend Class CustomDecorationEventConverter
		Inherits BaseDecorationActionConverter(Of CustomDecorationEvent)
		Public Sub New(level As RDLevel, inputSettings As LevelReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As CustomDecorationEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As CustomDecorationEvent
			Dim result = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			result.Data = jobj
			Return result
		End Function
		Public Overrides Function SetSerializedObject(value As CustomDecorationEvent, serializer As JsonSerializer) As JObject
			Dim jobj = MyBase.SetSerializedObject(value, serializer)
			Dim data = value.Data.DeepClone
			For Each item In jobj
				data(item.Key) = item.Value
			Next
			Return data
		End Function
	End Class
	Friend Class BaseRowActionConverter(Of TEvent As BaseRowAction)
		Inherits BaseEventConverter(Of TEvent)
		Public Sub New(level As RDLevel, inputSettings As LevelReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As TEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As TEvent
			Dim obj = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			Try
				Dim rowId = jobj("row").ToObject(Of Short)
				If rowId = -1 Then
					If obj.Type <> EventType.TintRows Then
						Select Case settings.UnreadableEventsHandling
							Case UnreadableEventHandling.Store
								settings.UnreadableEvents.Add(jobj)
								Return Nothing
							Case UnreadableEventHandling.ThrowException
								Throw New ConvertingException($"Cannot find the row ""{jobj("target")}"" at {obj}")
							Case Else

						End Select
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
	Friend Class BaseDecorationActionConverter(Of TEvent As BaseDecorationAction)
		Inherits BaseEventConverter(Of TEvent)
		Public Sub New(level As RDLevel, inputSettings As LevelReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As TEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As TEvent
			Dim obj = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)

			Dim decoId As String = jobj("target")?.ToObject(Of String)
			Dim Parent = level._Decorations.FirstOrDefault(Function(i) i.Id = decoId)
			obj._parent = Parent
			If Parent Is Nothing AndAlso obj.Type <> EventType.Comment Then
				Select Case settings.UnreadableEventsHandling
					Case UnreadableEventHandling.Store
						settings.UnreadableEvents.Add(jobj)
						Return Nothing
					Case UnreadableEventHandling.ThrowException
						Throw New ConvertingException($"Cannot find the decoration ""{jobj("target")}"" at {obj}")
					Case Else

				End Select
			End If
			Return obj
		End Function
	End Class
	'Friend Class AssetConverter
	'	Inherits JsonConverter(Of Sprite)
	'	Private ReadOnly fileLocation As String
	'	Private ReadOnly assets As HashSet(Of Sprite)
	'	Private ReadOnly settings As LevelReadOrWriteSettings
	'	Public Sub New(location As String, assets As HashSet(Of Sprite), settings As LevelReadOrWriteSettings)
	'		fileLocation = location
	'		Me.assets = assets
	'		Me.settings = settings
	'	End Sub
	'	Public Overrides Sub WriteJson(writer As JsonWriter, value As Sprite, serializer As JsonSerializer)
	'		writer.WriteValue(value.FileName)
	'	End Sub
	'	Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Sprite, hasExistingValue As Boolean, serializer As JsonSerializer) As Sprite
	'		Dim Json = JToken.ReadFrom(reader).ToObject(Of String)
	'		Dim assetName = Json
	'		Dim result As Sprite
	'		If assets.Any(Function(i) i.FileName = assetName) Then
	'			result = assets.Single(Function(i) i.FileName = assetName)
	'		ElseIf Json = String.Empty Then
	'			Return Nothing
	'		Else
	'			Dim file = IO.Path.GetDirectoryName(fileLocation) + "\" + Json
	'			result = New Sprite(file)
	'			If Me.settings.PreloadAssets Then
	'				result.Read()
	'			End If
	'			assets.Add(result)
	'		End If
	'		Return result
	'	End Function
	'End Class
	Friend Class AssetConverter(Of TAsset As IAssetFile)
		Inherits JsonConverter(Of Asset(Of TAsset))
		Private ReadOnly level As RDLevel
		Public Sub New(level As RDLevel)
			Me.level = level
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As Asset(Of TAsset), serializer As JsonSerializer)
			writer.WriteValue(value.Name)
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Asset(Of TAsset), hasExistingValue As Boolean, serializer As JsonSerializer) As Asset(Of TAsset)
			Return New Asset(Of TAsset)(level.Manager) With {.Name = JToken.Load(reader).ToObject(Of String)}
		End Function
	End Class
	Friend Class RoomConverter
		Inherits JsonConverter
		Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
			Select Case value.GetType
				Case GetType(Room)
					writer.WriteValue(CType(value, Room).Rooms)
				Case GetType(SingleRoom)
					writer.WriteValue({CType(value, SingleRoom).Value})
				Case Else
					Throw New NotImplementedException
			End Select
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
			Dim J = JArray.Load(reader).ToObject(Of Byte())
			Select Case objectType
				Case GetType(Room)
					existingValue = New Room(existingValue Is Nothing OrElse CType(existingValue, Room).EnableTop)
					For Each item In J
						existingValue(item) = True
					Next
					Return existingValue
				Case GetType(SingleRoom)
					Return New SingleRoom(J.Single)
				Case Else
					Throw New NotImplementedException
			End Select
		End Function
		Public Overrides Function CanConvert(objectType As Type) As Boolean
			Return objectType = GetType(Room) OrElse objectType = GetType(SingleRoom)
		End Function
	End Class
	Friend Class TagActionConverter
		Inherits BaseEventConverter(Of TagAction)
		Public Sub New(level As RDLevel, inputSettings As LevelReadOrWriteSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function SetSerializedObject(value As TagAction, serializer As JsonSerializer) As JObject
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
		Inherits JsonConverter(Of BaseConditional)
		Public Overrides Sub WriteJson(writer As JsonWriter, value As BaseConditional, serializer As JsonSerializer)
			Dim S As New JsonSerializerSettings
			S.Converters.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
			S.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
			writer.WriteRawValue(JsonConvert.SerializeObject(value, S))
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As BaseConditional, hasExistingValue As Boolean, serializer As JsonSerializer) As BaseConditional
			Dim J = JObject.Load(reader)
			Dim ConditionalsType As Type = GetType(BaseConditional)
			Dim SubClassType As Type = Type.GetType($"{GetType(BaseConditional).Namespace}.{NameOf(Conditions)}.{J("type")}")
			Dim Conditional As BaseConditional = J.ToObject(SubClassType)
			Return Conditional
		End Function
	End Class
	Friend Class ConditionConverter
		Inherits JsonConverter(Of Condition)
		Private ReadOnly conditionals As List(Of BaseConditional)
		Public Sub New(Conditionals As List(Of BaseConditional))
			Me.conditionals = Conditionals
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As Condition, serializer As JsonSerializer)
			writer.WriteValue(value.Serialize)
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Condition, hasExistingValue As Boolean, serializer As JsonSerializer) As Condition
			Dim J = JToken.Load(reader).ToObject(Of String)

			Dim Value As New Condition()

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
					Case GetType(PointE), GetType(PointE?)
						Dim temp = CType(value, PointE)
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
					Case GetType(PointE), GetType(PointE?)
						Return New PointE(
							If(ja(0).ToString.IsNullOrEmpty, Nothing, ja(0).ToObject(Of Expression)),
							If(ja(1).ToString.IsNullOrEmpty, Nothing, ja(1).ToObject(Of Expression)))
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
							If(ja(0).ToString.IsNullOrEmpty, Nothing, ja(0).ToObject(Of Expression)),
							If(ja(1).ToString.IsNullOrEmpty, Nothing, ja(1).ToObject(Of Expression)))
					Case Else
				End Select
			End With
			Throw New NotImplementedException()
		End Function
		Public Overrides Function CanConvert(objectType As Type) As Boolean
			Throw New NotImplementedException()
		End Function
	End Class
	Friend Class ExpressionConverter
		Inherits JsonConverter(Of Expression)
		Public Overrides Sub WriteJson(writer As JsonWriter, value As Expression, serializer As JsonSerializer)
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
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Expression, hasExistingValue As Boolean, serializer As JsonSerializer) As Expression
			Dim js = JToken.ReadFrom(reader).ToObject(Of String)
			Return New Expression(js.TrimStart("{"c).TrimEnd("}"c))
		End Function
	End Class
	Friend Class PanelColorConverter
		Inherits JsonConverter(Of PaletteColor)
		Private ReadOnly parent As LimitedList(Of SKColor)
		Friend Sub New(list As LimitedList(Of SKColor))
			parent = list
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As PaletteColor, serializer As JsonSerializer)
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
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As PaletteColor, hasExistingValue As Boolean, serializer As JsonSerializer) As PaletteColor
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
	Friend Class TabsConverter
		Inherits JsonConverter(Of Tabs)
		Private Shared TabNames() As String = {"Song", "Rows", "Actions", "Sprites", "Rooms"}
		Public Overrides Sub WriteJson(writer As JsonWriter, value As Tabs, serializer As JsonSerializer)
			writer.WriteValue(TabNames(value))
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Tabs, hasExistingValue As Boolean, serializer As JsonSerializer) As Tabs
			Dim value = JToken.Load(reader).ToObject(Of String)
			Dim t = TabNames.ToList.IndexOf(value)
			If t >= 0 Then
				Return t
			End If
			Return Tabs.Unknown
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
		Private ReadOnly _timeType
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
	Friend Class RDContractResolver
		Inherits DefaultContractResolver
		Public Shared Shadows ReadOnly Property Instance As New RDContractResolver
		Public Sub New()
			MyBase.New
			NamingStrategy = New CamelCaseNamingStrategy
		End Sub
		Protected Overrides Function CreateProperty(member As MemberInfo, memberSerialization As MemberSerialization) As JsonProperty
			Dim p As JsonProperty = MyBase.CreateProperty(member, memberSerialization)
			Dim f As Predicate(Of Object) = Nothing
			Select Case p.DeclaringType
				Case GetType(RowEventCollection)
					Select Case p.PropertyName
						Case NameOf(RowEventCollection.RowToMimic).ToLowerCamelCase
							f = Function(i) CType(i, RowEventCollection).RowToMimic >= 0
						Case Else

					End Select
				Case GetType(BaseEvent)
					Select Case p.PropertyName
						Case NameOf(BaseEvent.Active).ToLowerCamelCase
							f = Function(i) Not CType(i, BaseEvent).Active
						Case Else

					End Select
				Case Else
			End Select
			If f IsNot Nothing Then
				p.ShouldSerialize = f
			End If
			Return p
		End Function
	End Class
End Namespace