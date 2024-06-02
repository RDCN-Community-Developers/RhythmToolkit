Imports System.Diagnostics.CodeAnalysis
Imports System.Drawing
Imports RhythmBase.Expressions
#Disable Warning CA1812
Namespace Components
	Public Structure RDPointI
		Implements IEquatable(Of RDPointI)
		Public Sub New(sz As RDSizeI)
			X = sz.Width
			Y = sz.Height
		End Sub
		Public Sub New(x As Integer?, y As Integer?)
			_X = x
			_Y = y
		End Sub
		Public ReadOnly Property IsEmpty As Boolean
			Get
				Return X Is Nothing AndAlso Y Is Nothing
			End Get
		End Property
		Public Property X As Integer?
		Public Property Y As Integer?
		Public Sub Offset(p As RDPointI)
			X += p.X
			Y += p.Y
		End Sub
		Public Sub Offset(dx As Integer?, dy As Integer?)
			X += dx
			Y += dy
		End Sub
		Public Shared Function Ceiling(value As RDPoint) As RDPointI
			Return New RDPointI(If(value.X Is Nothing, Nothing, Math.Ceiling(value.X.Value)), If(value.Y Is Nothing, Nothing, Math.Ceiling(value.Y.Value)))
		End Function
		Public Shared Function Add(pt As RDPointI, sz As RDSizeI) As RDPointI
			Return New RDPointI(pt.X + sz.Width, pt.Y + sz.Height)
		End Function
		Public Shared Function Truncate(value As RDPoint) As RDPointI
			Return New RDPointI(If(value.X Is Nothing, Nothing, Math.Truncate(value.X.Value)), If(value.Y Is Nothing, Nothing, Math.Truncate(value.Y.Value)))
		End Function
		Public Shared Function Subtract(pt As RDPointI, sz As RDSizeI) As RDPointI
			Return New RDPointI(pt.X - sz.Width, pt.Y - sz.Height)
		End Function
		Public Shared Function Round(value As RDPoint) As RDPointI
			Return New RDPointI(If(value.X Is Nothing, Nothing, Math.Truncate(value.X.Value)), If(value.Y Is Nothing, Nothing, Math.Truncate(value.Y.Value)))
		End Function
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDPointI) AndAlso Equals(CType(obj, RDPointI))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim h As New HashCode
			h.Add(X)
			h.Add(Y)
			Return h.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return $"[{X}, {Y}]"
		End Function
		Private Overloads Function Equals(other As RDPointI) As Boolean Implements IEquatable(Of RDPointI).Equals
			Return other.X = X AndAlso other.Y = Y
		End Function
		Public Shared Operator +(pt As RDPointI, sz As RDSizeI) As RDPointI
			Return Add(pt, sz)
		End Operator
		Public Shared Operator -(pt As RDPointI, sz As RDSizeI) As RDPointI
			Return Subtract(pt, sz)
		End Operator
		Public Shared Operator =(left As RDPointI, right As RDPointI) As Boolean
			Return left.Equals(right)
		End Operator
		Public Shared Operator <>(left As RDPointI, right As RDPointI) As Boolean
			Return Not left.Equals(right)
		End Operator
		Public Shared Widening Operator CType(p As RDPointI) As RDPoint
			Return New RDPoint(p.X, p.Y)
		End Operator
		Public Shared Narrowing Operator CType(p As RDPointI) As RDSizeI
			Return New RDSizeI(p.X, p.Y)
		End Operator
	End Structure
	Public Structure RDPoint
		Implements IEquatable(Of RDPoint)
		Public Sub New(sz As RDSize)
			X = sz.Width
			Y = sz.Height
		End Sub
		Public Sub New(x As Single?, y As Single?)
			_X = x
			_Y = y
		End Sub
		Public ReadOnly Property IsEmpty As Boolean
			Get
				Return X Is Nothing AndAlso Y Is Nothing
			End Get
		End Property
		Public Property X As Single?
		Public Property Y As Single?
		Public Sub Offset(p As RDPoint)
			X += p.X
			Y += p.Y
		End Sub
		Public Sub Offset(dx As Single?, dy As Single?)
			X += dx
			Y += dy
		End Sub
		Public Shared Function Add(pt As RDPoint, sz As RDSizeI) As RDPoint
			Return New RDPoint(pt.X + sz.Width, pt.Y + sz.Height)
		End Function
		Public Shared Function Add(pt As RDPoint, sz As RDSize) As RDPoint
			Return New RDPoint(pt.X + sz.Width, pt.Y + sz.Height)
		End Function
		Public Shared Function Subtract(pt As RDPoint, sz As RDSizeI) As RDPoint
			Return New RDPoint(pt.X - sz.Width, pt.Y - sz.Height)
		End Function
		Public Shared Function Subtract(pt As RDPoint, sz As RDSize) As RDPoint
			Return New RDPoint(pt.X - sz.Width, pt.Y - sz.Height)
		End Function
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDPoint) AndAlso Equals(CType(obj, RDPoint))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim h As New HashCode
			h.Add(X)
			h.Add(Y)
			Return h.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return $"[{X}, {Y}]"
		End Function
		Private Overloads Function Equals(other As RDPoint) As Boolean Implements IEquatable(Of RDPoint).Equals
			Return other.X = X AndAlso other.Y = Y
		End Function
		Public Shared Operator +(pt As RDPoint, sz As RDSizeI) As RDPoint
			Return Add(pt, sz)
		End Operator
		Public Shared Operator +(pt As RDPoint, sz As RDSize) As RDPoint
			Return Add(pt, sz)
		End Operator
		Public Shared Operator -(pt As RDPoint, sz As RDSizeI) As RDPoint
			Return Subtract(pt, sz)
		End Operator
		Public Shared Operator -(pt As RDPoint, sz As RDSize) As RDPoint
			Return Subtract(pt, sz)
		End Operator
		Public Shared Operator =(left As RDPoint, right As RDPoint) As Boolean
			Return left.Equals(right)
		End Operator
		Public Shared Operator <>(left As RDPoint, right As RDPoint) As Boolean
			Return Not left.Equals(right)
		End Operator
	End Structure
	Public Structure RDSizeI
		Implements IEquatable(Of RDSizeI)
		Public Sub New(pt As RDPointI)
			Width = pt.X
			Height = pt.Y
		End Sub
		Public Sub New(width As Integer?, height As Integer?)
			_Width = width
			_Height = height
		End Sub
		Public ReadOnly Property IsEmpty As Boolean
			Get
				Return Width Is Nothing AndAlso Height Is Nothing
			End Get
		End Property
		Public Property Width As Integer?
		Public Property Height As Integer?
		Public Shared Function Add(sz1 As RDSizeI, sz2 As RDSizeI) As RDSizeI
			Return New RDSizeI(sz1.Width + sz2.Width, sz1.Height + sz2.Height)
		End Function
		Public Shared Function Truncate(value As RDSize) As RDSizeI
			Return New RDSizeI(If(value.Width Is Nothing, Nothing, Math.Truncate(value.Width.Value)), If(value.Height Is Nothing, Nothing, Math.Truncate(value.Height.Value)))
		End Function
		Public Shared Function Subtract(sz1 As RDSizeI, sz2 As RDSizeI) As RDSizeI
			Return New RDSizeI(sz1.Width - sz2.Width, sz1.Height - sz2.Height)
		End Function
		Public Shared Function Ceiling(value As RDSize) As RDSizeI
			Return New RDSizeI(If(value.Width Is Nothing, Nothing, Math.Ceiling(value.Width.Value)), If(value.Height Is Nothing, Nothing, Math.Ceiling(value.Height.Value)))
		End Function
		Public Shared Function Round(value As RDSize) As RDSizeI
			Return New RDSizeI(If(value.Width Is Nothing, Nothing, Math.Round(value.Width.Value)), If(value.Height Is Nothing, Nothing, Math.Round(value.Height.Value)))
		End Function
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDSizeI) AndAlso Equals(CType(obj, RDSizeI))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim h As New HashCode
			h.Add(Width)
			h.Add(Height)
			Return h.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return $"[{Width}, {Height}]"
		End Function
		Public Overloads Function Equals(other As RDSizeI) As Boolean Implements IEquatable(Of RDSizeI).Equals
			Return Width = other.Width AndAlso Height = other.Height
		End Function

		Public Shared Operator +(sz1 As RDSizeI, sz2 As RDSizeI) As RDSizeI
			Return Add(sz1, sz2)
		End Operator
		Public Shared Operator -(sz1 As RDSizeI, sz2 As RDSizeI) As RDSizeI
			Return Subtract(sz1, sz2)
		End Operator
		Public Shared Operator *(left As Single, right As RDSizeI) As RDSize
			Return New RDSize(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator *(left As RDSizeI, right As Single) As RDSize
			Return New RDSize(left.Width * right, left.Height * right)
		End Operator
		Public Shared Operator *(left As RDSizeI, right As Integer) As RDSizeI
			Return New RDSizeI(left.Width * right, left.Height * right)
		End Operator
		Public Shared Operator *(left As Integer, right As RDSizeI) As RDSizeI
			Return New RDSizeI(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator /(left As RDSizeI, right As Single) As RDSize
			Return New RDSize(left.Width / right, left.Height / right)
		End Operator
		Public Shared Operator /(left As RDSizeI, right As Integer) As RDSizeI
			Return New RDSizeI(left.Width / right, left.Height / right)
		End Operator
		Public Shared Operator =(sz1 As RDSizeI, sz2 As RDSizeI) As Boolean
			Return sz1.Equals(sz2)
		End Operator
		Public Shared Operator <>(sz1 As RDSizeI, sz2 As RDSizeI) As Boolean
			Return Not sz1.Equals(sz2)
		End Operator
		Public Shared Narrowing Operator CType(size As RDSizeI) As RDPointI
			Return New RDPointI(size.Width, size.Height)
		End Operator
		Public Shared Widening Operator CType(p As RDSizeI) As RDSize
			Return New RDSize(p.Width, p.Height)
		End Operator
	End Structure
	Public Structure RDSize
		Implements IEquatable(Of RDSize)
		Public Sub New(pt As RDPoint)
			Width = pt.X
			Height = pt.Y
		End Sub
		Public Sub New(width As Single?, height As Single?)
			_Width = width
			_Height = height
		End Sub
		Public ReadOnly Property IsEmpty As Boolean
			Get
				Return Width Is Nothing AndAlso Height Is Nothing
			End Get
		End Property
		Public Property Width As Single?
		Public Property Height As Single?
		Public Shared Function Add(sz1 As RDSize, sz2 As RDSize) As RDSize
			Return New RDSize(sz1.Width + sz2.Width, sz1.Height + sz2.Height)
		End Function
		Public Shared Function Subtract(sz1 As RDSize, sz2 As RDSize) As RDSize
			Return New RDSize(sz1.Width - sz2.Width, sz1.Height - sz2.Height)
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim h As New HashCode
			h.Add(Width)
			h.Add(Height)
			Return h.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return $"[{Width}, {Height}]"
		End Function
		Public Overloads Function Equals(other As RDSize) As Boolean Implements IEquatable(Of RDSize).Equals
			Return Width = other.Width AndAlso Height = other.Height
		End Function
		Public Function ToSize() As RDSizeI
			Return New RDSizeI(Width, Height)
		End Function
		Public Function ToPointF() As RDPoint
			Return New RDPoint(Width, Height)
		End Function
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDSize) AndAlso Equals(CType(obj, RDSize))
		End Function
		Public Shared Operator +(sz1 As RDSize, sz2 As RDSize) As RDSize
			Return Add(sz1, sz2)
		End Operator
		Public Shared Operator -(sz1 As RDSize, sz2 As RDSize) As RDSize
			Return Subtract(sz1, sz2)
		End Operator
		Public Shared Operator *(left As Single, right As RDSize) As RDSize
			Return New RDSize(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator *(left As RDSize, right As Single) As RDSize
			Return New RDSize(left.Width * right, left.Height * right)
		End Operator
		Public Shared Operator /(left As RDSize, right As Single) As RDSize
			Return New RDSize(left.Width / right, left.Height / right)
		End Operator
		Public Shared Operator =(sz1 As RDSize, sz2 As RDSize) As Boolean
			Return sz1.Equals(sz2)
		End Operator
		Public Shared Operator <>(sz1 As RDSize, sz2 As RDSize) As Boolean
			Return Not sz1.Equals(sz2)
		End Operator
		Public Shared Narrowing Operator CType(size As RDSize) As RDPoint
			Return New RDPoint(size.Width, size.Height)
		End Operator
	End Structure
	Public Structure RDExpression
		Implements IEquatable(Of RDExpression)
		Private ReadOnly _exp As String
		Public ReadOnly IsNumeric As Boolean
		Public ReadOnly Property NumericValue As Single
		Public ReadOnly Property ExpressionValue As String
			Get
				If IsNumeric Then
					Return NumericValue.ToString
				Else
					Return _exp
				End If
			End Get
		End Property
		Public ReadOnly Property Expression As Func(Of Variables, Single)
			Get
				Return GetFunctionalExpression(Of Single)(ExpressionValue)
			End Get
		End Property
		Public Sub New(value As Single)
			IsNumeric = True
			NumericValue = value
		End Sub
		Public Sub New(value As String)
			Try
				GetExpression(value, Linq.Expressions.Expression.Parameter(GetType(Variables), "v"))
			Catch ex As Exception
				Throw New Exceptions.ExpressionException($"Illegal Expression: {value}.")
			End Try
			IsNumeric = False
			_exp = value
		End Sub
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDExpression) AndAlso Equals(CType(obj, RDExpression))
		End Function
		Public Overloads Function Equals(other As RDExpression) As Boolean Implements IEquatable(Of RDExpression).Equals
			Return (IsNumeric = other.IsNumeric AndAlso NumericValue = other.NumericValue) OrElse _exp = other._exp
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim hash As New HashCode
			hash.Add(ExpressionValue)
			Return hash.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return ExpressionValue
		End Function
		Public Shared Operator +(left As RDExpression, right As Single) As RDExpression
			If left.IsNumeric Then
				Return New RDExpression(left.NumericValue + right)
			Else
				Return New RDExpression($"{left.ExpressionValue}+{right}")
			End If
		End Operator
		Public Shared Operator +(left As Single, right As RDExpression) As RDExpression
			If right.IsNumeric Then
				Return New RDExpression(left + right.NumericValue)
			Else
				Return New RDExpression($"{left}+{right.ExpressionValue}")
			End If
		End Operator
		Public Shared Operator +(left As RDExpression, right As RDExpression) As RDExpression
			If left.IsNumeric AndAlso right.IsNumeric Then
				Return New RDExpression(left.NumericValue + right.NumericValue)
			Else
				Return New RDExpression($"{left.ExpressionValue}+{right.ExpressionValue}")
			End If
		End Operator
		Public Shared Operator -(left As RDExpression, right As Single) As RDExpression
			If left.IsNumeric Then
				Return New RDExpression(left.NumericValue - right)
			Else
				Return New RDExpression($"{left.ExpressionValue}-{right}")
			End If
		End Operator
		Public Shared Operator -(left As Single, right As RDExpression) As RDExpression
			If right.IsNumeric Then
				Return New RDExpression(left - right.NumericValue)
			Else
				Return New RDExpression($"{left}-{right.ExpressionValue}")
			End If
		End Operator
		Public Shared Operator -(left As RDExpression, right As RDExpression) As RDExpression
			If left.IsNumeric AndAlso right.IsNumeric Then
				Return New RDExpression(left.NumericValue - right.NumericValue)
			Else
				Return New RDExpression($"{left.ExpressionValue}-{right.ExpressionValue}")
			End If
		End Operator
		Public Shared Operator *(left As RDExpression, right As Single) As RDExpression
			If left.IsNumeric Then
				Return New RDExpression(left.NumericValue * right)
			Else
				Return New RDExpression($"({left.ExpressionValue})*{right}")
			End If
		End Operator
		Public Shared Operator *(left As Single, right As RDExpression) As RDExpression
			If right.IsNumeric Then
				Return New RDExpression(left * right.NumericValue)
			Else
				Return New RDExpression($"{left}*({right.ExpressionValue})")
			End If
		End Operator
		Public Shared Operator *(left As RDExpression, right As RDExpression) As RDExpression
			If left.IsNumeric AndAlso right.IsNumeric Then
				Return New RDExpression(left.NumericValue * right.NumericValue)
			Else
				Return New RDExpression($"({left.ExpressionValue})*({right.ExpressionValue})")
			End If
		End Operator
		Public Shared Operator /(left As RDExpression, right As Single) As RDExpression
			If left.IsNumeric Then
				Return New RDExpression(left.NumericValue / right)
			Else
				Return New RDExpression($"({left.ExpressionValue})/{right}")
			End If
		End Operator
		Public Shared Operator /(left As Single, right As RDExpression) As RDExpression
			If right.IsNumeric Then
				Return New RDExpression(left / right.NumericValue)
			Else
				Return New RDExpression($"{left}/({right.ExpressionValue})")
			End If
		End Operator
		Public Shared Operator /(left As RDExpression, right As RDExpression) As RDExpression
			If left.IsNumeric AndAlso right.IsNumeric Then
				Return New RDExpression(left.NumericValue / right.NumericValue)
			Else
				Return New RDExpression($"({left.ExpressionValue})/({right.ExpressionValue})")
			End If
		End Operator
		Public Shared Operator =(left As RDExpression, right As RDExpression) As Boolean
			Return left.Equals(right)
		End Operator
		Public Shared Operator <>(left As RDExpression, right As RDExpression) As Boolean
			Return Not left = right
		End Operator

	End Structure
	'Public Structure RDPointFExp
	'    Implements IEquatable(Of RDPointFExp)
	'    Public Sub New(sz As RDSizeF)
	'        X = sz.Width
	'        Y = sz.Height
	'    End Sub
	'    Public Sub New(x As Single?, y As Single?)
	'        _X = x
	'        _Y = y
	'    End Sub
	'    Public ReadOnly Property IsEmpty As Boolean
	'        Get
	'            Return X Is Nothing AndAlso Y Is Nothing
	'        End Get
	'    End Property
	'    Public Property X As Single?
	'    Public Property Y As Single?
	'    Public Sub Offset(p As RDPointF)
	'        X += p.X
	'        Y += p.Y
	'    End Sub
	'    Public Sub Offset(dx As Single?, dy As Single?)
	'        X += dx
	'        Y += dy
	'    End Sub
	'    Public Shared Function Add(pt As RDPointF, sz As RDSize) As RDPointF
	'        Return New RDPointF(pt.X + sz.Width, pt.Y + sz.Height)
	'    End Function
	'    Public Shared Function Add(pt As RDPointF, sz As RDSizeF) As RDPointF
	'        Return New RDPointF(pt.X + sz.Width, pt.Y + sz.Height)
	'    End Function
	'    Public Shared Function Subtract(pt As RDPointF, sz As RDSize) As RDPointF
	'        Return New RDPointF(pt.X - sz.Width, pt.Y - sz.Height)
	'    End Function
	'    Public Shared Function Subtract(pt As RDPointF, sz As RDSizeF) As RDPointF
	'        Return New RDPointF(pt.X - sz.Width, pt.Y - sz.Height)
	'    End Function
	'    Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
	'        Return obj.GetType = GetType(RDPointF) AndAlso Equals(CType(obj, RDPointF))
	'    End Function
	'    Public Overrides Function GetHashCode() As Integer
	'        Dim h As New HashCode
	'        h.Add(X)
	'        h.Add(Y)
	'        Return h.ToHashCode
	'    End Function
	'    Public Overrides Function ToString() As String
	'        Return $"[{X}, {Y}]"
	'    End Function
	'    Private Overloads Function Equals(other As RDPointFExp) As Boolean Implements IEquatable(Of RDPointFExp).Equals
	'        Return other.X = X AndAlso other.Y = Y
	'    End Function
	'    Public Shared Operator +(pt As RDPointF, sz As RDSize) As RDPointF
	'        Return Add(pt, sz)
	'    End Operator
	'    Public Shared Operator +(pt As RDPointF, sz As RDSizeF) As RDPointF
	'        Return Add(pt, sz)
	'    End Operator
	'    Public Shared Operator -(pt As RDPointF, sz As RDSize) As RDPointF
	'        Return Subtract(pt, sz)
	'    End Operator
	'    Public Shared Operator -(pt As RDPointF, sz As RDSizeF) As RDPointF
	'        Return Subtract(pt, sz)
	'    End Operator
	'    Public Shared Operator =(left As RDPointF, right As RDPointF) As Boolean
	'        Return left.Equals(right)
	'    End Operator
	'    Public Shared Operator <>(left As RDPointF, right As RDPointF) As Boolean
	'        Return Not left.Equals(right)
	'    End Operator
	'End Structure
End Namespace