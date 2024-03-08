Imports System
Imports RhythmBase.Objects
Imports RhythmBase.Events
Imports RhythmAsset
Imports System.Data.Common
Imports RhythmBase.Util
Module Program
	Sub Main1(args As String())

		Dim leve2 As Objects.RDLevel = RDLevel.LoadFile(New IO.FileInfo("C:\Users\30698\OneDrive\文档\rdlevels\新年快乐\level.rdlevel"), New InputSettings.LevelInputSettings With {
														.SpriteSettings = New SpriteInputSettings With {
														.PlaceHolder = True}})
		Dim finish = leve2.firstordefault(Function(i) i.Type = EventType.FinishLevel).BeatOnly
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
		txt1.Anchor = FloatingText.AnchorStyle.Left Or FloatingText.AnchorStyle.Upper
		txt1.Text = (C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval)).ToString
		leve2.Add(txt1)
		Dim txt2 As New FloatingText With {
			.Y = 1,
			.BeatOnly = txt1.BeatOnly,
			.TextPosition = (5, 80),
			.Size = 10,
			.Id = t,
			.Mode = FloatingText.OutMode.HideAbruptly,
			.FadeOutRate = 1,
			.Anchor = FloatingText.AnchorStyle.Left Or FloatingText.AnchorStyle.Upper,
			.Text = C.BeatOnly_BarBeat(txt1.BeatOnly).ToString
		}
		txt2.Rooms(0) = True
		leve2.add(txt2)
		Do
			leve2.add(txt1.CreateAdvanceText(C.Time_BeatOnly(TimeSpan.FromSeconds(t / interval))))
			leve2.add(txt2.CreateAdvanceText(C.Time_BeatOnly(TimeSpan.FromSeconds(t / interval))))
			txt1.Text += vbCrLf + $"{(C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval))}"
			txt2.Text += vbCrLf + $"{(C.Time_BarBeat(TimeSpan.FromSeconds(t / interval)))}"
			t += 1
		Loop Until C.BeatOnly_Time(finish) - TimeSpan.FromSeconds(t / interval) < TimeSpan.Zero

		leve2.SaveFile(New IO.FileInfo("C:\Users\30698\OneDrive\文档\rdlevels\新年快乐\levelEdited.rdlevel"), New InputSettings.LevelInputSettings With {.SpriteSettings = New SpriteInputSettings With {.PlaceHolder = True}})

	End Sub
	Sub Main(args As String())
		Console.WriteLine("输入 RDLevel 文件路径 (.rdlevel):")
		Console.WriteLine()
		Dim Path = "C:\Users\30698\OneDrive\文档\rdlevels\xnkl\level.rdlevel" ' Console.ReadLine
		Dim Level = RDLevel.LoadFile(New IO.FileInfo(Path), New InputSettings.LevelInputSettings With {.SpriteSettings = New SpriteInputSettings With {.PlaceHolder = True}})

		Dim TypeList = Level.where(Function(i) i.Tab = Events.Tabs.Actions).GroupBy(Function(i) i.Type).Select(Function(i) i.First.Type).ToList
		For Each item In Level.where(Function(i) i.Tab = Events.Tabs.Actions)
			item.Y = TypeList.IndexOf(item.Type)
		Next

		Level.SaveFile(New IO.FileInfo("C:\Users\30698\OneDrive\文档\rdlevels\xnkl\levelEdited.rdlevel"), New InputSettings.LevelInputSettings With {.SpriteSettings = New SpriteInputSettings With {.PlaceHolder = True}}) '"output.rdlevel")
		Console.WriteLine()
		Console.WriteLine("已保存至本程序下的 output.rdlevel。")
	End Sub

End Module
