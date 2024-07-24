Imports Newtonsoft.Json
Imports SkiaSharp
Imports RhythmBase.Adofai.Components
Namespace Adofai
	Namespace Events
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
		Public Enum ADTileRelativeTo
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
End Namespace
