Imports System.Drawing
Imports RhythmBase.Objects
Imports RhythmBase.Objects.Events
'Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.CSharp.Scripting
Imports Microsoft.CodeAnalysis.Scripting
Imports Microsoft.CodeAnalysis.CSharp
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
	'	'	Public Function FilterCode(code As String) As Func(Of BaseEvent, Boolean)
	'	'		Dim provider As New VBCodeProvider
	'	'		Dim parameters As New CompilerParameters With {
	'	'			.GenerateExecutable = False,
	'	'			.GenerateInMemory = True
	'	'		}

	'	'		With parameters.ReferencedAssemblies
	'	'			.Add("mscorlib.dll")
	'	'			.Add("system.dll")
	'	'			.Add(GetType(BaseEvent).Assembly.Location)
	'	'		End With

	'	'		Dim sourceCode = $"Imports System
	'	'Public Class Filters
	'	'	Public Function EventFilter(e As BaseEvent) As Boolean
	'	'		{code}
	'	'	End Function
	'	'End Class
	'	'"
	'	'		' 编译代码
	'	'		Dim results As CompilerResults = provider.CompileAssemblyFromSource(parameters, sourceCode)

	'	'		If results.Errors.HasErrors Then
	'	'			Throw New Exception(String.Join(Environment.NewLine, results.Errors.Cast(Of CompilerError).Select(Function(err) $"{err.Line},{err.Column}: {err.ErrorText}")))
	'	'		End If

	'	'		' 从生成的程序集中加载类型并获取方法信息
	'	'		Dim dynamicType As Type = results.CompiledAssembly.GetType("Filters")
	'	'		Dim methodInfo As MethodInfo = dynamicType.GetMethod("EventFilter")

	'	'		' 创建委托
	'	'		Return DirectCast([Delegate].CreateDelegate(GetType(Func(Of BaseEvent, Boolean)), methodInfo), Func(Of BaseEvent, Boolean))

	'	'	End Function
	'	Public Function FilterCodeVisualBasic(code As String) As Func(Of BaseEvent, Boolean)
	'		Dim fullCode =
	'$"
	'Imports System
	'Namespace RDLevel
	'    Public Class Filters
	'		Public Shared Function EventFilter(e As BaseEvent) As Boolean
	'				{code}
	'		End Function
	'	End Class
	'End Namespace
	'		"
	'		Dim synt As SyntaxTree = VisualBasicSyntaxTree.ParseText(fullCode)
	'		Dim references = AppDomain.CurrentDomain.GetAssemblies.Where(Function(i) Not i.IsDynamic AndAlso i.Location IsNot Nothing).Select(Function(i) MetadataReference.CreateFromFile(i.Location)).Concat({MetadataReference.CreateFromFile(GetType(BaseEvent).Assembly.Location)})
	'		Dim compilation = VisualBasicCompilation.Create("DynamicCode", {}, references, New VisualBasicCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
	'		Using ms As New IO.MemoryStream
	'			Dim result As EmitResult = compilation.Emit(ms)
	'			If Not result.Success Then
	'				Throw New Exception("Failed to compile code. Errors: " +
	'					String.Join(Environment.NewLine, result.Diagnostics.Where(Function(d) d.IsWarningAsError OrElse d.Severity = DiagnosticSeverity.Error).Select(Function(d) $"{d.Id}: {d.GetMessage()}")))
	'			End If

	'			ms.Seek(0, IO.SeekOrigin.Begin)
	'			Dim assembly1 = Assembly.Load(ms.ToArray)
	'			Dim type = assembly1.GetType("RDLevel.Filters")
	'			Dim methodInfo = type.GetMethod("EventFilter")

	'			Dim parameter As ParameterExpression = Expression.Parameter(GetType(BaseEvent), "baseEvent")
	'			Dim methodCall As MethodCallExpression = Expression.Call(Nothing, methodInfo, parameter)
	'			Dim lambda As LambdaExpression = Expression.Lambda(Of Func(Of BaseEvent, Boolean))(methodCall, parameter)

	'			Return lambda.Compile()
	'		End Using
	'	End Function
	'	Public Function FilterCodeCSharp(code As String) As Func(Of BaseEvent, Boolean)
	'		Dim fullCode =
	'$"
	'using System
	'namespace RDLevel{{
	'    public static class Filters{{
	'		public static bool EventFilter(BaseEvent e){{
	'				{code}
	'		}}
	'	}}
	'}}
	'		"
	'		Dim synt As SyntaxTree = CSharpSyntaxTree.ParseText(fullCode)
	'		Dim references = AppDomain.CurrentDomain.GetAssemblies.Where(Function(i) Not i.IsDynamic AndAlso i.Location IsNot Nothing).Select(Function(i) MetadataReference.CreateFromFile(i.Location)).Concat({MetadataReference.CreateFromFile(GetType(BaseEvent).Assembly.Location)})
	'		Dim compilation = CSharpCompilation.Create("DynamicCode", {}, references, New CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
	'		Using ms As New IO.MemoryStream
	'			Dim result As EmitResult = compilation.Emit(ms)
	'			If Not result.Success Then
	'				Throw New Exception("Failed to compile code. Errors: " +
	'					String.Join(Environment.NewLine, result.Diagnostics.Where(Function(d) d.IsWarningAsError OrElse d.Severity = DiagnosticSeverity.Error).Select(Function(d) $"{d.Id}: {d.GetMessage()}")))
	'			End If

	'			ms.Seek(0, IO.SeekOrigin.Begin)
	'			Dim assembly1 = Assembly.Load(ms.ToArray)
	'			Dim A = assembly1.GetExportedTypes
	'			For Each item In assembly1.GetExportedTypes
	'				Console.WriteLine(item.ToString)
	'			Next
	'			Dim type = assembly1.GetType("RDLevel.Filters")
	'			Dim methodInfo = type.GetMethod("EventFilter")

	'			Dim parameter As ParameterExpression = Expression.Parameter(GetType(BaseEvent), "baseEvent")
	'			Dim methodCall As MethodCallExpression = Expression.Call(Nothing, methodInfo, parameter)
	'			Dim lambda As LambdaExpression = Expression.Lambda(Of Func(Of BaseEvent, Boolean))(methodCall, parameter)

	'			Return lambda.Compile()
	'		End Using
	'	End Function
	Public Function FilterCodeCSharp(code As String) As Func(Of BaseEvent, Boolean)
		Dim script = CSharpScript.Create(code, globalsType:=GetType(BaseEvent),
		options:=ScriptOptions.Default.WithReferences(GetType(BaseEvent).Assembly).WithImports("RDLevel.RhythmDoctorObjects"))
		script.Compile()
		Return Function(i As BaseEvent)
				   Return script.RunAsync(i).Result.ReturnValue
			   End Function
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
