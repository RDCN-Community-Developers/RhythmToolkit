Imports System
Imports RDLevel.Objects
Imports System.Data.Common
Imports RDLevel.Util
Module Program
	Sub Main1(args As String())
		'Dim LoadPath = "E:\Desktop\Resources\你我皆相连2\Connected\level.rdlevel"
		'Dim SavePath = "" '"C:\Users\30698\OneDrive\文档\rdlevels\Test2\out.rdlevel"

		'Dim level1 As RhythmDoctorObjects.RDLevel = RhythmDoctorObjects.LoadFile(LoadPath)
		'For Each item In level1.GetPulseEvents
		'	For Each beat In item.PulseTime
		'		If item.Type = RhythmDoctorObjects.Events.EventType.PulseFreeTimeBeat Then
		'			Console.WriteLine(beat)
		'		End If
		'	Next
		'Next
		'level1.Rows(0).CreateChildren(Of RhythmDoctorObjects.HideRow)(1, 2)
		'level1.Save(SavePath)
		'bss.Tables.Item("t2").Rows.Add(2)

		'Dim A = CType(level1.Events.Last(Function(i) i.Type = RhythmDoctorObjects.Events.EventType.PulseFreeTimeBeat), RhythmDoctorObjects.PulseFreeTimeBeat).Pulsable
		'Console.WriteLine(A)
		'For Each item In level1.EventsWhere(Of FloatingText)
		'	Console.WriteLine(item.Id)
		'Next
		'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''	level1.Save(SavePath)
		'

		Dim leve2 As Objects.RDLevel = Objects.LoadFile("C:\Users\30698\OneDrive\文档\rdlevels\新年快乐\level.rdlevel")
		Dim finish = leve2.Events.FirstOrDefault(Function(i) i.Type = EventType.FinishLevel).BeatOnly
		Dim t As Integer = 0
		Dim interval = 37
		Dim C As New BeatCalculator(leve2)

		Dim txt1 As New FloatingText
		txt1.Rooms(0) = True
		txt1.BeatOnly = C.Time_BeatOnly(TimeSpan.FromSeconds(t / interval))
		txt1.TextPosition = (5, 95)
		txt1.Size = 10
		txt1.Id = t
		txt1.Mode = FloatingText.OutMode.HideAbruptly
		txt1.FadeOutRate = 1
		txt1.AnchorType = AnchorType.Left Or AnchorType.Upper
		txt1.Text = (C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval)).ToString
		leve2.Events.Add(txt1)
		Dim txt2 As New FloatingText With {
			.Y = 1,
			.BeatOnly = txt1.BeatOnly,
			.TextPosition = (5, 80),
			.Size = 10,
			.Id = t,
			.Mode = FloatingText.OutMode.HideAbruptly,
			.FadeOutRate = 1,
			.AnchorType = AnchorType.Left Or AnchorType.Upper,
			.Text = C.BeatOnly_BarBeat(txt1.BeatOnly).ToString
		}
		txt2.Rooms(0) = True
		leve2.Events.Add(txt2)
		Do
			leve2.Events.Add(txt1.CreateAdvanceText(C.Time_BeatOnly(TimeSpan.FromSeconds(t / interval))))
			leve2.Events.Add(txt2.CreateAdvanceText(C.Time_BeatOnly(TimeSpan.FromSeconds(t / interval))))
			txt1.Text += vbCrLf + $"{(C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval))}"
			txt2.Text += vbCrLf + $"{(C.Time_BarBeat(TimeSpan.FromSeconds(t / interval)))}"
			t += 1
		Loop Until C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval) < TimeSpan.Zero

		leve2.SaveFile("C:\Users\30698\OneDrive\文档\rdlevels\新年快乐\levelEdited.rdlevel")

	End Sub

	Sub Main(args As String())
		Console.WriteLine("输入 RDLevel 文件路径 (.rdlevel):")
		Console.WriteLine()
		Dim Path = "C:\Users\30698\OneDrive\文档\rdlevels\xnkl\level.rdlevel" ' Console.ReadLine
		Dim Level = RDLevel.Objects.LoadFile(Path)

		Dim TypeList = Level.EventsWhere(Function(i) i.Tab = Events.Tabs.Actions).GroupBy(Function(i) i.Type).Select(Function(i) i.First.Type).ToList
		For Each item In Level.EventsWhere(Function(i) i.Tab = Events.Tabs.Actions)
			item.Y = TypeList.IndexOf(item.Type)
		Next

		Level.SaveFile("C:\Users\30698\OneDrive\文档\rdlevels\xnkl\levelEdited.rdlevel") '"output.rdlevel")
		Console.WriteLine()
		Console.WriteLine("已保存至本程序下的 output.rdlevel。")
	End Sub

End Module
