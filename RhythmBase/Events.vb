Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RhythmAsset.Sprites
Imports System.Text.RegularExpressions
Imports RhythmBase.Objects
Imports SkiaSharp
Imports RhythmBase.Animation
Imports System.Formats.Asn1.AsnWriter
Imports System.Reflection.Metadata
'#Disable Warning CA1507

Namespace Events
	Public Enum EventType
		PlaySong
		SetCrotchetsPerBar
		PlaySound
		SetBeatsPerMinute
		SetClapSounds
		SetHeartExplodeVolume
		SetHeartExplodeInterval
		SayReadyGetSetGo
		SetGameSound
		SetBeatSound
		SetCountingSound
		AddClassicBeat
		AddOneshotBeat
		SetOneshotWave
		AddFreeTimeBeat
		PulseFreeTimeBeat
		SetRowXs
		SetTheme
		SetVFXPreset
		SetBackgroundColor
		SetForeground
		SetSpeed
		Flash
		CustomFlash
		MoveCamera
		HideRow
		MoveRow
		PlayExpression
		TintRows
		BassDrop
		ShakeScreen
		FlipScreen
		InvertColors
		PulseCamera
		TextExplosion
		ShowDialogue
		ShowStatusSign
		FloatingText
		AdvanceText
		ChangePlayersRows
		FinishLevel
		Comment
		Stutter
		ShowHands
		PaintHands
		SetHandOwner
		SetPlayStyle
		TagAction
		CallCustomMethod
		NewWindowDance
		Move
		Tint
		PlayAnimation
		SetVisible
		ShowRooms
		MoveRoom
		ReorderRooms
		SetRoomContentMode
		MaskRoom
		FadeRoom
		SetRoomPerspective
		WindowResize
		ShowSubdivisionsRows
		ReadNarration
		UnknownObject
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
	Public Enum Tabs
		Song
		Rows
		Actions
		Sprites
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
	Public Interface IAnimation
		Property Ease As EaseType
		Property Duration As Single
		Function Animation() As Animation.IAnimation
	End Interface
	Public MustInherit Class BaseEvent
		<JsonIgnore>
		Public _Origin As JObject
		<JsonIgnore>
		Public PrivateData As New Dictionary(Of String, Object)
		<JsonProperty("type")>
		Public MustOverride ReadOnly Property Type As EventType
		<JsonIgnore>
		Public MustOverride ReadOnly Property Tab As Tabs
		<JsonIgnore>
		Public Property BeatOnly As Single
		Public Overridable Property Y As UInteger
		Public MustOverride ReadOnly Property Rooms As Rooms
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Tag As String
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property [If] As Condition
		Public Property Active As Boolean = True
		Public Overridable Function Copy(Of T As {BaseEvent, New})() As T
			Dim temp = New T With {.BeatOnly = BeatOnly, .Y = Y, .[If] = [If], .Tag = Tag, .Active = Active}
			If Me.If IsNot Nothing Then
				For Each item In Me.If.ConditionLists
					item.Conditional.Children.Add(temp)
				Next
			End If
			Return temp
		End Function
		Public Overrides Function ToString() As String
			Return $"[{BeatOnly}]>>[{Type}]"
		End Function
		Friend Function ShouldSerializeActive() As Boolean
			Return Not Active
		End Function
	End Class
	Public Class UnknownObject
		Inherits BaseEvent
		Public Property [Object] As JObject
			Get
				Return _Origin
			End Get
			Set(value As JObject)
				_Origin = value
			End Set
		End Property
		Private Sub New()
			Type = EventType.UnknownObject
		End Sub
		<JsonIgnore>
		Public Overrides ReadOnly Property Type As EventType = EventType.UnknownObject
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Unknown

		Public Sub New(beatOnly As Single)
			Me.New
			Me.BeatOnly = beatOnly
		End Sub
		Public Overrides Function ToString() As String
			Return $"[{BeatOnly}]>>[(Unknown){_Origin("type")}]"
		End Function
	End Class
	Public MustInherit Class BaseBeatsPerMinute
		Inherits BaseEvent
		<JsonIgnore>
		Public MustOverride Property BeatsPerMinute As Single
	End Class
	Public MustInherit Class BaseDecorationAction
		Inherits BaseEvent
		Private _parent As Decoration
		<JsonIgnore>
		Public Property Parent As Decoration
			Get
				Return _parent
			End Get
			Set(value As Decoration)
				_parent?.Children.Remove(Me)
				value?.Children.Add(Me)
				_parent = value
			End Set
		End Property
		Public Overridable ReadOnly Property Target As String
			Get
				Return If(Parent Is Nothing, "", Parent.Id)
			End Get
		End Property
		Public Overloads Function Copy(Of T As {BaseDecorationAction, New})() As T
			Dim Temp = MyBase.Copy(Of T)()
			Temp.Parent = Parent
			Return Temp
		End Function
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms
			Get
				Return Parent.Rooms
			End Get
		End Property
	End Class
	Public MustInherit Class BaseRowAction
		Inherits BaseEvent
		Private _parent As Row
		<JsonIgnore>
		Public Property Parent As Row
			Get
				Return _parent
			End Get
			Set(value As Row)
				_parent?.Children.Remove(Me)
				value.Children.Add(Me)
				_parent = value
			End Set
		End Property
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms
			Get
				Return Parent.Rooms
			End Get
		End Property
		Public ReadOnly Property Row As Integer
			Get
				Return If(Parent Is Nothing, -1, Parent.Row)
			End Get
		End Property
		Public Overloads Function Copy(Of T As {BaseRowAction, New})() As T
			Dim Temp = MyBase.Copy(Of T)()
			Temp.Parent = Parent
			Return Temp
		End Function
	End Class
	Public MustInherit Class BaseBeat
		Inherits BaseRowAction
		MustOverride Function PulseTime() As IEnumerable(Of Pulse)
		<JsonIgnore>
		MustOverride ReadOnly Property Pulsable As Boolean
	End Class
	Public MustInherit Class BaseRowAnimation
		Inherits BaseRowAction
	End Class
	Public Class PlaySong
		Inherits BaseBeatsPerMinute
		'Private _beatsPerMinute As single
		Public Song As Audio
		<JsonProperty("bpm")>
		Public Overrides Property BeatsPerMinute As Single
		Public Property BPM As Single
			Get
				Return BeatsPerMinute
			End Get
			Set(value As Single)
				BeatsPerMinute = value
			End Set
		End Property
		'	Get
		'		Return _beatsPerMinute
		'	End Get
		'	Set(value As single)
		'		If value <= 0 Then
		'			Throw New OverflowException("BeatsPerMinute must greater than 0")
		'		End If
		'		_beatsPerMinute = value
		'	End Set
		'End Property
		<JsonIgnore>
		Public Property Offset As UInteger
			Get
				Return Song.Offset
			End Get
			Set(value As UInteger)
				Song.Offset = value
			End Set
		End Property
		Public Property [Loop] As Boolean
		Public Overrides ReadOnly Property Type As EventType = EventType.PlaySong
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default

		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song

		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" BPM:{_BeatsPerMinute}, Song:{Song.Filename}"
		End Function
	End Class
	Public Class SetBeatsPerMinute
		Inherits BaseBeatsPerMinute
		Public Overrides ReadOnly Property Type As EventType = EventType.SetBeatsPerMinute
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song
		<JsonProperty("bpm")>
		Public Overrides Property BeatsPerMinute As Single
		Public Sub New(beatOnly As Single, bpm As Single, y As UInteger)
			Me.BeatOnly = beatOnly
			Me.Y = y
			Me.BeatsPerMinute = bpm
		End Sub
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" BPM:{BeatsPerMinute}"
		End Function

	End Class
	Public Class SetCrotchetsPerBar
		Inherits BaseEvent
		Private _visualBeatMultiplier As Single
		Private _crotchetsPerBar As UInteger
		Public Property Bar As UInteger = 1
		Public Overrides ReadOnly Property Type As EventType = Events.EventType.SetCrotchetsPerBar
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song
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
			End Set
		End Property
		Public Sub New(beatOnly As Single, y As UInteger, crotchetsPerBar As UInteger, visualBeatMultiplier As Single)
			Me.BeatOnly = beatOnly
			Me.Y = y
			Me.CrotchetsPerBar = crotchetsPerBar
			Me.VisualBeatMultiplier = visualBeatMultiplier
		End Sub
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
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song

	End Class
	Public Class SetClapSounds
		Inherits BaseEvent
		Public Property P1Sound As Audio
		Public Property P2Sound As Audio
		Public Property CpuSound As Audio
		Public Property RowType As RowType
		Public Overrides ReadOnly Property Type As EventType = EventType.SetClapSounds
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song

	End Class
	Public Class SetHeartExplodeVolume
		Inherits BaseEvent
		Public Property Volume As UInteger
		Public Overrides ReadOnly Property Type As EventType = EventType.SetHeartExplodeVolume
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song

	End Class

	Public Class SetHeartExplodeInterval
		Inherits BaseEvent
		Enum IntervalTypes
			OneBeatAfter
			Instant
			GatherNoCeil
			GatherAndCeil
		End Enum
		Public Property IntervalType As String
		Public Property Interval As Integer
		Public Overrides ReadOnly Property Type As EventType = EventType.SetHeartExplodeInterval
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song

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
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song
		<JsonIgnore>
		Public ReadOnly Property Splitable As Boolean
			Get
				Return PhraseToSay = Words.SayReaDyGetSetGoNew OrElse
				PhraseToSay = Words.SayGetSetGo OrElse
				PhraseToSay = Words.SayReaDyGetSetOne OrElse
				PhraseToSay = Words.SayGetSetOne OrElse
				PhraseToSay = Words.SayReadyGetSetGo
			End Get
		End Property
		Public Function Split() As IEnumerable(Of SayReadyGetSetGo)
			If Splitable Then
				Select Case PhraseToSay
					Case Words.SayReaDyGetSetGoNew
						Return New List(Of SayReadyGetSetGo) From {
						SplitCopy(0, Words.JustSayRea),
						SplitCopy(Tick, Words.JustSayDy),
						SplitCopy(Tick * 2, Words.JustSayGet),
						SplitCopy(Tick * 3, Words.JustSaySet),
						SplitCopy(Tick * 4, Words.JustSayGo)}
					Case Words.SayGetSetGo
						Return New List(Of SayReadyGetSetGo) From {
						SplitCopy(0, Words.JustSayGet),
						SplitCopy(Tick, Words.JustSaySet),
						SplitCopy(Tick * 2, Words.JustSayGo)}
					Case Words.SayReaDyGetSetOne
						Return New List(Of SayReadyGetSetGo) From {
						SplitCopy(0, Words.JustSayRea),
						SplitCopy(Tick, Words.JustSayDy),
						SplitCopy(Tick * 2, Words.JustSayGet),
						SplitCopy(Tick * 3, Words.JustSaySet),
						SplitCopy(Tick * 4, Words.Count1)}
					Case Words.SayGetSetOne
						Return New List(Of SayReadyGetSetGo) From {
						SplitCopy(0, Words.JustSayGet),
						SplitCopy(Tick, Words.JustSaySet),
						SplitCopy(Tick * 2, Words.Count1)}
					Case Words.SayReadyGetSetGo
						Return New List(Of SayReadyGetSetGo) From {
						SplitCopy(0, Words.JustSayReady),
						SplitCopy(Tick * 2, Words.JustSayGet),
						SplitCopy(Tick * 3, Words.JustSaySet),
						SplitCopy(Tick * 4, Words.JustSayGo)}
					Case Else
				End Select
			End If
			Return New List(Of SayReadyGetSetGo) From {Me}.AsEnumerable
		End Function
		Private Function SplitCopy(extraBeat As Single, word As Words) As SayReadyGetSetGo
			Dim Temp = Me.Copy(Of SayReadyGetSetGo)
			Temp.BeatOnly += extraBeat
			Temp.PhraseToSay = word
			Temp.Volume = Volume
			Return Temp
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
		Public Property Offset As Integer
			Get
				Return Audio.Offset
			End Get
			Set(value As Integer)
				Audio.Offset = value
			End Set
		End Property

		Public Property SoundSubtypes As List(Of Soundsubtype)
		Public Overrides ReadOnly Property Type As EventType = EventType.SetGameSound
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song
		Private Function ShouldSerialize() As Boolean
			Return Not (
		SoundType = SoundTypes.ClapSoundHold Or
		SoundType = SoundTypes.FreezeshotSound Or
		SoundType = SoundTypes.BurnshotSound)
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
	Public Class SetBeatSound
		Inherits BaseRowAction
		Public Property Sound As New Audio
		Public Overrides ReadOnly Property Type As EventType = EventType.SetBeatSound
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song

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
		End Enum
		Public Property VoiceSource As VoiceSources
		Public Property Enabled As Boolean
		Public Property SubdivOffset As Single
		Public Property Volume As Integer
		Public Overrides ReadOnly Property Type As EventType = EventType.SetCountingSound
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song

	End Class

	Public Class SetTheme
		Inherits BaseEvent
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
		Public Property Preset As Theme = Theme.OrientalTechno
		Public Overrides ReadOnly Property Type As EventType = Events.EventType.SetTheme
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Preset}"
		End Function
	End Class
	Public Class SetVFXPreset
		Inherits BaseEvent
		Implements IAnimation
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
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		Public Property Preset As Presets
		Public Property Enable As Boolean
		Public Property Threshold As Single
		Public Property Intensity As Single
		Public ReadOnly Property Color As New PanelColor(False)
		Public Property FloatX As Single
		Public Property FloatY As Single
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Property Duration As Single Implements IAnimation.Duration
		Public Overrides ReadOnly Property Type As EventType = EventType.SetVFXPreset
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			If PropertyHasDuration() Then
				If ShouldSerializeIntensity() Then
					Return New Pair(Me, New NumberOrExpressionPair(FloatX, FloatY), Ease, BeatOnly, Duration)
				ElseIf ShouldSerializeColor() Then
					Return New Gradient(Me, Color.Value, Nothing, Intensity, Threshold, Ease, BeatOnly, Duration)
				Else
					Return New Value(Me, New Number(Intensity), Ease, BeatOnly, Duration)
				End If
			Else
				Return Nothing
			End If
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Preset}"
		End Function
		Friend Function ShouldSerializeEnable() As Boolean
			Return Preset <> Presets.DisableAll
		End Function
		Friend Function ShouldSerializeThreshold() As Boolean '阈值
			Return Preset = Presets.Bloom
		End Function
		Friend Function ShouldSerializeIntensity() As Boolean '强度
			Return PropertyHasDuration() And
			Preset <> Presets.TileN And
			Preset <> Presets.CustomScreenScroll
		End Function
		Friend Function ShouldSerializeColor() As Boolean
			Return Preset = Presets.Bloom
		End Function
		Friend Function ShouldSerializeFloatX() As Boolean
			Return Preset = Presets.TileN Or
			Preset = Presets.CustomScreenScroll
		End Function
		Friend Function ShouldSerializeFloatY() As Boolean
			Return Preset = Presets.TileN Or
			Preset = Presets.CustomScreenScroll
		End Function
		Friend Function ShouldSerializeEase() As Boolean
			Return PropertyHasDuration()
		End Function
		Friend Function ShouldSerializeDuration() As Boolean
			Return PropertyHasDuration()
		End Function
		Public Function PropertyHasDuration()
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
		Implements IAnimation
		Enum BackgroundTypes
			Color
			Image
		End Enum
		Enum FilterModes
			NearestNeighbor
		End Enum
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Property ContentMode As ContentModes
		Public Property Filter As FilterModes '?
		Public ReadOnly Property Color As New PanelColor(True)
		Public Property Interval As Single
		Public Property BackgroundType As BackgroundTypes
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property Fps As Integer
		Public Property Image As List(Of ISprite)
		Public Property ScrollX As Integer
		Public Property ScrollY As Integer
		Public Property TilingType As TilingTypes
		Public Overrides ReadOnly Property Type As EventType = Events.EventType.SetBackgroundColor
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Pair(Me, New NumberOrExpressionPair(ScrollX, ScrollY), Ease, BeatOnly, Duration)
		End Function
	End Class
	Public Class SetForeground
		Inherits BaseEvent
		Implements IAnimation
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
		Public Property ContentMode As ContentModes
		Public Property TilingType As TilingTypes
		Public ReadOnly Property Color As New PanelColor(True)
		Public Property Image As List(Of ISprite)
		Public Property Fps As Single
		Public Property ScrollX As Single
		Public Property ScrollY As Single
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property Interval As Single
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.SetForeground
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Pair(Me, New NumberOrExpressionPair(ScrollX, ScrollY), Ease, BeatOnly, Duration)
		End Function

	End Class
	Public Class SetSpeed
		Inherits BaseEvent
		Implements IAnimation
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Property Speed As Single
		Public Property Duration As Single Implements IAnimation.Duration
		Public Overrides ReadOnly Property Type As EventType = EventType.SetSpeed
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Value(Me, New Number(Speed), Ease, BeatOnly, Duration)
		End Function
	End Class
	Public Class Flash
		Inherits BaseEvent
		Enum Durations
			[Short]
			Medium
			[Long]
		End Enum
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		Public Property Duration As Durations

		Public Overrides ReadOnly Property Type As EventType = EventType.Flash
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class
	Public Class CustomFlash
		Inherits BaseEvent
		Implements IAnimation
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public ReadOnly Property StartColor As New PanelColor(False)
		Public Property Background As Boolean
		Public ReadOnly Property EndColor As New PanelColor(False)
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property StartOpacity As Integer
		Public Property EndOpacity As Integer
		Public Overrides ReadOnly Property Type As EventType = EventType.CustomFlash
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Gradient(Me, StartColor.Value, EndColor.Value, StartOpacity, EndOpacity, Ease, BeatOnly, Duration)
		End Function
	End Class
	Public Class MoveCamera
		Inherits BaseEvent
		Implements IAnimation
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property CameraPosition As NumberOrExpressionPair
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Zoom As Integer?
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Angle As INumberOrExpression
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.MoveCamera
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Movement(Me, CameraPosition, New NumberOrExpressionPair(Zoom, Zoom), Nothing, Angle, Ease, BeatOnly, Duration)
		End Function
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
	Public Class MoveRow
		Inherits BaseRowAnimation
		Implements IAnimation
		Enum Targets
			WholeRow
			Heart
			Character
		End Enum
		Public Property CustomPosition As Boolean
		Public Property Target As Targets
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property RowPosition As NumberOrExpressionPair
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Scale As NumberOrExpressionPair
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Angle As INumberOrExpression
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Pivot As Single?
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.MoveRow
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Movement(Me, RowPosition, Scale, New NumberOrExpressionPair(Pivot, 50), Angle, Ease, BeatOnly, Duration)
		End Function

	End Class
	Public Class ShowSubdivisionsRows
		Inherits BaseEvent
		Public Enum SubdivisionRowMode
			Mini
			Normal
		End Enum
		Public Overrides ReadOnly Property type As EventType = EventType.ShowSubdivisionsRows
		Public Property Subdivisions As Integer
		Public Property Mode As SubdivisionRowMode
		Public Property ArcAngle As Integer
		Public Property SpinsPerSecond As Single
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Overrides ReadOnly Property Rooms As Rooms
	End Class

	Public Class PlayExpression
		Inherits BaseRowAnimation
		Public Property Expression As String
		Public Property Replace As Boolean
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Overrides ReadOnly Property Type As EventType = EventType.PlayExpression
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class
	Public Class TintRows
		Inherits BaseRowAnimation
		Implements IAnimation
		Enum RowEffect
			None
			Electric
