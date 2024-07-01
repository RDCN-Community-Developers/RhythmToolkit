Imports System.Text.RegularExpressions
Imports RhythmBase.Assets
Imports RhythmBase.Components
Imports RhythmBase.Events
Imports RhythmBase.Extensions
Imports RhythmBase.LevelElements
Imports RhythmBase.Utils
Namespace Tools
    Public Class RDLevelHandler
        Private ReadOnly Level As RDLevel
        Private ReadOnly Calculator As RDBeatCalculator
        Public Sub New(level As RDLevel)
            Me.Level = level
            Calculator = New RDBeatCalculator(level)
        End Sub
        ''' <summary>
        ''' 拆分护士语音提示
        ''' </summary>
        Public Sub SplitRDGSG()
            Dim Adds As New List(Of RDSayReadyGetSetGo)
            For Each item In Level.Where(Of RDSayReadyGetSetGo)()
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
            Dim Adds As New List(Of RDBaseBeat)
            For Each item In Level.Where(Of RDAddClassicBeat)()
                Adds.AddRange(item.Split)
                item.Active = False
            Next
            Level.AddRange(Adds)
        End Sub
        ''' <summary>
        ''' 拆分二拍子
        ''' </summary>
        Public Sub SplitOneShotBeat()
            Dim Adds As New List(Of RDBaseBeat)
            For Each item In Level.Where(Of RDAddOneshotBeat)()
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
            Dim Adds As New List(Of RDBaseEvent)
            For Each item In Level.Where(Of RDTagAction)
                Dim eventGroups = Level.GetTaggedEvents(item.ActionTag, Not item.Action = RDTagAction.Actions.Run)
                For Each group In eventGroups
                    Dim startBeat = group.First().Beat
                    Dim copiedGroup = group.Select(Function(i) Clone(i))
                    For Each copy As RDBaseEvent In copiedGroup
                        copy.Beat += (item.Beat.BeatOnly - startBeat.BeatOnly)
                        copy.Tag = ""
                        Adds.Add(copy)
                    Next
                    item.Active = False
                Next
            Next
            Level.AddRange(Adds)
        End Sub
#If DEBUG Then
        Public Sub CreateTags(ParamArray names() As String)

        End Sub
#End If
        ''' <summary>
        ''' 批量添加标签
        ''' </summary>
        ''' <param name="name">标签名</param>
        ''' <param name="predicate">添加的事件须满足的条件</param>
        ''' <param name="replace">指示标签名是否替换原有标签名</param>
        Public Sub CombineToTag(name As String, predicate As Func(Of RDBaseEvent, Boolean), replace As Boolean)
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
#If DEBUG Then
        ''' <summary>
        ''' [未完成] 缩放时间轴
        ''' </summary>
        ''' <param name="magnification"></param>
        Public Sub ZoomTime(magnification As Single)

        End Sub
