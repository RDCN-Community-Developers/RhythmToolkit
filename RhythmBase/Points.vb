Imports System.Diagnostics.CodeAnalysis
Imports Newtonsoft.Json
#Disable Warning CA1812
Namespace Components
	''' <summary>
	''' A point whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="integer"/>
	''' </summary>
	<JsonConverter(GetType(RDPointsConverter))>
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
		Public Function MultipyByMatrix(matrix(,) As Single) As RDPoint
			If matrix.Rank = 2 AndAlso matrix.Length = 4 Then
				Return New RDPoint(
					X * matrix(0, 0) + Y * matrix(1, 0),
					X * matrix(0, 1) + Y * matrix(1, 1))
			End If
			Throw New Exception("Matrix not match, 2*2 matrix expected.")
		End Function
		''' <summary>
		''' Rotate.
		''' </summary>
		Public Function Rotate(angle As Single) As RDPoint
			Return MultipyByMatrix(
			{
			{CSng(Math.Cos(angle)), CSng(Math.Sin(angle))},
			{CSng(-Math.Sin(angle)), CSng(Math.Cos(angle))}
			})
		End Function
		''' <summary>
		''' Rotate at a given pivot.
		''' </summary>
		''' <param name="pivot">Giver pivot.</param>
		''' <returns></returns>
		Public Function Rotate(pivot As RDPointN, angle As Single) As RDPointN
			Return (CType(Me, RDPointN) - New RDSizeN(pivot)).Rotate(angle) + New RDSizeN(pivot)
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
			Return $"[{X}, {Y}]"
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
		Public Shared Widening Operator CType(p As RDPointNI) As RDPointI
			Return New RDPointI(p.X, p.Y)
		End Operator
		Public Shared Widening Operator CType(p As RDPointNI) As RDPointE
			Return New RDPointE(p.X, p.Y)
		End Operator
		Public Shared Narrowing Operator CType(p As RDPointNI) As RDSizeNI
			Return New RDSizeNI(p.X, p.Y)
		End Operator
	End Structure
	''' <summary>
	''' A point whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="float"/>
	''' </summary>
	<JsonConverter(GetType(RDPointsConverter))>
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
		Public Sub Offset(p As RDSizeN)
			X += p.Width
			Y += p.Height
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
		Public Function MultipyByMatrix(matrix(,) As Single) As RDPointN
			If matrix.Rank = 2 AndAlso matrix.Length = 4 Then
				Return New RDPointN(
X * matrix(0, 0) + Y * matrix(1, 0),
X * matrix(0, 1) + Y * matrix(1, 1))
			End If
			Throw New Exception("Matrix not match, 2*2 matrix expected.")
		End Function
		''' <summary>
		''' Rotate.
		''' </summary>
		Public Function Rotate(angle As Single) As RDPointN
			Return MultipyByMatrix(
			{
			{CSng(Math.Cos(angle)), CSng(Math.Sin(angle))},
			{CSng(-Math.Sin(angle)), CSng(Math.Cos(angle))}
			})
		End Function
		''' <summary>
		''' Rotate at a given pivot.
		''' </summary>
		''' <param name="pivot">Giver pivot.</param>
		''' <returns></returns>
		Public Function Rotate(pivot As RDPointN, angle As Single) As RDPointN
			Return (Me - New RDSizeN(pivot)).Rotate(angle) + New RDSizeN(pivot)
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
			Return $"[{X}, {Y}]"
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
		Public Shared Widening Operator CType(p As RDPointN) As RDPoint
			Return New RDPoint(p.X, p.Y)
		End Operator
		Public Shared Widening Operator CType(p As RDPointN) As RDPointE
			Return New RDPointE(p.X, p.Y)
		End Operator
		Public Shared Narrowing Operator CType(p As RDPointN) As RDSizeN
			Return New RDSizeN(p.X, p.Y)
		End Operator
	End Structure
	''' <summary>
	''' A size whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="integer"/>
	''' </summary>
	<JsonConverter(GetType(RDPointsConverter))>
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
		Public ReadOnly Property Area As Integer
			Get
				Return Width * Height
			End Get
		End Property
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
			Return $"[{Width}, {Height}]"
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
		Public Shared Widening Operator CType(p As RDSizeNI) As RDSizeN
			Return New RDSizeN(p.Width, p.Height)
		End Operator
		Public Shared Widening Operator CType(p As RDSizeNI) As RDSizeI
			Return New RDSizeI(p.Width, p.Height)
		End Operator
		Public Shared Widening Operator CType(p As RDSizeNI) As RDSizeE
			Return New RDSizeE(p.Width, p.Height)
		End Operator
		Public Shared Narrowing Operator CType(size As RDSizeNI) As RDPointNI
			Return New RDPointNI(size.Width, size.Height)
		End Operator
	End Structure
	''' <summary>
	''' A size whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="float"/>
	''' </summary>
	<JsonConverter(GetType(RDPointsConverter))>
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
		Public ReadOnly Property Area As Single
			Get
				Return Width * Height
			End Get
		End Property
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
			Return $"[{Width}, {Height}]"
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
		Public Shared Widening Operator CType(size As RDSizeN) As RDSize
			Return New RDSize(size.Width, size.Height)
		End Operator
		Public Shared Widening Operator CType(size As RDSizeN) As RDSizeE
			Return New RDSizeE(size.Width, size.Height)
		End Operator
		Public Shared Narrowing Operator CType(size As RDSizeN) As RDPointN
			Return New RDPointN(size.Width, size.Height)
		End Operator
	End Structure
	Public Structure RDRectNI
		Implements IEquatable(Of RDRectNI)
		Public Property Left As Integer
		Public Property Right As Integer
		Public Property Top As Integer
		Public Property Bottom As Integer
		Public ReadOnly Property Width As Integer
			Get
				Return Right - Left
			End Get
		End Property
		Public ReadOnly Property Height As Integer
			Get
				Return Top - Bottom
			End Get
		End Property
		Public Sub New(left As Integer, top As Integer, right As Integer, bottom As Integer)
			Me.Left = left
			Me.Right = right
			Me.Top = top
			Me.Bottom = bottom
		End Sub
		Public Sub New(location As RDPointNI, size As RDSizeNI)
			Me.New(location.X,
				   location.Y + size.Height,
				   location.X + size.Width,
				   location.Y)
		End Sub
		Public Sub New(size As RDSizeNI)
			Me.New(0, size.Height, size.Width, 0)
		End Sub
		Public Sub New(width As Integer, height As Integer)
			Me.New(0, height, width, 0)
		End Sub
		Public ReadOnly Property Location As RDPointNI
			Get
				Return New RDPointNI(Left, Bottom)
			End Get
		End Property
		Public ReadOnly Property Size As RDSizeNI
			Get
				Return New RDSizeNI(Width, Height)
			End Get
		End Property
		Public Shared Function Inflate(rect As RDRectNI, size As RDSizeNI) As RDRectNI
			Dim result As New RDRectNI(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(size)
			Return result
		End Function
		Public Shared Function Inflate(rect As RDRectNI, x As Integer, y As Integer) As RDRectNI
			Dim result As New RDRectNI(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(x, y)
			Return result
		End Function
		Public Shared Function Ceiling(rect As RDRectN) As RDRectNI
			Return Ceiling(rect, False)
		End Function
		Public Shared Function Ceiling(rect As RDRectN, outwards As Boolean) As RDRectNI
			Return New RDRectNI(
				If(outwards AndAlso rect.Width > 0, Math.Floor(rect.Left), Math.Ceiling(rect.Left)),
				If(outwards AndAlso rect.Height > 0, Math.Floor(rect.Top), Math.Ceiling(rect.Top)),
				If(outwards AndAlso rect.Width < 0, Math.Floor(rect.Right), Math.Ceiling(rect.Right)),
				If(outwards AndAlso rect.Height < 0, Math.Floor(rect.Bottom), Math.Ceiling(rect.Bottom))
				)
		End Function
		Public Shared Function Floor(rect As RDRectN) As RDRectNI
			Return Ceiling(rect, False)
		End Function
		Public Shared Function Floor(rect As RDRectN, inwards As Boolean) As RDRectNI
			Return New RDRectNI(
				If(inwards AndAlso rect.Width > 0, Math.Ceiling(rect.Left), Math.Floor(rect.Left)),
				If(inwards AndAlso rect.Height > 0, Math.Ceiling(rect.Top), Math.Floor(rect.Top)),
				If(inwards AndAlso rect.Width < 0, Math.Ceiling(rect.Right), Math.Floor(rect.Right)),
				If(inwards AndAlso rect.Height < 0, Math.Ceiling(rect.Bottom), Math.Floor(rect.Bottom))
				)
		End Function
		Public Shared Function Round(rect As RDRectN) As RDRectNI
			Return New RDRectNI(
				Math.Round(rect.Left),
				Math.Round(rect.Top),
				Math.Round(rect.Right),
				Math.Round(rect.Bottom)
				)
		End Function
		Public Shared Function Union(rect1 As RDRectNI, rect2 As RDRectNI) As RDRectNI
			Return New RDRectNI(
				Math.Min(rect1.Left, rect2.Left),
				Math.Max(rect1.Top, rect2.Top),
				Math.Max(rect1.Right, rect2.Right),
				Math.Min(rect1.Bottom, rect2.Bottom)
				)
		End Function
		Public Shared Function Intersect(rect1 As RDRectNI, rect2 As RDRectNI) As RDRectNI
			Return If(rect1.IntersectsWithInclusive(rect2),
				New RDRectNI(
					Math.Max(rect1.Left, rect2.Left),
					Math.Max(rect1.Top, rect2.Top),
					Math.Min(rect1.Right, rect2.Right),
					Math.Min(rect1.Bottom, rect2.Bottom)),
				New RDRectNI)
		End Function
		Public Shared Function Truncate(rect As RDRectN) As RDRectNI
			Return New RDRectNI(
				rect.Left,
				rect.Top,
				rect.Right,
				rect.Bottom
				)
		End Function
		Public Sub Offset(x As Integer, y As Integer)
			Left += x
			Top += y
			Right += x
			Bottom += y
		End Sub
		Public Sub Offset(p As RDPointNI)
			Offset(p.X, p.Y)
		End Sub
		Public Sub Inflate(size As RDSizeNI)
			Left -= size.Width
			Top += size.Height
			Right += size.Width
			Bottom -= size.Height
		End Sub
		Public Sub Inflate(width As Integer, height As Integer)
			Left -= width
			Top += height
			Right += width
			Bottom -= height
		End Sub
		Public Function Contains(x As Integer, y As Integer) As Boolean
			Return Left < x AndAlso x < Right AndAlso Bottom < y AndAlso y < Top
		End Function
		Public Function Contains(p As RDPointN) As Boolean
			Return Left < p.X AndAlso p.X < Right AndAlso Bottom < p.Y AndAlso p.Y < Top
		End Function
		Public Function Contains(rect As RDRectNI) As Boolean
			Return Left < rect.Left AndAlso rect.Right < Right AndAlso Bottom < rect.Bottom AndAlso rect.Top < Top
		End Function
		Public Function Union(rect As RDRectNI) As RDRectNI
			Return Union(Me, rect)
		End Function
		Public Function Intersect(rect As RDRectNI)
			Return Intersect(Me, rect)
		End Function
		Public Function IntersectsWith(rect As RDRectNI) As Boolean
			Return Left < rect.Right AndAlso Right > rect.Left AndAlso Top < rect.Bottom AndAlso Bottom > rect.Top
		End Function
		Public Function IntersectsWithInclusive(rect As RDRectNI) As Boolean
			Return Left <= rect.Right AndAlso Right >= rect.Left AndAlso Top <= rect.Bottom AndAlso Bottom >= rect.Top
		End Function
		Public Shared Operator =(rect1 As RDRectNI, rect2 As RDRectNI) As Boolean
			Return rect1.Equals(rect2)
		End Operator
		Public Shared Operator <>(rect1 As RDRectNI, rect2 As RDRectNI) As Boolean
			Return Not rect1.Equals(rect2)
		End Operator
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDRectNI) AndAlso Equals(CType(obj, RDRectNI))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(Left, Top, Right, Bottom)
		End Function
		Public Overrides Function ToString() As String
			Return $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}"
		End Function
		Public Overloads Function Equals(other As RDRectNI) As Boolean Implements IEquatable(Of RDRectNI).Equals
			Return Left = other.Left AndAlso Top = other.Top AndAlso Right = other.Right AndAlso Bottom = other.Bottom
		End Function
		Public Shared Widening Operator CType(rect As RDRectNI) As RDRectN
			Return New RDRectN(rect.Left, rect.Top, rect.Right, rect.Bottom)
		End Operator
		Public Shared Widening Operator CType(rect As RDRectNI) As RDRectI
			Return New RDRectI(rect.Left, rect.Top, rect.Right, rect.Bottom)
		End Operator
		Public Shared Widening Operator CType(rect As RDRectNI) As RDRectE
			Return New RDRectE(rect.Left, rect.Top, rect.Right, rect.Bottom)
		End Operator
	End Structure
	Public Structure RDRotatedRectNI
		Implements IEquatable(Of RDRotatedRectNI)
		Public Property Location As RDPointNI
		Public Property Size As RDSizeNI
		Public Property Pivot As RDPointNI
		''' <summary>
		''' Radius angle value
		''' </summary>
		''' <returns></returns>
		Public Property Angle As Single
		Public ReadOnly Property LeftTop As RDPointN
			Get
				Return (Location - Pivot + New RDSizeNI(0, Size.Height)).Rotate(Location, Angle)
			End Get
		End Property
		Public ReadOnly Property RightTop As RDPointN
			Get
				Return (Location - Pivot + Size).Rotate(Location, Angle)
			End Get
		End Property
		Public ReadOnly Property LeftBottom As RDPointN
			Get
				Return (Location - Pivot).Rotate(Location, Angle)
			End Get
		End Property
		Public ReadOnly Property RightBottom As RDPointN
			Get
				Return (Location - Pivot + New RDSizeNI(Size.Width, 0)).Rotate(Location, Angle)
			End Get
		End Property
		Public ReadOnly Property WithoutRotate As RDRectNI
			Get
				Return New RDRectNI(Location - Pivot, Size)
			End Get
		End Property
		Public Sub New(location As RDPointNI, size As RDSizeNI, pivot As RDPointNI, angle As Single)
			Me.Location = location
			Me.Size = size
			Me.Pivot = pivot
			Me.Angle = angle
		End Sub
		Public Sub New(rect As RDRectNI)
			Me.New(rect.Location, rect.Size, New RDPointNI, 0)
		End Sub
		Public Shared Function Inflate(rect As RDRotatedRectNI, size As RDSizeNI) As RDRotatedRectNI
			Dim result = rect
			result.Inflate(size)
			Return result
		End Function
		Public Shared Function Inflate(rect As RDRotatedRectNI, x As Integer, y As Integer) As RDRotatedRectNI
			Dim result = rect
			result.Inflate(x, y)
			Return result
		End Function
		Public Sub Offset(x As Integer, y As Integer)
			Location += New RDPointNI(x, y)
		End Sub
		Public Sub Offset(p As RDPointNI)
			Offset(p.X, p.Y)
		End Sub
		Public Sub Inflate(size As RDSizeNI)
			Me.Size += New RDSizeNI(size.Width * 2, size.Height * 2)
			Me.Pivot -= New RDPointNI(size.Width, size.Height)
		End Sub
		Public Sub Inflate(width As Integer, height As Integer)
			Me.Size += New RDSizeNI(width * 2, height * 2)
			Me.Pivot -= New RDPointNI(width, height)
		End Sub
		Public Function Contains(x As Integer, y As Integer) As Boolean
			Return WithoutRotate.Contains(New RDPointN(x, y).Rotate(-Angle))
		End Function
		Public Function Contains(p As RDPointN) As Boolean
			Return WithoutRotate.Contains(p.Rotate(-Angle))
		End Function
		Public Function Contains(rect As RDRotatedRectNI) As Boolean
			Return Contains(rect.LeftTop) AndAlso
				Contains(rect.RightTop) AndAlso
				Contains(rect.LeftBottom) AndAlso
				Contains(rect.RightBottom)
		End Function
		Public Function IntersectsWith(rect As RDRotatedRectNI) As Boolean
			Return Contains(rect.LeftTop) OrElse
				Contains(rect.RightTop) OrElse
				Contains(rect.LeftBottom) OrElse
				Contains(rect.RightBottom)
		End Function
		'Public Function IntersectsWithInclusive(rect As RDRectNIR) As Boolean
		'	Return Left <= rect.Right AndAlso Right >= rect.Left AndAlso Top <= rect.Bottom AndAlso Bottom >= rect.Top
		'End Function
		Public Shared Operator =(rect1 As RDRotatedRectNI, rect2 As RDRotatedRectNI) As Boolean
			Return rect1.Equals(rect2)
		End Operator
		Public Shared Operator <>(rect1 As RDRotatedRectNI, rect2 As RDRotatedRectNI) As Boolean
			Return Not rect1.Equals(rect2)
		End Operator
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDRotatedRectNI) AndAlso Equals(CType(obj, RDRotatedRectNI))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(Location, Size, Pivot, Angle)
		End Function
		Public Overrides Function ToString() As String
			Return $"{{Location={Location},Size={Size},Pivot={Pivot},Angle={Angle}}}"
		End Function
		Public Overloads Function Equals(other As RDRotatedRectNI) As Boolean Implements IEquatable(Of RDRotatedRectNI).Equals
			Return Location = other.Location AndAlso Size = other.Size AndAlso Pivot = other.Pivot AndAlso Angle = other.Angle
		End Function
	End Structure
	Public Structure RDRectN
		Implements IEquatable(Of RDRectN)
		Public Property Left As Single
		Public Property Right As Single
		Public Property Top As Single
		Public Property Bottom As Single
		Public ReadOnly Property Width As Single
			Get
				Return Right - Left
			End Get
		End Property
		Public ReadOnly Property Height As Single
			Get
				Return Top - Bottom
			End Get
		End Property
		Public Sub New(left As Single, top As Single, right As Single, bottom As Single)
			Me.Left = left
			Me.Right = right
			Me.Top = top
			Me.Bottom = bottom
		End Sub
		Public Sub New(location As RDPointN, size As RDSizeN)
			Me.New(location.X,
				   location.Y + size.Height,
				   location.X + size.Width,
				   location.Y)
		End Sub
		Public Sub New(size As RDSizeN)
			Me.New(0, size.Height, size.Width, 0)
		End Sub
		Public Sub New(width As Single, height As Single)
			Me.New(0, height, width, 0)
		End Sub
		Public ReadOnly Property Location As RDPointNI
			Get
				Return New RDPointNI(Left, Bottom)
			End Get
		End Property
		Public ReadOnly Property Size As RDSizeNI
			Get
				Return New RDSizeNI(Width, Height)
			End Get
		End Property
		Public Shared Function Inflate(rect As RDRectN, size As RDSizeNI) As RDRectN
			Dim result As New RDRectN(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(size)
			Return result
		End Function
		Public Shared Function Inflate(rect As RDRectN, x As Single, y As Single) As RDRectN
			Dim result As New RDRectN(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(x, y)
			Return result
		End Function
		Public Shared Function Union(rect1 As RDRectN, rect2 As RDRectN) As RDRectN
			Return New RDRectN(
				Math.Min(rect1.Left, rect2.Left),
				Math.Max(rect1.Top, rect2.Top),
				Math.Max(rect1.Right, rect2.Right),
				Math.Min(rect1.Bottom, rect2.Bottom)
				)
		End Function
		Public Shared Function Intersect(rect1 As RDRectN, rect2 As RDRectN) As RDRectN
			Return If(rect1.IntersectsWithInclusive(rect2),
				New RDRectN(
					Math.Max(rect1.Left, rect2.Left),
					Math.Max(rect1.Top, rect2.Top),
					Math.Min(rect1.Right, rect2.Right),
					Math.Min(rect1.Bottom, rect2.Bottom)),
				New RDRectN)
		End Function
		Public Shared Function Truncate(rect As RDRectN) As RDRectN
			Return New RDRectN(
				rect.Left,
				rect.Top,
				rect.Right,
				rect.Bottom
				)
		End Function
		Public Sub Offset(x As Single, y As Single)
			Left += x
			Top += y
			Right += x
			Bottom += y
		End Sub
		Public Sub Offset(p As RDPointN)
			Offset(p.X, p.Y)
		End Sub
		Public Sub Inflate(size As RDSizeN)
			Left -= size.Width
			Top += size.Height
			Right += size.Width
			Bottom -= size.Height
		End Sub
		Public Sub Inflate(width As Single, height As Single)
			Left -= width
			Top += height
			Right += width
			Bottom -= height
		End Sub
		Public Function Union(rect As RDRectN) As RDRectN
			Return Union(Me, rect)
		End Function
		Public Function IntersectsWithInclusive(rect As RDRectN) As Boolean
			Return Left <= rect.Right AndAlso Right >= rect.Left AndAlso Top <= rect.Bottom AndAlso Bottom >= rect.Top
		End Function
		Public Shared Operator =(rect1 As RDRectN, rect2 As RDRectN) As Boolean
			Return rect1.Equals(rect2)
		End Operator
		Public Shared Operator <>(rect1 As RDRectN, rect2 As RDRectN) As Boolean
			Return Not rect1.Equals(rect2)
		End Operator
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDRectN) AndAlso Equals(CType(obj, RDRectN))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(Left, Top, Right, Bottom)
		End Function
		Public Overrides Function ToString() As String
			Return $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}"
		End Function
		Public Overloads Function Equals(other As RDRectN) As Boolean Implements IEquatable(Of RDRectN).Equals
			Return Left = other.Left AndAlso Top = other.Top AndAlso Right = other.Right AndAlso Bottom = other.Bottom
		End Function
		Public Shared Widening Operator CType(rect As RDRectN) As RDRect
			Return New RDRect(rect.Left, rect.Top, rect.Right, rect.Bottom)
		End Operator
		Public Shared Widening Operator CType(rect As RDRectN) As RDRectE
			Return New RDRectE(rect.Left, rect.Top, rect.Right, rect.Bottom)
		End Operator
	End Structure
	''' <summary>
	''' A point whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="integer"/>
	''' </summary>
	<JsonConverter(GetType(RDPointsConverter))>
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
		Public Function MultipyByMatrix(matrix(,) As Single) As RDPoint
			If matrix.Rank = 2 AndAlso matrix.Length = 4 Then
				Return New RDPoint(
					X * matrix(0, 0) + Y * matrix(1, 0),
					X * matrix(0, 1) + Y * matrix(1, 1))
			End If
			Throw New Exception("Matrix not match, 2*2 matrix expected.")
		End Function
		''' <summary>
		''' Rotate.
		''' </summary>
		Public Function Rotate(angle As Single) As RDPoint
			Return MultipyByMatrix(
			{
				{CSng(Math.Cos(angle)), CSng(Math.Sin(angle))},
				{CSng(-Math.Sin(angle)), CSng(Math.Cos(angle))}
			})
		End Function
		''' <summary>
		''' Rotate at a given pivot.
		''' </summary>
		''' <param name="pivot">Giver pivot.</param>
		''' <returns></returns>
		Public Function Rotate(pivot As RDPointN, angle As Single) As RDPoint
			Return (CType(Me, RDPoint) - New RDSizeN(pivot)).Rotate(angle) + New RDSizeN(pivot)
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
			Return $"[{If(X, "null")}, {If(Y, "null")}]"
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
		Public Shared Widening Operator CType(p As RDPointI) As RDPointE
			Return New RDPointE(p.X, p.Y)
		End Operator
		Public Shared Narrowing Operator CType(p As RDPointI) As RDSizeI
			Return New RDSizeI(p.X, p.Y)
		End Operator
	End Structure
	''' <summary>
	''' A point whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="float"/>
	''' </summary>
	<JsonConverter(GetType(RDPointsConverter))>
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
		Public Function MultipyByMatrix(matrix(,) As Single) As RDPoint
			If matrix.Rank = 2 AndAlso matrix.Length = 4 Then
				Return New RDPoint(
					X * matrix(0, 0) + Y * matrix(1, 0),
					X * matrix(0, 1) + Y * matrix(1, 1))
			End If
			Throw New Exception("Matrix not match, 2*2 matrix expected.")
		End Function
		''' <summary>
		''' Rotate.
		''' </summary>
		Public Function Rotate(angle As Single) As RDPoint
			Return MultipyByMatrix(
			{
			{CSng(Math.Cos(angle)), CSng(Math.Sin(angle))},
			{CSng(-Math.Sin(angle)), CSng(Math.Cos(angle))}
			})
		End Function
		''' <summary>
		''' Rotate at a given pivot.
		''' </summary>
		''' <param name="pivot">Giver pivot.</param>
		''' <returns></returns>
		Public Function Rotate(pivot As RDPointN, angle As Single) As RDPoint
			Return (Me - New RDSizeN(pivot)).Rotate(angle) + New RDSizeN(pivot)
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
			Return $"[{If(X, "null")}, {If(Y, "null")}]"
		End Function
		Private Overloads Function Equals(other As RDPoint) As Boolean Implements IEquatable(Of RDPoint).Equals
			Return other.X.NullableEquals(X) AndAlso other.Y.NullableEquals(Y)
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
		Public Shared Widening Operator CType(p As RDPoint) As RDPointE
			Return New RDPointE(p.X, p.Y)
		End Operator
	End Structure
	''' <summary>
	''' A size whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="integer"/>
	''' </summary>
	<JsonConverter(GetType(RDPointsConverter))>
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
		Public ReadOnly Property Area As Integer?
			Get
				Return Width * Height
			End Get
		End Property
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
			Return $"[{If(Width, "null")}, {If(Height, "null")}]"
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
		Public Shared Widening Operator CType(p As RDSizeI) As RDSize
			Return New RDSize(p.Width, p.Height)
		End Operator
		Public Shared Widening Operator CType(p As RDSizeI) As RDSizeE
			Return New RDSizeE(p.Width, p.Height)
		End Operator
		Public Shared Narrowing Operator CType(size As RDSizeI) As RDPointI
			Return New RDPointI(size.Width, size.Height)
		End Operator
	End Structure
	''' <summary>
	''' A size whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="float"/>
	''' </summary>

	<JsonConverter(GetType(RDPointsConverter))>
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
		Public ReadOnly Property Area As Single?
			Get
				Return Width * Height
			End Get
		End Property
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
			Return $"[{If(Width, "null")}, {If(Height, "null")}]"
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
		Public Shared Widening Operator CType(size As RDSize) As RDSizeE
			Return New RDSizeE(size.Width, size.Height)
		End Operator
		Public Shared Narrowing Operator CType(size As RDSize) As RDPoint
			Return New RDPoint(size.Width, size.Height)
		End Operator
	End Structure
	Public Structure RDRectI
		Implements IEquatable(Of RDRectI)
		Public Property Left As Integer?
		Public Property Right As Integer?
		Public Property Top As Integer?
		Public Property Bottom As Integer?
		Public ReadOnly Property Width As Integer?
			Get
				Return Right - Left
			End Get
		End Property
		Public ReadOnly Property Height As Integer?
			Get
				Return Top - Bottom
			End Get
		End Property
		Public Sub New(left As Integer?, top As Integer?, right As Integer?, bottom As Integer?)
			Me.Left = left
			Me.Right = right
			Me.Top = top
			Me.Bottom = bottom
		End Sub
		Public Sub New(location As RDPointI, size As RDSizeI)
			Me.New(location.X,
				   location.Y + size.Height,
				   location.X + size.Width,
				   location.Y)
		End Sub
		Public Sub New(size As RDSizeI)
			Me.New(0, size.Height, size.Width, 0)
		End Sub
		Public Sub New(width As Integer?, height As Integer?)
			Me.New(0, height, width, 0)
		End Sub
		Public ReadOnly Property Location As RDPointI
			Get
				Return New RDPointI(Left, Bottom)
			End Get
		End Property
		Public ReadOnly Property Size As RDSizeI
			Get
				Return New RDSizeI(Width, Height)
			End Get
		End Property
		Public Shared Function Inflate(rect As RDRectI, size As RDSizeI) As RDRectI
			Dim result As New RDRectI(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(size)
			Return result
		End Function
		Public Shared Function Inflate(rect As RDRectI, x As Integer?, y As Integer?) As RDRectI
			Dim result As New RDRectI(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(x, y)
			Return result
		End Function
		Public Shared Function Ceiling(rect As RDRect) As RDRectI
			Return Ceiling(rect, False)
		End Function
		Public Shared Function Ceiling(rect As RDRect, outwards As Boolean) As RDRectI
			Return New RDRectI(
			If(rect.Left Is Nothing, Nothing,
				If(outwards AndAlso rect.Width > 0, Math.Floor(rect.Left.Value), Math.Ceiling(rect.Left.Value))),
			If(rect.Top Is Nothing, Nothing,
				If(outwards AndAlso rect.Height > 0, Math.Floor(rect.Top.Value), Math.Ceiling(rect.Top.Value))),
			If(rect.Right Is Nothing, Nothing,
				If(outwards AndAlso rect.Width < 0, Math.Floor(rect.Right.Value), Math.Ceiling(rect.Right.Value))),
			If(rect.Bottom Is Nothing, Nothing,
				If(outwards AndAlso rect.Height < 0, Math.Floor(rect.Bottom.Value), Math.Ceiling(rect.Bottom.Value)))
				)
		End Function
		Public Shared Function Floor(rect As RDRect) As RDRectI
			Return Ceiling(rect, False)
		End Function
		Public Shared Function Floor(rect As RDRect, inwards As Boolean) As RDRectI
			Return New RDRectI(
			If(rect.Left Is Nothing, Nothing,
				If(inwards AndAlso rect.Width > 0, Math.Ceiling(rect.Left.Value), Math.Floor(rect.Left.Value))),
			If(rect.Top Is Nothing, Nothing,
				If(inwards AndAlso rect.Height > 0, Math.Ceiling(rect.Top.Value), Math.Floor(rect.Top.Value))),
			If(rect.Right Is Nothing, Nothing,
				If(inwards AndAlso rect.Width < 0, Math.Ceiling(rect.Right.Value), Math.Floor(rect.Right.Value))),
			If(rect.Bottom Is Nothing, Nothing,
				If(inwards AndAlso rect.Height < 0, Math.Ceiling(rect.Bottom.Value), Math.Floor(rect.Bottom.Value)))
				)
		End Function
		Public Shared Function Round(rect As RDRect) As RDRectI
			Return New RDRectI(
				If(rect.Left Is Nothing, Nothing, Math.Round(rect.Left.Value)),
				If(rect.Top Is Nothing, Nothing, Math.Round(rect.Top.Value)),
				If(rect.Right Is Nothing, Nothing, Math.Round(rect.Right.Value)),
				If(rect.Bottom Is Nothing, Nothing, Math.Round(rect.Bottom.Value))
				)
		End Function
		Public Shared Function Union(rect1 As RDRectI, rect2 As RDRectI) As RDRectI
			Return New RDRectI(
				If(rect1.Left Is Nothing OrElse rect2.Left Is Nothing, Nothing, Math.Min(rect1.Left.Value, rect2.Left.Value)),
				If(rect1.Top Is Nothing OrElse rect2.Top Is Nothing, Nothing, Math.Min(rect1.Top.Value, rect2.Top.Value)),
				If(rect1.Right Is Nothing OrElse rect2.Right Is Nothing, Nothing, Math.Min(rect1.Right.Value, rect2.Right.Value)),
				If(rect1.Bottom Is Nothing OrElse rect2.Bottom Is Nothing, Nothing, Math.Min(rect1.Bottom.Value, rect2.Bottom.Value))
				)
		End Function
		Public Shared Function Intersect(rect1 As RDRectI, rect2 As RDRectI) As RDRectI
			Return If(rect1.IntersectsWithInclusive(rect2),
				New RDRectI(
					If(rect1.Left Is Nothing OrElse rect2.Left Is Nothing, Nothing, Math.Max(rect1.Left.Value, rect2.Left.Value)),
					If(rect1.Top Is Nothing OrElse rect2.Top Is Nothing, Nothing, Math.Max(rect1.Top.Value, rect2.Top.Value)),
					If(rect1.Right Is Nothing OrElse rect2.Right Is Nothing, Nothing, Math.Min(rect1.Right.Value, rect2.Right.Value)),
					If(rect1.Bottom Is Nothing OrElse rect2.Bottom Is Nothing, Nothing, Math.Min(rect1.Bottom.Value, rect2.Bottom.Value))),
				New RDRectI)
		End Function
		Public Shared Function Truncate(rect As RDRect) As RDRectI
			Return New RDRectI(
				rect.Left,
				rect.Top,
				rect.Right,
				rect.Bottom
				)
		End Function
		Public Sub Offset(x As Integer?, y As Integer?)
			Left += x
			Top += y
			Right += x
			Bottom += y
		End Sub
		Public Sub Offset(p As RDPointI)
			Offset(p.X, p.Y)
		End Sub
		Public Sub Inflate(size As RDSizeI)
			Left -= size.Width
			Top += size.Height
			Right += size.Width
			Bottom -= size.Height
		End Sub
		Public Sub Inflate(width As Integer?, height As Integer?)
			Left -= width
			Top += height
			Right += width
			Bottom -= height
		End Sub
		Public Function Union(rect As RDRectI) As RDRectI
			Return Union(Me, rect)
		End Function
		Public Function IntersectsWithInclusive(rect As RDRectI) As Boolean
			Return Left <= rect.Right AndAlso Right >= rect.Left AndAlso Top <= rect.Bottom AndAlso Bottom >= rect.Top
		End Function
		Public Shared Operator =(rect1 As RDRectI, rect2 As RDRectI) As Boolean
			Return rect1.Equals(rect2)
		End Operator
		Public Shared Operator <>(rect1 As RDRectI, rect2 As RDRectI) As Boolean
			Return Not rect1.Equals(rect2)
		End Operator
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDRectI) AndAlso Equals(CType(obj, RDRectI))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(Left, Top, Right, Bottom)
		End Function
		Public Overrides Function ToString() As String
			Return $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}"
		End Function
		Public Overloads Function Equals(other As RDRectI) As Boolean Implements IEquatable(Of RDRectI).Equals
			Return Left = other.Left AndAlso Top = other.Top AndAlso Right = other.Right AndAlso Bottom = other.Bottom
		End Function
		Public Shared Widening Operator CType(rect As RDRectI) As RDRect
			Return New RDRect(rect.Left, rect.Top, rect.Right, rect.Bottom)
		End Operator
		Public Shared Widening Operator CType(rect As RDRectI) As RDRectE
			Return New RDRectE(rect.Left, rect.Top, rect.Right, rect.Bottom)
		End Operator
	End Structure
	Public Structure RDRect
		Implements IEquatable(Of RDRect)
		Public Property Left As Single?
		Public Property Right As Single?
		Public Property Top As Single?
		Public Property Bottom As Single?
		Public ReadOnly Property Width As Single?
			Get
				Return Right - Left
			End Get
		End Property
		Public ReadOnly Property Height As Single?
			Get
				Return Top - Bottom
			End Get
		End Property
		Public Sub New(left As Single?, top As Single?, right As Single?, bottom As Single?)
			Me.Left = left
			Me.Right = right
			Me.Top = top
			Me.Bottom = bottom
		End Sub
		Public Sub New(location As RDPoint, size As RDSize)
			Me.New(location.X,
				   location.Y + size.Height,
				   location.X + size.Width,
				   location.Y)
		End Sub
		Public Sub New(size As RDSize)
			Me.New(0, size.Height, size.Width, 0)
		End Sub
		Public Sub New(width As Single?, height As Single?)
			Me.New(0, height, width, 0)
		End Sub
		Public ReadOnly Property Location As RDPoint
			Get
				Return New RDPoint(Left, Bottom)
			End Get
		End Property
		Public ReadOnly Property Size As RDSize
			Get
				Return New RDSize(Width, Height)
			End Get
		End Property
		Public Shared Function Inflate(rect As RDRect, size As RDSize) As RDRect
			Dim result As New RDRect(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(size)
			Return result
		End Function
		Public Shared Function Inflate(rect As RDRect, x As Single?, y As Single?) As RDRect
			Dim result As New RDRect(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(x, y)
			Return result
		End Function
		Public Shared Function Union(rect1 As RDRect, rect2 As RDRect) As RDRect
			Return New RDRect(
				If(rect1.Left Is Nothing OrElse rect2.Left Is Nothing, Nothing, Math.Min(rect1.Left.Value, rect2.Left.Value)),
				If(rect1.Top Is Nothing OrElse rect2.Top Is Nothing, Nothing, Math.Min(rect1.Top.Value, rect2.Top.Value)),
				If(rect1.Right Is Nothing OrElse rect2.Right Is Nothing, Nothing, Math.Min(rect1.Right.Value, rect2.Right.Value)),
				If(rect1.Bottom Is Nothing OrElse rect2.Bottom Is Nothing, Nothing, Math.Min(rect1.Bottom.Value, rect2.Bottom.Value))
				)
		End Function
		Public Shared Function Intersect(rect1 As RDRect, rect2 As RDRect) As RDRect
			Return If(rect1.IntersectsWithInclusive(rect2),
				New RDRect(
					If(rect1.Left Is Nothing OrElse rect2.Left Is Nothing, Nothing, Math.Max(rect1.Left.Value, rect2.Left.Value)),
					If(rect1.Top Is Nothing OrElse rect2.Top Is Nothing, Nothing, Math.Max(rect1.Top.Value, rect2.Top.Value)),
					If(rect1.Right Is Nothing OrElse rect2.Right Is Nothing, Nothing, Math.Min(rect1.Right.Value, rect2.Right.Value)),
					If(rect1.Bottom Is Nothing OrElse rect2.Bottom Is Nothing, Nothing, Math.Min(rect1.Bottom.Value, rect2.Bottom.Value))),
				New RDRect)
		End Function
		Public Shared Function Truncate(rect As RDRect) As RDRect
			Return New RDRect(
				rect.Left,
				rect.Top,
				rect.Right,
				rect.Bottom
				)
		End Function
		Public Sub Offset(x As Single?, y As Single?)
			Left += x
			Top += y
			Right += x
			Bottom += y
		End Sub
		Public Sub Offset(p As RDPoint)
			Offset(p.X, p.Y)
		End Sub
		Public Sub Inflate(size As RDSize)
			Left -= size.Width
			Top += size.Height
			Right += size.Width
			Bottom -= size.Height
		End Sub
		Public Sub Inflate(width As Single?, height As Single?)
			Left -= width
			Top += height
			Right += width
			Bottom -= height
		End Sub
		Public Function Contains(x As Single?, y As Single?) As Boolean
			Return Left < x AndAlso x < Right AndAlso Bottom < y AndAlso y < Top
		End Function
		Public Function Contains(p As RDPoint) As Boolean
			Return Left < p.X AndAlso p.X < Right AndAlso Bottom < p.Y AndAlso p.Y < Top
		End Function
		Public Function Contains(rect As RDRect) As Boolean
			Return Left < rect.Left AndAlso rect.Right < Right AndAlso Bottom < rect.Bottom AndAlso rect.Top < Top
		End Function
		Public Function Union(rect As RDRect) As RDRect
			Return Union(Me, rect)
		End Function
		Public Function Intersect(rect As RDRect)
			Return Intersect(Me, rect)
		End Function
		Public Function IntersectsWith(rect As RDRect) As Boolean
			Return Left < rect.Right AndAlso Right > rect.Left AndAlso Top < rect.Bottom AndAlso Bottom > rect.Top
		End Function
		Public Function IntersectsWithInclusive(rect As RDRect) As Boolean
			Return Left <= rect.Right AndAlso Right >= rect.Left AndAlso Top <= rect.Bottom AndAlso Bottom >= rect.Top
		End Function
		Public Shared Operator =(rect1 As RDRect, rect2 As RDRect) As Boolean
			Return rect1.Equals(rect2)
		End Operator
		Public Shared Operator <>(rect1 As RDRect, rect2 As RDRect) As Boolean
			Return Not rect1.Equals(rect2)
		End Operator
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDRect) AndAlso Equals(CType(obj, RDRect))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(Left, Top, Right, Bottom)
		End Function
		Public Overrides Function ToString() As String
			Return $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}"
		End Function
		Public Overloads Function Equals(other As RDRect) As Boolean Implements IEquatable(Of RDRect).Equals
			Return Left = other.Left AndAlso Top = other.Top AndAlso Right = other.Right AndAlso Bottom = other.Bottom
		End Function
		Public Shared Widening Operator CType(rect As RDRect) As RDRectE
			Return New RDRectE(rect.Left, rect.Top, rect.Right, rect.Bottom)
		End Operator
	End Structure
	''' <summary>
	''' An Expression
	''' </summary>
	<JsonConverter(GetType(RDExpressionConverter))>
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
			'	Throw New Exceptions.ExpressionException($"Illegal Expression: [{value}].")
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
	''' <summary>
	''' A point whose horizontal and vertical coordinates are <strong>nullable</strong> <seealso cref="RDExpression"/>
	''' </summary>
	<JsonConverter(GetType(RDPointsConverter))>
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
			Return $"[{If(X?.ExpressionValue, "null")}, {If(Y?.ExpressionValue, "null")}]"
		End Function
		Private Overloads Function Equals(other As RDPointE) As Boolean Implements IEquatable(Of RDPointE).Equals
			Return other.X = X AndAlso other.Y = Y
		End Function
		''' <summary>
		''' This point is multiplied by a 2*2 matrix.
		''' </summary>
		''' <param name="matrix">2*2 matrix</param>
		Public Function MultipyByMatrix(matrix(,) As RDExpression) As RDPointE
			If matrix.Rank = 2 AndAlso matrix.Length = 4 Then
				Return New RDPointE(
X * matrix(0, 0) + Y * matrix(1, 0),
X * matrix(0, 1) + Y * matrix(1, 1))
			End If
			Throw New Exception("Matrix not match, 2*2 matrix expected.")
		End Function
		''' <summary>
		''' Rotate.
		''' </summary>
		Public Function Rotate(angle As Single) As RDPointE
			Return MultipyByMatrix(
			{
			{CSng(Math.Cos(angle)), CSng(Math.Sin(angle))},
			{CSng(-Math.Sin(angle)), CSng(Math.Cos(angle))}
			})
		End Function
		''' <summary>
		''' Rotate at a given pivot.
		''' </summary>
		''' <param name="pivot">Giver pivot.</param>
		''' <returns></returns>
		Public Function Rotate(pivot As RDPointE, angle As Single) As RDPointE
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
		Public Shared Narrowing Operator CType(v As RDPointE) As RDSizeE
			Return New RDSizeE(v)
		End Operator
	End Structure
	''' <summary>
	''' A size whose horizontal and vertical coordinates are <strong>nullable</strong> <seealso cref="RDExpression"/>
	''' </summary>
	<JsonConverter(GetType(RDPointsConverter))>
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
		Public ReadOnly Property Area As RDExpression?
			Get
				Return Width * Height
			End Get
		End Property
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
			Return $"[{If(Width?.ExpressionValue, "null")}, {If(Height?.ExpressionValue, "null")}]"
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
	End Structure
	Public Structure RDRectE
		Implements IEquatable(Of RDRectE)
		Public Property Left As RDExpression?
		Public Property Right As RDExpression?
		Public Property Top As RDExpression?
		Public Property Bottom As RDExpression?
		Public ReadOnly Property Width As RDExpression?
			Get
				Return Right - Left
			End Get
		End Property
		Public ReadOnly Property Height As RDExpression?
			Get
				Return Top - Bottom
			End Get
		End Property
		Public Sub New(left As RDExpression?, top As RDExpression?, right As RDExpression?, bottom As RDExpression?)
			Me.Left = left
			Me.Right = right
			Me.Top = top
			Me.Bottom = bottom
		End Sub
		Public Sub New(location As RDPointE, size As RDSizeE)
			Me.New(location.X,
				   location.Y + size.Height,
				   location.X + size.Width,
				   location.Y)
		End Sub
		Public Sub New(size As RDSizeE)
			Me.New(0, size.Height, size.Width, 0)
		End Sub
		Public Sub New(width As RDExpression?, height As RDExpression?)
			Me.New(0, height, width, 0)
		End Sub
		Public ReadOnly Property Location As RDPointE
			Get
				Return New RDPointE(Left, Bottom)
			End Get
		End Property
		Public ReadOnly Property Size As RDSizeE
			Get
				Return New RDSizeE(Width, Height)
			End Get
		End Property
		Public Shared Function Inflate(rect As RDRectE, size As RDSizeE) As RDRectE
			Dim result As New RDRectE(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(size)
			Return result
		End Function
		Public Shared Function Inflate(rect As RDRectE, x As RDExpression?, y As RDExpression?) As RDRectE
			Dim result As New RDRectE(rect.Left, rect.Top, rect.Right, rect.Bottom)
			result.Inflate(x, y)
			Return result
		End Function
		Public Shared Function Truncate(rect As RDRectE) As RDRectE
			Return New RDRectE(
				rect.Left,
				rect.Top,
				rect.Right,
				rect.Bottom
				)
		End Function
		Public Sub Offset(x As RDExpression?, y As RDExpression?)
			Left += x
			Top += y
			Right += x
			Bottom += y
		End Sub
		Public Sub Offset(p As RDPointE)
			Offset(p.X, p.Y)
		End Sub
		Public Sub Inflate(size As RDSizeE)
			Left -= size.Width
			Top += size.Height
			Right += size.Width
			Bottom -= size.Height
		End Sub
		Public Sub Inflate(width As RDExpression?, height As RDExpression?)
			Left -= width
			Top += height
			Right += width
			Bottom -= height
		End Sub
		Public Shared Operator =(rect1 As RDRectE, rect2 As RDRectE) As Boolean
			Return rect1.Equals(rect2)
		End Operator
		Public Shared Operator <>(rect1 As RDRectE, rect2 As RDRectE) As Boolean
			Return Not rect1.Equals(rect2)
		End Operator
		Public Overrides Function Equals(<NotNullWhen(True)> obj As Object) As Boolean
			Return obj.GetType = GetType(RDRectE) AndAlso Equals(CType(obj, RDRectE))
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return HashCode.Combine(Left, Top, Right, Bottom)
		End Function
		Public Overrides Function ToString() As String
			Return $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}"
		End Function
		Public Overloads Function Equals(other As RDRectE) As Boolean Implements IEquatable(Of RDRectE).Equals
			Return Left = other.Left AndAlso Top = other.Top AndAlso Right = other.Right AndAlso Bottom = other.Bottom
		End Function
	End Structure
End Namespace