Imports NAudio.CoreAudioApi
Imports Newtonsoft.Json
Imports SkiaSharp
'#Disable Warning CA1507

Namespace Events
	''' <summary>
	''' Rhythm Doctor event types.
	''' </summary>
	Public Enum RDEventType
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
	Public Enum RDPlayerType
		P1
		P2
		CPU
		NoChange
	End Enum
	Public Enum RDTilingTypes
		Scroll
		Pulse
	End Enum
	Public Enum RDContentModes
		ScaleToFill
		AspectFit
		AspectFill
		Center
		Tiled
	End Enum
	Public Enum RDDefaultAudios
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
	Public Enum RDRowType
		Classic
		Oneshot
	End Enum
	Public Enum RDTabs
		Song
		Rows
		Actions
		Sprites
		Rooms
		Unknown
	End Enum
	Public Enum RDPlayerHands
		Left
		Right
		Both
		p1
		p2
	End Enum
	Public Enum RDBorders
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
	Public Interface IEaseEvent
		Property Ease As EaseType
		Property Duration As Single
	End Interface
	Public Interface IRDRoomEvent
		Property Rooms As RDRoom
	End Interface
	Public Interface ISingleRDRoomEvent
		Property Room As RDSingleRoom
	End Interface
	Public Interface IRDBarBeginningEvent
	End Interface
	Public MustInherit Class RDBaseEvent
		''' <summary>
		''' Event type.
		''' </summary>
		<JsonProperty(NameOf(Type))> Public MustOverride ReadOnly Property Type As RDEventType
		''' <summary>
		''' Column to which the event belongs.
		''' </summary>
		<JsonIgnore> Public MustOverride ReadOnly Property Tab As RDTabs
		Friend _beat As New RDBeat(1)
		''' <summary>
		''' The beat of the event.
		''' </summary>
		<JsonIgnore> Public Overridable Property Beat As RDBeat
			Get
				Return _beat
			End Get
			Set(value As RDBeat)
				If _beat.baseLevel Is Nothing Then
					If value.baseLevel Is Nothing Then
						_beat = value
					Else
						_beat = value.WithoutBinding()
					End If
				Else
					value = New RDBeat(_beat.baseLevel.Calculator, value)
					_beat.baseLevel.Remove(Me)
					value.baseLevel.Add(Me)
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
		<JsonProperty("if", DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property Condition As RDCondition
		Public Property Active As Boolean = True
		''' <summary>
		''' Clone this event and its basic properties.
		''' If it is of the same type as the source event, then it will be cloned.
		''' </summary>
		''' <typeparam name="T">Type that will be generated.</typeparam>
		''' <returns></returns>
		Public Overridable Function Clone(Of T As {RDBaseEvent, New})() As T
			If ConvertToRDEnum(Of T)() = Type Then
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
		Friend Overridable Function Clone(Of T As {RDBaseEvent, New})(level As RDLevel) As T
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
		Friend Function ShouldSerializeActive() As Boolean
			Return Not Active
		End Function
	End Class
	Public MustInherit Class RDBaseBeatsPerMinute
		Inherits RDBaseEvent
		Private _bpm As Single = 100
		Public Overrides Property Beat As RDBeat
			Get
				Return MyBase.Beat
			End Get
			Set(value As RDBeat)
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
	Public MustInherit Class RDBaseDecorationAction
		Inherits RDBaseEvent
		Friend _parent As RDDecoration
		<JsonIgnore> Public ReadOnly Property Parent As RDDecoration
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
		Public Overloads Function Clone(Of T As {RDBaseDecorationAction, New})() As T
			Dim Temp = MyBase.Clone(Of T)()
			Temp._parent = Parent
			Return Temp
		End Function
		Friend Overloads Function Clone(Of T As {RDBaseDecorationAction, New})(decoration As RDDecoration) As T
			Dim Temp = MyBase.Clone(Of T)(decoration.Parent)
			Temp._parent = decoration
			Return Temp
		End Function
		<JsonIgnore> Public ReadOnly Property Room As RDSingleRoom
			Get
				If Parent Is Nothing Then
					Return RDSingleRoom.Default
				End If
				Return Parent.Room
			End Get
		End Property
	End Class
	Public MustInherit Class RDBaseRowAction
		Inherits RDBaseEvent
		Friend _parent As RDRow
		<JsonIgnore> Public Property Parent As RDRow
			Get
				Return _parent
			End Get
			Friend Set(value As RDRow)
				If _parent IsNot Nothing Then
					_parent.Remove(Me)
					value?.Add(Me)
				End If
				_parent = value
			End Set
		End Property
		<JsonIgnore> Public ReadOnly Property Room As RDSingleRoom
			Get
				If _parent Is Nothing Then
					Return RDSingleRoom.Default
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
		Public Overloads Function Clone(Of T As {RDBaseRowAction, New})() As T
			Dim Temp = MyBase.Clone(Of T)()
			Temp.Parent = Parent
			Return Temp
		End Function
		Friend Overloads Function Clone(Of T As {RDBaseRowAction, New})(row As RDRow) As T
			Dim Temp = MyBase.Clone(Of T)(row.Parent)
			Temp.Parent = row
			Return Temp
		End Function
	End Class
	Public MustInherit Class RDBaseBeat
		Inherits RDBaseRowAction
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Rows
	End Class
	Public MustInherit Class RDBaseRowAnimation
		Inherits RDBaseRowAction
	End Class
	Public Class RDCustomEvent
		Inherits RDBaseEvent
		<JsonIgnore> Public Data As New Linq.JObject
		<JsonIgnore> Public Overrides ReadOnly Property Type As RDEventType = RDEventType.CustomEvent
		<JsonIgnore> Public ReadOnly Property ActureType As String
			Get
				Return Data(NameOf(Type).ToLowerCamelCase).ToString
			End Get
		End Property
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Unknown
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
			Beat = New RDBeat(data("bar").ToObject(Of UInteger), If(data("beat")?.ToObject(Of Single), 1))
			Tag = data("tag")?.ToObject(Of String)
			Condition = If(data("condition") Is Nothing, Nothing, RDCondition.Load(data("condition")?.ToObject(Of String)))
			Active = If(data("active")?.ToObject(Of Boolean), True)
		End Sub
		Public Overrides Function ToString() As String
			Return $"{Beat} *{ActureType}"
		End Function
		Public Overridable Function TryConvert(ByRef value As RDBaseEvent, ByRef type As RDEventType?) As Boolean
			Return TryConvert(value, type, New LevelReadOrWriteSettings)
		End Function
		Public Overridable Function TryConvert(ByRef value As RDBaseEvent, ByRef type As RDEventType?, settings As LevelReadOrWriteSettings) As Boolean
			Dim serializer = _beat.baseLevel.GetSerializer(settings)
			Dim eventType = RDConvertToType(Data("type"))
			If eventType Is Nothing Then
				Dim TempEvent As RDBaseEvent
				If Data("target") IsNot Nothing Then
					TempEvent = Data.ToObject(Of RDCustomDecorationEvent)(serializer)
				ElseIf Data("row") IsNot Nothing Then
					TempEvent = Data.ToObject(Of RDCustomRowEvent)(serializer)
				Else
					TempEvent = Data.ToObject(Of RDCustomEvent)(serializer)
				End If
				value = TempEvent
				type = Nothing
				Return False
			Else
				Dim TempEvent As RDBaseEvent = Data.ToObject(eventType, serializer)
				value = TempEvent
				type = [Enum].Parse(Of RDEventType)(Data("type"))
				Return True
			End If
		End Function
	End Class
	Public Class RDCustomDecorationEvent
		Inherits RDBaseDecorationAction
		Public Data As New Linq.JObject
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.CustomDecorationEvent
		<JsonIgnore> Public ReadOnly Property ActureType As String
			Get
				Return Data(NameOf(Type).ToLowerCamelCase).ToString
			End Get
		End Property
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Sprites
		Public Sub New()
			Data = New Linq.JObject
		End Sub
		Public Sub New(data As Linq.JObject)
			Me.Data = data
			Beat = New RDBeat(data("bar").ToObject(Of UInteger), If(data("beat")?.ToObject(Of Single), 1))
			Tag = data("tag")?.ToObject(Of String)
			Condition = If(data("condition") Is Nothing, Nothing, RDCondition.Load(data("condition")?.ToObject(Of String)))
			Active = If(data("active")?.ToObject(Of Boolean), True)
		End Sub
		Public Overrides Function ToString() As String
			Return $"{Beat} *{ActureType}"
		End Function
		Public Overridable Function TryConvert(ByRef value As RDBaseEvent, ByRef type As RDEventType?) As Boolean
			Return TryConvert(value, type, New LevelReadOrWriteSettings)
		End Function
		Public Overridable Function TryConvert(ByRef value As RDBaseEvent, ByRef type As RDEventType?, settings As LevelReadOrWriteSettings) As Boolean
			Return CType(Me, RDCustomEvent).TryConvert(value, type, settings)
		End Function
		Public Shared Widening Operator CType(e As RDCustomDecorationEvent) As RDCustomEvent
			Return New RDCustomEvent(e.Data)
		End Operator
		Public Shared Narrowing Operator CType(e As RDCustomEvent) As RDCustomDecorationEvent
			If e.Data("row") IsNot Nothing Then
				Return New RDCustomDecorationEvent(e.Data)
			End If
			Throw New RhythmBaseException("The row field is missing from the field contained in this object.")
		End Operator
	End Class
	Public Class RDCustomRowEvent
		Inherits RDBaseRowAction
		Public Data As New Linq.JObject
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.CustomRowEvent
		<JsonIgnore> Public ReadOnly Property ActureType As String
			Get
				Return Data(NameOf(Type).ToLowerCamelCase).ToString
			End Get
		End Property
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Rows
		Public Sub New()
			Data = New Linq.JObject
		End Sub
		Public Sub New(data As Linq.JObject)
			Me.Data = data
			Beat = New RDBeat(data("bar").ToObject(Of UInteger), If(data("beat")?.ToObject(Of Single), 1))
			Tag = data("tag")?.ToObject(Of String)
			Condition = If(data("condition") Is Nothing, Nothing, RDCondition.Load(data("condition")?.ToObject(Of String)))
			Active = If(data("active")?.ToObject(Of Boolean), True)
		End Sub
		Public Overrides Function ToString() As String
			Return $"{Beat} *{ActureType}"
		End Function
		Public Overridable Function TryConvert(ByRef value As RDBaseEvent, ByRef type As RDEventType?) As Boolean
			Return TryConvert(value, type, New LevelReadOrWriteSettings)
		End Function
		Public Overridable Function TryConvert(ByRef value As RDBaseEvent, ByRef type As RDEventType?, settings As LevelReadOrWriteSettings) As Boolean
			Return CType(Me, RDCustomEvent).TryConvert(value, type, settings)
		End Function
		Public Shared Widening Operator CType(e As RDCustomRowEvent) As RDCustomEvent
			Return New RDCustomEvent(e.Data)
		End Operator
		Public Shared Narrowing Operator CType(e As RDCustomEvent) As RDCustomRowEvent
			If e.Data("row") IsNot Nothing Then
				Return New RDCustomRowEvent(e.Data)
			End If
			Throw New RhythmBaseException("The row field is missing from the field contained in this object.")
		End Operator
	End Class
	Public Class RDPlaySong
		Inherits RDBaseBeatsPerMinute
		Implements IRDBarBeginningEvent
		Public Song As Components.RDAudio
		<JsonIgnore> Public Property Offset As TimeSpan
			Get
				Return Song.Offset
			End Get
			Set(value As TimeSpan)
				Song.Offset = value
			End Set
		End Property
		Public Property [Loop] As Boolean
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.PlaySong
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" BPM:{BeatsPerMinute}, Song:{Song.Filename}"
		End Function
	End Class
	Public Class RDSetBeatsPerMinute
		Inherits RDBaseBeatsPerMinute
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetBeatsPerMinute
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" BPM:{BeatsPerMinute}"
		End Function
	End Class
	Public Class RDSetCrotchetsPerBar
		Inherits RDBaseEvent
		Implements IRDBarBeginningEvent
		Private _visualBeatMultiplier As Single = 0
		Protected Friend _crotchetsPerBar As UInteger = 7
		Public Overrides ReadOnly Property Type As RDEventType = Events.RDEventType.SetCrotchetsPerBar
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
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
	Public Class RDPlaySound
		Inherits RDBaseEvent
		Enum CustomSoundTypes
			CueSound
			MusicSound
			BeatSound
			HitSound
			OtherSound
		End Enum
		Public Property IsCustom As Boolean
		Public Property CustomSoundType As CustomSoundTypes
		Public Property Sound As Components.RDAudio
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.PlaySound
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {If(IsCustom, Sound.ToString, CustomSoundType.ToString)}"
		End Function
	End Class
	Public Class RDSetClapSounds
		Inherits RDBaseEvent
		Public Property P1Sound As Components.RDAudio
		Public Property P2Sound As Components.RDAudio
		Public Property CpuSound As Components.RDAudio
		Public Property RowType As RDRowType
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetClapSounds
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
	End Class
	Public Class RDSetHeartExplodeVolume
		Inherits RDBaseEvent
		Implements IRDBarBeginningEvent
		Public Property Volume As UInteger
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetHeartExplodeVolume
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
	End Class
	Public Class RDSetHeartExplodeInterval
		Inherits RDBaseEvent
		Enum IntervalTypes
			OneBeatAfter
			Instant
			GatherNoCeil
			GatherAndCeil
		End Enum
		Public Property IntervalType As IntervalTypes
		Public Property Interval As Integer
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetHeartExplodeInterval
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
	End Class
	Public Class RDSayReadyGetSetGo
		Inherits RDBaseEvent
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
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SayReadyGetSetGo
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
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
	Public Class RDSetGameSound
		Inherits RDBaseEvent
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
		Private Property Audio As New Components.RDAudio
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
		Public Property SoundSubtypes As List(Of RDSoundSubType)
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetGameSound
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
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
	Public Class RDSetBeatSound
		Inherits RDBaseRowAction
		Public Property Sound As New Components.RDAudio
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetBeatSound
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
	End Class
	Public Class RDSetCountingSound
		Inherits RDBaseRowAction
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
		Public Property Sounds As New LimitedList(Of Components.RDAudio)(7, New Components.RDAudio)
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetCountingSound
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Song
		Public Function ShouldSerializeSounds() As Boolean
			Return VoiceSource = VoiceSources.Custom
		End Function
	End Class
	Public Class RDSetTheme
		Inherits RDBaseEvent
		Implements IRDRoomEvent
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
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetTheme
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Property Rooms As New RDRoom(False, 0) Implements IRDRoomEvent.Rooms
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Preset}"
		End Function
	End Class
	Public Class RDSetVFXPreset
		Inherits RDBaseEvent
		Implements IEaseEvent
		Implements IRDRoomEvent
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
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property Preset As Presets
		Public Property Enable As Boolean
		Public Property Threshold As Single
		Public Property Intensity As Single
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Include)> Public Property Color As New RDPaletteColor(False)
		Public Property FloatX As Single
		Public Property FloatY As Single
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetVFXPreset
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
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
	Public Class RDSetBackgroundColor
		Inherits RDBaseEvent
		Implements IEaseEvent
		Implements IRDRoomEvent
		Enum BackgroundTypes
			Color
			Image
		End Enum
		Enum FilterModes
			NearestNeighbor
		End Enum
		Public Property Rooms As New RDRoom(False, 0) Implements IRDRoomEvent.Rooms
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property ContentMode As RDContentModes
		Public Property Filter As FilterModes '?
		Public Property Color As New RDPaletteColor(True)
		Public Property Interval As Single
		Public Property BackgroundType As BackgroundTypes
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Fps As Integer
		Public Property Image As List(Of RDSprite)
		Public Property ScrollX As Integer
		Public Property ScrollY As Integer
		Public Property TilingType As RDTilingTypes
		Public Overrides ReadOnly Property Type As RDEventType = Events.RDEventType.SetBackgroundColor
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {If(BackgroundType = BackgroundTypes.Color, Color.ToString, String.Join(","c, Image.Select(Function(i) i.ToString)))}"
		End Function
	End Class
	Public Class RDSetForeground
		Inherits RDBaseEvent
		Implements IEaseEvent
		Implements IRDRoomEvent
		Public Property Rooms As New RDRoom(False, 0) Implements IRDRoomEvent.Rooms
		Public Property ContentMode As RDContentModes
		Public Property TilingType As RDTilingTypes
		Public Property Color As New RDPaletteColor(True)
		Public Property Image As List(Of RDSprite)
		Public Property Fps As Single
		Public Property ScrollX As Single
		Public Property ScrollY As Single
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Interval As Single
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetForeground
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Color},{String.Join(","c, Image.Select(Function(i) i.ToString))}"
		End Function
	End Class
	Public Class RDSetSpeed
		Inherits RDBaseEvent
		Implements IEaseEvent
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Speed As Single
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetSpeed
		<JsonIgnore> Public Property Rooms As RDRoom = RDRoom.Default
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" Speed:{Speed}"
		End Function
	End Class
	Public Class RDFlash
		Inherits RDBaseEvent
		Enum Durations
			[Short]
			Medium
			[Long]
		End Enum
		Public Property Rooms As New RDRoom(True, 0)
		Public Property Duration As Durations
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.Flash
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Duration}"
		End Function
	End Class
	Public Class RDCustomFlash
		Inherits RDBaseEvent
		Implements IEaseEvent
		Implements IRDRoomEvent
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property StartColor As New RDPaletteColor(False)
		Public Property Background As Boolean
		Public Property EndColor As New RDPaletteColor(False)
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property StartOpacity As Integer
		Public Property EndOpacity As Integer
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.CustomFlash
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {StartColor}=>{EndColor}"
		End Function
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class RDMoveCamera
		Inherits RDBaseEvent
		Implements IEaseEvent
		Implements IRDRoomEvent
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property CameraPosition As RDPointE?
		Public Property Zoom As Integer?
		Public Property Angle As RDExpression?
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.MoveCamera
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	Public Class RDHideRow
		Inherits RDBaseRowAnimation
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
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.HideRow
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class RDMoveRow
		Inherits RDBaseRowAnimation
		Implements IEaseEvent
		Enum Targets
			WholeRow
			Heart
			Character
		End Enum
		Public Property CustomPosition As Boolean
		Public Property Target As Targets
		Public Property RowPosition As RDPointE?
		Public Property Scale As RDPointE?
		Public Property Angle As RDExpression?
		Public Property Pivot As Single?
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.MoveRow
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
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

	Public Class RDPlayExpression
		Inherits RDBaseRowAnimation
		Public Property Expression As String
		Public Property Replace As Boolean
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.PlayExpression
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Expression}"
		End Function
	End Class
	Public Class RDTintRows
		Inherits RDBaseRowAnimation
		Implements IEaseEvent
		Enum RowEffect
			None
			Electric
