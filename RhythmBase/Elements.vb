Imports System.IO
Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports SkiaSharp
Imports System.ComponentModel
#Disable Warning CA1507
#Disable Warning IDE1006
#Disable Warning CA1812
#Disable Warning CA1822
#Disable Warning IDE0060
Namespace Components
	Public Enum Characters
		Adog
		Athlete
		AthletePhysio
		Barista
		Beat
#If DEBUG Then
		BlankCPU
#End If
		Bodybuilder
		Boy
		BoyRaya
		BoyTangzhuang
		Buro
#If DEBUG Then
		Canary
#End If
		Clef
		Cockatiel
		ColeGuitar
		ColeSynth
		Controller
#If DEBUG Then
		Custom
#End If
		DancingCouple
		Edega
		Farmer
		FarmerAlternate
		Girl
		GirlCNY
		HoodieBoy
		HoodieBoyAlternate
		HoodieBoyBlue
		Ian
		IanBubble
		Janitor
		Kanye
		Lucia
		LuckyBag
		LuckyBaseball
		LuckyIce
#If DEBUG Then
		LuckyJersey
#End If
		Marija
		Miner
		MrsStevendog
		MrsStevenson
		MrStevendog
		MrStevenson
		NicoleCigs
		NicoleCoffee
		NicoleMints
		None
		Oriole
#If DEBUG Then
		Otto
#End If
		Owl
		Paige
		Parrot
		Player
		Politician
		Purritician
		Quaver
		Rin
		Rodney
		Samurai
		SamuraiBaseball
		SamuraiBlue
		SamuraiBoss
		SamuraiBossAlt
		SamuraiGirl
		SamuraiGreen
		SamuraiPirate
		SamuraiYellow
		SmokinBarista
		Tentacle
		Treble
		Weeknd
#If DEBUG Then
		Wren
