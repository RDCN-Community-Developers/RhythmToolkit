Imports System.Data
Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports RhythmBase.LevelElements
Imports RhythmBase.Components
Namespace Expressions
	Public Module ExpressionTree
		<Extension>
		Private Function IsOperator(e As TokenType) As Boolean
			Return e >= 0
		End Function
		<Extension>
		Private Function Level(e As TokenType) As Integer
			Select Case e
				Case TokenType.Function
					Return 16
				Case TokenType.ArrayIndex
					Return 16
				Case TokenType.Boolean
					Return 16
				Case TokenType.String
					Return 15
				Case TokenType.IntegerValue
					Return 15
				Case TokenType.FloatValue
					Return 15
				Case TokenType.BooleanValue
					Return 15
				Case TokenType.Constant
					Return 15
				Case TokenType.Variable
					Return 15
				Case TokenType.Increment
					Return 16
				Case TokenType.Decrement
					Return 16
				Case TokenType.Add
					Return 11
				Case TokenType.Subtract
					Return 11
				Case TokenType.Multipy
					Return 12
				Case TokenType.Divide
					Return 12
				Case TokenType.Equal
					Return 8
				Case TokenType.NotEqual
					Return 8
				Case TokenType.LessThanOrEqual
					Return 9
				Case TokenType.GreaterThanOrEqual
					Return 9
				Case TokenType.Assign
					Return 0
				Case TokenType.GreaterThan
					Return 9
				Case TokenType.LessThan
					Return 9
				Case TokenType.LeftParenthese
					Return 16
				Case TokenType.RightParenthese
					Return 16
				Case TokenType.LeftBracket
					Return 16
				Case TokenType.RightBracket
					Return 16
				Case TokenType.LeftBrace
					Return 16
				Case TokenType.RightBrace
					Return 16
				Case TokenType.Dot
					Return 16
				Case TokenType.Comma
					Return -1
				Case TokenType.And
					Return 4
				Case TokenType.Or
					Return 3
				Case TokenType.Not
					Return 16
			End Select
			Throw New Exception
		End Function
		<Extension>
		Private Function IsBinary(e As TokenType) As Boolean
			Return {
				TokenType.Add,
				TokenType.Subtract,
				TokenType.Multipy,
				TokenType.Divide,
				TokenType.Equal,
				TokenType.NotEqual,
				TokenType.LessThanOrEqual,
				TokenType.GreaterThanOrEqual,
				TokenType.Assign,
				TokenType.GreaterThan,
				TokenType.LessThan,
				TokenType.Dot,
				TokenType.And,
				TokenType.Or
			}.Contains(e)
		End Function
		<Extension>
		Private Function IsRightHalf(e As TokenType) As Boolean
			Return {
				TokenType.RightParenthese,
				TokenType.RightBracket,
				TokenType.RightBrace
			}.Contains(e)
		End Function
		Private Enum TokenType
			[Function]
			ArrayIndex
			[Boolean] = -1
			[String] = -2
			IntegerValue = -3
			FloatValue = -4
			BooleanValue = -5
			[Constant] = -6
			Variable = -7
			Increment = 2
			Decrement
			Add
			Subtract
			Multipy
			Divide
			Equal
			NotEqual
			LessThanOrEqual
			GreaterThanOrEqual
			Assign
			GreaterThan
			LessThan
			LeftParenthese
			RightParenthese
			LeftBracket
			RightBracket
			LeftBrace
			RightBrace
			Dot
			Comma
			[And]
			[Or]
			[Not]
		End Enum
		Private Structure Token
			Public value As String
			Public type As TokenType
			Public Sub New(value As String, token As TokenType)
				Me.value = value
				Me.type = token
			End Sub
			Public Overrides Function ToString() As String
				Return $"{value}, {type}"
			End Function
		End Structure
		ReadOnly Ops As New Dictionary(Of TokenType, Regex) From {
			{TokenType.Increment, New Regex("^(?<value>\+\+)")},
			{TokenType.Decrement, New Regex("^(?<value>\-\-)")},
			{TokenType.Add, New Regex("^(?<value>\+)")},
			{TokenType.Subtract, New Regex("^(?<value>\-)")},
			{TokenType.Multipy, New Regex("^(?<value>\*)")},
			{TokenType.Divide, New Regex("^(?<value>\/)")},
			{TokenType.Function, New Regex("^(?<value>[A-Za-z][A-Za-z0-9]+)\(")},
			{TokenType.ArrayIndex, New Regex("^(?<value>[A-Za-z][A-Za-z0-9]+)\[")},
			{TokenType.Boolean, New Regex("^(?<value>[Tt]rue|[Ff]alse)")},
			{TokenType.String, New Regex("^str:(?<value>[^,])")},
			{TokenType.IntegerValue, New Regex("^(?<!\d)(?<value>-?i\d)")},
			{TokenType.FloatValue, New Regex("^(?<!\d)(?<value>-?f\d)")},
			{TokenType.BooleanValue, New Regex("^(?<!\d)(?<value>-?b\d)")},
			{TokenType.Constant, New Regex("^(?<value>-?((\d+(\.(\d+)?)?)|(\.\d+)))")},
			{TokenType.Variable, New Regex("^(?<value>[A-Za-z][A-Za-z0-9]+)")},
			{TokenType.Equal, New Regex("^(?<value>==)")},
			{TokenType.NotEqual, New Regex("^(?<value>!=)")},
			{TokenType.GreaterThanOrEqual, New Regex("^(?<value>\>=)")},
			{TokenType.LessThanOrEqual, New Regex("^(?<value>\<=)")},
			{TokenType.Assign, New Regex("^(?<value>=)")},
			{TokenType.GreaterThan, New Regex("^(?<value>\>)")},
			{TokenType.LessThan, New Regex("^(?<value>\<)")},
			{TokenType.LeftParenthese, New Regex("^(?<value>\()")},
			{TokenType.RightParenthese, New Regex("^(?<value>\))")},
			{TokenType.LeftBracket, New Regex("^(?<value>\[)")},
			{TokenType.RightBracket, New Regex("^(?<value>\])")},
			{TokenType.LeftBrace, New Regex("^(?<value>\{)")},
			{TokenType.RightBrace, New Regex("^(?<value>\})")},
			{TokenType.Dot, New Regex("^(?<value>\.)")},
			{TokenType.Comma, New Regex("^(?<value>,)")},
			{TokenType.And, New Regex("^(?<value>&&)")},
			{TokenType.Or, New Regex("^(?<value>\|\|)")},
			{TokenType.Not, New Regex("^(?<value>!)")}
		}
		Friend Function GetFunctionalExpression(Of TResult)(exp As String) As Func(Of Variables, TResult)
			Dim param = Expression.Parameter(GetType(Variables), "v")
			Dim resultExp = GetExpression(exp, param)
			Dim lambda As Expression(Of Func(Of Variables, TResult)) = Expression.Lambda(Of Func(Of Variables, TResult))(Expression.Convert(resultExp, GetType(TResult)), param)
			Dim compiledFunction As Func(Of Variables, TResult) = lambda.Compile
			Return compiledFunction
		End Function
		Public Function GetExpression(exp As String, param As ParameterExpression) As Expression
			Dim Tokens = ReadExpressionString(exp)
			Return ReadTree(Tokens, param)
		End Function
		Private Function ReadExpressionString(exp As String) As IEnumerable(Of Token)
			Dim L As New List(Of Token)
			While exp.Length > 0
				Dim isReplaced = False
				For Each pair In Ops
					Dim match = pair.Value.Match(exp)
					If match.Success Then
						L.Add(New Token(match.Groups("value").Value, pair.Key))
						exp = pair.Value.Replace(exp, "")
						isReplaced = True
						Exit For
					End If
				Next
				If Not isReplaced Then
					Throw New Exception
				End If
			End While
			Return L.AsEnumerable
		End Function
		Private Function ReadTree(l As IEnumerable(Of Token), variableParameter As ParameterExpression) As Expression
			Dim OperatorStack As New Stack(Of Token)
			Dim ValueStack As New Stack(Of Expression)
			Dim subVariableParameter As Expression = variableParameter
			For Each item In l
				If item.type.IsOperator Then
					If OperatorStack.Any AndAlso (OperatorStack.Peek.type.Level > item.type.Level Or OperatorStack.Peek.type.IsRightHalf Or OperatorStack.Peek.type = TokenType.Comma) Then
						GroupNode(ValueStack, OperatorStack, variableParameter, subVariableParameter)
					End If
					If item.type <> TokenType.Dot Then
						subVariableParameter = variableParameter
					End If
					OperatorStack.Push(item)
				Else
					ValueStack.Push(ReadValueNode(item, ValueStack, OperatorStack, variableParameter, subVariableParameter))
				End If
			Next
			While OperatorStack.Any
				If OperatorStack.Peek.type <> TokenType.Dot Then
					subVariableParameter = variableParameter
				Else
					subVariableParameter = ValueStack.Peek
				End If
				GroupNode(ValueStack, OperatorStack, variableParameter, subVariableParameter)
			End While
			Return ValueStack.Single
		End Function
		Private Function ReadValueNode(token As Token, valueStack As Stack(Of Expression), operatorStack As Stack(Of Token), VariableParameter As ParameterExpression, ByRef subVariableParameter As Expression) As Expression
			Select Case token.type
				Case TokenType.Boolean
					Dim value As Expression = Expression.Constant(CBool(token.value), GetType(Boolean))
					Return value
				Case TokenType.String
					Dim Value As Expression = Expression.Constant(token.value, GetType(String))
					Return Value
				Case TokenType.IntegerValue
					Dim arrayIndex = CInt(token.value.Last.ToString)
					Dim Value As Expression = Expression.Property(Expression.PropertyOrField(VariableParameter, "i"), "Item", Expression.Constant(arrayIndex, GetType(Integer)))
					If token.value.StartsWith("-"c) Then
						Value = Expression.Negate(Value)
					End If
					Return Value
				Case TokenType.FloatValue
					Dim arrayIndex = CInt(token.value.Last.ToString)
					Dim Value As Expression = Expression.Property(Expression.PropertyOrField(VariableParameter, "f"), "Item", Expression.Constant(arrayIndex, GetType(Integer)))
					If token.value.StartsWith("-"c) Then
						Value = Expression.Negate(Value)
					End If
					Return Value
				Case TokenType.BooleanValue
					Dim arrayIndex = CInt(token.value.Last.ToString)
					Dim Value As Expression = Expression.Property(Expression.PropertyOrField(VariableParameter, "b"), "Item", Expression.Constant(arrayIndex, GetType(Integer)))
					If token.value.StartsWith("-"c) Then
						Value = Expression.Negate(Value)
					End If
					Return Value
				Case TokenType.Constant
					Dim Value As Expression = Expression.Constant(CSng(token.value), GetType(Single))
					Return Value
				Case TokenType.Variable
					Dim name = token.value
					If operatorStack.Peek.type = TokenType.Dot Then
						operatorStack.Pop()
						valueStack.Pop()
					End If
					Dim Value As Expression = Expression.PropertyOrField(subVariableParameter, name)
					Return Value
				Case Else
					Throw New Exceptions.RhythmBaseException($"Illegal parameter: {token.value}")
			End Select
		End Function
		Private Sub GroupNode(ValueStack As Stack(Of Expression), OperatorStack As Stack(Of Token), variableParameter As ParameterExpression, ByRef subVariableParameter As Expression)
			Dim op = OperatorStack.Pop
			Select Case op.type
				Case TokenType.Function
					OperatorStack.Push(op)
				Case TokenType.ArrayIndex
					OperatorStack.Push(op)
				Case TokenType.Increment
					ValueStack.Push(Expression.Decrement(ValueStack.Pop))
				Case TokenType.Decrement
					ValueStack.Push(Expression.Decrement(ValueStack.Pop))
				Case TokenType.Add
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.Add(left, Expression.Convert(right, left.Type)))
				Case TokenType.Subtract
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.Subtract(left, Expression.Convert(right, left.Type)))
				Case TokenType.Multipy
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.Multiply(left, Expression.Convert(right, left.Type)))
				Case TokenType.Divide
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.Divide(left, Expression.Convert(right, left.Type)))
				Case TokenType.Equal
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.Equal(left, Expression.Convert(right, left.Type)))
				Case TokenType.NotEqual
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.NotEqual(left, Expression.Convert(right, left.Type)))
				Case TokenType.LessThanOrEqual
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.LessThanOrEqual(left, Expression.Convert(right, left.Type)))
				Case TokenType.GreaterThanOrEqual
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.GreaterThanOrEqual(left, Expression.Convert(right, left.Type)))
				Case TokenType.Assign
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.Assign(left, Expression.Convert(right, left.Type)))
				Case TokenType.GreaterThan
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.GreaterThan(left, Expression.Convert(right, left.Type)))
				Case TokenType.LessThan
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.LessThan(left, Expression.Convert(right, left.Type)))
				Case TokenType.LeftParenthese
					OperatorStack.Push(op)
				Case TokenType.RightParenthese
					Dim Parameters As New List(Of Expression)
					Dim packed = False
					While Not packed
						Select Case OperatorStack.Peek.type
							Case TokenType.Function
								Parameters.Insert(0, ValueStack.Pop)
								Dim method = subVariableParameter.Type.GetMethod(OperatorStack.Pop.value)
								If method.GetParameters.Length <> Parameters.Count Then
									Throw New Exceptions.RhythmBaseException($"Parameters count not match. need {method.GetParameters.Length}")
								End If
								Dim member As Expression = Expression.Call(subVariableParameter, method, Parameters.Zip(method.GetParameters, Function(i, j) Expression.Convert(i, j.ParameterType)))
								ValueStack.Push(member)
								subVariableParameter = member
								packed = True
							Case TokenType.LeftParenthese
								Parameters.Insert(0, ValueStack.Pop)
								OperatorStack.Pop()
								Dim member As Expression = Parameters.Single
								ValueStack.Push(member)
								subVariableParameter = member
								packed = True
							Case TokenType.Comma
								Parameters.Insert(0, ValueStack.Pop)
								OperatorStack.Pop()
							Case Else
								GroupNode(ValueStack, OperatorStack, variableParameter, subVariableParameter)
						End Select
					End While
				Case TokenType.LeftBracket
					OperatorStack.Push(op)
				Case TokenType.RightBracket
					Select Case OperatorStack.Peek.type
						Case TokenType.ArrayIndex
							Dim member As Expression = Expression.Property(Expression.PropertyOrField(subVariableParameter, OperatorStack.Pop.value), "Item", Expression.Convert(ValueStack.Pop, GetType(Integer)))
							ValueStack.Push(member)
							subVariableParameter = member
						Case TokenType.LeftBracket
							Throw New Exceptions.ExpressionException("Not implemented.")
						Case Else
							Throw New Exceptions.ExpressionException("Not implemented.")
					End Select
				Case TokenType.LeftBrace
					OperatorStack.Push(op)
				Case TokenType.RightBrace
					Select Case OperatorStack.Peek.type
						Case TokenType.LeftBrace
							Throw New Exceptions.ExpressionException("Not implemented.")
						Case Else
							Throw New Exceptions.ExpressionException("Not implemented.")
					End Select
				Case TokenType.Dot
				Case TokenType.Comma
					OperatorStack.Push(op)
				Case TokenType.And
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.And(Expression.Convert(left, GetType(Boolean)), Expression.Convert(right, GetType(Boolean))))
				Case TokenType.Or
					Dim right = ValueStack.Pop
					Dim left = ValueStack.Pop
					ValueStack.Push(Expression.Or(Expression.Convert(left, GetType(Boolean)), Expression.Convert(right, GetType(Boolean))))
				Case TokenType.Not
					ValueStack.Push(Expression.Not(Expression.Convert(ValueStack.Pop, GetType(Boolean))))
				Case Else
					Throw New Exceptions.ExpressionException("Not implemented.")
			End Select
		End Sub
	End Module
End Namespace

Namespace Exceptions
	Public Class ExpressionException
		Inherits RhythmBaseException
		Public Sub New()
			MyBase.New
		End Sub
		Public Sub New(message As String)
			MyBase.New(message)
		End Sub
	End Class
End Namespace