Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Text.RegularExpressions
Imports SkiaSharp
#Disable Warning CA1507

Namespace Objects
	Public Module Events
		''' <summary>
		''' 事件类型的枚举
		''' </summary>
		Public Enum EventType
			''' <summary>
			''' 播放音乐
			''' </summary>
			PlaySong
			''' <summary>
			''' 设置音节长度
			''' </summary>
			SetCrotchetsPerBar
			''' <summary>
			''' 播放音效
			''' </summary>
			PlaySound
			''' <summary>
			''' 设置每分钟节拍数
			''' </summary>
			SetBeatsPerMinute
			''' <summary>
			''' 设置按拍音效
			''' </summary>
			SetClapSounds
			''' <summary>
			''' 设置心跳音量
			''' </summary>
			SetHeartExplodeVolume
			''' <summary>
			''' 设置心跳间隔
			''' </summary>
			SetHeartExplodeInterval
			''' <summary>
			''' 护士语音设置
			''' </summary>
			SayReadyGetSetGo
			''' <summary>
			''' 设置游戏音效
			''' </summary>
			SetGameSound
			''' <summary>
			''' 设置节拍音效
			''' </summary>
			SetBeatSound
			''' <summary>
			''' 设置数拍音效
			''' </summary>
			SetCountingSound
			''' <summary>
			''' 增加普通节拍
			''' </summary>
			AddClassicBeat
			''' <summary>
			''' 设置单发节拍
			''' </summary>
			AddOneshotBeat
			''' <summary>
			''' 设置单发波
			''' </summary>
			SetOneshotWave
			''' <summary>
			''' 设置自由节拍
			''' </summary>
			AddFreeTimeBeat
			''' <summary>
			''' 设置自由节拍脉冲
			''' </summary>
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
		Enum TilingTypes
			Scroll
			Pulse
		End Enum
		Enum ContentModes
			ScaleToFill
			AspectFit
			AspectFill
			Center
			Tiled
		End Enum
		Enum DefaultAudios
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
		Enum RowType
			Classic
			Oneshot
		End Enum
		Enum Tabs
			Song
			Rows
			Actions
			Sprites
			Rooms
			Unknown
		End Enum
		Enum PlayerHands
			Left
			Right
			Both
			p1
			p2
		End Enum
		Enum Borders
			None
			Outline
			Glow
		End Enum
		Enum RowEffect
			None
			Electric
#If Not DEBUG Then
			Smoke
#End If
		End Enum
		<Flags>
		Public Enum AnchorType
			[Default] = &B0
			Lower = &B1
			Upper = &B10
			Middle = &B11
			Right = &B100
			Left = &B1000
			Center = &B1100
		End Enum
		Public MustInherit Class BaseEvent
			<JsonIgnore>
			Public _Origin As JObject
			<JsonIgnore>
			Public PrivateData As New Dictionary(Of String, Object)
			''' <summary>
			''' 事件类型
			''' </summary>
			''' <returns></returns>
			<JsonProperty("type")>
			Public MustOverride ReadOnly Property Type As EventType
			''' <summary>
			''' 所属事件栏
			''' </summary>
			''' <returns></returns>
			<JsonIgnore>
			Public MustOverride ReadOnly Property Tab As Tabs
			''' <summary>
			''' 临时：纯节拍数
			''' </summary>
			''' <returns></returns>
			<JsonIgnore>
			Public Property BeatOnly As Single
			''' <summary>
			''' 事件排列高度
			''' </summary>
			''' <returns></returns>
			Public Overridable Property Y As UInteger
			''' <summary>
			''' 房间
			''' </summary>
			''' <returns></returns>
			Public MustOverride ReadOnly Property Rooms As Rooms
			''' <summary>
			''' 标签
			''' </summary>
			''' <returns></returns>
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property Tag As String
			''' <summary>
			''' 条件
			''' </summary>
			''' <returns></returns>
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property [If] As Conditions
			''' <summary>
			''' 激活
			''' </summary>
			''' <returns></returns>
			Public Property Active As Boolean = True
			Protected Sub New()
				_BeatOnly = 0
				_Y = 0
			End Sub
			Public Sub New(beatOnly As Single, Y As UInteger)
				_BeatOnly = beatOnly
				_Y = Y
			End Sub
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
			Public Function ShouldSerializeActive() As Boolean
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
		Public MustInherit Class BaseDecorationActions
			Inherits BaseEvent
			<JsonIgnore>
			Public Property Parent As Decoration
			Public Overridable ReadOnly Property Target As String
				Get
					Return If(Parent Is Nothing, "", Parent.Id)
				End Get
			End Property
			Public Overloads Function Copy(Of T As {BaseDecorationActions, New})() As T
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
			Public Sub ChangeParentTo(deco As Decoration)
				Parent.Children.Remove(Me)
				deco.Children.Add(Me)
				Parent = deco
			End Sub
		End Class
		Public MustInherit Class BaseRows
			Inherits BaseEvent
			<JsonIgnore>
			Public Property Parent As Row
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
			Public Overloads Function Copy(Of T As {BaseRows, New})() As T
				Dim Temp = MyBase.Copy(Of T)()
				Temp.Parent = Parent
				Return Temp
			End Function
			Public Sub ChangeParentTo(row As Row)
				Parent.Children.Remove(Me)
				row.Children.Add(Me)
				Parent = row
			End Sub
		End Class
		Public MustInherit Class BaseBeats
			Inherits BaseRows
			MustOverride Function PulseTime() As IEnumerable(Of Pulse)
			<JsonIgnore>
			MustOverride ReadOnly Property Pulsable As Boolean
		End Class
		Public MustInherit Class BaseRowAnimations
			Inherits BaseRows
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
				MyBase.New(beatOnly, y)
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
			Public Property SustomSoundType As CustomSoundTypes
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
		Public Class SetBeatSound
			Inherits BaseRows
			Public Property Sound As New Audio
			Public Overrides ReadOnly Property Type As EventType = EventType.SetBeatSound
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Song

		End Class

		Public Class SetCountingSound
			Inherits BaseRows
			Enum VoiceSources
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
			End Enum
			Public Property VoiceSource As VoiceSources
			Public Property Enabled As Boolean
			Public Property SubdivOffset As Single '?
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
			Public Overrides ReadOnly Property Rooms As New Rooms (False,true)
			Public Overrides Function ToString() As String
				Return MyBase.ToString() + $" {Preset}"
			End Function
		End Class
		Public Class SetVFXPreset
			Inherits BaseEvent
			Enum Presets
				''' <summary>
				''' 心跳剪影
				''' </summary>
				SilhouettesOnHBeat
				''' <summary>
				''' 晕影
				''' </summary>
				Vignette
				''' <summary>
				''' 闪烁晕影
				''' </summary>
				VignetteFlicker
				''' <summary>
				''' 彩色冲击波
				''' </summary>
				ColourfulShockwaves
				''' <summary>
				''' 按键重低音
				''' </summary>
				BassDropOnHit
				''' <summary>
				''' 心跳震屏
				''' </summary>
				ShakeOnHeartBeat
				''' <summary>
				''' 按键震屏
				''' </summary>
				ShakeOnHit
				''' <summary>
				''' 轨道浮动
				''' </summary>
				WavyRows
				''' <summary>
				''' 垂直亮纹
				''' </summary>
				LightStripVert
				''' <summary>
				''' VHS
				''' </summary>
				VHS
				''' <summary>
				''' 过场模式
				''' </summary>
				CutsceneMode
				''' <summary>
				''' 色调偏移
				''' </summary>
				HueShift
				''' <summary>
				''' 亮度
				''' </summary>
				Brightness
				''' <summary>
				''' 对比度
				''' </summary>
				Contrast
				''' <summary>
				''' 饱和度
				''' </summary>
				Saturation
				''' <summary>
				''' 噪点
				''' </summary>
				Noise
				''' <summary>
				''' 干扰
				''' </summary>
				GlitchObstruction
				''' <summary>
				''' 落雨
				''' </summary>
				Rain
				''' <summary>
				''' 矩阵
				''' </summary>
				Matrix
				''' <summary>
				''' 纸屑
				''' </summary>
				Confetti
				''' <summary>
				''' 落花
				''' </summary>
				FallingPetals
				''' <summary>
				''' 落花-即刻
				''' </summary>
				FallingPetalsInstant
				''' <summary>
				''' 飘雪
				''' </summary>
				FallingPetalsSnow
				''' <summary>
				''' 雪花
				''' </summary>
				Snow
				''' <summary>
				''' 高光
				''' </summary>
				Bloom
				''' <summary>
				''' 橙色高光
				''' </summary>
				OrangeBloom
				''' <summary>
				''' 蓝色高光
				''' </summary>
				BlueBloom
				''' <summary>
				''' 镜厅
				''' </summary>
				HallOfMirrors
				''' <summary>
				''' 自定义屏幕块
				''' </summary>
				TileN
				''' <summary>
				''' 怀旧
				''' </summary>
				Sepia
				''' <summary>
				''' 自定义滚屏
				''' </summary>
				CustomScreenScroll
				''' <summary>
				''' JPEG 失真
				''' </summary>
				JPEG
				''' <summary>
				''' 脉冲计数
				''' </summary>
				NumbersAbovePulses
				''' <summary>
				''' 马赛克
				''' </summary>
				Mosaic
				''' <summary>
				''' 海底波浪
				''' </summary>
				ScreenWaves
				''' <summary>
				''' 放克
				''' </summary>
				Funk
				''' <summary>
				''' 电影噪点
				''' </summary>
				Grain
				''' <summary>
				''' 暴风雪
				''' </summary>
				Blizzard
				''' <summary>
				''' 素描
				''' </summary>
				Drawing
				''' <summary>
				''' 色像差
				''' </summary>
				Aberration
				''' <summary>
				''' 模糊
				''' </summary>
				Blur
				''' <summary>
				''' 径向模糊
				''' </summary>
				RadialBlur
				''' <summary>
				''' 点阵
				''' </summary>
				Dots
				''' <summary>
				''' 禁用全部
				''' </summary>
				DisableAll
			End Enum
			<JsonProperty>
			Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
			''' <summary>
			''' 预设
			''' </summary>
			''' <returns></returns>
			Public Property Preset As Presets
			''' <summary>
			''' 启用
			''' </summary>
			''' <returns></returns>
			Public Property Enable As Boolean
			''' <summary>
			''' 强度
			''' </summary>
			''' <returns></returns>
			Public Property Threshold As Single
			Public Property Intensity As Single
			''' <summary>
			''' 颜色
			''' </summary>
			''' <returns></returns>
			Public Property Color As PanelColor
			''' <summary>
			''' X值
			''' </summary>
			''' <returns></returns>
			Public Property FloatX As Single
			''' <summary>
			''' Y值
			''' </summary>
			''' <returns></returns>
			Public Property FloatY As Single
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			Public Overrides ReadOnly Property Type As EventType = EventType.SetVFXPreset
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
			Public Overrides Function ToString() As String
				Return MyBase.ToString() + $" {Preset}"
			End Function
			Public Function ShouldSerializeEnable() As Boolean
				Return Preset <> Presets.DisableAll
			End Function
			Public Function ShouldSerializeThreshold() As Boolean
				Return Preset = Presets.Bloom
			End Function
			Public Function ShouldSerializeIntensity() As Boolean
				Return PropertyHasDuration() And
					Preset <> Presets.TileN And
					Preset <> Presets.CustomScreenScroll
			End Function
			Public Function ShouldSerializeColor() As Boolean
				Return Preset = Presets.Bloom
			End Function
			Public Function ShouldSerializeFloatX() As Boolean
				Return Preset = Presets.TileN Or
					Preset = Presets.CustomScreenScroll
			End Function
			Public Function ShouldSerializeFloatY() As Boolean
				Return Preset = Presets.TileN Or
					Preset = Presets.CustomScreenScroll
			End Function
			Public Function ShouldSerializeEase() As Boolean
				Return PropertyHasDuration()
			End Function
			Public Function ShouldSerializeDuration() As Boolean
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
			Enum BackgroundTypes
				Color
				Image
			End Enum
			Enum FilterModes
				NearestNeighbor
			End Enum
			<JsonProperty>
			Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Property ContentMode As ContentModes
			Public Property Filter As FilterModes '?
			Public Property Color As PanelColor
			Public Property Interval As Single
			Public Property BackgroundType As BackgroundTypes
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			Public Property Fps As Integer
			Public Property Image As List(Of String)
			Public Property ScrollX As Integer
			Public Property ScrollY As Integer
			Public Property TilingType As TilingTypes
			Public Overrides ReadOnly Property Type As EventType = Events.EventType.SetBackgroundColor
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

		End Class
		Public Class SetForeground
			Inherits BaseEvent
			<JsonProperty>
			Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
			Public Property ContentMode As ContentModes
			Public Property TilingType As TilingTypes
			Public Property Color As PanelColor
			Public Property Image As List(Of String)
			Public Property Fps As Integer
			Public Property ScrollX As Integer
			Public Property ScrollY As Integer
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			Public Property Interval As Single
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Overrides ReadOnly Property Type As EventType = EventType.SetForeground
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

		End Class
		Public Class SetSpeed
			Inherits BaseEvent
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Property Speed As Single
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			Public Overrides ReadOnly Property Type As EventType = EventType.SetSpeed
			<JsonIgnore>
			Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

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
			<JsonProperty>
			Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Property StartColor As PanelColor
			Public Property Background As Boolean
			Public Property EndColor As PanelColor
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			Public Property StartOpacity As Integer
			Public Property EndOpacity As Integer
			Public Overrides ReadOnly Property Type As EventType = EventType.CustomFlash
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

		End Class
		Public Class MoveCamera
			Inherits BaseEvent
			<JsonProperty>
			Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property CameraPosition As NumberOrExpressionPair
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property Zoom As Integer?
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property Angle As INumberOrExpression
			Public Property Duration As Single
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType

			Public Overrides ReadOnly Property Type As EventType = EventType.MoveCamera
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

		End Class
		Public Class HideRow
			Inherits BaseRowAnimations
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
			Inherits BaseRowAnimations
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
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Overrides ReadOnly Property Type As EventType = EventType.MoveRow
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

		End Class
		Public Enum SubdivisionRowMode
			Mini
			Normal
		End Enum
		Public Class ShowSubdivisionsRows
			Inherits BaseEvent
			Public Overrides ReadOnly Property type As EventType = EventType.ShowSubdivisionsRows
			Public Property Subdivisions As Integer
			Public Property Mode As SubdivisionRowMode
			Public Property ArcAngle As Integer
			Public Property SpinsPerSecond As Single
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
			Public Overrides ReadOnly Property Rooms As Rooms
		End Class

		Public Class PlayExpression
			Inherits BaseRowAnimations
			Public Property Expression As String
			Public Property Replace As Boolean
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Overrides ReadOnly Property Type As EventType = EventType.PlayExpression
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

		End Class
		Public Class TintRows
			Inherits BaseRowAnimations
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property TintColor As PanelColor
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Property Border As Borders
			Public Property BorderColor As PanelColor
			Public Property Opacity As Integer
			Public Property Tint As Boolean
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			Public Property Effect As RowEffect
			Public Overrides ReadOnly Property Type As EventType = EventType.TintRows
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
			Public Function ShouldSerializeDuration() As Boolean
				Return Duration <> 0
			End Function
			Public Function ShouldSerializeEase() As Boolean
				Return Duration <> 0
			End Function
			Public Overrides Function ToString() As String
				Return MyBase.ToString() + $"row:{Row}"
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
			Public Property Strength As Integer
			Public Property Count As Integer
			Public Property Frequency As Integer
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
			Public Property Color As PanelColor
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
			''' <summary>
			''' 持续时间
			''' </summary>
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
			Private Shared _PrivateId As UInteger = 0
			Private ReadOnly GeneratedId As UInteger
			Private ReadOnly _children As New List(Of AdvanceText)
			Private _anchor As AnchorType
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
			Public Property Color As PanelColor = New SKColor(&HFF, &HFF, &HFF, &HFF)
			Public Property Angle As INumberOrExpression
			Public Property Size As UInteger
			Public Property OutlineColor As PanelColor = New SKColor(0, 0, 0, &HFF)
			Public Property Id As Integer
				Get
					Return GeneratedId
				End Get
				Set(value As Integer)
				End Set
			End Property
			Public Property TextPosition As NumberOrExpressionPair = (50, 50)
			Public Property Anchor As String
				Get
					If (_anchor And &B1100 = 0) Or (_anchor And &B11 = 0) Then
						Throw New RhythmDoctorExcception("Anchor cannot be null.")
					End If
					Return (_anchor And AnchorType.Middle).ToString + (_anchor And AnchorType.Center).ToString
				End Get
				Set(value As String)
					Dim Split = Regex.Matches(value, "[A-Z][a-z]+")
					_anchor = [Enum].Parse(GetType(AnchorType), Split(0).Value) Or [Enum].Parse(GetType(AnchorType), Split(1).Value)
				End Set
			End Property
			<JsonIgnore>
			Public Property AnchorType As AnchorType
				Get
					Return _anchor
				End Get
				Set(value As AnchorType)
					_anchor = value
				End Set
			End Property
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
			Inherits BaseDecorationActions
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
			Public Property Color As PanelColor
			Public Overrides ReadOnly Property Type As EventType = EventType.Comment
			Public Function ShouldSerializeTarget() As Boolean
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
			Enum Borders
				None
				Outline
				Glow
			End Enum
			Public Property TintColor As PanelColor
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Property Border As Borders
			Public Property BorderColor As PanelColor
			Public Property Opacity As Integer
			Public Property Tint As Boolean
			Public Property Duration As Integer
			<JsonProperty>
			Public Overrides ReadOnly Property Rooms As New Rooms(True, True)
			Public Property Hands As PlayerHands
			Public Overrides ReadOnly Property Type As EventType = EventType.PaintHands
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

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

		End Class
		Public Class NewWindowDance
			Inherits BaseEvent
			Public Property Preset As String
			Public Property SamePresetBehavior As String
			Public Property Position As NumberOrExpressionPair
			Public Property Reference As String
			Public Property UseCircle As Boolean
			Public Property Speed As Integer
			Public Property Amplitude As New Integer
			Public Property AmplitudeVector As NumberOrExpressionPair
			Public Property Angle As INumberOrExpression
			Public Property Frequency As Integer
			Public Property Period As Integer
			Public Property EaseType As String
			Public Property SubEase As String
			Public Property EasingDuration As Single
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Overrides ReadOnly Property Type As EventType = EventType.NewWindowDance
			<JsonIgnore>
			Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions

		End Class
		Public Class WindowResize
			Inherits BaseEvent
			<JsonIgnore>
			Public Overrides ReadOnly Property Rooms As Rooms = Rooms.Default
			Public Overrides ReadOnly Property Type As EventType = EventType.WindowResize
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Actions
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property Scale As NumberOrExpressionPair
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property Pivot As NumberOrExpressionPair
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
		End Class
		Public Class PlayAnimation
			Inherits BaseDecorationActions
			Public Overrides ReadOnly Property Type As EventType = Events.EventType.PlayAnimation
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sprites
			Public Property Expression As String
			Public Overrides Function ToString() As String
				Return MyBase.ToString() + $" Expression:{Expression}"
			End Function
		End Class
		Public Class Tint
			Inherits BaseDecorationActions
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Property Border As Borders
			Public Property BorderColor As PanelColor
			Public Property Opacity As Integer
			Public Property Tint As Boolean
			Public Property TintColor As PanelColor = New SKColor(&HFF, &HFF, &HFF, &HFF)
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			Public Overrides ReadOnly Property Type As EventType = EventType.Tint
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sprites
			Public Function ShouldSerializeDuration() As Boolean
				Return Duration <> 0
			End Function
			Public Function ShouldSerializeEase() As Boolean
				Return Duration <> 0
			End Function
			Public Function ShouldSerializeTintColor() As Boolean
				Return True
			End Function
			Public Sub New()
				TintColor = New SKColor(&HFF, &HFF, &HFF, &HFF)
			End Sub
		End Class
		Public Class Move
			Inherits BaseDecorationActions
			Public Overrides ReadOnly Property Type As EventType = EventType.Move
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Sprites
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property Position As NumberOrExpressionPair
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property Scale As NumberOrExpressionPair
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property Angle As INumberOrExpression
			<JsonProperty(DefaultValueHandling:=DefaultValueHandling.Ignore)>
			Public Property Pivot As NumberOrExpressionPair
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
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
			Inherits BaseDecorationActions
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
			Inherits BaseBeats
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
				Dim Synco = X.SyncoSwing
				If X.SyncoBeat >= 0 Then
					Synco = 0.5
				End If
				Return New List(Of Pulse) From {New Pulse(BeatOnly + _Tick * 6 - _Tick * Synco, Hold)}.AsEnumerable
			End Function
			Public Function Split() As IEnumerable(Of BaseBeats)
				Return Split(RowXs)
			End Function
			Public Function Split(Xs As SetRowXs) As IEnumerable(Of BaseBeats)
				Dim L As New List(Of BaseBeats)
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
			Inherits BaseBeats
			Enum Patterns
				X
				Up
				Down
				Banana
				r
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
							Case Patterns.r
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
								L.Add(Patterns.r)
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
			Inherits BaseBeats
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
						L.Add(New Objects.Pulse(BeatOnly + i * _Interval + _Tick + _delay + j * (_Tick / _Subdivisions), 0))
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
			Public Overrides Function ToString() As String
				Return MyBase.ToString() + $" {_freezeBurnMode} {_PulseType}"
			End Function

		End Class
		Public Class SetOneshotWave
			Inherits BaseBeats
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
			Inherits BaseBeats
			Public Property Hold As Single
			Public Property Pulse As Byte
			Public Overrides ReadOnly Property Type As EventType = EventType.AddFreeTimeBeat
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rows
			Public Overrides Function PulseTime() As IEnumerable(Of Pulse)
				If Pulse = 6 Then
					Return New List(Of Pulse) From {New Pulse(BeatOnly, Hold)}.AsEnumerable
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
			Inherits BaseBeats
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
					Return New List(Of Pulse) From {New Pulse(BeatOnly, Hold)}
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
			<JsonProperty>
			Public Overrides ReadOnly Property Rooms As New Rooms(False, True)
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Property Heights As New List(Of Integer)(4)
			Public Property TransitionTime As Integer

			Public Overrides ReadOnly Property Type As EventType = EventType.ShowRooms
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms

		End Class
		Public Class MoveRoom
			Inherits BaseEvent
			'<JsonIgnore>
			'Public Property Room As Rooms
			'	Get
			'		Return New Rooms(Y) (False,False)
			'	End Get
			'	Set(value As Rooms)

			'	End Set
			'End Property
			Public Property RoomPosition As NumberOrExpressionPair
			Public Property Scale As NumberOrExpressionPair
			Public Property Angle As INumberOrExpression
			Public Property Pivot As NumberOrExpressionPair
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Overrides ReadOnly Property Type As EventType = EventType.MoveRoom
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
			<JsonIgnore>
			Public Overrides ReadOnly Property Rooms As Rooms
				Get
					Return New Rooms(Y)
				End Get
			End Property

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
			End Enum
			Enum AlphaModes
				Normal
			End Enum
			Enum ContentModes
				ScaleToFill
			End Enum
			Public Property MaskType As MaskTypes
			Public Property AlphaMode As AlphaModes
			Public Property SourceRoom As Integer
			Public Property Image As List(Of String)
			Public Property Fps As Integer
			Public Property KeyColor As PanelColor
			Public Property ColorCutoff As Integer
			Public Property ColorFeathering As Integer
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
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Property Opacity As UInteger
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			Public Overrides ReadOnly Property Type As EventType = EventType.FadeRoom
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
			<JsonIgnore>
			Public Overrides ReadOnly Property Rooms As Rooms
				Get
					Return New Rooms(Y)
				End Get
			End Property

		End Class
		Public Class SetRoomPerspective
			Inherits BaseEvent
			Public Property CornerPositions As New List(Of NumberOrExpressionPair)(4)
			''' <summary>
			''' 持续时间
			''' </summary>
			Public Property Duration As Single
			''' <summary>
			''' 缓动类型
			''' </summary>
			Public Property Ease As EaseType
			Public Overrides ReadOnly Property Type As EventType = EventType.SetRoomPerspective
			Public Overrides ReadOnly Property Tab As Tabs = Tabs.Rooms
			<JsonIgnore>
			Public Overrides ReadOnly Property Rooms As Rooms
				Get
					Return New Rooms(Y)
				End Get
			End Property

		End Class
	End Module
End Namespace