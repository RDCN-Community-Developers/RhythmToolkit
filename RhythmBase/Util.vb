Imports System.Drawing
Imports RhythmBase.Objects
Imports RhythmBase.Events
Imports Microsoft.CodeAnalysis
Imports System.Reflection
''' <summary>
''' 工具类
''' </summary>
Public Module Util
	Public Class BeatCalculator
		Private CPBs As IEnumerable(Of SetCrotchetsPerBar)
		Private BPMs As IEnumerable(Of BaseBeatsPerMinute)
		Public Sub New(CPBCollection As IEnumerable(Of SetCrotchetsPerBar), BPMCollection As IEnumerable(Of BaseBeatsPerMinute))
			Initialize(CPBCollection, BPMCollection)
		End Sub
		Public Sub New(level As RDLevel)
			Initialize(level.CPBs, level.BPMs)
		End Sub
		Private Sub Initialize(CPBs As IEnumerable(Of SetCrotchetsPerBar), BPMs As IEnumerable(Of BaseBeatsPerMinute))
			Me.CPBs = CPBs.OrderBy(Function(i) i.BeatOnly)
			Me.BPMs = BPMs.OrderBy(Function(i) i.BeatOnly)
			Initialize(CPBs)
		End Sub
		Public Shared Sub Initialize(CPBs As IEnumerable(Of SetCrotchetsPerBar))
			For Each item In CPBs
				item.Bar = BeatOnly_BarBeat(item.BeatOnly, CPBs).bar
			Next
		End Sub
		Public Function BarBeat_BeatOnly(bar As UInteger, beat As Single) As Single
			Return BarBeat_BeatOnly(bar, beat, CPBs)
		End Function
		Public Function BarBeat_Time(bar As UInteger, beat As Single) As TimeSpan
			Return BeatOnly_Time(BarBeat_BeatOnly(bar, beat))
		End Function
		Public Function BeatOnly_BarBeat(beat As Single) As (bar As UInteger, beat As Single)
			Return BeatOnly_BarBeat(beat, CPBs)
		End Function
		Public Function BeatOnly_Time(beatOnly As Single) As TimeSpan
			Return BeatOnly_Time(beatOnly, BPMs)
		End Function
		Public Function Time_BeatOnly(timeSpan As TimeSpan) As Single
			Return Time_BeatOnly(timeSpan, BPMs)
		End Function
		Public Function Time_BarBeat(timeSpan As TimeSpan) As (bar As UInteger, beat As Single)
			Return BeatOnly_BarBeat(Time_BeatOnly(timeSpan))
		End Function
		Public Shared Function BarBeat_BeatOnly(bar As UInteger, beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As Single
			Dim foreCPB As New SetCrotchetsPerBar(1, 0, 8, 1)
			Dim result As Single = 0
			Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.Bar < bar, foreCPB)
			result = LastCPB.BeatOnly + (bar - LastCPB.Bar) * LastCPB.CrotchetsPerBar + beat - 1
			Return result
		End Function
		Public Shared Function BeatOnly_BarBeat(beat As Single, Collection As IEnumerable(Of SetCrotchetsPerBar)) As (bar As UInteger, beat As Single)
			Dim foreCPB As New SetCrotchetsPerBar(1, 0, 8, 1)
			Dim result As (bar As UInteger, beat As Single) = (1, 1)

			Dim LastCPB = Collection.LastOrDefault(Function(i) i.Active AndAlso i.BeatOnly < beat, foreCPB)

			result.bar = LastCPB.Bar + Math.Floor((beat - LastCPB.BeatOnly) / LastCPB.CrotchetsPerBar)
			result.beat = (beat - LastCPB.BeatOnly) Mod LastCPB.CrotchetsPerBar + 1

			Return result
		End Function
		Private Shared Function BeatOnly_Time(beatOnly As Single, BPMCollection As IEnumerable(Of BaseBeatsPerMinute)) As TimeSpan

			Dim foreBPM As BaseBeatsPerMinute = New SetBeatsPerMinute(1, BPMCollection.FirstOrDefault(Function(i) i.Active AndAlso i.Type = EventType.PlaySong, New SetBeatsPerMinute(1, 100, 0)).BeatsPerMinute, 0)
			Dim resultMinute As Single = 0
			For Each item As BaseBeatsPerMinute In BPMCollection
				If beatOnly > item.BeatOnly Then
					resultMinute += (item.BeatOnly - foreBPM.BeatOnly) / foreBPM.BeatsPerMinute
					foreBPM = item
				Else
					Exit For
				End If
			Next
			resultMinute += (beatOnly - foreBPM.BeatOnly) / foreBPM.BeatsPerMinute
			Return TimeSpan.FromMinutes(resultMinute)
		End Function
		Private Shared Function Time_BeatOnly(timeSpan As TimeSpan, BPMCollection As IEnumerable(Of BaseBeatsPerMinute)) As Single
			Dim foreBPM As BaseBeatsPerMinute = New SetBeatsPerMinute(1, BPMCollection.FirstOrDefault(Function(i) i.Active AndAlso i.Type = EventType.PlaySong, New SetBeatsPerMinute(1, 100, 0)).BeatsPerMinute, 0)
			Dim beatOnly As Single = 1
			For Each item As BaseBeatsPerMinute In BPMCollection
				If timeSpan > BeatOnly_Time(item.BeatOnly, BPMCollection) Then
					beatOnly += (BeatOnly_Time(item.BeatOnly, BPMCollection) - BeatOnly_Time(foreBPM.BeatOnly, BPMCollection)).TotalMinutes * foreBPM.BeatsPerMinute
					foreBPM = item
				Else
					Exit For
				End If
			Next
			beatOnly += (timeSpan - BeatOnly_Time(foreBPM.BeatOnly, BPMCollection)).TotalMinutes * foreBPM.BeatsPerMinute
			Return beatOnly
		End Function
	End Class
	Public Function PercentToPixel(point As (X As Single?, Y As Single?)) As (X As Single?, Y As Single?)
		Return PercentToPixel(point, (352, 198))
	End Function
	Public Function PercentToPixel(point As (X As Single?, Y As Single?), size As (X As Single, Y As Single)) As (X As Single?, Y As Single?)
		Return (point.X * size.X / 100, point.Y * size.Y / 100)
	End Function
	Public Function PixelToPercent(point As (X As Single?, Y As Single?)) As (X As Single?, Y As Single?)
		Return PixelToPercent(point, (352, 198))
	End Function
	Public Function PixelToPercent(point As (X As Single?, Y As Single?), size As (X As Single, Y As Single)) As (X As Single?, Y As Single?)
		Return (point.X * 100 / size.X, point.Y * 100 / size.Y)
	End Function
	Public Function FixFraction(number As Single, splitBase As UInteger) As Single
		Dim integerPart As Integer = Math.Floor(number)
		Dim decimalPart As Single = ((number - integerPart) * splitBase * 2) + 1
		Return integerPart + (Math.Floor(decimalPart) \ 2) / splitBase
	End Function
	Public Function RgbaToArgb(Rgba As Int32) As Int32
		Return ((Rgba >> 8) And &HFFFFFF) Or ((Rgba << 24) And &HFF000000)
	End Function
	Public Function ArgbToRgba(Argb As Int32) As Int32
		Return ((Argb >> 24) And &HFF) Or ((Argb << 8) And &HFFFFFF00)
	End Function
	Public Function ToCamelCase(value As String, upper As Boolean) As String
		Dim S = value.ToArray
		If upper Then
			S(0) = S(0).ToString.ToUpper
		Else
			S(0) = S(0).ToString.ToLower
		End If
		Return String.Join("", S)
	End Function
	Public Function MaxSizeOf(Size1 As Size, Size2 As Size) As Size
		MaxSizeOf.Width = Math.Max(Size1.Width, Size2.Width)
		MaxSizeOf.Height = Math.Max(Size1.Height, Size2.Height)
	End Function
	Public Function IsIn(Size1 As Size, Size2 As Size) As Boolean
		Return Size1.Width < Size2.Width And Size1.Height < Size2.Height
	End Function
	Public Function Clone(Of T As BaseEvent)(e As T) As T
		Return Clone(e)
	End Function
	Public Function Clone(e As BaseEvent) As BaseEvent
		If e Is Nothing Then
			Return Nothing
		End If
		Dim type As Type = e.GetType
		Dim copy = Activator.CreateInstance(type)

		Dim properties() As Reflection.PropertyInfo = type.GetProperties(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance)
		For Each p In properties
			If p.CanWrite Then
				p.SetValue(copy, p.GetValue(e))
			End If
		Next
		Return copy
	End Function
	Public Class ModulesException
		Inherits Exception
		Sub New()
			MyBase.New()
		End Sub
		Sub New(Message As String)
			MyBase.New(Message)
		End Sub
	End Class
End Module