#If DEBUG Then
			Smoke
#End If
		End Enum
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property TintColor As New RDPaletteColor(True)
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Border As RDBorders
		Public Property BorderColor As New RDPaletteColor(True)
		Public Property Opacity As Integer
		Public Property Tint As Boolean
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Effect As RowEffect
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.TintRows
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
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
			Return MyBase.ToString() + $" {Border}{If(Border = RDBorders.None, "", ":" + BorderColor.ToString)}"
		End Function
	End Class
	Public Class RDBassDrop
		Inherits RDBaseEvent
		Implements IRDRoomEvent
		Enum StrengthType
			Low
			Medium
			High
		End Enum
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property Strength As StrengthType
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.BassDrop
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Strength}"
		End Function
	End Class
	Public Class RDShakeScreen
		Inherits RDBaseEvent
		Implements IRDRoomEvent
		Enum ShakeLevels
			Low
			Medium
			High
		End Enum
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property ShakeLevel As ShakeLevels
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.ShakeScreen
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {ShakeLevel}"
		End Function
	End Class
	Public Class RDFlipScreen
		Inherits RDBaseEvent
		Implements IRDRoomEvent
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property FlipX As Boolean
		Public Property FlipY As Boolean
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.FlipScreen
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
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
	Public Class RDInvertColors
		Inherits RDBaseEvent
		Implements IRDRoomEvent
		Public Property Rooms As New RDRoom(False, 0) Implements IRDRoomEvent.Rooms
		Public Property Enable As Boolean
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.InvertColors
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Enable}"
		End Function
	End Class
	Public Class RDPulseCamera
		Inherits RDBaseEvent
		Implements IRDRoomEvent
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property Strength As Byte
		Public Property Count As Integer
		Public Property Frequency As Single
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.PulseCamera
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Strength},{Count},{Frequency}"
		End Function
	End Class
	Public Class RDTextExplosion
		Inherits RDBaseEvent
		Implements IRDRoomEvent
		Enum Directions
			Left
			Right
		End Enum
		Enum Modes
			OneColor
			Random
		End Enum
		Public Property Rooms As New RDRoom(False, 0) Implements IRDRoomEvent.Rooms
		Public Property Color As New RDPaletteColor(False)
		Public Property Text As String
		Public Property Direction As Directions
		Public Property Mode As Modes
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.TextExplosion
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class RDNarrateRowInfo
		Inherits RDBaseRowAction
		Implements IRDRoomEvent
		Public Enum NarrateInfoType
			Connect
			Update
			Disconnect
			Online
			Offline
		End Enum
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.NarrateRowInfo
		Public Property Rooms As RDRoom Implements IRDRoomEvent.Rooms
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Property InfoType As NarrateInfoType
		Public Property SoundOnly As Boolean
		Public Property NarrateSkipBeats As String
		Public Property CustomPattern As New LimitedList(Of Patterns)(6)
		Public Property SkipsUnstable As Boolean
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {InfoType}:{NarrateSkipBeats}"
		End Function
	End Class
	Public Class RDReadNarration
		Inherits RDBaseEvent
		Public Enum NarrationCategory
			Fallback
			Navigation
			Instruction
			Notification
			Dialogue
			Description = 6
			Subtitles
		End Enum
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.ReadNarration
		Public Property Text As String
		Public Property Category As NarrationCategory
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Property Rooms As RDRoom = RDRoom.Default
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class RDShowDialogue
		Inherits RDBaseEvent
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
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.ShowDialogue
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class RDShowStatusSign
		Inherits RDBaseEvent
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
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.ShowStatusSign
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class RDFloatingText
		Inherits RDBaseEvent
		Implements IRDRoomEvent
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
		Private ReadOnly _children As New List(Of RDAdvanceText)
		Private _mode As OutMode = OutMode.FadeOut
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.FloatingText
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		<JsonIgnore> Public ReadOnly Property Children As List(Of RDAdvanceText)
			Get
				Return _children '.OrderBy(Function(i) i.Bar * 50 + i.Beat).ToList
			End Get
		End Property
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property FadeOutRate As Single
		Public Property Color As New RDPaletteColor(True) With {.Color = New SKColor(&HFF, &HFF, &HFF, &HFF)}
		Public Property Angle As Single
		Public Property Size As UInteger
		Public Property OutlineColor As New RDPaletteColor(True) With {.Color = New SKColor(0, 0, 0, &HFF)}
		<JsonProperty> Friend ReadOnly Property Id As Integer
			Get
				Return GeneratedId
			End Get
		End Property
		Public Property TextPosition As RDPoint = New RDPoint(50, 50)
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
	Public Class RDAdvanceText
		Inherits RDBaseEvent
		Implements IRDRoomEvent
		Public Overrides ReadOnly Property Type As RDEventType = Events.RDEventType.AdvanceText
		<JsonIgnore> Public Property Rooms As RDRoom Implements IRDRoomEvent.Rooms
			Get
				Return Parent.Rooms
			End Get
			Set(value As RDRoom)
				Parent.Rooms = value
			End Set
		End Property
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		<JsonIgnore> Public Property Parent As RDFloatingText
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
	Public Class RDChangePlayersRows
		Inherits RDBaseEvent
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
		Public Property Players As New List(Of RDPlayerType)(16)
		Public Property PlayerMode As PlayerModes
		Public Property CpuMarkers() As New List(Of CpuType)(16)
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.ChangePlayersRows
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	Public Class RDFinishLevel
		Inherits RDBaseEvent
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.FinishLevel
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	Public Class RDComment
		Inherits RDBaseDecorationAction
