Namespace Settings
    Public Class LevelInputSettings
        ''' <summary>
        ''' 优先使用关卡内 Id
        ''' </summary>
        Public UsingIdFromFile As Boolean = True
    End Class
    Public Class LevelOutputSettings
        Public OverWrite As Boolean = False
        Public Indented As Boolean = True
    End Class
    Public Class SpriteOutputSettings
        Public Sort As Boolean = False
        Public OverWrite As Boolean = False
        Public ExtraFile As Boolean = False
        Public LimitedSize As New RDSizeNI(16384, 16384)
        Public LimitedCount As RDPoint?
        Public WithImage As Boolean = False
    End Class
    Public Class SpriteInputSettings
    End Class
End Namespace