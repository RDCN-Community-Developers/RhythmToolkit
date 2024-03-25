Imports System.Runtime.CompilerServices
Namespace Components
    Public Module Ease
        Public Enum EaseType As Integer
#If DEBUG Then
            Unset = -1
#End If
            Linear

            InSine
            OutSine
            InOutSine

            InQuad
            OutQuad
            InOutQuad

            InCubic
            OutCubic
            InOutCubic

            InQuart
            OutQuart
            InOutQuart

            InQuint
            OutQuint
            InOutQuint

            InExpo
            OutExpo
            InOutExpo

            InCirc
            OutCirc
            InOutCirc

            InElastic
            OutElastic
            InOutElastic

            InBack
            OutBack
            InOutBack

            InBounce
            OutBounce
            InOutBounce
#If DEBUG Then
            SmoothStep
#End If
        End Enum
        Private Class EaseFunction
            Public Shared Function None(x As Single)
                Return x - x
            End Function
            Public Shared Function Linear(x As Single)
                Return x
            End Function
            Public Shared Function InSine(x As Single)
                Return 1 - Math.Cos((x * Math.PI) / 2)
            End Function
            Public Shared Function OutSine(x As Single)
                Return Math.Sin((x * Math.PI) / 2)
            End Function
            Public Shared Function InOutSine(x As Single)
                Return -(Math.Cos(x * Math.PI) - 1) / 2
            End Function
            Public Shared Function InQuad(x As Single)
                Return x ^ 2
            End Function
            Public Shared Function OutQuad(x As Single)
                Return 1 - (1 - x) ^ 2
            End Function
            Public Shared Function InOutQuad(x As Single)
                Return IIf(x < 0.5, 2 * x ^ 2, 1 - (-2 * x + 2) ^ 2 / 2)
            End Function
            Public Shared Function InCubic(x As Single)
                Return x ^ 3
            End Function
            Public Shared Function OutCubic(x As Single)
                Return 1 - (1 - x) ^ 3
            End Function
            Public Shared Function InOutCubic(x As Single)
                Return IIf(x < 0.5, 4 * x ^ 3, 1 - (-2 * x + 2) ^ 3 / 2)
            End Function
            Public Shared Function InQuart(x As Single)
                Return x ^ 4
            End Function
            Public Shared Function OutQuart(x As Single)
                Return 1 - (1 - x) ^ 4
            End Function
            Public Shared Function InOutQuart(x As Single)
                Return IIf(x < 0.5, 8 * x ^ 4, 1 - (-2 * x + 2) ^ 4 / 2)
            End Function
            Public Shared Function InQuint(x As Single)
                Return x ^ 5
            End Function
            Public Shared Function OutQuint(x As Single)
                Return 1 - (1 - x) ^ 5
            End Function
            Public Shared Function InOutQuint(x As Single)
                Return IIf(x < 0.5, 16 * x ^ 5, 1 - (-2 * x + 2) ^ 5 / 2)
            End Function
            Public Shared Function InExpo(x As Single)
                Return IIf(x = 0, 0, 2 ^ (10 * x - 10))
            End Function
            Public Shared Function OutExpo(x As Single)
                Return IIf(x = 1, 1, 1 - 2 ^ (-10 * x))
            End Function
            Public Shared Function InOutExpo(x As Single)
                Return IIf(x = 0, 0,
                   IIf(x = 1, 1,
                       IIf(x < 0.5,
                           Math.Pow(2, 20 * x - 10) / 2,
                           (2 - Math.Pow(2, -20 * x + 10)) / 2)))
            End Function
            Public Shared Function InCirc(x As Single)
                Return 1 - Math.Sqrt(1 - x ^ 2)
            End Function
            Public Shared Function OutCirc(x As Single)
                Return Math.Sqrt(1 - (x - 1) ^ 2)
            End Function
            Public Shared Function InOutCirc(x As Single)
                Return IIf(x < 0.5,
                   (1 - Math.Sqrt(1 - (2 * x) ^ 2)) / 2,
                   (Math.Sqrt(1 - (-2 * x + 2) ^ 2) + 1) / 2)
            End Function
            Public Shared Function InElastic(x As Single)
                Const c4 = (2 * Math.PI) / 3
                Return IIf(x = 0, 0,
                   IIf(x = 1, 1,
                        -Math.Pow(2, 10 * x - 10) * Math.Sin((x * 10 - 10.75) * c4)))
            End Function
            Public Shared Function OutElastic(x As Single)
                Const c4 = (2 * Math.PI) / 3
                Return IIf(x = 0, 0,
                   IIf(x = 1, 1,
                       Math.Pow(2, -10 * x) * Math.Sin((x * 10 - 0.75) * c4) + 1))
            End Function
            Public Shared Function InOutElastic(x As Single)
                Const c5 = (2 * Math.PI) / 4.5
                Return IIf(x = 0, 0,
                   IIf(x = 1, 1,
                       IIf(x < 0.5, -(Math.Pow(2, 20 * x - 10) * Math.Sin((20 * x - 11.125) * c5)) / 2,
                           (Math.Pow(2, -20 * x + 10) * Math.Sin((20 * x - 11.125) * c5)) / 2 + 1)))
            End Function
            Public Shared Function InBack(x As Single)
                Const c1 = 1.70158
                Const c3 = c1 + 1
                Return c3 * x ^ 3 - c1 * x ^ 2
            End Function
            Public Shared Function OutBack(x As Single)
                Const c1 = 1.70158
                Const c3 = c1 + 1
                Return 1 + c3 * (x - 1) ^ 3 + c1 * (x - 1) ^ 2
            End Function
            Public Shared Function InOutBack(x As Single)
                Const c1 = 1.70158
                Const c2 = c1 * 1.525
                Return IIf(x < 0.5,
                   (2 * x) ^ 2 * ((c2 + 1) * 2 * x - c2) / 2,
                   ((2 * x - 2) ^ 2 * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2)
            End Function
            Public Shared Function InBounce(x As Single)
                Return 1 - OutBounce(1 - x)
            End Function
            Public Shared Function OutBounce(x As Single)
                Const n1 = 7.5625
                Const d1 = 2.75
                If x < 1 / d1 Then
                    Return n1 * x ^ 2
                ElseIf (x < 2 / d1) Then
                    Return n1 * (x - 1.5 / d1) ^ 2 + 0.75
                ElseIf (x < 2.5 / d1) Then
                    Return n1 * (x - 2.25 / d1) ^ 2 + 0.9375
                Else
                    Return n1 * (x - 2.625 / d1) ^ 2 + 0.984375
                End If
            End Function
            Public Shared Function InOutBounce(x As Single)
                Return IIf(x < 0.5,
                   (1 - OutBounce(1 - 2 * x)) / 2,
                   (1 + OutBounce(2 * x - 1)) / 2)
            End Function
            Public Shared Function SmoothStep(x As Single)
                Return (3 - 2 * x) * x ^ 2
            End Function
        End Class
        <Extension>
        Public Function Calculate(Type As EaseType, x As Single) As Single
            Select Case Type
                Case EaseType.Unset
                    Return EaseFunction.None(x)
                Case EaseType.Linear
                    Return EaseFunction.Linear(x)
                Case EaseType.InSine
                    Return EaseFunction.InSine(x)
                Case EaseType.OutSine
                    Return EaseFunction.OutSine(x)
                Case EaseType.InOutSine
                    Return EaseFunction.InOutSine(x)
                Case EaseType.InQuad
                    Return EaseFunction.InQuad(x)
                Case EaseType.OutQuad
                    Return EaseFunction.OutQuad(x)
                Case EaseType.InOutQuad
                    Return EaseFunction.InOutQuad(x)
                Case EaseType.InCubic
                    Return EaseFunction.InCubic(x)
                Case EaseType.OutCubic
                    Return EaseFunction.OutCubic(x)
                Case EaseType.InOutCubic
                    Return EaseFunction.InOutCubic(x)
                Case EaseType.InQuart
                    Return EaseFunction.InQuart(x)
                Case EaseType.OutQuart
                    Return EaseFunction.OutQuart(x)
                Case EaseType.InOutQuart
                    Return EaseFunction.InOutQuart(x)
                Case EaseType.InQuint
                    Return EaseFunction.InQuint(x)
                Case EaseType.OutQuint
                    Return EaseFunction.OutQuint(x)
                Case EaseType.InOutQuint
                    Return EaseFunction.InOutQuint(x)
                Case EaseType.InExpo
                    Return EaseFunction.InExpo(x)
                Case EaseType.OutExpo
                    Return EaseFunction.OutExpo(x)
                Case EaseType.InOutExpo
                    Return EaseFunction.InOutExpo(x)
                Case EaseType.InCirc
                    Return EaseFunction.InCirc(x)
                Case EaseType.OutCirc
                    Return EaseFunction.OutCirc(x)
                Case EaseType.InOutCirc
                    Return EaseFunction.InOutCirc(x)
                Case EaseType.InElastic
                    Return EaseFunction.InElastic(x)
                Case EaseType.OutElastic
                    Return EaseFunction.OutElastic(x)
                Case EaseType.InOutElastic
                    Return EaseFunction.InOutElastic(x)
                Case EaseType.InBack
                    Return EaseFunction.InBack(x)
                Case EaseType.OutBack
                    Return EaseFunction.OutBack(x)
                Case EaseType.InOutBack
                    Return EaseFunction.InOutBack(x)
                Case EaseType.InBounce
                    Return EaseFunction.InBounce(x)
                Case EaseType.OutBounce
                    Return EaseFunction.OutBounce(x)
                Case EaseType.InOutBounce
                    Return EaseFunction.InOutBounce(x)
                Case EaseType.SmoothStep
                    Return EaseFunction.SmoothStep(x)
                Case Else
                    Return 0
            End Select
        End Function
        <Extension>
        Public Function Calculate(Type As EaseType, x As Single, from As Single, [to] As Single) As Single
            Return Type.Calculate(x) * ([to] - from) + from
        End Function
    End Module
End Namespace