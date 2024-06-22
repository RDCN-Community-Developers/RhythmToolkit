Imports System.Reflection
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports SkiaSharp
Namespace Converters
	Friend Class RDLevelConverter
		Inherits JsonConverter(Of RDLevel)
		Private ReadOnly fileLocation As String
		Private ReadOnly inputSettings As LevelInputSettings
		Private ReadOnly outputSettings As LevelOutputSettings
		Public Sub New(location As String, settings As LevelInputSettings)
			fileLocation = location
			Me.inputSettings = settings
		End Sub
		Public Sub New(location As String, settings As LevelOutputSettings)
			fileLocation = location
			Me.outputSettings = settings
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As RDLevel, serializer As JsonSerializer)

			Dim AllInOneSerializer As New JsonSerializerSettings With {
				.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver,
				.Formatting = Formatting.None
				}
			With AllInOneSerializer.Converters
				.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				.Add(New CharacterConverter(fileLocation, value.Assets))
				.Add(New AssetConverter(fileLocation, value.Assets))
				.Add(New BookmarkConverter(New BeatCalculator(value)))
				.Add(New ColorConverter)
				.Add(New PanelColorConverter(value.ColorPalette))
				.Add(New ConditionConverter(value.Conditionals))
				.Add(New CustomEventConverter(value, inputSettings))
				.Add(New TagActionConverter(value, inputSettings))
				.Add(New BaseEventConverter(Of BaseEvent)(value, inputSettings))
			End With

			With writer
				.Formatting = If(outputSettings.Indented, Formatting.Indented, Formatting.None)
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
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDLevel, hasExistingValue As Boolean, serializer As JsonSerializer) As RDLevel
			Dim assetsCollection As New HashSet(Of RDSprite)
			Dim J = JObject.Load(reader)
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
				.Add(New AssetConverter(fileLocation, assetsCollection))
			End With
			Dim ConditionalsSerializer As New JsonSerializer
			With ConditionalsSerializer.Converters
				.Add(New ConditionalConverter)
			End With
			Dim ColorPaletteSerializer As New JsonSerializer
			With ColorPaletteSerializer.Converters
				.Add(New ColorConverter)
			End With

			Dim Mods = J("settings")("mods")
			If Mods?.Type = JTokenType.String Then
				J("settings")("mods") = New JArray(Mods)
			End If

			Dim Level As New RDLevel With {.Settings = J("settings").ToObject(Of LevelElements.Settings)(SettingsSerializer)}
			Try

				With Level
					._Rows.AddRange(J("rows").ToObject(Of List(Of Row))(RowsSerializer))
					._Decorations.AddRange(J("decorations").ToObject(Of List(Of Decoration))(DecorationsSerializer))
					.Conditionals.AddRange(J("conditionals").ToObject(Of List(Of BaseConditional))(ConditionalsSerializer))
					For Each item In J("colorPalette").ToObject(Of SKColor())(ColorPaletteSerializer)
						.ColorPalette.Add(item)
					Next
					For Each item In assetsCollection
						.Assets.Add(item)
					Next
					._path = fileLocation
				End With

				Dim EventsSerializer As New JsonSerializer With {
						.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
					}
				With EventsSerializer.Converters
					.Add(New PanelColorConverter(Level.ColorPalette))
					.Add(New AssetConverter(Level.Path, Level.Assets))
					.Add(New ConditionConverter(Level.Conditionals))
					.Add(New TagActionConverter(Level, inputSettings))
					.Add(New CustomEventConverter(Level, inputSettings))
					.Add(New BaseRowActionConverter(Of BaseRowAction)(Level, inputSettings))
					.Add(New BaseDecorationActionConverter(Of BaseDecorationAction)(Level, inputSettings))
					.Add(New BaseEventConverter(Of BaseEvent)(Level, inputSettings))
					.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				End With

				For Each item In Level.Rows
					item.Parent = Level
				Next
				For Each item In Level.Decorations
					item.Parent = Level
				Next

				For Each item In Level.Conditionals
					item.ParentCollection = Level.Conditionals
				Next

				Dim FloatingTextCollection As New List(Of ([event] As FloatingText, id As Integer))
				Dim AdvanceTextCollection As New List(Of ([event] As AdvanceText, id As Integer))

				Dim calculator As New BeatCalculator(Level)

				Dim bar = 0

				For Each item In J("events")
					Dim TempEvent As BaseEvent = item.ToObject(ConvertToType(item("type")), EventsSerializer)
					If Not TempEvent.Type = EventType.CustomEvent Then
						Select Case [Enum].Parse(Of EventType)(item("type"))
							Case Else
								'浮动文字事件记录
								If TempEvent.Type = EventType.FloatingText Then
									FloatingTextCollection.Add((CType(TempEvent, FloatingText), item("id")))
								End If
								If TempEvent.Type = EventType.AdvanceText Then
									AdvanceTextCollection.Add((CType(TempEvent, AdvanceText), item("id")))
								End If
								'未处理事件加入
						End Select
					End If
					Level.Add(TempEvent)
					'Level.Calculator.Refresh()
				Next
				'浮动文字事件关联
				For Each AdvancePair In AdvanceTextCollection
					Dim Parent = FloatingTextCollection.First(Function(i) i.id = AdvancePair.id).event
					Parent.Children.Add(AdvancePair.event)
					AdvancePair.event.Parent = Parent
				Next
				Dim BookmarksSerializer As New JsonSerializer
				With BookmarksSerializer.Converters
					.Add(New BookmarkConverter(New BeatCalculator(Level)))
				End With
				Level.Bookmarks.AddRange(J("bookmarks").ToObject(Of List(Of Bookmark))(BookmarksSerializer))
				Return Level
			Catch ex As Exception
				If Level.Settings.Version < 55 Then
					Throw New VersionTooLowException(Level.Settings.Version, ex)
				Else
					Throw New ConvertingException(ex)
				End If
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
	Friend Class BaseEventConverter(Of T As BaseEvent)
		Inherits JsonConverter(Of T)
		Protected ReadOnly level As RDLevel
		Protected ReadOnly settings As LevelInputSettings
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
		Public Sub New(level As RDLevel, inputSettings As LevelInputSettings)
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
			Dim BaseActionType As Type = GetType(BaseEvent)

			Dim SubClassType As Type = ConvertToType(jobj("type").ToObject(Of String))
			_canread = False
			existingValue = If(SubClassType IsNot Nothing, jobj.ToObject(SubClassType, serializer), jobj.ToObject(Of CustomEvent)(serializer))
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
		Inherits BaseEventConverter(Of CustomEvent)
		Public Sub New(level As RDLevel, inputSettings As LevelInputSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As CustomEvent, hasExistingValue As Boolean, serializer As JsonSerializer) As CustomEvent
			Dim result = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			result.Data = jobj
			Return result
		End Function
		Public Overrides Function SetSerializedObject(value As CustomEvent, serializer As JsonSerializer) As JObject
			Dim jobj = MyBase.SetSerializedObject(value, serializer)
			jobj.Remove(NameOf(CustomEvent.Data).ToLower)
			jobj.Remove(NameOf(CustomEvent.Type).ToLower)
			Dim data = value.Data.DeepClone
			For Each item In jobj
				data(item.Key) = item.Value
			Next
			Return data
		End Function
	End Class
	Friend Class BaseRowActionConverter(Of T As BaseRowAction)
		Inherits BaseEventConverter(Of BaseRowAction)
		Public Sub New(level As RDLevel, inputSettings As LevelInputSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As BaseRowAction, hasExistingValue As Boolean, serializer As JsonSerializer) As BaseRowAction
			Dim obj = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			Try
				Dim rowId = jobj("row").ToObject(Of Short)
				If rowId = -1 Then
				Else
					Dim Parent = level._Rows(rowId)
					obj._parent = Parent
				End If
			Catch e As Exception
				Throw New ConvertingException($"Cannot find the row {jobj("row")} at {obj}", e)
			End Try
			Return obj
		End Function
	End Class
	Friend Class BaseDecorationActionConverter(Of T As BaseDecorationAction)
		Inherits BaseEventConverter(Of T)
		Public Sub New(level As RDLevel, inputSettings As LevelInputSettings)
			MyBase.New(level, inputSettings)
		End Sub
		Public Overrides Function GetDeserializedObject(jobj As JObject, objectType As Type, existingValue As T, hasExistingValue As Boolean, serializer As JsonSerializer) As T
			Dim obj = MyBase.GetDeserializedObject(jobj, objectType, existingValue, hasExistingValue, serializer)
			Try
				Dim decoId As String = jobj("target")?.ToObject(Of String)
				Dim Parent = level._Decorations.FirstOrDefault(Function(i) i.Id = decoId)
				obj._parent = Parent
				If obj.Parent Is Nothing AndAlso decoId = String.Empty AndAlso obj.Type = EventType.Comment Then
				End If
			Catch e As Exception
				Throw New ConvertingException($"Cannot find the decoration ""{jobj("target")}"" at {obj}", e)
			End Try
			Return obj
		End Function
	End Class
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
		Inherits JsonConverter(Of PanelColor)
		Private ReadOnly parent As LimitedList(Of SKColor)
		Friend Sub New(list As LimitedList(Of SKColor))
			parent = list
		End Sub
		Public Overrides Sub WriteJson(writer As JsonWriter, value As PanelColor, serializer As JsonSerializer)
			If value.EnablePanel Then
				writer.WriteValue($"pal{value.Panel}")
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
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As PanelColor, hasExistingValue As Boolean, serializer As JsonSerializer) As PanelColor
			Dim JString = JToken.Load(reader).Value(Of String)
			Dim reg = Regex.Match(JString, "pal(\d+)")
			existingValue.parent = parent
			If reg.Success Then
				existingValue.Panel = reg.Groups(1).Value
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
			Dim Reg = Regex.Match(JString, "([0-9A-Fa-f]{2})([0-9A-Fa-f]{6})")
			writer.WriteValue(Reg.Groups(1).Value + Reg.Groups(2).Value)
		End Sub
		Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As SKColor, hasExistingValue As Boolean, serializer As JsonSerializer) As SKColor
			Dim JString = JToken.Load(reader).Value(Of String)
			Dim Reg = Regex.Match(JString, "([0-9A-Fa-f]{6})([0-9A-Fa-f]{2})")
			Return SKColor.Parse(Reg.Groups(1).Value + Reg.Groups(2).Value)
		End Function
	End Class
	Friend Class AssetConverter
		Inherits JsonConverter(Of RDSprite)
		Private ReadOnly fileLocation As String
		Private ReadOnly assets As HashSet(Of RDSprite)
		Public Sub New(location As String, assets As HashSet(Of RDSprite))
			fileLocation = location
			Me.assets = assets
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
		Inherits BaseEventConverter(Of TagAction)
		Public Sub New(level As RDLevel, inputSettings As LevelInputSettings)
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
End Namespace