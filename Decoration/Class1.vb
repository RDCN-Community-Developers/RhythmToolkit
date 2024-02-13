Imports System.Runtime.Remoting.Metadata
Imports Newtonsoft.Json
Imports System


Public Module Sprite
	Public Enum LoopOption
		no
		yes
		onBeat
	End Enum

	Public Class Sprite
		Private Images As List(Of bitmap)
		Private _size As Size
		<JsonProperty("size")>
		Public ReadOnly Property Size As Integer()
			Get
				Return {_size.Width, _size.Height}
			End Get
		End Property
		<JsonIgnore>
		Public Property SetSize As Size
			Get
				Return _size
			End Get
			Set(value As Size)
				_size = value
			End Set
		End Property
		<JsonIgnore>
		Public ReadOnly Property Name As String
		<JsonProperty("clips")>
		Public Property Clips As New List(Of Clip)
		Public Class Clip
			Private _portraitSize As New Size(25, 25)
			Private _portraitOffset As New Size(0, 0)
			Private _loop As LoopOption = LoopOption.no
			<JsonProperty("frames")>
			Public Property Frames As List(Of Integer)
			<JsonProperty("portraitScale")>
			Public Property PortraitScale As Integer = 2
			<JsonProperty("loopStart")>
			Public Property LoopStart As Integer
			<JsonProperty("name")>
			Public Property Name As String
			<JsonProperty("portraitSize")>
			Public ReadOnly Property PortraitSize As Integer()
				Get
					Return {_portraitSize.Width, _portraitSize.Height}
				End Get
			End Property
			<JsonProperty("loop")>
			Public ReadOnly Property [Loop] As String
				Get
					Return _loop.ToString
				End Get
			End Property
			<JsonProperty("fps")>
			Public Property Fps As Integer = 0
			<JsonProperty("portraitOffset")>
			Public ReadOnly Property PortraitOffset As Integer()
				Get
					Return {_portraitOffset.Width, _portraitOffset.Height}
				End Get
			End Property
			Public Sub New(name As String, frames As List(Of Integer))
				_Name = name
				_Frames = frames
			End Sub
		End Class
		Public Sub New(name As String, size As Size)
			Clips.Add(New Clip("neutral", New List(Of Integer)))
			_size = size
			_Name = name
		End Sub
		Public Function Exist(text As String) As Boolean
			Return Clips.Exists(Function(clip) clip.Name = text)
		End Function
	End Class
End Module