#If DEBUG Then
			Smoke
#End If
		End Enum
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public ReadOnly Property TintColor As New PanelColor(True)
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Property Border As Borders
		Public ReadOnly Property BorderColor As New PanelColor(True)
		Public Property Opacity As Integer
		Public Property Tint As Boolean
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property Effect As RowEffect
		Public Overrides ReadOnly Property Type As EventType = EventType.TintRows
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Friend Function ShouldSerializeDuration() As Boolean
			Return Duration <> 0
		End Function
		Friend Function ShouldSerializeEase() As Boolean
			Return Duration <> 0
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $"row:{Row}"
		End Function
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Gradient(Me,
				If(Tint, TintColor.Value, SKColors.Transparent),
				If(Not Border = Borders.None, BorderColor.Value, SKColors.Transparent),
				Opacity,
				Nothing,
				Ease,
				BeatOnly,
				Duration
			)
		End Function
	End Class

	Public Class BassDrop
		Inherits BaseEvent
		Enum StrengthType
			Low
			Medium
			High
		End Enum
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		Public Property Strength As StrengthType
		Public Overrides ReadOnly Property Type As EventType = EventType.BassDrop
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class
	Public Class ShakeScreen
		Inherits BaseEvent
		Enum ShakeLevels
			Low
			Medium
			High
		End Enum
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		Public Property ShakeLevel As ShakeLevels
		Public Overrides ReadOnly Property Type As EventType = EventType.ShakeScreen
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class

	Public Class FlipScreen
		Inherits BaseEvent
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		Public Property FlipX As Boolean
		Public Property FlipY As Boolean

		Public Overrides ReadOnly Property Type As EventType = EventType.FlipScreen
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class

	Public Class InvertColors
		Inherits BaseEvent
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
		Public Property Enable As Boolean
		Public Overrides ReadOnly Property Type As EventType = EventType.InvertColors
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class
	Public Class PulseCamera
		Inherits BaseEvent
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		Public Property Strength As Byte
		Public Property Count As Integer
		Public Property Frequency As Single
		Public Overrides ReadOnly Property Type As EventType = EventType.PulseCamera
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
	End Class
	Public Class TextExplosion
		Inherits BaseEvent
		Enum Directions
			Left
			Right
		End Enum
		Enum Modes
			OneColor
			Random
		End Enum
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
		Public ReadOnly Property Color As New PanelColor(False)
		Public Property Text As String
		Public Property Direction As Directions
		Public Property Mode As Modes
		Public Overrides ReadOnly Property Type As EventType = EventType.TextExplosion
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class
	'NarrateRowInfo
	'Tile
	'MaskRoom
	Public Enum NarrationCategory
		Fallback
		Navigation
		Instruction
		Notification
		Dialogue
		Description = 6
		Subtitles
	End Enum
	Public Class ReadNarration
		Inherits BaseEvent
		Public Overrides ReadOnly Property Type As EventType = EventType.ReadNarration

		Public Property Text As String
		Public Property Category As NarrationCategory

		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
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
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class
	Public Class ShowStatusSign
		Inherits BaseEvent
		Public Property UseBeats As Boolean = True
		Public Property Narrate As Boolean = True
		Public Property Text As String
		Public Property Duration As Single
		Public Overrides ReadOnly Property Type As EventType = EventType.ShowStatusSign
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class
	Public Class FloatingText
		Inherits BaseEvent
		<Flags>
		Public Enum OutMode
			FadeOut
			HideAbruptly
		End Enum
		<Flags>
		Public Enum AnchorStyle
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
		Public Overrides ReadOnly Property Type As EventType = Events.EventType.FloatingText
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		<JsonIgnore>
		Public ReadOnly Property Children As List(Of AdvanceText)
			Get
				Return _children '.OrderBy(Function(i) i.Bar * 50 + i.Beat).ToList
			End Get
		End Property
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		Public Property FadeOutRate As Single
		Public ReadOnly Property Color As New PanelColor(True) With {.Color = New SKColor(&HFF, &HFF, &HFF, &HFF)}
		Public Property Angle As INumberOrExpression
		Public Property Size As UInteger
		Public ReadOnly Property OutlineColor As New PanelColor(True) With {.Color = New SKColor(0, 0, 0, &HFF)}
		Public Property Id As Integer
			Get
				Return GeneratedId
			End Get
			Set(value As Integer)
			End Set
		End Property
		Public Property TextPosition As NumberOrExpressionPair = (50, 50)
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
		<JsonConstructor>
		Public Sub New()
			MyBase.New()
			GeneratedId = _PrivateId
			_PrivateId += 1
		End Sub
		Public Function CreateAdvanceText(beatOnly As Single) As AdvanceText
			Dim A As New AdvanceText With {.BeatOnly = beatOnly, .Y = Y, .Parent = Me}
			_children.Add(A)
			Return A
		End Function
		'Public Sub Split(mode As SplitMode)
		'	Select Case mode
		'		Case SplitMode.PER_CHAR
		'			_Text = String.Join("/", _Text.ToList)
		'		Case SplitMode.PROGRESSIVE_CHAR
		'			_Text = String.Join("/", _Text.ToList)
		'	End Select
		'End Sub
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" Text:{_Text}"
		End Function

	End Class
	Public Class AdvanceText
		Inherits BaseEvent
		Public Overrides ReadOnly Property Type As EventType = Events.EventType.AdvanceText
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		<JsonIgnore>
		Public Property Parent As FloatingText
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property FadeOutDuration As Single
		Public ReadOnly Property Id As Integer
			Get
				Return Parent.Id
			End Get
		End Property
		Public Overrides Function ToString() As String
			Return MyBase.ToString + $" Index: {_Parent.Children.IndexOf(Me)}"
		End Function
	End Class
	Public Class ChangePlayersRows
		Inherits BaseEvent
		Enum PlayerTypes
			P1
			P2
			CPU
			NoChange
		End Enum
		Enum CpuType
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
		Public Property Players As New List(Of PlayerTypes)(16)
		Public Property PlayerMode As PlayerModes
		Public Property CpuMarkers() As New List(Of CpuType)(16)
		Public Overrides ReadOnly Property Type As EventType = EventType.ChangePlayersRows
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class
	Public Class FinishLevel
		Inherits BaseEvent
		Public Overrides ReadOnly Property Type As EventType = EventType.FinishLevel
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class

	Public Class Comment
		Inherits BaseDecorationAction
		<JsonProperty("tab")>
		Public CustomTab As Tabs
		<JsonIgnore>
		Public Overrides ReadOnly Property Tab As Tabs
			Get
				Return CustomTab
			End Get
		End Property
		Public Property Show As Boolean
		Public Property Text As String
		Public Overrides ReadOnly Property Target As String
			Get
				Return MyBase.Target
			End Get
		End Property
		Public ReadOnly Property Color As New PanelColor(False)
		Public Overrides ReadOnly Property Type As EventType = EventType.Comment
		Friend Function ShouldSerializeTarget() As Boolean
			Return Tab = Tabs.Sprites
		End Function

	End Class
	Public Class Stutter
		Inherits BaseEvent
		Enum Actions
			Add
			Cancel
		End Enum
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
		Public Property SourceBeat As Single
		Public Property Length As Single
		Public Property Action As Actions
		Public Property Loops As Integer
		Public Overrides ReadOnly Property Type As EventType = EventType.Stutter
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

	End Class
	Public Class ShowHands
		Inherits BaseEvent
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
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
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
		Implements IAnimation
		Enum Borders
			None
			Outline
			Glow
		End Enum
		Public ReadOnly Property TintColor As New PanelColor(True)
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Property Border As Borders
		Public ReadOnly Property BorderColor As New PanelColor(True)
		Public Property Opacity As Integer
		Public Property Tint As Boolean
		Public Property Duration As Single Implements IAnimation.Duration
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
		Public Property Hands As PlayerHands
		Public Overrides ReadOnly Property Type As EventType = EventType.PaintHands
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Gradient(Me,
			If(Tint, TintColor.Value, SKColors.Transparent),
			If(Not Border = Borders.None, BorderColor.Value, SKColors.Transparent),
			Opacity,
			Nothing,
			Ease,
			BeatOnly,
			Duration
		)
		End Function
	End Class
	Public Class SetHandOwner
		Inherits BaseEvent
		Enum Characters
			Players
			Ian
			Paige
			Edega
		End Enum
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
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
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
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
		<JsonIgnore>
		Public Property Action As Actions
		<JsonProperty("Tag")>
		Public Property ActionTag As String
		Public Overrides ReadOnly Property Type As EventType = EventType.TagAction
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function ControllingEvents(level As RDLevel) As IEnumerable(Of IGrouping(Of String, BaseEvent))
			Return level.GetTaggedEvents(ActionTag, Action.HasFlag(Actions.All))
		End Function
		Public Function ControllingEventsPadLeft(level As RDLevel) As IEnumerable(Of IGrouping(Of String, BaseEvent))
			Dim L = ControllingEvents(level)
			For Each pair In L
				Dim start = pair(0).BeatOnly
				For Each item In pair
					item.BeatOnly -= start
				Next
			Next
			Return L
		End Function
		Public Shared Function ControllingEvents(level As RDLevel, ParamArray tag As SpecialTag()) As IEnumerable(Of IGrouping(Of String, BaseEvent))
			Return level.GetTaggedEvents("[" + tag.ToString + "]", False)
		End Function
		Public Shared Function ControllingEventsPadLeft(level As RDLevel, ParamArray tag As SpecialTag()) As IEnumerable(Of IGrouping(Of String, BaseEvent))
			Dim L = ControllingEvents(level, tag)
			For Each pair In L
				Dim start = pair(0).BeatOnly
				For Each item In pair
					item.BeatOnly -= start
				Next
			Next
			Return L
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
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Sub Run(variables As Variables)
			Throw New NotImplementedException
		End Sub
	End Class
	Public Class NewWindowDance
		Inherits BaseEvent
		Implements IAnimation
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
		Public Property Position As NumberOrExpressionPair
		Public Property Reference As References
		Public Property UseCircle As Boolean
		Public Property Speed As Single
		Public Property Amplitude As New Single
		Public Property AmplitudeVector As NumberOrExpressionPair
		Public Property Angle As INumberOrExpression
		Public Property Frequency As Single
		Public Property Period As Single
		Public Property EaseType As EaseTypes
		Public Property SubEase As EaseType
		Public Property EasingDuration As Single Implements IAnimation.Duration
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.NewWindowDance
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Movement(Me, Position, Nothing, Nothing, Nothing, Ease, BeatOnly, EasingDuration)
		End Function
	End Class
	Public Class WindowResize
		Inherits BaseEvent
		Implements IAnimation
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
		Public Overrides ReadOnly Property Type As EventType = EventType.WindowResize
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Scale As NumberOrExpressionPair
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Pivot As NumberOrExpressionPair
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property Ease As EaseType Implements IAnimation.Ease
		Private Sub New()
		End Sub
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Movement(Me, Nothing, Scale, Pivot, Nothing, Ease, BeatOnly, Duration)
		End Function
	End Class
	Public Class PlayAnimation
		Inherits BaseDecorationAction
		Public Overrides ReadOnly Property Type As EventType = Events.EventType.PlayAnimation
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sprites
		Public Property Expression As String
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" Expression:{Expression}"
		End Function
	End Class
	Public Class Tint
		Inherits BaseDecorationAction
		Implements IAnimation
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Property Border As Borders
		Public ReadOnly Property BorderColor As New PanelColor(True)
		Public Property Opacity As Integer
		Public Property Tint As Boolean
		Public ReadOnly Property TintColor As New PanelColor(True) With {.Color = New SKColor(&HFF, &HFF, &HFF, &HFF)}
		Public Property Duration As Single Implements IAnimation.Duration
		Public Overrides ReadOnly Property Type As EventType = EventType.Tint
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sprites
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Animation.Gradient(Me,
				If(Tint, TintColor.Value, SKColors.Transparent),
				If(Not Border = Borders.None, BorderColor.Value, SKColors.Transparent),
				Opacity,
				Nothing,
				Ease,
				BeatOnly,
				Duration
			)
		End Function
		Friend Function ShouldSerializeDuration() As Boolean
			Return Duration <> 0
		End Function
		Friend Function ShouldSerializeEase() As Boolean
			Return Duration <> 0
		End Function
	End Class
	Public Class Move
		Inherits BaseDecorationAction
		Implements IAnimation
		Public Overrides ReadOnly Property Type As EventType = EventType.Move
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sprites
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Position As NumberOrExpressionPair?
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Scale As NumberOrExpressionPair?
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Angle As INumberOrExpression
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property Pivot As NumberOrExpressionPair?
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Movement(Me,
				Position,
				Scale,
				Pivot,
				Angle,
				Ease,
				BeatOnly,
				Duration)
		End Function
		<JsonIgnore>
		Public Overrides Property Y As UInteger
			Get
				Return 0
			End Get
			Set(value As UInteger)
			End Set
		End Property

		Public Overrides Function ToString() As String
			Return MyBase.ToString()
		End Function
	End Class
	Public Class SetVisible
		Inherits BaseDecorationAction
		Private _visible As Boolean
		Public Overrides ReadOnly Property Type As EventType = EventType.SetVisible
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sprites
		Public Property Visible As Boolean
			Get
				Return _visible
			End Get
			Set(value As Boolean)
				_visible = value
			End Set
		End Property

		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {_visible}"
		End Function
	End Class
	Public Class AddClassicBeat
		Inherits BaseBeat
		Enum Patterns
			ThreeBeat
			FourBeat
		End Enum
		Public Property Tick As Single
		Public Property Swing As Single
		Public Property Hold As Single
		<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
		Public Property SetXs As Patterns?
		<JsonIgnore>
		Public ReadOnly Property Pattern As String
			Get
				If SetXs Is Nothing Then
					Return CType(Parent.Children.LastOrDefault(Function(i) i.Type = EventType.SetRowXs AndAlso Parent.Children.IndexOf(i) < Parent.Children.IndexOf(Me), New SetRowXs With {.Pattern = "------"}), SetRowXs).Pattern
				Else
					Select Case SetXs
						Case Patterns.ThreeBeat
							Return "-xx-xx"
						Case Patterns.FourBeat
							Return "-x-x-x"
						Case Else
							Throw New RhythmDoctorExcception("how")
					End Select
				End If
			End Get
		End Property
		<JsonIgnore>
		Public ReadOnly Property RowXs As SetRowXs
			Get
				If SetXs Is Nothing Then
					Return CType(Parent.Children.LastOrDefault(Function(i) i.Type = EventType.SetRowXs AndAlso Parent.Children.IndexOf(i) < Parent.Children.IndexOf(Me), New SetRowXs With {.Pattern = "------"}), SetRowXs)
				Else
					Dim T = Copy(Of SetRowXs)()
					Select Case SetXs
						Case Patterns.ThreeBeat
							T.Pattern = "-xx-xx"
						Case Patterns.FourBeat
							T.Pattern = "-x-x-x"
						Case Else
							Throw New RhythmDoctorExcception("how")
					End Select
					Return T
				End If
			End Get
		End Property
		Public Overrides ReadOnly Property Type As EventType = EventType.AddClassicBeat
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rows
		Public Overrides ReadOnly Property Pulsable As Boolean
			Get
				Return True
			End Get
		End Property
		Public Overrides Function PulseTime() As IEnumerable(Of Pulse)
			Dim X = RowXs
			Dim Synco As Single
			If X.SyncoBeat >= 0 Then
				Synco = If(X.SyncoSwing = 0, 0.5, X.SyncoSwing)
			Else
				Synco = 0
			End If
			Return New List(Of Pulse) From {New Pulse(Me, BeatOnly + _Tick * 6 - _Tick * Synco, Hold)}.AsEnumerable
		End Function
		Public Function Split() As IEnumerable(Of BaseBeat)
			Return Split(RowXs)
		End Function
		Public Function Split(Xs As SetRowXs) As IEnumerable(Of BaseBeat)
			Dim L As New List(Of BaseBeat)
			Dim Head As AddFreeTimeBeat = Copy(Of AddFreeTimeBeat)()
			Head.Pulse = 0
			Head.Hold = Hold
			L.Add(Head)
			Dim tempBeat = BeatOnly
			For i = 1 To 6
				If i < 6 AndAlso Xs.PatternEnum(i) = SetRowXs.Patterns.X Then
					Continue For
				End If
				Dim Pulse As PulseFreeTimeBeat = Copy(Of PulseFreeTimeBeat)()
				Pulse.BeatOnly += Tick * i
				If i >= Xs.SyncoBeat Then
					Pulse.BeatOnly -= Xs.SyncoSwing
				End If
				If i Mod 2 = 1 Then
					Pulse.BeatOnly += Tick - If(Swing = 0, Tick, Swing)
				End If
				Pulse.Hold = Hold
				Pulse.Action = PulseFreeTimeBeat.ActionType.Increment
				L.Add(Pulse)
			Next
			Return L.AsEnumerable
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $"{If(_Swing = 0.5, "", " Swing")}"
		End Function

	End Class
	Public Class SetRowXs
		Inherits BaseBeat
		Enum Patterns
			X
			Up
			Down
			Banana
			[Return]
			None
		End Enum
		Private _pattern As New LimitedList(Of Patterns)(6, Patterns.None)
		Public Overrides ReadOnly Property Type As EventType = EventType.SetRowXs
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rows
		Public Property Pattern As String
			Get
				Dim out = ""
				For Each item In _pattern
					Select Case item
						Case Patterns.X
							out += "x"
						Case Patterns.Up
							out += "u"
						Case Patterns.Down
							out += "d"
						Case Patterns.Banana
							out += "b"
						Case Patterns.Return
							out += "r"
						Case Patterns.None
							out += "-"
					End Select
				Next
				Return out
			End Get
			Set(value As String)
				Dim L As New LimitedList(Of Patterns)(6, Patterns.None)
				For Each c In value
					Select Case c
						Case "x"c
							L.Add(Patterns.X)
						Case "u"c
							L.Add(Patterns.Up)
						Case "d"c
							L.Add(Patterns.Down)
						Case "b"c
							L.Add(Patterns.Banana)
						Case "r"c
							L.Add(Patterns.Return)
						Case "-"c
							L.Add(Patterns.None)
					End Select
				Next
				_pattern = L
			End Set
		End Property
		<JsonIgnore>
		Public Property PatternEnum As LimitedList(Of Patterns)
			Get
				Return _pattern
			End Get
			Set(value As LimitedList(Of Patterns))
				_pattern = value
			End Set
		End Property
		Public Property SyncoBeat As SByte = -1
		Public Property SyncoSwing As Single
		Public Overrides ReadOnly Property Pulsable As Boolean
			Get
				Return False
			End Get
		End Property
		Public Overrides Function PulseTime() As IEnumerable(Of Pulse)
			Return New List(Of Pulse)
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {Pattern}"
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
		Private _freezeBurnMode As FreezeBurn?
		Private _delay As Single = 0
		Public Property PulseType As Pulse
		Public Property Subdivisions As Byte = 1
		Public Property SubdivSound As Boolean
		Public Property Tick As Single
		Public Property Loops As UInteger
		Public Property Interval As Single
		Public Property Skipshot As Boolean
		Public Property FreezeBurnMode As FreezeBurn?
			Get
				Return _freezeBurnMode
			End Get
			Set(value As FreezeBurn?)
				_freezeBurnMode = [Enum].Parse(GetType(FreezeBurn), value)
			End Set
		End Property
		Public Property Delay As Single
			Get
				Return _delay
			End Get
			Set(value As Single)
				If _freezeBurnMode = FreezeBurn.Freezeshot Then
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
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rows

		Public Overrides ReadOnly Property Pulsable As Boolean
			Get
				Return True
			End Get
		End Property

		Public Overrides Function PulseTime() As IEnumerable(Of Objects.Pulse)
			Dim L As New List(Of Objects.Pulse)
			For i As UInteger = 0 To _Loops
				For j As SByte = 0 To _Subdivisions - 1
					L.Add(New Objects.Pulse(Me, BeatOnly + i * _Interval + _Tick + _delay + j * (_Tick / _Subdivisions), 0))
				Next
			Next
			Return L.AsEnumerable
		End Function
		Public Function Split() As IEnumerable(Of AddOneshotBeat)
			Dim L As New List(Of AddOneshotBeat)
			For i As UInteger = 0 To _Loops
				Dim T = Copy(Of AddOneshotBeat)()
				T._freezeBurnMode = _freezeBurnMode
				T._delay = Delay
				T.PulseType = PulseType
				T.Subdivisions = Subdivisions
				T.SubdivSound = SubdivSound
				T.Tick = Tick
				T.Loops = 0
				T.Interval = 0
				T.BeatOnly += i * _Interval
				L.Add(T)
			Next
			Return L.AsEnumerable
		End Function
		Public Function ShouldSerializeSkipshot() As Boolean
			Return Skipshot
		End Function
		Public Overrides Function ToString() As String
			Return MyBase.ToString() + $" {_freezeBurnMode} {_PulseType}"
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
		Public Overrides ReadOnly Property Type As EventType = Events.EventType.SetOneshotWave
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rows

		Public Overrides ReadOnly Property Pulsable As Boolean
			Get
				Return False
			End Get
		End Property

		Public Overrides Function PulseTime() As IEnumerable(Of Pulse)
			Return New List(Of Pulse)
		End Function

	End Class
	Public Class AddFreeTimeBeat
		Inherits BaseBeat
		Public Property Hold As Single
		Public Property Pulse As Byte
		Public Overrides ReadOnly Property Type As EventType = EventType.AddFreeTimeBeat
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rows
		Public Overrides Function PulseTime() As IEnumerable(Of Pulse)
			If Pulse = 6 Then
				Return New List(Of Pulse) From {New Pulse(Me, BeatOnly, Hold)}.AsEnumerable
			End If
			Return New List(Of Pulse)
		End Function
		Public Overrides ReadOnly Property Pulsable As Boolean
			Get
				Return Pulse = 6
			End Get
		End Property
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
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rows
		Public Overrides Function PulseTime() As IEnumerable(Of Pulse)
			If Pulsable Then
				Return New List(Of Pulse) From {New Pulse(Me, BeatOnly, Hold)}
			End If
			Return New List(Of Pulse)
		End Function
		Public Overrides ReadOnly Property Pulsable As Boolean
			Get
				Dim PulseIndexMin = 6
				Dim PulseIndexMax = 6
				For Each item In Parent.Children.Where(
				Function(i) Parent.Children.IndexOf(i) <= Parent.Children.IndexOf(Me) AndAlso
				(i.Type = EventType.AddFreeTimeBeat Or i.Type = EventType.PulseFreeTimeBeat)).Reverse
					Select Case item.Type
						Case EventType.AddFreeTimeBeat
							Dim Temp = CType(item, AddFreeTimeBeat)
							If PulseIndexMin <= Temp.Pulse And Temp.Pulse <= PulseIndexMax Then
								Return True
							End If
						Case EventType.PulseFreeTimeBeat
							Dim Temp = CType(item, PulseFreeTimeBeat)
							Select Case Temp.Action
								Case ActionType.Increment
									If PulseIndexMin > 0 Then
										PulseIndexMin -= 1
									End If
									If PulseIndexMax > 0 Then
										PulseIndexMax -= 1
									Else
										Return False
									End If
								Case ActionType.Decrement
									If PulseIndexMin > 0 Then
										PulseIndexMin += 1
									End If
									If PulseIndexMax < 6 Then
										PulseIndexMax += 1
									Else
										Return False
									End If
								Case ActionType.Custom
									If PulseIndexMin <= Temp.CustomPulse And Temp.CustomPulse <= PulseIndexMax Then
										PulseIndexMin = 0
										PulseIndexMax = 5
									Else
										Return False
									End If
								Case ActionType.Remove
									Return False
							End Select
							If PulseIndexMin > PulseIndexMax Then
								Return False
							End If
					End Select
				Next
				Return False
			End Get
		End Property
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
		Implements IAnimation
		<JsonProperty>
		Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Property Heights As New List(Of Integer)(4)
		Public Property TransitionTime As Single Implements IAnimation.Duration

		Public Overrides ReadOnly Property Type As EventType = EventType.ShowRooms
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Animation.Object(Me, Heights, Ease, BeatOnly, TransitionTime)
		End Function

	End Class
	Public Class MoveRoom
		Inherits BaseEvent
		Implements IAnimation
		Public Property RoomPosition As NumberOrExpressionPair
		Public Property Scale As NumberOrExpressionPair
		Public Property Angle As INumberOrExpression
		Public Property Pivot As NumberOrExpressionPair
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.MoveRoom
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms
			Get
				Return New Rooms(Y)
			End Get
		End Property
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Movement(Me, RoomPosition, Scale, Pivot, Angle, Ease, BeatOnly, Duration)
		End Function
	End Class
	Public Class ReorderRooms
		Inherits BaseEvent
		Public Property Order As List(Of UInteger)
		Public Overrides ReadOnly Property Type As EventType = EventType.ReorderRooms
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
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
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms
			Get
				Return New Rooms(Y)
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
		Public Property Image As List(Of ISprite)
		Public Property Fps As UInteger
		Public ReadOnly Property KeyColor As New PanelColor(False)
		Public Property ColorCutoff As Single
		Public Property ColorFeathering As Single
		Public Property ContentMode As ContentModes
		Public Overrides ReadOnly Property Type As EventType = EventType.MaskRoom
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms
			Get
				Return New Rooms(Y)
			End Get
		End Property
	End Class
	Public Class FadeRoom
		Inherits BaseEvent
		Implements IAnimation
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Property Opacity As UInteger
		Public Property Duration As Single Implements IAnimation.Duration
		Public Overrides ReadOnly Property Type As EventType = EventType.FadeRoom
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms
			Get
				Return New Rooms(Y)
			End Get
		End Property
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Value(Me, New Number(Opacity), Ease, BeatOnly, Duration)
		End Function
	End Class
	Public Class SetRoomPerspective
		Inherits BaseEvent
		Implements IAnimation
		Public Property CornerPositions As New List(Of NumberOrExpressionPair)(4)
		Public Property Duration As Single Implements IAnimation.Duration
		Public Property Ease As EaseType Implements IAnimation.Ease
		Public Overrides ReadOnly Property Type As EventType = EventType.SetRoomPerspective
		Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
		<JsonIgnore>
		Public Overrides ReadOnly Property Rooms As Rooms
			Get
				Return New Rooms(Y)
			End Get
		End Property
		Public Function Animation() As Animation.IAnimation Implements IAnimation.Animation
			Return New Animation.Object(Me, CornerPositions, Ease, BeatOnly, Duration)
		End Function
	End Class
End Namespace