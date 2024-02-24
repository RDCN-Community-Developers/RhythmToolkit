Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Reflection

'<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field Or AttributeTargets.Enum)>
'Public Class PropertyTranslationAttribute
'	Inherits Attribute
'	Public Property Name As String
'	Public Sub New(name As String)
'		Me.Name = name
'	End Sub
'End Class
Public Class TranaslationManager
	Private ReadOnly jsonpath As String
	Private ReadOnly values As JObject
	Public Sub New(filepath As String)
		jsonpath = filepath
		values = JsonConvert.DeserializeObject(IO.File.ReadAllText(jsonpath))
	End Sub
	Public Function GetValue(p As MemberInfo, value As String) As String
		Dim current As JObject = values
		Dim keys = GetPath(p)

		For i = 0 To keys.Length - 2
			Dim j As JToken
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
		IO.File.WriteAllText(jsonpath, values.ToString)
	End Sub
End Class
