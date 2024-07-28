Imports Newtonsoft.Json
Imports SkiaSharp
'#Disable Warning CA1507

Namespace Events
	Public Interface IEaseEvent
		Property Ease As EaseType
		Property Duration As Single
	End Interface
	''' <summary>
	''' Rhythm Doctor event types.
	''' </summary>
	Public Enum EventType
		AddClassicBeat
		AddFreeTimeBeat
		AddOneshotBeat
		AdvanceText
		BassDrop
		CallCustomMethod
		ChangePlayersRows
		Comment
		''' <summary>
		''' Custom decoration event, from unknown event in the level or user-defined event.
		''' </summary>
		CustomDecorationEvent
		''' <summary>
		''' Custom event, from unknown event in the level or user-defined event.
		''' </summary>
		CustomEvent
		''' <summary>
		''' Custom row event, from unknown event in the level or user-defined event.
		''' </summary>
		CustomRowEvent
		CustomFlash
		FadeRoom
		FinishLevel
		Flash
		FlipScreen
		FloatingText
		HideRow
		InvertColors
		MaskRoom
		Move
		MoveCamera
		MoveRoom
		MoveRow
		NarrateRowInfo
		NewWindowDance
		PaintHands
		PlayAnimation
		PlayExpression
		PlaySong
		PlaySound
		PulseCamera
		PulseFreeTimeBeat
		ReadNarration
		ReorderRooms
		SayReadyGetSetGo
		SetBackgroundColor
		SetBeatSound
		SetBeatsPerMinute
		SetClapSounds
		SetCountingSound
		SetCrotchetsPerBar
		SetForeground
		SetGameSound
		SetHandOwner
		SetHeartExplodeInterval
		SetHeartExplodeVolume
		SetOneshotWave
		SetPlayStyle
		SetRoomContentMode
		SetRoomPerspective
		SetRowXs
		SetSpeed
		SetTheme
		SetVFXPreset
		SetVisible
		ShakeScreen
		ShowDialogue
		ShowHands
		ShowRooms
		ShowStatusSign
		Stutter
		TagAction
		TextExplosion
		Tile
		Tint
		TintRows
	End Enum
	Public Enum PlayerType
		P1
		P2
		CPU
		NoChange
	End Enum
	Public Enum TilingTypes
		Scroll
		Pulse
	End Enum
	Public Enum ContentModes
		ScaleToFill
		AspectFit
		AspectFill
		Center
		Tiled
	End Enum
	Public Enum DefaultAudios
		sndTutorialHouse_Base
		sndTutorialHouse_Rest
		sndTutorialHouse_AmenFill
		sndTutorialHouse_Freeze1
		sndTutorialHouse_Freeze2
		sndTutorialHouse_FreezeCPU
		sndTutorialHouse_Burn1
		sndTutorialHouse_Burn2
		sndTutorialHouse_BurnCPU
	End Enum
	Public Enum RowType
		Classic
		Oneshot
	End Enum
	<JsonConverter(GetType(TabsConverter))>
	Public Enum Tabs
		Sounds
		Rows
		Actions
		Decorations
		Rooms
		Unknown
	End Enum
	Public Enum PlayerHands
		Left
		Right
		Both
		p1
		p2
	End Enum
	Public Enum Borders
		None
		Outline
		Glow
	End Enum
	<JsonConverter(GetType(PatternConverter))> Public Enum Patterns
		None
		X
		Up
		Down
		Banana
		[Return]
	End Enum
	Public Interface IRoomEvent
		Property Rooms As Room
	End Interface
	Public Interface ISingleRoomEvent
		Property Room As SingleRoom
	End Interface
	Public Interface IBarBeginningEvent
	End Interface
	Public MustInherit Class BaseEvent
		''' <summary>
		''' Event type.
		''' </summary>
		<JsonIgnore> Public MustOverride ReadOnly Property Type As EventType
		''' <summary>
		''' Column to which the event belongs.
		''' </summary>
		<JsonIgnore> Public MustOverride ReadOnly Property Tab As Tabs
		Friend _beat As New Beat(1)
		''' <summary>
		''' The beat of the event.
		''' </summary>
		<JsonIgnore> Public Overridable Property Beat As Beat
			Get
				Return _beat
			End Get
			Set(value As Beat)
				If _beat.baseLevel Is Nothing Then
					If value.baseLevel Is Nothing Then
						_beat = value
					Else
						_beat = value.WithoutBinding()
					End If
				Else
					value = New Beat(_beat.baseLevel.Calculator, value)
					_beat.baseLevel.Remove(Me)
					value.baseLevel.Add(Me)
					_beat = value
				End If
			End Set
		End Property
		''' <summary>
		''' The number of rows this event is on
		''' </summary>
		Public Overridable Property Y As Integer
		''' <summary>
		''' Event tag.
		''' </summary>
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property Tag As String
		''' <summary>
		''' Event conditions.
		''' </summary>
		<JsonProperty("if", DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property Condition As Condition
		Public Property Active As Boolean = True
		''' <summary>
		''' Clone this event and its basic properties.
		''' If it is of the same type as the source event, then it will be cloned.
		''' </summary>
		''' <typeparam name="T">Type that will be generated.</typeparam>
		''' <returns></returns>
		Public Overridable Function Clone(Of T As {BaseEvent, New})() As T
			If ConvertToEnum(Of T)() = Type Then
				Dim e = CType(MemberwiseClone(), T)
				e._beat = Beat.WithoutBinding()
				Return e
			End If
			Dim temp = New T With {.Beat = Beat.WithoutBinding, .Y = Y, .Condition = Condition, .Tag = Tag, .Active = Active}
			If Me.Condition IsNot Nothing Then
				For Each item In Me.Condition.ConditionLists
					temp.Condition.ConditionLists.Add(item)
				Next
			End If
			Return temp
		End Function
		Friend Overridable Function Clone(Of T As {BaseEvent, New})(level As RDLevel) As T
			Dim temp = New T With {.Beat = Beat.WithoutBinding, .Y = Y, .Condition = Condition, .Tag = Tag, .Active = Active}
			If Me.Condition IsNot Nothing Then
				For Each item In Me.Condition.ConditionLists
					temp.Condition.ConditionLists.Add(item)
				Next
			End If
			Return temp
		End Function
		Public Overrides Function ToString() As String
			Return $"{Beat} {Type}"
		End Function
	End Class
	Public MustInherit Class BaseBeatsPerMinute
		Inherits BaseEvent
		Private _bpm As Single = 100
		Public Overrides Property Beat As Beat
			Get
				Return MyBase.Beat
			End Get
			Set(value As Beat)
				MyBase.Beat = value
				ResetTimeLine()
			End Set
		End Property
		<JsonProperty("bpm")> Public Property BeatsPerMinute As Single
			Get
				Return _bpm
			End Get
			Set(value As Single)
				_bpm = value
				ResetTimeLine()
			End Set
		End Property
		Private Sub ResetTimeLine()
			If Beat.baseLevel IsNot Nothing Then
				For Each item In Beat.baseLevel.Where(Function(i) i.Beat > Me.Beat)
					item.Beat.ResetBPM()
				Next
			End If
		End Sub
	End Class
	Public MustInherit Class BaseDecorationAction
		Inherits BaseEvent
		Friend _parent As DecorationEventCollection
		<JsonIgnore> Public ReadOnly Property Parent As DecorationEventCollection
			Get
				Return _parent
			End Get
			'Set(value As Decoration)
			'	If _parent IsNot Nothing Then
			'		_parent.Remove(Me)
			'		value?.Add(Me)
			'	End If
			'	_parent = value
			'End Set
		End Property
		Public Overridable ReadOnly Property Target As String
			Get
				Return If(Parent Is Nothing, "", Parent.Id)
			End Get
		End Property
		''' <summary>
		''' Clone this event and its basic properties. Clone will be added to the level.
		''' </summary>
		''' <typeparam name="T">Type that will be generated.</typeparam>
		''' <returns></returns>
		Public Overloads Function Clone(Of T As {BaseDecorationAction, New})() As T
			Dim Temp = MyBase.Clone(Of T)()
			Temp._parent = Parent
			Return Temp
		End Function
		Friend Overloads Function Clone(Of T As {BaseDecorationAction, New})(decoration As DecorationEventCollection) As T
			Dim Temp = MyBase.Clone(Of T)(decoration.Parent)
			Temp._parent = decoration
			Return Temp
		End Function
		<JsonIgnore> Public ReadOnly Property Room As SingleRoom
			Get
				If Parent Is Nothing Then
					Return SingleRoom.Default
				End If
				Return Parent.Room
			End Get
		End Property
	End Class
	Public MustInherit Class BaseRowAction
		Inherits BaseEvent
		Friend _parent As RowEventCollection
		<JsonIgnore> Public Property Parent As RowEventCollection
			Get
				Return _parent
			End Get
			Friend Set(value As RowEventCollection)
				If _parent IsNot Nothing Then
					_parent.Remove(Me)
					value?.Add(Me)
				End If
				_parent = value
			End Set
		End Property
		<JsonIgnore> Public ReadOnly Property Room As SingleRoom
			Get
				If _parent Is Nothing Then
					Return SingleRoom.Default
				End If
				Return Parent.Rooms
			End Get
		End Property
		<JsonIgnore> <Obsolete("This function is obsolete and may be removed in the next release. Use Index instead.")> Public ReadOnly Property Row As Integer
		<JsonProperty("row", DefaultValueHandling:=DefaultValueHandling.Include)> Public ReadOnly Property Index As Integer
			Get
				Return If(Parent?.Index, -1)
			End Get
		End Property
		''' <summary>
		''' Clone this event and its basic properties. Clone will be added to the level.
		''' </summary>
		''' <typeparam name="T">Type that will be generated.</typeparam>
		''' <returns></returns>
		Public Overloads Function Clone(Of T As {BaseRowAction, New})() As T
			Dim Temp = MyBase.Clone(Of T)()
			Temp.Parent = Parent
			Return Temp
		End Function
		Friend Overloads Function Clone(Of T As {BaseRowAction, New})(row As RowEventCollection) As T
			Dim Temp = MyBase.Clone(Of T)(row.Parent)
			Temp.Parent = row
			Return Temp
		End Function
	End Class
	Public MustInherit Class BaseBeat
		Inherits BaseRowAction
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rows
	End Class
	Public MustInherit Class BaseRowAnimation
		Inherits BaseRowAction
	End Class
	Public Class CustomEvent
		Inherits BaseEvent
		<JsonIgnore> Public Data As New Linq.JObject
		<JsonIgnore> Public Overrides ReadOnly Property Type As EventType = EventType.CustomEvent
		<JsonIgnore> Public ReadOnly Property ActureType As String
			Get
				Return Data(NameOf(Type).ToLowerCamelCase).ToString
			End Get
		End Property
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Unknown
		Public Overrides Property Y As Integer
			Get
				Return CInt(If(Data(NameOf(Y).ToLowerCamelCase), 0))
			End Get
			Set(value As Integer)
				Data(NameOf(Y).ToLowerCamelCase) = value
			End Set
		End Property
		Public Sub New()
			Data = New Linq.JObject
		End Sub
		Public Sub New(data As Linq.JObject)
			Me.Data = data
			Beat = New Beat(data("bar").ToObject(Of UInteger), If(data("beat")?.ToObject(Of Single), 1))
			Tag = data("tag")?.ToObject(Of String)
			Condition = If(data("condition") Is Nothing, Nothing, Condition.Load(data("condition")?.ToObject(Of String)))
			Active = If(data("active")?.ToObject(Of Boolean), True)
		End Sub
		Public Overrides Function ToString() As String
			Return $"{Beat} *{ActureType}"
		End Function
		Public Overridable Function TryConvert(ByRef value As BaseEvent, ByRef type As EventType?) As Boolean
			Return TryConvert(value, type, New LevelReadOrWriteSettings)
		End Function
		Public Overridable Function TryConvert(ByRef value As BaseEvent, ByRef type As EventType?, settings As LevelReadOrWriteSettings) As Boolean
			Dim serializer = JsonSerializer.Create(_beat.baseLevel.GetSerializer(settings))
			Dim eventType = ConvertToType(Data("type"))
			If eventType Is Nothing Then
				Dim TempEvent As BaseEvent
				If Data("target") IsNot Nothing Then
					TempEvent = Data.ToObject(Of CustomDecorationEvent)(serializer)
				ElseIf Data("row") IsNot Nothing Then
					TempEvent = Data.ToObject(Of CustomRowEvent)(serializer)
				Else
					TempEvent = Data.ToObject(Of CustomEvent)(serializer)
				End If
				value = TempEvent
				type = Nothing
				Return False
			Else
				Dim TempEvent As BaseEvent = Data.ToObject(eventType, serializer)
				value = TempEvent
				type = [Enum].Parse(Of EventType)(Data("type"))
				Return True
			End If
		End Function
	End Class
	Public Class CustomDecorationEvent
		Inherits BaseDecorationAction
		Public Data As New Linq.JObject
		Public Overrides ReadOnly Property Type As EventType = EventType.CustomDecorationEvent
		<JsonIgnore> Public ReadOnly Property ActureType As String
			Get
				Return Data(NameOf(Type).ToLowerCamelCase).ToString
			End Get
		End Property
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Decorations
		Public Sub New()
			Data = New Linq.JObject
		End Sub
		Public Sub New(data As Linq.JObject)
			Me.Data = data
			Beat = New Beat(data("bar").ToObject(Of UInteger), If(data("beat")?.ToObject(Of Single), 1))
			Tag = data("tag")?.ToObject(Of String)
			Condition = If(data("condition") Is Nothing, Nothing, Condition.Load(data("condition")?.ToObject(Of String)))
			Active = If(data("active")?.ToObject(Of Boolean), True)
		End Sub
		Public Overrides Function ToString() As String
			Return $"{Beat} *{ActureType}"
		End Function
		Public Overridable Function TryConvert(ByRef value As BaseEvent, ByRef type As EventType?) As Boolean
			Return TryConvert(value, type, New LevelReadOrWriteSettings)
		End Function
		Public Overridable Function TryConvert(ByRef value As BaseEvent, ByRef type As EventType?, settings As LevelReadOrWriteSettings) As Boolean
			Return CType(Me, CustomEvent).TryConvert(value, type, settings)
		End Function
		Public Shared Widening Operator CType(e As CustomDecorationEvent) As CustomEvent
			Return New CustomEvent(e.Data)
		End Operator
		Public Shared Narrowing Operator CType(e As CustomEvent) As CustomDecorationEvent
			If e.Data("row") IsNot Nothing Then
				Return New CustomDecorationEvent(e.Data)
			End If
			Throw New RhythmBaseException("The row field is missing from the field contained in this object.")
		End Operator
	End Class
	Public Class CustomRowEvent
		Inherits BaseRowAction
		Public Data As New Linq.JObject
		Public Overrides ReadOnly Property Type As EventType = EventType.CustomRowEvent
		<JsonIgnore> Public ReadOnly Property ActureType As String
			Get
				Return Data(NameOf(Type).ToLowerCamelCase).ToString
			End Get
		End Property
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rows
		Public Sub New()
			Data = New Linq.JObject
		End Sub
		Public Sub New(data As Linq.JObject)
			Me.Data = data
			Beat = New Beat(data("bar").ToObject(Of UInteger), If(data("beat")?.ToObject(Of Single), 1))
			Tag = data("tag")?.ToObject(Of String)
			Condition = If(data("condition") Is Nothing, Nothing, Condition.Load(data("condition")?.ToObject(Of String)))
			Active = If(data("active")?.ToObject(Of Boolean), True)
		End Sub
		Public Overrides Function ToString() As String
			Return $"{Beat} *{ActureType}"
		End Function
		Public Overridable Function TryConvert(ByRef value As BaseEvent, ByRef type As EventType?) As Boolean
			Return TryConvert(value, type, New LevelReadOrWriteSettings)
		End Function
		Public Overridable Function TryConvert(ByRef value As BaseEvent, ByRef type As EventType?, settings As LevelReadOrWriteSettings) As Boolean
			Return CType(Me, CustomEvent).TryConvert(value, type, settings)
		End Function
		Public Shared Widening Operator CType(e As CustomRowEvent) As CustomEvent
			Return New CustomEvent(e.Data)
		End Operator
		Public Shared Narrowing Operator CType(e As CustomEvent) As CustomRowEvent
			If e.Data("row") IsNot Nothing Then
				Return New CustomRowEvent(e.Data)
			End If
			Throw New RhythmBaseException("The row field is missing from the field contained in this object.")
		End Operator
	End Class
	Public Class PlaySong
		Inherits BaseBeatsPerMinute
		Implements IBarBeginningEvent
		Public Song As Audio
		<JsonIgnore> Public Property Offset As TimeSpan
			Get
				Return Song.Offset
			End Get
			Set(value As TimeSpan)
				Song.Offset = value
			End Set
		End Property
		Public Property [Loop] As Boolean
		Public Overrides ReadOnly Property Type As EventType = EventType.PlaySong
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" BPM:{BeatsPerMinute}, Song:{Song.Filename}"
		End Function
	End Class
	Public Class SetBeatsPerMinute
		Inherits BaseBeatsPerMinute
		Public Overrides ReadOnly Property Type As EventType = EventType.SetBeatsPerMinute
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" BPM:{BeatsPerMinute}"
		End Function
	End Class
	Public Class SetCrotchetsPerBar
		Inherits BaseEvent
		Implements IBarBeginningEvent
		Private _visualBeatMultiplier As Single = 0
		Protected Friend _crotchetsPerBar As UInteger = 7
		Public Overrides ReadOnly Property Type As EventType = EventType.SetCrotchetsPerBar
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
		Public Property VisualBeatMultiplier As Single
			Get
				Return _visualBeatMultiplier + 1
			End Get
			Set(value As Single)
				If value < 1 Then
					Throw New OverflowException("VisualBeatMultiplier must greater than 1.")
				End If
				_visualBeatMultiplier = value - 1
			End Set
		End Property
		Public Property CrotchetsPerBar As UInteger
			Get
				Return _crotchetsPerBar + 1
			End Get
			Set(value As UInteger)
				_crotchetsPerBar = value - 1
				If _beat._calculator IsNot Nothing Then
					Beat += 0
				End If
			End Set
		End Property
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" CPB:{_crotchetsPerBar + 1}"
		End Function
	End Class
	Public Class PlaySound
		Inherits BaseEvent
		Enum CustomSoundTypes
			CueSound
			MusicSound
			BeatSound
			HitSound
			OtherSound
		End Enum
		Public Property IsCustom As Boolean
		Public Property CustomSoundType As CustomSoundTypes
		Public Property Sound As Audio
		Public Overrides ReadOnly Property Type As EventType = EventType.PlaySound
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {If(IsCustom, Sound.ToString, CustomSoundType.ToString)}"
		End Function
	End Class
	Public Class SetClapSounds
		Inherits BaseEvent
		Public Property P1Sound As Audio
		Public Property P2Sound As Audio
		Public Property CpuSound As Audio
		Public Property RowType As RowType
		Public Overrides ReadOnly Property Type As EventType = EventType.SetClapSounds
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
	End Class
	Public Class SetHeartExplodeVolume
		Inherits BaseEvent
		Implements IBarBeginningEvent
		Public Property Volume As UInteger
		Public Overrides ReadOnly Property Type As EventType = EventType.SetHeartExplodeVolume
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
	End Class
	Public Class SetHeartExplodeInterval
		Inherits BaseEvent
		Enum IntervalTypes
			OneBeatAfter
			Instant
			GatherNoCeil
			GatherAndCeil
		End Enum
		Public Property IntervalType As IntervalTypes
		Public Property Interval As Integer
		Public Overrides ReadOnly Property Type As EventType = EventType.SetHeartExplodeInterval
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
	End Class
	Public Class SayReadyGetSetGo
		Inherits BaseEvent
		Enum Words
			SayReaDyGetSetGoNew
			SayGetSetGo
			SayReaDyGetSetOne
			SayGetSetOne
			JustSayRea
			JustSayDy
			JustSayGet
			JustSaySet
			JustSayAnd
			JustSayGo
			JustSayStop
			JustSayAndStop
			Count1
			Count2
			Count3
			Count4
			Count5
			Count6
			Count7
			Count8
			Count9
			Count10
			SayReadyGetSetGo
			JustSayReady
		End Enum
		Enum VoiceSources
			Nurse
			NurseTired
			NurseSwing
			NurseSwingCalm
			IanExcited
			IanCalm
			IanSlow
			NoneBottom
			NoneTop
		End Enum
		Public Property PhraseToSay As Words
		Public Property VoiceSource As VoiceSources
		Public Property Tick As Single
		Public Property Volume As UInteger
		Public Overrides ReadOnly Property Type As EventType = EventType.SayReadyGetSetGo
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
		<JsonIgnore> Public ReadOnly Property Splitable As Boolean
			Get
				Return PhraseToSay = Words.SayReaDyGetSetGoNew OrElse
PhraseToSay = Words.SayGetSetGo OrElse
PhraseToSay = Words.SayReaDyGetSetOne OrElse
PhraseToSay = Words.SayGetSetOne OrElse
PhraseToSay = Words.SayReadyGetSetGo
			End Get
		End Property
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {VoiceSource}: {PhraseToSay}"
		End Function
	End Class
	Public Class SetGameSound
		Inherits BaseEvent
		Enum SoundTypes
			SmallMistake
			BigMistake
			Hand1PopSound
			Hand2PopSound
			HeartExplosion
			HeartExplosion2
			HeartExplosion3
			Skipshot
			ClapSoundHold
			FreezeshotSound
			BurnshotSound
		End Enum
		Private Property Audio As New Audio
		Public Property SoundType As SoundTypes
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
		Public Property SoundSubtypes As List(Of SoundSubType)
		Public Overrides ReadOnly Property Type As EventType = EventType.SetGameSound
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
		Private Function ShouldSerialize() As Boolean
			Return Not (
SoundType = SoundTypes.ClapSoundHold Or
SoundType = SoundTypes.FreezeshotSound Or
SoundType = SoundTypes.BurnshotSound)
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
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {SoundType}"
		End Function
	End Class
	Public Class SetBeatSound
		Inherits BaseRowAction
		Public Property Sound As New Audio
		Public Overrides ReadOnly Property Type As EventType = EventType.SetBeatSound
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
	End Class
	Public Class SetCountingSound
		Inherits BaseRowAction
		Enum VoiceSources
			'ClassicBeat
			JyiCount
			JyiCountFast
			JyiCountCalm
			JyiCountTired
			JyiCountVeryTired
			JyiCountJapanese
			IanCount
			IanCountFast
			IanCountCalm
			IanCountSlow
			IanCountSlower
			WhistleCount
			BirdCount
			ParrotCount
			OwlCount
			OrioleCount
			WrenCount
			CanaryCount
			JyiCountLegacy
			'OnshotBeat
			JyiCountEnglish
			IanCountEnglish
			IanCountEnglishCalm
			IanCountEnglishSlow
			Custom
		End Enum
		Public Property VoiceSource As VoiceSources
		Public Property Enabled As Boolean
		Public Property SubdivOffset As Single
		Public Property Volume As Integer = 100
		Public Property Sounds As New LimitedList(Of Audio)(7, New Audio)
		Public Overrides ReadOnly Property Type As EventType = EventType.SetCountingSound
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sounds
		Public Function ShouldSerializeSounds() As Boolean
			Return VoiceSource = VoiceSources.Custom
		End Function
	End Class
	Public Class SetTheme
		Inherits BaseEvent
		Implements IRoomEvent
		Enum Theme
			None
			Intimate
			IntimateSimple
			InsomniacDay
			InsomniacNight
			Matrix
			NeonMuseum
			CrossesStraight
			CrossesFalling
			CubesFalling
			CubesFallingNiceBlue
			OrientalTechno
			Kaleidoscope
			PoliticiansRally
			Rooftop
			RooftopSummer
			RooftopAutumn
			BackAlley
			Sky
			NightSky
			HallOfMirrors
			CoffeeShop
			CoffeeShopNight
			Garden
			GardenNight
			TrainDay
			TrainNight
			DesertDay
			DesertNight
			HospitalWard
			HospitalWardNight
			PaigeOffice
			Basement
			ColeWardNight
			ColeWardSunrise
			BoyWard
			GirlWard
			Skyline
			SkylineBlue
			FloatingHeart
			FloatingHeartWithCubes
			FloatingHeartBroken
			FloatingHeartBrokenWithCubes
			ZenGarden
			Space
			Vaporwave
			RollerDisco
			Stadium
			StadiumStormy
			AthleteWard
			AthleteWardNight
			ProceduralTree
		End Enum
		Public Property Preset As Theme = Theme.None
		Public Property [Variant] As Byte
		Public Overrides ReadOnly Property Type As EventType = EventType.SetTheme
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Property Rooms As New Room(False, 0) Implements IRoomEvent.Rooms
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Preset}"
		End Function
	End Class
	Public Class SetVFXPreset
		Inherits BaseEvent
		Implements IEaseEvent
		Implements IRoomEvent
		Enum Presets
			SilhouettesOnHBeat
			Vignette
			VignetteFlicker
			ColourfulShockwaves
			BassDropOnHit
			ShakeOnHeartBeat
			ShakeOnHit
			WavyRows
			LightStripVert
			VHS
			CutsceneMode
			HueShift
			Brightness
			Contrast
			Saturation
			Noise
			GlitchObstruction
			Rain
			Matrix
			Confetti
			FallingPetals
			FallingPetalsInstant
			FallingPetalsSnow
			Snow
			Bloom
			OrangeBloom
			BlueBloom
			HallOfMirrors
			TileN
			Sepia
			CustomScreenScroll
			JPEG
			NumbersAbovePulses
			Mosaic
			ScreenWaves
			Funk
			Grain
			Blizzard
			Drawing
			Aberration
			Blur
			RadialBlur
			Dots
			DisableAll
			Diamonds
			Tutorial

			'旧版特效
			BlackAndWhite
			Blackout
			ScreenScrollX
			ScreenScroll
			ScreenScrollXSansVHS
			ScreenScrollSansVHS
			RowGlowWhite
			RowAllWhite
			RowOutline
			RowShadow
			RowSilhouetteGlow
			RowPlain
			Tile2
			Tile3
			Tile4
		End Enum
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property Preset As Presets
		Public Property Enable As Boolean
		Public Property Threshold As Single
		Public Property Intensity As Single
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Include)> Public Property Color As New PaletteColor(False)
		Public Property FloatX As Single
		Public Property FloatY As Single
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As EventType = EventType.SetVFXPreset
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Preset}"
		End Function
		Public Function ShouldSerializeEnable() As Boolean
			Return Preset <> Presets.DisableAll
		End Function
		Public Function ShouldSerializeThreshold() As Boolean '阈值
			Return Enable AndAlso Preset = Presets.Bloom
		End Function
		Public Function ShouldSerializeIntensity() As Boolean '强度
			Return Enable AndAlso PropertyHasDuration() And
