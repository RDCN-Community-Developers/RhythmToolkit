Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports SkiaSharp
Imports System.Text.RegularExpressions

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
					Call .Add(New Newtonsoft.Json.Converters.StringEnumConverter)
					.Add(New RoomConverter)
				End With
				Dim DecorationsSerializer As New JsonSerializerSettings With {
					.ContractResolver = New Serialization.CamelCasePropertyNamesContractResolver
				}
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
					.Add(New EventConverter(value.CPBs, value.BPMs))
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
				Dim SetCPB As New List(Of SetCrotchetsPerBar)
				Dim SetBPM As New List(Of BaseBeatsPerMinute)
				Dim J = JObject.Load(reader)
				Dim SettingsSerializer As New JsonSerializer
				With SettingsSerializer.Converters
					.Add(New LimitedListConverter(Of String))
					.Add(New LimitedListConverter(Of Integer))
				End With
				Dim RowsSerializer As New JsonSerializer
				With RowsSerializer.Converters
					Call .Add(New Newtonsoft.Json.Converters.StringEnumConverter)
					.Add(New RoomConverter)
				End With
				Dim DecorationsSerializer As New JsonSerializer
				With DecorationsSerializer.Converters
					.Add(New DecorationConverter(fileLocation, settings.SpriteSettings))
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
					.Add(New TagActionConverter(SetCPB))
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
					TempEvent.BeatOnly = BeatCalculator.BarBeat_BeatOnly(CUInt(item("bar")), 1, SetCPB)
					SetCPB.Add(TempEvent)
				Next


				For Each item In J("events").Where(Function(i) i("type") = NameOf(SetBeatsPerMinute))
					Dim TempEvent As BaseBeatsPerMinute = item.ToObject(GetType(SetBeatsPerMinute), EventsSerializer)
					TempEvent.BeatOnly = BeatCalculator.BarBeat_BeatOnly(CUInt(item("bar")), CDbl(item("beat")), SetCPB)
					SetBPM.Add(TempEvent)
				Next


				For Each item In J("events").Where(Function(i) i("type") = NameOf(PlaySong))
					Dim TempEvent As BaseBeatsPerMinute = item.ToObject(GetType(PlaySong), EventsSerializer)
					TempEvent.BeatOnly = BeatCalculator.BarBeat_BeatOnly(CUInt(item("bar")), CDbl(item("beat")), SetCPB)
					SetBPM.Add(TempEvent)
				Next

				Level.BPMs = SetBPM
				Level.CPBs = SetCPB

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
					TempEvent.BeatOnly = BeatCalculator.BarBeat_BeatOnly(CUInt(item("bar")), CDbl(If(item("beat"), 1)), SetCPB)
					'色盘
					'For Each info In SubClassType.GetProperties.Where(Function(i) i.PropertyType = GetType(PanelColor))
					'	Dim PColor As New PanelColor
					'	Dim name = ToCamelCase(info.Name, False)
					'	If item(name) IsNot Nothing Then
					'		Dim panelName = item(ToCamelCase(info.Name, False)).ToString
					'		Dim match = Regex.Match(item(name).ToString, "pal(?<index>[\d]+)")
					'		If match.Success Then
					'			PColor.Panel = match.Groups("index").Value
					'			info.SetValue(TempEvent, PColor)
					'		Else
					'			Dim S = RgbaToArgb(Convert.ToInt32(item(name).ToString.PadRight(8, "f"c), 16))
					'			PColor.Color = SKColor.Parse(item(name))
					'			PColor.Panel = -1
					'		End If
					'		PColor.Parent = Level.ColorPalette
					'		info.SetValue(TempEvent, PColor)
					'	Else
					'		PColor.Panel = -1
					'		PColor.Color = New SKColor(&HFF, &HFF, &HFF, &HFF)
					'		PColor.Parent = Level.ColorPalette
					'		info.SetValue(TempEvent, PColor)
					'	End If
					'Next
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
					'标签
					'If item("tag") IsNot Nothing Then
					'	Dim tagName = item("tag")
					'	If Level.Tags.ContainsKey(tagName) Then
					'		Level.Tags(tagName).Add(TempEvent)
					'	Else
					'		Level.Tags(tagName) = New List(Of BaseEvent) From {TempEvent}
					'	End If
					'End If
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
		Public Class EventConverter
			Inherits JsonConverter(Of List(Of BaseEvent))
			Private ReadOnly setCPB As IEnumerable(Of SetCrotchetsPerBar)
			Private ReadOnly setBPM As IEnumerable(Of BaseBeatsPerMinute)
			Public Sub New(setCPBCollection As IEnumerable(Of SetCrotchetsPerBar), setBPMCollection As IEnumerable(Of BaseBeatsPerMinute))
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
						existingValue.Color = SKColor.Parse(alpha + rgb) ' UInteger.Parse(rgb, Globalization.NumberStyles.HexNumber) '+ UInteger.Parse(alpha, Globalization.NumberStyles.HexNumber) << 24
					Else
						existingValue.Color = SKColor.Parse(rgb)
					End If
					'	existingValue.Color = SKColor.Parse(JString)
				End If
				Return existingValue
			End Function
		End Class
		'Public Class FileConverter
		'	Inherits JsonConverter(Of FileLocator)
		'	Public Overrides Sub WriteJson(writer As JsonWriter, value As FileLocator, serializer As JsonSerializer)
		'		writer.WriteRawValue(value.Name)
		'	End Sub
		'	Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As FileLocator, hasExistingValue As Boolean, serializer As JsonSerializer) As FileLocator
		'		Return New FileLocator(reader.ReadAsString())
		'	End Function
		'End Class
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
		Public Class DecorationConverter
			Inherits JsonConverter(Of Decoration)
			Private ReadOnly fileLocation As IO.FileInfo
			Private ReadOnly settings As InputSettings.SpriteInputSettings
			Public Sub New(location As IO.FileInfo, settings As InputSettings.SpriteInputSettings)
				fileLocation = location
				Me.settings = settings
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
				End With
				Dim result = Json.ToObject(Of Decoration)(settings)
				Dim file = fileLocation.Directory.FullName + "\" + Json("filename").ToString
				If Me.settings.PlaceHolder Then
					result.Parent = New RhythmSprite.Placeholder(file)
				ElseIf RhythmSprite.Sprite.CanRead(file) Then
					result.Parent = RhythmSprite.Sprite.FromPath(file)
				ElseIf RhythmSprite.Image.CanRead(file) Then
					result.Parent = RhythmSprite.Image.FromPath(file)
				End If
				Return result
			End Function
		End Class
		Public Class RoomConverter
			Inherits JsonConverter(Of Rooms)
			Public Overrides Sub WriteJson(writer As JsonWriter, value As Rooms, serializer As JsonSerializer)
				writer.WriteRawValue($"[{String.Join(",", value.Rooms)}]")
			End Sub
			Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Rooms, hasExistingValue As Boolean, serializer As JsonSerializer) As Rooms
				Dim J = JArray.Load(reader)
				For Each item In J
					existingValue(CType(item, Byte)) = True
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
		Public SpriteSettings As New SpriteInputSettings
	End Class
	Public Class SpriteInputSettings
		''' <summary>
		''' 启用精灵占位符，以换取更快的读取速度。精灵将不可更改，精灵表情将无法读取。禁用则会读取完整精灵图。
		''' </summary>
		Public PlaceHolder As Boolean
	End Class
End Namespace