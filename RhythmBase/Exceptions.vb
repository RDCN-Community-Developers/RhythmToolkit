Imports System.IO

Namespace Exceptions

    Public Class RhythmBaseException
        Inherits Exception
        Public Sub New()
            MyBase.New
        End Sub
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub
    End Class
    Public Class IllegalEventTypeException
        Inherits RhythmBaseException
        Public Overrides ReadOnly Property Message As String
            Get
                Return $"Illegal type: ""{IllegalTypeName}""{If(ExtraMessage.IsNullOrEmpty, ".", $", {ExtraMessage}")}"
            End Get
        End Property
        Public ReadOnly Property ExtraMessage As String
        Public ReadOnly Property IllegalTypeName As String
        Public Sub New(type As Type)
            Me.New(type, String.Empty)
        End Sub
        Public Sub New(type As String)
            Me.New(type, String.Empty)
        End Sub
        Public Sub New(type As Type, extraMessage As String)
            IllegalTypeName = type.Name
            Me.ExtraMessage = extraMessage
        End Sub
        Public Sub New(type As String, extraMessage As String)
            IllegalTypeName = type
            Me.ExtraMessage = extraMessage
        End Sub
        Public Sub New(type As Type, extraMessage As String, innerException As Exception)
            MyBase.New("", innerException)
            IllegalTypeName = type.Name
            Me.ExtraMessage = extraMessage
        End Sub
        Public Sub New(type As String, extraMessage As String, innerException As Exception)
            MyBase.New("", innerException)
            IllegalTypeName = type
            Me.ExtraMessage = extraMessage
        End Sub
    End Class
    Public Class ConvertingException
        Inherits RhythmBaseException
        Public Sub New(innerException As Exception)
            MyBase.New($"An exception was thrown on reading the level.", innerException)
        End Sub
        Public Sub New(message As String)
            MyBase.New($"An exception was thrown on reading the event: {message}")
        End Sub
        Public Sub New([event] As Newtonsoft.Json.Linq.JObject, innerException As Exception)
            MyBase.New($"An exception was thrown on reading the event. ""{[event]}""", innerException)
        End Sub
    End Class
    Public Class VersionTooLowException
        Inherits RhythmBaseException
        Public LevelVersion As Integer
        Public Overrides ReadOnly Property Message As String = $"Might not support. The version {LevelVersion} is too low. Save this level with the latest version of the game to update the level version."
        Public Sub New(version As Integer)
            LevelVersion = version
        End Sub
        Public Sub New(version As Integer, innerException As Exception)
            MyBase.New(String.Empty, innerException)
            LevelVersion = version
        End Sub
    End Class
    Class SpriteException
        Inherits Exception
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub
    End Class
    Class FileExtensionMismatchException
        Inherits SpriteException
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub
    End Class
    Class OverwriteNotAllowedException
        Inherits RhythmBaseException
        Public Property FilePath As String
        Private ReadOnly _referType As Type
        Public Overrides ReadOnly Property Message As String
            Get
                Return $"Cannot save file '{FilePath}' because overwriting is disabled by the settings and a file with the same name already exists.
To correct this, change the path or filename or set the OverWrite property of {_referType.Name} to false."
            End Get
        End Property
        Public Sub New(filepath As String, referType As Type)
            MyBase.New(filepath)
            _referType = referType
        End Sub
    End Class
    Class InvalidRDBeatException
        Inherits RhythmBaseException
        Public Overrides ReadOnly Property Message As String = "The beat is invalid, possibly because the beat is not associated with the RDLevel."
    End Class
    Class IllegalBeatException
        Inherits RhythmBaseException
        Public Item As IRDBarBeginningEvent
        Public Overrides ReadOnly Property Message As String
            Get
                Return $"This beat is invalid, the event {CType(Item, RDBaseEvent).Type} only allows the beat to be at the beginning of the bar."
            End Get
        End Property
        Public Sub New(item As IRDBarBeginningEvent)
            Me.Item = item
        End Sub
    End Class
End Namespace