#End If
	End Enum
	'Public Enum wavetype
	'	BoomAndRush
	'	Spring
	'	Spike
	'	SpikeHuge
	'	Ball
	'	[Single]
	'End Enum
	'Public Enum ShockWaveType
	'	size
	'	distortion
	'	duration
	'End Enum
	'Public Enum Particle
	'	HitExplosion
	'	leveleventexplosion
	'End Enum
	Public Enum Filters
		NearestNeighbor
		BiliNear
	End Enum
	<Flags>
	Public Enum RoomIndex
		None = &B0
		Room1 = &B1
		Room2 = &B10
		Room3 = &B100
		Room4 = &B1000
		RoomTop = &B10000
		RoomNotAvaliable = &B1111111
	End Enum
	Public NotInheritable Class Variables
		Public ReadOnly i As New LimitedList(Of Integer)(10, 0)
		Public ReadOnly f As New LimitedList(Of Single)(10, 0)
		Public ReadOnly b As New LimitedList(Of Boolean)(10, False)
		Public barNumber As Integer
		Public buttonPressCount As Integer
		Public missesToCrackHeart As Integer
		Public numEarlyHits As Integer
		Public numLateHits As Integer
		Public numMisses As Integer
		Public numPerfectHits As Integer
		Public bpm As Single
		Public deltaTime As Single
		Public levelSpeed As Single
		Public numMistakes As Single
		Public numMistakesP1 As Single
		Public numMistakesP2 As Single
		Public shockwaveDistortionMultiplier As Single
		Public shockwaveDurationMultiplier As Single
		Public shockwaveSizeMultiplier As Single
		Public statusSignWidth As Single
		Public activeDialogues As Boolean
		Public activeDialoguesImmediately As Boolean
		Public alternativeMatrix As Boolean
		Public anyPlayerPress As Boolean
		Public autoplay As Boolean
		Public booleansDefaultToTrue As Boolean
		Public charsOnlyOnStart As Boolean
		Public cpuIsP2On2P As Boolean
		Public disableRowChangeWarningFlashes As Boolean
		Public downPress As Boolean
		Public hideHandsOnStart As Boolean
		Public invisibleChars As Boolean
		Public invisibleHeart As Boolean
		Public leftPress As Boolean
		Public noBananaBeats As Boolean
		Public noHands As Boolean
		Public noHitFlashBorder As Boolean
		Public noHitStrips As Boolean
		Public noOneshotShadows As Boolean
		Public noRowAnimsOnStart As Boolean
		Public noSmartJudgment As Boolean
		Public p1IsPressed As Boolean
		Public p1Press As Boolean
		Public p1Release As Boolean
		Public p2IsPressed As Boolean
		Public p2Press As Boolean
		Public p2Release As Boolean
		Public rightPress As Boolean
		Public rotateShake As Boolean
		Public rowReflectionsJumping As Boolean
		Public skippableRankScreen As Boolean
		Public skipRankText As Boolean
		Public smoothShake As Boolean
		Public upPress As Boolean
		Public useFlashFontForFloatingText As Boolean
		Public wobblyLines As Boolean
		Public Function Rand(int As Integer) As Integer
			Return Random.Shared.Next(1, int)
		End Function
		Public Function atLeastRank([char] As Char) As Boolean
			Throw New NotImplementedException
		End Function
		Public Function atLeastNPerfects(hitsToCheck As Integer, numberOfPerfects As Integer) As Boolean
			Return False
		End Function
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
	Public Structure Hit
		Public ReadOnly Beat As RDBeat
		Public ReadOnly Hold As Single
		Public ReadOnly Parent As BaseBeat
		'Public ReadOnly Property BarBeat As (Bar As UInteger, Beat As Single)
		'	Get
		'		Dim Calculator As New BeatCalculator(Parent.ParentLevel)
		'		Return Calculator.BeatOnly_BarBeat(BeatOnly)
		'	End Get
		'End Property
		'Public ReadOnly Property Time As TimeSpan
		'	Get
		'		Dim Calculator As New BeatCalculator(Parent.ParentLevel)
		'		Return Calculator.BeatOnly_Time(BeatOnly)
		'	End Get
		'End Property
		Public ReadOnly Property Holdable As Boolean
			Get
				Return Hold > 0
			End Get
		End Property
		Public Sub New(parent As BaseBeat, beat As RDBeat, Optional hold As Single = 0)
			Me.Parent = parent
			Me.Beat = beat
			Me.Hold = hold
		End Sub
		Public Overrides Function ToString() As String
			Return $"{{{Beat}, {Parent}}}"
		End Function
	End Structure
	Public Structure RDBeat
		Implements IComparable(Of RDBeat)
		Implements IEquatable(Of RDBeat)
		Friend ReadOnly _calculator As BeatCalculator
		Public _isBeatLoaded As Boolean
		Public _isBarBeatLoaded As Boolean
		Private _isTimeSpanLoaded As Boolean
		Private _isBPMLoaded As Boolean
		Private _isCPBLoaded As Boolean
		Private _beat As Single
		Private _BarBeat As (Bar As UInteger, Beat As Single)
		Private _TimeSpan As TimeSpan
		Private _BPM As Single
		Private _CPB As UInteger
		Friend ReadOnly Property baseLevel As RDLevel
			Get
				Return _calculator?.Collection
			End Get
		End Property
		Public ReadOnly Property IsEmpty As Boolean
			Get
				Return _calculator Is Nothing OrElse Not (_isBeatLoaded OrElse _isBarBeatLoaded OrElse _isTimeSpanLoaded)
			End Get
		End Property
		Public ReadOnly Property BeatOnly As Single
			Get
				IfNullThrowException()
				If Not _isBeatLoaded Then
					If _isBarBeatLoaded Then
						_beat = _calculator.BarBeat_BeatOnly(_BarBeat.Bar, _BarBeat.Beat)
					ElseIf _isTimeSpanLoaded Then
						_beat = _calculator.Time_BeatOnly(_TimeSpan)
					End If
					_isBeatLoaded = True
				End If
				Return _beat
			End Get
		End Property
		Public ReadOnly Property BarBeat As (bar As UInteger, beat As Single)
			Get
				IfNullThrowException()
				If Not _isBarBeatLoaded Then
					If _isBeatLoaded Then
						_BarBeat = _calculator.BeatOnly_BarBeat(_beat)
					ElseIf _isTimeSpanLoaded Then
						_beat = _calculator.Time_BeatOnly(_TimeSpan)
						_isBeatLoaded = True
						_BarBeat = _calculator.BeatOnly_BarBeat(_beat)
					End If
					_isBarBeatLoaded = True
				End If
				Return _BarBeat
			End Get
		End Property
		Public ReadOnly Property TimeSpan As TimeSpan
			Get
				IfNullThrowException()
				If Not _isTimeSpanLoaded Then
					If _isBeatLoaded Then
						_TimeSpan = _calculator.BeatOnly_Time(_beat)
					ElseIf _isBarBeatLoaded Then
						_beat = _calculator.BarBeat_BeatOnly(_BarBeat.Bar, _BarBeat.Beat)
						_isBeatLoaded = True
						_TimeSpan = _calculator.BeatOnly_Time(_beat)
					End If
					_isTimeSpanLoaded = True
				End If
				Return _TimeSpan
			End Get
		End Property
		Public ReadOnly Property BPM As Single
			Get
				If Not _isBPMLoaded Then
					_BPM = _calculator.BeatsPerMinuteOf(Me)
					_isBPMLoaded = True
				End If
				Return _BPM
			End Get
		End Property
		Public ReadOnly Property CPB As Single
			Get
				If Not _isCPBLoaded Then
					_CPB = _calculator.CrotchetsPerBarOf(Me)
					_isCPBLoaded = True
				End If
				Return _CPB
			End Get
		End Property
		Public Sub New(calculator As BeatCalculator, beatOnly As Single)
			_calculator = calculator
			_beat = beatOnly
			_isBeatLoaded = True
		End Sub
		Public Sub New(calculator As BeatCalculator, bar As UInteger, beat As Single)
			_calculator = calculator
			_BarBeat = (bar, beat)
			_beat = _calculator.BarBeat_BeatOnly(bar, beat)
			_isBarBeatLoaded = True
			_isBeatLoaded = True
		End Sub
		Public Sub New(calculator As BeatCalculator, timeSpan As TimeSpan)
			_calculator = calculator
			_TimeSpan = timeSpan
			_beat = _calculator.Time_BeatOnly(timeSpan)
			_isTimeSpanLoaded = True
			_isBeatLoaded = True
		End Sub
		Public Shared Function [Default](calculator As BeatCalculator) As RDBeat
			Return New RDBeat(calculator, 1)
		End Function
		Public Shared Function FromSameLevel(a As RDBeat, b As RDBeat, Optional [throw] As Boolean = False) As Boolean
			If a.baseLevel.Equals(b.baseLevel) Then
				Return True
			Else
				If [throw] Then
					Throw New RhythmBaseException("Beats must come from the same RDLevel.")
				End If
				Return False
			End If
		End Function
		Private Sub IfNullThrowException()
			If IsEmpty Then
				Throw New RhythmBaseException("The beat cannot do anything because it cannot be calculated.")
			End If
		End Sub
		Public Sub ResetCache()
			Dim m = BeatOnly
			_isBarBeatLoaded = False
			_isTimeSpanLoaded = False
		End Sub
		Public Sub ResetBPM()
			If Not _isBeatLoaded Then
				_beat = _calculator.Time_BeatOnly(_TimeSpan)
			End If
			_isBeatLoaded = True
			_isTimeSpanLoaded = False
			_isBPMLoaded = False
		End Sub
		Public Sub ResetCPB()
			If Not _isBeatLoaded Then
				_beat = _calculator.BarBeat_BeatOnly(_BarBeat.Bar, _BarBeat.Beat)
			End If
			_isBeatLoaded = True
			_isBarBeatLoaded = False
			_isCPBLoaded = False
		End Sub
		Public Shared Operator +(a As RDBeat, b As RDBeat) As RDBeat
			If FromSameLevel(a, b, True) Then
				Return New RDBeat(a._calculator, a.BeatOnly + b.BeatOnly)
			End If
		End Operator
		Public Shared Operator +(a As RDBeat, b As Single) As RDBeat
			Return New RDBeat(a._calculator, a.BeatOnly + b)
		End Operator
		Public Shared Operator -(a As RDBeat, b As Single) As RDBeat
			Return New RDBeat(a._calculator, a.BeatOnly - b)
		End Operator
		Public Shared Operator -(a As RDBeat, b As RDBeat) As RDBeat
			If FromSameLevel(a, b, True) Then
				Return New RDBeat(a._calculator, a.BeatOnly - b.BeatOnly)
			End If
		End Operator
		Public Shared Operator >(a As RDBeat, b As RDBeat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso a.BeatOnly > b.BeatOnly
		End Operator
		Public Shared Operator <(a As RDBeat, b As RDBeat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso a.BeatOnly < b.BeatOnly
		End Operator
		Public Shared Operator >=(a As RDBeat, b As RDBeat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso a.BeatOnly >= b.BeatOnly
		End Operator
		Public Shared Operator <=(a As RDBeat, b As RDBeat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso a.BeatOnly <= b.BeatOnly
		End Operator
		Public Shared Operator =(a As RDBeat, b As RDBeat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso
				(a._isBeatLoaded AndAlso b._isBeatLoaded AndAlso a._beat = b._beat) OrElse
				(a._isBarBeatLoaded AndAlso b._isBarBeatLoaded AndAlso a._BarBeat.Bar = b._BarBeat.Bar AndAlso a._BarBeat.Beat = b._BarBeat.Beat) OrElse
				(a._isTimeSpanLoaded AndAlso b._isTimeSpanLoaded AndAlso a._TimeSpan = b._TimeSpan) OrElse
				(a.BeatOnly = b.BeatOnly)
		End Operator
		Public Shared Operator <>(a As RDBeat, b As RDBeat) As Boolean
			Return Not a = b
		End Operator
		Public Overrides Function ToString() As String
			Return $"[{BarBeat.bar},{BarBeat.beat}]"
		End Function
		Public Overrides Function Equals(<CodeAnalysis.NotNull> obj As Object) As Boolean
			Return obj.GetType = GetType(RDBeat) AndAlso Equals(CType(obj, RDBeat))
		End Function
		Public Overloads Function Equals(other As RDBeat) As Boolean Implements IEquatable(Of RDBeat).Equals
			Return Me = other
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(Me.BeatOnly, Me.baseLevel)
		End Function
		Public Function CompareTo(other As RDBeat) As Integer Implements IComparable(Of RDBeat).CompareTo
			Dim result = BeatOnly - other.BeatOnly
			If result > 0 Then
				Return 1
			ElseIf result < 0 Then
				Return -1
			End If
			Return 0
		End Function
	End Structure
	Public Structure RDRange
		Public ReadOnly Start? As RDBeat
		Public ReadOnly [End]? As RDBeat
		Public ReadOnly Property BeatInterval As Single
			Get
				If Start.HasValue AndAlso [End].HasValue Then
					Return [End].Value.BeatOnly - Start.Value.BeatOnly
				End If
				Return Single.PositiveInfinity
			End Get
		End Property
		Public ReadOnly Property TimeInterval As TimeSpan
			Get
				If Start.HasValue AndAlso [End].HasValue Then
					If Start.Value.BeatOnly = [End].Value.BeatOnly Then
						Return TimeSpan.Zero
					End If
					Return [End].Value.TimeSpan - Start.Value.TimeSpan
				End If
				Return TimeSpan.MaxValue
			End Get
		End Property
		Public Sub New(start As RDBeat?, [end] As RDBeat?)
			If start.HasValue AndAlso [end].HasValue AndAlso (Not start.Value._calculator.Equals([end].Value._calculator)) Then
				Throw New RhythmBaseException("RDIndexes must come from the same RDLevel.")
			End If
			If start.HasValue AndAlso [end].HasValue AndAlso start.Value.BeatOnly > [end].Value.BeatOnly Then
				Me.Start = [end]
				Me.End = start
			Else
				Me.Start = start
				Me.End = [end]
			End If
		End Sub
	End Structure
	Public Structure ClassicBeatStatus
		Public Enum StatusType
			Unset = -1
			None
			Synco
			Beat_Open
			Beat_Flash
			Beat_Double_Flash
			Beat_Triple_Flash
			Beat_Close
			X_Open
			X_Flash
			X_Close
			X_Synco_Open
			X_Synco_Flash
			X_Synco_Close
			Up_Open
			Up_Close
			Down_Open
			Down_Close
			Swing_Left
			Swing_Right
			Swing_Bounce
			Held_Start
			Held_End
		End Enum
		Public Status As StatusType
		Public BeatCount As UShort
	End Structure
	Public Class PanelColor
		Private _panel As Integer
		Private _color As SKColor?
		Friend parent As LimitedList(Of SKColor)
		Public Property Color As SKColor?
			Get
				Return If(EnablePanel, parent(_panel), Nothing)
			End Get
			Set(value As SKColor?)
				_panel = -1
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
				If value >= 0 Then
					_color = Nothing
					_panel = value
				End If
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
			Return If(EnablePanel, $"{_panel}: {Value}", Value.ToString)
		End Function
	End Class
	<JsonConverter(GetType(RoomConverter))>
	Public Structure RDSingleRoom

		Private _data As Byte
		Public ReadOnly EnableTop As Boolean
		Public ReadOnly Property Avaliable As Boolean
			Get
				Return _data < 5
			End Get
		End Property
		Public Property Room As RoomIndex
			Get
				Return _data
			End Get
			Set(value As RoomIndex)
				_data = value
			End Set
		End Property
		Public Property Value As Byte
			Get
				Return _data
			End Get
			Set(value As Byte)

			End Set
		End Property
		Public Overrides Function ToString() As String
			Return $"[{_data}]"
		End Function
		Public Shared ReadOnly Property [Default] As RDSingleRoom
			Get
				Return New RDSingleRoom() With {._data = 0}
			End Get
		End Property
		Public Sub New(room As Byte)
			_data = room
		End Sub
		Public Shared Operator =(R1 As RDSingleRoom, R2 As RDSingleRoom) As Boolean
			Return R1._data = R2._data
		End Operator
		Public Shared Operator <>(R1 As RDSingleRoom, R2 As RDSingleRoom) As Boolean
			Return Not R1 = R2
		End Operator
		Public Overrides Function Equals(obj As Object) As Boolean
			Return Me = obj
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(_data)
		End Function
	End Structure

	<JsonConverter(GetType(RoomConverter))>
	Public Structure RDRoom
		Private _data As RoomIndex
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
		Public Shared Function [Default]() As RDRoom
			Return New RDRoom(False, False, Array.Empty(Of Byte)) With {
._data = RoomIndex.RoomNotAvaliable
}
		End Function
		Public Sub New(enableTop As Boolean, multipy As Boolean)
			Me.EnableTop = enableTop
			Me.Multipy = multipy
		End Sub
		Public Sub New(enableTop As Boolean, multipy As Boolean, ParamArray rooms() As Byte)
			Select Case rooms.Length
				Case 0
					_data = RoomIndex.RoomNotAvaliable
					Exit Sub
				Case 1
					Room(rooms.Single) = True
					Me.EnableTop = rooms.Contains(5)
					Me.Multipy = False
				Case Else
					For Each item In rooms
						Room(item) = True
					Next
					Me.EnableTop = rooms.Contains(5)
					Me.Multipy = True
			End Select
			If enableTop <> Me.EnableTop OrElse multipy <> Me.Multipy Then
				Throw New RhythmBaseException("Parameters are not match.")
			End If
		End Sub
		Public Function Contains(rooms As RDRoom) As Boolean
			If _data = RoomIndex.RoomNotAvaliable Then
				Return False
			End If
			For i = 0 To 4
				If Not Me.Room(i) = rooms.Room(i) Then
					Return False
				End If
			Next
			Return True
		End Function
		Public Shared Operator =(R1 As RDRoom, R2 As RDRoom) As Boolean
			Return R1._data = R2._data
		End Operator
		Public Shared Operator <>(R1 As RDRoom, R2 As RDRoom) As Boolean
			Return Not R1 = R2
		End Operator
		Public Shared Widening Operator CType(room As RDSingleRoom) As RDRoom
			Return New RDRoom(room.EnableTop, False, room.Room)
		End Operator
		Public Shared Narrowing Operator CType(room As RDRoom) As RDSingleRoom
			If Not room.Multipy Then
				Return New RDSingleRoom(room.Rooms.Single)
			End If
		End Operator
		Public Overrides Function Equals(obj As Object) As Boolean
			Return Me = obj
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(_data)
		End Function
	End Structure
	Public NotInheritable Class Audio
		Public Property Filename As String
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)>
		<DefaultValue(100)>
		Public Property Volume As Integer = 100
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)>
		<DefaultValue(100)>
		Public Property Pitch As Integer = 100
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)>
		Public Property Pan As Integer = 0
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)>
		Public Property Offset As Integer = 0
		Public Overrides Function ToString() As String
			Return Filename
		End Function
	End Class
	<JsonConverter(GetType(Converters.LimitedListConverter))>
	Public Class LimitedList
		Implements ICollection
		Protected ReadOnly list As List(Of (value As Object, isDefault As Boolean))
		<JsonIgnore>
		Public Property DefaultValue As Object
		Public ReadOnly Property Count As Integer Implements ICollection.Count
			Get
				Return list.Count
			End Get
		End Property
		Public ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
			Get
				Return False
			End Get
		End Property
		Public ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
			Get
				Return Nothing
			End Get
		End Property
		Public Sub New(count As UInteger, defaultValue As Object)
			list = New List(Of (value As Object, isDefault As Boolean))(count)
			For i = 0 To count - 1
				list.Add((GetDefaultValue(), True))
			Next
			Me.DefaultValue = defaultValue
		End Sub
		Public Sub New(count As UInteger)
			Me.New(count, Nothing)
		End Sub
		Public Sub Add(item As Object)
			Dim index = list.IndexOf(list.FirstOrDefault(Function(i) i.isDefault = True))
			If index >= 0 Then
				list(index) = (item, False)
			End If
		End Sub
		Public Sub Clear()
			For i = 0 To list.Count - 1
				list(i) = Nothing
			Next
		End Sub
		Public Sub Remove(index As UInteger)
			If index >= list.Count Then
				Throw New IndexOutOfRangeException
			End If
			list(index) = Nothing
		End Sub
		Protected Overridable Function GetDefaultValue() As Object
			If TypeOf DefaultValue Is ValueType Then
				Return Activator.CreateInstance(GetType(Object))
			Else
				Return Nothing
			End If
		End Function
		Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return list.Select(Of Object)(Function(i) If(i.Equals(GetDefaultValue()), DefaultValue, i.value)).GetEnumerator
		End Function
		Public Sub CopyTo(array As Array, arrayIndex As Integer) Implements ICollection.CopyTo
			For i = 0 To list.Count - 1
				array(arrayIndex + i) = list(i).value
			Next
		End Sub
		Public Overrides Function ToString() As String
			Return $"Count = {Count}"
		End Function
	End Class
	Public Class LimitedList(Of T)
		Inherits LimitedList
		Implements ICollection(Of T)
		'Private ReadOnly list As List(Of (value As T, isDefault As Boolean))
		<JsonIgnore>
		Public Overloads Property DefaultValue As T
			Get
				Return MyBase.DefaultValue
			End Get
			Set(value As T)
				MyBase.DefaultValue = value
			End Set
		End Property
		Default Public Property Item(index As Integer) As T
			Get
				If index >= list.Count Then
					Throw New IndexOutOfRangeException
				End If
				If list(index).isDefault Then
					Dim ValueCloned = Clone(DefaultValue)
					list(index) = (ValueCloned, False)
					Return ValueCloned
				End If
				Return list(index).value
			End Get
			Set(value As T)
				If index >= list.Count Then
					Throw New IndexOutOfRangeException
				End If
				list(index) = (value, False)
			End Set
		End Property
		Public Overloads ReadOnly Property Count As Integer Implements ICollection(Of T).Count
			Get
				Return MyBase.Count
			End Get
		End Property
		Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of T).IsReadOnly
		Public Sub New(count As UInteger, defaultValue As T)
			MyBase.New(count, defaultValue)
		End Sub
		Public Sub New(count As UInteger)
			Me.New(count, Nothing)
		End Sub
		Public Sub RemoveAt(index As UInteger)
			If index >= MyBase.Count Then
				Throw New IndexOutOfRangeException
			End If
			Item(index) = Nothing
		End Sub
		Public Overloads Iterator Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
			For Each i In list
				Yield If(i.Equals(GetDefaultValue()), DefaultValue, i.value)
			Next
		End Function
		Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return MyBase.GetEnumerator
		End Function
		Public Overloads Sub Add(item As T) Implements ICollection(Of T).Add
			MyBase.Add(item)
		End Sub
		Public Overloads Sub Clear() Implements ICollection(Of T).Clear
			MyBase.Clear()
		End Sub
		Public Function Contains(item As T) As Boolean Implements ICollection(Of T).Contains
			Return list.Contains((item, True))
		End Function
		Public Overloads Sub CopyTo(array() As T, arrayIndex As Integer) Implements ICollection(Of T).CopyTo
			MyBase.CopyTo(array, arrayIndex)
		End Sub
		Public Overloads Function Remove(item As T) As Boolean Implements ICollection(Of T).Remove
			Return MyBase.SyncRoot(item)
		End Function
		Public Overrides Function ToString() As String
			Return $"Count = {Count}"
		End Function
	End Class
	Friend Class TypedList(Of T As BaseEvent)
		Friend ReadOnly list As New List(Of T)
		Protected Friend _types As New HashSet(Of EventType)
		Public Overloads Sub Add(item As T)
			list.Add(item)
			_types.Add(item.Type)
		End Sub
		Public Overloads Function Remove(item As T)
			_types.Remove(item.Type)
			Return list.Remove(item)
		End Function
		Public Overrides Function ToString() As String
			Return $"{If(_types.Contains(EventType.SetBeatsPerMinute) OrElse _types.Contains(EventType.PlaySong),
				"BPM, ", If(_types.Contains(EventType.SetCrotchetsPerBar),
				"CPB, ", ""))}Count={list.Count}"
		End Function
	End Class
	Public MustInherit Class OrderedEventCollection
		Implements ICollection(Of BaseEvent)
		Friend Property EventsBeatOrder As New SortedDictionary(Of RDBeat, TypedList(Of BaseEvent))
		<JsonIgnore> Public Overridable ReadOnly Property Count As Integer Implements ICollection(Of BaseEvent).Count
			Get
				Return Me.EventsBeatOrder.Sum(Function(i) i.Value.list.Count)
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of BaseEvent).IsReadOnly
		<JsonIgnore> Public ReadOnly Property Length As RDBeat
			Get
				Return Me.EventsBeatOrder.LastOrDefault().Value.list.First.Beat
			End Get
		End Property
		Public Sub New()
		End Sub
		Public Sub New(items As IEnumerable(Of BaseEvent))
			For Each item In items
				Me.Add(item)
			Next
		End Sub
		Public Function ConcatAll() As List(Of BaseEvent)
			Return Me.EventsBeatOrder.SelectMany(Function(i) i.Value.list).ToList
		End Function
		Public Sub Add(item As BaseEvent) Implements ICollection(Of BaseEvent).Add
			Dim value As TypedList(Of BaseEvent) = Nothing

			If Not EventsBeatOrder.TryGetValue(item.Beat, value) Then
				value = New TypedList(Of BaseEvent)
				EventsBeatOrder.Add(item.Beat, value)
			End If

			value.Add(item)
		End Sub
		Public Sub AddRange(items As IEnumerable(Of BaseEvent))
			For Each item In items
				Add(item)
			Next
		End Sub
		Public Sub Clear() Implements ICollection(Of BaseEvent).Clear
			'EventsTypeOrder.Clear()
			EventsBeatOrder.Clear()
		End Sub
		Public Overridable Function Contains(item As BaseEvent) As Boolean Implements ICollection(Of BaseEvent).Contains
			'            Return EventsTypeOrder.ContainsKey(item.Type) AndAlso
			'EventsTypeOrder(item.Type).ContainsKey(item.Beat.BeatOnly) AndAlso
			'EventsTypeOrder(item.Type)(item.Beat.BeatOnly).Contains(item)
			Return EventsBeatOrder.ContainsKey(item.Beat) AndAlso EventsBeatOrder(item.Beat).list.Contains(item)
		End Function
		Public Sub CopyTo(array() As BaseEvent, arrayIndex As Integer) Implements ICollection(Of BaseEvent).CopyTo
			ConcatAll.CopyTo(array, arrayIndex)
		End Sub
		Friend Function Remove(item As BaseEvent) As Boolean Implements ICollection(Of BaseEvent).Remove
			If Contains(item) Then
				Dim result = Me.EventsBeatOrder(item.Beat).Remove(item)
				If Me.EventsBeatOrder(item.Beat).list.Count = 0 Then
					EventsBeatOrder.Remove(item.Beat)
				End If
				Return result
			End If
			Return False
		End Function
		Public Iterator Function GetEnumerator() As IEnumerator(Of BaseEvent) Implements IEnumerable(Of BaseEvent).GetEnumerator
			For Each pair In Me.EventsBeatOrder
				For Each item In pair.Value.list
					Yield item
				Next
			Next
		End Function
		Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			For Each pair In Me.EventsBeatOrder
				For Each item In pair.Value.list
					Yield item
				Next
			Next
		End Function
		Public Overrides Function ToString() As String
			Return $"Count = {Count}"
		End Function
	End Class
	Public Class OrderedEventCollection(Of T As BaseEvent)
		Inherits OrderedEventCollection
		Public Sub New()
		End Sub
		Public Sub New(items As IEnumerable(Of T))
			For Each item In items
				Me.Add(item)
			Next
		End Sub
		Public Overridable Overloads Sub Add(item As T)
			MyBase.Add(item)
		End Sub
		Public Overridable Overloads Function Remove(item As T) As Boolean
			Return MyBase.Remove(item)
		End Function
		Public Overrides Function ToString() As String
			Return $"Count = {Count}"
		End Function
	End Class
	Public Class Union(Of A, B)
		Private value As (A As A, B As B)
		Public Sub New(value As A)
			Me.value.A = value
		End Sub
		Public Sub New(value As B)
			Me.value.B = value
		End Sub
		Public Shared Widening Operator CType(value As A) As Union(Of A, B)
			Return New Union(Of A, B)(value)
		End Operator
		Public Shared Widening Operator CType(value As B) As Union(Of A, B)
			Return New Union(Of A, B)(value)
		End Operator
		Public Shared Widening Operator CType(value As Union(Of A, B)) As A
			Return value.value.A
		End Operator
		Public Shared Widening Operator CType(value As Union(Of A, B)) As B
			Return value.value.B
		End Operator
	End Class
	Public Class SoundSubType
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
		Public Property GroupSubtype As GroupSubtypes
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
			Return ShouldSerialize() AndAlso Volume <> 100
		End Function
		Friend Function ShouldSerializePitch() As Boolean
			Return ShouldSerialize() AndAlso Pitch <> 100
		End Function
		Friend Function ShouldSerializePan() As Boolean
			Return ShouldSerialize()
		End Function
		Friend Function ShouldSerializeOffset() As Boolean
			Return ShouldSerialize()
		End Function
	End Class
