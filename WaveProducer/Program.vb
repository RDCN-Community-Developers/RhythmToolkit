Imports System
Imports NAudio.Dsp
Imports NAudio.Wave
Imports MathNet.Numerics
Imports RhythmBase.Components
Imports RhythmBase.Components
Imports RhythmBase.Events
Imports RhythmBase.Utils
Imports MathNet.Numerics.IntegralTransforms

Module Program
	Sub Main(args As String())

		Dim rd = New Mp3FileReader("D:\download\Music\Vicki Vox - Pretty without You.mp3")

		Dim ws As New WaveChannel32(rd)

		'比特率/8,每个单通道采样点的位宽
		Dim depth = ws.WaveFormat.BitsPerSample / 8
		'块长,所有通道占用的位宽总数
		Dim block = ws.WaveFormat.BlockAlign
		'通道数
		Dim channels = ws.WaveFormat.Channels
		'采样率,每秒采样数
		Dim rate = ws.WaveFormat.SampleRate
		'	Dim time = ws.WaveFormat.

		'缓存字节
		Dim buffer(rd.Length) As Byte
		Dim readed As Integer
		Dim head = 0
		Do
			readed = rd.Read(buffer, head, buffer.Length)
			head += readed
		Loop Until readed = 0

		'第一通道数据
		Dim singleChannelData(buffer.Length / ws.WaveFormat.Channels) As Double


		Dim myChannel = 0

		Dim i = 0, j = 0
		While i * ws.WaveFormat.Channels * depth < buffer.Length - 1
			singleChannelData(j) = BitConverter.ToInt32(buffer, i * block + myChannel) / 32768
			i += 1
			j += 1
		End While

		Dim max1 = singleChannelData.Max

		'For s = 250000 To singleChannelData.Length - 1 Step 20
		'	For t = -400 To singleChannelData(s) / max1 * 400
		'		Console.Write("=")
		'	Next
		'	If s Mod rate = 0 Then
		'		For t = -300 To 700
		'			Console.Write("=")
		'		Next
		'	End If
		'	Console.WriteLine()
		'Next


		'每 st 秒进行依次 FFT
		Dim st As Double = 0.025
		Dim Frames(singleChannelData.Length / (rate * st))() As Double
		For q As Integer = 0 To singleChannelData.Length / (rate * st)

			Dim frame(rate * st * 2) As Double
			For r = 0 To rate * st * 2
				frame(r) = singleChannelData(q + r)
			Next
			Frames(q) = FFT(frame, frame.Length - 1)
		Next

		Dim max = Frames.Max(Function(q) If(q, {0}).Max(Function(s) Math.Sqrt(s)))
		'Dim outputString = " ▁▂▃▄▅▆▇█"

		'Dim colors() As ConsoleColor = {
		'	ConsoleColor.DarkGreen,
		'	ConsoleColor.Green,
		'	ConsoleColor.Cyan,
		'	ConsoleColor.Blue,
		'	ConsoleColor.DarkBlue,
		'	ConsoleColor.DarkMagenta,
		'	ConsoleColor.Magenta,
		'	ConsoleColor.Red,
		'	ConsoleColor.DarkYellow,
		'	ConsoleColor.Yellow,
		'	ConsoleColor.Gray,
		'	ConsoleColor.White}

		'Dim time As Double = 0
		'Dim index = 0
		'For i = 0 To Frames.Count - 1
		'	Dim id = 0
		'	For Each s In Frames(i)
		'		Console.ForegroundColor = colors(Math.Min(Math.Floor((s ^ 0.5) * (colors.Length - 1) / max), colors.Length - 1))
		'		Console.Write("+")
		'		id += 1
		'		If id > 513 Then
		'			Exit For
		'		End If
		'	Next
		'	Console.WriteLine()
		'	index += 1
		'	'time += 0.02
		'	'If time > 182 Then
		'	'	Exit For
		'	'End If
		'Next




		Dim Level As RDLevel = RDLevel.Read("C:\Users\30698\OneDrive\文档\rdlevels\test3\level.rdlevel")

		Dim SpriteCollection As New List(Of DecorationEventCollection)
		'For i = 0 To 100
		Dim A = Level.CreateDecoration(SingleRoom.Default)
		Dim mv2 As New Move With {
			.Beat = Level.DefaultBeat,
			.Position = New PointE(0, 0),
			.Scale = New PointE(0.1F, 0.1F)
		}
		A.Add(mv2)
		SpriteCollection.Add(A)
		'Next
		Dim Calculator = Level.Calculator

		For time = 0 To 182 Step 0.1
			For index = 0 To Level.Decorations.Count - 1
				Dim t = Calculator.BeatOf(TimeSpan.FromSeconds(time))
				Dim mv = New Move() With {.Beat = t}
				Level.Decorations(index).Add(mv)
				Dim frame = Frames((Frames.Length - 1) * time / 182)
				mv.Position = New PointE(CType(Nothing, Single?), CSng(frame((frame.Length - 1) * index / 100) / max))
				mv.Duration = 1.5
				mv.Ease = EaseType.InOutSine
				Level.Add(mv)
			Next
			If CInt(time) Mod 10 = 0 Then
				Console.WriteLine(time)
			End If
		Next



		Level.Write("C:\Users\30698\OneDrive\文档\rdlevels\test3\levelEdited.rdlevel")


		'Dim mag() As Double = FFT(singleChannelData, 2048)

		''Dim log As Double = Math.Ceiling(Math.Log(singleChannelData.Length))

		''Dim comp() As Complex = singleChannelData.Select(Of Complex)(Function(s) New Complex With {.X = s}).ToArray
		''NAudio.Dsp.FastFourierTransform.FFT(False, log, comp)

		''Dim mag(singleChannelData.Length - 1) As Double

		''Dim max = 0
		''For i = 0 To singleChannelData.Length - 1
		''	mag(i) = Math.Sqrt(comp(i).X ^ 2 + comp(i).Y ^ 2)
		''	max = Math.Max(max, mag(i))
		''Next



		'For j = 0 To mag.Length - 1 Step 8
		'	For i = 0 To mag(j) / mag.Max * 2000
		'		Console.Write("+")
		'	Next
		'	Console.WriteLine()
		'	If j Mod rate = 0 Then
		'		Console.WriteLine(j / rate)
		'		Debug.Print(j / rate)
		'	End If
		'Next

	End Sub
	Public Function FFT(data() As Double, size As UInteger) As Double()
		Dim log As Double = Math.Ceiling(Math.Log(data.Length))
		Dim comp() As Complex = data.Select(Of Complex)(Function(s) New Complex With {.X = s}).ToArray
		NAudio.Dsp.FastFourierTransform.FFT(False, log, comp)
		Dim mag(size) As Double
		For i = 0 To size - 1
			mag(i) = Math.Sqrt(comp(i).X ^ 2 + comp(i).Y ^ 2)
		Next
		Return mag
	End Function
End Module