#Disable Warning CA1507
		<JsonProperty("tab")> Public Property CustomTab As RDTabs
#Enable Warning CA1507
		<JsonIgnore> Public Overrides ReadOnly Property Tab As RDTabs
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
		Public Property Color As New RDPaletteColor(False) With {.Color = New SKColor(242, 230, 68)}
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.Comment
		Public Function ShouldSerializeTarget() As Boolean
			Return Tab = RDTabs.Sprites
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Text}"
		End Function
	End Class
	Public Class RDStutter
		Inherits RDBaseEvent
		Implements IRDRoomEvent
		Enum Actions
			Add
			Cancel
		End Enum
		Public Property Rooms As New RDRoom(False, 0) Implements IRDRoomEvent.Rooms
		Public Property SourceBeat As Single
		Public Property Length As Single
		Public Property Action As Actions
		Public Property Loops As Integer
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.Stutter
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	Public Class RDShowHands
		Inherits RDBaseEvent
		Implements IRDRoomEvent
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
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property Action As Actions
		Public Property Hand As RDPlayerHands
		Public Property Align As Boolean
		Public Property Instant As Boolean
		Public Property Extent As Extents
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.ShowHands
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	Public Class RDPaintHands
		Inherits RDBaseEvent
		Implements IEaseEvent
		Implements IRDRoomEvent
		Enum Borders
			None
			Outline
			Glow
		End Enum
		Public Property TintColor As New RDPaletteColor(True)
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Border As Borders
		Public Property BorderColor As New RDPaletteColor(True)
		Public Property Opacity As Integer
		Public Property Tint As Boolean
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property Hands As RDPlayerHands
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.PaintHands
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	Public Class RDSetHandOwner
		Inherits RDBaseEvent
		Implements IRDRoomEvent
		Enum Characters
			Players
			Ian
			Paige
			Edega
		End Enum
		Public Property Rooms As New RDRoom(True, 0) Implements IRDRoomEvent.Rooms
		Public Property Hand As RDPlayerHands
		Public Property Character As String
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetHandOwner
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	Public Class RDSetPlayStyle
		Inherits RDBaseEvent
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
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetPlayStyle
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	Public Class RDTagAction
		Inherits RDBaseEvent
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
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.TagAction
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {ActionTag}"
		End Function
	End Class
	Public Class RDCallCustomMethod
		Inherits RDBaseEvent
		Enum ExecutionTimeOptions
			OnPrebar
			OnBar
		End Enum
		Public Property MethodName As String
		Public Property ExecutionTime As ExecutionTimeOptions
		Public Property SortOffset As Integer
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.CallCustomMethod
		<JsonIgnore> Public Property Rooms As RDRoom = RDRoom.Default
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {MethodName}"
		End Function
	End Class
	Public Class RDNewWindowDance
		Inherits RDBaseEvent
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
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property Position As RDPointE
		Public Property Reference As References
		Public Property UseCircle As Boolean
		Public Property Speed As Single
		Public Property Amplitude As New Single
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property AmplitudeVector As RDPointE
		Public Property Angle As RDExpression?
		Public Property Frequency As Single
		Public Property Period As Single
		Public Property EaseType As EaseTypes
		Public Property SubEase As EaseType
		Public Property EasingDuration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.NewWindowDance
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Actions
	End Class
	Public Class RDPlayAnimation
		Inherits RDBaseDecorationAction
		Public Overrides ReadOnly Property Type As RDEventType = Events.RDEventType.PlayAnimation
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Sprites
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Populate)>
		Public Property Expression As String
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" Expression:{Expression}"
		End Function
	End Class
	Public Class RDTint
		Inherits RDBaseDecorationAction
		Implements IEaseEvent
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Border As RDBorders
		Public Property BorderColor As New RDPaletteColor(True)
		Public Property Opacity As Integer
		Public Property Tint As Boolean
		Public Property TintColor As New RDPaletteColor(True) With {.Color = New SKColor(&HFF, &HFF, &HFF, &HFF)}
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.Tint
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Sprites
		Friend Function ShouldSerializeDuration() As Boolean
			Return Duration <> 0
		End Function
		Friend Function ShouldSerializeEase() As Boolean
			Return Duration <> 0
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Border}{If(Border = RDBorders.None, "", ":" + BorderColor.ToString)}"
		End Function
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class RDTile
		Inherits RDBaseDecorationAction
		Implements IEaseEvent
		Public Enum TilingTypes
			Scroll
			Pulse
		End Enum
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.Tile
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Sprites
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
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class RDMove
		Inherits RDBaseDecorationAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.Move
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Sprites
		Public Property Position As RDPointE?
		Public Property Scale As RDPointE?
		Public Property Angle As RDExpression?
		Public Property Pivot As RDPointE?
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
	Public Class RDSetVisible
		Inherits RDBaseDecorationAction
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetVisible
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Sprites
		Public Property Visible As Boolean
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {_Visible}"
		End Function
	End Class
	Public Class RDAddClassicBeat
		Inherits RDBaseBeat
		Enum ClassicBeatPatterns
			ThreeBeat
			FourBeat
		End Enum
		Public Property Tick As Single = 1
		Public Property Swing As Single
		Public Property Hold As Single
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)> Public Property SetXs As ClassicBeatPatterns?
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.AddClassicBeat
		Public Overrides Function ToString() As String
			Return $"{MyBase.ToString()} {Utils.GetPatternString(RowXs)} {If(_Swing = 0.5 Or _Swing = 0, "", " Swing")}"
		End Function
	End Class
	Public Class RDSetRowXs
		Inherits RDBaseBeat
		Private _pattern As New LimitedList(Of Patterns)(6, Patterns.None)
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetRowXs
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
	Public Class RDAddOneshotBeat
		Inherits RDBaseBeat
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
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.AddOneshotBeat
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
	Public Class RDSetOneshotWave
		Inherits RDBaseBeat
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
		Public Overrides ReadOnly Property Type As RDEventType = Events.RDEventType.SetOneshotWave
	End Class
	Public Class RDAddFreeTimeBeat
		Inherits RDBaseBeat
		Public Property Hold As Single
		Public Property Pulse As Byte
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.AddFreeTimeBeat
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {_Pulse + 1}"
		End Function
	End Class
	Public Class RDPulseFreeTimeBeat
		Inherits RDBaseBeat
		Enum ActionType
			Increment
			Decrement
			Custom
			Remove
		End Enum
		Public Property Hold As Single
		Public Property Action As ActionType
		Public Property CustomPulse As UInteger
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.PulseFreeTimeBeat
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
	Public Class RDShowRooms
		Inherits RDBaseEvent
		Implements IEaseEvent
		Implements IRDRoomEvent
		<JsonProperty>
		Public Property Rooms As New RDRoom(False, 0) Implements IRDRoomEvent.Rooms
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Heights As New List(Of Integer)(4)
		Public Property TransitionTime As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.ShowRooms
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Rooms
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class RDMoveRoom
		Inherits RDBaseEvent
		Implements IEaseEvent
		Public Property RoomPosition As RDPointE?
		Public Property Scale As RDSizeE?
		Public Property Angle As RDExpression?
		Public Property Pivot As RDPointE?
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.MoveRoom
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Rooms
		<JsonIgnore>
		Public ReadOnly Property Rooms As RDRoom
			Get
				Return New RDSingleRoom(Y)
			End Get
		End Property
	End Class
	Public Class RDReorderRooms
		Inherits RDBaseEvent
		Public Property Order As List(Of UInteger)
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.ReorderRooms
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Rooms
	End Class
	Public Class RDSetRoomContentMode
		Inherits RDBaseEvent
		Enum Modes
			Center
			AspectFill
		End Enum
		Public Property Mode As String
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetRoomContentMode
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Rooms
		<JsonIgnore> Public ReadOnly Property Room As RDRoom
			Get
				Return New RDSingleRoom(Y)
			End Get
		End Property
	End Class
	Public Class RDMaskRoom
		Inherits RDBaseEvent
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
		Public Property Image As List(Of RDSprite)
		Public Property Fps As UInteger
		Public Property KeyColor As New RDPaletteColor(False)
		Public Property ColorCutoff As Single
		Public Property ColorFeathering As Single
		Public Property ContentMode As ContentModes
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.MaskRoom
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Rooms
		<JsonIgnore> Public ReadOnly Property Room As RDRoom
			Get
				Return New RDSingleRoom(Y)
			End Get
		End Property
	End Class
	Public Class RDFadeRoom
		Inherits RDBaseEvent
		Implements IEaseEvent
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property Opacity As UInteger
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.FadeRoom
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Rooms
		<JsonIgnore> Public ReadOnly Property Room As RDRoom
			Get
				Return New RDSingleRoom(Y)
			End Get
		End Property
	End Class
	Public Class RDSetRoomPerspective
		Inherits RDBaseEvent
		Implements IEaseEvent
		Public Property CornerPositions As New List(Of RDPointE?)(4)
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Overrides ReadOnly Property Type As RDEventType = RDEventType.SetRoomPerspective
		Public Overrides ReadOnly Property Tab As RDTabs = RDTabs.Rooms
		<JsonIgnore> Public ReadOnly Property Room As RDRoom
			Get
				Return New RDSingleRoom(Y)
			End Get
		End Property
	End Class
	Public Enum ADEventType
		'AddComponent
		AddDecoration
		AddObject
		AddText
		AnimateTrack
		AutoPlayTiles
		Bloom
		Bookmark
		'CallMethod
		'ChangeTrack
		Checkpoint
		ColorTrack
		CustomBackground
		CustomEvent
		CustomTileEvent
		EditorComment
		Flash
		FreeRoam
		FreeRoamRemove
		FreeRoamTwirl
		'FreeRoamWarning
		HallOfMirrors
		Hide
		Hold
		'KillPlayer
		MoveCamera
		MoveDecorations
		MoveTrack
		MultiPlanet
		'Multitap
		Pause
		PlaySound
		PositionTrack
		RecolorTrack
		RepeatEvents
		ScaleMargin
		ScalePlanets
		ScaleRadius
		ScreenScroll
		ScreenTile
		SetConditionalEvents
		SetDefaultText
		SetFilter
		'SetFilterAdvanced
		'SetFloorIcon
		SetHitsound
		SetHoldSound
		SetObject
		SetPlanetRotation
		SetSpeed
		SetText
		ShakeScreen
		'TileDimensions
		Twirl
	End Enum
	Public Enum ADTrackColorTypes
		[Single]
		Stripes
		Glow
		Blink
		Switch
		Rainbow
		Volume
	End Enum
	Public Enum ADTrackColorPulses
		None
		Forward
		Backward
	End Enum
	Public Enum ADTrackStyles
		Standard
		Neon
		NeonLight
		Basic
		Gems
		Minimal
	End Enum
	Public Enum TileRelativeTo
		ThisTile
		Start
		[End]
	End Enum
	Public Enum ADCameraRelativeTo
		Player
		Tile
		[Global]
		LastPosition
		LastPositionNoRotation
	End Enum
	Public Enum ADDecorationRelativeTo
		Tile
		[Global]
		RedPlanet
		BluePlanet
		GreenPlanet
		Camera
		CameraAspect
		LastPosition
	End Enum
	Public Enum ADEasePartBehaviors
		Repeat
		Mirror
	End Enum
	Public Class ADTile
		Inherits ADTypedList(Of ADBaseTileEvent)
		Private _angle As Single
		Public Property Angle As Single
			Get
				Return _angle
			End Get
			Set(value As Single)
				If -360 < value AndAlso value < 360 Then
					_angle = (value + 540) Mod 360 - 180
				Else
					_angle = -999
				End If
			End Set
		End Property
		Public ReadOnly Property IsMidSpin As Boolean
			Get
				Return _angle < -180 OrElse _angle > 180
			End Get
		End Property
		Public Property Beat As ADBeat
			Get
				Return New ADBeat(Parent.Calculator, Index + _angle / 180)
			End Get
			Set(value As ADBeat)

			End Set
		End Property
		<JsonIgnore>
		Public Property Parent As ADLevel
		Public Sub New()
		End Sub
		Public Sub New(actions As IEnumerable(Of ADBaseTileEvent))
			For Each i In actions
				i.Parent = Me
				Add(i)
			Next
		End Sub
		Public ReadOnly Property Index As Integer
			Get
				Return Parent.IndexOf(Me)
			End Get
		End Property
		Public Overrides Function ToString() As String
			Return $"[{Index}]{Beat}<{If(IsMidSpin, "MS".PadRight(4), _angle.ToString.PadLeft(4))}>{If(Any, $", Count = {Count}", String.Empty)}"
			Return $"{Beat}{If(Any, $", Count = {Count}", String.Empty)}"
		End Function
	End Class
	Public MustInherit Class ADBaseEvent
		Public MustOverride ReadOnly Property Type As ADEventType
		Public Overrides Function ToString() As String
			Return Type.ToString
		End Function
	End Class
	Public MustInherit Class ADBaseTileEvent
		Inherits ADBaseEvent
		<JsonIgnore> Public Property Parent As ADTile
		Public Overrides Function ToString() As String
			Return $"{Type}"
		End Function
	End Class
	Public MustInherit Class ADBaseTaggedTileAction
		Inherits ADBaseTileEvent
		Public Property AngleOffset As Single
		Public Property EventTag As String
	End Class
	Public Class ADCustomEvent
		Inherits ADBaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.CustomEvent
		<JsonIgnore> Public ReadOnly Property ActureType As String
			Get
				Return Data("eventType").ToString
			End Get
		End Property
		Public Property Data As Linq.JObject
		Public Overrides Function ToString() As String
			Return ActureType
		End Function
		'Public Overridable Function TryConvert(ByRef value As ADBaseEvent, ByRef type As ADEventType?) As Boolean
		'	Return TryConvert(value, type, New LevelReadOrWriteSettings)
		'End Function
		'Public Overridable Function TryConvert(ByRef value As ADBaseEvent, ByRef type As ADEventType?, settings As LevelReadOrWriteSettings) As Boolean
		'	Dim serializer = ADLevel.
		'	Dim SubClassType As Type = ADConvertToType(Data("eventType").ToObject(Of String))

		'	Dim result = If(SubClassType IsNot Nothing,
		'		jobj.ToObject(SubClassType, serializer),
		'		jobj.ToObject(Of ADCustomEvent)(serializer))

		'	Return existingValue
		'End Function

	End Class
	Public Class ADCustomTileEvent
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.CustomTileEvent
		<JsonIgnore> Public ReadOnly Property ActureType As String
			Get
				Return Data("eventType").ToString
			End Get
		End Property
		Public Property Data As Linq.JObject
		Public Overrides Function ToString() As String
			Return $"{Parent.Index}({Parent.Angle}): {ActureType}"
		End Function
	End Class
	Public Class ADSetSpeed
		Inherits ADBaseTaggedTileAction
		Public Enum SpeedTypes
			Bpm
			Multiplier
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.SetSpeed
		Public Property SpeedType As SpeedTypes
		Public Property BeatsPerMinute As Single
		Public Property BpmMultiplier As Single
	End Class
	Public Class ADTwirl
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.Twirl
	End Class
	Public Class ADCheckpoint
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.Checkpoint
		Public Property TileOffset As Integer
	End Class
	Public Class ADSetHitsound
		Inherits ADBaseTileEvent
		Public Enum GameSounds
			Hitsound
			Midspin
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.SetHitsound
		Public Property GameSound As String
		Public Property Hitsound As String
		Public Property HitsoundVolume As Integer
	End Class
	Public Class ADPlaySound
		Inherits ADBaseTaggedTileAction
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.PlaySound
		Public Property Hitsound As String
		Public Property HitsoundVolume As Integer
	End Class
	Public Class ADSetPlanetRotation
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.SetPlanetRotation
		Public Property Ease As String
		Public Property EaseParts As Integer '??????????????????????????
		Public Property EasePartBehavior As ADEasePartBehaviors
	End Class
	Public Class ADPause
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.Pause
		Public Property Duration As Single
		Public Property CountdownTicks As Integer
		Public Property AngleCorrectionDir As Integer
	End Class
	Public Class ADAutoPlayTiles
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.AutoPlayTiles
		Public Property Enabled As Boolean
		Public Property ShowStatusText As Boolean
		Public Property SafetyTiles As Boolean
	End Class
	Public Class ADScalePlanets
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Enum TargetPlanets
			FirePlanet
			IcePlanet
			GreenPlanet
			All
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.ScalePlanets
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property TargetPlanet As TargetPlanets
		Public Property Scale As Integer
		Public Property Ease As EaseType Implements IEaseEvent.Ease
	End Class
	Public Class ADColorTrack
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.ColorTrack
		Public Property TrackColorType As ADTrackColorTypes
		Public Property TrackColor As SKColor
		Public Property SecondaryTrackColor As SKColor
		Public Property TrackColorAnimDuration As Single
		Public Property TrackColorPulse As ADTrackColorPulses
		Public Property TrackPulseLength As Single
		Public Property TrackStyle As ADTrackStyles
		Public Property TrackTexture As String 'ADAsset
		Public Property TrackTextureScale As Single
		Public Property TrackGlowIntensity As Single
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)>
	Public Class ADAnimateTrack
		Inherits ADBaseTileEvent
		Public Enum TrackAnimations
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
		Public Enum TrackDisappearAnimations
			None
			Scatter
			Scatter_Far
			Retract
			Shrink
			Shrink_Spin
			Fade
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.AnimateTrack
		Public Property TrackAnimation As TrackAnimations?
		Public Property TrackDisappearAnimation As TrackDisappearAnimations?
		Public Property BeatsAhead As Integer
		Public Property BeatsBehind As Integer
	End Class
	Public Class ADRecolorTrack
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.RecolorTrack
		Public Property StartTile() As Object ''''ADTileRefer
		Public Property EndTile() As Object ''''ADTileRefer
		Public Property GapLength As Integer
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property TrackColorType As ADTrackColorTypes
		Public Property TrackColor As SKColor
		Public Property SecondaryTrackColor As SKColor
		Public Property TrackColorAnimDuration As Single
		Public Property TrackColorPulse As ADTrackColorPulses
		Public Property TrackPulseLength As Single
		Public Property TrackStyle As ADTrackStyles
		Public Property TrackGlowIntensity As Single
		Public Property Ease As EaseType Implements IEaseEvent.Ease
	End Class
	Public Class ADMoveTrack
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.MoveTrack
		Public Property StartTile() As Object ''''ADTileRefer
		Public Property EndTile() As Object ''''ADTileRefer
		Public Property GapLength As Integer
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property PositionOffset As RDPoint
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property MaxVfxOnly As Boolean
	End Class
	Public Class ADPositionTrack
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.PositionTrack
		Public Property PositionOffset As RDPoint
		Public Property RelativeTo() As Object ''''ADTileRefer
		Public Property Rotation As Single
		Public Property Scale As Single
		Public Property Opacity As Single
		Public Property JustThisTile As Boolean
		Public Property SditorOnly As Boolean
		<JsonProperty(NullValueHandling:=NullValueHandling.Ignore)> Public Property StickToFloors As Boolean?
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class ADMoveDecorations
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Enum MaskingTypes
			None
			Mask
			VisibleInsideMask
			VisibleOutsideMask
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.MoveDecorations
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Tag As String
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		'Belows are nullable
		Public Property PositionOffset As RDPoint?
		Public Property ParallaxOffset As RDPoint?
		Public Property Visible As Boolean?
		Public Property RelativeTo As ADDecorationRelativeTo?
		Public Property DecorationImage As String
		Public Property PivotOffset As RDSize?
		Public Property RotationOffset As Single?
		Public Property Scale As RDSize?
		Public Property Color As SKColor?
		Public Property Opacity As Single?
		Public Property Depth As Integer?
		Public Property Parallax As RDPoint?
		Public Property MaskingType As MaskingTypes?
		Public Property UseMaskingDepth As Boolean?
		Public Property MaskingFrontDepth As Integer?
		Public Property MaskingBackDepth As Integer?
	End Class
	Public Class ADSetText
		Inherits ADBaseTaggedTileAction
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.SetText
		Public Property DecText As String
		Public Property Tag As String
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class ADSetObject
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.SetObject
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Tag As String
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		'Belows are nullable
		Public Property PlanetColor As SKColor?
		Public Property PlanetTailColor As SKColor?
		Public Property TrackAngle As Single?
		Public Property TrackColorType As ADTrackColorTypes?
		Public Property TrackColor As SKColor?
		Public Property SecondaryTrackColor As SKColor?
		Public Property TrackColorAnimDuration As Single?
		Public Property TrackOpacity As Single?
		Public Property TrackStyle As ADTrackStyles?
		Public Property TrackIcon As String
		Public Property TrackIconAngle As Single?
		Public Property TrackRedSwirl As Boolean?
		Public Property TrackGraySetSpeedIcon As Boolean?
		Public Property TrackGlowEnabled As Boolean?
		Public Property TrackGlowColor As SKColor?
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class ADSetDefaultText
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.SetDefaultText
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		'Belows are nullable
		Public Property DefaultTextColor As SKColor?
		Public Property DefaultTextShadowColor As SKColor?
		Public Property LevelTitlePosition As RDPoint?
		Public Property LevelTitleText As String
		Public Property CongratsText As String
		Public Property PerfectText As String
	End Class
	Public Class ADCustomBackground
		Inherits ADBaseTaggedTileAction
		Public Enum BgDisplayModes
			FitToScreen
			Unscaled
			Tiled
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.CustomBackground
		Public Property Color As SKColor
		Public Property BgImage As String
		Public Property ImageColor As SKColor
		Public Property Parallax As RDPoint
		Public Property BgDisplayMode As BgDisplayModes
		Public Property ImageSmoothing As Boolean
		Public Property LockRot As Boolean
		Public Property LoopBG As Boolean
		Public Property ScalingRatio As Single
	End Class
	Public Class ADFlash
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.Flash
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Plane As String
		Public Property StartColor As SKColor
		Public Property StartOpacity As Single
		Public Property EndColor As SKColor
		Public Property EndOpacity As Single
		Public Property Ease As EaseType Implements IEaseEvent.Ease
	End Class
	<JsonObject(ItemNullValueHandling:=NullValueHandling.Ignore)> Public Class ADMoveCamera
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.MoveCamera
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property DontDisable As Boolean
		Public Property MinVfxOnly As Boolean
		'Belows are nullable
		Public Property RelativeTo As ADCameraRelativeTo
		Public Property Position As RDPoint?
		Public Property Rotation As Single
		Public Property Zoom As Single
	End Class
	Public Class ADSetFilter
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Enum Filters
			Grayscale
			Sepia
			Invert
			VHS
			EightiesTV
			FiftiesTV
			Arcade
			LED
			Rain
			Blizzard
			PixelSnow
			Compression
			Glitch
			Pixelate
			Waves
			[Static]
			Grain
			MotionBlur
			Fisheye
			Aberration
			Drawing
			Neon
			Handheld
			NightVision
			Funk
			Tunnel
			Weird3D
			Blur
			BlurFocus
			GaussianBlur
			HexagonBlack
			Posterize
			Sharpen
			Contrast
			EdgeBlackLine
			OilPaint
			SuperDot
			WaterDrop
			LightWater
			Petals
			PetalsInstant
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.SetFilter
		Public Property Filter As Filters
		Public Property Enabled As String
		Public Property Intensity As Integer
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property DisableOthers As String
	End Class
	Public Class ADHallOfMirrors
		Inherits ADBaseTaggedTileAction
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.HallOfMirrors
		Public Property Enabled As Boolean
	End Class
	Public Class ADShakeScreen
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.ShakeScreen
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Strength As Single
		Public Property Intensity As Single
		Public Property Ease As EaseType Implements IEaseEvent.Ease
		Public Property FadeOut As Single
	End Class
	Public Class ADBloom
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.Bloom
		Public Property Enabled As Boolean
		Public Property Threshold As Integer
		Public Property Intensity As Integer
		Public Property Color As SKColor
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Ease As EaseType Implements IEaseEvent.Ease
	End Class
	Public Class ADScreenTile
		Inherits ADBaseTaggedTileAction
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.ScreenTile
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Tile As RDPoint
		Public Property Ease As EaseType Implements IEaseEvent.Ease
	End Class
	Public Class ADScreenScroll
		Inherits ADBaseTaggedTileAction
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.ScreenScroll
		Public Property Scroll As RDSizeN
	End Class
	Public Class ADRepeatEvents
		Inherits ADBaseTileEvent
		Public Enum RepeatTypes
			Beat
			Floor
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.RepeatEvents
		Public Property RepeatType As RepeatTypes
		Public Property Repetitions As Integer
		Public Property FloorCount As Integer '?????????
		Public Property Interval As Single
		Public Property ExecuteOnCurrentFloor As Boolean
		Public Property Tag As String
	End Class
	Public Class ADSetConditionalEvents
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.SetConditionalEvents
		Public Property PerfectTag As String
		Public Property HitTag As String
		Public Property EarlyPerfectTag As String
		Public Property LatePerfectTag As String
		Public Property BarelyTag As String
		Public Property VeryEarlyTag As String
		Public Property VeryLateTag As String
		Public Property MissTag As String
		Public Property TooEarlyTag As String
		Public Property TooLateTag As String
		Public Property LossTag As String
		Public Property OnCheckpointTag As String
	End Class
	Public Class ADEditorComment
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.EditorComment
		Public Property Comment As String
	End Class
	Public Class ADBookmark
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.Bookmark
	End Class
	Public Class ADAddDecoration
		Inherits ADBaseTileEvent
		Public Enum BlendModes
			None
			Darken
			Multiply
			ColorBurn
			LinearBurn
			DarkerColor
			Lighten
			Screen
			ColorDodge
			LinearDodge
			LighterColor
			Overlay
			SoftLight
			HardLight
			VividLight
			LinearLight
			PinLight
			HardMix
			Difference
			Exclusion
			Subtract
			Divide
			Hue
			Saturation
			Color
			Luminosity
		End Enum
		Public Enum MaskingTypes
			None
			Mask
			VisibleInsideMask
			VisibleOutsideMask
		End Enum
		Public Enum HitboxTypes
			None
			Kill
			[Event]
		End Enum
		Public Enum FailHitboxTypes
			Box
			Circle
			Capsule
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.AddDecoration
		Public Property DecorationImage As String
		Public Property Position As RDPointN
		Public Property RelativeTo As ADDecorationRelativeTo
		Public Property PivotOffset As RDSizeN
		Public Property Rotation As Single
		Public Property LockRotation As Boolean
		Public Property Scale As RDSizeN
		Public Property LockScale As Boolean
		Public Property Tile As RDSizeN
		Public Property Color As SKColor
		Public Property Opacity As Single
		Public Property Depth As Integer
		Public Property Parallax As RDSizeN
		Public Property ParallaxOffset As RDSizeN
		Public Property Tag As String
		Public Property ImageSmoothing As Boolean
		Public Property BlendMode As BlendModes
		Public Property MaskingType As MaskingTypes
		Public Property UseMaskingDepth As Boolean
		Public Property MaskingFrontDepth As Integer
		Public Property MaskingBackDepth As Integer
		Public Property Hitbox As HitboxTypes
		Public Property HitboxEventTag As String
		Public Property FailHitboxType As FailHitboxTypes
		Public Property FailHitboxScale As RDSizeN
		Public Property FailHitboxOffset As RDSizeN
		Public Property FailHitboxRotation As Integer
		Public Property Components As String
	End Class
	Public Class ADAddText
		Inherits ADBaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.AddText
		Public Property DecText As String
		Public Property Font As String
		Public Property Position As RDPointN
		Public Property RelativeTo As ADCameraRelativeTo
		Public Property PivotOffset As RDSizeN
		Public Property Rotation As Single
		Public Property LockRotation As Boolean
		Public Property Scale As RDSizeN
		Public Property LockScale As Boolean
		Public Property Color As SKColor
		Public Property Opacity As Single
		Public Property Depth As Integer
		Public Property Parallax As RDSizeN
		Public Property ParallaxOffset As RDSizeN
		Public Property Tag As String
	End Class
	Public Class ADAddObject
		Inherits ADBaseEvent
		Public Enum ObjectTypes
			Floor
			Planet
		End Enum
		Public Enum PlanetColorTypes
			DefaultRed
			planetColorType
			Gold
			Overseer
			Custom
		End Enum
		Public Enum TrackTypes
			Normal
			Midspin
		End Enum
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.AddObject
		Public Property ObjectType As ObjectTypes
		Public Property PlanetColorType As PlanetColorTypes
		Public Property PlanetColor As SKColor
		Public Property PlanetTailColor As SKColor
		Public Property TrackType As TrackTypes
		Public Property TrackAngle As Single
		Public Property TrackColorType As ADTrackColorTypes
		Public Property TrackColor As SKColor
		Public Property SecondaryTrackColor As SKColor
		Public Property TrackColorAnimDuration As Single
		Public Property TrackOpacity As Single
		Public Property TrackStyle As ADTrackStyles
		Public Property TrackIcon As String
		Public Property TrackIconAngle As Single
		Public Property TrackRedSwirl As Boolean
		Public Property TrackGraySetSpeedIcon As Boolean
		Public Property TrackSetSpeedIconBpm As Single
		Public Property TrackGlowEnabled As Boolean
		Public Property TrackGlowColor As SKColor
		Public Property Position As RDPointN
		Public Property RelativeTo As ADCameraRelativeTo
		Public Property PivotOffset As RDSizeN
		Public Property Rotation As Single
		Public Property LockRotation As Boolean
		Public Property Scale As RDSizeN
		Public Property LockScale As Boolean
		Public Property Depth As Integer
		Public Property Parallax As RDSizeN
		Public Property ParallaxOffset As RDSizeN
		Public Property Tag As String
	End Class
	Public Class ADHold
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.Hold
		Public Property Duration As Integer
		Public Property DistanceMultiplier As Integer
		Public Property LandingAnimation As Boolean
	End Class
	Public Class ADSetHoldSound
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.SetHoldSound
		Public Property HoldStartSound As String
		Public Property HoldLoopSound As String
		Public Property HoldEndSound As String
		Public Property HoldMidSound As String
		Public Property HoldMidSoundType As String
		Public Property HoldMidSoundDelay As Single
		Public Property HoldMidSoundTimingRelativeTo As String
		Public Property HoldSoundVolume As Integer
	End Class
	Public Class ADMultiPlanet
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.MultiPlanet
		Public Property Planets As String
	End Class
	Public Class ADFreeRoam
		Inherits ADBaseTileEvent
		Implements IEaseEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.FreeRoam
		Public Property Duration As Single Implements IEaseEvent.Duration
		Public Property Size() As Integer
		Public Property PositionOffset() As Integer
		Public Property OutTime As Integer
		Public Property OutEase As EaseType Implements IEaseEvent.Ease
		Public Property HitsoundOnBeats As String
		Public Property HitsoundOffBeats As String
		Public Property CountdownTicks As Integer
		Public Property AngleCorrectionDir As Integer
	End Class
	Public Class ADFreeRoamTwirl
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.FreeRoamTwirl
		Public Property Position() As Integer
	End Class
	Public Class ADFreeRoamRemove
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType
		Public Property Position() As Integer
		Public Property Size() As Integer
	End Class

	Public Class ADHide
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.Hide
		Public Property HideJudgment As Boolean
		Public Property HideTileIcon As Boolean
	End Class
	Public Class ADScaleMargin
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.ScaleMargin
		Public Property Scale As Integer
	End Class
	Public Class ADScaleRadius
		Inherits ADBaseTileEvent
		Public Overrides ReadOnly Property Type As ADEventType = ADEventType.ScaleRadius
		Public Property Scale As Integer
	End Class
End Namespace