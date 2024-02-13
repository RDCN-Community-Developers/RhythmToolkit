Imports Newtonsoft.Json
Imports RhythmSprite
Imports System.Text.RegularExpressions
Imports SkiaSharp
#Disable Warning CA1507

Namespace Objects
	Public NotInheritable Class Variables
		Public i As New LimitedList(Of Integer)(10, 0)
		Public f As New LimitedList(Of Single)(10, 0)
		Public b As New LimitedList(Of Boolean)(10, False)

		Public bpm As Double
		Public barNumber As Integer
		Public numEarlyHits As Integer
		Public numLateHits As Integer
		Public numPerfectHits As Integer
		Public numMisses As Integer

		Public numMistakes As Double
		Public numMistakesP1 As Double
		Public numMistakesP2 As Double

		Public p1Press As Double
		Public p2Press As Double
		Public p1Release As Double
		Public p2Release As Double
		Public p1IsPressed As Double
		Public p2IsPressed As Double
		Public anyPlayerPress As Double
		Public upPress As Double
		Public downPress As Double
		Public leftPress As Double
		Public rightPress As Double

		Public wobblyLines As Boolean

		Public noHitStrip As Boolean
		Public noBananaBeats As Boolean
		Public noOneshotShadows As Boolean
		Public hideHandsOnStart As Boolean
		Public noHands As Boolean
		Public noRowAnimsOnStart As Boolean
		Public charsOnlyOnStart As Boolean
		Public rowReflectionsJumping As Boolean
		Public cpuIsP2On2P As Boolean
		Public activeDialogues As Boolean
		Public activeDialoguesImmediately As Boolean
		Public alternativeMatrix As Boolean

		Public booleansDefaultToTrue As Boolean
		Public adaptRowsToRoomHeight As Boolean

	End Class
	Public Interface INumberOrExpression
		Function Serialize()
	End Interface
	Public Class Number
		Implements INumberOrExpression
		Private value As Single
		Public Sub New(value As String)
			Me.value = value
		End Sub
		Public Shared Function CanPalse(value As String) As Boolean
			Return Single.TryParse(value, 0)
		End Function
		Public Overrides Function ToString() As String
			Return value
		End Function
		Public Function Serialize() As Object Implements INumberOrExpression.Serialize
			Return value
		End Function
		Public Shared Widening Operator CType(value As Single) As Number
			Return New Number(value)
		End Operator
		'Public Shared Operator +(a As Number, b As Number) As Number
		'	Return a.value + b.value
		'End Operator
		'Public Shared Operator -(a As Number, b As Number) As Number
		'	Return a.value - b.value
		'End Operator
		'Public Shared Operator *(a As Number, b As Number) As Number
		'	Return a.value * b.value
		'End Operator
		'Public Shared Operator /(a As Number, b As Number) As Number
		'	Return a.value / b.value
		'End Operator
		'Public Shared Operator \(a As Number, b As Number) As Number
		'	Return a.value \ b.value
		'End Operator
	End Class
	Public Class Expression
		Implements INumberOrExpression
		Private value As String
		Public Sub New(value As String)
			Me.value = Regex.Match(value, "^\{(.*)\}$").Groups(1).Value
		End Sub
		Public Shared Function CanPalse(value As String) As Boolean
			Return Regex.Match(value, "^\{.*\}$").Success
		End Function
		Public Overrides Function ToString() As String
			Return value
		End Function
		Public Function Serialize() As Object Implements INumberOrExpression.Serialize
			Return $"""{{{value}}}"""
		End Function
		Public Shared Widening Operator CType(value As String) As Expression
			Return New Expression(value)
		End Operator
	End Class
	Public Class NumberOrExpressionPair
		Public X As INumberOrExpression
		Public Y As INumberOrExpression
		Public Sub New(x As INumberOrExpression, y As INumberOrExpression)
			Me.X = x
			Me.Y = y
		End Sub
		Public Sub New(x As String, y As String)
			If x Is Nothing OrElse x.Length = 0 Then
				Me.X = Nothing
			ElseIf Number.CanPalse(x) Then
				Me.X = New Number(x)
			ElseIf Expression.CanPalse(x) Then
				Me.X = New Expression(x)
			Else
				Throw New RhythmDoctorExcception
			End If
			If y Is Nothing OrElse y.Length = 0 Then
				Me.Y = Nothing
			ElseIf Number.CanPalse(y) Then
				Me.Y = New Number(y)
			ElseIf Expression.CanPalse(y) Then
				Me.Y = New Expression(y)
			Else
				Throw New RhythmDoctorExcception
			End If
		End Sub
		Public Shared Widening Operator CType(value As (x As INumberOrExpression, y As INumberOrExpression)) As NumberOrExpressionPair
			Return New NumberOrExpressionPair(value.x, value.y)
		End Operator
		Public Shared Widening Operator CType(value As (x As String, y As String)) As NumberOrExpressionPair
			Return New NumberOrExpressionPair(value.x, value.y)
		End Operator
		Public Overrides Function ToString() As String
			Return $"{{{X},{Y}}}"
		End Function
	End Class
	Public Class FunctionalPointF
		Public X_Expression As String
		Public Y_Expression As String
		Public X As Single?
		Public Y As Single?
		Public Sub New(x As Single?, y As Single?)
			Me.X = x
			Me.Y = y
		End Sub
		Public Sub New(x As String, y As Single?)
			Me.X_Expression = x
			Me.Y = y
		End Sub
		Public Sub New(x As Single?, y As String)
			Me.X = x
			Me.Y_Expression = y
		End Sub
		Public Sub New(x As String, y As String)
			Me.X_Expression = x
			Me.Y_Expression = y
		End Sub
		Public Shared Widening Operator CType(array As (Single?, Single?)) As FunctionalPointF
			Return New FunctionalPointF(array.Item1, array.Item2)
		End Operator
		Public Shared Widening Operator CType(array As (String, Single?)) As FunctionalPointF
			Return New FunctionalPointF(array.Item1, array.Item2)
		End Operator
		Public Shared Widening Operator CType(array As (Single?, String)) As FunctionalPointF
			Return New FunctionalPointF(array.Item1, array.Item2)
		End Operator
		Public Shared Widening Operator CType(array As (String, String)) As FunctionalPointF
			Return New FunctionalPointF(array.Item1, array.Item2)
		End Operator
		Public Shared Widening Operator CType(point As System.Drawing.Point) As FunctionalPointF
			Return New FunctionalPointF(point.X, point.Y)
		End Operator
		Public Shared Widening Operator CType(point As System.Drawing.PointF) As FunctionalPointF
			Return New FunctionalPointF(point.X, point.Y)
		End Operator
		Public Shared Widening Operator CType(point As System.Drawing.Size) As FunctionalPointF
			Return New FunctionalPointF(point.Width, point.Height)
		End Operator
		Public Shared Widening Operator CType(point As System.Drawing.SizeF) As FunctionalPointF
			Return New FunctionalPointF(point.Width, point.Height)
		End Operator
		Public Shared Widening Operator CType(self As FunctionalPointF) As System.Drawing.PointF
			Return New System.Drawing.PointF(self.X, self.Y)
		End Operator
		Public Function PositionReverse() As FunctionalPointF
			Dim copy = Me.MemberwiseClone
			copy.Y = 100 - Y
			Return copy
		End Function
		Public Overrides Function ToString() As String
			Return $"{X}, {Y}"
		End Function
	End Class
	Public Class Functionalsingle
		Public value_Expression As String
		Public value As Single?
		Public Sub New(value As Single?)
			Me.value = value
		End Sub
		Public Sub New(value As String)
			Me.value_Expression = value
		End Sub
		Public Shared Widening Operator CType(value As Single?) As Functionalsingle
			Return New Functionalsingle(value)
		End Operator
		Public Shared Widening Operator CType(value As String) As Functionalsingle
			Return New Functionalsingle(value)
		End Operator
		Public Overrides Function ToString() As String
			Return value
		End Function
	End Class
	Public Class Pulse
		Public beatOnly As Single
		Public hold As Single
		Public ReadOnly Property Holdable As Boolean
			Get
				Return hold > 0
			End Get
		End Property
		Public Sub New(beatOnly As Single, Optional hold As Single = 0)
			Me.beatOnly = beatOnly
			Me.hold = hold
		End Sub
	End Class
	Public Class PanelColor
		Public Property Parent As LimitedList(Of SKColor)
		Public Property Color As SKColor?
		Public Property Panel As Integer = -1
		Public Property PanelEnabled As Boolean = Panel >= 0
		Public Shared Widening Operator CType(color As SKColor) As PanelColor
			Dim out As New PanelColor With {.Color = color}
			Return out
		End Operator
		Public Overrides Function ToString() As String
			Return If(Panel < 0, Color?.ToString, Parent(Panel).ToString)
		End Function
	End Class
	Public Class Rooms
		<Flags>
		Enum RoomIndex
			None = &B0
			Room1 = &B1
			Room2 = &B10
			Room3 = &B100
			Room4 = &B1000
			RoomTop = &B10000
			RoomNotAvaliable = &B1111111
		End Enum
		Public _data As RoomIndex
		Public ReadOnly EnableTop As Boolean
		Public ReadOnly Multipy As Boolean
		Default Public Property Room(Index As Byte) As Boolean
			Get
				If Not Avaliable Then
					Return False
				End If
				Return _data.HasFlag(CType([Enum].Parse(GetType(RoomIndex), 1 << Index), RoomIndex))
			End Get
			Set(value As Boolean)
				If Index = 4 And Not EnableTop Then
					Exit Property
				End If
				If Multipy Then
					_data = If(value, _data Or 1 << Index, _data And 1 << Index)
				Else
					_data = If(value, 1 << Index, _data)
				End If
			End Set
		End Property
		Public Property Avaliable As Boolean
			Get
				Return Not _data = RoomIndex.RoomNotAvaliable
			End Get
			Set(value As Boolean)
				If Not value Then
					_data = RoomIndex.RoomNotAvaliable
				End If
			End Set
		End Property
		Public ReadOnly Property Rooms As List(Of Byte)
			Get
				If Not Avaliable Then
					Return New List(Of Byte)
				End If
				Dim L As New List(Of Byte)
				For i = 0 To 4
					If _data.HasFlag(CType([Enum].Parse(GetType(RoomIndex), 1 << i), RoomIndex)) Then
						L.Add(i)
					End If
				Next
				Return L
			End Get
		End Property
		Public Overrides Function ToString() As String
			Return $"[{String.Join(",", Rooms)}]"
		End Function
		Public Shared ReadOnly Property [Default] As Rooms
			Get
				Return New Rooms(Array.Empty(Of Byte)) With {
					.Avaliable = False
				}
			End Get
		End Property
		Public Sub New(enableTop As Boolean, multipy As Boolean)
			Me.EnableTop = enableTop
			Me.Multipy = multipy
		End Sub
		Public Sub New(ParamArray rooms() As Byte)
			For Each item In rooms
				Room(item) = True
			Next
		End Sub
		Public Function Contains(R As Rooms) As Boolean
			If _data = Objects.Rooms.RoomIndex.RoomNotAvaliable Then
				Return False
			End If
			For i = 0 To 4
				If Not Me.Room(i) = R.Room(i) Then
					Return False
				End If
			Next
			Return True
		End Function
		Public Shared Operator =(R1 As Rooms, R2 As Rooms) As Boolean
			Return R1._data = R2._data
		End Operator
		Public Shared Operator <>(R1 As Rooms, R2 As Rooms) As Boolean
			Return Not R1 = R2
		End Operator
		Public Overrides Function Equals(obj As Object) As Boolean
			Return Me = obj
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return MyBase.GetHashCode()
		End Function
	End Class
	Public NotInheritable Class Audio
		Public Property Filename As String
		Public Property Volume As Integer
		Public Property Pitch As Integer
		Public Property Pan As Integer
		Public Property Offset As Integer
		Friend Sub New()
		End Sub
		Sub New(filename As String, offset As Integer, Optional volume As Integer = 100, Optional pitch As Integer = 100, Optional pan As Integer = 100)
			_Filename = filename
			_Volume = volume
			_Pitch = pitch
			_Pan = pan
			_Offset = offset
		End Sub

	End Class
	Public Class LimitedList(Of T)
		Implements IEnumerable(Of T)
		Implements ICollection(Of T)
		Private ReadOnly list As List(Of (value As T, isDefault As Boolean))
		<JsonIgnore>
		Public Property DefaultValue As T
		Default Public Property Item(index As UInteger) As T
			Get
				If index >= list.Count Then
					Throw New IndexOutOfRangeException
				End If
				Return If(list(index).isDefault, DefaultValue, list(index).value)
			End Get
			Set(value As T)
				If index >= list.Count Then
					Throw New IndexOutOfRangeException
				End If
				list(index) = (value, False)
			End Set
		End Property
		Public ReadOnly Property Count As Integer Implements ICollection(Of T).Count
			Get
				Return list.Count
			End Get
		End Property
		Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of T).IsReadOnly
		Public Sub New(count As UInteger, defaultValue As T)
			list = New List(Of (value As T, isDefault As Boolean))(count)
			For i = 0 To count - 1
				list.Add((GetDefaultValue(), True))
			Next
			Me.DefaultValue = defaultValue
		End Sub
		Public Sub New(count As UInteger)
			Me.New(count, Nothing)
		End Sub
		Public Sub Remove(index As UInteger)
			If index >= list.Count Then
				Throw New IndexOutOfRangeException
			End If
			list(index) = Nothing
		End Sub
		Private Function GetDefaultValue() As T
			If TypeOf DefaultValue Is ValueType Then
				Return Activator.CreateInstance(GetType(T))
			Else
				Return Nothing
			End If
		End Function
		Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
			Return list.Select(Function(i) If(i.Equals(GetDefaultValue()), DefaultValue, i.value)).GetEnumerator
		End Function
		Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return list.GetEnumerator
		End Function
		Public Sub Add(item As T) Implements ICollection(Of T).Add
			Dim index = list.IndexOf(list.FirstOrDefault(Function(i) i.isDefault = True))
			If index >= 0 Then
				list(index) = (item, False)
			End If
		End Sub
		Public Sub Clear() Implements ICollection(Of T).Clear
			For i = 0 To list.Count - 1
				list(i) = Nothing
			Next
		End Sub
		Public Function Contains(item As T) As Boolean Implements ICollection(Of T).Contains
			Return list.Contains((item, True))
		End Function
		Public Sub CopyTo(array() As T, arrayIndex As Integer) Implements ICollection(Of T).CopyTo
			For i = 0 To list.Count - 1
				array(arrayIndex + i) = list(i).value
			Next
		End Sub
		Public Function Remove(item As T) As Boolean Implements ICollection(Of T).Remove
			Dim T = False
			For i = 0 To list.Count - 1
				If list(i).value.Equals(item) Then
					list(i) = (Nothing, True)
					T = True
				End If
			Next
			Return T
		End Function
	End Class
	Public Class Soundsubtype
		Enum GroupSubtypes
			ClapSoundHoldLongEnd
			ClapSoundHoldLongStart
			ClapSoundHoldShortEnd
			ClapSoundHoldShortStart
			FreezeshotSoundCueLow
			FreezeshotSoundCueHigh
			FreezeshotSoundRiser
			FreezeshotSoundCymbal
			BurnshotSoundCueLow
			BurnshotSoundCueHigh
			BurnshotSoundRiser
			BurnshotSoundCymbal
		End Enum
		Private Property Audio As New Audio
		Public Property GroupSubtype As String
		Public Property Used As Boolean
		Public Property Filename As String
			Get
				Return Audio.Filename
			End Get
			Set(value As String)
				Audio.Filename = value
			End Set
		End Property
		Public Property Volume As Integer
			Get
				Return Audio.Volume
			End Get
			Set(value As Integer)
				Audio.Volume = value
			End Set
		End Property
		Public Property Pitch As Integer
			Get
				Return Audio.Pitch
			End Get
			Set(value As Integer)
				Audio.Pitch = value
			End Set
		End Property
		Public Property Pan As Integer
			Get
				Return Audio.Pan
			End Get
			Set(value As Integer)
				Audio.Pan = value
			End Set
		End Property
		Public Property Offset As Integer
			Get
				Return Audio.Offset
			End Get
			Set(value As Integer)
				Audio.Offset = value
			End Set
		End Property
		Private Function ShouldSerialize() As Boolean
			Return Used
		End Function
		Public Function ShouldSerializeFilename() As Boolean
			Return ShouldSerialize()
		End Function
		Public Function ShouldSerializeVolume() As Boolean
			Return ShouldSerialize()
		End Function
		Public Function ShouldSerializePitch() As Boolean
			Return ShouldSerialize()
		End Function
		Public Function ShouldSerializePan() As Boolean
			Return ShouldSerialize()
		End Function
		Public Function ShouldSerializeOffset() As Boolean
			Return ShouldSerialize()
		End Function
	End Class
	Public Class Conditions
		Public Property ConditionLists As New List(Of (Enabled As Boolean, Conditional As BaseConditional))
		Public Property Duration As Single
		Public Shared Function Load(text As String) As Conditions
			Dim out As New Conditions
			Dim Matches = Regex.Matches(text, "(~?\d+)(?=[&d])")
			If Matches.Count > 0 Then
				out.Duration = CDbl(Regex.Match(text, "[\d\.]").Value)
				Return out
			Else
				Throw New RhythmDoctorExcception("Wrong condition.")
			End If
		End Function
		Public Overrides Function ToString() As String
			Return String.Join("&", ConditionLists.Select(Of String)(Function(i) If(i.Enabled, "", "~") + i.Conditional.Id.ToString)) + "d" + Duration.ToString
		End Function
	End Class
	Public Class RhythmDoctorExcception
		Inherits Exception
		Public Sub New()
			MyBase.New
		End Sub
		Public Sub New(Message As String)
			MyBase.New(Message)
		End Sub
	End Class
	Public Class Decoration
		<JsonIgnore>
		Public Parent As ISprite
		Private _id As String
		<JsonIgnore>
		Public ReadOnly Property Children As New List(Of BaseDecorationActions)
		<JsonProperty("id")>
		Public Property Id As String
			Get
				Return _id
			End Get
			Set(value As String)
				_id = value
				For Each Item In _Children
				Next
			End Set
		End Property
		<JsonIgnore>
		Public ReadOnly Property Size As Numerics.Vector2
			Get
				Return If(Parent?.Size, New Numerics.Vector2(32, 31))
			End Get
		End Property
		<JsonIgnore>
		Public ReadOnly Property Expressions As IEnumerable(Of String)
			Get
				Return If(Parent?.Expressions, New List(Of String))
			End Get
		End Property
		Public Property Row As ULong
		Public Property Rooms As New Rooms(False, False)
		Public ReadOnly Property Filename As String ' FileLocator
			Get
				Return Parent?.Name
			End Get
		End Property
		Public Property Depth As Integer
		Public Property Visible As Boolean
		Private Sub New()
		End Sub
		Sub New(room As Rooms, parent As ISprite, Optional depth As Integer = 0, Optional visible As Boolean = True)

			_id = Me.GetHashCode
			_Depth = depth
			_Visible = visible
			Me.Parent = parent
		End Sub
		Sub New(room As Rooms, parent As ISprite, id As String, Optional depth As Integer = 0, Optional visible As Boolean = True)
			Me.New(room, parent, depth, visible)
			_id = id
		End Sub
		Public Function CreateChildren(Of T As {BaseDecorationActions, New})(beatOnly As Single) As T
			Dim Temp As New T With {
					.BeatOnly = beatOnly,
					.Parent = Me
				}
			Me.Children.Add(Temp)
			Return Temp
		End Function
		Public Function CreateChildren(Of T As {BaseDecorationActions, New})(item As BaseEvent) As T
			Dim Temp As T = item.Copy(Of T)
			Temp.Parent = Me
			Me.Children.Add(Temp)
			Return Temp
		End Function
		'Public Function Preview() As Drawing.Bitmap
		'	If Parent IsNot Nothing Then
		'		Return New Drawing.Bitmap(Filename.FullName)
		'	Else
		'		Return New Drawing.Bitmap(1, 1)
		'	End If
		'End Function
		Public Overrides Function ToString() As String
			Return $"{_id}, {_Row}, {_Rooms}, {Filename}"
		End Function
		Public Function Copy() As Decoration
			Return Me.MemberwiseClone
		End Function
	End Class
	Public Class Row
			Public Enum PlayerMode
				P1
				P2
				CPU
			End Enum
			<JsonIgnore>
			Public ParentCollection As List(Of Row)
			<JsonIgnore>
			Public ReadOnly Children As New List(Of BaseRows)
			Public Property Character As String
			Public Property RowType As RowType
			Public ReadOnly Property Row As SByte
				Get
					Return ParentCollection.IndexOf(Me)
				End Get
			End Property
		Public Property Rooms As New Rooms(False, False)
		Public Property HideAtStart As Boolean
			Public Property Player As PlayerMode
			<JsonIgnore>
			Public Property PlayerType As PlayerMode
				Get
					Return _Player
				End Get
				Set(value As PlayerMode)
					_Player = value
				End Set
			End Property
			<JsonIgnore>
			Public Property Sound As New Audio
			Public Property MuteBeats As Boolean
			Public Property PulseSound As String
				Get
					Return Sound.Filename
				End Get
				Set(value As String)
					Sound.Filename = value
				End Set
			End Property
			Public Property PulseSoundVolume As Integer
				Get
					Return Sound.Volume
				End Get
				Set(value As Integer)
					Sound.Volume = value
				End Set
			End Property
			Public Property PulseSoundPitch As Integer
				Get
					Return Sound.Pitch
				End Get
				Set(value As Integer)
					Sound.Pitch = value
				End Set
			End Property
			Public Property PulseSoundPan As Integer
				Get
					Return Sound.Pan
				End Get
				Set(value As Integer)
					Sound.Pan = value
				End Set
			End Property
			Public Property PulseSoundOffset As Integer
				Get
					Return Sound.Offset
				End Get
				Set(value As Integer)
					Sound.Offset = value
				End Set
			End Property
			Public Sub New()
			End Sub
			Private Function ClassicBeats() As IEnumerable(Of BaseBeats)
				Return Children.Where(Function(i)
										  Return (i.Type = EventType.AddClassicBeat Or
													i.Type = EventType.AddFreeTimeBeat Or
													i.Type = EventType.PulseFreeTimeBeat) AndAlso
													CType(i, BaseBeats).Pulsable
									  End Function)
			End Function
			Private Function OneshotBeats() As IEnumerable(Of BaseBeats)
				Return Children.Where(Function(i)
										  Return i.Type = EventType.AddOneshotBeat AndAlso
													CType(i, BaseBeats).Pulsable
									  End Function)
			End Function
			Public Function PulseBeats() As IEnumerable(Of Pulse)
				Select Case _RowType
					Case RowType.Classic
						Return ClassicBeats().Select(Function(i) i.PulseTime)
					Case RowType.Oneshot
						Return OneshotBeats().Select(Function(i) i.PulseTime)
					Case Else
						Throw New RhythmDoctorExcception("How?")
				End Select
			End Function
			Public Function PulseEvents() As IEnumerable(Of BaseBeats)
				Select Case _RowType
					Case RowType.Classic
						Return ClassicBeats()
					Case RowType.Oneshot
						Return OneshotBeats()
					Case Else
						Throw New RhythmDoctorExcception("How?")
				End Select
			End Function
			Public Function ShouldSerializeMuteBeats() As Boolean
				Return MuteBeats
			End Function
			Public Function ShouldSerializeHideAtStart() As Boolean
				Return HideAtStart
			End Function
			Public Function CreateChildren(Of T As {BaseRows, New})(beatOnly As Single) As T
				Dim temp = New T With {
					.BeatOnly = beatOnly,
					.Parent = Me
				}
				Me.Children.Add(temp)
				Return New T
			End Function
			Public Overrides Function ToString() As String
				Return $"{_RowType}: {Character}"
			End Function
		End Class
	Public Class Bookmark
		Enum BookmarkColors
			Blue
			Red
			Yellow
			Green
		End Enum
		Public Property Bar As UInteger
		Public Property Beat As UInteger
		Public Property Color As BookmarkColors
	End Class
	Public Enum ConditionalType
		LastHit
		Custom
		TimesExecuted
		Language
		PlayerMode
	End Enum
	Public MustInherit Class BaseConditional
			<JsonIgnore>
			Public ParentCollection As List(Of BaseConditional)
			Public MustOverride ReadOnly Property Type As ConditionalType
			Public Property Tag As String 'throw new NotImplementedException()
			Public Property Name As String
			Public ReadOnly Property Id As Integer
				Get
					Return ParentCollection.IndexOf(Me) + 1
				End Get
			End Property
			<JsonIgnore>
			Public Property Children As New List(Of BaseEvent)
			Public Overrides Function ToString() As String
				Return Name
			End Function
		End Class
		Public Class LastHit
			Inherits BaseConditional
			Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.LastHit
		End Class

		Public Class Custom
			Inherits BaseConditional
			Public Property Expression As String

			Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.Custom
		End Class

		Public Class TimesExecuted
			Inherits BaseConditional
			Public Property MaxTimes As Integer
			Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.TimesExecuted
		End Class

		Public Class Language
			Inherits BaseConditional
			Public Property Language As String
			Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.Language
		End Class

	Public Class PlayerMode
		Inherits BaseConditional
		Public Property TwoPlayerMode As Boolean

		Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.PlayerMode
	End Class
	Public Module Level
		<Flags>
		Public Enum WriteOption
			none = &B0
			writeSettings = &B1
			writeRows = &B10
			writeDecorations = &B100
			writeEvents = &B1000
			writeConditionals = &B10000
			writeBookmarks = &B100000
			writeColorPalette = &B1000000
			all = &B1111111
		End Enum
		Public Class RDLevel
			Implements ICollection(Of BaseEvent)
			Dim _path As IO.FileInfo
			Public Property Settings As Settings
			Public Property Rows As New List(Of Row)
			Public Property Decorations As New List(Of Decoration)
			Public Property Events As New List(Of BaseEvent)
			Public Property Conditionals As New List(Of BaseConditional)
			Public Property Bookmarks As New List(Of Bookmark)
			Public Property ColorPalette As New LimitedList(Of SKColor)(21, New SKColor(&HFF, &HFF, &HFF, &HFF))
			<JsonIgnore>
			Public ReadOnly Property Path As IO.FileInfo
				Get
					Return _path
				End Get
			End Property
			<JsonIgnore>
			Public Property CPBs As New List(Of SetCrotchetsPerBar)
			<JsonIgnore>
			Public Property BPMs As New List(Of BaseBeatsPerMinute)
			Public ReadOnly Property Count As Integer Implements ICollection(Of BaseEvent).Count
				Get
					Return CPBs.Count + BPMs.Count + Events.Count
				End Get
			End Property
			Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of BaseEvent).IsReadOnly
			Public Function GetTaggedEvents(name As String, direct As Boolean) As IEnumerable(Of IGrouping(Of String, BaseEvent))
				If direct Then
					Return Where(Function(i) i.Tag = name).GroupBy(Function(i) i.Tag)
				Else
					Return Where(Function(i) If(i.Tag, "").Contains(name)).GroupBy(Function(i) i.Tag)
				End If
			End Function
			Public Function ToRDLevelJson(settings As InputSettings.LevelInputSettings) As String
				Dim LevelSerializerSettings = New JsonSerializerSettings() With {
					.Converters = {
						New RDLevelConverter(_path, settings)
					}
				}
				Me.Events.RemoveAll(Function(i) i Is Nothing)
				Return JsonConvert.SerializeObject(Me, LevelSerializerSettings)
			End Function
			Public Shared Function ReadFromString(json As String, fileLocation As IO.FileInfo, settings As InputSettings.LevelInputSettings) As RDLevel
				Dim LevelSerializerSettings = New JsonSerializerSettings() With {
					.Converters = {
						New RDLevelConverter(fileLocation, settings)
					}
				}
				json = Regex.Replace(json, ",(?=[ \n\r\t]*?[\]\)\}])", "")
				Dim level As RDLevel = JsonConvert.DeserializeObject(Of RDLevel)(json, LevelSerializerSettings)
				Return level
			End Function
			Public Shared Function LoadFile(filepath As String)
				Return LoadFile(filepath, New InputSettings.LevelInputSettings With {.SpriteSettings = New InputSettings.SpriteInputSettings})
			End Function
			Public Shared Function LoadFile(file As String, settings As InputSettings.LevelInputSettings) As RDLevel
				Dim path = New IO.FileInfo(file)
				Dim json = IO.File.ReadAllText(file)
				Dim level = ReadFromString(json, path, settings)
				level._path = path
				Return level
			End Function
			Public Sub SaveFile(filepath As String)
				IO.File.WriteAllText(filepath, ToRDLevelJson(New InputSettings.LevelInputSettings With {.SpriteSettings = New InputSettings.SpriteInputSettings}))
			End Sub
			Public Sub SaveFile(filepath As String, settings As InputSettings.LevelInputSettings)
				IO.File.WriteAllText(filepath, ToRDLevelJson(settings))
			End Sub
			Public Function GetPulseBeat() As IEnumerable(Of Pulse)
				Dim L As New List(Of Pulse)
				For Each item In Rows
					L.AddRange(item.PulseBeats)
				Next
				Return L
			End Function
			Public Function GetPulseEvents() As IEnumerable(Of BaseBeats)
				Return Where(Of BaseBeats).Where(Function(i) i.Pulsable)
			End Function
			Public Sub Add(item As BaseEvent) Implements ICollection(Of BaseEvent).Add
				Select Case item.Type
					Case EventType.PlaySong
						BPMs.Add(item)
					Case EventType.SetCrotchetsPerBar
						CPBs.Add(item)
					Case EventType.SetBeatsPerMinute
						BPMs.Add(item)
					Case Else
						Events.Add(item)
				End Select
			End Sub
			Public Sub AddRange(items As IEnumerable(Of BaseEvent))
				BPMs.AddRange(items.Where(Function(i) i.Type = EventType.SetBeatsPerMinute))
				BPMs.AddRange(items.Where(Function(i) i.Type = EventType.PlaySong))
				CPBs.AddRange(items.Where(Function(i) i.Type = EventType.SetBeatsPerMinute))
				Events.AddRange(items.Where(Function(i) {
												EventType.SetBeatsPerMinute,
												EventType.PlaySong,
												EventType.SetBeatsPerMinute
											}.Contains(i.Type)))
			End Sub
			Public Sub Clear() Implements ICollection(Of BaseEvent).Clear
				Events.Clear()
			End Sub
			Public Function Contains(item As BaseEvent) As Boolean Implements ICollection(Of BaseEvent).Contains
				Return CPBs.Contains(item) OrElse BPMs.Contains(item) OrElse Events.Contains(item)
			End Function
			Public Function Where(predicate As Func(Of BaseEvent, Boolean)) As IEnumerable(Of BaseEvent)
				Return ConcatAll.Where(predicate)
			End Function
			Public Function Where(Of T As BaseEvent)() As IEnumerable(Of T)
				Return ConcatAll.Where(Function(i) i.GetType = GetType(T))
			End Function
			Public Function Where(Of T As BaseEvent)(predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
				Return ConcatAll.Where(Function(i) i.GetType = GetType(T) AndAlso predicate(i))
			End Function
			Public Function FirstOrDefault() As BaseEvent
				Return Events.FirstOrDefault()
			End Function
			Public Function FirstOrDefault(defaultValue As BaseEvent) As BaseEvent
				Return Events.FirstOrDefault(defaultValue)
			End Function
			Public Function FirstOrDefault(predicate As Func(Of BaseEvent, Boolean)) As BaseEvent
				Return Events.FirstOrDefault(predicate)
			End Function
			Public Function FirstOrDefault(predicate As Func(Of BaseEvent, Boolean), defaultValue As BaseEvent) As BaseEvent
				Return Events.FirstOrDefault(predicate, defaultValue)
			End Function
			Public Function FirstOrDefault(Of T As BaseEvent)() As T
				Return Where(Of T).FirstOrDefault()
			End Function
			Public Function FirstOrDefault(Of T As BaseEvent)(defaultValue As BaseEvent) As BaseEvent
				Return Where(Of T).FirstOrDefault(defaultValue)
			End Function
			Public Function FirstOrDefault(Of T As BaseEvent)(predicate As Func(Of BaseEvent, Boolean)) As BaseEvent
				Return Where(Of T).FirstOrDefault(predicate)
			End Function
			Public Function FirstOrDefault(Of T As BaseEvent)(predicate As Func(Of BaseEvent, Boolean), defaultValue As BaseEvent) As BaseEvent
				Return Where(Of T).FirstOrDefault(predicate, defaultValue)
			End Function
			Public Function ConcatAll() As IEnumerable(Of BaseEvent)
				Dim L As New List(Of BaseEvent)
				Return L.Concat(CPBs).Concat(BPMs).Concat(Events)
			End Function
			Public Sub CopyTo(array() As BaseEvent, arrayIndex As Integer) Implements ICollection(Of BaseEvent).CopyTo
				Dim index = 0
				For Each item In CPBs
					array(arrayIndex + index) = item
					index += 1
				Next
				For Each item In BPMs
					array(arrayIndex + index) = item
					index += 1
				Next
				For Each item In Events
					array(arrayIndex + index) = item
					index += 1
				Next
			End Sub
			Public Function Remove(item As BaseEvent) As Boolean Implements ICollection(Of BaseEvent).Remove
				Return CPBs.Remove(item) OrElse BPMs.Remove(item) OrElse Events.Remove(item)
			End Function
			Public Function GetEnumerator() As IEnumerator(Of BaseEvent) Implements IEnumerable(Of BaseEvent).GetEnumerator
				Return ConcatAll.GetEnumerator
			End Function
			Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
				Return GetEnumerator()
			End Function
		End Class
	End Module
	Public Class Settings
			Public Property Version As Integer
			Public Property Artist As String 'Done
			Public Property Song As String 'Done
			Public Property SpecialArtistType As String 'Enum
			Public Property ArtistPermission As String '?
			Public Property ArtistLinks As String '?
			Public Property Author As String 'Done
			Public Property Difficulty As String 'Enum
			Public Property SeizureWarning As Boolean
			Public Property PreviewImage As String 'FilePath
			Public Property SyringeIcon As String 'FilePath
			Public Property PreviewSong As String 'Done
			Public Property PreviewSongStartTime As Single
			Public Property PreviewSongDuration As Single
			Public Property SongNameHue As Single
			Public Property SongLabelGrayscale As Boolean
			Public Property Description As String 'Done
			Public Property Tags As String 'Done
			Public Property Separate2PLevelFilename As String 'FilePath
			Public Property CanBePlayedOn As String 'Enum
			Public Property FirstBeatBehavior As String 'Enum
			Public Property MultiplayerAppearance As String 'Enum
			Public Property LevelVolume As Integer
			Public Property RankMaxMistakes As New LimitedList(Of Integer)(4, 20)
			Public Property RankDescription As New LimitedList(Of String)(6, "")
		End Class
End Namespace