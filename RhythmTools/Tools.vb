Imports RhythmBase.Objects
Imports RhythmBase.Util
Imports RhythmAsset.Sprites
Imports RhythmAsset
Public Module Tools
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
					For Each copy In copiedGroup
						copy.BeatOnly += (item.BeatOnly - startBeat)
						copy.Tag = ""
						Adds.Add(copy)
					Next
					item.Active = False
				Next
			Next
			Level.AddRange(Adds)
		End Sub
		''' <summary>
		''' 批量添加标签
		''' </summary>
		''' <param name="name">标签名</param>
		''' <param name="func">添加的事件须满足的条件</param>
		''' <param name="replace">指示标签名是否替换原有标签名</param>
		Public Sub CombineToTag(name As String, func As Func(Of BaseEvent, Boolean), replace As Boolean)
			If replace Then
				For Each item In Level.Where(Function(i) func(i))
					item.Tag = name
				Next
			Else
				For Each item In Level.Where(Function(i) func(i))
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
			For Each deco In Level.Decorations
				If deco.GetType = GetType(Sprite) Then
					For Each image In CType(deco.Parent, Sprite).Images
					Next
				End If
			Next
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
		Public Sub SplitRow(Character As ISprite, ClassicBeat As ISprite, Heart As ISprite, beatSettings As SplitRowSettings)
			For Each row In Level.Rows.Where(Function(i) i.RowType = RowType.Classic)
				Dim commentColor = Drawing.Color.FromArgb(Random.Shared.Next)
				Dim Decos As New List(Of (deco As Decoration, left As Double)) From {
					(New Decoration(row.Rooms, Character,,), 0),
					(New Decoration(row.Rooms, ClassicBeat,,), 29),
					(New Decoration(row.Rooms, ClassicBeat,,), 53),
					(New Decoration(row.Rooms, ClassicBeat,,), 77),
					(New Decoration(row.Rooms, ClassicBeat,,), 101),
					(New Decoration(row.Rooms, ClassicBeat,,), 125),
					(New Decoration(row.Rooms, ClassicBeat,,), 149),
					(New Decoration(row.Rooms, ClassicBeat,,), 214),
					(New Decoration(row.Rooms, Heart,,), 282)
				}
				For Each item In Decos
					'item.deco.Rooms = row.Rooms
					item.deco.Visible = False
					Level.Decorations.Add(item.deco)
					Dim visible As SetVisible = item.deco.CreateChildren(Of SetVisible)(1)
					visible.Visible = Not row.HideAtStart
					Level.Add(visible)
				Next
				For Each part In Decos
					Dim tempEvent = New MoveRow With {.RowPosition = (35 / 352 * 100, 50), .Pivot = 0}
					Dim CharEvent As Move = part.deco.CreateChildren(Of Move)(1)
					CharEvent.Position = (35 / 352 * 100, 50)
					CharEvent.Scale = Nothing
					CharEvent.Angle = Nothing
					CharEvent.Pivot = (((tempEvent.Pivot * 282) - (part.left - part.deco.Size.X / 2)) * 100 / part.deco.Size.X, 50)
					CharEvent.Ease = RhythmBase.Ease.EaseType.Linear
					CharEvent.Duration = 0
					Level.Add(CharEvent)
				Next
				Dim tempRowXs As New SetRowXs With {.Pattern = "------"}
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
								Dim CharEvent As SetVisible = part.deco.CreateChildren(Of SetVisible)(item)
								CharEvent.Visible = (tempEvent.Show = HideRow.Shows.Visible) Or (tempEvent.Show = HideRow.Shows.OnlyCharacter)
								Level.Add(CharEvent)
							Next
						Case EventType.MoveRow
							For Each part In Decos
								Dim tempEvent = CType(item, MoveRow)
								Dim CharEvent As Move = part.deco.CreateChildren(Of Move)(item)
								If tempEvent.CustomPosition Then
									Select Case tempEvent.Target
										Case MoveRow.Targets.WholeRow
											CharEvent.Position = tempEvent.RowPosition
											CharEvent.Scale = tempEvent.Scale
											CharEvent.Angle = tempEvent.Angle
											If tempEvent.Pivot IsNot Nothing Then
												CharEvent.Pivot = (((tempEvent.Pivot * 282) - (part.left - part.deco.Size.X / 2)) * 100 / part.deco.Size.X, 50)
											End If
											CharEvent.Ease = tempEvent.Ease
											CharEvent.Duration = tempEvent.Duration
										Case MoveRow.Targets.Character
											CharEvent.Position = tempEvent.RowPosition
											CharEvent.Scale = tempEvent.Scale
											CharEvent.Angle = tempEvent.Angle
											CharEvent.Pivot = ((-(part.left - part.deco.Size.X / 2)) * 100 / part.deco.Size.X, 50)
											CharEvent.Ease = tempEvent.Ease
											CharEvent.Duration = tempEvent.Duration
											Level.Add(CharEvent)
									End Select
								End If
							Next
						Case EventType.TintRows
							For Each part In Decos
								Dim tempEvent = CType(item, TintRows)
								Dim CharEvent As Tint = part.deco.CreateChildren(Of Tint)(item)
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
							Dim charEvent As PlayAnimation = part.deco.CreateChildren(Of PlayAnimation)(item)
							charEvent.Expression = tempEvent.Expression
							Level.Add(charEvent)
						Case EventType.SetRowXs
							Dim tempEvent = CType(item, SetRowXs)
							For i = 0 To 5
								If tempEvent.PatternEnum(i) <> tempRowXs.PatternEnum(i) Then
									Dim tempAnimation = Decos(i + 1).deco.CreateChildren(Of PlayAnimation)(tempEvent)
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
				Level.Decorations.Add(New Decoration(room, sprite, depth, visible))
			Next
		End Sub
		''' <summary>
		''' 批量添加精灵
		''' </summary>
		''' <param name="decoration">精灵模板</param>
		''' <param name="count">个数</param>
		Public Sub AddLotsOfDecos(decoration As Decoration, count As UInteger)
			For i As UInteger = 0 To count
				Level.Decorations.Add(decoration.Copy)
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
		Public Function GetLevelMinIntervalTime() As IEnumerable(Of (Pulse, Pulse, TimeSpan))
			Dim Pulses As New List(Of Pulse)
			Dim PulsesInterval As New List(Of (Pulse, Pulse, TimeSpan))
			For Each row In Level.Rows
				Pulses.AddRange(row.PulseBeats)
			Next
			Pulses = Pulses.GroupBy(Function(i) i.BeatOnly).Select(Function(i) i.First).OrderBy(Function(i) i.BeatOnly).ToList
			For i = 0 To Pulses.Count - 2
				PulsesInterval.Add((Pulses(i), Pulses(i + 1), Calculator.BeatOnly_Time(Pulses(i + 1).BeatOnly + Pulses(i + 1).Hold) - Calculator.BeatOnly_Time(Pulses(i).BeatOnly)))
			Next
			Dim min = PulsesInterval.Min(Function(i) i.Item3)
			Return PulsesInterval.Where(Function(i) i.Item3 = min)
		End Function
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
End Module
