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
		Public ReadOnly Parent As RDBaseBeat
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
		Public Sub New(parent As RDBaseBeat, beat As RDBeat, Optional hold As Single = 0)
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
		Friend _calculator As RDBeatCalculator
		Private Shared _isBeatLoaded As Boolean
		Private _isBarBeatLoaded As Boolean
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
						_beat = _calculator.BarBeat_BeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1
					ElseIf _isTimeSpanLoaded Then
						_beat = _calculator.Time_BeatOnly(_TimeSpan) - 1
					End If
					_isBeatLoaded = True
				End If
				Return _beat + 1
			End Get
		End Property
		Public ReadOnly Property BarBeat As (bar As UInteger, beat As Single)
			Get
				IfNullThrowException()
				If Not _isBarBeatLoaded Then
					If _isBeatLoaded Then
						_BarBeat = _calculator.BeatOnly_BarBeat(_beat + 1)
					ElseIf _isTimeSpanLoaded Then
						_beat = _calculator.Time_BeatOnly(_TimeSpan) - 1
						_isBeatLoaded = True
						_BarBeat = _calculator.BeatOnly_BarBeat(_beat + 1)
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
						_TimeSpan = _calculator.BeatOnly_Time(_beat + 1)
					ElseIf _isBarBeatLoaded Then
						_beat = _calculator.BarBeat_BeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1
						_isBeatLoaded = True
						_TimeSpan = _calculator.BeatOnly_Time(_beat + 1)
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
		Shared Sub New()
			_isBeatLoaded = True
		End Sub
		Public Sub New(beatOnly As Single)
			_beat = beatOnly - 1
		End Sub
		Public Sub New(calculator As RDBeatCalculator, beatOnly As Single)
			If beatOnly < 1 Then
				Throw New OverflowException($"The beat must not be less than 1, but {beatOnly} is given")
			End If
			_calculator = calculator
			_beat = beatOnly - 1
		End Sub
		Public Sub New(calculator As RDBeatCalculator, bar As UInteger, beat As Single)
			If bar < 1 Then
				Throw New OverflowException($"The bar must not be less than 1, but {bar} is given")
			End If
			If beat < 1 Then
				Throw New OverflowException($"The beat must not be less than 1, but {beat} is given")
			End If
			_calculator = calculator
			_BarBeat = (bar, beat)
			_beat = _calculator.BarBeat_BeatOnly(bar, beat) - 1
			_isBarBeatLoaded = True
		End Sub
		Public Sub New(calculator As RDBeatCalculator, timeSpan As TimeSpan)
			If timeSpan < TimeSpan.Zero Then
				Throw New OverflowException($"The time must not be less than zero, but {timeSpan} is given")
			End If
			_calculator = calculator
			_TimeSpan = timeSpan
			_beat = _calculator.Time_BeatOnly(timeSpan) - 1
			_isTimeSpanLoaded = True
		End Sub
		Public Shared Function [Default](calculator As RDBeatCalculator) As RDBeat
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
		Public Shared Function FromSameLevelOrNull(a As RDBeat, b As RDBeat, Optional [throw] As Boolean = False) As Boolean
			Return a.baseLevel Is Nothing OrElse b.baseLevel Is Nothing OrElse FromSameLevel(a, b, [throw])
		End Function
		Public Function FromSameLevel(b As RDBeat, Optional [throw] As Boolean = False) As Boolean
			Return FromSameLevel(Me, b, [throw])
		End Function
		Public Function FromSameLevelOrNull(b As RDBeat, Optional [throw] As Boolean = False) As Boolean
			Return baseLevel Is Nothing OrElse b.baseLevel Is Nothing OrElse FromSameLevel(b, [throw])
		End Function
		Public Function WithoutConnection() As RDBeat
			Dim result = Me
			result._calculator = Nothing
			Return result
		End Function
		Private Sub IfNullThrowException()
			If IsEmpty Then
				Throw New InvalidRDBeatException
			End If
		End Sub
		Public Sub ResetCache()
			Dim m = BeatOnly
			_isBarBeatLoaded = False
			_isTimeSpanLoaded = False
		End Sub
		Public Sub ResetBPM()
			If Not _isBeatLoaded Then
				_beat = _calculator.Time_BeatOnly(_TimeSpan) - 1
			End If
			_isBeatLoaded = True
			_isTimeSpanLoaded = False
			_isBPMLoaded = False
		End Sub
		Public Sub ResetCPB()
			If Not _isBeatLoaded Then
				_beat = _calculator.BarBeat_BeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1
			End If
			_isBeatLoaded = True
			_isBarBeatLoaded = False
			_isCPBLoaded = False
		End Sub
		Public Shared Operator +(a As RDBeat, b As Single) As RDBeat
			Return New RDBeat(a._calculator, a.BeatOnly + b)
		End Operator
		Public Shared Operator -(a As RDBeat, b As Single) As RDBeat
			Return New RDBeat(a._calculator, a.BeatOnly - b)
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
				(a._beat = b._beat) OrElse
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
	Public NotInheritable Class RDAudio
		Public Property Filename As String
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> <DefaultValue(100)> Public Property Volume As Integer = 100
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> <DefaultValue(100)> Public Property Pitch As Integer = 100
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> Public Property Pan As Integer = 0
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> Public Property Offset As Integer = 0
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
	Friend Class RDTypedList(Of T As RDBaseEvent)
		Implements IEnumerable(Of T)

		Private ReadOnly list As New List(Of T)
		Protected Friend _types As New HashSet(Of RDEventType)
		Public Overloads Sub Add(item As T)
			list.Add(item)
			_types.Add(item.Type)
		End Sub
		Public Overloads Function Remove(item As T)
			_types.Remove(item.Type)
			Return list.Remove(item)
		End Function
		Public Function BeforeThan(item1 As RDBaseEvent, item2 As RDBaseEvent)
			Return list.IndexOf(item1) < list.IndexOf(item2)
		End Function
		Public Overrides Function ToString() As String
			Return $"{If(_types.Contains(RDEventType.SetBeatsPerMinute) OrElse _types.Contains(RDEventType.PlaySong),
				"BPM, ", If(_types.Contains(RDEventType.SetCrotchetsPerBar),
				"CPB, ", ""))}Count={list.Count}"
		End Function
		Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
			Return list.GetEnumerator
		End Function
		Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return list.GetEnumerator
		End Function
	End Class
	Public Class ADTypedList(Of T As ADBaseEvent)
		Implements IEnumerable(Of T)

		Private ReadOnly list As New List(Of T)
		Protected Friend _types As New HashSet(Of ADEventType)
		Public Overloads Sub Add(item As T)
			list.Add(item)
			_types.Add(item.Type)
		End Sub
		Public Overloads Function Remove(item As T)
			_types.Remove(item.Type)
			Return list.Remove(item)
		End Function
		Public Overrides Function ToString() As String
			'Return $"{If(_types.Contains(ADEventType.SetBeatsPerMinute) OrElse _types.Contains(RDEventType.PlaySong),
			'	"BPM, ", If(_types.Contains(ADEventType.SetCrotchetsPerBar),
			'	"CPB, ", ""))}Count = {list.Count}"
			Return $"Count = {list.Count}"
		End Function
		Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
			Return list.GetEnumerator
		End Function
		Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return list.GetEnumerator
		End Function
	End Class
	Public MustInherit Class RDOrderedEventCollection
		Implements ICollection(Of RDBaseEvent)
		Friend eventsBeatOrder As New SortedDictionary(Of RDBeat, RDTypedList(Of RDBaseEvent))
		<JsonIgnore> Public Overridable ReadOnly Property Count As Integer Implements ICollection(Of RDBaseEvent).Count
			Get
				Return Me.eventsBeatOrder.Sum(Function(i) i.Value.Count)
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of RDBaseEvent).IsReadOnly
		<JsonIgnore> Public ReadOnly Property Length As RDBeat
			Get
				Return Me.eventsBeatOrder.LastOrDefault().Value.First.Beat
			End Get
		End Property
		Public Sub New()
		End Sub
		Public Sub New(items As IEnumerable(Of RDBaseEvent))
			For Each item In items
				Me.Add(item)
			Next
		End Sub
		Public Function ConcatAll() As List(Of RDBaseEvent)
			Return Me.eventsBeatOrder.SelectMany(Function(i) i.Value).ToList
		End Function
		Public Sub Add(item As RDBaseEvent) Implements ICollection(Of RDBaseEvent).Add
			Dim value As RDTypedList(Of RDBaseEvent) = Nothing

			If Not eventsBeatOrder.TryGetValue(item.Beat, value) Then
				value = New RDTypedList(Of RDBaseEvent)
				eventsBeatOrder.Add(item.Beat, value)
			End If

			value.Add(item)
		End Sub
		Public Sub Clear() Implements ICollection(Of RDBaseEvent).Clear
			'EventsTypeOrder.Clear()
			eventsBeatOrder.Clear()
		End Sub
		Public Overridable Function Contains(item As RDBaseEvent) As Boolean Implements ICollection(Of RDBaseEvent).Contains
			'            Return EventsTypeOrder.ContainsKey(item.Type) AndAlso
			'EventsTypeOrder(item.Type).ContainsKey(item.Beat.BeatOnly) AndAlso
			'EventsTypeOrder(item.Type)(item.Beat.BeatOnly).Contains(item)
			Return eventsBeatOrder.ContainsKey(item.Beat) AndAlso eventsBeatOrder(item.Beat).Contains(item)
		End Function
		Public Sub CopyTo(array() As RDBaseEvent, arrayIndex As Integer) Implements ICollection(Of RDBaseEvent).CopyTo
			ConcatAll.CopyTo(array, arrayIndex)
		End Sub
		Friend Function Remove(item As RDBaseEvent) As Boolean Implements ICollection(Of RDBaseEvent).Remove
			If Contains(item) Then
				Dim result = Me.eventsBeatOrder(item.Beat).Remove(item)
				If Not eventsBeatOrder(item.Beat).Any() Then
					eventsBeatOrder.Remove(item.Beat)
				End If
				Return result
			End If
			Return False
		End Function
		Public Iterator Function GetEnumerator() As IEnumerator(Of RDBaseEvent) Implements IEnumerable(Of RDBaseEvent).GetEnumerator
			For Each pair In Me.eventsBeatOrder
				For Each item In pair.Value
					Yield item
				Next
			Next
		End Function
		Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			For Each pair In Me.eventsBeatOrder
				For Each item In pair.Value
					Yield item
				Next
			Next
		End Function
		Public Overrides Function ToString() As String
			Return $"Count = {Count}"
		End Function
	End Class
	Public Class RDOrderedEventCollection(Of T As RDBaseEvent)
		Inherits RDOrderedEventCollection
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
	Public MustInherit Class ADTileCollection
		Implements ICollection(Of ADTile)
		Friend eventsOrder As New List(Of ADTile)
		Public ReadOnly Property Count As Integer = eventsOrder.Count Implements ICollection(Of ADTile).Count
		Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of ADTile).IsReadOnly
		Public Sub Add(item As ADTile) Implements ICollection(Of ADTile).Add
			eventsOrder.Add(item)
		End Sub
		Public Sub Clear() Implements ICollection(Of ADTile).Clear
			eventsOrder.Clear()
		End Sub
		Public Sub CopyTo(array() As ADTile, arrayIndex As Integer) Implements ICollection(Of ADTile).CopyTo
			eventsOrder.CopyTo(array, arrayIndex)
		End Sub
		Public Function Contains(item As ADTile) As Boolean Implements ICollection(Of ADTile).Contains
			Return eventsOrder.Contains(item)
		End Function
		Public Function Remove(item As ADTile) As Boolean Implements ICollection(Of ADTile).Remove
			Return eventsOrder.Remove(item)
		End Function
		Public Function GetEnumerator() As IEnumerator(Of ADTile) Implements IEnumerable(Of ADTile).GetEnumerator
			Return eventsOrder.GetEnumerator
		End Function
		Public Overridable Iterator Function Events() As IEnumerable(Of ADBaseEvent)
			For Each item In eventsOrder
				For Each action In item
					Yield action
				Next
			Next
		End Function
		Public Function IndexOf(item As ADTile) As Integer
			Return eventsOrder.IndexOf(item)
		End Function
		Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return GetEnumerator()
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
		Private Property Audio As New RDAudio
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
	Public Class RDCondition
		Public Property ConditionLists As New List(Of (Enabled As Boolean, Conditional As BaseConditional))
		Public Property Duration As Single
		Public Sub New()
		End Sub
		Friend Shared Function Load(text As String) As RDCondition
			Dim out As New RDCondition
			Dim Matches = Regex.Matches(text, "(~?\d+)(?=[&d])")
			If Matches.Count > 0 Then
				out.Duration = CDbl(Regex.Match(text, "[\d\.]").Value)
				Return out
			Else
				Throw New RhythmBaseException($"Illegal condition: {text}.")
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
		Inherits RDOrderedEventCollection(Of RDBaseDecorationAction)
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
		Public Overrides Sub Add(item As RDBaseDecorationAction)
			item._parent = Me
			Parent.Add(item)
		End Sub
		Friend Sub AddSafely(item As RDBaseDecorationAction)
			MyBase.Add(item)
		End Sub
		Public Overrides Function Remove(item As RDBaseDecorationAction) As Boolean
			Return Parent.Remove(item)
		End Function
		Friend Function RemoveSafely(item As RDBaseDecorationAction) As Boolean
			Return MyBase.Remove(item)
		End Function
		Public Overrides Function ToString() As String
			Return $"{_id}, {Row}, {_Rooms}, {File.FileName}"
		End Function
		Friend Function Clone() As Decoration
			Return Me.MemberwiseClone
		End Function
	End Class
	<JsonObject>
	Public Class RDRow
		Inherits RDOrderedEventCollection(Of RDBaseRowAction)
		Public Enum PlayerMode
			P1
			P2
			CPU
		End Enum
		Private _rowType As RDRowType
		<JsonIgnore>
		Friend Parent As RDLevel
		Public Property Character As RDCharacter
		'Public Property CpuMaker As Characters?
		Public Property RowType As RDRowType
			Get
				Return _rowType
			End Get
			Set(value As RDRowType)
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
		Public Property Sound As New Components.RDAudio
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
		Public Overrides Sub Add(item As RDBaseRowAction)
			item._parent = Me
			Parent.Add(item)
		End Sub
		Friend Sub AddSafely(item As RDBaseRowAction)
			MyBase.Add(item)
		End Sub
		Public Overrides Function Remove(item As RDBaseRowAction) As Boolean
			Return Parent.Remove(item)
		End Function
		Friend Function RemoveSafely(item As RDBaseRowAction) As Boolean
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
		Inherits RDOrderedEventCollection(Of RDBaseEvent)
		Friend _path As String
		<JsonIgnore> Public ReadOnly Assets As New HashSet(Of RDSprite)
		<JsonIgnore> Public ReadOnly Variables As New Variables
		<JsonIgnore> Public ReadOnly Calculator As New RDBeatCalculator(Me)
		Public Property Settings As New RDSettings
		Friend ReadOnly Property _Rows As New List(Of RDRow)(16)
		Friend ReadOnly Property _Decorations As New List(Of Decoration)
		Public ReadOnly Property Rows As IReadOnlyCollection(Of RDRow)
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
		<JsonIgnore> Public ReadOnly Property DefaultBeat As RDBeat
			Get
				Return Calculator.BeatOf(1)
			End Get
		End Property
		Public Sub New()
		End Sub
		Public Sub New(items As IEnumerable(Of RDBaseEvent))
			For Each item In items
				Me.Add(item)
			Next
		End Sub
		Public Shared ReadOnly Property [Default] As RDLevel
			Get
				Dim rdl As New RDLevel From {
					New RDPlaySong With {.Song = New Components.RDAudio With {.Filename = "sndOrientalTechno"}},
					New RDSetTheme With {.Preset = RDSetTheme.Theme.OrientalTechno}
				}
				Dim samurai = rdl.CreateRow(New RDSingleRoom(0), New RDCharacter(Characters.Samurai))
				samurai.Add(New RDAddClassicBeat)
				Return rdl
			End Get
		End Property
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
		Public Function CreateRow(room As RDSingleRoom, character As RDCharacter) As RDRow
			Dim temp As New RDRow() With {.Character = character, .Rooms = room, .Parent = Me}
			temp.Parent = Me
			_Rows.Add(temp)
			Return temp
		End Function
		Public Function RemoveRow(row As RDRow) As Boolean
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
				Return tempDirectory.GetFiles.Single(Function(i) i.Extension = ".rdlevel")
			Catch ex As InvalidOperationException
				Throw New RhythmBaseException("Found more than one rdlevel file.", ex)
			Catch ex As Exception
				Throw New RhythmBaseException("Cannot extract the file.", ex)
			End Try
		End Function
		Public Shared Function LoadFile(filepath As String) As RDLevel
			Return LoadFile(filepath, New LevelInputSettings)
		End Function
		Public Shared Function LoadFile(filepath As String, settings As LevelInputSettings) As RDLevel
			Dim LevelSerializer = New JsonSerializer()
			LevelSerializer.Converters.Add(New RDLevelConverter(filepath, settings))
			'Dim json As String
			Select Case IO.Path.GetExtension(filepath)
				Case ".rdzip", ".zip"
					Return LevelSerializer.Deserialize(Of RDLevel)(New JsonTextReader(File.OpenText(LoadZip(filepath).FullName)))
				Case ".rdlevel"
					Return LevelSerializer.Deserialize(Of RDLevel)(New JsonTextReader(File.OpenText(filepath)))
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
				Throw New OverwriteNotAllowedException(_path, GetType(LevelOutputSettings))
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
		Public Overrides Sub Add(item As RDBaseEvent)

			'添加默认节拍
			If item._beat.IsEmpty Then
				item._beat._calculator = Calculator
			End If

			'部分事件只能在小节的开头
			If TryCast(item, IRDBarBeginningEvent) IsNot Nothing AndAlso item._beat.BarBeat.beat <> 1 Then
				Throw New IllegalBeatException(item)
			End If

			'更改节拍的关联关卡
			item._beat._calculator = Calculator
			item._beat.ResetCache()

			If item.Type = RDEventType.Comment AndAlso CType(item, RDComment).Parent Is Nothing Then
				'注释事件可能在精灵板块，也可能不在
				MyBase.Add(item)

			ElseIf item.Type = RDEventType.TintRows AndAlso CType(item, RDTintRows).Parent Is Nothing Then
				'轨道染色事件可能是为所有轨道染色
				MyBase.Add(item)

			ElseIf RowTypes.Contains(item.Type) Then
				'添加至对应轨道
				CType(item, RDBaseRowAction).Parent?.AddSafely(item)
				MyBase.Add(item)
				Return

			ElseIf DecorationTypes.Contains(item.Type) Then
				'添加至对应精灵
				CType(item, RDBaseDecorationAction).Parent?.AddSafely(item)
				MyBase.Add(item)
				Return
			Else
				If item.Type = RDEventType.SetCrotchetsPerBar Then
					AddSetCrotchetsPerBar(item)
				ElseIf ConvertToRDEnums(Of RDBaseBeatsPerMinute).Contains(item.Type) Then
					AddBaseBeatsPerMinute(item)
				Else
					MyBase.Add(item)
				End If
			End If
		End Sub
		Public Overrides Function Contains(item As RDBaseEvent) As Boolean
			Return (RowTypes.Contains(item.Type) AndAlso Rows.Any(Function(i) i.Contains(item))) OrElse
					(DecorationTypes.Contains(item.Type) AndAlso Decorations.Any(Function(i) i.Contains(item))) OrElse
					MyBase.Contains(item)
		End Function
		Public Overrides Function Remove(item As RDBaseEvent) As Boolean
			If RowTypes.Contains(item.Type) AndAlso Rows.Any(Function(i) i.RemoveSafely(CType(item, RDBaseRowAction))) Then
				MyBase.Remove(item)
				item._beat._calculator = Nothing
				Return True
			End If
			If DecorationTypes.Contains(item.Type) AndAlso Decorations.Any(Function(i) i.RemoveSafely(CType(item, RDBaseDecorationAction))) Then
				MyBase.Remove(item)
				item._beat._calculator = Nothing
				Return True
			End If
			If Contains(item) Then
				If item.Type = RDEventType.SetCrotchetsPerBar Then
					Return RemoveSetCrotchetsPerBar(item)
				ElseIf ConvertToADEnums(Of RDBaseBeatsPerMinute).Contains(item.Type) Then
					Return RemoveBaseBeatsPerMinute(item)
				Else
					Dim result = MyBase.Remove(item)
					item._beat._calculator = Nothing
					Return result
				End If
			End If
			Return False
		End Function
		Protected Friend Sub AddSetCrotchetsPerBar(item As RDSetCrotchetsPerBar)

			Dim frt = item.FrontOrDefault
			Dim nxt = item.NextOrDefault

			''忽略无意义 CPB
			'If frt?.CrotchetsPerBar = If(item?.CrotchetsPerBar, 8) Then
			'	Return
			'End If

			'更新拍号
			RefreshCPBs(item._beat)
			'添加事件
			MyBase.Add(item)

			'更新计算器
			Calculator.Refresh()

			If nxt IsNot Nothing Then
				Dim nxtE = item.After(Of RDBaseEvent).FirstOrDefault(Function(i) TryCast(i, IRDBarBeginningEvent) IsNot Nothing AndAlso
																	   i.Type <> RDEventType.SetCrotchetsPerBar AndAlso
																	   i._beat < nxt._beat)
				Dim interval = If(nxtE?._beat.BeatOnly, nxt._beat.BeatOnly) - item._beat.BeatOnly
				Dim c = interval Mod item.CrotchetsPerBar
				If c > 0 Then
					c = If(c < 2, c + item.CrotchetsPerBar, c)
					MyBase.Add(New RDSetCrotchetsPerBar() With {._beat = item._beat + interval - c, ._crotchetsPerBar = c - 1})
				ElseIf nxt.CrotchetsPerBar = item.CrotchetsPerBar Then
					MyBase.Remove(nxt)
				End If

				If nxtE IsNot Nothing Then
					MyBase.Add(New RDSetCrotchetsPerBar() With {._beat = nxtE._beat, ._crotchetsPerBar = If(frt?.CrotchetsPerBar, 8) - 1})
				End If
			End If

			'更新计算器
			Calculator.Refresh()
		End Sub
		Protected Friend Function RemoveSetCrotchetsPerBar(item As RDSetCrotchetsPerBar) As Boolean

			Dim frt = item.FrontOrDefault
			Dim nxt = item.NextOrDefault

			If nxt IsNot Nothing Then
				Dim nxtE = item.After(Of RDBaseEvent).FirstOrDefault(Function(i) TryCast(i, IRDBarBeginningEvent) IsNot Nothing AndAlso
																	   i.Type <> RDEventType.SetCrotchetsPerBar AndAlso
																	   i._beat < nxt._beat)
				Dim cpb = item.CrotchetsPerBar
				Dim interval = If(nxtE?._beat.BeatOnly, nxt._beat.BeatOnly) - item._beat.BeatOnly
				Dim c = interval Mod If(frt?.CrotchetsPerBar, 8)
				If c > 0 Then
					c = If(c < 2, c + item.CrotchetsPerBar, c)
					If c = nxt.CrotchetsPerBar Then
						MyBase.Remove(nxt)
					End If
					MyBase.Add(New RDSetCrotchetsPerBar With {._beat = item._beat + interval - c, ._crotchetsPerBar = c - 1})
				Else
					If nxtE Is Nothing AndAlso nxt.CrotchetsPerBar = If(frt?.CrotchetsPerBar, 8) Then
						MyBase.Remove(nxt)
					End If
				End If
				If nxtE IsNot Nothing Then
					MyBase.Add(New RDSetCrotchetsPerBar() With {._beat = nxtE._beat, ._crotchetsPerBar = If(frt?.CrotchetsPerBar, 8) - 1})
				End If
				Calculator.Refresh()
			End If

			'更新计算器
			Calculator.Refresh()

			Dim result = MyBase.Remove(item)
			RefreshCPBs(item.Beat)
			item._beat._calculator = Nothing
			Calculator.Refresh()
			Return result
		End Function
		Protected Friend Sub AddBaseBeatsPerMinute(item As RDBaseBeatsPerMinute)
			'更新 BPM
			RefreshBPMs(item.Beat)
			'添加事件
			MyBase.Add(item)
		End Sub
		Protected Friend Function RemoveBaseBeatsPerMinute(item As RDBaseBeatsPerMinute) As Boolean
			Dim result = MyBase.Remove(item)
			RefreshBPMs(item.Beat)
			item._beat._calculator = Nothing
			Return result
		End Function
		Private Sub RefreshBPMs(start As RDBeat)
			For Each item In eventsBeatOrder.Keys
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
			For Each item In eventsBeatOrder.Keys
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
	Public Enum RTSpecialArtistTypes
		None
		AuthorIsArtist
		PublicLicense
	End Enum
	Public Class RDSettings
		Public Enum RDDifficultyLevel
			Easy
			Medium
			Tough
			VeryTough
		End Enum
		Public Enum RDLevelPlayedMode
			OnePlayerOnly
			TwoPlayerOnly
			BothModes
		End Enum
		Public Enum RDFirstBeatBehaviors
			RunNormally
			RunEventsOnPrebar
		End Enum
		Public Enum RDMultiplayerAppearances
			HorizontalStrips
		End Enum
		Public Property Version As Integer = 60
		Public Property Artist As String = "" 'Done
		Public Property Song As String = "" 'Done
		Public Property SpecialArtistType As RTSpecialArtistTypes = RTSpecialArtistTypes.None 'Enum
		Public Property ArtistPermission As String = "" 'Done
		Public Property ArtistLinks As String = "" 'Link
		Public Property Author As String = "" 'done
		Public Property Difficulty As RDDifficultyLevel = RDDifficultyLevel.Easy 'Enum
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
		Public Property CanBePlayedOn As RDLevelPlayedMode = RDLevelPlayedMode.OnePlayerOnly 'Enum
		Public Property FirstBeatBehavior As RDFirstBeatBehaviors = RDFirstBeatBehaviors.RunNormally 'Enum
		Public Property MultiplayerAppearance As RDMultiplayerAppearances = RDMultiplayerAppearances.HorizontalStrips 'Enum
		Public Property LevelVolume As Single = 1
		Public Property RankMaxMistakes As New LimitedList(Of Integer)(4, 20)
		Public Property RankDescription As New LimitedList(Of String)(6, "")
		<JsonProperty(NullValueHandling:=NullValueHandling.Ignore)> Public Property Mods As List(Of String)
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
	Public Enum ADTrackAnimationTypes
		None
		Assemble
		Assemble_Far
		Extend
		Grow
		Grow_Spin
		Fade
		Drop
		Rise
	End Enum
	Public Enum ADTrackDisappearAnimationTypes
		None
		Scatter
		Scatter_Far
		Retract
		Shrink
		Shrink_Spin
		Fade
	End Enum
	Public Enum BgDisplayModes
		FitToScreen
		Unscaled
		Tiled
	End Enum
	Public Class ADLevel
		Inherits ADTileCollection
		Friend _path As String
		Public Property Settings As New ADSettings
		Public Property Decorations As New List(Of ADBaseEvent)
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
		Public Sub New()
		End Sub
		Public Sub New(items As IEnumerable(Of ADTile))
			For Each item In items
				Me.Add(item)
			Next
		End Sub
		Public Shared ReadOnly Property [Default] As ADLevel
			Get
				Return New ADLevel
			End Get
		End Property
		Public Shared Function LoadFile(filepath As String) As ADLevel
			Return LoadFile(filepath, New LevelInputSettings)
		End Function
		Public Shared Function LoadFile(filepath As String, settings As LevelInputSettings) As ADLevel
			Dim LevelSerializer = New JsonSerializer()
			LevelSerializer.Converters.Add(New ADLevelConverter(filepath, settings))
			Select Case IO.Path.GetExtension(filepath)
				Case ".adofai"
					Return LevelSerializer.Deserialize(Of ADLevel)(New JsonTextReader(File.OpenText(filepath)))
				Case Else
					Throw New RhythmBaseException("File not supported.")
			End Select
		End Function
		Public Overrides Iterator Function Events() As IEnumerable(Of ADBaseEvent)
			For Each item In MyBase.Events()
				Yield item
			Next
			For Each item In Decorations
				Yield item
			Next
		End Function
	End Class
	Public Class ADSettings
		Public Property Version As Integer '13
		Public Property Artist As String
		Public Property SpecialArtistType As RTSpecialArtistTypes
		Public Property ArtistPermission As String
		Public Property Song As String
		Public Property Author As String
		Public Property SeparateCountdownTime As Boolean
		Public Property PreviewImage As String
		Public Property PreviewIcon As String
		Public Property PreviewIconColor As SKColor
		Public Property PreviewSongStart As Single
		Public Property PreviewSongDuration As Single
		Public Property SeizureWarning As Boolean
		Public Property LevelDesc As String
		Public Property LevelTags As String
		Public Property ArtistLinks As String
		Public Property Difficulty As Integer
		Public Property RequiredMods As New List(Of String)
		Public Property SongFilename As String
		Public Property Bpm As Single
		Public Property Volume As Single
		Public Property Offset As Single
		Public Property Pitch As Single
		Public Property Hitsound As String
		Public Property HitsoundVolume As Single
		Public Property CountdownTicks As Single
		Public Property TrackColorType As ADTrackColorTypes
		Public Property TrackColor As SKColor
		Public Property SecondaryTrackColor As SKColor
		Public Property TrackColorAnimDuration As Single
		Public Property TrackColorPulse As ADTrackColorPulses
		Public Property TrackPulseLength As Single
		Public Property TrackStyle As ADTrackStyles
		Public Property TrackAnimation As ADTrackAnimationTypes
		Public Property BeatsAhead As Integer
		Public Property TrackDisappearAnimation As ADTrackDisappearAnimationTypes
		Public Property BeatsBehind As Integer
		Public Property BackgroundColor As SKColor
		Public Property ShowDefaultBGIfNoImage As Boolean
		Public Property BgImage As String
		Public Property BgImageColor As SKColor
		Public Property Parallax As RDPointI
		Public Property BgDisplayMode As BgDisplayModes
		Public Property LockRot As Boolean
		Public Property LoopBG As Boolean
		Public Property ScalingRatio As Single
		Public Property RelativeTo As ADCameraRelativeTo
		Public Property Position As RDPointI
		Public Property Rotation As Single
		Public Property Zoom As Single
		Public Property BgVideo As String
		Public Property LoopVideo As Boolean
		Public Property VidOffset As Integer
		Public Property FloorIconOutlines As Boolean
		Public Property StickToFloors As Boolean
		Public Property PlanetEase As EaseType
		Public Property PlanetEaseParts As Integer
		Public Property PlanetEasePartBehavior As ADEasePartBehaviors
		Public Property DefaultTextColor As SKColor
		Public Property DefaultTextShadowColor As SKColor
		Public Property CongratsText As String
		Public Property PerfectText As String
		'Public Property customClass As String
		Public Property LegacyFlash As Boolean
		Public Property LegacyCamRelativeTo As Boolean
		Public Property LegacySpriteTiles As Boolean
	End Class
End Namespace