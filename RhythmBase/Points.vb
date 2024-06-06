Imports System.Diagnostics.CodeAnalysis
Imports RhythmBase.Expressions
Imports Newtonsoft.Json
#Disable Warning CA1812
Namespace Components
	<JsonConverter(GetType(Converters.RDPointNIConverter))>
	Public Structure RDPointNI
		Implements IEquatable(Of RDPointNI)
		Public Sub New(sz As RDSizeI)
			X = sz.Width
			Y = sz.Height
		End Sub
		Public Sub New(x As Integer, y As Integer)
			_X = x
			_Y = y
		End Sub
		Public Property X As Integer
		Public Property Y As Integer
		Public Sub Offset(p As RDPointNI)
			X += p.X
			Y += p.Y
		End Sub
		Public Sub Offset(dx As Integer, dy As Integer)
			X += dx
			Y += dy
		End Sub
		Public Shared Function Ceiling(value As RDPointN) As RDPointNI
			Return New RDPointNI(Math.Ceiling(value.X), Math.Ceiling(value.Y))
		End Function
		Public Shared Function Add(pt As RDPointNI, sz As RDSizeNI) As RDPointNI
			Return New RDPointNI(pt.X + sz.Width, pt.Y + sz.Height)
		End Function
		Public Shared Function Truncate(value As RDPointN) As RDPointNI
			Return New RDPointNI(Math.Truncate(value.X), Math.Truncate(value.Y))
		End Function
		Public Shared Function Subtract(pt As RDPointNI, sz As RDSizeNI) As RDPointNI
			Return New RDPointNI(pt.X - sz.Width, pt.Y - sz.Height)
		End Function
		Public Shared Function Round(value As RDPointN) As RDPointNI
			Return New RDPointNI(Math.Truncate(value.X), Math.Truncate(value.Y))
		End Function
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDPointNI) AndAlso Equals(CType(obj, RDPointNI))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim h As New HashCode
			h.Add(X)
			h.Add(Y)
			Return h.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return $"{{{X}, {Y}}}"
		End Function
		Private Overloads Function Equals(other As RDPointNI) As Boolean Implements IEquatable(Of RDPointNI).Equals
			Return other.X = X AndAlso other.Y = Y
		End Function
		Public Shared Operator +(pt As RDPointNI, sz As RDSizeNI) As RDPointNI
			Return Add(pt, sz)
		End Operator
		Public Shared Operator -(pt As RDPointNI, sz As RDSizeNI) As RDPointNI
			Return Subtract(pt, sz)
		End Operator
		Public Shared Operator =(left As RDPointNI, right As RDPointNI) As Boolean
			Return left.Equals(right)
		End Operator
		Public Shared Operator <>(left As RDPointNI, right As RDPointNI) As Boolean
			Return Not left.Equals(right)
		End Operator
		Public Shared Widening Operator CType(p As RDPointNI) As RDPointN
			Return New RDPointN(p.X, p.Y)
		End Operator
		Public Shared Narrowing Operator CType(p As RDPointNI) As RDSizeNI
			Return New RDSizeNI(p.X, p.Y)
		End Operator
	End Structure
	<JsonConverter(GetType(Converters.RDPointNConverter))>
	Public Structure RDPointN
		Implements IEquatable(Of RDPointN)
		Public Sub New(sz As RDSizeN)
			X = sz.Width
			Y = sz.Height
		End Sub
		Public Sub New(x As Single, y As Single)
			_X = x
			_Y = y
		End Sub
		Public Property X As Single
		Public Property Y As Single
		Public Sub Offset(p As RDPointN)
			X += p.X
			Y += p.Y
		End Sub
		Public Sub Offset(dx As Single, dy As Single)
			X += dx
			Y += dy
		End Sub
		Public Shared Function Add(pt As RDPointN, sz As RDSizeNI) As RDPointN
			Return New RDPointN(pt.X + sz.Width, pt.Y + sz.Height)
		End Function
		Public Shared Function Add(pt As RDPointN, sz As RDSizeN) As RDPointN
			Return New RDPointN(pt.X + sz.Width, pt.Y + sz.Height)
		End Function
		Public Shared Function Subtract(pt As RDPointN, sz As RDSizeNI) As RDPointN
			Return New RDPointN(pt.X - sz.Width, pt.Y - sz.Height)
		End Function
		Public Shared Function Subtract(pt As RDPointN, sz As RDSizeN) As RDPointN
			Return New RDPointN(pt.X - sz.Width, pt.Y - sz.Height)
		End Function
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDPointN) AndAlso Equals(CType(obj, RDPointN))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim h As New HashCode
			h.Add(X)
			h.Add(Y)
			Return h.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return $"{{{X}, {Y}}}"
		End Function
		Private Overloads Function Equals(other As RDPointN) As Boolean Implements IEquatable(Of RDPointN).Equals
			Return other.X = X AndAlso other.Y = Y
		End Function
		Public Shared Operator +(pt As RDPointN, sz As RDSizeNI) As RDPointN
			Return Add(pt, sz)
		End Operator
		Public Shared Operator +(pt As RDPointN, sz As RDSizeN) As RDPointN
			Return Add(pt, sz)
		End Operator
		Public Shared Operator -(pt As RDPointN, sz As RDSizeNI) As RDPointN
			Return Subtract(pt, sz)
		End Operator
		Public Shared Operator -(pt As RDPointN, sz As RDSizeN) As RDPointN
			Return Subtract(pt, sz)
		End Operator
		Public Shared Operator =(left As RDPointN, right As RDPointN) As Boolean
			Return left.Equals(right)
		End Operator
		Public Shared Operator <>(left As RDPointN, right As RDPointN) As Boolean
			Return Not left.Equals(right)
		End Operator
	End Structure
	<JsonConverter(GetType(Converters.RDSizeNIConverter))>
	Public Structure RDSizeNI
		Implements IEquatable(Of RDSizeNI)
		Public Sub New(pt As RDPointNI)
			Width = pt.X
			Height = pt.Y
		End Sub
		Public Sub New(width As Integer, height As Integer)
			_Width = width
			_Height = height
		End Sub
		Public Property Width As Integer
		Public Property Height As Integer
		Public Shared Function Add(sz1 As RDSizeNI, sz2 As RDSizeNI) As RDSizeNI
			Return New RDSizeNI(sz1.Width + sz2.Width, sz1.Height + sz2.Height)
		End Function
		Public Shared Function Truncate(value As RDSizeN) As RDSizeNI
			Return New RDSizeNI(Math.Truncate(value.Width), Math.Truncate(value.Height))
		End Function
		Public Shared Function Subtract(sz1 As RDSizeNI, sz2 As RDSizeNI) As RDSizeNI
			Return New RDSizeNI(sz1.Width - sz2.Width, sz1.Height - sz2.Height)
		End Function
		Public Shared Function Ceiling(value As RDSizeN) As RDSizeNI
			Return New RDSizeNI(Math.Ceiling(value.Width), Math.Ceiling(value.Height))
		End Function
		Public Shared Function Round(value As RDSizeN) As RDSizeNI
			Return New RDSizeNI(Math.Round(value.Width), Math.Round(value.Height))
		End Function
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDSizeNI) AndAlso Equals(CType(obj, RDSizeNI))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim h As New HashCode
			h.Add(Width)
			h.Add(Height)
			Return h.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return $"{{{Width}, {Height}}}"
		End Function
		Public Overloads Function Equals(other As RDSizeNI) As Boolean Implements IEquatable(Of RDSizeNI).Equals
			Return Width = other.Width AndAlso Height = other.Height
		End Function

		Public Shared Operator +(sz1 As RDSizeNI, sz2 As RDSizeNI) As RDSizeNI
			Return Add(sz1, sz2)
		End Operator
		Public Shared Operator -(sz1 As RDSizeNI, sz2 As RDSizeNI) As RDSizeNI
			Return Subtract(sz1, sz2)
		End Operator
		Public Shared Operator *(left As Single, right As RDSizeNI) As RDSizeN
			Return New RDSizeN(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator *(left As RDSizeNI, right As Single) As RDSizeN
			Return New RDSizeN(left.Width * right, left.Height * right)
		End Operator
		Public Shared Operator *(left As Integer, right As RDSizeNI) As RDSizeNI
			Return New RDSizeNI(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator *(left As RDSizeNI, right As Integer) As RDSizeNI
			Return New RDSizeNI(left.Width * right, left.Height * right)
		End Operator
		Public Shared Operator /(left As RDSizeNI, right As Single) As RDSizeN
			Return New RDSizeN(left.Width / right, left.Height / right)
		End Operator
		Public Shared Operator /(left As RDSizeNI, right As Integer) As RDSizeNI
			Return New RDSizeNI(left.Width / right, left.Height / right)
		End Operator
		Public Shared Operator =(sz1 As RDSizeNI, sz2 As RDSizeNI) As Boolean
			Return sz1.Equals(sz2)
		End Operator
		Public Shared Operator <>(sz1 As RDSizeNI, sz2 As RDSizeNI) As Boolean
			Return Not sz1.Equals(sz2)
		End Operator
		Public Shared Narrowing Operator CType(size As RDSizeNI) As RDPointNI
			Return New RDPointNI(size.Width, size.Height)
		End Operator
		Public Shared Widening Operator CType(p As RDSizeNI) As RDSizeN
			Return New RDSizeN(p.Width, p.Height)
		End Operator
	End Structure

	<JsonConverter(GetType(Converters.RDSizeNConverter))>
	Public Structure RDSizeN
		Implements IEquatable(Of RDSizeN)
		Public Sub New(pt As RDPointN)
			Width = pt.X
			Height = pt.Y
		End Sub
		Public Sub New(width As Single, height As Single)
			_Width = width
			_Height = height
		End Sub
		Public Property Width As Single
		Public Property Height As Single
		Public Shared Function Add(sz1 As RDSizeN, sz2 As RDSizeN) As RDSizeN
			Return New RDSizeN(sz1.Width + sz2.Width, sz1.Height + sz2.Height)
		End Function
		Public Shared Function Subtract(sz1 As RDSizeN, sz2 As RDSizeN) As RDSizeN
			Return New RDSizeN(sz1.Width - sz2.Width, sz1.Height - sz2.Height)
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim h As New HashCode
			h.Add(Width)
			h.Add(Height)
			Return h.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return $"{{{Width}, {Height}}}"
		End Function
		Public Overloads Function Equals(other As RDSizeN) As Boolean Implements IEquatable(Of RDSizeN).Equals
			Return Width = other.Width AndAlso Height = other.Height
		End Function
		Public Function ToSize() As RDSizeNI
			Return New RDSizeNI(Width, Height)
		End Function
		Public Function ToPointF() As RDPointN
			Return New RDPointN(Width, Height)
		End Function
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDSizeN) AndAlso Equals(CType(obj, RDSizeN))
		End Function
		Public Shared Operator +(sz1 As RDSizeN, sz2 As RDSizeN) As RDSizeN
			Return Add(sz1, sz2)
		End Operator
		Public Shared Operator -(sz1 As RDSizeN, sz2 As RDSizeN) As RDSizeN
			Return Subtract(sz1, sz2)
		End Operator
		Public Shared Operator *(left As Single, right As RDSizeN) As RDSizeN
			Return New RDSizeN(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator *(left As RDSizeN, right As Single) As RDSizeN
			Return New RDSizeN(left.Width * right, left.Height * right)
		End Operator
		Public Shared Operator /(left As RDSizeN, right As Single) As RDSizeN
			Return New RDSizeN(left.Width / right, left.Height / right)
		End Operator
		Public Shared Operator =(sz1 As RDSizeN, sz2 As RDSizeN) As Boolean
			Return sz1.Equals(sz2)
		End Operator
		Public Shared Operator <>(sz1 As RDSizeN, sz2 As RDSizeN) As Boolean
			Return Not sz1.Equals(sz2)
		End Operator
		Public Shared Narrowing Operator CType(size As RDSizeN) As RDPointN
			Return New RDPointN(size.Width, size.Height)
		End Operator
	End Structure
	<JsonConverter(GetType(Converters.RDPointIConverter))>
	Public Structure RDPointI
		Implements IEquatable(Of RDPointI)
		Public Sub New(sz As RDSizeI)
			X = sz.Width
			Y = sz.Height
		End Sub
		Public Sub New(sz As RDSizeN)
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
			Return $"{{{If(X, "null")}, {If(Y, "null")}}}"
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
	<JsonConverter(GetType(Converters.RDPointConverter))>
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
			Return $"{{{If(X, "null")}, {If(Y, "null")}}}"
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
	<JsonConverter(GetType(Converters.RDSizeIConverter))>
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
			Return $"{{{If(Width, "null")}, {If(Height, "null")}}}"
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
		Public Shared Operator *(left As Integer, right As RDSizeI) As RDSizeI
			Return New RDSizeI(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator *(left As RDSizeI, right As Integer) As RDSizeI
			Return New RDSizeI(left.Width * right, left.Height * right)
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

	<JsonConverter(GetType(Converters.RDSizeConverter))>
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
			Return $"{{{If(Width, "null")}, {If(Height, "null")}}}"
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
	<JsonConverter(GetType(Converters.RDExpressionConverter))>
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
				Throw New NotImplementedException
				Return GetFunctionalExpression(Of Single)(ExpressionValue)
			End Get
		End Property
		Public Sub New(value As Single)
			IsNumeric = True
			NumericValue = value
		End Sub
		Public Sub New(value As String)
			Dim numeric As Single
			If Single.TryParse(value, numeric) Then
				IsNumeric = True
				NumericValue = numeric
				Return
			End If
			'Try
			'	GetExpression(value, System.Linq.Expressions.Expression.Parameter(GetType(Variables), "v"))
			'Catch ex As Exception
			'	Throw New Exceptions.ExpressionException($"Illegal Expression: {{{value}}}.")
			'End Try
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
		Public Shared Function Nullable(s As String) As RDExpression?
			If s?.Length Then
				Return New RDExpression(s)
			End If
			Return Nothing
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
		Public Shared Widening Operator CType(v As Single) As RDExpression
			Return New RDExpression(v)
		End Operator
		Public Shared Widening Operator CType(v As String) As RDExpression
			Return New RDExpression(v)
		End Operator
	End Structure
	<JsonConverter(GetType(Converters.RDPointEConverter))>
	Public Structure RDPointE
		Implements IEquatable(Of RDPointE)
		Public Sub New(sz As RDSize)
			X = sz.Width
			Y = sz.Height
		End Sub
		Public Sub New(x As Single, y As Single)
			_X = x
			_Y = y
		End Sub
		Public Sub New(x As RDExpression?, y As Single)
			_X = x
			_Y = y
		End Sub
		Public Sub New(x As Single, y As RDExpression?)
			_X = x
			_Y = y
		End Sub
		Public Sub New(x As RDExpression?, y As RDExpression?)
			_X = x
			_Y = y
		End Sub
		Public Sub New(x As String, y As Single)
			_X = RDExpression.Nullable(x)
			_Y = y
		End Sub
		Public Sub New(x As Single, y As String)
			_X = x
			_Y = RDExpression.Nullable(y)
		End Sub
		Public Sub New(x As String, y As RDExpression?)
			_X = RDExpression.Nullable(x)
			_Y = y
		End Sub
		Public Sub New(x As RDExpression?, y As String)
			_X = x
			If y?.Length Then
				_Y = y
			End If
		End Sub
		Public Sub New(x As String, y As String)
			_X = RDExpression.Nullable(x)
			_Y = RDExpression.Nullable(y)
		End Sub
		Public Sub New(p As RDPointI)
			_X = p.X
			_Y = p.Y
		End Sub
		Public Sub New(p As RDPoint)
			_X = p.X
			_Y = p.Y
		End Sub
		Public ReadOnly Property IsEmpty As Boolean
			Get
				Return X Is Nothing AndAlso Y Is Nothing
			End Get
		End Property
		Public Property X As RDExpression?
		Public Property Y As RDExpression?
		Public Sub Offset(p As RDPoint)
			X += p.X
			Y += p.Y
		End Sub
		Public Sub Offset(dx As Single?, dy As Single?)
			X += dx
			Y += dy
		End Sub
		Public Shared Function Add(pt As RDPointE, sz As RDSizeI) As RDPointE
			Dim x = pt.X + sz.Width
			Return New RDPointE(pt.X + sz.Width, pt.Y + sz.Height)
		End Function
		Public Shared Function Add(pt As RDPointE, sz As RDSize) As RDPointE
			Return New RDPointE(pt.X + sz.Width, pt.Y + sz.Height)
		End Function
		Public Shared Function Add(pt As RDPointE, sz As RDSizeE) As RDPointE
			Return New RDPointE(pt.X + sz.Width, pt.Y + sz.Height)
		End Function
		Public Shared Function Subtract(pt As RDPointE, sz As RDSizeI) As RDPointE
			Return New RDPointE(pt.X - sz.Width, pt.Y - sz.Height)
		End Function
		Public Shared Function Subtract(pt As RDPointE, sz As RDSize) As RDPointE
			Return New RDPointE(pt.X - sz.Width, pt.Y - sz.Height)
		End Function
		Public Shared Function Subtract(pt As RDPointE, sz As RDSizeE) As RDPointE
			Return New RDPointE(pt.X - sz.Width, pt.Y - sz.Height)
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
			Return $"{{{If(X?.ExpressionValue, "null")}, {If(Y?.ExpressionValue, "null")}}}"
		End Function
		Private Overloads Function Equals(other As RDPointE) As Boolean Implements IEquatable(Of RDPointE).Equals
			Return other.X = X AndAlso other.Y = Y
		End Function
		Public Function MultipyByMatrix(matrix(,) As RDExpression?) As RDPointE
			If matrix.Rank = 2 AndAlso matrix.Length = 4 Then
				Return New RDPointE(
X * matrix(0, 0) + Y * matrix(1, 0),
X * matrix(0, 1) + Y * matrix(1, 1))
			End If
			Throw New Exception("Matrix not match.")
		End Function
		Public Function Rotate(angle As Single) As RDPointE
			Return MultipyByMatrix(
{
{CSng(Math.Cos(angle)), CSng(Math.Sin(angle))},
{CSng(-Math.Sin(angle)), CSng(Math.Cos(angle))}
})
		End Function
		Public Function Rotate(pivot As RDPoint, angle As Single) As RDPointE
			Return (Me - New RDSizeE(pivot)).Rotate(angle) + New RDSizeE(pivot)
		End Function

		Public Shared Operator +(pt As RDPointE, sz As RDSizeI) As RDPointE
			Return Add(pt, sz)
		End Operator
		Public Shared Operator +(pt As RDPointE, sz As RDSize) As RDPointE
			Return Add(pt, sz)
		End Operator
		Public Shared Operator +(pt As RDPointE, sz As RDSizeE) As RDPointE
			Return Add(pt, sz)
		End Operator
		Public Shared Operator -(pt As RDPointE, sz As RDSizeI) As RDPointE
			Return Subtract(pt, sz)
		End Operator
		Public Shared Operator -(pt As RDPointE, sz As RDSize) As RDPointE
			Return Subtract(pt, sz)
		End Operator
		Public Shared Operator -(pt As RDPointE, sz As RDSizeE) As RDPointE
			Return Subtract(pt, sz)
		End Operator
		Public Shared Operator =(left As RDPointE, right As RDPointE) As Boolean
			Return left.Equals(right)
		End Operator
		Public Shared Operator <>(left As RDPointE, right As RDPointE) As Boolean
			Return Not left.Equals(right)
		End Operator
		Public Shared Widening Operator CType(v As RDPointI) As RDPointE
			Return New RDPointE(v)
		End Operator
		Public Shared Widening Operator CType(v As RDPoint) As RDPointE
			Return New RDPointE(v)
		End Operator
	End Structure
	<JsonConverter(GetType(Converters.RDSizeEConverter))>
	Public Structure RDSizeE
		Implements IEquatable(Of RDSizeE)
		Public Sub New(width As Single, height As Single)
			_Width = width
			_Height = height
		End Sub
		Public Sub New(width As RDExpression?, height As Single)
			_Width = width
			_Height = height
		End Sub
		Public Sub New(width As Single, height As RDExpression?)
			_Width = width
			_Height = height
		End Sub
		Public Sub New(width As RDExpression?, height As RDExpression?)
			_Width = width
			_Height = height
		End Sub
		Public Sub New(width As String, height As Single)
			_Width = RDExpression.Nullable(width)
			_Height = height
		End Sub
		Public Sub New(width As Single, height As String)
			_Width = width
			_Height = RDExpression.Nullable(height)
		End Sub
		Public Sub New(width As String, height As String)
			_Width = RDExpression.Nullable(width)
			_Height = RDExpression.Nullable(height)
		End Sub
		Public Sub New(width As String, height As RDExpression?)
			_Width = RDExpression.Nullable(width)
			_Height = height
		End Sub
		Public Sub New(width As RDExpression?, height As String)
			_Width = width
			_Height = RDExpression.Nullable(height)
		End Sub
		Public Sub New(p As RDSizeI)
			_Width = p.Width
			_Height = p.Height
		End Sub
		Public Sub New(p As RDSize)
			_Width = p.Width
			_Height = p.Height
		End Sub
		Public Sub New(p As RDPointI)
			_Width = p.X
			_Height = p.Y
		End Sub
		Public Sub New(p As RDPoint)
			_Width = p.X
			_Height = p.Y
		End Sub
		Public Sub New(p As RDPointE)
			_Width = p.X
			_Height = p.Y
		End Sub
		Public ReadOnly Property IsEmpty As Boolean
			Get
				Return Width Is Nothing AndAlso Height Is Nothing
			End Get
		End Property
		Public Property Width As RDExpression?
		Public Property Height As RDExpression?
		Public Shared Function Add(sz1 As RDSizeE, sz2 As RDSize) As RDSizeE
			Return New RDSizeE(sz1.Width + sz2.Width, sz1.Height + sz2.Height)
		End Function
		Public Shared Function Add(sz1 As RDSizeE, sz2 As RDSizeE) As RDSizeE
			Return New RDSizeE(sz1.Width + sz2.Width, sz1.Height + sz2.Height)
		End Function
		Public Shared Function Subtract(sz1 As RDSizeE, sz2 As RDSize) As RDSizeE
			Return New RDSizeE(sz1.Width - sz2.Width, sz1.Height - sz2.Height)
		End Function
		Public Shared Function Subtract(sz1 As RDSizeE, sz2 As RDSizeE) As RDSizeE
			Return New RDSizeE(sz1.Width - sz2.Width, sz1.Height - sz2.Height)
		End Function
		Public Overrides Function GetHashCode() As Integer
			Dim h As New HashCode
			h.Add(Width)
			h.Add(Height)
			Return h.ToHashCode
		End Function
		Public Overrides Function ToString() As String
			Return $"{{{If(Width?.ExpressionValue, "null")}, {If(Height?.ExpressionValue, "null")}}}"
		End Function
		Public Overloads Function Equals(other As RDSizeE) As Boolean Implements IEquatable(Of RDSizeE).Equals
			Return Width = other.Width AndAlso Height = other.Height
		End Function
		Public Function ToRDPointE() As RDPointE
			Return New RDPointE(Width, Height)
		End Function
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDSize) AndAlso Equals(CType(obj, RDSize))
		End Function
		Public Shared Operator +(sz1 As RDSizeE, sz2 As RDSizeI) As RDSizeE
			Return Add(sz1, sz2)
		End Operator
		Public Shared Operator +(sz1 As RDSizeE, sz2 As RDSize) As RDSizeE
			Return Add(sz1, sz2)
		End Operator
		Public Shared Operator +(sz1 As RDSizeE, sz2 As RDSizeE) As RDSizeE
			Return Add(sz1, sz2)
		End Operator
		Public Shared Operator -(sz1 As RDSizeE, sz2 As RDSizeI) As RDSizeE
			Return Subtract(sz1, sz2)
		End Operator
		Public Shared Operator -(sz1 As RDSizeE, sz2 As RDSize) As RDSizeE
			Return Subtract(sz1, sz2)
		End Operator
		Public Shared Operator -(sz1 As RDSizeE, sz2 As RDSizeE) As RDSizeE
			Return Subtract(sz1, sz2)
		End Operator
		Public Shared Operator *(left As Integer, right As RDSizeE) As RDSizeE
			Return New RDSizeE(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator *(left As RDSizeE, right As Integer) As RDSizeE
			Return New RDSizeE(left.Width * right, left.Height * right)
		End Operator
		Public Shared Operator *(left As Single, right As RDSizeE) As RDSizeE
			Return New RDSizeE(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator *(left As RDSizeE, right As Single) As RDSizeE
			Return New RDSizeE(left.Width * right, left.Height * right)
		End Operator
		Public Shared Operator *(left As RDExpression, right As RDSizeE) As RDSizeE
			Return New RDSizeE(left * right.Width, left * right.Height)
		End Operator
		Public Shared Operator *(left As RDSizeE, right As RDExpression) As RDSizeE
			Return New RDSizeE(left.Width * right, left.Height * right)
		End Operator
		Public Shared Operator /(left As RDSizeE, right As Single) As RDSizeE
			Return New RDSizeE(left.Width / right, left.Height / right)
		End Operator
		Public Shared Operator /(left As RDSizeE, right As RDExpression) As RDSizeE
			Return New RDSizeE(left.Width / right, left.Height / right)
		End Operator
		Public Shared Operator =(sz1 As RDSizeE, sz2 As RDSizeE) As Boolean
			Return sz1.Equals(sz2)
		End Operator
		Public Shared Operator <>(sz1 As RDSizeE, sz2 As RDSizeE) As Boolean
			Return Not sz1.Equals(sz2)
		End Operator
		Public Shared Narrowing Operator CType(size As RDSizeE) As RDPointE
			Return New RDPointE(size.Width, size.Height)
		End Operator
		Public Shared Widening Operator CType(v As RDSizeI) As RDSizeE
			Return New RDSizeE(v)
		End Operator
		Public Shared Widening Operator CType(v As RDSize) As RDSizeE
			Return New RDSizeE(v)
		End Operator
	End Structure
End Namespace