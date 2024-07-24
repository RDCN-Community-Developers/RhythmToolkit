Imports System.IO
Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports SkiaSharp
Imports System.ComponentModel
Imports System.Formats
#Disable Warning CA1507
#Disable Warning IDE1006
#Disable Warning CA1812
#Disable Warning CA1822
#Disable Warning IDE0060
Namespace Components
	''' <summary>
	''' Special artist types.
	''' </summary>
	Public Enum RTSpecialArtistTypes
		None
		AuthorIsArtist
		PublicLicense
	End Enum
	''' <summary>
	''' In-game character.
	''' </summary>
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
	''' <summary>
	''' Render filter type.
	''' </summary>
	Public Enum Filters
		NearestNeighbor
		BiliNear
	End Enum
	''' <summary>
	''' Room index.
	''' </summary>
	<Flags> Public Enum RoomIndex
		None = &B0
		Room1 = &B1
		Room2 = &B10
		Room3 = &B100
		Room4 = &B1000
		RoomTop = &B10000
		RoomNotAvaliable = &B1111111
	End Enum
	''' <summary>
	''' Variables.
	''' </summary>
	Public NotInheritable Class Variables
		''' <summary>
		''' Integer variables.
		''' </summary>
		Public ReadOnly i As New LimitedList(Of Integer)(10, 0)
		''' <summary>
		''' Float variables.
		''' </summary>
		Public ReadOnly f As New LimitedList(Of Single)(10, 0)
		''' <summary>
		''' Boolean variables.
		''' </summary>
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
	''' <summary>
	''' The moment the beat is hit.
	''' </summary>
	Public Structure Hit
		''' <summary>
		''' The moment of pressing.
		''' </summary>
		Public ReadOnly Property Beat As Beat
		''' <summary>
		''' The length of time player held it.
		''' </summary>
		Public ReadOnly Property Hold As Single
		''' <summary>
		''' The source event for this hit.
		''' </summary>
		Public ReadOnly Property Parent As BaseBeat
		''' <summary>
		''' Indicates whether this hit needs to be held down continuously.
		''' </summary>
		Public ReadOnly Property Holdable As Boolean
			Get
				Return Hold > 0
			End Get
		End Property
		''' <summary>
		''' Construct a hit.
		''' </summary>
		''' <param name="parent">The source event for this hit.</param>
		''' <param name="beat">The moment of pressing.</param>
		''' <param name="hold">The source event for this hit.</param>
		Public Sub New(parent As BaseBeat, beat As Beat, Optional hold As Single = 0)
			Me.Parent = parent
			Me.Beat = beat
			Me.Hold = hold
		End Sub
		Public Overrides Function ToString() As String
			Return $"{{{Beat}, {Parent}}}"
		End Function
	End Structure
	Public Structure Beat
		Implements IComparable(Of Beat)
		Implements IEquatable(Of Beat)
		Friend _calculator As BeatCalculator
		Private _isBeatLoaded As Boolean
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
		''' <summary>
		''' Whether this beat cannot be calculated.
		''' </summary>
		Public ReadOnly Property IsEmpty As Boolean
			Get
				Return _calculator Is Nothing OrElse Not (_isBeatLoaded OrElse _isBarBeatLoaded OrElse _isTimeSpanLoaded)
			End Get
		End Property
		''' <summary>
		''' The total number of beats from this moment to the beginning of the level.
		''' </summary>
		Public ReadOnly Property BeatOnly As Single
			Get
				IfNullThrowException()
				If Not _isBeatLoaded Then
					If _isBarBeatLoaded Then
						_beat = _calculator.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1
					ElseIf _isTimeSpanLoaded Then
						_beat = _calculator.TimeSpanToBeatOnly(_TimeSpan) - 1
					End If
					_isBeatLoaded = True
				End If
				Return _beat + 1
			End Get
		End Property
		''' <summary>
		''' The actual bar and beat of this moment.
		''' </summary>
		Public ReadOnly Property BarBeat As (bar As UInteger, beat As Single)
			Get
				IfNullThrowException()
				If Not _isBarBeatLoaded Then
					If _isBeatLoaded Then
						_BarBeat = _calculator.BeatOnlyToBarBeat(_beat + 1)
					ElseIf _isTimeSpanLoaded Then
						_beat = _calculator.TimeSpanToBeatOnly(_TimeSpan) - 1
						_isBeatLoaded = True
						_BarBeat = _calculator.BeatOnlyToBarBeat(_beat + 1)
					End If
					_isBarBeatLoaded = True
				End If
				Return _BarBeat
			End Get
		End Property
		''' <summary>
		''' The total amount of time from the beginning of the level to this beat.
		''' </summary>
		Public ReadOnly Property TimeSpan As TimeSpan
			Get
				IfNullThrowException()
				If Not _isTimeSpanLoaded Then
					If _isBeatLoaded Then
						_TimeSpan = _calculator.BeatOnlyToTimeSpan(_beat + 1)
					ElseIf _isBarBeatLoaded Then
						_beat = _calculator.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1
						_isBeatLoaded = True
						_TimeSpan = _calculator.BeatOnlyToTimeSpan(_beat + 1)
					End If
					_isTimeSpanLoaded = True
				End If
				Return _TimeSpan
			End Get
		End Property
		''' <summary>
		''' The number of beats per minute followed at this moment.
		''' </summary>
		Public ReadOnly Property BPM As Single
			Get
				If Not _isBPMLoaded Then
					_BPM = _calculator.BeatsPerMinuteOf(Me)
					_isBPMLoaded = True
				End If
				Return _BPM
			End Get
		End Property
		''' <summary>
		''' The number of beats per bar followed at this moment.
		''' </summary>
		Public ReadOnly Property CPB As Single
			Get
				If Not _isCPBLoaded Then
					_CPB = _calculator.CrotchetsPerBarOf(Me)
					_isCPBLoaded = True
				End If
				Return _CPB
			End Get
		End Property
		''' <summary>
		''' Construct an instance without specifying a calculator.
		''' </summary>
		''' <param name="beatOnly">The total number of beats from this moment to the beginning of the level.</param>
		Public Sub New(beatOnly As Single)
			If beatOnly < 1 Then
				Throw New OverflowException($"The beat must not be less than 1, but {beatOnly} is given")
			End If
			_beat = beatOnly - 1
			_isBeatLoaded = True
		End Sub
		Public Sub New(bar As UInteger, beat As Single)
			If bar < 1 Then
				Throw New OverflowException($"The bar must not be less than 1, but {bar} is given")
			End If
			If beat < 1 Then
				Throw New OverflowException($"The beat must not be less than 1, but {beat} is given")
			End If
			_BarBeat = (bar, beat)
			_isBarBeatLoaded = True
		End Sub
		Public Sub New(timeSpan As TimeSpan)
			If timeSpan < TimeSpan.Zero Then
				Throw New OverflowException($"The time must not be less than zero, but {timeSpan} is given")
			End If
			_TimeSpan = timeSpan
			_isTimeSpanLoaded = True
		End Sub
		''' <summary>
		''' Construct an instance with specifying a calculator.
		''' </summary>
		''' <param name="calculator">Specified calculator.</param>
		''' <param name="beatOnly">The total number of beats from this moment to the beginning of the level.</param>
		Public Sub New(calculator As BeatCalculator, beatOnly As Single)
			Me.New(beatOnly)
			_calculator = calculator
		End Sub
		''' <summary>
		''' Construct an instance with specifying a calculator.
		''' </summary>
		''' <param name="calculator">Specified calculator.</param>
		''' <param name="bar">The actual bar of this moment.</param>
		''' <param name="beat">The actual beat of this moment.</param>
		Public Sub New(calculator As BeatCalculator, bar As UInteger, beat As Single)
			Me.New(bar, beat)
			_calculator = calculator
			_beat = _calculator.BarBeatToBeatOnly(bar, beat) - 1
		End Sub
		''' <summary>
		''' Construct an instance with specifying a calculator.
		''' </summary>
		''' <param name="calculator">Specified calculator.</param>
		''' <param name="timeSpan">The total amount of time from the start of the level to the moment</param>
		Public Sub New(calculator As BeatCalculator, timeSpan As TimeSpan)
			Me.New(timeSpan)
			_calculator = calculator
			_beat = _calculator.TimeSpanToBeatOnly(timeSpan) - 1
		End Sub
		''' <summary>
		''' Construct an instance with specifying a calculator.
		''' </summary>
		''' <param name="calculator">Specified calculator.</param>
		''' <param name="beat">Another instance.</param>
		Public Sub New(calculator As BeatCalculator, beat As Beat)
			If beat._isBeatLoaded Then
				If beat._beat < 1 Then
					Throw New OverflowException($"The beat must not be less than 1, but {beat._beat} is given")
				End If
				_beat = beat._beat
				_isBeatLoaded = True
				_calculator = calculator
			ElseIf beat._isBarBeatLoaded Then
				If beat._BarBeat.Bar < 1 Then
					Throw New OverflowException($"The bar must not be less than 1, but {beat._BarBeat.Bar} is given")
				End If
				If beat._BarBeat.Beat < 1 Then
					Throw New OverflowException($"The beat must not be less than 1, but {beat._BarBeat.Beat} is given")
				End If
				_BarBeat = beat._BarBeat
				_isBarBeatLoaded = True
				_calculator = calculator
				_beat = _calculator.BarBeatToBeatOnly(beat._BarBeat.Bar, beat._BarBeat.Beat) - 1
			ElseIf beat._isTimeSpanLoaded Then
				If beat._TimeSpan < TimeSpan.Zero Then
					Throw New OverflowException($"The time must not be less than zero, but {beat._TimeSpan} is given")
				End If
				_TimeSpan = beat._TimeSpan
				_isTimeSpanLoaded = True
				_calculator = calculator
				_beat = _calculator.TimeSpanToBeatOnly(TimeSpan) - 1
			End If
		End Sub
		''' <summary>
		''' Construct a beat of the 1st beat from the calculator
		''' </summary>
		''' <param name="calculator">Specified calculator.</param>
		''' <returns>The first beat tied to the level.</returns>
		Public Shared Function [Default](calculator As BeatCalculator) As Beat
			Return New Beat(calculator, 1)
		End Function
		''' <summary>
		''' Determine if two beats come from the same level
		''' </summary>
		''' <param name="a">A beat.</param>
		''' <param name="b">Another beat.</param>
		''' <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		''' <returns></returns>
		Public Shared Function FromSameLevel(a As Beat, b As Beat, Optional [throw] As Boolean = False) As Boolean
			If a.baseLevel.Equals(b.baseLevel) Then
				Return True
			Else
				If [throw] Then
					Throw New RhythmBaseException("Beats must come from the same RDLevel.")
				End If
				Return False
			End If
		End Function
		''' <summary>
		''' Determine if two beats are from the same level.
		''' <br/>
		''' If any of them does not come from any level, it will also return true.
		''' </summary>
		''' <param name="a">A beat.</param>
		''' <param name="b">Another beat.</param>
		''' <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		''' <returns></returns>
		Public Shared Function FromSameLevelOrNull(a As Beat, b As Beat, Optional [throw] As Boolean = False) As Boolean
			Return a.baseLevel Is Nothing OrElse b.baseLevel Is Nothing OrElse FromSameLevel(a, b, [throw])
		End Function
		Public Function FromSameLevel(b As Beat, Optional [throw] As Boolean = False) As Boolean
			Return FromSameLevel(Me, b, [throw])
		End Function
		''' <summary>
		''' Determine if two beats are from the same level.
		''' <br/>
		''' If any of them does not come from any level, it will also return true.
		''' </summary>
		''' <param name="b">Another beat.</param>
		''' <param name="throw">If true, an exception will be thrown when two beats do not come from the same level.</param>
		''' <returns></returns>	
		Public Function FromSameLevelOrNull(b As Beat, Optional [throw] As Boolean = False) As Boolean
			Return baseLevel Is Nothing OrElse b.baseLevel Is Nothing OrElse FromSameLevel(b, [throw])
		End Function
		''' <summary>
		''' Returns a new instance of unbinding the level.
		''' </summary>
		''' <returns>A new instance of unbinding the level.</returns>
		Public Function WithoutBinding() As Beat
			Dim result = Me
			If result._calculator IsNot Nothing Then
				result.Cache()
			End If
			result._calculator = Nothing
			Return result
		End Function
		Private Sub IfNullThrowException()
			If IsEmpty Then
				Throw New InvalidRDBeatException
			End If
		End Sub
		''' <summary>
		''' Refresh the cache.
		''' </summary>
		Public Sub ResetCache()
			Dim __ As Object = BeatOnly
			_isBarBeatLoaded = False
			_isTimeSpanLoaded = False
		End Sub
		Public Sub Cache()
			IfNullThrowException()
			Dim __ As Object
			__ = BeatOnly
			__ = BarBeat
			__ = TimeSpan
		End Sub
		''' <summary>
		''' 
		''' </summary>
		Friend Sub ResetBPM()
			If Not _isBeatLoaded Then
				_beat = _calculator.TimeSpanToBeatOnly(_TimeSpan) - 1
			End If
			_isBeatLoaded = True
			_isTimeSpanLoaded = False
			_isBPMLoaded = False
		End Sub
		Friend Sub ResetCPB()
			If Not _isBeatLoaded Then
				_beat = _calculator.BarBeatToBeatOnly(_BarBeat.Bar, _BarBeat.Beat) - 1
			End If
			_isBeatLoaded = True
			_isBarBeatLoaded = False
			_isCPBLoaded = False
		End Sub
		Public Shared Operator +(a As Beat, b As Single) As Beat
			If Not a.IsEmpty Then
				Return New Beat(a._calculator, a.BeatOnly + b)
			ElseIf a._isBeatLoaded Then
				Return New Beat(a._calculator, a._beat + b)
			End If
			Throw New ArgumentNullException(NameOf(a), "The beat cannot be calculate.")
		End Operator
		Public Shared Operator +(a As Beat, b As TimeSpan) As Beat
			If Not a.IsEmpty Then
				Return New Beat(a._calculator, a.TimeSpan + b)
			ElseIf a._isBeatLoaded Then
				Return New Beat(a._calculator, a._TimeSpan + b)
			End If
			Throw New ArgumentNullException(NameOf(a), "The beat cannot be calculate.")
		End Operator
		Public Shared Operator -(a As Beat, b As Single) As Beat
			If Not a.IsEmpty Then
				Return New Beat(a._calculator, a.BeatOnly - b)
			ElseIf a._isBeatLoaded Then
				Return New Beat(a._calculator, a._beat - b)
			End If
			Throw New ArgumentNullException(NameOf(a), "The beat cannot be calculate.")
		End Operator
		Public Shared Operator -(a As Beat, b As TimeSpan) As Beat
			If Not a.IsEmpty Then
				Return New Beat(a._calculator, a.TimeSpan - b)
			ElseIf a._isBeatLoaded Then
				Return New Beat(a._calculator, a._TimeSpan - b)
			End If
			Throw New ArgumentNullException(NameOf(a), "The beat cannot be calculate.")
		End Operator
		Public Shared Operator >(a As Beat, b As Beat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso a.BeatOnly > b.BeatOnly
		End Operator
		Public Shared Operator <(a As Beat, b As Beat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso a.BeatOnly < b.BeatOnly
		End Operator
		Public Shared Operator >=(a As Beat, b As Beat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso a.BeatOnly >= b.BeatOnly
		End Operator
		Public Shared Operator <=(a As Beat, b As Beat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso a.BeatOnly <= b.BeatOnly
		End Operator
		Public Shared Operator =(a As Beat, b As Beat) As Boolean
			Return FromSameLevel(a, b, True) AndAlso
						(a._beat = b._beat) OrElse
						(a._isBarBeatLoaded AndAlso b._isBarBeatLoaded AndAlso a._BarBeat.Bar = b._BarBeat.Bar AndAlso a._BarBeat.Beat = b._BarBeat.Beat) OrElse
						(a._isTimeSpanLoaded AndAlso b._isTimeSpanLoaded AndAlso a._TimeSpan = b._TimeSpan) OrElse
						(a.BeatOnly = b.BeatOnly)
		End Operator
		Public Shared Operator <>(a As Beat, b As Beat) As Boolean
			Return Not a = b
		End Operator
		Public Overrides Function ToString() As String
			If IsEmpty Then
				Return $"[{If(_isBeatLoaded, _beat.ToString, "?")},{If(_isBarBeatLoaded, _BarBeat.ToString, "?")},{If(_isTimeSpanLoaded, _TimeSpan.ToString, "?")}]"
			End If
			Return $"[{BarBeat.bar},{BarBeat.beat}]"
		End Function
		Public Overrides Function Equals(<CodeAnalysis.NotNull> obj As Object) As Boolean
			Return obj.GetType = GetType(Beat) AndAlso Equals(CType(obj, Beat))
		End Function
		Public Overloads Function Equals(other As Beat) As Boolean Implements IEquatable(Of Beat).Equals
			Return Me = other
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(Me.BeatOnly, Me.baseLevel)
		End Function
		Public Function CompareTo(other As Beat) As Integer Implements IComparable(Of Beat).CompareTo
			Dim result = BeatOnly - other.BeatOnly
			If result > 0 Then
				Return 1
			ElseIf result < 0 Then
				Return -1
			End If
			Return 0
		End Function
	End Structure

	''' <summary>
	''' Beat range.
	''' </summary>
	Public Structure RDRange
		''' <summary>
		''' Start beat.
		''' </summary>
		Public ReadOnly Property Start As Beat?
		''' <summary>
		''' End beat.
		''' </summary>
		Public ReadOnly Property [End] As Beat?
		''' <summary>
		''' Beat interval.
		''' </summary>
		Public ReadOnly Property BeatInterval As Single
			Get
				If Start.HasValue AndAlso [End].HasValue Then
					Return [End].Value.BeatOnly - Start.Value.BeatOnly
				End If
				Return Single.PositiveInfinity
			End Get
		End Property
		''' <summary>
		''' Time interval.
		''' </summary>
		''' <returns></returns>
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
		''' <param name="start">Start beat.</param>
		''' <param name="[end]">End beat.</param>
		Public Sub New(start As Beat?, [end] As Beat?)
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
#If DEBUG Then
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
#End If
	''' <summary>
	''' Palette color
	''' </summary>
	Public Class PaletteColor
		Private _panel As Integer
		Private _color As SKColor?
		Friend parent As LimitedList(Of SKColor)
		''' <summary>
		''' Get or set a custom color.
		''' </summary>
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
		''' <summary>
		''' Go back to or set the palette color index.
		''' </summary>
		Public Property PaletteIndex As Integer
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
		''' <summary>
		''' Specifies whether this object supports alpha channel.
		''' </summary>
		''' <returns></returns>
		Public ReadOnly Property EnableAlpha As Boolean
		''' <summary>
		''' Specifies whether this object is used for this color.
		''' </summary>
		Public ReadOnly Property EnablePanel As Boolean
			Get
				Return PaletteIndex >= 0
			End Get
		End Property
		''' <summary>
		''' The actual color of this object.<br/>
		''' If comes from a palette, it's a palette color.
		''' If not, it's a custom color.
		''' </summary>
		Public ReadOnly Property Value As SKColor
			Get
				Return If(EnablePanel, parent(_panel), _color)
			End Get
		End Property
		''' <param name="enableAlpha">Specifies whether this object supports alpha channel.</param>
		Public Sub New(enableAlpha As Boolean)
			Me.EnableAlpha = enableAlpha
		End Sub
		Public Overrides Function ToString() As String
			Return If(EnablePanel, $"{_panel}: {Value}", Value.ToString)
		End Function
	End Class
	''' <summary>
	''' Can only be applied to one room.
	''' </summary>
	<JsonConverter(GetType(RoomConverter))> Public Structure SingleRoom
		Private _data As RoomIndex
		''' <summary>
		''' Whether it can be used in the top room.
		''' </summary>
		Public ReadOnly Property EnableTop As Boolean
		''' <summary>
		''' Applied rooms.
		''' </summary>
		Public Property Room As RoomIndex
			Get
				Return _data
			End Get
			Set(value As RoomIndex)
				_data = value
			End Set
		End Property
		''' <summary>
		''' Applied room indexes.
		''' </summary>
		Public Property Value As Byte
			Get
				For i = 0 To 4
					If _data = [Enum].Parse(Of RoomIndex)(1 << i) Then
						Return i
					End If
				Next
				Return Byte.MaxValue
			End Get
			Set(value As Byte)
				_data = 1 << value
			End Set
		End Property
		Public Overrides Function ToString() As String
			Return $"[{_data}]"
		End Function
		''' <summary>
		''' Represents room 0.
		''' </summary>
		Public Shared ReadOnly Property [Default] As SingleRoom
			Get
				Return New SingleRoom() With {._data = Byte.MaxValue}
			End Get
		End Property
		Public Sub New(room As Byte)
			_data = 1 << room
		End Sub
		Public Shared Operator =(R1 As SingleRoom, R2 As SingleRoom) As Boolean
			Return R1._data = R2._data
		End Operator
		Public Shared Operator <>(R1 As SingleRoom, R2 As SingleRoom) As Boolean
			Return Not R1 = R2
		End Operator
		Public Overrides Function Equals(obj As Object) As Boolean
			Return Me = obj
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(_data)
		End Function
	End Structure
	''' <summary>
	''' Indicates that can be applied to multiple rooms.
	''' </summary>
	<JsonConverter(GetType(RoomConverter))>
	Public Structure Room
		Private _data As RoomIndex
		''' <summary>
		''' Can be applied to top room.
		''' </summary>
		Public ReadOnly Property EnableTop As Boolean
		''' <summary>
		''' Whether the specified room is enabled.
		''' </summary>
		''' <param name="Index"></param>
		Default Public Property Room(Index As Byte) As Boolean
			Get
				Return _data.HasFlag([Enum].Parse(Of RoomIndex)(1 << Index))
			End Get
			Set(value As Boolean)
				If Index = 4 And Not EnableTop Then
					Exit Property
				End If
				_data = If(value, _data Or 1 << Index, _data And 1 << Index)
			End Set
		End Property
		''' <summary>
		''' List of enabled rooms.
		''' </summary>
		Public ReadOnly Property Rooms As List(Of Byte)
			Get
				Dim L As New List(Of Byte)
				For i = 0 To 4
					If _data.HasFlag([Enum].Parse(Of RoomIndex)(1 << i)) Then
						L.Add(i)
					End If
				Next
				Return L
			End Get
		End Property
		Public Overrides Function ToString() As String
			Return $"[{String.Join(",", Rooms)}]"
		End Function
		''' <summary>
		''' Returns an instance with only room 1 enabled.
		''' </summary>
		''' <returns>An instance with only room 1 enabled.</returns>
		Public Shared Function [Default]() As Room
			Return New Room(False, Array.Empty(Of Byte)) With {
						._data = RoomIndex.Room1
					}
		End Function
		Public Sub New(enableTop As Boolean)
			Me.EnableTop = enableTop
		End Sub
		Public Sub New(enableTop As Boolean, ParamArray rooms() As Byte)
			Me.EnableTop = enableTop
			Select Case rooms.Length
				Case 0
					_data = RoomIndex.RoomNotAvaliable
					Exit Sub
				Case 1
					Room(rooms.Single) = True
				Case Else
					For Each item In rooms
						Room(item) = True
					Next
			End Select
			'If enableTop <> Me.EnableTop Then
			'	Throw New RhythmBaseException("Parameters are not match.")
			'End If
		End Sub
		''' <summary>
		''' Check if the room is included.
		''' </summary>
		''' <param name="rooms">Rooms inspected.</param>
		''' <returns></returns>
		Public Function Contains(rooms As Room) As Boolean
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
		Public Shared Operator =(R1 As Room, R2 As Room) As Boolean
			Return R1._data = R2._data
		End Operator
		Public Shared Operator <>(R1 As Room, R2 As Room) As Boolean
			Return Not R1 = R2
		End Operator
		Public Shared Widening Operator CType(room As SingleRoom) As Room
			Return New Room(room.EnableTop, False, room.Room)
		End Operator
		Public Shared Narrowing Operator CType(room As Room) As SingleRoom
			If room.Rooms.Count = 1 Then
				Return New SingleRoom(room.Rooms.Single)
			End If
		End Operator
		Public Overrides Function Equals(obj As Object) As Boolean
			Return Me = obj
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(_data)
		End Function
	End Structure
	'''' <summary>
	'''' Audio.
	'''' </summary>
	'Public NotInheritable Class RDAudio
	'	''' <summary>
	'	''' File name.
	'	''' </summary>
	'	Public Property Filename As String
	'	''' <summary>
	'	''' Audio volume.
	'	''' </summary>
	'	<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> <DefaultValue(100)> Public Property Volume As Integer = 100
	'	''' <summary>
	'	''' Audio Pitch.
	'	''' </summary>
	'	<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> <DefaultValue(100)> Public Property Pitch As Integer = 100
	'	''' <summary>
	'	''' Audio Pan.
	'	''' </summary>
	'	<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> Public Property Pan As Integer = 0
	'	''' <summary>
	'	''' Audio Offset.
	'	''' </summary>
	'	<JsonProperty(DefaultValueHandling:=DefaultValueHandling.IgnoreAndPopulate)> <JsonConverter(GetType(TimeConverter))> Public Property Offset As TimeSpan = TimeSpan.Zero
	'	Public Overrides Function ToString() As String
	'		Return Filename
	'	End Function
	'End Class
	''' <summary>
	''' A list that limits the capacity of the list and uses the default value to fill the free capacity.
	''' </summary>
	<JsonConverter(GetType(Converters.LimitedListConverter))> Public Class LimitedList
		Implements ICollection
		Protected ReadOnly list As List(Of (value As Object, isDefault As Boolean))
		''' <summary>
		''' Default value.
		''' </summary>
		<JsonIgnore> Protected Friend Property DefaultValue As Object
		Public ReadOnly Property Count As Integer Implements ICollection.Count
			Get
				Return list.Count
			End Get
		End Property
		Public ReadOnly Property IsSynchronized As Boolean = False Implements ICollection.IsSynchronized
		Public ReadOnly Property SyncRoot As Object = Nothing Implements ICollection.SyncRoot
		''' <param name="count">Capacity limit.</param>
		''' <param name="defaultValue">Default value.</param>
		Public Sub New(count As UInteger, defaultValue As Object)
			list = New List(Of (value As Object, isDefault As Boolean))(count)
			For i = 0 To count - 1
				list.Add((GetDefaultValue(), True))
			Next
			Me.DefaultValue = defaultValue
		End Sub
		''' <param name="count">Capacity limit.</param>
		Public Sub New(count As UInteger)
			Me.New(count, Nothing)
		End Sub
		Protected Friend Sub Add(item As Object)
			Dim index = list.IndexOf(list.FirstOrDefault(Function(i) i.isDefault = True))
			If index >= 0 Then
				list(index) = (item, False)
			End If
		End Sub
		''' <summary>
		''' Remove all items.
		''' </summary>
		Public Sub Clear()
			For i = 0 To list.Count - 1
				list(i) = Nothing
			Next
		End Sub
		''' <summary>
		''' Remove item.
		''' </summary>
		''' <param name="index">Item index.</param>
		Protected Friend Sub RemoveAt(index As UInteger)
			If index >= list.Count Then
				Throw New IndexOutOfRangeException
			End If
			list(index) = Nothing
		End Sub
		''' <summary>
		''' Get the default value.
		''' </summary>
		''' <returns>The default value.</returns>
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
	''' <summary>
	''' A list that limits the capacity of the list and uses the default value to fill the free capacity.
	''' </summary>
	Public Class LimitedList(Of T)
		Inherits LimitedList
		Implements ICollection(Of T)
		''' <summary>
		''' Default value
		''' </summary>
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
					Dim ValueCloned = DefaultValue
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
		''' <param name="count">Capacity limit.</param>
		''' <param name="defaultValue">Default value.</param>
		Public Sub New(count As UInteger, defaultValue As T)
			MyBase.New(count, defaultValue)
		End Sub
		''' <param name="count">Capacity limit.</param>
		Public Sub New(count As UInteger)
			Me.New(count, Nothing)
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
	Friend Class TypedEventCollection(Of T As BaseEvent)
		Implements IEnumerable(Of T)

		Private ReadOnly list As New List(Of T)
		Protected Friend _types As New HashSet(Of EventType)
		Public Overloads Sub Add(item As T)
			list.Add(item)
			_types.Add(item.Type)
		End Sub
		Public Overloads Function Remove(item As T)
			_types.Remove(item.Type)
			Return list.Remove(item)
		End Function
		Public Function BeforeThan(item1 As BaseEvent, item2 As BaseEvent)
			Return list.IndexOf(item1) < list.IndexOf(item2)
		End Function
		Public Overrides Function ToString() As String
			Return $"{If(_types.Contains(EventType.SetBeatsPerMinute) OrElse _types.Contains(EventType.PlaySong),
							"BPM, ", If(_types.Contains(EventType.SetCrotchetsPerBar),
							"CPB, ", ""))}Count={list.Count}"
		End Function
		Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
			Return list.GetEnumerator
		End Function
		Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return list.GetEnumerator
		End Function
	End Class

	''' <summary>
	''' A collection of events that maintains the sequence of events.
	''' </summary>
	Public MustInherit Class OrderedEventCollection
		Implements ICollection(Of BaseEvent)
		Friend eventsBeatOrder As New SortedDictionary(Of Beat, TypedEventCollection(Of BaseEvent))
		<JsonIgnore> Public Overridable ReadOnly Property Count As Integer Implements ICollection(Of BaseEvent).Count
			Get
				Return Me.eventsBeatOrder.Sum(Function(i) i.Value.Count)
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of BaseEvent).IsReadOnly
		''' <summary>
		''' Returns the beat of the last event.
		''' </summary>
		''' <returns>The beat of the last event.</returns>
		<JsonIgnore> Public ReadOnly Property Length As Beat
			Get
				Return Me.eventsBeatOrder.LastOrDefault().Value.First.Beat
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
			Return Me.eventsBeatOrder.SelectMany(Function(i) i.Value).ToList
		End Function
		Public Sub Add(item As BaseEvent) Implements ICollection(Of BaseEvent).Add
			Dim value As TypedEventCollection(Of BaseEvent) = Nothing

			If Not eventsBeatOrder.TryGetValue(item.Beat, value) Then
				value = New TypedEventCollection(Of BaseEvent)
				eventsBeatOrder.Add(item.Beat, value)
			End If

			value.Add(item)
		End Sub
		Public Sub Clear() Implements ICollection(Of BaseEvent).Clear
			'EventsTypeOrder.Clear()
			eventsBeatOrder.Clear()
		End Sub
		Public Overridable Function Contains(item As BaseEvent) As Boolean Implements ICollection(Of BaseEvent).Contains
			'            Return EventsTypeOrder.ContainsKey(item.Type) AndAlso
			'EventsTypeOrder(item.Type).ContainsKey(item.Beat.BeatOnly) AndAlso
			'EventsTypeOrder(item.Type)(item.Beat.BeatOnly).Contains(item)
			Return eventsBeatOrder.ContainsKey(item.Beat) AndAlso eventsBeatOrder(item.Beat).Contains(item)
		End Function
		Public Sub CopyTo(array() As BaseEvent, arrayIndex As Integer) Implements ICollection(Of BaseEvent).CopyTo
			ConcatAll.CopyTo(array, arrayIndex)
		End Sub
		Friend Function Remove(item As BaseEvent) As Boolean Implements ICollection(Of BaseEvent).Remove
			If Contains(item) Then
				Dim result = Me.eventsBeatOrder(item.Beat).Remove(item)
				If Not eventsBeatOrder(item.Beat).Any() Then
					eventsBeatOrder.Remove(item.Beat)
				End If
				Return result
			End If
			Return False
		End Function
		Public Iterator Function GetEnumerator() As IEnumerator(Of BaseEvent) Implements IEnumerable(Of BaseEvent).GetEnumerator
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
	''' <summary>
	''' Subtypes of sound effects.
	''' </summary>
	Public Class SoundSubType
		''' <summary>
		''' Types of sound effects.
		''' </summary>
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
		''' <summary>
		''' Referenced audio.
		''' </summary>
		Private Property Audio As New Audio
		''' <summary>
		''' Sound effect name.
		''' </summary>
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
		<JsonConverter(GetType(TimeConverter))> Public Property Offset As TimeSpan
			Get
				Return Audio.Offset
			End Get
			Set(value As TimeSpan)
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
	''' <summary>
	''' The conditions of the event.
	''' </summary>
	Public Class Condition
		''' <summary>
		''' Condition list.
		''' </summary>
		Public Property ConditionLists As New List(Of (Enabled As Boolean, Conditional As BaseConditional))
		''' <summary>
		''' The time of effectiveness of the condition.
		''' </summary>
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
				Throw New RhythmBaseException($"Illegal condition: {text}.")
			End If
		End Function
		''' <summary>
		''' Converting conditions to strings
		''' </summary>
		''' <returns>A string in the format supported by RDLevel.</returns>
		Public Function Serialize() As String
			Return String.Join("&", ConditionLists.Select(Of String)(Function(i) If(i.Enabled, "", "~") + i.Conditional.Id.ToString)) + "d" + Duration.ToString
		End Function
		Public Overrides Function ToString() As String
			Return Serialize()
		End Function
	End Class
	''' <summary>
	''' A decoration.
	''' </summary>
	<JsonObject> Public Class DecorationEventCollection
		Inherits OrderedEventCollection(Of BaseDecorationAction)
		Private _id As String
		<JsonIgnore> Friend Parent As RDLevel
		Private _file As Sprite
		''' <summary>
		''' Decorated ID.
		''' </summary>
		<JsonProperty("id")> Public Property Id As String
			Get
				Return _id
			End Get
			Set(value As String)
				_id = value
			End Set
		End Property
		''' <summary>
		''' Decoration size.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property Size As SKSizeI
			Get
				Return If(File?.Size, New SKSizeI(32, 31))
			End Get
		End Property
		''' <summary>
		''' Decoration expression name list. Empty list if it is an image file.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property Expressions As IEnumerable(Of String)
			Get
				Return If(File?.Expressions, New List(Of String))
			End Get
		End Property
		''' <summary>
		''' Decoration index.
		''' </summary>
		<JsonProperty("row")> Public ReadOnly Property Index As ULong
			Get
				Return Parent.Decorations.ToList.IndexOf(Me)
			End Get
		End Property
		<JsonProperty("rooms")> Public Property Room As New SingleRoom(0)
		''' <summary>
		''' The file reference used by the decoration.
		''' </summary>
		<JsonProperty("filename")> Public Property File As Sprite
			Get
				Return _file
			End Get
			Set(value As Sprite)
				_file = value
				Parent?.Assets.Add(value)
			End Set
		End Property
		''' <summary>
		''' Decoration depth.
		''' </summary>
		Public Property Depth As Integer
		''' <summary>
		''' The filter used for this decoration.
		''' </summary>
		Public Property Filter As Filters
		''' <summary>
		''' The initial visibility of this decoration.
		''' </summary>
		Public Property Visible As Boolean
		Sub New()
		End Sub
		''' <param name="room">Decoration room.</param>
		Friend Sub New(room As SingleRoom)
			Me.Room = room
			Me._id = Me.GetHashCode
		End Sub
		''' <summary>
		''' Add an event to decoration.
		''' </summary>
		''' <param name="item">Decoration event.</param>
		Public Overrides Sub Add(item As BaseDecorationAction)
			item._parent = Me
			Parent.Add(item)
		End Sub
		Friend Sub AddSafely(item As BaseDecorationAction)
			MyBase.Add(item)
		End Sub
		''' <summary>
		''' Remove an event from decoration.
		''' </summary>
		''' <param name="item">A decoration event.</param>
		Public Overrides Function Remove(item As BaseDecorationAction) As Boolean
			Return Parent.Remove(item)
		End Function
		Friend Function RemoveSafely(item As BaseDecorationAction) As Boolean
			Return MyBase.Remove(item)
		End Function
		Public Overrides Function ToString() As String
			Return $"{_id}, {Index}, {_Room}, {File.FileName}"
		End Function
		Friend Function Clone() As DecorationEventCollection
			Return Me.MemberwiseClone
		End Function
	End Class
	''' <summary>
	''' A row.
	''' </summary>
	<JsonObject> Public Class RowEventCollection
		Inherits OrderedEventCollection(Of BaseRowAction)
		''' <summary>
		''' Player mode.
		''' </summary>
		Public Enum PlayerMode
			P1
			P2
			CPU
		End Enum
		Private _rowType As RowType
		<JsonIgnore> Friend Parent As RDLevel
		Public Property Character As RDCharacter
		''' <summary>
		''' Row type.
		''' </summary>
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
		''' <summary>
		''' Decoration index.
		''' </summary>
		<JsonProperty("row", DefaultValueHandling:=DefaultValueHandling.Include)> Public ReadOnly Property Index As SByte
			Get
				Return Parent._Rows.IndexOf(Me)
			End Get
		End Property
		Public Property Rooms As New SingleRoom(0)
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property HideAtStart As Boolean
		''' <summary>
		''' Initial play mode for this row.
		''' </summary>
		Public Property Player As PlayerMode
		''' <summary>
		''' Initial beat sound for this row.
		''' </summary>
		<JsonIgnore> Public Property Sound As New Audio
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property MuteBeats As Boolean
		''' <summary>
		''' Mirroring of the row.
		''' </summary>
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property RowToMimic As SByte = -1
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
		<JsonConverter(GetType(TimeConverter))> Public Property PulseSoundOffset As TimeSpan
			Get
				Return Sound.Offset
			End Get
			Set(value As TimeSpan)
				Sound.Offset = value
			End Set
		End Property
		Friend Sub New()
		End Sub
		''' <summary>
		''' Add an item to row.
		''' </summary>
		''' <param name="item">Row event.</param>
		Public Overrides Sub Add(item As BaseRowAction)
			item._parent = Me
			Parent.Add(item)
		End Sub
		Friend Sub AddSafely(item As BaseRowAction)
			MyBase.Add(item)
		End Sub
		''' <summary>
		''' Remove an item from row.
		''' </summary>
		''' <param name="item">Row event.</param>
		Public Overrides Function Remove(item As BaseRowAction) As Boolean
			Return Parent.Remove(item)
		End Function
		Friend Function RemoveSafely(item As BaseRowAction) As Boolean
			Return MyBase.Remove(item)
		End Function
	End Class
	''' <summary>
	''' Bookmark.
	''' </summary>
	Public Class Bookmark
		''' <summary>
		''' Colors available for bookmarks.
		''' </summary>
		Enum BookmarkColors
			Blue
			Red
			Yellow
			Green
		End Enum
		''' <summary>
		''' The beat where the bookmark is located.
		''' </summary>
		Public Property Beat As Beat
		''' <summary>
		''' Color on bookmark.
		''' </summary>
		Public Property Color As BookmarkColors
		Public Overrides Function ToString() As String
			Return $"{Beat}, {Color}"
		End Function
	End Class
	''' <summary>
	''' Condition.
	''' </summary>
	Public MustInherit Class BaseConditional
		''' <summary>
		''' Type of condition
		''' </summary>
		Public Enum ConditionType
			LastHit
			Custom
			TimesExecuted
			Language
			PlayerMode
		End Enum
		<JsonIgnore> Friend ParentCollection As List(Of BaseConditional)
		''' <summary>
		''' Type of this condition
		''' </summary>
		Public MustOverride ReadOnly Property Type As ConditionType
		''' <summary>
		''' Condition tag. Its role has not been clarified.
		''' </summary>
		Public Property Tag As String
		''' <summary>
		''' Condition name.
		''' </summary>
		Public Property Name As String
		''' <summary>
		''' 1-based serial number.
		''' </summary>
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
		''' <summary>
		''' Last hit.
		''' </summary>
		Public Class LastHit
			Inherits BaseConditional
			<Flags> Enum HitResult
				Perfect = &B0
				SlightlyEarly = &B10
				SlightlyLate = &B11
				VeryEarly = &B100
				VeryLate = &B101
				AnyEarlyOrLate = &B111
				Missed = &B1111
			End Enum
			Public Overrides ReadOnly Property Type As ConditionType = ConditionType.LastHit
			''' <summary>
			''' The row.
			''' </summary>
			Public Property Row As SByte
			''' <summary>
			''' determines under what result the event will be executed.
			''' </summary>
			Public Property Result As HitResult
		End Class
		''' <summary>
		''' Expression condition.
		''' </summary>
		Public Class Custom
			Inherits BaseConditional
			''' <summary>
			''' Expression.
			''' </summary>
			''' <returns></returns>
			Public Property Expression As String
			Public Overrides ReadOnly Property Type As ConditionType = ConditionType.Custom
		End Class
		''' <summary>
		''' Number of executions.
		''' </summary>
		Public Class TimesExecuted
			Inherits BaseConditional
			''' <summary>
			''' Maximum number of executions.
			''' </summary>
			Public Property MaxTimes As Integer
			Public Overrides ReadOnly Property Type As ConditionType = ConditionType.TimesExecuted
		End Class
		''' <summary>
		''' Game Language.
		''' </summary>
		Public Class Language
			Inherits BaseConditional
			''' <summary>
			''' Game Language.
			''' </summary>
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
			''' <summary>
			''' Game Language.
			''' </summary>
			Public Property Language As Languages
			Public Overrides ReadOnly Property Type As ConditionType = ConditionType.Language
		End Class
		''' <summary>
		''' Player mode.
		''' </summary>
		Public Class PlayerMode
			Inherits BaseConditional
			''' <summary>
			''' Enable two-player mode.
			''' </summary>
			Public Property TwoPlayerMode As Boolean
			Public Overrides ReadOnly Property Type As ConditionType = ConditionType.PlayerMode
		End Class
	End Namespace
	''' <summary>
	''' Rhythm Doctor level.
	''' </summary>
	Public Class RDLevel
		Inherits OrderedEventCollection(Of BaseEvent)
		Friend _path As String
		''' <summary>
		''' Asset collection.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property Assets As New HashSet(Of Sprite)
		''' <summary>
		''' Variables.
		''' </summary>
		<JsonIgnore> Public ReadOnly Variables As New Variables
		''' <summary>
		''' The calculator that comes with the level.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property Calculator As New BeatCalculator(Me)
		''' <summary>
		''' Level Settings.
		''' </summary>
		Public Property Settings As New Settings
		Friend ReadOnly Property _Rows As New List(Of RowEventCollection)(16)
		Friend ReadOnly Property _Decorations As New List(Of DecorationEventCollection)
		''' <summary>
		''' Level tile collection.
		''' </summary>
		Public ReadOnly Property Rows As IReadOnlyCollection(Of RowEventCollection)
			Get
				Return _Rows.AsReadOnly
			End Get
		End Property
		''' <summary>
		''' Level decoration collection.
		''' </summary>
		Public ReadOnly Property Decorations As IReadOnlyCollection(Of DecorationEventCollection)
			Get
				Return _Decorations.AsReadOnly
			End Get
		End Property
		''' <summary>
		''' Level condition collection.
		''' </summary>
		Public ReadOnly Property Conditionals As New List(Of BaseConditional)
		''' <summary>
		''' Level bookmark collection.
		''' </summary>
		Public ReadOnly Property Bookmarks As New List(Of Bookmark)
		''' <summary>
		''' Level colorPalette collection.
		''' </summary>
		Public ReadOnly Property ColorPalette As New LimitedList(Of SKColor)(21, New SKColor(&HFF, &HFF, &HFF, &HFF))
		''' <summary>
		''' Level file path.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property Path As String
			Get
				Return _path
			End Get
		End Property
		''' <summary>
		''' Level directory path.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property Directory As String
			Get
				Return IO.Path.GetDirectoryName(_path)
			End Get
		End Property
		''' <summary>
		''' Default beats with levels.
		''' The beat is 1.
		''' </summary>
		<JsonIgnore> Public ReadOnly Property DefaultBeat As Beat
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
		''' <summary>
		''' The default level within the game.
		''' </summary>
		Public Shared ReadOnly Property [Default] As RDLevel
			Get
				Dim rdlevel As New RDLevel From {
							New PlaySong With {
								.Song = New Audio With {.Filename = "sndOrientalTechno"}},
							New SetTheme With {
								.Preset = SetTheme.Theme.OrientalTechno}
						}
				Dim samurai = rdlevel.CreateRow(
							New SingleRoom(0), New RDCharacter(Characters.Samurai)
							)
				samurai.PulseSound = "Shaker"
				samurai.Add(New AddClassicBeat)
				Return rdlevel
			End Get
		End Property
		''' <summary>
		''' Create a decoration and add it to the level.
		''' </summary>
		''' <param name="room">The room where this decoration is in.</param>
		''' <param name="sprite">The sprite referenced by this decoration.</param>
		''' <returns>Decoration that created and added to the level.</returns>
		Public Function CreateDecoration(room As SingleRoom, Optional sprite As Sprite = Nothing) As DecorationEventCollection
			Dim temp As New DecorationEventCollection(room) With {.Parent = Me, .File = sprite}
			_Decorations.Add(temp)
			Return temp
		End Function
		''' <summary>
		''' Clone the decoration and add it to the level.
		''' </summary>
		''' <param name="decoration">Decoration that was copied.</param>
		''' <returns></returns>
		Public Function CloneDecoration(decoration As DecorationEventCollection) As DecorationEventCollection
			Dim temp = decoration.Clone
			Me._Decorations.Add(temp)
			Return temp
		End Function
		''' <summary>
		''' Remove the decoration from the level.
		''' </summary>
		''' <param name="decoration">The decoration to be removed.</param>
		''' <returns></returns>
		Public Function RemoveDecoration(decoration As DecorationEventCollection) As Boolean
			If Decorations.Contains(decoration) Then
				MyBase.RemoveRange(decoration)
				Return _Decorations.Remove(decoration)
			End If
			Return False
		End Function
		''' <summary>
		''' Create a row and add it to the level.
		''' </summary>
		''' <param name="room">The room where this row is in.</param>
		''' <param name="character">The character used by this row.</param>
		''' <returns>Row that created and added to the level.</returns>
		Public Function CreateRow(room As SingleRoom, character As RDCharacter) As RowEventCollection
			Dim temp As New RowEventCollection() With {.Character = character, .Rooms = room, .Parent = Me}
			temp.Parent = Me
			_Rows.Add(temp)
			Return temp
		End Function
		''' <summary>
		''' Remove the row from the level.
		''' </summary>
		''' <param name="row">The row to be removed.</param>
		''' <returns></returns>
		Public Function RemoveRow(row As RowEventCollection) As Boolean
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
		''' <summary>
		''' Read from file as level.
		''' Use default input settings.
		''' Supports .rdlevel, .rdzip, .zip file extension.
		''' </summary>
		''' <param name="filepath">File path.</param>
		''' <exception cref="VersionTooLowException">The minimum level version number supported by this library is 54.</exception>
		''' <exception cref="ConvertingException"></exception>
		''' <exception cref="RhythmBaseException">File not supported.</exception>
		''' <returns>An instance of a level that reads from a file.</returns>
		Public Shared Function LoadFile(filepath As String) As RDLevel
			Return LoadFile(filepath, New LevelReadOrWriteSettings)
		End Function
		''' <summary>
		''' Read from file as level.
		''' Supports .rdlevel, .rdzip, .zip file extension.
		''' </summary>
		''' <param name="filepath">File path.</param>
		''' <param name="settings">Input settings.</param>
		''' <exception cref="VersionTooLowException">The minimum level version number supported by this library is 54.</exception>
		''' <exception cref="ConvertingException"></exception>
		''' <exception cref="RhythmBaseException">File not supported.</exception>
		''' <returns>An instance of a level that reads from a file.</returns>
		Public Shared Function LoadFile(filepath As String, settings As LevelReadOrWriteSettings) As RDLevel
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
		Private Function Serializer(settings As LevelReadOrWriteSettings) As JsonSerializer
			Dim LevelSerializerSettings = New JsonSerializer()
			LevelSerializerSettings.Converters.Add(New Converters.RDLevelConverter(_path, settings))
			Return LevelSerializerSettings
		End Function
		Private Sub WriteStream(fileStream As TextWriter, settings As LevelReadOrWriteSettings)
			Using writer As New JsonTextWriter(fileStream)
				Serializer(settings).Serialize(writer, Me)
			End Using
		End Sub
		''' <summary>
		''' Save the level.
		''' Use default output settings.
		''' </summary>
		''' <param name="filepath">File path.</param>
		''' <exception cref="OverwriteNotAllowedException">Overwriting is disabled by the settings and a file with the same name already exists.</exception>
		Public Sub SaveFile(filepath As String)
			SaveFile(filepath, New LevelReadOrWriteSettings)
		End Sub
		''' <summary>
		''' Save the level.
		''' </summary>
		''' <param name="filepath">File path.</param>
		''' <param name="settings">Output settings.</param>
		''' <exception cref="OverwriteNotAllowedException">Overwriting is disabled by the settings and a file with the same name already exists.</exception>
		Public Sub SaveFile(filepath As String, settings As LevelReadOrWriteSettings)
			If Not _path.IsNullOrEmpty AndAlso IO.Path.GetFullPath(_path) = IO.Path.GetFullPath(filepath) Then
				Throw New OverwriteNotAllowedException(_path, GetType(LevelReadOrWriteSettings))
			End If
			Using file = IO.File.CreateText(filepath)
				WriteStream(file, settings)
			End Using
		End Sub
		''' <summary>
		''' Convert to JObject type.
		''' </summary>
		''' <returns>A JObject type that stores all the data for the level.</returns>
		Public Function ToJObject() As Linq.JObject
			Return ToJObject(New LevelReadOrWriteSettings)
		End Function
		''' <summary>
		''' Convert to JObject type.
		''' </summary>
		''' <returns>A JObject type that stores all the data for the level.</returns>
		Public Function ToJObject(settings As LevelReadOrWriteSettings) As Linq.JObject
			Dim LevelSerializer = New JsonSerializer()
			LevelSerializer.Converters.Add(New RDLevelConverter(Path, settings))
			Return Linq.JObject.FromObject(Me, LevelSerializer)
		End Function
		''' <summary>
		''' Convert to a string that can be read by the game.
		''' Use default output settings.
		''' </summary>
		''' <returns>Level string.</returns>
		Public Function ToRDLevelJson() As String
			Return ToRDLevelJson(New LevelReadOrWriteSettings)
		End Function
		''' <summary>
		''' Convert to a string that can be read by the game.
		''' </summary>
		''' <param name="settings">Output settings.</param>
		''' <returns>Level string.</returns>
		Public Function ToRDLevelJson(settings As LevelReadOrWriteSettings) As String
			Dim file = New IO.StringWriter()
			WriteStream(file, settings)
			file.Close()
			Return file.ToString
		End Function
		''' <summary>
		''' Add event to the level.
		''' </summary>
		''' <param name="item">Event to be added.</param>
		''' <exception cref="RhythmBaseException"></exception>
		Public Overrides Sub Add(item As BaseEvent)

			'添加默认节拍
			If item._beat.IsEmpty Then
				item._beat._calculator = Calculator
			End If

			'部分事件只能在小节的开头
			If TryCast(item, IBarBeginningEvent) IsNot Nothing AndAlso item._beat.BarBeat.beat <> 1 Then
				Throw New IllegalBeatException(item)
			End If

			'更改节拍的关联关卡
			item._beat._calculator = Calculator
			item._beat.ResetCache()

			If item.Type = EventType.Comment AndAlso CType(item, Comment).Parent Is Nothing Then
				'注释事件可能在精灵板块，也可能不在
				MyBase.Add(item)

			ElseIf item.Type = EventType.TintRows AndAlso CType(item, TintRows).Parent Is Nothing Then
				'轨道染色事件可能是为所有轨道染色
				MyBase.Add(item)

			ElseIf RowTypes.Contains(item.Type) Then
				Dim rowAction = CType(item, BaseRowAction)
				If rowAction.Parent Is Nothing Then
					Throw New UnreadableEventException("The Parent property of this event should not be null. Call RDRow.Add() instead.", item)
				End If
				'添加至对应轨道
				rowAction.Parent.AddSafely(item)
				MyBase.Add(item)
				Return

			ElseIf DecorationTypes.Contains(item.Type) Then
				Dim decoAction = CType(item, BaseDecorationAction)
				If decoAction.Parent Is Nothing Then
					Throw New UnreadableEventException("The Parent property of this event should not be null. Call RDDecoration.Add() instead.", item)
				End If
				'添加至对应精灵
				decoAction.Parent.AddSafely(item)
				MyBase.Add(item)
				Return

			Else
				If item.Type = EventType.SetCrotchetsPerBar Then
					AddSetCrotchetsPerBar(item)
				ElseIf ConvertToEnums(Of BaseBeatsPerMinute).Contains(item.Type) Then
					AddBaseBeatsPerMinute(item)
				Else
					MyBase.Add(item)
				End If
			End If
		End Sub
		''' <summary>
		''' Determine if the level contains this event.
		''' </summary>
		''' <param name="item">Event.</param>
		''' <returns></returns>
		Public Overrides Function Contains(item As BaseEvent) As Boolean
			Return (RowTypes.Contains(item.Type) AndAlso Rows.Any(Function(i) i.Contains(item))) OrElse
							(DecorationTypes.Contains(item.Type) AndAlso Decorations.Any(Function(i) i.Contains(item))) OrElse
							MyBase.Contains(item)
		End Function
		''' <summary>
		''' Remove event from the level.
		''' </summary>
		''' <param name="item">Event to be removed.</param>
		''' <returns></returns>
		Public Overrides Function Remove(item As BaseEvent) As Boolean
			If RowTypes.Contains(item.Type) AndAlso Rows.Any(Function(i) i.RemoveSafely(CType(item, BaseRowAction))) Then
				MyBase.Remove(item)
				item._beat._calculator = Nothing
				Return True
			End If
			If DecorationTypes.Contains(item.Type) AndAlso Decorations.Any(Function(i) i.RemoveSafely(CType(item, BaseDecorationAction))) Then
				MyBase.Remove(item)
				item._beat._calculator = Nothing
				Return True
			End If
			If Contains(item) Then
				If item.Type = EventType.SetCrotchetsPerBar Then
					Return RemoveSetCrotchetsPerBar(item)
				ElseIf ConvertToEnums(Of BaseBeatsPerMinute).Contains(item.Type) Then
					Return RemoveBaseBeatsPerMinute(item)
				Else
					Dim result = MyBase.Remove(item)
					item._beat._calculator = Nothing
					Return result
				End If
			End If
			Return False
		End Function
		Protected Friend Sub AddSetCrotchetsPerBar(item As SetCrotchetsPerBar)

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
				Dim nxtE = item.After(Of BaseEvent).FirstOrDefault(Function(i) TryCast(i, IBarBeginningEvent) IsNot Nothing AndAlso
																			   i.Type <> EventType.SetCrotchetsPerBar AndAlso
																			   i._beat < nxt._beat)
				Dim interval = If(nxtE?._beat.BeatOnly, nxt._beat.BeatOnly) - item._beat.BeatOnly
				Dim c = interval Mod item.CrotchetsPerBar
				If c > 0 Then
					c = If(c < 2, c + item.CrotchetsPerBar, c)
					MyBase.Add(New SetCrotchetsPerBar() With {._beat = item._beat + interval - c, ._crotchetsPerBar = c - 1})
				ElseIf nxt.CrotchetsPerBar = item.CrotchetsPerBar Then
					MyBase.Remove(nxt)
				End If

				If nxtE IsNot Nothing Then
					MyBase.Add(New SetCrotchetsPerBar() With {._beat = nxtE._beat, ._crotchetsPerBar = If(frt?.CrotchetsPerBar, 8) - 1})
				End If
			End If

			'更新计算器
			Calculator.Refresh()
		End Sub
		Protected Friend Function RemoveSetCrotchetsPerBar(item As SetCrotchetsPerBar) As Boolean

			Dim frt = item.FrontOrDefault
			Dim nxt = item.NextOrDefault

			If nxt IsNot Nothing Then
				Dim nxtE = item.After(Of BaseEvent).FirstOrDefault(Function(i) TryCast(i, IBarBeginningEvent) IsNot Nothing AndAlso
																			   i.Type <> EventType.SetCrotchetsPerBar AndAlso
																			   i._beat < nxt._beat)
				Dim cpb = item.CrotchetsPerBar
				Dim interval = If(nxtE?._beat.BeatOnly, nxt._beat.BeatOnly) - item._beat.BeatOnly
				Dim c = interval Mod If(frt?.CrotchetsPerBar, 8)
				If c > 0 Then
					c = If(c < 2, c + item.CrotchetsPerBar, c)
					If c = nxt.CrotchetsPerBar Then
						MyBase.Remove(nxt)
					End If
					MyBase.Add(New SetCrotchetsPerBar With {._beat = item._beat + interval - c, ._crotchetsPerBar = c - 1})
				Else
					If nxtE Is Nothing AndAlso nxt.CrotchetsPerBar = If(frt?.CrotchetsPerBar, 8) Then
						MyBase.Remove(nxt)
					End If
				End If
				If nxtE IsNot Nothing Then
					MyBase.Add(New SetCrotchetsPerBar() With {._beat = nxtE._beat, ._crotchetsPerBar = If(frt?.CrotchetsPerBar, 8) - 1})
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
		Protected Friend Sub AddBaseBeatsPerMinute(item As BaseBeatsPerMinute)
			'更新 BPM
			RefreshBPMs(item.Beat)
			'添加事件
			MyBase.Add(item)
			Calculator.Refresh()
		End Sub
		Protected Friend Function RemoveBaseBeatsPerMinute(item As BaseBeatsPerMinute) As Boolean
			Dim result = MyBase.Remove(item)
			RefreshBPMs(item.Beat)
			item._beat._calculator = Nothing
			Return result
		End Function
		Private Sub RefreshBPMs(start As Beat)
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
		Private Sub RefreshCPBs(start As Beat)
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
	''' <summary>
	''' level settings.
	''' </summary>
	Public Class Settings
		''' <summary>
		''' Difficulty level of the level.
		''' </summary>
		Public Enum DifficultyLevel
			Easy
			Medium
			Tough
			VeryTough
		End Enum
		''' <summary>
		''' Play mode of the level.
		''' </summary>
		Public Enum LevelPlayedMode
			OnePlayerOnly
			TwoPlayerOnly
			BothModes
		End Enum
		''' <summary>
		''' Behavior of the first beat of the level.
		''' </summary>
		Public Enum FirstBeatBehaviors
			RunNormally
			RunEventsOnPrebar
		End Enum
		Public Enum MultiplayerAppearances
			HorizontalStrips
			[Nothing]
		End Enum
		''' <summary>
		''' The version number of the level.
		''' The minimum level version number supported by this library is 55.
		''' </summary>
		Public Property Version As Integer = 60
		''' <summary>
		''' Song artist.
		''' </summary>
		Public Property Artist As String = "" 'Done
		''' <summary>
		''' Song name.
		''' </summary>
		Public Property Song As String = "" 'Done
		''' <summary>
		''' Special artlist type
		''' </summary>
		Public Property SpecialArtistType As RTSpecialArtistTypes = RTSpecialArtistTypes.None 'Enum
		''' <summary>
		''' File path for proof of artist's permission.
		''' </summary>
		Public Property ArtistPermission As String = "" 'Done
		''' <summary>
		''' Artlist links.
		''' </summary>
		Public Property ArtistLinks As String = "" 'Link
		''' <summary>
		''' Level author.
		''' </summary>
		Public Property Author As String = "" 'done
		''' <summary>
		''' Level difficulty.
		''' </summary>
		Public Property Difficulty As DifficultyLevel = DifficultyLevel.Easy 'Enum
		''' <summary>
		''' Show seizure warning.
		''' </summary>
		Public Property SeizureWarning As Boolean = False
		''' <summary>
		''' Preview image file path.
		''' </summary>
		Public Property PreviewImage As String = "" 'FilePath
		''' <summary>
		''' Syringe packaging image file path.
		''' </summary>
		Public Property SyringeIcon As String = "" 'FilePath
		''' <summary>
		''' The file path of the music used for previewing.
		''' </summary>
		Public Property PreviewSong As String = "" 'Done
		''' <summary>
		''' Start time of preview music.
		''' </summary>
		<JsonConverter(GetType(SecondConverter))> Public Property PreviewSongStartTime As TimeSpan
		''' <summary>
		''' Duration of preview music.
		''' </summary>
		<JsonConverter(GetType(SecondConverter))> Public Property PreviewSongDuration As TimeSpan
		''' <summary>
		''' Hue offset or grayscale of the level name on the syringe.
		''' </summary>
		<JsonProperty("songNameHue")> Public Property SongNameHueOrGrayscale As Single
		''' <summary>
		''' Whether grayscale is enabled.
		''' </summary>
		Public Property SongLabelGrayscale As Boolean
		''' <summary>
		''' Level Description.
		''' </summary>
		Public Property Description As String = "" 'Done
		''' <summary>
		''' Level labels.
		''' </summary>
		Public Property Tags As String = "" 'Done
		''' <summary>
		''' Separate two-player level file paths.
		''' It Is uncertain if this attribute Is still being used.
		''' </summary>
		Public Property Separate2PLevelFilename As String = "" 'FilePath
		''' <summary>
		''' Level play mode.
		''' </summary>
		Public Property CanBePlayedOn As LevelPlayedMode = LevelPlayedMode.OnePlayerOnly 'Enum
		''' <summary>
		''' Behavior of the first beat of the level.
		''' </summary>
		Public Property FirstBeatBehavior As FirstBeatBehaviors = FirstBeatBehaviors.RunNormally 'Enum
		Public Property MultiplayerAppearance As MultiplayerAppearances = MultiplayerAppearances.HorizontalStrips 'Enum
		''' <summary>
		''' A percentage value indicating the total volume of the level.
		''' </summary>
		Public Property LevelVolume As Single = 1
		''' <summary>
		''' Maximum number of mistakes per rank
		''' </summary>
		Public Property RankMaxMistakes As New LimitedList(Of Integer)(4, 20)
		''' <summary>
		''' Description of each rank
		''' </summary>
		Public Property RankDescription As New LimitedList(Of String)(6, "")
		''' <summary>
		''' Mods enabled for the level.
		''' </summary>
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
End Namespace