#End If
#If DEBUG Then
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
#End If
        ''' <summary>
        ''' 在七拍子的每一拍按键！
        ''' </summary>
        Public Sub PressOnEveryBeat()
            Dim Add1 As New List(Of RDBaseBeat)
            Dim Add2 As New List(Of RDAddFreeTimeBeat)
            For Each item In Level.Where(Of RDAddClassicBeat)()
                Add1.AddRange(item.Split())
                item.Active = False
            Next
            For Each item In Add1
                Dim n = item.Clone(Of RDAddFreeTimeBeat)
                n.Pulse = 6
                Add2.Add(n)
            Next
            Level.AddRange(Add2)
        End Sub
        ''' <summary>
        ''' [未完成] 拆分轨道为精灵图
        ''' </summary>
        Public Sub SplitRow(Character As RDSprite, ClassicBeat As RDSprite, Heart As RDSprite, beatSettings As SplitRowSettings, rows As IEnumerable(Of RDRow), startBeat As RDBeat, endBeat As RDBeat, ShowRow As Boolean)

            '对于每个七拍轨道
            For Each row In rows.Where(Function(i) i.RowType = RDRowType.Classic)
                Dim commentColor = Drawing.Color.FromArgb(Random.Shared.Next)
                Dim Decos As New List(Of (deco As Decoration, left As Double)) From {
                    (Level.CreateDecoration(row.Rooms, Character), 0),
                    (Level.CreateDecoration(row.Rooms, ClassicBeat), 29),
                    (Level.CreateDecoration(row.Rooms, ClassicBeat), 53),
                    (Level.CreateDecoration(row.Rooms, ClassicBeat), 77),
                    (Level.CreateDecoration(row.Rooms, ClassicBeat), 101),
                    (Level.CreateDecoration(row.Rooms, ClassicBeat), 125),
                    (Level.CreateDecoration(row.Rooms, ClassicBeat), 149),
                    (Level.CreateDecoration(row.Rooms, ClassicBeat), 214),
                    (Level.CreateDecoration(row.Rooms, Heart), 282)
                }

                '精灵初始化
                For Each item In Decos
                    item.deco.Visible = False
                    Dim visible As New RDSetVisible With {.Beat = Level.DefaultBeat}
                    item.deco.Add(visible)
                    visible.Visible = Not row.HideAtStart
                Next

                '精灵轨道初始位置
                For Each part In Decos
                    Dim tempEvent = New RDMoveRow With {.RowPosition = New RDPoint(35 / 352 * 100, 50), .Pivot = 0}
                    Dim CharEvent As New RDMove With {
                        .Beat = Level.DefaultBeat,
                        .Position = New RDPoint(35 / 352 * 100, 50),
                        .Scale = Nothing,
                        .Angle = Nothing,
                        .Pivot = New RDPoint(((tempEvent.Pivot * 282) - (part.left - part.deco.Size.Width / 2)) * 100 / part.deco.Size.Width, 50),
                        .Ease = EaseType.Linear,
                        .Duration = 0
                    }
                    part.deco.Add(CharEvent)
                Next

                '精灵初始表情
                Dim tempRowXs As New RDSetRowXs
                For i = 0 To 5
                    Dim tempExpression As New RDPlayAnimation With {.Beat = Level.DefaultBeat}
                    Decos(i + 1).deco.Add(tempExpression)
                    tempExpression.Expression = beatSettings.Line
                Next

                '对于在范围内的每个节拍
                For Each item In row.Where(Function(i) i.Active, startBeat, endBeat)

                    Select Case item.Type
                        Case RDEventType.HideRow
                            For Each part In Decos
                                Dim tempEvent = CType(item, RDHideRow)
                                Dim CharEvent As New RDSetVisible With {.Beat = item.Beat}
                                part.deco.Add(CharEvent)
                                CharEvent.Visible = (tempEvent.Show = RDHideRow.Shows.Visible) Or (tempEvent.Show = RDHideRow.Shows.OnlyCharacter)
                            Next
                            item.Active = ShowRow
                        Case RDEventType.MoveRow
                            For Each part In Decos
                                Dim tempEvent = CType(item, RDMoveRow)
                                Dim CharEvent As New RDMove With {.Beat = item.Beat}
                                part.deco.Add(CharEvent)
                                If tempEvent.CustomPosition Then
                                    Select Case tempEvent.Target
                                        Case RDMoveRow.Targets.WholeRow
                                            CharEvent.Position = tempEvent.RowPosition
                                            CharEvent.Scale = tempEvent.Scale
                                            CharEvent.Angle = tempEvent.Angle
                                            If tempEvent.Pivot IsNot Nothing Then
                                                CharEvent.Pivot = New RDPoint(((tempEvent.Pivot * 282) - (part.left - part.deco.Size.Width / 2)) * 100 / part.deco.Size.Width, 50)
                                            End If
                                            CharEvent.Ease = tempEvent.Ease
                                            CharEvent.Duration = tempEvent.Duration
                                        Case RDMoveRow.Targets.Character
                                            CharEvent.Position = tempEvent.RowPosition
                                            CharEvent.Scale = tempEvent.Scale
                                            CharEvent.Angle = tempEvent.Angle
                                            CharEvent.Pivot = New RDPoint((-(part.left - part.deco.Size.Width / 2)) * 100 / part.deco.Size.Width, 50)
                                            CharEvent.Ease = tempEvent.Ease
                                            CharEvent.Duration = tempEvent.Duration
                                            Level.Add(CharEvent)
                                    End Select
                                End If
                            Next
                            item.Active = ShowRow
                        Case RDEventType.TintRows
                            For Each part In Decos
                                Dim tempEvent = CType(item, RDTintRows)
                                Dim CharEvent As New RDTint With {
                                    .Beat = item.Beat,
                                    .Border = tempEvent.Border,
                                    .BorderColor = tempEvent.BorderColor,
                                    .Tint = tempEvent.Tint,
                                    .Opacity = tempEvent.Opacity,
                                    .Ease = tempEvent.Ease,
                                    .Duration = tempEvent.Duration
                                }
                                part.deco.Add(CharEvent)
                            Next
                            item.Active = ShowRow
                        Case RDEventType.PlayAnimation
                            Dim part = Decos(0)
                            Dim tempEvent = CType(item, RDPlayExpression)
                            Dim charEvent As New RDPlayAnimation With {.Beat = item.Beat}
                            part.deco.Add(charEvent)
                            charEvent.Expression = tempEvent.Expression
                            item.Active = ShowRow
                        Case RDEventType.SetRowXs
                            Dim tempEvent = CType(item, RDSetRowXs)
                            For i = 0 To 5
                                If tempEvent.Pattern(i) <> tempRowXs.Pattern(i) Then
                                    Dim tempAnimation As New RDPlayAnimation With {.Beat = tempEvent.Beat}
                                    Decos(i + 1).deco.Add(tempAnimation)
                                    '	tempAnimation.Expression =
                                End If
                            Next
                        Case RDEventType.AddClassicBeat

                        Case RDEventType.AddFreeTimeBeat

                        Case RDEventType.PulseFreeTimeBeat

                    End Select

                Next
                row.HideAtStart = Not ShowRow
            Next
        End Sub
        Private Function GetExpression(names As SplitRowSettings, before As RDSetRowXs, after As RDSetRowXs, index As Byte) As String
            'If before Then
            Throw New NotImplementedException
        End Function
        ''' <summary>
        ''' 添加浮动文字式计时器
        ''' </summary>
        ''' <param name="copy">浮动文字的模板，用于提供浮动文字的所有参数</param>
        ''' <param name="interval">细分间隔，每秒事件数</param>
        ''' <param name="increase">递增(true)或递减(false)</param>
        Public Sub AddTimer(copy As RDFloatingText, interval As UInteger, increase As Boolean)
            Dim finish = Level.FirstOrDefault(Function(i) i.Type = RDEventType.FinishLevel, Level.Last).Beat
            Dim t As Integer = 0
            Dim C As New RDBeatCalculator(Level)

            Dim txt = Clone(copy)
            txt.Beat = New RDBeat(Level.Calculator, 1)
            txt.Text = If(increase, TimeSpan.Zero, finish.TimeSpan - TimeSpan.Zero).ToString
            Level.Add(txt)
            Do
                Level.Add(txt.CreateAdvanceText(C.BeatOf(TimeSpan.FromSeconds(t / interval))))
                txt.Text += vbCrLf + $"{If(increase, TimeSpan.FromSeconds(t / interval), finish.TimeSpan - TimeSpan.FromSeconds(t / interval))}"
                t += 1
            Loop Until finish.TimeSpan - TimeSpan.FromSeconds(t / interval) < TimeSpan.Zero
        End Sub
        ''' <summary>
        ''' 添加浮动文字式计拍器
        ''' </summary>
        ''' <param name="copy">浮动文字的模板，用于提供浮动文字的所有参数</param>
        ''' <param name="interval">细分间隔，每秒事件数</param>
        ''' <param name="increase">递增(true)或递减(false)</param>
        Public Sub AddBeater(copy As RDFloatingText, interval As UInteger, increase As Boolean)
            Dim finish = Level.FirstOrDefault(Function(i) i.Type = RDEventType.FinishLevel).Beat
            Dim t As Integer = 0
            Dim C As New RDBeatCalculator(Level)

            Dim txt = Clone(copy)
            txt.Beat = New RDBeat(Level.Calculator, 1)
            txt.Text = If(increase, (1, 1), (finish - 1).BarBeat).ToString
            Level.Add(txt)
            Do
                Level.Add(txt.CreateAdvanceText(C.BeatOf(TimeSpan.FromSeconds(t / interval))))
                txt.Text += vbCrLf + $"{If(increase, C.Time_BarBeat(TimeSpan.FromSeconds(t / interval)), C.Time_BarBeat(finish.TimeSpan - TimeSpan.FromSeconds(t / interval)))}"
                t += 1
            Loop Until finish.TimeSpan - TimeSpan.FromSeconds(t / interval) < TimeSpan.Zero
        End Sub
        ''' <summary>
        ''' 批量添加精灵
        ''' </summary>
        ''' <param name="room">房间</param>
        ''' <param name="sprite">精灵对象</param>
        ''' <param name="count">个数</param>
        ''' <param name="depth">精灵深度</param>
        ''' <param name="visible">精灵的初始可见性</param>
        Public Sub AddLotsOfDecos(room As RDSingleRoom, sprite As RDSprite, count As UInteger, Optional depth As Integer = 0, Optional visible As Boolean = True)
            For i As UInteger = 0 To count
                Dim s = Level.CreateDecoration(room)
                With s
                    .File = sprite
                    .Depth = depth
                    .Visible = visible
                End With
            Next
        End Sub
        ''' <summary>
        ''' 批量添加精灵
        ''' </summary>
        ''' <param name="decoration">精灵模板</param>
        ''' <param name="count">个数</param>
        Public Sub AddLotsOfDecos(decoration As Decoration, count As UInteger)
            For i As UInteger = 0 To count
                Level.CloneDecoration(decoration)
            Next
        End Sub
        ''' <summary>
        ''' 全局更改拍号和移动事件
        ''' </summary>
        Public Sub MoveBeats(cpb As UInteger, offset As Integer)
            For Each item In Level.Where(Of RDSetCrotchetsPerBar)
                item.CrotchetsPerBar = cpb
            Next
            For Each item In Level
                item.Beat += offset
            Next
            If offset > 0 Then
                Level.Add(New RDSetCrotchetsPerBar() With{.CrotchetsPerBar=cpb})
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
            Pulses = Pulses.GroupBy(Function(i) i.Beat).Select(Function(i) i.First).OrderBy(Function(i) i.Beat).ToList
            For i = 0 To Pulses.Count - 2
                PulsesInterval.Add((Pulses(i), Pulses(i + 1), (Pulses(i + 1).Beat + Pulses(i + 1).Hold).TimeSpan - Pulses(i).Beat.TimeSpan))
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
        Public Sub SortRange(eventsTobeSort As Func(Of RDBaseEvent, Boolean), sortKey As Func(Of RDBaseEvent, Single))
            Dim sortList = Level.Where(eventsTobeSort)
            Level.RemoveAll(sortList)
            Level.AddRange(sortList.OrderBy(sortKey))
        End Sub
        Public Class SplitRowSettings

            Public Line As String
            Public Synco As String

            Public Beat_Open As String
            Public Beat_Double_Flash As String
            Public Beat_Triple_Flash As String
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
End Namespace
Namespace Events
End Namespace