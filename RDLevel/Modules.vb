Imports System.Drawing
#Disable Warning CA1507
#Disable Warning CA1416
Public Module PublicLib
	Public Class ModulesException
		Inherits Exception
		Sub New()
			MyBase.New()
		End Sub
		Sub New(Message As String)
			MyBase.New(Message)
		End Sub
	End Class
	Public Function MaxSizeOf(Size1 As Size, Size2 As Size) As Size
		MaxSizeOf.Width = Math.Max(Size1.Width, Size2.Width)
		MaxSizeOf.Height = Math.Max(Size1.Height, Size2.Height)
	End Function
	Public Function IsIn(Size1 As Size, Size2 As Size) As Boolean
		Return Size1.Width < Size2.Width And Size1.Height < Size2.Height
	End Function
	'Public Class FileLocator
	'	Private Filepath As IO.FileInfo
	'	Private Shared CurrentFilePath As IO.DirectoryInfo
	'	Public Shared Property Current As IO.DirectoryInfo
	'		Get
	'			Return CurrentFilePath
	'		End Get
	'		Set(value As IO.DirectoryInfo)
	'			CurrentFilePath = value
	'		End Set
	'	End Property
	'	Public ReadOnly Property Name As String
	'		Get
	'			Return Filepath.Name
	'		End Get
	'	End Property
	'	Public ReadOnly Property Extension As String
	'		Get
	'			Return Filepath.Extension
	'		End Get
	'	End Property
	'	Public ReadOnly Property FullName As String
	'		Get
	'			Return Filepath.FullName
	'		End Get
	'	End Property
	'	Public Sub New(filename As String)
	'		Try
	'			Filepath = New IO.FileInfo(CurrentFilePath.FullName + filename)
	'		Catch
	'			Throw New NotImplementedException
	'		End Try
	'	End Sub
	'	Public Function GetFile() As Byte()
	'		Return IO.File.ReadAllBytes(Filepath.FullName)
	'	End Function
	'	Public Function GetFileAsText() As String
	'		Return IO.File.ReadAllText(Filepath.FullName)
	'	End Function
	'	Public Function GetFileAsJson() As Linq.JObject
	'		Return JsonConvert.DeserializeObject(IO.File.ReadAllText(Filepath.FullName))
	'	End Function
	'	Public Function GetFIleAsImage() As Bitmap
	'		Return New Bitmap(Filepath.FullName)
	'	End Function
	'	'Public Shared Widening Operator CType(filename As String) As FileLocator
	'	'	Return New FileLocator(filename)
	'	'End Operator
	''End Class
End Module