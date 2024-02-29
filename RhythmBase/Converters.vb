Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RhythmAsset
Imports RhythmAsset.Sprites
Imports SkiaSharp

Namespace Objects
	Module Converters
		Public Class RDLevelConverter
			Inherits JsonConverter(Of RDLevel)
			Private ReadOnly fileLocation As IO.FileInfo
			Private ReadOnly settings As InputSettings.LevelInputSettings
			Public Sub New(location As IO.FileInfo, settings As InputSettings.LevelInputSettings)
				fileLocation = location
				Me.settings = settings
			End Sub
			Public Overrides Sub WriteJson(writer As JsonWriter, value As RDLevel, serializer As JsonSerializer)
				Dim SettingsSerializer As New JsonSerializerSettings With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
				}
				With SettingsSerializer.Converters
					.Add(New LimitedListConverter(Of String))
					.Add(New LimitedListConverter(Of Integer))
				End With
				Dim RowsSerializer As New JsonSerializerSettings With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
				}
				With RowsSerializer.Converters
					.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
					.Add(New RoomConverter)
				End With
				Dim DecorationsSerializer As New JsonSerializerSettings With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
				}
				With DecorationsSerializer.Converters
					.Add(New AssetConverter(fileLocation, Me.settings.SpriteSettings, value.Assets))
					.Add(New RoomConverter)
				End With
				With DecorationsSerializer.Converters
					.Add(New RoomConverter)
				End With
				Dim ConditionalsSerializer As New JsonSerializerSettings With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
				}
				With ConditionalsSerializer.Converters
					.Add(New ConditionalConverter)
				End With
				Dim BookmarksSerializer As New JsonSerializerSettings With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
				}
				With BookmarksSerializer.Converters
				End With
				Dim ColorPaletteSerializer As New JsonSerializerSettings With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
				}
				With ColorPaletteSerializer.Converters
					.Add(New ColorConverter)
					.Add(New LimitedListConverter(Of SKColor))
				End With
				Dim EventsSerializer As New JsonSerializerSettings With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver,
					.NullValueHandling = NullValueHandling.Ignore
				}
				With EventsSerializer.Converters
					.Add(New EventConverter(value.CPBs, value.BPMs, value.Path, settings.SpriteSettings, value.Assets))
				End With

				With writer
					.WriteStartObject()
					.WritePropertyName("settings")
					.WriteRawValue(JsonConvert.SerializeObject(value.Settings, SettingsSerializer))
					.WritePropertyName("rows")
					.WriteRawValue(JsonConvert.SerializeObject(value.Rows, RowsSerializer))
					.WritePropertyName("decorations")
					.WriteRawValue(JsonConvert.SerializeObject(value.Decorations, DecorationsSerializer))
					.WritePropertyName("events")
					.WriteRawValue(JsonConvert.SerializeObject(value.Events, EventsSerializer))
					.WritePropertyName("conditionals")
					.WriteRawValue(JsonConvert.SerializeObject(value.Conditionals, ConditionalsSerializer))
					.WritePropertyName("bookmarks")
					.WriteRawValue(JsonConvert.SerializeObject(value.Bookmarks, BookmarksSerializer))
					.WritePropertyName("colorPalette")
					.WriteRawValue(JsonConvert.SerializeObject(value.ColorPalette, ColorPaletteSerializer))
					.WriteEndObject()
				End With
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As RDLevel, hasExistingValue As Boolean, serializer As JsonSerializer) As RDLevel
				Dim SetCPBCollection As New List(Of SetCrotchetsPerBar)
				Dim SetBPMCollection As New List(Of BaseBeatsPerMinute)
				Dim assetsCollection As New HashSet(Of ISprite)
				Dim J = JObject.Load(reader)
				Dim SettingsSerializer As New JsonSerializer
				With SettingsSerializer.Converters
					.Add(New LimitedListConverter(Of String))
					.Add(New LimitedListConverter(Of Integer))
				End With
				Dim RowsSerializer As New JsonSerializer
				With RowsSerializer.Converters
					.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
					.Add(New RoomConverter)
				End With
				Dim DecorationsSerializer As New JsonSerializer
				With DecorationsSerializer.Converters
					.Add(New AssetConverter(fileLocation, settings.SpriteSettings, assetsCollection))
					.Add(New RoomConverter)
				End With
				Dim ConditionalsSerializer As New JsonSerializer
				With ConditionalsSerializer.Converters
					.Add(New ConditionalConverter)
				End With
				Dim BookmarksSerializer As New JsonSerializer
				With BookmarksSerializer.Converters
				End With
				Dim ColorPaletteSerializer As New JsonSerializer
				With ColorPaletteSerializer.Converters
					.Add(New ColorConverter)
					.Add(New LimitedListConverter(Of SKColor))
				End With
				Dim EventsSerializer As New JsonSerializer With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver,
					.NullValueHandling = NullValueHandling.Ignore
				}
				With EventsSerializer.Converters
					.Add(New INumberOrExpressionConverter)
					.Add(New NumberOrExpressionPairConverter)
					.Add(New PanelColorConverter)
					.Add(New RoomConverter)
					.Add(New AssetConverter(fileLocation, settings.SpriteSettings, assetsCollection))
					.Add(New AnchorStyleConverter)
					.Add(New TagActionConverter(SetCPBCollection))
					.Add(New ConditionConverter)
					Call .Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				End With

				Dim Level As New RDLevel With {.Settings = J("settings").ToObject(Of Settings)(SettingsSerializer)}
				'Level.Data.Add(J("settings").ToObject(Of Settings)(SettingsSerializer))
				If Level.Settings.Version < 55 Then
					Throw New RhythmDoctorExcception($"Might not support. Version is too low ({Level.Settings.Version}).")
				End If
				With Level
					.Rows.AddRange(J("rows").ToObject(Of List(Of Row))(RowsSerializer))
					.Decorations.AddRange(J("decorations").ToObject(Of List(Of Decoration))(DecorationsSerializer))
					.Conditionals.AddRange(J("conditionals").ToObject(Of List(Of BaseConditional))(ConditionalsSerializer))
					.Bookmarks.AddRange(J("bookmarks").ToObject(Of List(Of Bookmark))(BookmarksSerializer))
					For Each item In J("colorPalette").ToObject(Of LimitedList(Of SKColor))(ColorPaletteSerializer)
						.ColorPalette.Add(item)
					Next
					For Each item In assetsCollection
						.Assets.Add(item)
					Next

				End With


				For Each item In Level.Rows
					item.ParentCollection = Level.Rows
				Next

				For Each item In Level.Conditionals
					item.ParentCollection = Level.Conditionals
				Next

				Dim BaseActionType As Type = GetType(BaseEvent)

				Dim FloatingTextCollection As New List(Of ([event] As FloatingText, id As Integer))
				Dim AdvanceTextCollection As New List(Of ([event] As AdvanceText, id As Integer))

				For Each item In J("events").Where(Function(i) i("type") = NameOf(SetCrotchetsPerBar))
					Dim TempEvent As SetCrotchetsPerBar = item.ToObject(GetType(SetCrotchetsPerBar), EventsSerializer)
					TempEvent.BeatOnly = BeatCalculator.BarBeat_BeatOnly(CUInt(item("bar")), 1, SetCPBCollection)
					SetCPBCollection.Add(TempEvent)
				Next


				For Each item In J("events").Where(Function(i) i("type") = NameOf(SetBeatsPerMinute))
					Dim TempEvent As BaseBeatsPerMinute = item.ToObject(GetType(SetBeatsPerMinute), EventsSerializer)
					TempEvent.BeatOnly = BeatCalculator.BarBeat_BeatOnly(CUInt(item("bar")), CDbl(item("beat")), SetCPBCollection)
					SetBPMCollection.Add(TempEvent)
				Next


				For Each item In J("events").Where(Function(i) i("type") = NameOf(PlaySong))
					Dim TempEvent As BaseBeatsPerMinute = item.ToObject(GetType(PlaySong), EventsSerializer)
					TempEvent.BeatOnly = BeatCalculator.BarBeat_BeatOnly(CUInt(item("bar")), CDbl(item("beat")), SetCPBCollection)
					SetBPMCollection.Add(TempEvent)
				Next

				Level.BPMs = SetBPMCollection
				Level.CPBs = SetCPBCollection

				For Each item In J("events")

					If {
						NameOf(SetCrotchetsPerBar),
						NameOf(SetBeatsPerMinute),
						NameOf(PlaySong)
					}.Contains(item("type")) Then
						Continue For
					End If

					Dim SubClassType As Type = Type.GetType($"{BaseActionType.Namespace}.{NameOf(Events)}+{item("type")}")
					Dim TempEvent As BaseEvent = item.ToObject(SubClassType, EventsSerializer)
					TempEvent.BeatOnly = BeatCalculator.BarBeat_BeatOnly(CUInt(item("bar")), CDbl(If(item("beat"), 1)), SetCPBCollection)
					'条件
					If item("if") IsNot Nothing Then
						Dim ConditionIds = Regex.Matches(item("if").ToString, "~?\d+(?=[&d])")
						For Each match As Match In ConditionIds
							Dim vs = Val("~2")
							Dim Parent = Level.Conditionals.Where(Function(i) i.Id = CInt(Regex.Match(match.Value, "\d+").Value)).First
							Parent.Children.Add(TempEvent)
							TempEvent.If.ConditionLists.Add((match.Value(0) <> "~"c, Parent))
						Next
						TempEvent.If.Duration = Regex.Match(item("if").ToString, "(?<=d)[\d\.]+").Value
					End If
					Dim Added As Boolean = False
					'轨道事件关联
					If SubClassType.IsAssignableTo(GetType(BaseRowAction)) Then
						Dim SubTempEvent As BaseRowAction = CType(TempEvent, BaseRowAction)
						If item("row").Value(Of Integer) >= 0 Then
							Dim Parent = Level.Rows(item("row"))
							Parent.Children.Add(TempEvent)
							SubTempEvent.Parent = Parent
						End If
						Level.Add(TempEvent)
						Added = True
					End If
					'精灵事件关联
					If SubClassType.IsAssignableTo(GetType(BaseDecorationAction)) Then
						Dim SubTempEvent As BaseDecorationAction = CType(TempEvent, BaseDecorationAction)
						If item("target") IsNot Nothing Then
							Dim Parent = Level.Decorations.FirstOrDefault(Function(i) i.Id = item("target"))
							If Parent IsNot Nothing Then
								Parent.Children.Add(TempEvent)
								SubTempEvent.Parent = Parent
							End If
						End If
						Level.Add(TempEvent)
						Added = True
					End If
					'浮动文字事件记录
					If SubClassType Is GetType(FloatingText) Then
						FloatingTextCollection.Add((CType(TempEvent, FloatingText), item("id")))
					End If
					If SubClassType Is GetType(AdvanceText) Then
						AdvanceTextCollection.Add((CType(TempEvent, AdvanceText), item("id")))
					End If
					'未处理事件加入
					If Not Added Then
						Level.Add(TempEvent)
						'If Level.Events2(TempEvent.GetType) Is Nothing Then
						'	Level.Events2(TempEvent.GetType) = New List(Of BaseEvent)
						'End If
						'Level.Events2(TempEvent.GetType).Add(TempEvent)
					End If
				Next
				'浮动文字事件关联
				For Each AdvancePair In AdvanceTextCollection
					Dim Parent = FloatingTextCollection.First(Function(i) i.id = AdvancePair.id).event
					Parent.Children.Add(AdvancePair.event)
					AdvancePair.event.Parent = Parent
				Next
				Return Level
			End Function
		End Class
		Public Class AnchorStyleConverter
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
		Public Class EventConverter
			Inherits JsonConverter(Of List(Of BaseEvent))
			Private ReadOnly setCPB As IEnumerable(Of SetCrotchetsPerBar)
			Private ReadOnly setBPM As IEnumerable(Of BaseBeatsPerMinute)
			Private ReadOnly fileLocation As IO.FileInfo
			Private ReadOnly settings As SpriteInputSettings
			Private ReadOnly assets As HashSet(Of ISprite)
			Public Sub New(setCPBCollection As IEnumerable(Of SetCrotchetsPerBar), setBPMCollection As IEnumerable(Of BaseBeatsPerMinute), location As IO.FileInfo, settings As SpriteInputSettings, assets As HashSet(Of ISprite))
				fileLocation = location
				Me.settings = settings
				Me.assets = assets
				setCPB = setCPBCollection
				setBPM = setBPMCollection
			End Sub
			Public Overrides Sub WriteJson(writer As JsonWriter, value As List(Of BaseEvent), serializer As JsonSerializer)
				Dim EventsSerializer As New JsonSerializer With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver,
					.NullValueHandling = NullValueHandling.Ignore
				}
				With EventsSerializer.Converters
					.Add(New NumberOrExpressionPairConverter)
					.Add(New INumberOrExpressionConverter)
					.Add(New PanelColorConverter)
					.Add(New ColorConverter)
					.Add(New TagActionConverter(setCPB))
					.Add(New RoomConverter)
					.Add(New ConditionConverter)
					.Add(New AssetConverter(fileLocation, settings, assets))
					.Add(New AnchorStyleConverter)
					Call .Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				End With
				With writer
					.WriteStartArray()
					For Each item In value.Concat(setCPB).Concat(setBPM)
						Dim JO = JObject.FromObject(item, EventsSerializer)
						Dim b = BeatCalculator.BeatOnly_BarBeat(item.BeatOnly, setCPB)
						JO("bar") = b.bar
						JO("beat") = b.beat
						JO.Remove("beatTick")
						.WriteRawValue(JsonConvert.SerializeObject(JO))
					Next
					.WriteEndArray()
				End With
			End Sub
			Public Overrides ReadOnly Property CanRead As Boolean = False
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As List(Of BaseEvent), hasExistingValue As Boolean, serializer As JsonSerializer) As List(Of BaseEvent)
				Throw New NotImplementedException()
			End Function
		End Class
		Public Class LimitedListConverter(Of T)
			Inherits JsonConverter(Of LimitedList(Of T))
			Public Overrides Sub WriteJson(writer As JsonWriter, value As LimitedList(Of T), serializer As JsonSerializer)
				writer.WriteStartArray()
				Dim S As New JsonSerializerSettings
				For Each item In serializer.Converters
					S.Converters.Add(item)
				Next
				For Each item In value
					writer.WriteRawValue(JsonConvert.SerializeObject(item, S))
				Next
				writer.WriteEndArray()
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As LimitedList(Of T), hasExistingValue As Boolean, serializer As JsonSerializer) As LimitedList(Of T)
				Dim J = JArray.Load(reader)
				If existingValue Is Nothing Then
					existingValue = New LimitedList(Of T)(J.Count)
				End If
				existingValue.Clear()
				For Each item In J
					existingValue.Add(item.ToObject(Of T)(serializer))
				Next
				Return existingValue
			End Function
		End Class
		Public Class INumberOrExpressionConverter
			Inherits JsonConverter(Of INumberOrExpression)
			Public Overrides Sub WriteJson(writer As JsonWriter, value As INumberOrExpression, serializer As JsonSerializer)
				writer.WriteRawValue(value.Serialize)
			End Sub

			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As INumberOrExpression, hasExistingValue As Boolean, serializer As JsonSerializer) As INumberOrExpression
				Dim value = JToken.ReadFrom(reader)
				If value IsNot Nothing Then
					If Number.CanPalse(value) Then
						Return New Number(value)
					End If
					Return New Expression(value)
				End If
				Return Nothing
			End Function
		End Class
		Public Class NumberOrExpressionPairConverter
			Inherits JsonConverter(Of NumberOrExpressionPair)
			Public Overrides Sub WriteJson(writer As JsonWriter, value As NumberOrExpressionPair, serializer As JsonSerializer)
				With writer
					.WriteStartArray()
					If value.X Is Nothing Then
						.WriteNull()
					Else
						.WriteRawValue(value.X.Serialize)
					End If
					If value.Y Is Nothing Then
						.WriteNull()
					Else
						.WriteRawValue(value.Y.Serialize)
					End If
					.WriteEndArray()
				End With
			End Sub

			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As NumberOrExpressionPair, hasExistingValue As Boolean, serializer As JsonSerializer) As NumberOrExpressionPair
				Dim J = JToken.ReadFrom(reader).ToObject(Of String())
				Dim S As New JsonSerializerSettings
				S.Converters.Add(New INumberOrExpressionConverter)
				Return (J(0), J(1))
			End Function
		End Class
		Public Class PanelColorConverter
			Inherits JsonConverter(Of PanelColor)
			Public Overrides Sub WriteJson(writer As JsonWriter, value As PanelColor, serializer As JsonSerializer)
				If value.EnablePanel Then
					writer.WriteValue($"pal{value.Panel}")
				Else
					Dim s = value.Color.Value.ToString.Replace("#", "")
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
		Public Class ColorConverter
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
		Public Class AssetConverter
			Inherits JsonConverter(Of ISprite)
			Private ReadOnly fileLocation As IO.FileInfo
			Private ReadOnly settings As SpriteInputSettings
			Private ReadOnly assets As HashSet(Of ISprite)
			Public Sub New(location As IO.FileInfo, settings As SpriteInputSettings, assets As HashSet(Of ISprite))
				fileLocation = location
				Me.settings = settings
				Me.assets = assets
			End Sub
			Public Overrides Sub WriteJson(writer As JsonWriter, value As ISprite, serializer As JsonSerializer)
				writer.WriteValue(value.Name)
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As ISprite, hasExistingValue As Boolean, serializer As JsonSerializer) As ISprite
				Dim Json = JToken.ReadFrom(reader).ToObject(Of String)
				Dim assetName = Json
				Dim result As ISprite
				If assets.Any(Function(i) i.Name = assetName) Then
					result = assets.Single(Function(i) i.Name = assetName)
				Else
					Dim file = New IO.FileInfo(fileLocation.Directory.FullName + "\" + Json)
					If Me.settings.PlaceHolder Then
						result = New Placeholder(file)
					ElseIf Image.CanRead(file) Then
						result = Image.FromPath(file)
					ElseIf Sprite.CanRead(file) Then
						result = Sprite.FromPath(file)
					Else
						result = New NullAsset
						'Throw New RhythmDoctorExcception($"Cannot read the file: ""{assetName}""")
					End If
					assets.Add(result)
				End If
				Return result
			End Function
		End Class
		Public Class DecorationConverter
			Inherits JsonConverter(Of Decoration)
			Private ReadOnly fileLocation As IO.FileInfo
			Private ReadOnly settings As SpriteInputSettings
			Private ReadOnly assets As HashSet(Of ISprite)
			Public Sub New(location As IO.FileInfo, settings As SpriteInputSettings, assets As HashSet(Of ISprite))
				fileLocation = location
				Me.settings = settings
				Me.assets = assets
			End Sub
			Public Overrides ReadOnly Property CanWrite As Boolean = False
			Public Overrides Sub WriteJson(writer As JsonWriter, value As Decoration, serializer As JsonSerializer)
				Throw New NotImplementedException()
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Decoration, hasExistingValue As Boolean, serializer As JsonSerializer) As Decoration
				Dim Json = JToken.ReadFrom(reader)
				Dim settings As New JsonSerializer
				With settings.Converters
					.Add(New RoomConverter)
					.Add(New AssetConverter(fileLocation, Me.settings, assets))
				End With
				Dim result = Json.ToObject(Of Decoration)(settings)
				Return result
			End Function
		End Class
		Public Class RoomConverter
			Inherits JsonConverter(Of Rooms)
			Public Overrides Sub WriteJson(writer As JsonWriter, value As Rooms, serializer As JsonSerializer)
				writer.WriteRawValue($"[{String.Join(",", value.Rooms)}]")
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Rooms, hasExistingValue As Boolean, serializer As JsonSerializer) As Rooms
				Dim J = JArray.Load(reader).ToObject(Of Byte())
				For Each item In J
					existingValue(item) = True
				Next
				Return existingValue
			End Function
		End Class
		Public Class TagActionConverter
			Inherits JsonConverter(Of TagAction)
			Private ReadOnly setCPB As IEnumerable(Of SetCrotchetsPerBar)
			Public Sub New(setCPBCollection As IEnumerable(Of SetCrotchetsPerBar))
				setCPB = setCPBCollection
			End Sub
			Public Overrides Sub WriteJson(writer As JsonWriter, value As TagAction, serializer As JsonSerializer)
				Dim S As New JsonSerializerSettings
				S.Converters.Add(New ConditionConverter)
				S.Converters.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				Dim BarBeat = BeatCalculator.BeatOnly_BarBeat(value.BeatOnly, setCPB)
				With writer
					.WriteStartObject()
					.WritePropertyName("bar")
					.WriteRawValue(BarBeat.bar)
					.WritePropertyName("beat")
					.WriteRawValue(BarBeat.beat)
					.WritePropertyName("type")
					.WriteValue(value.Type.ToString)
					.WritePropertyName("y")
					.WriteRawValue(value.Y)
					If value.If IsNot Nothing Then
						.WritePropertyName("if")
						.WriteRawValue(JsonConvert.SerializeObject(value.If, S))
					End If
					If value.Tag IsNot Nothing Then
						.WritePropertyName("tag")
						.WriteValue(value.Tag)
					End If
					.WritePropertyName("Tag")
					.WriteValue(value.ActionTag)
					.WritePropertyName("Action")
					If value.Action.HasFlag(TagAction.Actions.All) Then
						.WriteValue([Enum].Parse(Of TagAction.Actions)(&B110 And value.Action).ToString + TagAction.Actions.All.ToString)
					Else
						.WriteValue([Enum].Parse(Of TagAction.Actions)(&B110 And value.Action).ToString)
					End If
					If value.Active = False Then
						.WritePropertyName("active")
						.WriteRawValue(value.Active.ToString.ToLower)
					End If
					.WriteEndObject()
				End With
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As TagAction, hasExistingValue As Boolean, serializer As JsonSerializer) As TagAction
				Dim S As New JsonSerializer
				S.Converters.Add(New ConditionConverter)
				Dim Json = JObject.Load(reader)
				Dim Obj = Json.ToObject(Of TagAction)(S)
				Dim Action = Json("Action").ToString
				If Action.Contains(TagAction.Actions.All.ToString) Then
					Obj.Action = TagAction.Actions.All Or [Enum].Parse(Of TagAction.Actions)(Action.Replace(TagAction.Actions.All.ToString, ""))
				Else
					Obj.Action = [Enum].Parse(Of TagAction.Actions)(Action.Replace(TagAction.Actions.All.ToString, ""))
				End If
				Obj.ActionTag = Json("Tag")
				Obj.Tag = Json("tag")
				Return Obj
			End Function
		End Class
		Public Class ConditionalConverter
			Inherits JsonConverter(Of BaseConditional)
			Public Overrides Sub WriteJson(writer As JsonWriter, value As BaseConditional, serializer As JsonSerializer)
				Dim S As New JsonSerializerSettings
				S.Converters.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				S.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
				writer.WriteRawValue(JsonConvert.SerializeObject(value, S))
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As BaseConditional, hasExistingValue As Boolean, serializer As JsonSerializer) As BaseConditional
				Dim J = JObject.Load(reader)
				Dim BaseActionType As Type = GetType(BaseConditional)
				Dim SubClassType As Type = Type.GetType($"{BaseActionType.Namespace}.{J("type")}")
				Dim S As New JsonSerializer
				S.Converters.Add(New Newtonsoft.Json.Converters.StringEnumConverter)
				S.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
				S.DefaultValueHandling = DefaultValueHandling.Ignore
				Dim Conditional As BaseConditional = J.ToObject(SubClassType, S)
				Return Conditional
			End Function
		End Class
		Public Class ConditionConverter
			Inherits JsonConverter(Of Condition)
			Public Overrides Sub WriteJson(writer As JsonWriter, value As Condition, serializer As JsonSerializer)
				writer.WriteValue(value.ToString)
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Condition, hasExistingValue As Boolean, serializer As JsonSerializer) As Condition
				Dim J = JToken.Load(reader)
				Return Condition.Load(J)
			End Function
		End Class
	End Module
End Namespace
Namespace InputSettings
	Public Class LevelInputSettings
		Public SpriteSettings As New RhythmAsset.SpriteInputSettings
	End Class
End Namespace