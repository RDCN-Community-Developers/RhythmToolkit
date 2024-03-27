Imports System.IO
Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports RhythmBase.Assets
Imports RhythmBase.Components
Imports RhythmBase.Events
Imports RhythmBase.Exceptions
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
        Public i As New LimitedList(Of Integer)(10, 0)
        Public f As New LimitedList(Of Single)(10, 0)
        Public b As New LimitedList(Of Boolean)(10, False)

        Public bpm As Single
        Public barNumber As Integer
        Public numEarlyHits As Integer
        Public numLateHits As Integer
        Public numPerfectHits As Integer
        Public numMisses As Integer

        Public numMistakes As Single
        Public numMistakesP1 As Single
        Public numMistakesP2 As Single

        Public p1Press As Single
        Public p2Press As Single
        Public p1Release As Single
        Public p2Release As Single
        Public p1IsPressed As Single
        Public p2IsPressed As Single
        Public anyPlayerPress As Single
        Public upPress As Single
        Public downPress As Single
        Public leftPress As Single
        Public rightPress As Single
        Public statusSignWidth As Single

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
        Public invisibleChars As Boolean
        Public invisibleHeart As Boolean
        Public noHitFlashBorder As Boolean
        Public noHitStrips As Boolean
        Public smoothShake As Boolean
        Public rotateShake As Boolean
        Public disableRowChangeWarningFlashes As Boolean
        Public skippableRankScreen As Boolean
        Public skipRankText As Boolean

        Public missesToCrackHeart As Integer

        Public booleansDefaultToTrue As Boolean
        Public adaptRowsToRoomHeight As Boolean

        Public autoplay As Boolean
        Public useFlashFontForFloatingText As Boolean
        Public shockwaveSizeMultiplier As Single
        Public shockwaveDistortionMultiplier As Single
        Public shockwaveDurationMultiplier As Single

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
        Function GetValue(variables As Variables) As Single
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
        Public Function GetValue(variables As Variables) As Single Implements INumOrExp.GetValue
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
        Public Function GetValue(variables As Variables) As Single Implements INumOrExp.GetValue
            Throw New NotImplementedException
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
        Public Function GetValue(variables As Variables) As (X As Single, Y As Single)
            Return (X.GetValue(variables), Y.GetValue(variables))
        End Function
        Public Shared Widening Operator CType(value As (x As INumOrExp, y As INumOrExp)) As NumOrExpPair
            Return New NumOrExpPair(value.x, value.y)
        End Operator
        Public Shared Widening Operator CType(value As (x As String, y As String)) As NumOrExpPair
            Return New NumOrExpPair(value.x, value.y)
        End Operator
        Public Overrides Function ToString() As String
            Return $"{{{X},{Y}}}"
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
    Public Class PanelColor
        '	Public Property Parent As LimitedList(Of SKColor)
        Private _panel As Integer
        Private _color As SKColor?
        Friend parent As LimitedList(Of SKColor)
        Public Property Color As SKColor?
            Get
                Return If(EnablePanel, parent(_panel), Nothing)
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
        Implements IEnumerable(Of T)
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
    Public Class Decoration
        '<JsonIgnore>
        'Public Parent As ISprite
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
                Return If(File?.Size, New Numerics.Vector2(32, 31))
            End Get
        End Property
        <JsonIgnore>
        Public ReadOnly Property Expressions As IEnumerable(Of String)
            Get
                Return If(File?.Expressions, New List(Of String))
            End Get
        End Property
        Public Property Row As ULong
        Public ReadOnly Property Rooms As New Rooms(False, False)
        <JsonProperty("filename")>
        Public Property File As ISprite
        Public Property Depth As Integer
        Public Property Visible As Boolean
        Sub New()
        End Sub
        Friend Sub New(room As Rooms, asset As ISprite, Optional depth As Integer = 0, Optional visible As Boolean = True)
            Me.Rooms.SetRooms(room)
            Me._id = Me.GetHashCode
            Me._Depth = depth
            Me._Visible = visible
            Me.File = asset
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
        Public Function CreateChildren(Of T As {BaseDecorationAction, New})(item As T) As T
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
        Public Property CpuMaker As Characters
        Public Property RowType As RowType
            Get
                Return _rowType
            End Get
            Set(value As RowType)
                If value <> _rowType Then
                    Children.Clear()
                    _rowType = value
                End If
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
            Return Children.Where(Function(i)
                                      Return (i.Type = EventType.AddClassicBeat Or
                                                    i.Type = EventType.AddFreeTimeBeat Or
                                                    i.Type = EventType.PulseFreeTimeBeat) AndAlso
                                                    CType(i, BaseBeat).Hitable
                                  End Function).Cast(Of BaseBeat)
        End Function
        Private Function OneshotBeats() As IEnumerable(Of BaseBeat)
            Return Children.Where(Function(i)
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
            Me.Children.Add(temp)
            Return temp
        End Function
        Public Function CreateChildren(Of T As {BaseRowAction, New})(item As T) As T
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
        Implements ICollection(Of BaseEvent)
        Friend _path As IO.FileInfo
        Public Property Settings As New Settings
        Friend ReadOnly Property _Rows As New List(Of Row)
        Friend ReadOnly Property _Decorations As New List(Of Decoration)
        Friend Property EventsBeatOrder As New SortedDictionary(Of Single, List(Of BaseEvent))
        Friend Property EventsTypeOrder As New Dictionary(Of EventType, SortedDictionary(Of Single, List(Of BaseEvent)))
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
        '<JsonIgnore>
        'Friend Property CPBs As New List(Of SetCrotchetsPerBar)
        '<JsonIgnore>
        'Friend Property BPMs As New List(Of BaseBeatsPerMinute)
        <JsonIgnore>
        Public ReadOnly Property Count As Integer Implements ICollection(Of BaseEvent).Count
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
        Private Function ToRDLevelJson(settings As LevelOutputSettings) As String
            Dim LevelSerializerSettings = New JsonSerializerSettings() With {
                    .Converters = {
                        New Converters.RDLevelConverter(_path, settings)
                    }
                }
            Return JsonConvert.SerializeObject(Me, LevelSerializerSettings)
        End Function
        Public Shared Function ReadFromString(json As String, fileLocation As IO.FileInfo, settings As LevelInputSettings) As RDLevel
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
        Public Function ConcatAll() As List(Of BaseEvent)
            Return EventsBeatOrder.SelectMany(Function(i) i.Value).ToList
        End Function
        Private Shared Function LoadZip(RDLevelFile As FileInfo) As FileInfo
            Dim tempDirectoryName As String = RDLevelFile.FullName
            Dim tempDirectory = New IO.DirectoryInfo(IO.Path.Combine(IO.Path.GetTempPath, IO.Path.GetRandomFileName))
            tempDirectory.Create()
            Try
                ZipFile.ExtractToDirectory(RDLevelFile.FullName, tempDirectory.FullName)
                Return tempDirectory.GetFiles.Where(Function(i) i.Name = "main.rdlevel").First
            Catch ex As Exception
                Throw New RhythmBaseException("Cannot extract the file.", ex)
            End Try
        End Function
        Public Shared Function LoadFile(RDLevelFile As FileInfo) As RDLevel
            Return LoadFile(RDLevelFile, New LevelInputSettings)
        End Function
        Public Shared Function LoadFile(RDLevelFile As FileInfo, settings As LevelInputSettings) As RDLevel
            Dim json As String
            Select Case RDLevelFile.Extension
                Case ".rdzip"
                    json = File.ReadAllText(LoadZip(RDLevelFile).FullName)
                Case ".zip"
                    json = File.ReadAllText(LoadZip(RDLevelFile).FullName)
                Case ".rdlevel"
                    json = File.ReadAllText(RDLevelFile.FullName)
                Case Else
                    Throw New RhythmBaseException("File not supported")
            End Select
            Dim level = ReadFromString(json, RDLevelFile, settings)
            Return level
        End Function
        Public Sub SaveFile(filepath As FileInfo)
            IO.File.WriteAllText(filepath.FullName, ToRDLevelJson(New LevelOutputSettings))
        End Sub
        Public Sub SaveFile(filepath As FileInfo, settings As LevelOutputSettings)
            IO.File.WriteAllText(filepath.FullName, ToRDLevelJson(settings))
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
        Public Function CreateRow(character As String) As Row
            Return New Row With {
                    .ParentCollection = Rows,
                    .Character = character
                }
        End Function
        Public Sub Add(item As BaseEvent) Implements ICollection(Of BaseEvent).Add
            item.ParentLevel = Me
            If Not EventsTypeOrder.ContainsKey(item.Type) Then
                EventsTypeOrder.Add(item.Type, New SortedDictionary(Of Single, List(Of BaseEvent)))
            End If
            If Not EventsTypeOrder(item.Type).ContainsKey(item.BeatOnly) Then
                EventsTypeOrder(item.Type).Add(item.BeatOnly, New List(Of BaseEvent))
            End If
            EventsTypeOrder(item.Type)(item.BeatOnly).Add(item)
            If Not EventsBeatOrder.ContainsKey(item.BeatOnly) Then
                EventsBeatOrder.Add(item.BeatOnly, New List(Of BaseEvent))
            End If
            EventsBeatOrder(item.BeatOnly).Add(item)
        End Sub
        Public Sub AddRange(items As IEnumerable(Of BaseEvent))
            For Each item In items
                Add(item)
            Next
        End Sub
        Public Sub Clear() Implements ICollection(Of BaseEvent).Clear
            EventsTypeOrder.Clear()
            EventsBeatOrder.Clear()
        End Sub
        Public Function Contains(item As BaseEvent) As Boolean Implements ICollection(Of BaseEvent).Contains
            Return EventsTypeOrder(item.Type)(item.BeatOnly).Contains(item)
        End Function
        Public Function Where(predicate As Func(Of BaseEvent, Boolean)) As IEnumerable(Of BaseEvent)
            Return ConcatAll.Where(predicate)
        End Function
        Public Function Where(Of T As BaseEvent)() As IEnumerable(Of T)
            Return From type In ConvertToEnums(Of T)()
                   Where EventsTypeOrder.ContainsKey(type)
                   From pair In EventsTypeOrder(type)
                   From item In pair.Value.Cast(Of T)
                   Select item
        End Function
        Public Function Where(Of T As BaseEvent)(predicate As Func(Of T, Boolean)) As IEnumerable(Of T)
            Return From type In ConvertToEnums(Of T)()
                   Where EventsTypeOrder.ContainsKey(type)
                   From pair In EventsTypeOrder(type)
                   From item In pair.Value.Cast(Of T)
                   Where predicate(item)
                   Select item
        End Function
        Public Function First() As BaseEvent
            Return EventsBeatOrder.First.Value.First
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
            Return EventsBeatOrder.FirstOrDefault.Value?.FirstOrDefault
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
            Return EventsBeatOrder.Last.Value.Last
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
            Return EventsBeatOrder.LastOrDefault.Value?.LastOrDefault()
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
            Return From item In ConcatAll()
                   Select predicate(item)
        End Function
        Public Sub CopyTo(array() As BaseEvent, arrayIndex As Integer) Implements ICollection(Of BaseEvent).CopyTo
            ConcatAll.CopyTo(array, arrayIndex)
        End Sub
        Public Function Remove(item As BaseEvent) As Boolean Implements ICollection(Of BaseEvent).Remove
            Try
                EventsTypeOrder(item.Type)(item.BeatOnly).Remove(item)
            Catch
            End Try
            If EventsBeatOrder.ContainsKey(item.BeatOnly) Then
                If EventsBeatOrder(item.BeatOnly).Count = 1 Then
                    EventsBeatOrder(item.BeatOnly).Remove(item)
                    EventsBeatOrder.Remove(item.BeatOnly)
                    Return True
                Else
                    Return EventsBeatOrder(item.BeatOnly).Remove(item)
                End If
            End If
            Return False
        End Function
        Public Function RemoveAll(predicate As Func(Of BaseEvent, Boolean)) As Integer
            Dim count As UInteger = 0
            Dim l = Where(predicate)
            For Each item In l
                count += Remove(item)
            Next
            Return count
        End Function
        Friend Function GetEnumerator() As IEnumerator(Of BaseEvent) Implements IEnumerable(Of BaseEvent).GetEnumerator
            Return EventsBeatOrder.SelectMany(Function(i) i.Value).GetEnumerator
        End Function
        Public Sub RefreshCPBs()
            BeatCalculator.Initialize(EventsTypeOrder(EventType.SetCrotchetsPerBar))
        End Sub
        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
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
    End Class
End Namespace
Namespace Exceptions

    Public Class RhythmBaseException
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
End Namespace