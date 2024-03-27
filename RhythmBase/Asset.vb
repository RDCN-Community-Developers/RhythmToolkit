Imports System.IO
Imports System.Numerics
Imports Newtonsoft.Json
Imports SkiaSharp

Namespace Assets
    Public Interface ISprite
        ReadOnly Property FileInfo As FileInfo
        Property Size As Vector2
        ReadOnly Property Name As String
        ReadOnly Property Expressions As IEnumerable(Of String)
        ReadOnly Property Preview As SKBitmap
    End Interface
    Public Class Quote
        Implements ISprite
        Private ReadOnly _File As FileInfo
        Public Sub New(path As FileInfo)
            _File = path
        End Sub
        <JsonIgnore>
        Public ReadOnly Property Expressions As IEnumerable(Of String) Implements ISprite.Expressions
            Get
                Return New List(Of String)
            End Get
        End Property
        Public ReadOnly Property FileInfo As IO.FileInfo Implements ISprite.FileInfo
            Get
                Return _File
            End Get
        End Property
        Public Property Size As Vector2 Implements ISprite.Size
            Get
                Return New Vector2
            End Get
            Set(value As Vector2)
                Throw New NotImplementedException()
            End Set
        End Property
        <JsonIgnore>
        Public ReadOnly Property Preview As New SKBitmap Implements ISprite.Preview
        Public ReadOnly Property Name As String Implements ISprite.Name
            Get
                If _File.Extension = ".json" Then
                    Return IO.Path.GetFileNameWithoutExtension(_File.Name)
                Else
                    Return _File.Name
                End If
            End Get
        End Property
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    'Public Class NullAsset
    '    Implements ISprite
    '    Public ReadOnly Property FileInfo As FileInfo Implements ISprite.FileInfo
    '        Get
    '            Return Nothing
    '        End Get
    '    End Property

    '    Public Property Size As New Vector2 Implements ISprite.Size
    '    Public ReadOnly Property Name As String = "" Implements ISprite.Name
    '    Public ReadOnly Property Expressions As IEnumerable(Of String) = New HashSet(Of String) Implements ISprite.Expressions
    '    <JsonIgnore>
    '    Public ReadOnly Property Preview As New SKBitmap Implements ISprite.Preview
    'End Class

End Namespace