Preset <> Presets.TileN And
Preset <> Presets.CustomScreenScroll
		End Function
		Public Function ShouldSerializeColor() As Boolean
			Return Enable AndAlso Preset = Presets.Bloom
		End Function
		Public Function ShouldSerializeFloatX() As Boolean
			Return Enable AndAlso Preset = Presets.TileN Or
Preset = Presets.CustomScreenScroll
		End Function
		Public Function ShouldSerializeFloatY() As Boolean
			Return Enable AndAlso Preset = Presets.TileN Or
Preset = Presets.CustomScreenScroll
		End Function
		Public Function ShouldSerializeEase() As Boolean
			Return Enable AndAlso PropertyHasDuration()
		End Function
		Public Function ShouldSerializeDuration() As Boolean
			Return Enable AndAlso PropertyHasDuration()
		End Function
		Private Function PropertyHasDuration()
			Return {
Presets.HueShift,
Presets.Brightness,
Presets.Contrast,
Presets.Saturation,
Presets.Rain,
Presets.Bloom,
Presets.TileN,
Presets.CustomScreenScroll,
Presets.JPEG,
Presets.Mosaic,
Presets.ScreenWaves,
Presets.Grain,
Presets.Blizzard,
Presets.Drawing,
Presets.Aberration,
Presets.Blur,
Presets.RadialBlur,
Presets.Dots
}.Contains(Preset)
		End Function
	End Class
	Public Class SetBackgroundColor
		Inherits BaseEvent
		Implements IEaseEvent
		Implements IRoomEvent
		Enum BackgroundTypes
			Color
			Image
		End Enum
		Enum FilterModes
			NearestNeighbor
		End Enum
		Public Property Rooms As New Room(False, 0) Implements IRoomEvent.Rooms
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property ContentMode As ContentModes
		Public Property Filter As FilterModes '?
		Public Property Color As New PaletteColor(True)
		Public Property Interval As Single
		Public Property BackgroundType As BackgroundTypes
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Fps As Integer
		Public Property Image As List(Of Sprite)
		Public Property ScrollX As Integer
		Public Property ScrollY As Integer
		Public Property TilingType As TilingTypes
		Public Overrides ReadOnly Property Type As EventType = EventType.SetBackgroundColor
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {If(BackgroundType = BackgroundTypes.Color, Color.ToString, String.Join(","c, Image.Select(Function(i) i.ToString)))}"
		End Function
	End Class
	Public Class SetForeground
		Inherits BaseEvent
		Implements IEaseEvent
		Implements IRoomEvent
		Public Property Rooms As New Room(False, 0) Implements IRoomEvent.Rooms
		Public Property ContentMode As ContentModes
		Public Property TilingType As TilingTypes
		Public Property Color As New PaletteColor(True)
		Public Property Image As List(Of Sprite)
		Public Property Fps As Single
		Public Property ScrollX As Single
		Public Property ScrollY As Single
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Interval As Single
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.SetForeground
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Color},{String.Join(","c, Image.Select(Function(i) i.ToString))}"
		End Function
	End Class
	Public Class SetSpeed
		Inherits BaseEvent
		Implements IEaseEvent
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Speed As Single
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As EventType = EventType.SetSpeed
		<JsonIgnore> Public Property Rooms As Room = Room.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" Speed:{Speed}"
		End Function
	End Class
	Public Class Flash
		Inherits BaseEvent
		Enum Durations
			[Short]
			Medium
			[Long]
		End Enum
		Public Property Rooms As New Room(True, 0)
		Public Property Duration As Durations
		Public Overrides ReadOnly Property Type As EventType = EventType.Flash
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Duration}"
		End Function
	End Class
	Public Class CustomFlash
		Inherits BaseEvent
		Implements IEaseEvent
		Implements IRoomEvent
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property StartColor As New PaletteColor(False)
		Public Property Background As Boolean
		Public Property EndColor As New PaletteColor(False)
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property StartOpacity As Integer
		Public Property EndOpacity As Integer
		Public Overrides ReadOnly Property Type As EventType = EventType.CustomFlash
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {StartColor}=>{EndColor}"
		End Function
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class MoveCamera
		Inherits BaseEvent
		Implements IEaseEvent
		Implements IRoomEvent
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property CameraPosition As PointE?
		Public Property Zoom As Integer?
		Public Property Angle As Expression?
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.MoveCamera
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class HideRow
		Inherits BaseRowAnimation
		Enum Transitions
			Smooth
			Instant
			Full
		End Enum
		Enum Shows
			Visible
			Hidden
			OnlyCharacter
			OnlyRow
		End Enum
		Public Property Transition As Transitions
		Public Property Show As Shows
		Public Overrides ReadOnly Property Type As EventType = EventType.HideRow
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class MoveRow
		Inherits BaseRowAnimation
		Implements IEaseEvent
		Enum Targets
			WholeRow
			Heart
			Character
		End Enum
		Public Property CustomPosition As Boolean
		Public Property Target As Targets
		Public Property RowPosition As PointE?
		Public Property Scale As PointE?
		Public Property Angle As Expression?
		Public Property Pivot As Single?
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.MoveRow
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	'Public Class ShowSubdivisionsRows
	'    Inherits BaseEvent
	'    Public Enum SubdivisionRowMode
	'        Mini
	'        Normal
	'    End Enum
	'    Public Overrides ReadOnly Property type As EventType = EventType.ShowSubdivisionsRows
	'    Public Property Subdivisions As Integer
	'    Public Property Mode As SubdivisionRowMode
	'    Public Property ArcAngle As Integer
	'    Public Property SpinsPerSecond As Single
	'    Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	'    Public Overrides ReadOnly Property Rooms As Rooms

	'End Class

	Public Class PlayExpression
		Inherits BaseRowAnimation
		Public Property Expression As String
		Public Property Replace As Boolean
		Public Overrides ReadOnly Property Type As EventType = EventType.PlayExpression
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Expression}"
		End Function
	End Class
	Public Class TintRows
		Inherits BaseRowAnimation
		Implements IEaseEvent
		Enum RowEffect
			None
			Electric
