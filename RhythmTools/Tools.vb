Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RhythmBase.Assets
Imports RhythmBase.Components
Imports RhythmBase.Events
Imports RhythmBase.Extensions
Imports RhythmBase.LevelElements
Imports RhythmBase.Utils
Namespace Tools
	Public Class RDLevelHandler
		Private ReadOnly Level As RDLevel
		Private ReadOnly Calculator As BeatCalculator
		Public Sub New(level As RDLevel)
			Me.Level = level
			Calculator = New BeatCalculator(level)
		End Sub
		''' <summary>
		''' 拆分护士语音提示
		''' </summary>
		Public Sub SplitRDGSG()
			Dim Adds As New List(Of SayReadyGetSetGo)
			For Each item In Level.Where(Of SayReadyGetSetGo)()
				If item.Splitable Then
					Adds.AddRange(item.Split)
					item.Active = False
				End If
			Next
			Level.AddRange(Adds)
		End Sub
		''' <summary>
		''' 拆分七拍子
		''' </summary>
		Public Sub SplitClassicBeat()
			Dim Adds As New List(Of BaseBeat)
			For Each item In Level.Where(Of AddClassicBeat)()
				Adds.AddRange(item.Split)
				item.Active = False
			Next
			Level.AddRange(Adds)
		End Sub
		''' <summary>
		''' 拆分二拍子
		''' </summary>
		Public Sub SplitOneShotBeat()
			Dim Adds As New List(Of BaseBeat)
			For Each item In Level.Where(Of AddOneshotBeat)()
				Adds.AddRange(item.Split)
				item.Active = False
			Next
			Level.AddRange(Adds)
		End Sub
		''' <summary>
		''' 移除未激活事件
		''' </summary>
		Public Sub RemoveUnactive()
			Level.RemoveAll(Function(i) Not i.Active)
		End Sub
		''' <summary>
		''' 释放标签事件
		''' </summary>
		Public Sub DisposeTags()
			Dim Adds As New List(Of BaseEvent)
			For Each item In Level.Where(Of TagAction)
				Dim eventGroups = Level.GetTaggedEvents(item.ActionTag, Not item.Action = TagAction.Actions.Run)
				For Each group In eventGroups
					Dim startBeat = group.First().BeatOnly
					Dim copiedGroup = group.Select(Function(i) Clone(i))
					For Each copy As BaseEvent In copiedGroup
						copy.BeatOnly += (item.BeatOnly - startBeat)
						copy.Tag = ""
						Adds.Add(copy)
					Next
					item.Active = False
				Next
			Next
			Level.AddRange(Adds)
		End Sub
		Public Sub CreateTags(ParamArray names() As String)

		End Sub
		''' <summary>
		''' 批量添加标签
		''' </summary>
		''' <param name="name">标签名</param>
		''' <param name="predicate">添加的事件须满足的条件</param>
		''' <param name="replace">指示标签名是否替换原有标签名</param>
		Public Sub CombineToTag(name As String, predicate As Func(Of BaseEvent, Boolean), replace As Boolean)
			If replace Then
				For Each item In Level.Where(Function(i) predicate(i))
					item.Tag = name
				Next
			Else
				For Each item In Level.Where(Function(i) predicate(i))
					item.Tag += name
				Next
			End If
		End Sub
		''' <summary>
		''' [未完成] 缩放时间轴
		''' </summary>
		''' <param name="magnification"></param>
		Public Sub ZoomTime(magnification As Single)

		End Sub
		''' <summary>
		''' [未完成] 缩放精灵图
		''' </summary>
		''' <param name="magnification"></param>
		Public Sub ZoomSprite(magnification As Single)
			'For Each deco In Level.Decorations
			'	If deco.GetType = GetType(Sprite) Then
			'		For Each image In CType(deco.File, Sprite).Images
			'		Next
			'	End If
			'Next
		End Sub
		''' <summary>
		''' 在七拍子的每一拍按键！
		''' </summary>
		Public Sub PressOnEveryBeat()
			Dim Add1 As New List(Of BaseBeat)
			Dim Add2 As New List(Of AddFreeTimeBeat)
			For Each item In Level.Where(Of AddClassicBeat)()
				Add1.AddRange(item.Split())
				item.Active = False
			Next
			For Each item In Add1
				Dim n = item.Copy(Of AddFreeTimeBeat)
				n.Pulse = 6
				Add2.Add(n)
			Next
			Level.AddRange(Add2)
		End Sub
		''' <summary>
		''' [未完成] 拆分轨道为精灵图
		''' </summary>
		Public Sub SplitRow(Character As Sprite, ClassicBeat As Sprite, Heart As Sprite, beatSettings As SplitRowSettings)
			For Each row In Level.Rows.Where(Function(i) i.RowType = RowType.Classic)
				Dim commentColor = Drawing.Color.FromArgb(Random.Shared.Next)
				Dim Decos As New List(Of (deco As Decoration, left As Double)) From {
					(Level.CreateDecoration(row.Rooms, Character,,), 0),
					(Level.CreateDecoration(row.Rooms, ClassicBeat,,), 29),
					(Level.CreateDecoration(row.Rooms, ClassicBeat,,), 53),
					(Level.CreateDecoration(row.Rooms, ClassicBeat,,), 77),
					(Level.CreateDecoration(row.Rooms, ClassicBeat,,), 101),
					(Level.CreateDecoration(row.Rooms, ClassicBeat,,), 125),
					(Level.CreateDecoration(row.Rooms, ClassicBeat,,), 149),
					(Level.CreateDecoration(row.Rooms, ClassicBeat,,), 214),
					(Level.CreateDecoration(row.Rooms, Heart,,), 282)
				}
				For Each item In Decos
					'item.deco.Rooms = row.Rooms
					item.deco.Visible = False
					Dim visible As SetVisible = item.deco.CreateChildren(Of SetVisible)(1)
					visible.Visible = Not row.HideAtStart
					Level.Add(visible)
				Next
				For Each part In Decos
					Dim tempEvent = New MoveRow With {.RowPosition = New NumOrExpPair(35 / 352 * 100, 50), .Pivot = 0}
					Dim CharEvent As Move = part.deco.CreateChildren(Of Move)(1)
					CharEvent.Position = New NumOrExpPair(35 / 352 * 100, 50)
					CharEvent.Scale = Nothing
					CharEvent.Angle = Nothing
					CharEvent.Pivot = New NumOrExpPair(((tempEvent.Pivot * 282) - (part.left - part.deco.Size.X / 2)) * 100 / part.deco.Size.X, 50)
					CharEvent.Ease = EaseType.Linear
					CharEvent.Duration = 0
					Level.Add(CharEvent)
				Next
				Dim tempRowXs As New SetRowXs
				For i = 0 To 5
					Dim tempExpression = Decos(i + 1).deco.CreateChildren(Of PlayAnimation)(1)
					tempExpression.Expression = beatSettings.Line
					Level.Add(tempExpression)
				Next
				For Each item In row.Children

					Select Case item.Type
						Case EventType.HideRow
							For Each part In Decos
								Dim tempEvent = CType(item, HideRow)
								Dim CharEvent As SetVisible = part.deco.CreateChildren(Of SetVisible)(item.BeatOnly)
								CharEvent.Visible = (tempEvent.Show = HideRow.Shows.Visible) Or (tempEvent.Show = HideRow.Shows.OnlyCharacter)
								Level.Add(CharEvent)
							Next
						Case EventType.MoveRow
							For Each part In Decos
								Dim tempEvent = CType(item, MoveRow)
								Dim CharEvent As Move = part.deco.CreateChildren(Of Move)(item.BeatOnly)
								If tempEvent.CustomPosition Then
									Select Case tempEvent.Target
										Case MoveRow.Targets.WholeRow
											CharEvent.Position = tempEvent.RowPosition
											CharEvent.Scale = tempEvent.Scale
											CharEvent.Angle = tempEvent.Angle
											If tempEvent.Pivot IsNot Nothing Then
												CharEvent.Pivot = New NumOrExpPair(((tempEvent.Pivot * 282) - (part.left - part.deco.Size.X / 2)) * 100 / part.deco.Size.X, 50)
											End If
											CharEvent.Ease = tempEvent.Ease
											CharEvent.Duration = tempEvent.Duration
										Case MoveRow.Targets.Character
											CharEvent.Position = tempEvent.RowPosition
											CharEvent.Scale = tempEvent.Scale
											CharEvent.Angle = tempEvent.Angle
											CharEvent.Pivot = New NumOrExpPair((-(part.left - part.deco.Size.X / 2)) * 100 / part.deco.Size.X, 50)
											CharEvent.Ease = tempEvent.Ease
											CharEvent.Duration = tempEvent.Duration
											Level.Add(CharEvent)
									End Select
								End If
							Next
						Case EventType.TintRows
							For Each part In Decos
								Dim tempEvent = CType(item, TintRows)
								Dim CharEvent As Tint = part.deco.CreateChildren(Of Tint)(item.BeatOnly)
								CharEvent.Border = tempEvent.Border
								CharEvent.BorderColor.Color = tempEvent.BorderColor.Color
								CharEvent.Tint = tempEvent.Tint
								CharEvent.Opacity = tempEvent.Opacity
								CharEvent.Ease = tempEvent.Ease
								CharEvent.Duration = tempEvent.Duration
								Level.Add(CharEvent)
							Next
						Case EventType.PlayAnimation
							Dim part = Decos(0)
							Dim tempEvent = CType(item, PlayExpression)
							Dim charEvent As PlayAnimation = part.deco.CreateChildren(Of PlayAnimation)(item.BeatOnly)
							charEvent.Expression = tempEvent.Expression
							Level.Add(charEvent)
						Case EventType.SetRowXs
							Dim tempEvent = CType(item, SetRowXs)
							For i = 0 To 5
								If tempEvent.Pattern(i) <> tempRowXs.Pattern(i) Then
									Dim tempAnimation = Decos(i + 1).deco.CreateChildren(Of PlayAnimation)(tempEvent.BeatOnly)
									'	tempAnimation.Expression =
									Level.Add(tempAnimation)
								End If
							Next
					End Select
				Next
			Next
		End Sub
		Private Function GetExpression(names As SplitRowSettings, before As SetRowXs, after As SetRowXs, index As Byte) As String
			'If before Then
			Throw New NotImplementedException
		End Function
		''' <summary>
		''' 添加浮动文字式计时器
		''' </summary>
		''' <param name="copy">浮动文字的模板，用于提供浮动文字的所有参数</param>
		''' <param name="interval">细分间隔，每秒事件数</param>
		''' <param name="increase">递增(true)或递减(false)</param>
		Public Sub AddTimer(copy As FloatingText, interval As UInteger, increase As Boolean)
			Dim finish = Level.FirstOrDefault(Function(i) i.Type = EventType.FinishLevel, Level.Last).BeatOnly
			Dim t As Integer = 0
			Dim C As New BeatCalculator(Level)

			Dim txt = Clone(copy)
			txt.BeatOnly = 1
			txt.Text = If(increase, TimeSpan.Zero, C.BeatOnly_Time(finish) - TimeSpan.Zero).ToString
			Level.Add(txt)
			Do
				Level.Add(txt.CreateAdvanceText(C.Time_BeatOnly(TimeSpan.FromSeconds(t / interval))))
				txt.Text += vbCrLf + $"{If(increase, TimeSpan.FromSeconds(t / interval), C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval))}"
				t += 1
			Loop Until C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval) < TimeSpan.Zero
		End Sub
		''' <summary>
		''' 添加浮动文字式计拍器
		''' </summary>
		''' <param name="copy">浮动文字的模板，用于提供浮动文字的所有参数</param>
		''' <param name="interval">细分间隔，每秒事件数</param>
		''' <param name="increase">递增(true)或递减(false)</param>
		Public Sub AddBeater(copy As FloatingText, interval As UInteger, increase As Boolean)
			Dim finish = Level.FirstOrDefault(Function(i) i.Type = EventType.FinishLevel).BeatOnly
			Dim t As Integer = 0
			Dim C As New BeatCalculator(Level)

			Dim txt = Clone(copy)
			txt.BeatOnly = 1
			txt.Text = If(increase, (1, 1), C.BeatOnly_BarBeat(finish - 1)).ToString
			Level.Add(txt)
			Do
				Level.Add(txt.CreateAdvanceText(C.Time_BeatOnly(TimeSpan.FromSeconds(t / interval))))
				txt.Text += vbCrLf + $"{If(increase, C.Time_BarBeat(TimeSpan.FromSeconds(t / interval)), C.Time_BarBeat(C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval)))}"
				t += 1
			Loop Until C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval) < TimeSpan.Zero
		End Sub
		''' <summary>
		''' 批量添加精灵
		''' </summary>
		''' <param name="room">房间</param>
		''' <param name="sprite">精灵对象</param>
		''' <param name="count">个数</param>
		''' <param name="depth">精灵深度</param>
		''' <param name="visible">精灵的初始可见性</param>
		Public Sub AddLotsOfDecos(room As Rooms, sprite As Sprite, count As UInteger, Optional depth As Integer = 0, Optional visible As Boolean = True)
			For i As UInteger = 0 To count
				Level.CreateDecoration(room, sprite, depth, visible)
			Next
		End Sub
		''' <summary>
		''' 批量添加精灵
		''' </summary>
		''' <param name="decoration">精灵模板</param>
		''' <param name="count">个数</param>
		Public Sub AddLotsOfDecos(decoration As Decoration, count As UInteger)
			For i As UInteger = 0 To count
				Level.CopyDecoration(decoration)
			Next
		End Sub
		''' <summary>
		''' 全局更改拍号和移动事件
		''' </summary>
		Public Sub MoveBeats(cpb As UInteger, offset As Integer)
			For Each item In Level.Where(Of SetCrotchetsPerBar)
				item.CrotchetsPerBar = cpb
			Next
			For Each item In Level
				item.BeatOnly += offset
			Next
			If offset > 0 Then
				Level.Add(New SetCrotchetsPerBar(1, 0, cpb, 1))
			End If
		End Sub
		''' <summary>
		''' 检查最短按拍间隔(包括长按)
		''' </summary>
		''' <returns></returns>
		Public Function GetLevelMinIntervalTime() As IEnumerable(Of (Hit, Hit, TimeSpan))
			Dim Pulses As New List(Of Hit)
			Dim PulsesInterval As New List(Of (Hit, Hit, TimeSpan))
			For Each row In Level.Rows
				Pulses.AddRange(row.HitBeats)
			Next
			Pulses = Pulses.GroupBy(Function(i) i.BeatOnly).Select(Function(i) i.First).OrderBy(Function(i) i.BeatOnly).ToList
			For i = 0 To Pulses.Count - 2
				PulsesInterval.Add((Pulses(i), Pulses(i + 1), Calculator.BeatOnly_Time(Pulses(i + 1).BeatOnly + Pulses(i + 1).Hold) - Calculator.BeatOnly_Time(Pulses(i).BeatOnly)))
			Next
			Dim min = PulsesInterval.Min(Function(i) i.Item3)
			Return PulsesInterval.Where(Function(i) i.Item3 = min)
		End Function
		''' <summary>
		''' 排序指定区域事件
		''' </summary>
		''' <param name="eventsTobeSort">需要排序的事件满足的条件</param>
		''' <param name="sortKey">排序事件所用的键</param>
		''' <returns></returns>
		Public Sub SortRange(eventsTobeSort As Func(Of BaseEvent, Boolean), sortKey As Func(Of BaseEvent, Single))
			Dim sortList = Level.Where(eventsTobeSort)
			Level.RemoveAll(sortList)
			Level.AddRange(sortList.OrderBy(sortKey))
		End Sub
		Public Class SplitRowSettings

			Public Line As String
			Public Synco As String

			Public Beat_Open As String
			Public Beat_Flash As String
			Public Beat_Close As String

			Public X_Open As String
			Public X_Flash As String
			Public X_Close As String

			Public X_Synco_Open As String
			Public X_Synco_Flash As String
			Public X_Synco_Close As String

			Public Up_Open As String
			Public Up_Close As String

			Public Down_Open As String
			Public Down_Close As String

			Public Swing_Left As String
			Public Swing_Right As String
			Public Swing_Bounce As String

			Public Held_Start As String
			Public Held_End As String

		End Class
	End Class
End Namespace
Namespace Extensions
	Public Module Extensions
		Private Function GetPulseCollection(e As Row) As IEnumerable(Of (Parent As BaseBeat, Byte))
			'Dim result = New List(Of (BaseBeat, Byte))
			'If e.RowType = RowType.Classic Then
			'	Dim Beats As IEnumerable(Of BaseBeat) = From item In e.Children
			'											Where item.Type = EventType.AddFreeTimeBeat Or item.Type = EventType.PulseFreeTimeBeat
			'											Select item
			'	For Each item In Beats
			'		If item.Type = EventType.AddFreeTimeBeat Then
			'			Dim temp = CType(item, AddFreeTimeBeat)
			'			result.Add((temp, temp.Pulse))
			'		Else
			'			Dim temp = CType(item, PulseFreeTimeBeat)
			'			Select Case temp.Action
			'				Case PulseFreeTimeBeat.ActionType.Increment

			'				Case PulseFreeTimeBeat.ActionType.Decrement

			'				Case PulseFreeTimeBeat.ActionType.Custom

			'				Case PulseFreeTimeBeat.ActionType.Remove

			'			End Select
			'		End If
			'	Next
			'End If
			'Return result
		End Function
	End Module
	Public Module EventsExtension
		Public Enum wavetype
			BoomAndRush
			Spring
			Spike
			SpikeHuge
			Ball
			[Single]
		End Enum
		Public Enum ShockWaveType
			size
			distortion
			duration
		End Enum
		Public Enum Particle
			HitExplosion
			leveleventexplosion
		End Enum
		Public Class ProceduralTree
			Public brachedPerlteration As Single?
			Public branchesPerDivision As Single?
			Public iterationsPerSecond As Single?
			Public thickness As Single?
			Public targetLength As Single?
			Public maxDeviation As Single?
			Public angle As Single?
			Public camAngle As Single?
			Public camDistance As Single?
			Public camDegreesPerSecond As Single?
			Public camSpeed As Single?
			Public pulseIntensity As Single?
			Public pulseRate As Single?
			Public pulseWavelength As Single?
		End Class
		<Extension> Public Sub SetScoreboardLights(e As CallCustomMethod, Mode As Boolean, Text As String)
			e.MethodName = $"{NameOf(SetScoreboardLights)}({Mode},{Text})"
		End Sub
		<Extension> Public Sub InvisibleChars(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(InvisibleChars).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub InvisibleHeart(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(InvisibleHeart).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub NoHitFlashBorder(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(NoHitFlashBorder).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub NoHitStrips(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(NoHitStrips).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub SetOneshotType(e As CallCustomMethod, rowID As Integer, wavetype As ShockWaveType)
			e.MethodName = $"{NameOf(SetOneshotType)}({rowID},str:{wavetype})"
		End Sub
		<Extension> Public Sub WobblyLines(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(WobblyLines).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub TrueCameraMove(e As Comment, RoomID As Integer, p As RDPoint, AnimationDuration As Single, Ease As EaseType)
			e.Text = $"()=>{NameOf(TrueCameraMove).LowerCamelCase}({RoomID},{p.X},{p.Y},{AnimationDuration},{Ease})"
		End Sub
		<Extension> Public Sub Create(e As Comment, particleName As Particle, p As RDPoint)
			e.Text = $"()=>{NameOf(Create).LowerCamelCase}(CustomParticles/{particleName},{p.X},{p.Y})"
		End Sub
		<Extension> Public Sub ShockwaveSizeMultiplier(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(ShockwaveSizeMultiplier).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub ShockwaveDistortionMultiplier(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(ShockwaveDistortionMultiplier).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub ShockwaveDurationMultiplier(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(ShockwaveDurationMultiplier).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub Shockwave(e As Comment, type As ShockWaveType, value As Single)
			e.Text = $"()=>{NameOf(Shockwave).LowerCamelCase}({type},{value})"
		End Sub
		<Extension> Public Sub MistakeOrHeal(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = $"{NameOf(MistakeOrHeal)}({damageOrHeal})"
		End Sub
		<Extension> Public Sub MistakeOrHealP1(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = $"{NameOf(MistakeOrHealP1)}({damageOrHeal})"
		End Sub
		<Extension> Public Sub MistakeOrHealP2(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = $"{NameOf(MistakeOrHealP2)}({damageOrHeal})"
		End Sub
		<Extension> Public Sub MistakeOrHealSilent(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = $"{NameOf(MistakeOrHealSilent)}({damageOrHeal})"
		End Sub
		<Extension> Public Sub MistakeOrHealP1Silent(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = $"{NameOf(MistakeOrHealP1Silent)}({damageOrHeal})"
		End Sub
		<Extension> Public Sub MistakeOrHealP2Silent(e As CallCustomMethod, damageOrHeal As Single)
			e.MethodName = $"{NameOf(MistakeOrHealP2Silent)}({damageOrHeal})"
		End Sub
		<Extension> Public Sub SetMistakeWeight(e As CallCustomMethod, rowID As Integer, weight As Single)
			e.MethodName = $"{NameOf(SetMistakeWeight)}({rowID},{weight})"
		End Sub
		<Extension> Public Sub DamageHeart(e As CallCustomMethod, rowID As Integer, damage As Single)
			e.MethodName = $"{NameOf(DamageHeart)}({rowID},{damage})"
		End Sub
		<Extension> Public Sub HealHeart(e As CallCustomMethod, rowID As Integer, damage As Single)
			e.MethodName = $"{NameOf(HealHeart)}({rowID},{damage})"
		End Sub
		<Extension> Public Sub WavyRowsAmplitude(e As CallCustomMethod, roomID As Byte, amplitude As Single)
			e.MethodName = $"room[{roomID}].{NameOf(WavyRowsAmplitude).LowerCamelCase} = {amplitude}"
		End Sub
		<Extension> Public Sub WavyRowsAmplitude(e As Comment, roomID As Byte, amplitude As Single, duration As Single)
			e.Text = $"()=>{NameOf(WavyRowsAmplitude).LowerCamelCase}({roomID},{amplitude},{duration})"
		End Sub
		<Extension> Public Sub WavyRowsFrequency(e As CallCustomMethod, roomID As Byte, frequency As Single)
			e.MethodName = $"room[{roomID}].{NameOf(WavyRowsFrequency).LowerCamelCase} = {frequency}"
		End Sub
		<Extension> Public Sub SetShakeIntensityOnHit(e As CallCustomMethod, roomID As Byte, number As Integer, strength As Integer)
			e.MethodName = $"room[{roomID}].{NameOf(SetShakeIntensityOnHit)}({number},{strength})"
		End Sub
		<Extension> Public Sub ShowPlayerHand(e As CallCustomMethod, roomID As Byte, isPlayer1 As Boolean, isShortArm As Boolean, isInstant As Boolean)
			e.MethodName = $"{NameOf(ShowPlayerHand)}({roomID},{isPlayer1},{isShortArm},{isInstant})"
		End Sub
		<Extension> Public Sub TintHandsWithInts(e As CallCustomMethod, roomID As Byte, R As Single, G As Single, B As Single, A As Single)
			e.MethodName = $"{NameOf(TintHandsWithInts)}({roomID},{R},{G},{B},{A})"
		End Sub
		<Extension> Public Sub SetHandsBorderColor(e As CallCustomMethod, roomID As Byte, R As Single, G As Single, B As Single, A As Single, style As Integer)
			e.MethodName = $"{NameOf(SetHandsBorderColor)}({roomID},{R},{G},{B},{A},{style})"
		End Sub
		<Extension> Public Sub SetAllHandsBorderColor(e As CallCustomMethod, R As Single, G As Single, B As Single, A As Single, style As Integer)
			e.MethodName = $"{NameOf(SetAllHandsBorderColor)}({R},{G},{B},{A},{style})"
		End Sub
		<Extension> Public Sub SetHandToP1(e As CallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = $"{NameOf(SetHandToP1)}({room},{rightHand})"
		End Sub
		<Extension> Public Sub SetHandToP2(e As CallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = $"{NameOf(SetHandToP2)}({room},{rightHand})"
		End Sub
		<Extension> Public Sub SetHandToIan(e As CallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = $"{NameOf(SetHandToIan)}({room},{rightHand})"
		End Sub
		<Extension> Public Sub SetHandToPaige(e As CallCustomMethod, room As Integer, rightHand As Boolean)
			e.MethodName = $"{NameOf(SetHandToPaige)}({room},{rightHand})"
		End Sub
		<Extension> Public Sub SetShadowRow(e As CallCustomMethod, mimickerRowID As Integer, mimickedRowID As Integer)
			e.MethodName = $"{NameOf(SetShadowRow)}({mimickerRowID},{mimickedRowID})"
		End Sub
		<Extension> Public Sub UnsetShadowRow(e As CallCustomMethod, mimickerRowID As Integer, mimickedRowID As Integer)
			e.MethodName = $"{NameOf(UnsetShadowRow)}({mimickerRowID},{mimickedRowID})"
		End Sub
		<Extension> Public Sub ShakeCam(e As CallCustomMethod, number As Integer, strength As Integer, roomID As Integer)
			e.MethodName = $"vfx.{NameOf(ShakeCam)}({number},{strength},{roomID})"
		End Sub
		<Extension> Public Sub StopShakeCam(e As CallCustomMethod, roomID As Integer)
			e.MethodName = $"vfx.{NameOf(StopShakeCam)}({roomID})"
		End Sub
		<Extension> Public Sub ShakeCamSmooth(e As CallCustomMethod, duration As Integer, strength As Integer, roomID As Integer)
			e.MethodName = $"vfx.{NameOf(ShakeCamSmooth)}({duration},{strength},{roomID})"
		End Sub
		<Extension> Public Sub ShakeCamRotate(e As CallCustomMethod, duration As Integer, strength As Integer, roomID As Integer)
			e.MethodName = $"vfx.{NameOf(ShakeCamRotate)}({duration},{strength},{roomID})"
		End Sub
		<Extension> Public Sub SetKaleidoscopeColor(e As CallCustomMethod, roomID As Integer, R1 As Single, G1 As Single, B1 As Single, R2 As Single, G2 As Single, B2 As Single)
			e.MethodName = $"{NameOf(SetKaleidoscopeColor)}({roomID},{R1},{G1},{B1},{R2},{G2},{B2})"
		End Sub
		<Extension> Public Sub SyncKaleidoscopes(e As CallCustomMethod, targetRoomID As Integer, otherRoomID As Integer)
			e.MethodName = $"{NameOf(SyncKaleidoscopes)}({targetRoomID},{otherRoomID})"
		End Sub
		<Extension> Public Sub SetVignetteAlpha(e As CallCustomMethod, alpha As Single, roomID As Integer)
			e.MethodName = $"vfx.{NameOf(SetVignetteAlpha)}({alpha},{roomID})"
		End Sub
		<Extension> Public Sub NoOneshotShadows(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(NoOneshotShadows).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Function TextOnly(e As ShowDialogue) As String
			Dim result = e.Text
			For Each item In {
				"shake",
				"shakeRadius=\d+",
				"wave",
				"waveHeight=\d+",
				"waveSpeed=\d+",
				"swirl",
				"swirlRadius=\d+",
				"swirlSpeed=\d+",
				"static"
			}
				result = Regex.Replace(result, $"\[{item}\]", "")
			Next
			Return result
		End Function
		<Extension> Public Sub EnableRowReflections(e As CallCustomMethod, roomID As Integer)
			e.MethodName = $"{NameOf(EnableRowReflections)}({roomID})"
		End Sub
		<Extension> Public Sub DisableRowReflections(e As CallCustomMethod, roomID As Integer)
			e.MethodName = $"{NameOf(DisableRowReflections)}({roomID})"
		End Sub
		<Extension> Public Sub ChangeCharacter(e As CallCustomMethod, Name As String, roomID As Integer)
			e.MethodName = $"{NameOf(ChangeCharacter)}(str:{Name},{roomID})"
		End Sub
		<Extension> Public Sub ChangeCharacter(e As CallCustomMethod, Name As Characters, roomID As Integer)
			e.MethodName = $"{NameOf(ChangeCharacter)}(str:{Name},{roomID})"
		End Sub
		<Extension> Public Sub ChangeCharacterSmooth(e As CallCustomMethod, Name As String, roomID As Integer)
			e.MethodName = $"{NameOf(ChangeCharacterSmooth)}(str:{Name},{roomID})"
		End Sub
		<Extension> Public Sub ChangeCharacterSmooth(e As CallCustomMethod, Name As Characters, roomID As Integer)
			e.MethodName = $"{NameOf(ChangeCharacterSmooth)}(str:{Name},{roomID})"
		End Sub
		<Extension> Public Sub SmoothShake(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(SmoothShake).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub RotateShake(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(RotateShake).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub DisableRowChangeWarningFlashes(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(DisableRowChangeWarningFlashes).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub StatusSignWidth(e As CallCustomMethod, value As Single)
			e.MethodName = $"{NameOf(StatusSignWidth).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub SkippableRankScreen(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(SkippableRankScreen).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub MissesToCrackHeart(e As CallCustomMethod, value As Integer)
			e.MethodName = $"{NameOf(MissesToCrackHeart).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub SkipRankText(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(SkipRankText).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub AlternativeMatrix(e As CallCustomMethod, value As Boolean)
			e.MethodName = $"{NameOf(AlternativeMatrix).LowerCamelCase} = {value}"
		End Sub
		<Extension> Public Sub ToggleSingleRowReflections(e As CallCustomMethod, room As Byte, row As Byte, value As Boolean)
			e.MethodName = $"{NameOf(ToggleSingleRowReflections)}({room},{row},{value})"
		End Sub
		<Extension> Public Sub SetScrollSpeed(e As CallCustomMethod, roomID As Byte, speed As Single, duration As Single, ease As EaseType)
			e.MethodName = $"room[{roomID}].{NameOf(SetScrollSpeed)}({speed},{duration},str:{ease})"
		End Sub
		<Extension> Public Sub SetScrollOffset(e As CallCustomMethod, roomID As Byte, cameraOffset As Single, duration As Single, ease As EaseType)
			e.MethodName = $"room[{roomID}].{NameOf(SetScrollOffset)}({cameraOffset},{duration},str:{ease})"
		End Sub
		<Extension> Public Sub DarkenedRollerdisco(e As CallCustomMethod, roomID As Byte, value As Single)
			e.MethodName = $"room[{roomID}].{NameOf(DarkenedRollerdisco)}({value})"
		End Sub
		<Extension> Public Sub CurrentSongVol(e As CallCustomMethod, targetVolume As Single, fadeTimeSeconds As Single)
			e.MethodName = $"{NameOf(CurrentSongVol)}({targetVolume},{fadeTimeSeconds})"
		End Sub
		<Extension> Public Sub PreviousSongVol(e As CallCustomMethod, targetVolume As Single, fadeTimeSeconds As Single)
			e.MethodName = $"{NameOf(PreviousSongVol)}({targetVolume},{fadeTimeSeconds})"
		End Sub
		<Extension> Public Sub EditTree(e As CallCustomMethod, room As Byte, [property] As String, value As Single, beats As Single, ease As EaseType)
			e.MethodName = $"room[{room}].{NameOf(EditTree)}(""{[property]}"",{value},{beats},""{ease}"")"
		End Sub
		<Extension> Public Function EditTree(e As CallCustomMethod, room As Byte, treeProperties As ProceduralTree, beats As Single, ease As EaseType) As IEnumerable(Of CallCustomMethod)
			Dim L As New List(Of CallCustomMethod)
			For Each p In GetType(ProceduralTree).GetProperties
				If p.GetValue(treeProperties) IsNot Nothing Then
					Dim T As New CallCustomMethod
					T.EditTree(room, p.Name, p.GetValue(treeProperties), beats, ease)
					L.Add(T)
				End If
			Next
			Return L
		End Function
		<Extension> Public Sub EditTreeColor(e As CallCustomMethod, room As Byte, location As Boolean, color As String, beats As Single, ease As EaseType)
			e.MethodName = $"room[{room}].{NameOf(EditTreeColor)}({location},{color},{beats},{ease})"
		End Sub

		<Extension> Public Sub MoveToPosition(e As Move, point As RDPoint)

		End Sub
	End Module
	Public Class TranaslationManager
		Private ReadOnly jsonpath As IO.FileInfo
		Private ReadOnly values As JObject
		Public Sub New(filepath As IO.FileInfo)
			jsonpath = filepath
			If jsonpath.Exists Then
			Else
				jsonpath.Directory.Create()
				Using stream = New IO.StreamWriter(jsonpath.Create())
					stream.Write("{}")
				End Using
			End If

			Using Stream = New IO.StreamReader(jsonpath.OpenRead)
				values = JsonConvert.DeserializeObject(Stream.ReadToEnd)
			End Using
		End Sub
		Public Function GetValue(p As MemberInfo, value As String) As String
			Dim current As JObject = values
			Dim keys = GetPath(p)

			For i = 0 To keys.Length - 2
				Dim j As JToken = Nothing
				If Not current.TryGetValue(keys(i), j) Then
					current(keys(i)) = New JObject
					current = current(keys(i))
				Else
					current = j
				End If
			Next
			If Not current.ContainsKey(keys.Last) OrElse current(keys.Last) Is Nothing Then
				current(keys.Last) = value
				Save()
				Return value
			Else
				Return current(keys.Last).ToString
			End If
		End Function
		Public Function GetValue(p As MemberInfo)
			Return GetValue(p, GetPath(p).Last)
		End Function
		Private Shared Function GetPath(p As MemberInfo) As String()
			Return {p.DeclaringType.Namespace, p.DeclaringType.Name, p.Name}
		End Function
		Private Sub Save()
			Using Stream As New IO.StreamWriter(jsonpath.OpenWrite)
				Stream.Write(values.ToString)
			End Using
		End Sub
	End Class
End Namespace
Namespace Events
End Namespace