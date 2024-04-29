Imports System.IO
Imports System.Numerics
Imports Newtonsoft.Json
Imports SkiaSharp
Imports RhythmBase.Components

Namespace Assets
    Public Interface ISprite
        ReadOnly Property FilePath As String
        ReadOnly Property Size As RDPoint
        ReadOnly Property Name As String
        ReadOnly Property Expressions As IEnumerable(Of String)
        ReadOnly Property Preview As SKBitmap
    End Interface
    Public Class Sprite

    End Class
    'Public Class Quote
    '    Implements ISprite
    '    Private ReadOnly _File As String
    '    Public Sub New(path As String)
    '        _File = path
    '    End Sub
    '    <JsonIgnore>
    '    Public ReadOnly Property Expressions As IEnumerable(Of String) Implements ISprite.Expressions
    '        Get
    '            Return New List(Of String)
    '        End Get
    '    End Property
    '    Public ReadOnly Property FilePath As String Implements ISprite.FilePath
    '        Get
    '            Return _File
    '        End Get
    '    End Property
    '    Public ReadOnly Property Size As RDPoint Implements ISprite.Size
    '        Get
    '            Return New RDPoint
    '        End Get
    '    End Property
    '    <JsonIgnore>
    '    Public ReadOnly Property Preview As New SKBitmap Implements ISprite.Preview
    '    Public ReadOnly Property Name As String Implements ISprite.Name
    '        Get
    '            If _File.Extension = ".json" Then
    '                Return IO.Path.GetFileNameWithoutExtension(_File)
    '            Else
    '                Return _File
    '            End If
    '        End Get
    '    End Property
    '    Public Overrides Function ToString() As String
    '        Return Name
    '    End Function
    'End Class
End Namespace