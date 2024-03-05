Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports RhythmAsset
Imports RhythmAsset.Sprites
Imports SkiaSharp
Imports System.IO.Compression
Imports System.IO
Imports RhythmBase.Events
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

		Public Property Value(variableName As String) As Object
			Get
				Dim match = Regex.Match(variableName, "^([ifb])(\d{2})$")
				If match.Success Then
					Select Case match.Groups(1).Value
						Case "i"
							Return i(match.Groups(2).Value)
						Case "f"
							Return f(match.Groups(2).Value)
						Case "b"
							Return b(match.Groups(2).Value)
					End Select
				End If
				Return Me.GetType.GetField(variableName)?.GetValue(Me)
			End Get
			Set(value As Object)
				Dim match = Regex.Match(variableName, "^([ifb])(\d{2})$")
				If match.Success Then
					Select Case match.Groups(1).Value
						Case "i"
							i(match.Groups(2).Value) = value
						Case "f"
							f(match.Groups(2).Value) = value
						Case "b"
							b(match.Groups(2).Value) = value
					End Select
				Else
					Me.GetType.GetField(variableName)?.SetValue(Me, value)
				End If
			End Set
		End Property
	End Class
	Public Interface INumberOrExpression
		Function Serialize() As String
		Function GetValue(variables As Variables) As Single
	End Interface
	Public Structure Number
		Implements INumberOrExpression
		Private ReadOnly value As Single
		Public Sub New(value As String)
			Me.value = value
		End Sub
		Public Shared Function CanPalse(value As String) As Boolean
			Return Single.TryParse(value, 0)
		End Function
		Public Overrides Function ToString() As String
			Return value
		End Function
		Public Function Serialize() As String Implements INumberOrExpression.Serialize
			Return value
		End Function
		Public Function GetValue(variables As Variables) As Single Implements INumberOrExpression.GetValue
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
	End Structure
	Public Structure Expression
		Implements INumberOrExpression
		Private ReadOnly value As String
		Public Sub New(value As String)
			Me.value = Regex.Match(value, "^\{(.*)\}$").Groups(1).Value
		End Sub
		Public Shared Function CanPalse(value As String) As Boolean
			Return Regex.Match(value, "^\{.*\}$").Success
		End Function
		Public Overrides Function ToString() As String
			Return value
		End Function
		Public Function Serialize() As String Implements INumberOrExpression.Serialize
			Return $"""{{{value}}}"""
		End Function
		Public Function GetValue(variables As Variables) As Single Implements INumberOrExpression.GetValue
			Throw New NotImplementedException
		End Function
		Public Shared Widening Operator CType(value As String) As Expression
			Return New Expression(value)
		End Operator
	End Structure
	Public Structure [Function]
		Implements INumberOrExpression
		Private ReadOnly [Function] As Func(Of Single)
		Public Sub New(func As Func(Of Single))
			Me.Function = func
		End Sub
		Public Function Serialize() As String Implements INumberOrExpression.Serialize
			Return [Function]()
		End Function
		Public Function GetValue(variables As Variables) As Single Implements INumberOrExpression.GetValue
			Return [Function]()
		End Function
		Public Overrides Function ToString() As String
			Return $"Value: {[Function]()}"
		End Function
	End Structure
	Public Structure NumberOrExpressionPair
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
		Public Function GetValue(variables As Variables) As (X As Single, Y As Single)
			Return (X.GetValue(variables), Y.GetValue(variables))
		End Function
		Public Shared Widening Operator CType(value As (x As INumberOrExpression, y As INumberOrExpression)) As NumberOrExpressionPair
			Return New NumberOrExpressionPair(value.x, value.y)
		End Operator
		Public Shared Widening Operator CType(value As (x As String, y As String)) As NumberOrExpressionPair
			Return New NumberOrExpressionPair(value.x, value.y)
		End Operator
		Public Overrides Function ToString() As String
			Return $"{{{X},{Y}}}"
		End Function
	End Structure
	Public Class MultipleEnum

	End Class
	Public Structure Pulse
		Public BeatOnly As Single
		Public Hold As Single
		Public Parent As BaseBeat
		Public ReadOnly Property Holdable As Boolean
			Get
				Return Hold > 0
			End Get
		End Property
		Public Sub New(parent As BaseBeat, beatOnly As Single, Optional hold As Single = 0)
			Me.Parent = parent
			Me.BeatOnly = beatOnly
			Me.Hold = hold
		End Sub
		Public Overrides Function ToString() As String
			Return $"{{{BeatOnly}, {Parent}}}"
		End Function
	End Structure
	Public Class PanelColor
		'	Public Property Parent As LimitedList(Of SKColor)
		Private _panel As Integer
		Private _color As SKColor
		Friend parent As LimitedList(Of SKColor)
		Public Property Color As SKColor?
			Get
				Return _color
			End Get
			Set(value As SKColor?)
				Panel = -1
				If EnableAlpha Then
					_color = value
				Else
					_color = value?.WithAlpha(255)
				End If
			End Set
		End Property
		Public Property Panel As Integer
			Get
				Return _panel
			End Get
			Set(value As Integer)
				_color = Nothing
				_panel = value
			End Set
		End Property
		Public ReadOnly Property EnableAlpha As Boolean
		Public ReadOnly Property EnablePanel As Boolean
			Get
				Return Panel >= 0
			End Get
		End Property
		Public ReadOnly Property Value As SKColor
			Get
				Return If(EnablePanel, parent(_panel), _color)
			End Get
		End Property
		Public Sub New(enableAlpha As Boolean)
			Me.EnableAlpha = enableAlpha
		End Sub
		Public Overrides Function ToString() As String
			Return If(_panel < 0, Value, $"{_panel}: {Value}")
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
		Private _data As RoomIndex
		Public ReadOnly EnableTop As Boolean
		Public ReadOnly Multipy As Boolean
		Private _avaliable As Boolean
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
		Public ReadOnly Property Avaliable As Boolean
			Get
				Return Not _data = RoomIndex.RoomNotAvaliable
			End Get
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
					._avaliable = False
				}
			End Get
		End Property
		Public Sub New(enableTop As Boolean, multipy As Boolean)
			Me.EnableTop = enableTop
			Me.Multipy = multipy
		End Sub
		Public Sub New(ParamArray rooms() As Byte)
			If rooms.Count = 0 Then
				_data = RoomIndex.RoomNotAvaliable
				Exit Sub
			End If
			For Each item In rooms
				Room(item) = True
			Next
		End Sub
		Public Function Contains(rooms As Rooms) As Boolean
			If _data = Objects.Rooms.RoomIndex.RoomNotAvaliable Then
				Return False
			End If
			For i = 0 To 4
				If Not Me.Room(i) = rooms.Room(i) Then
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
		Friend Function ShouldSerializeFilename() As Boolean
			Return ShouldSerialize()
		End Function
		Friend Function ShouldSerializeVolume() As Boolean
			Return ShouldSerialize()
		End Function
		Friend Function ShouldSerializePitch() As Boolean
			Return ShouldSerialize()
		End Function
		Friend Function ShouldSerializePan() As Boolean
			Return ShouldSerialize()
		End Function
		Friend Function ShouldSerializeOffset() As Boolean
			Return ShouldSerialize()
		End Function
	End Class
	Public Class Condition
		Public Property ConditionLists As New List(Of (Enabled As Boolean, Conditional As BaseConditional))
		Public Property Duration As Single
		Private Sub New()
		End Sub
		Public Shared Function Load(text As String) As Condition
			Dim out As New Condition
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
		Public Sub New(message As String)
			MyBase.New(message)
		End Sub
		Public Sub New(message As String, ex As Exception)
			MyBase.New(message, ex)
		End Sub
	End Class
	Public Class Decoration
		<JsonIgnore>
		Public Parent As ISprite
		Private _id As String
		<JsonIgnore>
		Public ReadOnly Property Children As New List(Of BaseDecorationAction)
		<JsonProperty("id")>
		Public Property Id As String
			Get
				Return _id
			End Get
			Set(value As String)
				_id = value
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
		Public ReadOnly Property Rooms As New Rooms(False, False)
		<JsonProperty("filename")>
		Public Property File As ISprite ' FileLocator
		Public Property Depth As Integer
		Public Property Visible As Boolean
		Sub New()
		End Sub
		Friend Sub New(room As Rooms, parent As ISprite, Optional depth As Integer = 0, Optional visible As Boolean = True)
			Throw New NotImplementedException
			'Me.Rooms._data = room._data
			_id = Me.GetHashCode
			_Depth = depth
			_Visible = visible
			Me.Parent = parent
		End Sub
		Sub New(room As Rooms, parent As ISprite, id As String, Optional depth As Integer = 0, Optional visible As Boolean = True)
			Me.New(room, parent, depth, visible)
			_id = id
		End Sub
		Public Function CreateChildren(Of T As {BaseDecorationAction, New})(beatOnly As Single) As T
			Dim Temp As New T With {
				.BeatOnly = beatOnly,
				.Parent = Me
			}
			Me.Children.Add(Temp)
			Return Temp
		End Function
		Public Function CreateChildren(Of T As {BaseDecorationAction, New})(item As BaseEvent) As T
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
			Return $"{_id}, {_Row}, {_Rooms}, {File.Name}"
		End Function
		Friend Function Copy() As Decoration
			Return Me.MemberwiseClone
		End Function
	End Class
	Public Class Row
		Public Enum PlayerMode
			P1
			P2
			CPU
		End Enum
		Private _rowType As RowType
		<JsonIgnore>
		Friend ParentCollection As List(Of Row)
		<JsonIgnore>
		Public ReadOnly Children As New List(Of BaseRowAction)
		Public Property Character As String
		Public Property RowType As RowType
			Get
				Return _rowType
			End Get
			Set(value As RowType)
				Children.Clear()
				_rowType = value
			End Set
		End Property
		Public ReadOnly Property Row As SByte
			Get
				Return ParentCollection.IndexOf(Me)
			End Get
		End Property
		Public Property Rooms As New Rooms(False, False)
		Public Property HideAtStart As Boolean
		Public Property Player As PlayerMode
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
		Friend Sub New()
		End Sub
		Private Function ClassicBeats() As IEnumerable(Of BaseBeat)
			Return Children.Where(Function(i)
									  Return (i.Type = EventType.AddClassicBeat Or
													i.Type = EventType.AddFreeTimeBeat Or
													i.Type = EventType.PulseFreeTimeBeat) AndAlso
													CType(i, BaseBeat).Pulsable
								  End Function).Cast(Of BaseBeat)
		End Function
		Private Function OneshotBeats() As IEnumerable(Of BaseBeat)
			Return Children.Where(Function(i)
									  Return i.Type = EventType.AddOneshotBeat AndAlso
													CType(i, BaseBeat).Pulsable
								  End Function).Cast(Of BaseBeat)
		End Function
		Public Function PulseBeats() As IEnumerable(Of Pulse)
			Select Case _rowType
				Case RowType.Classic
					Return ClassicBeats().Select(Function(i) i.PulseTime).SelectMany(Function(i) i)
				Case RowType.Oneshot
					Return OneshotBeats().Select(Function(i) i.PulseTime).SelectMany(Function(i) i)
				Case Else
					Throw New RhythmDoctorExcception("How?")
			End Select
		End Function
		Public Function PulseEvents() As IEnumerable(Of BaseBeat)
			Select Case _rowType
				Case RowType.Classic
					Return ClassicBeats()
				Case RowType.Oneshot
					Return OneshotBeats()
				Case Else
					Throw New RhythmDoctorExcception("How?")
			End Select
		End Function
		Friend Function ShouldSerializeMuteBeats() As Boolean
			Return MuteBeats
		End Function
		Friend Function ShouldSerializeHideAtStart() As Boolean
			Return HideAtStart
		End Function
		Public Function CreateChildren(Of T As {BaseRowAction, New})(beatOnly As Single) As T
			Dim temp = New T With {
					.BeatOnly = beatOnly,
					.Parent = Me
				}
			Me.Children.Add(temp)
			Return temp
		End Function
		Public Function CreateChildren(Of T As {BaseRowAction, New})(item As BaseRowAction) As T
			Dim Temp As T = item.Copy(Of T)
			Temp.Parent = Me
			Me.Children.Add(Temp)
			Return Temp
		End Function
		Public Overrides Function ToString() As String
			Return $"{_rowType}: {Character}"
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
	Public MustInherit Class BaseConditional
		Public Enum ConditionalType
			LastHit
			Custom
			TimesExecuted
			Language
			PlayerMode
		End Enum
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
		Enum HitResult
			Perfect
			SlightlyEarly
			SlightlyLate
			VeryEarly
			VeryLate
			AnyEarlyOrLate
			Missed
		End Enum
		Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.LastHit
		Public Property Row As SByte
		Public Property Result As HitResult
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
		Enum Languages
			English
			Spanish
			Portuguese
			ChineseSimplified
			ChineseTraditional
			Korean
			Polish
			Japanese
			German
		End Enum
		Public Property Language As String
		Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.Language
	End Class

	Public Class PlayerMode
		Inherits BaseConditional
		Public Property TwoPlayerMode As Boolean
		Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.PlayerMode
	End Class
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
		Public Property Settings As New Settings
		Friend ReadOnly Property _Rows As New List(Of Row)
		Friend ReadOnly Property _Decorations As New List(Of Decoration)
		Friend ReadOnly Property Events As New List(Of BaseEvent)
		Public ReadOnly Property Rows As IReadOnlyCollection(Of Row)
			Get
				Return _Rows.AsReadOnly
			End Get
		End Property
		Public ReadOnly Property Decorations As IReadOnlyCollection(Of Decoration)
			Get
				Return _Decorations.AsReadOnly
			End Get
		End Property
		Public ReadOnly Property Conditionals As New List(Of BaseConditional)
		Public ReadOnly Property Bookmarks As New List(Of Bookmark)
		Public ReadOnly Property ColorPalette As New LimitedList(Of SKColor)(21, New SKColor(&HFF, &HFF, &HFF, &HFF))
		<JsonIgnore>
		Public ReadOnly Property Path As IO.FileInfo
			Get
				Return _path
			End Get
		End Property
		<JsonIgnore>
		Friend Property CPBs As New List(Of SetCrotchetsPerBar)
		<JsonIgnore>
		Friend Property BPMs As New List(Of BaseBeatsPerMinute)
		<JsonIgnore>
		Public ReadOnly Property Count As Integer Implements ICollection(Of BaseEvent).Count
			Get
				Return CPBs.Count + BPMs.Count + Events.Count
			End Get
		End Property
		<JsonIgnore>
		Public ReadOnly Property Assets As New HashSet(Of ISprite)
		<JsonIgnore>
		Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of BaseEvent).IsReadOnly
		<JsonIgnore>
		Public ReadOnly Property Variables As New Variables
		Public Function GetTaggedEvents(name As String, direct As Boolean) As IEnumerable(Of IGrouping(Of String, BaseEvent))
			If name Is Nothing Then
				Return Nothing
			End If
			If direct Then
				Return Where(Function(i) i.Tag = name).GroupBy(Function(i) i.Tag)
			Else
				Return Where(Function(i) If(i.Tag, "").Contains(name)).GroupBy(Function(i) i.Tag)
			End If
		End Function
		Public Function CreateDecoration(room As Rooms, parent As ISprite, Optional depth As Integer = 0, Optional visible As Boolean = True) As Decoration
			Assets.Add(parent)
			Dim temp As New Decoration(room, parent, depth, visible)
			_Decorations.Add(temp)
			Return temp
		End Function
		Public Function CopyDecoration(decoration As Decoration) As Decoration
			Dim temp = decoration.Copy
			Me._Decorations.Add(temp)
			Return temp
		End Function
		Public Function RemoveDecoration(decoration As Decoration) As Boolean
			Return _Decorations.Remove(decoration)
		End Function
		Public Function CreateRow(room As Rooms, character As String, Optional visible As Boolean = True) As Row
			Dim temp As New Row() With {.Character = character, .Rooms = room, .ParentCollection = Me.Rows, .HideAtStart = Not visible}
			_Rows.Add(temp)
			Return temp
		End Function
		Public Function RemoveRow(row As Row) As Boolean
			Return _Rows.Remove(row)
		End Function
		Private Function ToRDLevelJson(settings As InputSettings.LevelInputSettings) As String
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
			Dim level As RDLevel
			Try
				level = JsonConvert.DeserializeObject(Of RDLevel)(json, LevelSerializerSettings)
			Catch ex As Exception
				Throw New RhythmDoctorExcception("File cannot be read.", ex)
			End Try
			level._path = fileLocation
			Return level
		End Function
		Private Shared Function LoadZip(RDLevelFile As FileInfo) As FileInfo
			Dim tempDirectoryName As String = RDLevelFile.FullName
			Dim tempDirectory = New IO.DirectoryInfo(IO.Path.Combine(IO.Path.GetTempPath, IO.Path.GetRandomFileName))
			tempDirectory.Create()
			Try
				ZipFile.ExtractToDirectory(RDLevelFile.FullName, tempDirectory.FullName)
				Return tempDirectory.GetFiles.Where(Function(i) i.Name = "main.rdlevel").First
			Catch ex As Exception
				Throw New RhythmDoctorExcception("Cannot extract the file.", ex)
			End Try
		End Function
		Public Shared Function LoadFile(RDLevelFile As FileInfo) As RDLevel
			Return LoadFile(RDLevelFile, New InputSettings.LevelInputSettings With {.SpriteSettings = New SpriteInputSettings})
		End Function
		Public Shared Function LoadFile(RDLevelFile As FileInfo, settings As InputSettings.LevelInputSettings) As RDLevel
			Dim json As String
			Select Case RDLevelFile.Extension
				Case ".rdzip"
					json = File.ReadAllText(LoadZip(RDLevelFile).FullName)
				Case ".zip"
					json = File.ReadAllText(LoadZip(RDLevelFile).FullName)
				Case ".rdlevel"
					json = File.ReadAllText(RDLevelFile.FullName)
				Case Else
					Throw New RhythmDoctorExcception("File not supported")
			End Select
			Dim level = ReadFromString(json, RDLevelFile, settings)
			Return level
		End Function
		Public Sub SaveFile(filepath As String)
			IO.File.WriteAllText(filepath, ToRDLevelJson(New InputSettings.LevelInputSettings With {.SpriteSettings = New SpriteInputSettings}))
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
		Public Function GetPulseEvents() As IEnumerable(Of BaseBeat)
			Return Where(Of BaseBeat).Where(Function(i) i.Pulsable)
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
		Public Function CreateRow(character As String) As Row
			Return New Row With {
					.ParentCollection = Rows,
					.Character = character
				}
		End Function
		Public Sub AddRange(items As IEnumerable(Of BaseEvent))
			BPMs.AddRange(items.Where(Function(i) {
											  EventType.SetBeatsPerMinute,
											  EventType.PlaySong
											}.Contains(i.Type)).Cast(Of BaseBeatsPerMinute))
			CPBs.AddRange(items.Where(Function(i) i.Type = EventType.SetBeatsPerMinute).Cast(Of SetCrotchetsPerBar))
			Events.AddRange(items.Where(Function(i) Not {
												EventType.SetBeatsPerMinute,
												EventType.PlaySong,
												EventType.SetBeatsPerMinute
											}.Contains(i.Type)))
		End Sub
		Public Sub Clear() Implements ICollection(Of BaseEvent).Clear
			Events.Clear()
		End Sub
		Public Function Contains(item As BaseEvent) As Boolean Implements ICollection(Of BaseEvent).Contains
			Return ConcatAll.Contains(item)
		End Function
		Public Function Where(predicate As Func(Of BaseEvent, Boolean)) As IEnumerable(Of BaseEvent)
			Return ConcatAll.Where(predicate)
		End Function
		Public Function Where(Of T As BaseEvent)() As IEnumerable(Of T)
			Return ConcatAll.Where(Function(i) i.GetType = GetType(T) OrElse i.GetType.IsAssignableTo(GetType(T))).Select(Function(i) CType(i, T))
		End Function
		Public Function Where(Of T As BaseEvent)(predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			Return ConcatAll.Where(Function(i) i.GetType = GetType(T) AndAlso predicate(i)).Select(Function(i) CType(i, T))
		End Function
		Public Function First() As BaseEvent
			Return ConcatAll.First
		End Function
		Public Function First(predicate As Func(Of BaseEvent, Boolean)) As BaseEvent
			Return ConcatAll.First(predicate)
		End Function
		Public Function First(Of T As BaseEvent)() As T
			Return Where(Of T).First
		End Function
		Public Function First(Of T As BaseEvent)(predicate As Func(Of BaseEvent, Boolean)) As BaseEvent
			Return Where(Of T).First(predicate)
		End Function
		Public Function FirstOrDefault() As BaseEvent
			Return ConcatAll.FirstOrDefault()
		End Function
		Public Function FirstOrDefault(defaultValue As BaseEvent) As BaseEvent
			Return ConcatAll.FirstOrDefault(defaultValue)
		End Function
		Public Function FirstOrDefault(predicate As Func(Of BaseEvent, Boolean)) As BaseEvent
			Return ConcatAll.FirstOrDefault(predicate)
		End Function
		Public Function FirstOrDefault(predicate As Func(Of BaseEvent, Boolean), defaultValue As BaseEvent) As BaseEvent
			Return ConcatAll.FirstOrDefault(predicate, defaultValue)
		End Function
		Public Function FirstOrDefault(Of T As BaseEvent)() As T
			Return Where(Of T).FirstOrDefault()
		End Function
		Public Function FirstOrDefault(Of T As BaseEvent)(defaultValue As T) As T
			Return Where(Of T).FirstOrDefault(defaultValue)
		End Function
		Public Function FirstOrDefault(Of T As BaseEvent)(predicate As Func(Of T, Boolean)) As T
			Return Where(Of T).FirstOrDefault(predicate)
		End Function
		Public Function FirstOrDefault(Of T As BaseEvent)(predicate As Func(Of T, Boolean), defaultValue As BaseEvent) As T
			Return Where(Of T).FirstOrDefault(predicate, defaultValue)
		End Function
		Public Function Last() As BaseEvent
			Return ConcatAll.Last
		End Function
		Public Function Last(predicate As Func(Of BaseEvent, Boolean)) As BaseEvent
			Return ConcatAll.Last(predicate)
		End Function
		Public Function Last(Of T As BaseEvent)() As T
			Return Where(Of T).Last
		End Function
		Public Function Last(Of T As BaseEvent)(predicate As Func(Of BaseEvent, Boolean)) As BaseEvent
			Return Where(Of T).Last(predicate)
		End Function
		Public Function LastOrDefault() As BaseEvent
			Return ConcatAll.LastOrDefault()
		End Function
		Public Function LastOrDefault(defaultValue As BaseEvent) As BaseEvent
			Return ConcatAll.LastOrDefault(defaultValue)
		End Function
		Public Function LastOrDefault(predicate As Func(Of BaseEvent, Boolean)) As BaseEvent
			Return ConcatAll.LastOrDefault(predicate)
		End Function
		Public Function LastOrDefault(predicate As Func(Of BaseEvent, Boolean), defaultValue As BaseEvent) As BaseEvent
			Return ConcatAll.LastOrDefault(predicate, defaultValue)
		End Function
		Public Function LastOrDefault(Of T As BaseEvent)() As T
			Return Where(Of T).LastOrDefault()
		End Function
		Public Function LastOrDefault(Of T As BaseEvent)(defaultValue As T) As T
			Return Where(Of T).LastOrDefault(defaultValue)
		End Function
		Public Function LastOrDefault(Of T As BaseEvent)(predicate As Func(Of T, Boolean)) As T
			Return Where(Of T).LastOrDefault(predicate)
		End Function
		Public Function LastOrDefault(Of T As BaseEvent)(predicate As Func(Of T, Boolean), defaultValue As BaseEvent) As T
			Return Where(Of T).LastOrDefault(predicate, defaultValue)
		End Function
		Public Function [Select](Of T)(predicate As Func(Of BaseEvent, T)) As IEnumerable(Of T)
			Return Events.Select(predicate)
		End Function
		Private Function ConcatAll() As IEnumerable(Of BaseEvent)
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
		Public Function RemoveAll(predicate As Predicate(Of BaseEvent)) As Integer
			Return CPBs.RemoveAll(predicate) + BPMs.RemoveAll(predicate) + Events.RemoveAll(predicate)
		End Function
		Friend Function GetEnumerator() As IEnumerator(Of BaseEvent) Implements IEnumerable(Of BaseEvent).GetEnumerator
			Return ConcatAll.GetEnumerator
		End Function
		Public Sub RefreshCPBs()
			BeatCalculator.Initialize(CPBs)
		End Sub
		Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return GetEnumerator()
		End Function
	End Class
	Public Enum SpecialArtistType
		None
		AuthorIsArtist
		PublicLicense
	End Enum
	Public Enum Difficulty
		Easy
		Medium
		Tough
		VeryTough
	End Enum
	Public Enum CanBePlayedOn
		OnePlayerOnly
		TwoPlayerOnly
		BothModes
	End Enum
	Public Enum FirstBeatBehavior
		RunNormally
		RunEventsOnPrebar
	End Enum
	Public Enum MultiplayerAppearance
		HorizontalStrips
	End Enum
	Public Class Settings
		Public Property Version As Integer
		Public Property Artist As String = "" 'Done
		Public Property Song As String = "" 'Done
		Public Property SpecialArtistType As SpecialArtistType = SpecialArtistType.None 'Enum
		Public Property ArtistPermission As String = "" 'Done
		Public Property ArtistLinks As String = "" 'Link
		Public Property Author As String = "" 'done
		Public Property Difficulty As Difficulty = Difficulty.Easy 'Enum
		Public Property SeizureWarning As Boolean = False
		Public Property PreviewImage As String = "" 'FilePath
		Public Property SyringeIcon As String = "" 'FilePath
		Public Property PreviewSong As String = "" 'Done
		Public Property PreviewSongStartTime As Single
		Public Property PreviewSongDuration As Single
		Public Property SongNameHue As Single
		Public Property SongLabelGrayscale As Boolean
		Public Property Description As String = "" 'Done
		Public Property Tags As String = "" 'Done
		Public Property Separate2PLevelFilename As String = "" 'FilePath
		Public Property CanBePlayedOn As CanBePlayedOn = CanBePlayedOn.OnePlayerOnly 'Enum
		Public Property FirstBeatBehavior As FirstBeatBehavior = FirstBeatBehavior.RunNormally 'Enum
		Public Property MultiplayerAppearance As MultiplayerAppearance = MultiplayerAppearance.HorizontalStrips 'Enum
		Public Property LevelVolume As Single = 1
		Public Property RankMaxMistakes As New LimitedList(Of Integer)(4, 20)
		Public Property RankDescription As New LimitedList(Of String)(6, "")
		Public Property Mods As List(Of String)
	End Class
End Namespace