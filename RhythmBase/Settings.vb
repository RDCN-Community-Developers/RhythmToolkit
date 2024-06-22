Namespace Settings
    Public Class LevelInputSettings
        Public Property PreloadAssets As Boolean = False
        Public Property UsingIdFromFile As Boolean = True
    End Class
    Public Class LevelOutputSettings
        Public Property OverWrite As Boolean = False
        Public Property Indented As Boolean = True
    End Class
    Public Class SpriteInputSettings
    End Class
    Public Class SpriteOutputSettings
        Public Property OverWrite As Boolean = False
        Public Property Indented As Boolean = True
        Public Property IgnoreNullValue As Boolean = False
        Public Property WithImage As Boolean = False
    End Class
End Namespace