End Namespace
Namespace LevelElements
	Public Class Condition
		Public Property ConditionLists As New List(Of (Enabled As Boolean, Conditional As BaseConditional))
		Public Property Duration As Single
		Public Sub New()
		End Sub
		Friend Shared Function Load(text As String) As Condition
			Dim out As New Condition
			Dim Matches = Regex.Matches(text, "(~?\d+)(?=[&d])")
			If Matches.Count > 0 Then
				out.Duration = CDbl(Regex.Match(text, "[\d\.]").Value)
				Return out
			Else
				Throw New RhythmBaseException("Wrong condition.")
			End If
		End Function
		Public Function Serialize() As String
			Return String.Join("&", ConditionLists.Select(Of String)(Function(i) If(i.Enabled, "", "~") + i.Conditional.Id.ToString)) + "d" + Duration.ToString
		End Function
		Public Overrides Function ToString() As String
			Return Serialize()
		End Function
	End Class
	<JsonObject>
	Public Class Decoration
		Inherits OrderedEventCollection(Of BaseDecorationAction)
		Private _id As String
		<JsonIgnore>
		Friend Parent As RDLevel
		Private _file As RDSprite
		<JsonProperty("id")> Public Property Id As String
			Get
				Return _id
			End Get
			Set(value As String)
				_id = value
			End Set
		End Property
		<JsonIgnore> Public ReadOnly Property Size As SKSizeI
			Get
				Return If(File?.Size, New SKSizeI(32, 31))
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property Expressions As IEnumerable(Of String)
			Get
				Return If(File?.Expressions, New List(Of String))
			End Get
		End Property
		Public ReadOnly Property Row As ULong
			Get
				Return Parent.Decorations.ToList.IndexOf(Me)
			End Get
		End Property
		Public Property Rooms As New RDSingleRoom(0)
		<JsonProperty("filename")> Public Property File As RDSprite
			Get
				Return _file
			End Get
			Set(value As RDSprite)
				_file = value
				Parent?.Assets.Add(value)
			End Set
		End Property
		Public Property Depth As Integer
		Public Property Filter As Filters
		Public Property Visible As Boolean
		Sub New()
		End Sub
		Friend Sub New(room As RDSingleRoom)
			Me.Rooms = room
			Me._id = Me.GetHashCode
		End Sub
		<Obsolete("This function is obsolete and may be removed in the next release. Call Add() instead.")> Public Function CreateChildren(Of T As {BaseDecorationAction, New})(beatOnly As Single) As T
			Throw New RhythmBaseException("This function is obsolete and may be removed in the next release. Call CreateChildren<T>(RDBeat beatOnly) instead.")
		End Function
		<Obsolete("This function is obsolete and may be removed in the next release. Call Add() instead.")> Public Function CreateChildren(Of T As {BaseDecorationAction, New})(beatOnly As RDBeat) As T
			Throw New RhythmBaseException("This function is obsolete and may be removed in the next release. Call CreateChildren<T>(RDBeat beatOnly) instead.")
		End Function
		<Obsolete("This function is obsolete and may be removed in the next release. Call Add() instead.")> Public Function CreateChildren(Of T As {BaseDecorationAction, New})(item As T) As T
			Throw New RhythmBaseException("This function is obsolete and may be removed in the next release. Call Add() instead.")
		End Function
		Public Overrides Sub Add(item As BaseDecorationAction)
			item._parent = Me
			Parent.Add(item)
		End Sub
		Friend Sub AddSafely(item As BaseDecorationAction)
			MyBase.Add(item)
		End Sub
		Public Overrides Function Remove(item As BaseDecorationAction) As Boolean
			Return Parent.Remove(item)
		End Function
		Friend Function RemoveSafely(item As BaseDecorationAction) As Boolean
			Return MyBase.Remove(item)
		End Function
		Public Overrides Function ToString() As String
			Return $"{_id}, {Row}, {_Rooms}, {File.Name}"
		End Function
		Friend Function Clone() As Decoration
			Return Me.MemberwiseClone
		End Function
	End Class
	<JsonObject>
	Public Class Row
		Inherits OrderedEventCollection(Of BaseRowAction)
		Public Enum PlayerMode
			P1
			P2
			CPU
		End Enum
		Private _rowType As RowType
		<JsonIgnore>
		Friend Parent As RDLevel
		Public Property Character As RDCharacter
		'Public Property CpuMaker As Characters?
		Public Property RowType As RowType
			Get
				Return _rowType
			End Get
			Set(value As RowType)
				If value <> _rowType Then
					Clear()
					_rowType = value
				End If
			End Set
		End Property
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Include)>
		Public ReadOnly Property Row As SByte
			Get
				Return Parent._Rows.IndexOf(Me)
			End Get
		End Property
		Public Property Rooms As New RDSingleRoom(0)
		Public Property HideAtStart As Boolean
		Public Property Player As PlayerMode
		<JsonIgnore>
		Public Property Sound As New Audio
		Public Property MuteBeats As Boolean
		Public Property RowToMimic As SByte = -1
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
		Friend Function ShouldSerializeMuteBeats() As Boolean
			Return MuteBeats
		End Function
		Friend Function ShouldSerializeHideAtStart() As Boolean
			Return HideAtStart
		End Function
		Friend Function ShouldSerializeRowToMimic() As Boolean
			Return RowToMimic >= -1
		End Function
		<Obsolete("This function is obsolete and may be removed in the next release. Call Add() instead.")> Public Function CreateChildren(Of T As {BaseRowAction, New})(beatOnly As Single) As T
			Throw New RhythmBaseException("This function is obsolete and may be removed in the next release. Call Add() instead.")
		End Function
		<Obsolete("This function is obsolete and may be removed in the next release. Call Add() instead.")> Public Function CreateChildren(Of T As {BaseRowAction, New})(item As T) As T
			Throw New RhythmBaseException("This function is obsolete and may be removed in the next release. Call Add() instead.")
		End Function
		Public Overrides Sub Add(item As BaseRowAction)
			item._parent = Me
			Parent.Add(item)
		End Sub
		Friend Sub AddSafely(item As BaseRowAction)
			MyBase.Add(item)
		End Sub
		Public Overrides Function Remove(item As BaseRowAction) As Boolean
			Return Parent.Remove(item)
		End Function
		Friend Function RemoveSafely(item As BaseRowAction) As Boolean
			Return MyBase.Remove(item)
		End Function
	End Class
	Public Class Bookmark
		Enum BookmarkColors
			Blue
			Red
			Yellow
			Green
		End Enum
		Public Property Beat As RDBeat
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
		Friend ParentCollection As List(Of BaseConditional)
		Public MustOverride ReadOnly Property Type As ConditionalType
		Public Property Tag As String
		Public Property Name As String
		Public ReadOnly Property Id As Integer
			Get
				Return ParentCollection.IndexOf(Me) + 1
			End Get
		End Property
		Public Overrides Function ToString() As String
			Return Name
		End Function
	End Class
	Namespace Conditions
		Public Class LastHit
			Inherits BaseConditional
			<Flags>
			Enum HitResult
				Perfect = &B0
				SlightlyEarly = &B10
				SlightlyLate = &B11
				VeryEarly = &B100
				VeryLate = &B101
				AnyEarlyOrLate = &B111
				Missed = &B1111
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
			Public Property Language As Languages
			Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.Language
		End Class
		Public Class PlayerMode
			Inherits BaseConditional
			Public Property TwoPlayerMode As Boolean
			Public Overrides ReadOnly Property Type As ConditionalType = ConditionalType.PlayerMode
		End Class
	End Namespace
	Public Class RDLevel
		Inherits OrderedEventCollection(Of BaseEvent)
		Friend _path As String
		Public Property Settings As New Settings
		Friend ReadOnly Property _Rows As New List(Of Row)(16)
		Friend ReadOnly Property _Decorations As New List(Of Decoration)
		Public ReadOnly Property Rows As ICollection(Of Row)
			Get
				Return _Rows
			End Get
		End Property
		Public ReadOnly Property Decorations As ICollection(Of Decoration)
			Get
				Return _Decorations
			End Get
		End Property
		Public ReadOnly Property Conditionals As New List(Of BaseConditional)
		Public ReadOnly Property Bookmarks As New List(Of Bookmark)
		Public ReadOnly Property ColorPalette As New LimitedList(Of SKColor)(21, New SKColor(&HFF, &HFF, &HFF, &HFF))
		<JsonIgnore> Public ReadOnly Property Path As String
			Get
				Return _path
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property Directory As String
			Get
				Return IO.Path.GetDirectoryName(_path)
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Assets As New HashSet(Of RDSprite)
		<JsonIgnore> Public ReadOnly Variables As New Variables
		<JsonIgnore> Public ReadOnly Calculator As New BeatCalculator(Me)
		<JsonIgnore> Public ReadOnly Property DefaultBeat As RDBeat
			Get
				Return Calculator.BeatOf(1)
			End Get
		End Property
		Public Sub New()
		End Sub
		Public Sub New(items As IEnumerable(Of BaseEvent))
			For Each item In items
				Me.Add(item)
			Next
		End Sub
		Public Function CreateDecoration(room As RDSingleRoom, Optional sprite As RDSprite = Nothing) As Decoration
			Dim temp As New Decoration(room) With {.Parent = Me, .File = sprite}
			_Decorations.Add(temp)
			Return temp
		End Function
		Public Function CloneDecoration(decoration As Decoration) As Decoration
			Dim temp = decoration.Clone
			Me._Decorations.Add(temp)
			Return temp
		End Function
		Public Function RemoveDecoration(decoration As Decoration) As Boolean
			If Decorations.Contains(decoration) Then
				MyBase.RemoveRange(decoration)
				Return _Decorations.Remove(decoration)
			End If
			Return False
		End Function
		Public Function CreateRow(room As RDSingleRoom, character As RDCharacter) As Row
			Dim temp As New Row() With {.Character = character, .Rooms = room, .Parent = Me}
			temp.Parent = Me
			_Rows.Add(temp)
			Return temp
		End Function
		Public Function RemoveRow(row As Row) As Boolean
			If Rows.Contains(row) Then
				Return _Rows.Remove(row)
			End If
			Return False
		End Function
		Private Shared Function LoadZip(RDLevelFile As String) As FileInfo
			Dim tempDirectoryName As String = RDLevelFile
			Dim tempDirectory = New IO.DirectoryInfo(IO.Path.Combine(IO.Path.GetTempPath, IO.Path.GetRandomFileName))
			tempDirectory.Create()
			Try
				ZipFile.ExtractToDirectory(RDLevelFile, tempDirectory.FullName)
				Return tempDirectory.GetFiles.Where(Function(i) i.Name = "main.rdlevel").First
			Catch ex As Exception
				Throw New RhythmBaseException("Cannot extract the file.", ex)
			End Try
		End Function
		Public Shared Function LoadFile(RDLevelFilePath As String) As RDLevel
			Return LoadFile(RDLevelFilePath, New LevelInputSettings)
		End Function
		Public Shared Function LoadFile(RDLevelFilePath As String, settings As LevelInputSettings) As RDLevel
			Dim LevelSerializerSettings = New JsonSerializer()
			LevelSerializerSettings.Converters.Add(New RDLevelConverter(RDLevelFilePath, settings))
			'Dim json As String
			Select Case IO.Path.GetExtension(RDLevelFilePath)
				Case ".rdzip"
					Return LevelSerializerSettings.Deserialize(Of RDLevel)(New JsonTextReader(File.OpenText(LoadZip(RDLevelFilePath).FullName)))
				Case ".zip"
					Return LevelSerializerSettings.Deserialize(Of RDLevel)(New JsonTextReader(File.OpenText(LoadZip(RDLevelFilePath).FullName)))
				Case ".rdlevel"
					Return LevelSerializerSettings.Deserialize(Of RDLevel)(New JsonTextReader(File.OpenText(RDLevelFilePath)))
				Case Else
					Throw New RhythmBaseException("File not supported.")
			End Select
		End Function
		Private Function Serializer(settings As LevelOutputSettings) As JsonSerializer
			Dim LevelSerializerSettings = New JsonSerializer()
			LevelSerializerSettings.Converters.Add(New Converters.RDLevelConverter(_path, settings))
			Return LevelSerializerSettings
		End Function
		Private Sub WriteStream(fileStream As TextWriter, settings As LevelOutputSettings)
			Using writer As New JsonTextWriter(fileStream)
				Serializer(settings).Serialize(writer, Me)
			End Using
		End Sub
		Public Sub SaveFile(filepath As String)
			SaveFile(filepath, New LevelOutputSettings)
		End Sub
		Public Sub SaveFile(filepath As String, settings As LevelOutputSettings)
			If IO.Path.GetFullPath(_path) = IO.Path.GetFullPath(filepath) Then
				Throw New RhythmBaseException($"Cannot save file '{_path}' because overwriting is disabled by the settings and a file with the same name already exists.
To correct this, change the path or filename or change the OverWrite property of LevelOutputSettings to false.")
			End If
			Using file = IO.File.CreateText(filepath)
				WriteStream(file, settings)
			End Using
		End Sub
		Public Function ToJObject() As Linq.JObject
			Return Linq.JObject.FromObject(Me)
		End Function
		Public Function ToRDLevelJson() As String
			Return ToRDLevelJson(New LevelOutputSettings)
		End Function
		Public Function ToRDLevelJson(settings As LevelOutputSettings) As String
			Dim file = New IO.StringWriter()
			WriteStream(file, settings)
			file.Close()
			Return file.ToString
		End Function
		Public Overrides Sub Add(item As BaseEvent)

			'添加默认节拍
			If item._beat.IsEmpty Then
				item._beat = Calculator.BeatOf(1)
			End If

			If item.Type = EventType.Comment AndAlso CType(item, Comment).Parent Is Nothing Then
				'注释事件可能在精灵板块，也可能不在
				MyBase.Add(item)

			ElseIf item.Type = EventType.TintRows AndAlso CType(item, TintRows).Parent Is Nothing Then
				'轨道染色事件可能是为所有轨道染色
				MyBase.Add(item)

			ElseIf RowTypes.Contains(item.Type) Then
				'添加至对应轨道
				CType(item, BaseRowAction).Parent?.AddSafely(item)
				MyBase.Add(item)
				Return

			ElseIf DecorationTypes.Contains(item.Type) Then
				'添加至对应精灵
				CType(item, BaseDecorationAction).Parent?.AddSafely(item)
				MyBase.Add(item)
				Return
			Else
				If item.Type = EventType.SetCrotchetsPerBar Then
					'更新拍号
					RefreshCPBs(item.Beat)

				ElseIf ConvertToEnums(Of BaseBeatsPerMinute).Contains(item.Type) Then
					'更新 BPM
					RefreshBPMs(item.Beat)
				End If
				'添加事件
				MyBase.Add(item)

				If item.Type = EventType.SetCrotchetsPerBar Then
					'更新拍号
					Calculator.Refresh()

				End If
			End If
		End Sub
		Public Overrides Function Contains(item As BaseEvent) As Boolean
			Return (RowTypes.Contains(item.Type) AndAlso Rows.Any(Function(i) i.Contains(item))) OrElse
(DecorationTypes.Contains(item.Type) AndAlso Decorations.Any(Function(i) i.Contains(item))) OrElse
MyBase.Contains(item)
		End Function
		Public Overrides Function Remove(item As BaseEvent) As Boolean
			If RowTypes.Contains(item.Type) AndAlso Rows.Any(Function(i) i.RemoveSafely(CType(item, BaseRowAction))) Then
				MyBase.Remove(item)
				Return True
			End If
			If DecorationTypes.Contains(item.Type) AndAlso Decorations.Any(Function(i) i.RemoveSafely(CType(item, BaseDecorationAction))) Then
				MyBase.Remove(item)
				Return True
			End If
			If Contains(item) Then
				Dim result = MyBase.Remove(item)
				If item.Type = EventType.SetCrotchetsPerBar Then
					RefreshCPBs(item.Beat)
				ElseIf ConvertToEnums(Of BaseBeatsPerMinute).Contains(item.Type) Then
					RefreshBPMs(item.Beat)
				End If
				Return result
			End If
			Return False
		End Function
		Private Sub RefreshBPMs(start As RDBeat)
			For Each item In EventsBeatOrder.Keys
				item.ResetBPM()
			Next
			For Each jtem In Me.Where(Function(i) i.Beat > start)
				jtem._beat.ResetBPM()
			Next
			For Each item In Bookmarks
				item.Beat.ResetBPM()
			Next
		End Sub
		Private Sub RefreshCPBs(start As RDBeat)
			For Each item In EventsBeatOrder.Keys
				item.ResetCPB()
			Next
			For Each jtem In Me.Where(Function(i) i.Beat > start)
				jtem._beat.ResetCPB()
			Next
			For Each item In Bookmarks
				item.Beat.ResetCPB()
			Next
		End Sub
		Public Overrides Function ToString() As String
			Return $"""{Settings.Song}"" Count = {Count}"
		End Function
	End Class
	Public Class Settings
		Public Enum SpecialArtistTypes
			None
			AuthorIsArtist
			PublicLicense
		End Enum
		Public Enum DifficultyLevel
			Easy
			Medium
			Tough
			VeryTough
		End Enum
		Public Enum LevelPlayedMode
			OnePlayerOnly
			TwoPlayerOnly
			BothModes
		End Enum
		Public Enum FirstBeatBehaviors
			RunNormally
			RunEventsOnPrebar
		End Enum
		Public Enum MultiplayerAppearances
			HorizontalStrips
		End Enum
		Public Property Version As Integer = 60
		Public Property Artist As String = "" 'Done
		Public Property Song As String = "" 'Done
		Public Property SpecialArtistType As SpecialArtistTypes = SpecialArtistTypes.None 'Enum
		Public Property ArtistPermission As String = "" 'Done
		Public Property ArtistLinks As String = "" 'Link
		Public Property Author As String = "" 'done
		Public Property Difficulty As DifficultyLevel = DifficultyLevel.Easy 'Enum
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
		Public Property CanBePlayedOn As LevelPlayedMode = LevelPlayedMode.OnePlayerOnly 'Enum
		Public Property FirstBeatBehavior As FirstBeatBehaviors = FirstBeatBehaviors.RunNormally 'Enum
		Public Property MultiplayerAppearance As MultiplayerAppearances = MultiplayerAppearances.HorizontalStrips 'Enum
		Public Property LevelVolume As Single = 1
		Public Property RankMaxMistakes As New LimitedList(Of Integer)(4, 20)
		Public Property RankDescription As New LimitedList(Of String)(6, "")
		<JsonProperty(NullValueHandling:=NullValueHandling.Ignore)>
		Public Property Mods As List(Of String)
		'oldBassDrop
		'startImmediately
		'classicHitParticles
		'adaptRowsToRoomHeight
		'noSmartJudgment
		'smoothShake
		'rotateShake
		'wobblyLines
		'bombBeats
		'noDoublePulse
		'invisibleCharacters
		'gentleBassDrop
	End Class
End Namespace