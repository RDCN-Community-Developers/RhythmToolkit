Imports System.IO
Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Newtonsoft.Json
Imports RhythmBase.Assets
Imports RhythmBase.Components
Imports RhythmBase.Events
Imports RhythmBase.Exceptions
Imports RhythmBase.LevelElements
Imports RhythmBase.Settings
Imports RhythmBase.Utils
Imports SkiaSharp
#Disable Warning CA1507
#Disable Warning IDE1006
#Disable Warning CA1812
#Disable Warning CA1822
#Disable Warning IDE0060
Namespace Components
	Public Enum Characters
		Adog
		Barista
		Beat
		Bodybuilder
		Boy
		BoyRaya
		BoyTangzhuang
		Buro
		Clef
		Cockatiel
		ColeGuitar
		ColeSynth
		Controller
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
		Kanye
		Lucia
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
		Otto
		Oriole
		Owl
		Paige
		Parrot
		Politician
		Purritician
		Quaver
		Rin
		Rodney
		Samurai
		SamuraiBlue
		SamuraiBoss
		SamuraiBossAlt
		SamuraiGirl
		SamuraiGreen
		SamuraiYellow
		SmokinBarista
		Tentacle
		Treble
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
		'Public room As New LimitedList(Of RoomObject)(4, New RoomObject)
		'Public vfx As VfxObject
		'Public Function SetOneshotType(rowID As Integer, wavetype As String) As Single?
		'	Return Nothing
		'End Function
		'Public Function trueCameraMove(RoomID As Integer, Xpx As Integer, Ypx As Integer, AnimationDuration As Single, Ease As String) As Single?
		'	Return Nothing
		'End Function
		'Public Function MistakeOrHealSilent(weight As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function MistakeOrHealP1Silent(weight As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function MistakeOrHealP2Silent(weight As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function MistakeOrHeal(damageOrHeal As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function MistakeOrHealP1(damageOrHeal As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function MistakeOrHealP2(damageOrHeal As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function SetMistakeWeight(rowID As Integer, weight As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function DamageHeart(rowID As Integer, damage As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function HealHeart(rowID As Integer, damage As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function create(ObjectName As String, x As Single, y As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function shockwave([property] As String, value As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function ShowPlayerHand(roomID As Integer, isPlayer1 As Boolean, isShortArm As Boolean, isInstant As Boolean) As Single?
		'	Return Nothing
		'End Function
		'Public Function TintHandsWithInts(roomID As Integer, R As Single, G As Single, B As Single, A As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function SetHandsBorderColor(roomID As Integer, R As Single, G As Single, B As Single, A As Single, style As Integer) As Single?
		'	Return Nothing
		'End Function
		'Public Function SetAllHandsBorderColor(R As Single, G As Single, B As Single, A As Single, style As Integer) As Single?
		'	Return Nothing
		'End Function
		'Public Function SetHandToP1(room As Integer, rightHand As Boolean) As Single?
		'	Return Nothing
		'End Function
		'Public Function SetHandToP2(room As Integer, rightHand As Boolean) As Single?
		'	Return Nothing
		'End Function
		'Public Function SetHandToIan(room As Integer, rightHand As Boolean) As Single?
		'	Return Nothing
		'End Function
		'Public Function SetHandToPaige(room As Integer, rightHand As Boolean) As Single?
		'	Return Nothing
		'End Function
		'Public Function SetShadowRow(mimickerRowID As Integer, mimickedRowID As Integer) As Single?
		'	Return Nothing
		'End Function
		'Public Function UnsetShadowRow(mimickerRowID As Integer, mimickedRowID As Integer) As Single?
		'	Return Nothing
		'End Function
		'Public Function SetKaleidoscopeColor(roomID As Integer, R1 As Single, G1 As Single, B1 As Single, R2 As Single, G2 As Single, B2 As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function SyncKaleidoscopes(targetRoomID As Integer, otherRoomID As Integer) As Single?
		'	Return Nothing
		'End Function
		'Public Function EnableRowReflections(roomID As Integer) As Single?
		'	Return Nothing
		'End Function
		'Public Function DisableRowReflections(roomID As Integer) As Single?
		'	Return Nothing
		'End Function
		'Public Function ChangeCharacter(Name As String, rowID As Integer) As Single?
		'	Return Nothing
		'End Function
		'Public Function ChangeCharacterSmooth(Name As String, rowID As Integer) As Single?
		'	Return Nothing
		'End Function
		'Public Function ToggleSingleRowReflections(room As Integer, row As Integer, action As Boolean) As Single?
		'	Return Nothing
		'End Function
		'Public Function CurrentSongVol(targetVolume As Single, fadeTimeSeconds As Single) As Single?
		'	Return Nothing
		'End Function
		'Public Function PreviousSongVol(targetVolume As Single, fadeTimeSeconds As Single) As Single?
		'	Return Nothing
		'End Function

		'Public Class RoomObject
		'	Public wavyRowsAmplitude As Boolean
		'	Public wavyRowsFrequency As Single
		'	Public Function EditTree(room As Byte, [property] As String, value As Single, beats As Single, ease As String) As Single?
		'		Return Nothing
		'	End Function
		'	Public Function EditTreeColor(location As Boolean, color As String, beats As Single, ease As String) As Single?
		'		Return Nothing
		'	End Function
		'	Public Function SetShakeIntensityOnHit(number As Integer, strength As Integer) As Single?
		'		Return Nothing
		'	End Function
		'	Public Function SetScrollSpeed(speed As Single, duration As Single, Ease As String) As Single?
		'		Return Nothing
		'	End Function
		'	Public Function SetScrollOffset(cameraOffset As Integer, duration As Single, ease As String) As Single?
		'		Return Nothing
		'	End Function
		'	Public Function DarkenedRollerdisco(value As Boolean) As Single?
		'		Return Nothing
		'	End Function
		'End Class
		'Public Class VfxObject
		'	Public Function ShakeCam(number As Integer, strength As Integer, roomID As Integer) As Single?
		'		Return Nothing
		'	End Function
		'	Public Function StopShakeCam(roomID As Integer) As Single?
		'		Return Nothing
		'	End Function
		'	Public Function ShakeCamSmooth(duration As Single, strength As Integer, roomID As Integer) As Single?
		'		Return Nothing
		'	End Function
		'	Public Function ShakeCamRotate(duration As Single, strength As Integer, roomID As Integer) As Single?
		'		Return Nothing
		'	End Function
		'	Public Function SetVignetteAlpha(alpha As Single, roomID As Integer) As Single?
		'		Return Nothing
		'	End Function
		'End Class
	End Class
	Public Interface INumOrExp
		Function Serialize() As String
		Function TryGetValue() As Single?
	End Interface
	Public Structure Num
		Implements INumOrExp
		Public ReadOnly value As Single
		Public Sub New(value As String)
			Me.value = value
		End Sub
		Public Shared Function CanCast(value As String) As Boolean
			Return Single.TryParse(value, 0)
		End Function
		Public Overrides Function ToString() As String
			Return value
		End Function
		Friend Function Serialize() As String Implements INumOrExp.Serialize
			Return value
		End Function
		Public Function TryGetValue() As Single? Implements INumOrExp.TryGetValue
			Return value
		End Function
		Public Shared Widening Operator CType(value As Single) As Num
			Return New Num(value)
		End Operator
	End Structure
	Public Structure Exp
		Implements INumOrExp
		Public ReadOnly value As String
		Public Sub New(value As String)
			Me.value = Regex.Match(value, "^\{(.*)\}$").Groups(1).Value
		End Sub
		Public Shared Function CanCast(value As String) As Boolean
			Return Regex.Match(value, "^\{.*\}$").Success
		End Function
		Public Overrides Function ToString() As String
			Return value
		End Function
		Friend Function Serialize() As String Implements INumOrExp.Serialize
			Return $"""{{{value}}}"""
		End Function
		Public Function TryGetValue() As Single? Implements INumOrExp.TryGetValue
			Return Nothing
		End Function
		Public Shared Widening Operator CType(value As String) As Exp
			Return New Exp(value)
		End Operator
	End Structure
	'Public Structure [Function]
	'	Implements INumberOrExp
	'	Private ReadOnly [Function] As Func(Of Single)
	'	Public Sub New(func As Func(Of Single))
	'		Me.Function = func
	'	End Sub
	'	Public Function Serialize() As String Implements INumberOrExp.Serialize
	'		Return [Function]()
	'	End Function
	'	Public Function GetValue(variables As Variables) As Single Implements INumberOrExp.GetValue
	'		Return [Function]()
	'	End Function
	'	Public Overrides Function ToString() As String
	'		Return $"Value: {[Function]()}"
	'	End Function
	'End Structure
	Public Structure NumOrExpPair
		Public X As INumOrExp
		Public Y As INumOrExp
		Public Sub New(x As INumOrExp, y As INumOrExp)
			Me.X = x
			Me.Y = y
		End Sub
		Public Sub New(x As String, y As String)
			If x Is Nothing OrElse x.Length = 0 Then
				Me.X = Nothing
			ElseIf Num.CanCast(x) Then
				Me.X = New Num(x)
			ElseIf Exp.CanCast(x) Then
				Me.X = New Exp(x)
			Else
				Throw New RhythmBaseException($"Illegal expression: {x}")
			End If
			If y Is Nothing OrElse y.Length = 0 Then
				Me.Y = Nothing
			ElseIf Num.CanCast(y) Then
				Me.Y = New Num(y)
			ElseIf Exp.CanCast(y) Then
				Me.Y = New Exp(y)
			Else
				Throw New RhythmBaseException($"Illegal expression: {y}")
			End If
		End Sub
		Public Shared Widening Operator CType(value As (x As INumOrExp, y As INumOrExp)) As NumOrExpPair
			Return New NumOrExpPair(value.x, value.y)
		End Operator
		Public Shared Widening Operator CType(value As (x As String, y As String)) As NumOrExpPair
			Return New NumOrExpPair(value.x, value.y)
		End Operator
		Public Overrides Function ToString() As String
			Return $"{{{X},{Y}}}"
		End Function
		Public Function TryGetValue() As RDPoint
			Return New RDPoint(X.TryGetValue, Y.TryGetValue)
		End Function
		Public Shared Widening Operator CType(v As Linq.JArray) As NumOrExpPair
			Return New NumOrExpPair(v(0).ToString, v(1).ToString)
		End Operator
	End Structure
	Public Structure RDPoint
		Public X As Single
		Public Y As Single
		Public Property Width As Single
			Get
				Return X
			End Get
			Set(value As Single)
				X = value
			End Set
		End Property
		Public Property Height As Single
			Get
				Return Y
			End Get
			Set(value As Single)
				Y = value
			End Set
		End Property
		Public Sub New(x As Single, y As Single)
			Me.X = x : Me.Y = y
		End Sub
		Public Shared Narrowing Operator CType(e As RDPoint) As SKPointI
			Return New SKPointI(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As RDPoint) As SKPoint
			Return New SKPoint(e.X, e.Y)
		End Operator
		Public Shared Narrowing Operator CType(e As RDPoint) As SKSizeI
			Return New SKSizeI(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As RDPoint) As SKSize
			Return New SKSize(e.X, e.Y)
		End Operator
		Public Shared Narrowing Operator CType(e As RDPoint) As System.Drawing.Point
			Return New Drawing.Point(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As RDPoint) As System.Drawing.PointF
			Return New Drawing.PointF(e.X, e.Y)
		End Operator
		Public Shared Narrowing Operator CType(e As RDPoint) As System.Drawing.Size
			Return New Drawing.Size(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As RDPoint) As System.Drawing.SizeF
			Return New Drawing.SizeF(e.X, e.Y)
		End Operator
		Public Shared Narrowing Operator CType(e As RDPoint) As (X As Integer, Y As Integer)
			Return (e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As RDPoint) As (X As Single, Y As Single)
			Return (e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As RDPoint) As (X As Double, Y As Double)
			Return (e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As RDPoint) As NumOrExpPair
			Return New NumOrExpPair(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As SKPointI) As RDPoint
			Return New RDPoint(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As SKPoint) As RDPoint
			Return New RDPoint(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As SKSizeI) As RDPoint
			Return New RDPoint(e.Width, e.Height)
		End Operator
		Public Shared Widening Operator CType(e As SKSize) As RDPoint
			Return New RDPoint(e.Width, e.Height)
		End Operator
		Public Shared Widening Operator CType(e As Drawing.Point) As RDPoint
			Return New RDPoint(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As Drawing.PointF) As RDPoint
			Return New RDPoint(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As Drawing.Size) As RDPoint
			Return New RDPoint(e.Width, e.Height)
		End Operator
		Public Shared Widening Operator CType(e As Drawing.SizeF) As RDPoint
			Return New RDPoint(e.Width, e.Height)
		End Operator
		Public Shared Widening Operator CType(e As (X As Integer, Y As Integer)) As RDPoint
			Return New RDPoint(e.X, e.Y)
		End Operator
		Public Shared Widening Operator CType(e As (X As Single, Y As Single)) As RDPoint
			Return New RDPoint(e.X, e.Y)
		End Operator
		Public Shared Narrowing Operator CType(e As (X As Double, Y As Double)) As RDPoint
			Return New RDPoint(e.X, e.Y)
		End Operator
		Public Shared Narrowing Operator CType(e As NumOrExpPair) As RDPoint
			Return New RDPoint(e.X.TryGetValue, e.Y.TryGetValue)
		End Operator
		Public Shared Operator +(e1 As RDPoint) As RDPoint
			Return e1
		End Operator
		Public Shared Operator -(e1 As RDPoint) As RDPoint
			Return New RDPoint(-e1.X, -e1.Y)
		End Operator
		Public Shared Operator +(e1 As RDPoint, e2 As RDPoint) As RDPoint
			Return New RDPoint(e1.X + e2.X, e1.Y + e2.Y)
		End Operator
		Public Shared Operator -(e1 As RDPoint, e2 As RDPoint) As RDPoint
			Return New RDPoint(e1.X - e2.X, e1.Y - e2.Y)
		End Operator
		Public Shared Operator *(e1 As RDPoint, e2 As Single) As RDPoint
			Return New RDPoint(e1.X * e2, e1.Y * e2)
		End Operator
		Public Shared Operator \(e1 As RDPoint, e2 As Single) As RDPoint
			Return New RDPoint(e1.X \ e2, e1.Y \ e2)
		End Operator
		Public Shared Operator /(e1 As RDPoint, e2 As Single) As RDPoint
			Return New RDPoint(e1.X / e2, e1.Y / e2)
		End Operator
		Public Shared Operator Mod(e1 As RDPoint, e2 As Single) As RDPoint
			Return New RDPoint(e1.X Mod e2, e1.Y Mod e2)
		End Operator
		Public Shared Operator *(e1 As RDPoint, e2 As RDPoint) As RDPoint
			Return New RDPoint(e1.X * e2.X, e1.Y * e2.Y)
		End Operator
		Public Shared Operator \(e1 As RDPoint, e2 As RDPoint) As RDPoint
			Return New RDPoint(e1.X \ e2.X, e1.Y \ e2.Y)
		End Operator
		Public Shared Operator /(e1 As RDPoint, e2 As RDPoint) As RDPoint
			Return New RDPoint(e1.X / e2.X, e1.Y / e2.Y)
		End Operator
		Public Shared Operator Mod(e1 As RDPoint, e2 As RDPoint) As RDPoint
			Return New RDPoint(e1.X Mod e2.X, e1.Y Mod e2.Y)
		End Operator
		Public Shared Operator =(e1 As RDPoint, e2 As RDPoint) As Boolean
			Return e1.X = e2.X AndAlso e1.Y = e2.Y
		End Operator
		Public Shared Operator <>(e1 As RDPoint, e2 As RDPoint) As Boolean
			Return e1.X <> e2.X OrElse e1.Y <> e2.Y
		End Operator
		Public Function MultipyByMatrix(matrix(,) As Single) As RDPoint
			If matrix.Rank = 2 AndAlso matrix.Length = 4 Then
				Return New RDPoint(
X * matrix(0, 0) + Y * matrix(1, 0),
X * matrix(0, 1) + Y * matrix(1, 1))
			End If
			Throw New Exception("Matrix not match.")
		End Function
		Public Function Rotate(angle As Single) As RDPoint
			Return MultipyByMatrix(
{
{Math.Cos(angle), Math.Sin(angle)},
{-Math.Sin(angle), Math.Cos(angle)}
})
		End Function
		Public Function Rotate(pivot As RDPoint, angle As Single) As RDPoint
			Return (Me - pivot).Rotate(angle) + pivot
		End Function
	End Structure
	Public Structure Hit
		Public ReadOnly BeatOnly As Single
		Public ReadOnly Hold As Single
		Public ReadOnly Parent As BaseBeat
		Public ReadOnly Property BarBeat As (Bar As UInteger, Beat As Single)
			Get
				Dim Calculator As New BeatCalculator(Parent.ParentLevel)
				Return Calculator.BeatOnly_BarBeat(BeatOnly)
			End Get
		End Property
		Public ReadOnly Property Time As TimeSpan
			Get
				Dim Calculator As New BeatCalculator(Parent.ParentLevel)
				Return Calculator.BeatOnly_Time(BeatOnly)
			End Get
		End Property
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
	Public Structure RowStatus
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
._data = RoomIndex.RoomNotAvaliable
}
			End Get
		End Property
		Public Sub New(enableTop As Boolean, multipy As Boolean)
			Me.EnableTop = enableTop
			Me.Multipy = multipy
		End Sub
		Public Sub New(ParamArray rooms() As Byte)
			If rooms.Length = 0 Then
				_data = RoomIndex.RoomNotAvaliable
				Exit Sub
			End If
			For Each item In rooms
				Room(item) = True
			Next
			EnableTop = True
			Multipy = True
		End Sub
		Public Sub SetRooms(rooms As Rooms)
			For Each item In rooms.Rooms
				Me.Room(item) = True
			Next
		End Sub
		Public Function Contains(rooms As Rooms) As Boolean
			If _data = Rooms.RoomIndex.RoomNotAvaliable Then
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
		Public Overrides Function ToString() As String
			Return Filename
		End Function
	End Class
	Public Class LimitedList(Of T)
		Implements ICollection(Of T)
		Private ReadOnly list As List(Of (value As T, isDefault As Boolean))
		<JsonIgnore>
		Public Property DefaultValue As T
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
		Public Overrides Function ToString() As String
			Return $"Count = {Count}"
		End Function
	End Class
	Public Class OrderedEventCollection(Of T As BaseEvent)
		Implements ICollection(Of T)
		Protected Friend Property EventsBeatOrder As New SortedDictionary(Of Single, List(Of T))
		Protected Friend Property EventsTypeOrder As New Dictionary(Of EventType, SortedDictionary(Of Single, List(Of T)))
		<JsonIgnore>
		Public Overridable ReadOnly Property Count As Integer Implements ICollection(Of T).Count
			Get
				Dim count1 = EventsTypeOrder.Sum(Function(i) i.Value.Sum(Function(j) j.Value.Count))
				Dim count2 = EventsBeatOrder.Sum(Function(i) i.Value.Count)
				If count1 = count2 Then
					Return ConcatAll.Count
				End If
				Dim errorList1 = From list1 In EventsBeatOrder
								 From item1 In list1.Value
								 Select item1
				Dim errorList2 = From typePair In EventsTypeOrder
								 From list2 In typePair.Value
								 From item2 In list2.Value
								 Select item2
				Dim errorL = errorList2.Except(errorList1)
				Throw New RhythmBaseException($"Internal exception: {count1}, {count2}")
			End Get
		End Property
		<JsonIgnore>
		Public ReadOnly Property IsReadOnly As Boolean = False Implements ICollection(Of T).IsReadOnly
		Public Sub New()
		End Sub
		Public Sub New(items As IEnumerable(Of T))
			For Each item In items
				Me.Add(item)
			Next
		End Sub
		Public Function GetTaggedEvents(name As String, direct As Boolean) As IEnumerable(Of IGrouping(Of String, T))
			If name Is Nothing Then
				Return Nothing
			End If
			If direct Then
				Return Where(Function(i) i.Tag = name).GroupBy(Function(i) i.Tag)
			Else
				Return Where(Function(i) If(i.Tag, "").Contains(name)).GroupBy(Function(i) i.Tag)
			End If
		End Function
		Public Function ConcatAll() As List(Of T)
			Return EventsBeatOrder.SelectMany(Function(i) i.Value).ToList
		End Function
		Public Overridable Sub Add(item As T) Implements ICollection(Of T).Add
			If Not EventsTypeOrder.ContainsKey(item.Type) Then
				EventsTypeOrder.Add(item.Type, New SortedDictionary(Of Single, List(Of T)))
			End If
			If Not EventsTypeOrder(item.Type).ContainsKey(item.BeatOnly) Then
				EventsTypeOrder(item.Type).Add(item.BeatOnly, New List(Of T))
			End If
			EventsTypeOrder(item.Type)(item.BeatOnly).Add(item)
			If Not EventsBeatOrder.ContainsKey(item.BeatOnly) Then
				EventsBeatOrder.Add(item.BeatOnly, New List(Of T))
			End If
			EventsBeatOrder(item.BeatOnly).Add(item)
		End Sub
		Public Sub AddRange(items As IEnumerable(Of T))
			For Each item In items
				Add(item)
			Next
		End Sub
		Public Sub Clear() Implements ICollection(Of T).Clear
			EventsTypeOrder.Clear()
			EventsBeatOrder.Clear()
		End Sub
		Public Overridable Function Contains(item As T) As Boolean Implements ICollection(Of T).Contains
			Return EventsTypeOrder.ContainsKey(item.Type) AndAlso
				EventsTypeOrder(item.Type).ContainsKey(item.BeatOnly) AndAlso
				EventsTypeOrder(item.Type)(item.BeatOnly).Contains(item)
		End Function
		Public Iterator Function Where(predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
			For Each pair In EventsBeatOrder
				For Each item In pair.Value
					If predicate(item) Then
						Yield item
					End If
				Next
			Next
		End Function
		Public Iterator Function Where(startBeat As Single, endBeat As Single) As IEnumerable(Of T)
			For Each pair In EventsBeatOrder
				If endBeat < pair.Key Then
					Exit Function
				End If
				If startBeat <= pair.Key Then
					For Each item In pair.Value
						Yield item
					Next
				End If
			Next
		End Function
		Public Iterator Function Where(predicate As Func(Of T, Boolean), startBeat As Single, endBeat As Single) As IEnumerable(Of T)
			For Each pair In EventsBeatOrder
				If endBeat < pair.Key Then
					Exit Function
				End If
				If startBeat <= pair.Key Then
					For Each item In pair.Value
						If predicate(item) Then
							Yield item
						End If
					Next
				End If
			Next
		End Function
		Public Iterator Function Where(Of U As T)() As IEnumerable(Of U)
			Dim types = ConvertToEnums(Of U)()
			For Each pair In EventsBeatOrder
				For Each item In pair.Value.OfType(Of U)
					Yield item
				Next
			Next
		End Function
		Public Iterator Function Where(Of U As T)(startBeat As Single, endBeat As Single) As IEnumerable(Of U)
			Dim types = ConvertToEnums(Of U)()
			For Each pair In EventsBeatOrder
				If endBeat < pair.Key Then
					Exit Function
				End If
				If startBeat <= pair.Key Then
					For Each item In pair.Value.OfType(Of U)
						Yield item
					Next
				End If
			Next
		End Function
		Public Iterator Function Where(Of U As T)(predicate As Func(Of U, Boolean)) As IEnumerable(Of U)
			Dim types = ConvertToEnums(Of U)()
			For Each pair In EventsBeatOrder
				For Each item In pair.Value.OfType(Of U)
					If predicate(item) Then
						Yield item
					End If
				Next
			Next
		End Function
		Public Iterator Function Where(Of U As T)(predicate As Func(Of U, Boolean), startBeat As Single, endBeat As Single) As IEnumerable(Of U)
			Dim types = ConvertToEnums(Of U)()
			For Each pair In EventsBeatOrder
				If endBeat < pair.Key Then
					Exit Function
				End If
				If startBeat <= pair.Key Then
					For Each item In pair.Value.OfType(Of U)
						If predicate(item) Then
							Yield item
						End If
					Next
				End If
			Next
		End Function
		Public Function First() As T
			Return EventsBeatOrder.First.Value.First
		End Function
		Public Function First(predicate As Func(Of T, Boolean)) As T
			Return ConcatAll.First(predicate)
		End Function
		Public Function First(Of U As T)() As U
			Return Where(Of U).First
		End Function
		Public Function First(Of U As T)(predicate As Func(Of U, Boolean)) As U
			Return Where(Of U).First(predicate)
		End Function
		Public Function FirstOrDefault() As T
			Return EventsBeatOrder.FirstOrDefault.Value?.FirstOrDefault
		End Function
		Public Function FirstOrDefault(defaultValue As T) As T
			Return ConcatAll.FirstOrDefault(defaultValue)
		End Function
		Public Function FirstOrDefault(predicate As Func(Of T, Boolean)) As T
			Return ConcatAll.FirstOrDefault(predicate)
		End Function
		Public Function FirstOrDefault(predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return ConcatAll.FirstOrDefault(predicate, defaultValue)
		End Function
		Public Function FirstOrDefault(Of U As T)() As U
			Return Where(Of U).FirstOrDefault()
		End Function
		Public Function FirstOrDefault(Of U As T)(defaultValue As U) As U
			Return Where(Of U).FirstOrDefault(defaultValue)
		End Function
		Public Function FirstOrDefault(Of U As T)(predicate As Func(Of U, Boolean)) As U
			Return Where(Of U).FirstOrDefault(predicate)
		End Function
		Public Function FirstOrDefault(Of U As T)(predicate As Func(Of U, Boolean), defaultValue As U) As U
			Return Where(Of U).FirstOrDefault(predicate, defaultValue)
		End Function
		Public Function Last() As T
			Return EventsBeatOrder.Last.Value.Last
		End Function
		Public Function Last(predicate As Func(Of T, Boolean)) As T
			Return ConcatAll.Last(predicate)
		End Function
		Public Function Last(Of U As T)() As U
			Return Where(Of U).Last
		End Function
		Public Function Last(Of U As T)(predicate As Func(Of T, Boolean)) As U
			Return Where(Of U).Last(predicate)
		End Function
		Public Function LastOrDefault() As T
			Return EventsBeatOrder.LastOrDefault.Value?.LastOrDefault()
		End Function
		Public Function LastOrDefault(defaultValue As T) As T
			Return ConcatAll.LastOrDefault(defaultValue)
		End Function
		Public Function LastOrDefault(predicate As Func(Of T, Boolean)) As T
			Return ConcatAll.LastOrDefault(predicate)
		End Function
		Public Function LastOrDefault(predicate As Func(Of T, Boolean), defaultValue As T) As T
			Return ConcatAll.LastOrDefault(predicate, defaultValue)
		End Function
		Public Function LastOrDefault(Of U As T)() As U
			Return Where(Of U).LastOrDefault()
		End Function
		Public Function LastOrDefault(Of U As T)(defaultValue As U) As U
			Return Where(Of U).LastOrDefault(defaultValue)
		End Function
		Public Function LastOrDefault(Of U As T)(predicate As Func(Of U, Boolean)) As U
			Return Where(Of U).LastOrDefault(predicate)
		End Function
		Public Function LastOrDefault(Of U As T)(predicate As Func(Of U, Boolean), defaultValue As U) As U
			Return Where(Of U).LastOrDefault(predicate, defaultValue)
		End Function
		Public Function IndexOf(item As T) As Integer
			Dim count As Integer
			For Each pair In EventsBeatOrder
				If pair.Key < item.BeatOnly Then
					count += pair.Value.Count
				ElseIf pair.Key > item.BeatOnly Then
					Return -1
				Else
					Dim shortIndex = pair.Value.IndexOf(item)
					If shortIndex < 0 Then
						Return -1
					End If
					Return count + shortIndex
				End If
			Next
			Return -1
		End Function
		Public Function [Select](Of U)(predicate As Func(Of T, U)) As IEnumerable(Of T)
			Return From item In ConcatAll()
				   Select predicate(item)
		End Function
		Public Sub CopyTo(array() As T, arrayIndex As Integer) Implements ICollection(Of T).CopyTo
			ConcatAll.CopyTo(array, arrayIndex)
		End Sub
		Public Overridable Function Remove(item As T) As Boolean Implements ICollection(Of T).Remove
			If Contains(item) Then
				Dim result = EventsTypeOrder(item.Type)?(item.BeatOnly).Remove(item) And EventsBeatOrder(item.BeatOnly).Remove(item)
				If Not EventsTypeOrder(item.Type)(item.BeatOnly).Any Then
					EventsTypeOrder(item.Type).Remove(item.BeatOnly)
					If Not EventsTypeOrder(item.Type).Any Then
						EventsTypeOrder.Remove(item.Type)
					End If
				End If
				If Not EventsBeatOrder(item.BeatOnly).Any Then
					EventsBeatOrder.Remove(item.BeatOnly)
				End If
				Return result
			End If
			Return False
		End Function
		Public Function RemoveAll(predicate As Func(Of T, Boolean)) As Integer
			Dim count As UInteger = 0
			For Each item In New List(Of T)(Where(predicate))
				count += If(Remove(item), 1, 0)
			Next
			Return count
		End Function
		Public Sub RemoveRange(items As IEnumerable(Of T))
			For Each item In items
				Remove(item)
			Next
		End Sub
		Public Iterator Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
			For Each pair In EventsBeatOrder
				For Each item In pair.Value
					Yield item
				Next
			Next
		End Function
		Public Iterator Function ExtractEventsAt(beat As Single) As IEnumerable(Of BaseEvent)
			Dim temp = EventsBeatOrder(beat)
			For Each item In temp
				Yield item
			Next
			RemoveAll(Function(i) temp.Contains(i))
		End Function
		Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			For Each pair In EventsBeatOrder
				For Each item In pair.Value
					Yield item
				Next
			Next
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
		'<JsonIgnore> Public ReadOnly Property Children As New List(Of BaseDecorationAction)
		<JsonIgnore>
		Friend Parent As RDLevel
		<JsonProperty("id")> Public Property Id As String
			Get
				Return _id
			End Get
			Set(value As String)
				_id = value
			End Set
		End Property
		<JsonIgnore> Public ReadOnly Property Size As RDPoint
			Get
				Return If(File?.Size, New RDPoint(32, 31))
			End Get
		End Property
		<JsonIgnore> Public ReadOnly Property Expressions As IEnumerable(Of String)
			Get
				Return If(File?.Expressions, New List(Of String))
			End Get
		End Property
		Public Property Row As ULong
		Public ReadOnly Property Rooms As New Rooms(False, False)
		<JsonProperty("filename")> Public Property File As Sprite
		Public Property Depth As Integer
		Public Property Visible As Boolean
		Sub New()
		End Sub
		Friend Sub New(room As Rooms, asset As Sprite, Optional depth As Integer = 0, Optional visible As Boolean = True)
			Me.Rooms.SetRooms(room)
			Me._id = Me.GetHashCode
			Me._Depth = depth
			Me._Visible = visible
			Me.File = asset
		End Sub
		Sub New(room As Rooms, parent As Sprite, id As String, Optional depth As Integer = 0, Optional visible As Boolean = True)
			Me.New(room, parent, depth, visible)
			_id = id
		End Sub
		Public Function CreateChildren(Of T As {BaseDecorationAction, New})(beatOnly As Single) As T
			Dim Temp As New T With {
				.BeatOnly = beatOnly,
				.Parent = Me
				}
			Return Temp
		End Function
		Public Function CreateChildren(Of T As {BaseDecorationAction, New})(item As T) As T
			Dim Temp As T = item.Clone(Of T)(Me)
			Return Temp
		End Function
		Public Overrides Sub Add(item As BaseDecorationAction)
			If Not Parent.EventsBeatOrder.ContainsKey(item.BeatOnly) Then
				Parent.EventsBeatOrder.Add(item.BeatOnly, New List(Of BaseEvent))
			End If
			Parent.EventsBeatOrder(item.BeatOnly).Add(item)
			MyBase.Add(item)
		End Sub
		Public Overrides Function Remove(item As BaseDecorationAction) As Boolean
			Parent.EventsBeatOrder(item.BeatOnly).Remove(item)
			If Not Parent.EventsBeatOrder(item.BeatOnly).Any Then
				Parent.EventsBeatOrder.Remove(item.BeatOnly)
			End If
			Return MyBase.Remove(item)
		End Function
		Public Overrides Function ToString() As String
			Return $"{_id}, {_Row}, {_Rooms}, {File.Name}"
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
		Public Property Character As Character
		Public Property CpuMaker As Characters?
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
		Public ReadOnly Property Row As SByte
			Get
				Return Parent._Rows.IndexOf(Me)
			End Get
		End Property
		Public Property Rooms As New Rooms(False, False)
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
		Private Function ClassicBeats() As IEnumerable(Of BaseBeat)
			Return Where(Function(i)
							 Return (i.Type = EventType.AddClassicBeat Or
i.Type = EventType.AddFreeTimeBeat Or
i.Type = EventType.PulseFreeTimeBeat) AndAlso
CType(i, BaseBeat).Hitable
						 End Function).Cast(Of BaseBeat)
		End Function
		Private Function OneshotBeats() As IEnumerable(Of BaseBeat)
			Return Where(Function(i)
							 Return i.Type = EventType.AddOneshotBeat AndAlso
CType(i, BaseBeat).Hitable
						 End Function).Cast(Of BaseBeat)
		End Function
		Public Function HitBeats() As IEnumerable(Of Hit)
			Select Case _rowType
				Case RowType.Classic
					Return ClassicBeats().Select(Function(i) i.HitTimes).SelectMany(Function(i) i)
				Case RowType.Oneshot
					Return OneshotBeats().Select(Function(i) i.HitTimes).SelectMany(Function(i) i)
				Case Else
					Throw New RhythmBaseException("How?")
			End Select
		End Function
		Public Function PulseEvents() As IEnumerable(Of BaseBeat)
			Select Case _rowType
				Case RowType.Classic
					Return ClassicBeats()
				Case RowType.Oneshot
					Return OneshotBeats()
				Case Else
					Throw New RhythmBaseException("How?")
			End Select
		End Function
		Friend Function ShouldSerializeMuteBeats() As Boolean
			Return MuteBeats
		End Function
		Friend Function ShouldSerializeHideAtStart() As Boolean
			Return HideAtStart
		End Function
		Friend Function ShouldSerializeRowToMimic() As Boolean
			Return RowToMimic >= -1
		End Function
		Public Function CreateChildren(Of T As {BaseRowAction, New})(beatOnly As Single) As T
			Dim temp = New T With {
				.BeatOnly = beatOnly,
				.Parent = Me
			}
			Return temp
		End Function
		Public Function CreateChildren(Of T As {BaseRowAction, New})(item As T) As T
			Dim Temp As T = item.Clone(Of T)(Me)
			Return Temp
		End Function
		Public Function GetRowBeatStatus() As SortedDictionary(Of Single, Integer())
			Dim L As New SortedDictionary(Of Single, Integer())
			Select Case RowType
				Case RowType.Classic
					L.Add(0, New Integer(6) {})
					For Each beat In Me
						Select Case beat.Type
							Case EventType.AddClassicBeat
								Dim trueBeat = CType(beat, AddClassicBeat)
								For i = 0 To 6
									Dim statusArray As Integer() = If(L(beat.BeatOnly), New Integer(6) {})
									statusArray(i) += 1
									L(beat.BeatOnly) = statusArray
								Next
							Case EventType.AddFreeTimeBeat

							Case EventType.PulseFreeTimeBeat

							Case EventType.SetRowXs

						End Select
					Next
				Case RowType.Oneshot

				Case Else
					Throw New RhythmBaseException("How")
			End Select
		End Function
		Public Overrides Sub Add(item As BaseRowAction)
			If Not Parent.EventsBeatOrder.ContainsKey(item.BeatOnly) Then
				Parent.EventsBeatOrder.Add(item.BeatOnly, New List(Of BaseEvent))
			End If
			Parent.EventsBeatOrder(item.BeatOnly).Add(item)
			MyBase.Add(item)
		End Sub
		Public Overrides Function Remove(item As BaseRowAction) As Boolean
			Parent.EventsBeatOrder(item.BeatOnly).Remove(item)
			If Not Parent.EventsBeatOrder(item.BeatOnly).Any Then
				Parent.EventsBeatOrder.Remove(item.BeatOnly)
			End If
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
		Public Property BeatOnly As Single
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
		Public Property Tag As String 'throw new NotImplementedException()
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
		Friend ReadOnly Property _Rows As New List(Of Row)
		Friend ReadOnly Property _Decorations As New List(Of Decoration)
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
		Public ReadOnly Property Path As String
			Get
				Return _path
			End Get
		End Property
		'<JsonIgnore>
		'Friend Property CPBs As New List(Of SetCrotchetsPerBar)
		'<JsonIgnore>
		'Friend Property BPMs As New List(Of BaseBeatsPerMinute)
		<JsonIgnore>
		Public Overrides ReadOnly Property Count As Integer
			Get
				Dim count1 = EventsTypeOrder.Sum(Function(i) i.Value.Sum(Function(j) j.Value.Count)) + Rows.Sum(Function(i) i.Count) + Decorations.Sum(Function(i) i.Count)
				Dim count2 = EventsBeatOrder.Sum(Function(i) i.Value.Count)
				If count1 = count2 Then
					Return ConcatAll.Count
				End If
				Throw New RhythmBaseException($"Internal exception: {count1}, {count2}")
			End Get
		End Property
		<JsonIgnore>
		Public ReadOnly Property Assets As New HashSet(Of Sprite)
		<JsonIgnore>
		Public ReadOnly Property Variables As New Variables
		Public Sub New()
		End Sub
		Public Sub New(items As IEnumerable(Of BaseEvent))
			For Each item In items
				Me.Add(item)
			Next
		End Sub
		Public Function CreateDecoration(room As Rooms, parent As Sprite, Optional depth As Integer = 0, Optional visible As Boolean = True) As Decoration
			Assets.Add(parent)
			Dim temp As New Decoration(room, parent, depth, visible)
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
				RemoveRange(decoration)
				Return _Decorations.Remove(decoration)
			End If
			Return False
		End Function
		Public Function CreateRow(room As Rooms, character As Character, Optional visible As Boolean = True) As Row
			Dim temp As New Row() With {.Character = character, .Rooms = room, .Parent = Me, .HideAtStart = Not visible}
			_Rows.Add(temp)
			Return temp
		End Function
		Public Function RemoveRow(row As Row) As Boolean
			If Rows.Contains(row) Then
				Return _Rows.Remove(row)
			End If
			Return False
		End Function
		Private Function ToRDLevelJson(settings As LevelOutputSettings) As String
			Dim LevelSerializerSettings = New JsonSerializerSettings() With {
.Converters = {
New Converters.RDLevelConverter(_path, settings)
}
}
			Return JsonConvert.SerializeObject(Me, LevelSerializerSettings)
		End Function
		Public Shared Function ReadFromString(json As String, fileLocation As String, settings As LevelInputSettings) As RDLevel
			Dim LevelSerializerSettings = New JsonSerializerSettings() With {
.Converters = {
New Converters.RDLevelConverter(fileLocation, settings)
}
}
			json = Regex.Replace(json, ",(?=[ \n\r\t]*?[\]\)\}])", "")
			Dim level As RDLevel
			Try
				level = JsonConvert.DeserializeObject(Of RDLevel)(json, LevelSerializerSettings)
			Catch ex As Exception
				Throw New RhythmBaseException("File cannot be read.", ex)
			End Try
			Return level
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
			Dim json As String
			Select Case IO.Path.GetExtension(RDLevelFilePath)
				Case ".rdzip"
					json = File.ReadAllText(LoadZip(RDLevelFilePath).FullName)
				Case ".zip"
					json = File.ReadAllText(LoadZip(RDLevelFilePath).FullName)
				Case ".rdlevel"
					json = File.ReadAllText(RDLevelFilePath)
				Case Else
					Throw New RhythmBaseException("File not supported.")
			End Select
			Dim level = ReadFromString(json, RDLevelFilePath, settings)
			Return level
		End Function
		Public Sub SaveFile(filepath As String)
			IO.File.WriteAllText(filepath, ToRDLevelJson(New LevelOutputSettings))
		End Sub
		Public Sub SaveFile(filepath As String, settings As LevelOutputSettings)
			IO.File.WriteAllText(filepath, ToRDLevelJson(settings))
		End Sub
		Public Function GetHitBeat() As IEnumerable(Of Hit)
			Dim L As New List(Of Hit)
			For Each item In Rows
				L.AddRange(item.HitBeats)
			Next
			Return L
		End Function
		Public Function GetHitEvents() As IEnumerable(Of BaseBeat)
			Return Where(Of BaseBeat).Where(Function(i) i.Hitable)
		End Function
		Public Function CreateRow(character As Character) As Row
			Return New Row With {
				.Parent = Me,
				.Character = character
				}
		End Function
		Public Overrides Sub Add(item As BaseEvent)
			item.ParentLevel = Me
			If item.Type = EventType.Comment AndAlso CType(item, Comment).Parent Is Nothing Then
				MyBase.Add(item)
			ElseIf item.Type = EventType.TintRows AndAlso CType(item, TintRows).Parent Is Nothing Then
				MyBase.Add(item)
			ElseIf ConvertToEnums(Of BaseRowAction).Contains(item.Type) Then
				Throw New RhythmBaseException("Use `Row.Add()` instead.")
			ElseIf ConvertToEnums(Of BaseDecorationAction).Contains(item.Type) Then
				Throw New RhythmBaseException("Use `Decoration.Add()` instead.")
			Else
				MyBase.Add(item)
			End If
		End Sub
		Public Overrides Function Contains(item As BaseEvent) As Boolean
			Return (RowTypes.Contains(item.Type) AndAlso Rows.Any(Function(i) i.Contains(item))) OrElse
				(DecorationTypes.Contains(item.Type) AndAlso Decorations.Any(Function(i) i.Contains(item))) OrElse
				MyBase.Contains(item)
		End Function
		Public Overrides Function Remove(item As BaseEvent) As Boolean
			If RowTypes.Contains(item.Type) Then
				Return Rows.Any(Function(i) i.Remove(item))
			ElseIf DecorationTypes.Contains(item.Type) Then
				Return Decorations.Any(Function(i) i.Remove(item))
			End If
			If Contains(item) Then
				Dim result = EventsTypeOrder(item.Type)?(item.BeatOnly).Remove(item) And EventsBeatOrder(item.BeatOnly).Remove(item)
				If Not EventsTypeOrder(item.Type)(item.BeatOnly).Any Then
					EventsTypeOrder(item.Type).Remove(item.BeatOnly)
					If Not EventsTypeOrder(item.Type).Any Then
						EventsTypeOrder.Remove(item.Type)
					End If
				End If
				If Not EventsBeatOrder(item.BeatOnly).Any Then
					EventsBeatOrder.Remove(item.BeatOnly)
				End If
				Return result
			End If
			Return False
		End Function
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
		Public Property Version As Integer
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
		Public Property Mods As List(Of String)
		'''oldBassDrop
		'''startImmediately
		'''classicHitParticles
		'''adaptRowsToRoomHeight
		'''noSmartJudgment
		'''smoothShake
		'''rotateShake
		'''wobblyLines
		'''bombBeats
		'''noDoublePulse
		'''invisibleCharacters
		'''gentleBassDrop
	End Class
End Namespace