#If DEBUG Then
			Smoke
#End If
		End Enum
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property TintColor As New PaletteColor(True)
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Border As Borders
		Public Property BorderColor As New PaletteColor(True)
		Public Property Opacity As Integer
		Public Property Tint As Boolean
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Effect As RowEffect
		Public Overrides ReadOnly Property Type As EventType = EventType.TintRows
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		<JsonIgnore>
		Public ReadOnly Property TintAll As Boolean
			Get
				Return Parent IsNot Nothing
			End Get
		End Property
		Friend Function ShouldSerializeDuration() As Boolean
			Return Duration <> 0
		End Function
		Friend Function ShouldSerializeEase() As Boolean
			Return Duration <> 0
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Border}{If(Border = Borders.None, "", ":" + BorderColor.ToString)}"
		End Function
	End Class
	Public Class BassDrop
		Inherits BaseEvent
		Implements IRoomEvent
		Enum StrengthType
			Low
			Medium
			High
		End Enum
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property Strength As StrengthType
		Public Overrides ReadOnly Property Type As EventType = EventType.BassDrop
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Strength}"
		End Function
	End Class
	Public Class ShakeScreen
		Inherits BaseEvent
		Implements IRoomEvent
		Enum ShakeLevels
			Low
			Medium
			High
		End Enum
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property ShakeLevel As ShakeLevels
		Public Overrides ReadOnly Property Type As EventType = EventType.ShakeScreen
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {ShakeLevel}"
		End Function
	End Class
	Public Class FlipScreen
		Inherits BaseEvent
		Implements IRoomEvent
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property FlipX As Boolean
		Public Property FlipY As Boolean
		Public Overrides ReadOnly Property Type As EventType = EventType.FlipScreen
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Dim result As String
			If FlipX Then
				If FlipY Then
					result = "X"
				Else
					result = "^v"
				End If
			Else
				If FlipY Then
					result = "<>"
				Else
					result = ""
				End If
			End If
			Return MyBase.ToString() + $" {result}"
		End Function
	End Class
	Public Class InvertColors
		Inherits BaseEvent
		Implements IRoomEvent
		Public Property Rooms As New Room(False, 0) Implements IRoomEvent.Rooms
		Public Property Enable As Boolean
		Public Overrides ReadOnly Property Type As EventType = EventType.InvertColors
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Enable}"
		End Function
	End Class
	Public Class PulseCamera
		Inherits BaseEvent
		Implements IRoomEvent
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property Strength As Byte
		Public Property Count As Integer
		Public Property Frequency As Single
		Public Overrides ReadOnly Property Type As EventType = EventType.PulseCamera
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Strength},{Count},{Frequency}"
		End Function
	End Class
	Public Class TextExplosion
		Inherits BaseEvent
		Implements IRoomEvent
		Enum Directions
			Left
			Right
		End Enum
		Enum Modes
			OneColor
			Random
		End Enum
		Public Property Rooms As New Room(False, 0) Implements IRoomEvent.Rooms
		Public Property Color As New PaletteColor(False)
		Public Property Text As String
		Public Property Direction As Directions
		Public Property Mode As Modes
		Public Overrides ReadOnly Property Type As EventType = EventType.TextExplosion
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class NarrateRowInfo
		Inherits BaseRowAction
		Implements IRoomEvent
		Public Enum NarrateInfoType
			Connect
			Update
			Disconnect
			Online
			Offline
		End Enum
		Public Overrides ReadOnly Property Type As EventType = EventType.NarrateRowInfo
		Public Property Rooms As Room Implements IRoomEvent.Rooms
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Property InfoType As NarrateInfoType
		Public Property SoundOnly As Boolean
		Public Property NarrateSkipBeats As String
		Public Property CustomPattern As New LimitedList(Of Patterns)(6)
		Public Property SkipsUnstable As Boolean
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {InfoType}:{NarrateSkipBeats}"
		End Function
	End Class
	Public Class ReadNarration
		Inherits BaseEvent
		Public Enum NarrationCategory
			Fallback
			Navigation
			Instruction
			Notification
			Dialogue
			Description = 6
			Subtitles
		End Enum
		Public Overrides ReadOnly Property Type As EventType = EventType.ReadNarration
		Public Property Text As String
		Public Property Category As NarrationCategory
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Property Rooms As Room = Room.Default
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class ShowDialogue
		Inherits BaseEvent
		Enum Sides
			Bottom
			Top
		End Enum
		Enum PortraitSides
			Left
			Right
		End Enum
		Public Property Text As String
		Public Property PanelSide As Sides
		Public Property PortraitSide As PortraitSides
		Public Property Speed As Integer = 1 '?
		Public Property PlayTextSounds As Boolean
		Public Overrides ReadOnly Property Type As EventType = EventType.ShowDialogue
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class ShowStatusSign
		Inherits BaseEvent
		Public Property UseBeats As Boolean = True
		Public Property Narrate As Boolean = True
		Public Property Text As String
		Public Property Duration As Single
		<JsonIgnore> Public Property TimeDuration As TimeSpan
			Get
				If UseBeats Then
					Return TimeSpan.Zero
				End If
				Return TimeSpan.FromSeconds(Duration)
			End Get
			Set(value As TimeSpan)
				UseBeats = False
				Duration = value.TotalSeconds
			End Set
		End Property
		Public Overrides ReadOnly Property Type As EventType = EventType.ShowStatusSign
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class FloatingText
		Inherits BaseEvent
		Implements IRoomEvent
		<Flags> Public Enum OutMode
			FadeOut
			HideAbruptly
		End Enum
		<JsonConverter(GetType(AnchorStyleConverter))> <Flags> Public Enum AnchorStyle
			Lower = &B1
			Upper = &B10
			Left = &B100
			Right = &B1000
			Center = &B0
		End Enum
		Private Shared _PrivateId As UInteger = 0
		Private ReadOnly GeneratedId As UInteger
		Private ReadOnly _children As New List(Of AdvanceText)
		Private _mode As OutMode = OutMode.FadeOut
		Public Overrides ReadOnly Property Type As EventType = EventType.FloatingText
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		<JsonIgnore> Public ReadOnly Property Children As List(Of AdvanceText)
			Get
				Return _children '.OrderBy(Function(i) i.Bar * 50 + i.Beat).ToList
			End Get
		End Property
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property FadeOutRate As Single
		Public Property Color As New PaletteColor(True) With {.Color = New SKColor(&HFF, &HFF, &HFF, &HFF)}
		Public Property Angle As Single
		Public Property Size As UInteger
		Public Property OutlineColor As New PaletteColor(True) With {.Color = New SKColor(0, 0, 0, &HFF)}
		<JsonProperty> Friend ReadOnly Property Id As Integer
			Get
				Return GeneratedId
			End Get
		End Property
		Public Property TextPosition As New RDPoint(50, 50)
		Public Property Anchor As AnchorStyle
		Public Property Mode As OutMode
			Get
				Return _mode
			End Get
			Set(value As OutMode)
				_mode = value
			End Set
		End Property
		Public Property ShowChildren As Boolean = False
		Public Property Text As String = "等呀等得好心慌……"
		Public Sub New()
			GeneratedId = _PrivateId
			_PrivateId += 1
		End Sub
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {_Text}"
		End Function
	End Class
	Public Class AdvanceText
		Inherits BaseEvent
		Implements IRoomEvent
		Public Overrides ReadOnly Property Type As EventType = EventType.AdvanceText
		<JsonIgnore> Public Property Rooms As Room Implements IRoomEvent.Rooms
			Get
				Return Parent.Rooms
			End Get
			Set(value As Room)
				Parent.Rooms = value
			End Set
		End Property
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		<JsonIgnore> Public Property Parent As FloatingText
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property FadeOutDuration As Single
#Disable Warning IDE0051
		<JsonProperty> Private ReadOnly Property Id As Integer
			Get
				Return Parent.Id
			End Get
		End Property
