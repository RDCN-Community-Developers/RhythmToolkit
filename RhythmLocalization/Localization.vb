Imports System.Reflection
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
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