#Enable Warning IDE0051
		Public Overrides Function ToString() As String
			Return MyBase.ToString + $" Index:{_Parent.Children.IndexOf(Me)}"
		End Function
	End Class
	Public Class ChangePlayersRows
		Inherits BaseEvent
		Enum CpuType
			None
			Otto
			Ian
			Paige
			Edega
			BlankCPU
			Samurai
		End Enum
		Enum PlayerModes
			OnePlayer
			TwoPlayers
		End Enum
		Public Property Players As New List(Of PlayerType)(16)
		Public Property PlayerMode As PlayerModes
		Public Property CpuMarkers() As New List(Of CpuType)(16)
		Public Overrides ReadOnly Property Type As EventType = EventType.ChangePlayersRows
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class FinishLevel
		Inherits BaseEvent
		Public Overrides ReadOnly Property Type As EventType = EventType.FinishLevel
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class Comment
		Inherits BaseDecorationAction
#Disable Warning CA1507
		<JsonProperty("tab")> Public Property CustomTab As Tabs
#Enable Warning CA1507
		<JsonIgnore> Public Overrides ReadOnly Property Tab As Tabs
			Get
				Return CustomTab
			End Get
		End Property
		Public Property Show As Boolean
		Public Property Text As String = ""
		Public Overrides ReadOnly Property Target As String
			Get
				Return MyBase.Target
			End Get
		End Property
		Public Property Color As New PaletteColor(False) With {.Color = New SKColor(242, 230, 68)}
		Public Overrides ReadOnly Property Type As EventType = EventType.Comment
		Public Function ShouldSerializeTarget() As Boolean
			Return Tab = Tabs.Decorations
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class Stutter
		Inherits BaseEvent
		Implements IRoomEvent
		Enum Actions
			Add
			Cancel
		End Enum
		Public Property Rooms As New Room(False, 0) Implements IRoomEvent.Rooms
		Public Property SourceBeat As Single
		Public Property Length As Single
		Public Property Action As Actions
		Public Property Loops As Integer
		Public Overrides ReadOnly Property Type As EventType = EventType.Stutter
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class ShowHands
		Inherits BaseEvent
		Implements IRoomEvent
		Enum Actions
			Show
			Hide
			Raise
			Lower
		End Enum
		Enum Extents
			Full
			[Short]
		End Enum
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property Action As Actions
		Public Property Hand As PlayerHands
		Public Property Align As Boolean
		Public Property Instant As Boolean
		Public Property Extent As Extents
		Public Overrides ReadOnly Property Type As EventType = EventType.ShowHands
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class PaintHands
		Inherits BaseEvent
		Implements IEaseEvent
		Implements IRoomEvent
		Enum Borders
			None
			Outline
			Glow
		End Enum
		Public Property TintColor As New PaletteColor(True)
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Border As Borders
		Public Property BorderColor As New PaletteColor(True)
		Public Property Opacity As Integer
		Public Property Tint As Boolean
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property Hands As PlayerHands
		Public Overrides ReadOnly Property Type As EventType = EventType.PaintHands
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class SetHandOwner
		Inherits BaseEvent
		Implements IRoomEvent
		Enum Characters
			Players
			Ian
			Paige
			Edega
		End Enum
		Public Property Rooms As New Room(True, 0) Implements IRoomEvent.Rooms
		Public Property Hand As PlayerHands
		Public Property Character As String
		Public Overrides ReadOnly Property Type As EventType = EventType.SetHandOwner
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class SetPlayStyle
		Inherits BaseEvent
		Enum PlayStyles
			Normal
			[Loop]
			Prolong
			Immediately
			ExtraImmediately
			ProlongOneBar
		End Enum
		Public Property PlayStyle As PlayStyles
		Public Property NextBar As Integer
		Public Property Relative As Boolean
		Public Overrides ReadOnly Property Type As EventType = EventType.SetPlayStyle
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class TagAction
		Inherits BaseEvent
		<Flags>
		Enum Actions
			Run = &B10
			All = &B1
			Enable = &B110
			Disable = &B100
		End Enum
		Enum SpecialTag
			onHit
			onMiss
			onHeldPressHit
			onHeldReleaseHit
			onHeldPressMiss
			onHeldReleaseMiss
			row0
			row1
			row2
			row3
			row4
			row5
			row6
			row7
			row8
			row9
			row10
			row11
			row12
			row13
			row14
			row15
		End Enum
		<JsonIgnore> Public Property Action As Actions
		<JsonProperty("Tag")> Public Property ActionTag As String
		Public Overrides ReadOnly Property Type As EventType = EventType.TagAction
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {ActionTag}"
		End Function
	End Class
	Public Class CallCustomMethod
		Inherits BaseEvent
		Enum ExecutionTimeOptions
			OnPrebar
			OnBar
		End Enum
		Public Property MethodName As String
		Public Property ExecutionTime As ExecutionTimeOptions
		Public Property SortOffset As Integer
		Public Overrides ReadOnly Property Type As EventType = EventType.CallCustomMethod
		<JsonIgnore> Public Property Rooms As Room = Room.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {MethodName}"
		End Function
	End Class
	Public Class NewWindowDance
		Inherits BaseEvent
		Implements IEaseEvent
		Enum Presets
			Move
			Sway
			Wrap
			Ellipse
			ShakePer
		End Enum
		Enum SamePresetBehaviors
			Reset
			Keep
		End Enum
		Enum References
			Center
			Edge
		End Enum
		Enum EaseTypes
			Repeat
		End Enum
		Public Property Preset As String
		Public Property SamePresetBehavior As String
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property Position As PointE
		Public Property Reference As References
		Public Property UseCircle As Boolean
		Public Property Speed As Single
		Public Property Amplitude As New Single
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property AmplitudeVector As PointE
		Public Property Angle As Expression?
		Public Property Frequency As Single
		Public Property Period As Single
		Public Property EaseType As EaseTypes
		Public Property SubEase As EaseType
		Public Property EasingDuration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.NewWindowDance
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class PlayAnimation
		Inherits BaseDecorationAction
		Public Overrides ReadOnly Property Type As EventType = EventType.PlayAnimation
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Decorations
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Populate)>
		Public Property Expression As String
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" Expression:{Expression}"
		End Function
	End Class
	Public Class Tint
		Inherits BaseDecorationAction
		Implements IEaseEvent
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Border As Borders
		Public Property BorderColor As New PaletteColor(True)
		Public Property Opacity As Integer
		Public Property Tint As Boolean
		Public Property TintColor As New PaletteColor(True) With {.Color = New SKColor(&HFF, &HFF, &HFF, &HFF)}
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As EventType = EventType.Tint
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Decorations
		Friend Function ShouldSerializeDuration() As Boolean
			Return Duration <> 0
		End Function
		Friend Function ShouldSerializeEase() As Boolean
			Return Duration <> 0
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Border}{If(Border = Borders.None, "", ":" + BorderColor.ToString)}"
		End Function
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class Tile
		Inherits BaseDecorationAction
		Implements IEaseEvent
		Public Enum TilingTypes
			Scroll
			Pulse
		End Enum
		Public Overrides ReadOnly Property Type As EventType = EventType.Tile
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Decorations
		Public Property Position As RDPoint?
		Public Property Tiling As RDPoint?
		Public Property Speed As RDPoint?
		Public Property TilingType As TilingTypes
		Public Property Interval As Single
		<JsonIgnore> Public Overrides Property Y As Integer
			Get
				Return 0
			End Get
			Set(value As Integer)
			End Set
		End Property
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Function ShouldSerializeTilingType() As Boolean
			Return Speed IsNot Nothing
		End Function
		Public Function ShouldSerializeInterval() As Boolean
			Return TilingType = TilingTypes.Pulse
		End Function
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class Move
		Inherits BaseDecorationAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As EventType = EventType.Move
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Decorations
		Public Property Position As PointE?
		Public Property Scale As PointE?
		Public Property Angle As Expression?
		Public Property Pivot As PointE?
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		<JsonIgnore> Public Overrides Property Y As Integer
			Get
				Return 0
			End Get
			Set(value As Integer)
			End Set
		End Property
		Public Overrides Function ToString() As String
			Return MyBase.ToString()
		End Function
	End Class
	Public Class SetVisible
		Inherits BaseDecorationAction
		Public Overrides ReadOnly Property Type As EventType = EventType.SetVisible
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Decorations
		Public Property Visible As Boolean
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {_Visible}"
		End Function
	End Class
	Public Class AddClassicBeat
		Inherits BaseBeat
		Enum ClassicBeatPatterns
			ThreeBeat
			FourBeat
		End Enum
		Public Property Tick As Single = 1
		Public Property Swing As Single
		Public Property Hold As Single
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property SetXs As ClassicBeatPatterns?
		Public Overrides ReadOnly Property Type As EventType = EventType.AddClassicBeat
		Public Overrides Function ToString() As String
			Return $"{MyBase.ToString()} {Utils.GetPatternString(RowXs)} {If(_Swing = 0.5 Or _Swing = 0, "", " Swing")}"
		End Function
	End Class
	Public Class SetRowXs
		Inherits BaseBeat
		Private _pattern As New LimitedList(Of Patterns)(6, Patterns.None)
		Public Overrides ReadOnly Property Type As EventType = EventType.SetRowXs
		<JsonConverter(GetType(PatternConverter))> Public Property Pattern As LimitedList(Of Patterns)
			Get
				Return _pattern
			End Get
			Set(value As LimitedList(Of Patterns))
				_pattern = value
			End Set
		End Property
		Public Property SyncoBeat As SByte = -1
		Public Property SyncoSwing As Single
		Public Property SyncoPlayModifierSound As Boolean
		Public Property SyncoVolume As Integer = 100
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {GetPatternString()}"
		End Function
	End Class
	Public Class AddOneshotBeat
		Inherits BaseBeat
		Public Enum Pulse
			Wave
			Square
			Triangle
			Heart
		End Enum
		Public Enum FreezeBurn
			Wave
			Freezeshot
			Burnshot
		End Enum
		Private _delay As Single = 0
		Public Property PulseType As Pulse
		Public Property Subdivisions As Byte = 1
		Public Property SubdivSound As Boolean
		Public Property Tick As Single
		Public Property Loops As UInteger
		Public Property Interval As Single
		Public Property Skipshot As Boolean
		Public Property FreezeBurnMode As FreezeBurn?
		Public Property Delay As Single
			Get
				Return _delay
			End Get
			Set(value As Single)
				If _FreezeBurnMode = FreezeBurn.Freezeshot Then
					If value <= 0 Then
						_delay = 0.5
					Else
						_delay = value
					End If
				Else
					_delay = 0
				End If
			End Set
		End Property
		Public Overrides ReadOnly Property Type As EventType = EventType.AddOneshotBeat
		Friend Function ShouldSerializeSkipshot() As Boolean
			Return Skipshot
		End Function
		Public Function ShouldSerializeFreezeBurnMode() As Boolean
			Return FreezeBurnMode IsNot Nothing
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {_FreezeBurnMode} {_PulseType}"
		End Function
	End Class
	Public Class SetOneshotWave
		Inherits BaseBeat
		Enum Waves
			BoomAndRush
			Ball
			Spring
			Spike
			SpikeHuge
			[Single]
		End Enum
		Public Property WaveType As Waves
		Public Property Height As Integer
		Public Property Width As Integer
		Public Overrides ReadOnly Property Type As EventType = EventType.SetOneshotWave
	End Class
	Public Class AddFreeTimeBeat
		Inherits BaseBeat
		Public Property Hold As Single
		Public Property Pulse As Byte
		Public Overrides ReadOnly Property Type As EventType = EventType.AddFreeTimeBeat
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {_Pulse + 1}"
		End Function
	End Class
	Public Class PulseFreeTimeBeat
		Inherits BaseBeat
		Enum ActionType
			Increment
			Decrement
			Custom
			Remove
		End Enum
		Public Property Hold As Single
		Public Property Action As ActionType
		Public Property CustomPulse As UInteger
		Public Overrides ReadOnly Property Type As EventType = EventType.PulseFreeTimeBeat
		Public Overrides Function ToString() As String
			Dim Out As String = ""
			Select Case _Action
				Case ActionType.Increment
					Out = ">"
				Case ActionType.Decrement
					Out = "<"
				Case ActionType.Custom
					Out = _CustomPulse + 1
				Case ActionType.Remove
					Out = "X"
			End Select
			Return MyBase.ToString() + $" {Out}"
		End Function
	End Class
	Public Class ShowRooms
		Inherits BaseEvent
		Implements IEaseEvent
		Implements IRoomEvent
		<JsonProperty>
		Public Property Rooms As New Room(False, 0) Implements IRoomEvent.Rooms
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Heights As New List(Of Integer)(4)
		Public Property TransitionTime As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As EventType = EventType.ShowRooms
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class MoveRoom
		Inherits BaseEvent
		Implements IEaseEvent
		Public Property RoomPosition As PointE?
		Public Property Scale As RDSizeE?
		Public Property Angle As Expression?
		Public Property Pivot As PointE?
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.MoveRoom
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		<JsonIgnore>
		Public ReadOnly Property Rooms As Room
			Get
				Return New SingleRoom(Y)
			End Get
		End Property
	End Class
	Public Class ReorderRooms
		Inherits BaseEvent
		Public Property Order As List(Of UInteger)
		Public Overrides ReadOnly Property Type As EventType = EventType.ReorderRooms
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
	End Class
	Public Class SetRoomContentMode
		Inherits BaseEvent
		Enum Modes
			Center
			AspectFill
		End Enum
		Public Property Mode As String
		Public Overrides ReadOnly Property Type As EventType = EventType.SetRoomContentMode
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		<JsonIgnore> Public ReadOnly Property Room As Room
			Get
				Return New SingleRoom(Y)
			End Get
		End Property
	End Class
	Public Class MaskRoom
		Inherits BaseEvent
		Enum MaskTypes
			Image
			Room
			Color
			None
		End Enum
		Enum AlphaModes
			Normal
			Inverted
		End Enum
		Enum ContentModes
			ScaleToFill
		End Enum
		Public Property MaskType As MaskTypes
		Public Property AlphaMode As AlphaModes
		Public Property SourceRoom As Byte
		Public Property Image As List(Of Sprite)
		Public Property Fps As UInteger
		Public Property KeyColor As New PaletteColor(False)
		Public Property ColorCutoff As Single
		Public Property ColorFeathering As Single
		Public Property ContentMode As ContentModes
		Public Overrides ReadOnly Property Type As EventType = EventType.MaskRoom
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		<JsonIgnore> Public ReadOnly Property Room As Room
			Get
				Return New SingleRoom(Y)
			End Get
		End Property
	End Class
	Public Class FadeRoom
		Inherits BaseEvent
		Implements IEaseEvent
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Opacity As UInteger
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As EventType = EventType.FadeRoom
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		<JsonIgnore> Public ReadOnly Property Room As Room
			Get
				Return New SingleRoom(Y)
			End Get
		End Property
	End Class
	Public Class SetRoomPerspective
		Inherits BaseEvent
		Implements IEaseEvent
		Public Property CornerPositions As New List(Of PointE?)(4)
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.SetRoomPerspective
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		<JsonIgnore> Public ReadOnly Property Room As Room
			Get
				Return New SingleRoom(Y)
			End Get
		End Property
	End Class
End Namespace