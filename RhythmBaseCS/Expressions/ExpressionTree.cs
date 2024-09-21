using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;
using RhythmBase.Components;
using RhythmBase.Exceptions;
namespace RhythmBase.Expressions
{
	[StandardModule]
	public static class ExpressionTree
	{
		private static bool IsOperator(this TokenType e) => e >= TokenType.Function;

		private static int Level(this TokenType e)
		{
			int Level;
			switch (e)
			{
				case TokenType.Variable:
					Level = 15;
					break;
				case TokenType.Constant:
					Level = 15;
					break;
				case TokenType.BooleanValue:
					Level = 15;
					break;
				case TokenType.FloatValue:
					Level = 15;
					break;
				case TokenType.IntegerValue:
					Level = 15;
					break;
				case TokenType.String:
					Level = 15;
					break;
				case TokenType.Boolean:
					Level = 16;
					break;
				case TokenType.Function:
					Level = 16;
					break;
				case TokenType.ArrayIndex:
					Level = 16;
					break;
				case TokenType.Increment:
					Level = 16;
					break;
				case TokenType.Decrement:
					Level = 16;
					break;
				case TokenType.Add:
					Level = 11;
					break;
				case TokenType.Subtract:
					Level = 11;
					break;
				case TokenType.Multipy:
					Level = 12;
					break;
				case TokenType.Divide:
					Level = 12;
					break;
				case TokenType.Equal:
					Level = 8;
					break;
				case TokenType.NotEqual:
					Level = 8;
					break;
				case TokenType.LessThanOrEqual:
					Level = 9;
					break;
				case TokenType.GreaterThanOrEqual:
					Level = 9;
					break;
				case TokenType.Assign:
					Level = 0;
					break;
				case TokenType.GreaterThan:
					Level = 9;
					break;
				case TokenType.LessThan:
					Level = 9;
					break;
				case TokenType.LeftParenthese:
					Level = 16;
					break;
				case TokenType.RightParenthese:
					Level = 16;
					break;
				case TokenType.LeftBracket:
					Level = 16;
					break;
				case TokenType.RightBracket:
					Level = 16;
					break;
				case TokenType.LeftBrace:
					Level = 16;
					break;
				case TokenType.RightBrace:
					Level = 16;
					break;
				case TokenType.Dot:
					Level = 16;
					break;
				case TokenType.Comma:
					Level = -1;
					break;
				case TokenType.And:
					Level = 4;
					break;
				case TokenType.Or:
					Level = 3;
					break;
				case TokenType.Not:
					Level = 16;
					break;
				default:
					throw new Exception();
			}
			return Level;
		}

		private static bool IsBinary(this TokenType e) => new TokenType[]
			{
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
			}.Contains(e);

		private static bool IsRightHalf(this TokenType e) => new TokenType[]
			{
				TokenType.RightParenthese,
				TokenType.RightBracket,
				TokenType.RightBrace
			}.Contains(e);

		internal static Func<Variables, TResult> GetFunctionalExpression<TResult>(string exp)
		{
			ParameterExpression param = System.Linq.Expressions.Expression.Parameter(typeof(Variables), "v");
			System.Linq.Expressions.Expression resultExp = GetExpression(exp, param);
			Expression<Func<Variables, TResult>> lambda = System.Linq.Expressions.Expression.Lambda<Func<Variables, TResult>>(System.Linq.Expressions.Expression.Convert(resultExp, typeof(TResult)),
			[
				param
			]);
			return lambda.Compile();
		}

		public static System.Linq.Expressions.Expression GetExpression(string exp, ParameterExpression param)
		{
			IEnumerable<Token> Tokens = ReadExpressionString(exp);
			return ReadTree(Tokens, param);
		}

		private static IEnumerable<Token> ReadExpressionString(string exp)
		{
			List<Token> L = [];
			while (exp.Length > 0)
			{
				bool isReplaced = false;
				foreach (KeyValuePair<TokenType, Regex> pair in Ops)
				{
					Match match = pair.Value.Match(exp);
					if (match.Success)
					{
						L.Add(new Token(match.Groups["value"].Value, pair.Key));
						exp = pair.Value.Replace(exp, "");
						isReplaced = true;
						break;
					}
				}
				if (!isReplaced)
				{
					throw new Exception();
				}
			}
			return L.AsEnumerable();
		}

		private static System.Linq.Expressions.Expression ReadTree(IEnumerable<Token> l, ParameterExpression variableParameter)
		{
			Stack<Token> OperatorStack = new();
			Stack<System.Linq.Expressions.Expression> ValueStack = new();
			System.Linq.Expressions.Expression subVariableParameter = variableParameter;
			foreach (Token item in l)
			{
				if (item.type.IsOperator())
				{
					if (OperatorStack.Any() && (OperatorStack.Peek().type.Level() > item.type.Level() | OperatorStack.Peek().type.IsRightHalf() | OperatorStack.Peek().type == TokenType.Comma))
					{
						GroupNode(ValueStack, OperatorStack, variableParameter, ref subVariableParameter);
					}
					if (item.type != TokenType.Dot)
					{
						subVariableParameter = variableParameter;
					}
					OperatorStack.Push(item);
				}
				else
				{
					ValueStack.Push(ReadValueNode(item, ValueStack, OperatorStack, variableParameter, ref subVariableParameter));
				}
			}
			while (OperatorStack.Any())
			{
				if (OperatorStack.Peek().type != TokenType.Dot)
				{
					subVariableParameter = variableParameter;
				}
				else
				{
					subVariableParameter = ValueStack.Peek();
				}
				GroupNode(ValueStack, OperatorStack, variableParameter, ref subVariableParameter);
			}
			return ValueStack.Single();
		}

		private static System.Linq.Expressions.Expression ReadValueNode(Token token, Stack<System.Linq.Expressions.Expression> valueStack, Stack<Token> operatorStack, ParameterExpression VariableParameter, ref System.Linq.Expressions.Expression subVariableParameter)
		{
			System.Linq.Expressions.Expression ReadValueNode;
			switch (token.type)
			{
				case TokenType.Variable:
					{
						string name = token.value;
						if (operatorStack.Peek().type == TokenType.Dot)
						{
							operatorStack.Pop();
							valueStack.Pop();
						}
						System.Linq.Expressions.Expression Value = System.Linq.Expressions.Expression.PropertyOrField(subVariableParameter, name);
						ReadValueNode = Value;
						break;
					}
				case TokenType.Constant:
					{
						System.Linq.Expressions.Expression Value2 = System.Linq.Expressions.Expression.Constant(Conversions.ToSingle(token.value), typeof(float));
						ReadValueNode = Value2;
						break;
					}
				case TokenType.BooleanValue:
					{
						int arrayIndex = Conversions.ToInteger(token.value.Last().ToString());
						System.Linq.Expressions.Expression Value3 = System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.PropertyOrField(VariableParameter, "b"), "Item",
						[
					System.Linq.Expressions.Expression.Constant(arrayIndex, typeof(int))
						]);
						if (token.value.StartsWith('-'))
						{
							Value3 = System.Linq.Expressions.Expression.Negate(Value3);
						}
						ReadValueNode = Value3;
						break;
					}
				case TokenType.FloatValue:
					{
						int arrayIndex2 = Conversions.ToInteger(token.value.Last().ToString());
						System.Linq.Expressions.Expression Value4 = System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.PropertyOrField(VariableParameter, "f"), "Item",
						[
					System.Linq.Expressions.Expression.Constant(arrayIndex2, typeof(int))
						]);
						if (token.value.StartsWith('-'))
						{
							Value4 = System.Linq.Expressions.Expression.Negate(Value4);
						}
						ReadValueNode = Value4;
						break;
					}
				case TokenType.IntegerValue:
					{
						int arrayIndex3 = Conversions.ToInteger(token.value.Last().ToString());
						System.Linq.Expressions.Expression Value5 = System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.PropertyOrField(VariableParameter, "i"), "Item",
						[
					System.Linq.Expressions.Expression.Constant(arrayIndex3, typeof(int))
						]);
						if (token.value.StartsWith('-'))
						{
							Value5 = System.Linq.Expressions.Expression.Negate(Value5);
						}
						ReadValueNode = Value5;
						break;
					}
				case TokenType.String:
					{
						System.Linq.Expressions.Expression Value6 = System.Linq.Expressions.Expression.Constant(token.value, typeof(string));
						ReadValueNode = Value6;
						break;
					}
				case TokenType.Boolean:
					{
						System.Linq.Expressions.Expression value = System.Linq.Expressions.Expression.Constant(Conversions.ToBoolean(token.value), typeof(bool));
						ReadValueNode = value;
						break;
					}
				default:
					throw new RhythmBaseException(string.Format("Illegal parameter: {0}", token.value));
			}
			return ReadValueNode;
		}

		private static void GroupNode(Stack<System.Linq.Expressions.Expression> ValueStack, Stack<Token> OperatorStack, ParameterExpression variableParameter, ref System.Linq.Expressions.Expression subVariableParameter)
		{
			Token op = OperatorStack.Pop();
			switch (op.type)
			{
				case TokenType.Function:
					OperatorStack.Push(op);
					break;
				case TokenType.ArrayIndex:
					OperatorStack.Push(op);
					break;
				case TokenType.Increment:
					ValueStack.Push(System.Linq.Expressions.Expression.Decrement(ValueStack.Pop()));
					break;
				case TokenType.Decrement:
					ValueStack.Push(System.Linq.Expressions.Expression.Decrement(ValueStack.Pop()));
					break;
				case TokenType.Add:
					{
						System.Linq.Expressions.Expression right = ValueStack.Pop();
						System.Linq.Expressions.Expression left = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.Add(left, System.Linq.Expressions.Expression.Convert(right, left.Type)));
						break;
					}
				case TokenType.Subtract:
					{
						System.Linq.Expressions.Expression right2 = ValueStack.Pop();
						System.Linq.Expressions.Expression left2 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.Subtract(left2, System.Linq.Expressions.Expression.Convert(right2, left2.Type)));
						break;
					}
				case TokenType.Multipy:
					{
						System.Linq.Expressions.Expression right3 = ValueStack.Pop();
						System.Linq.Expressions.Expression left3 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.Multiply(left3, System.Linq.Expressions.Expression.Convert(right3, left3.Type)));
						break;
					}
				case TokenType.Divide:
					{
						System.Linq.Expressions.Expression right4 = ValueStack.Pop();
						System.Linq.Expressions.Expression left4 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.Divide(left4, System.Linq.Expressions.Expression.Convert(right4, left4.Type)));
						break;
					}
				case TokenType.Equal:
					{
						System.Linq.Expressions.Expression right5 = ValueStack.Pop();
						System.Linq.Expressions.Expression left5 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.Equal(left5, System.Linq.Expressions.Expression.Convert(right5, left5.Type)));
						break;
					}
				case TokenType.NotEqual:
					{
						System.Linq.Expressions.Expression right6 = ValueStack.Pop();
						System.Linq.Expressions.Expression left6 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.NotEqual(left6, System.Linq.Expressions.Expression.Convert(right6, left6.Type)));
						break;
					}
				case TokenType.LessThanOrEqual:
					{
						System.Linq.Expressions.Expression right7 = ValueStack.Pop();
						System.Linq.Expressions.Expression left7 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.LessThanOrEqual(left7, System.Linq.Expressions.Expression.Convert(right7, left7.Type)));
						break;
					}
				case TokenType.GreaterThanOrEqual:
					{
						System.Linq.Expressions.Expression right8 = ValueStack.Pop();
						System.Linq.Expressions.Expression left8 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.GreaterThanOrEqual(left8, System.Linq.Expressions.Expression.Convert(right8, left8.Type)));
						break;
					}
				case TokenType.Assign:
					{
						System.Linq.Expressions.Expression right9 = ValueStack.Pop();
						System.Linq.Expressions.Expression left9 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.Assign(left9, System.Linq.Expressions.Expression.Convert(right9, left9.Type)));
						break;
					}
				case TokenType.GreaterThan:
					{
						System.Linq.Expressions.Expression right10 = ValueStack.Pop();
						System.Linq.Expressions.Expression left10 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.GreaterThan(left10, System.Linq.Expressions.Expression.Convert(right10, left10.Type)));
						break;
					}
				case TokenType.LessThan:
					{
						System.Linq.Expressions.Expression right11 = ValueStack.Pop();
						System.Linq.Expressions.Expression left11 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.LessThan(left11, System.Linq.Expressions.Expression.Convert(right11, left11.Type)));
						break;
					}
				case TokenType.LeftParenthese:
					OperatorStack.Push(op);
					break;
				case TokenType.RightParenthese:
					{
						List<System.Linq.Expressions.Expression> Parameters = [];
						bool packed = false;
						while (!packed)
						{
							TokenType type = OperatorStack.Peek().type;
							if (type != TokenType.Function)
							{
								if (type != TokenType.LeftParenthese)
								{
									if (type != TokenType.Comma)
									{
										GroupNode(ValueStack, OperatorStack, variableParameter, ref subVariableParameter);
									}
									else
									{
										Parameters.Insert(0, ValueStack.Pop());
										OperatorStack.Pop();
									}
								}
								else
								{
									Parameters.Insert(0, ValueStack.Pop());
									OperatorStack.Pop();
									System.Linq.Expressions.Expression member = Parameters.Single();
									ValueStack.Push(member);
									subVariableParameter = member;
									packed = true;
								}
							}
							else
							{
								Parameters.Insert(0, ValueStack.Pop());
								MethodInfo method = subVariableParameter.Type.GetMethod(OperatorStack.Pop().value);
								if (method.GetParameters().Length != Parameters.Count)
								{
									throw new RhythmBaseException(string.Format("Parameters count not match. need {0}", method.GetParameters().Length));
								}
								//System.Linq.Expressions.Expression member2 = System.Linq.Expressions.Expression.Call(subVariableParameter, method, Parameters.Zip(method.GetParameters(), (ExpressionTree._Closure$__.$I13-0 == null) ? (ExpressionTree._Closure$__.$I13-0 = (System.Linq.Expressions.Expression i, ParameterInfo j) => System.Linq.Expressions.Expression.Convert(i, j.ParameterType)) : ExpressionTree._Closure$__.$I13-0));
								//ValueStack.Push(member2);
								//subVariableParameter = member2;
								packed = true;
							}
						}
						break;
					}
				case TokenType.LeftBracket:
					OperatorStack.Push(op);
					break;
				case TokenType.RightBracket:
					{
						TokenType type2 = OperatorStack.Peek().type;
						if (type2 != TokenType.ArrayIndex)
						{
							if (type2 != TokenType.LeftBracket)
							{
								throw new ExpressionException("Not implemented.");
							}
							throw new ExpressionException("Not implemented.");
						}
						else
						{
							System.Linq.Expressions.Expression member3 = System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.PropertyOrField(subVariableParameter, OperatorStack.Pop().value), "Item",
							[
						System.Linq.Expressions.Expression.Convert(ValueStack.Pop(), typeof(int))
							]);
							ValueStack.Push(member3);
							subVariableParameter = member3;
						}
						break;
					}
				case TokenType.LeftBrace:
					OperatorStack.Push(op);
					break;
				case TokenType.RightBrace:
					{
						TokenType type3 = OperatorStack.Peek().type;
						if (type3 != TokenType.LeftBrace)
						{
							throw new ExpressionException("Not implemented.");
						}
						throw new ExpressionException("Not implemented.");
					}
				case TokenType.Dot:
					break;
				case TokenType.Comma:
					OperatorStack.Push(op);
					break;
				case TokenType.And:
					{
						System.Linq.Expressions.Expression right12 = ValueStack.Pop();
						System.Linq.Expressions.Expression left12 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.And(System.Linq.Expressions.Expression.Convert(left12, typeof(bool)), System.Linq.Expressions.Expression.Convert(right12, typeof(bool))));
						break;
					}
				case TokenType.Or:
					{
						System.Linq.Expressions.Expression right13 = ValueStack.Pop();
						System.Linq.Expressions.Expression left13 = ValueStack.Pop();
						ValueStack.Push(System.Linq.Expressions.Expression.Or(System.Linq.Expressions.Expression.Convert(left13, typeof(bool)), System.Linq.Expressions.Expression.Convert(right13, typeof(bool))));
						break;
					}
				case TokenType.Not:
					ValueStack.Push(System.Linq.Expressions.Expression.Not(System.Linq.Expressions.Expression.Convert(ValueStack.Pop(), typeof(bool))));
					break;
				default:
					throw new ExpressionException("Not implemented.");
			}
		}

		private static readonly Dictionary<TokenType, Regex> Ops = new()
		{
			{
				TokenType.Increment,
				new Regex("^(?<value>\\+\\+)")
			},
			{
				TokenType.Decrement,
				new Regex("^(?<value>\\-\\-)")
			},
			{
				TokenType.Add,
				new Regex("^(?<value>\\+)")
			},
			{
				TokenType.Subtract,
				new Regex("^(?<value>\\-)")
			},
			{
				TokenType.Multipy,
				new Regex("^(?<value>\\*)")
			},
			{
				TokenType.Divide,
				new Regex("^(?<value>\\/)")
			},
			{
				TokenType.Function,
				new Regex("^(?<value>[A-Za-z][A-Za-z0-9]+)\\(")
			},
			{
				TokenType.ArrayIndex,
				new Regex("^(?<value>[A-Za-z][A-Za-z0-9]+)\\[")
			},
			{
				TokenType.Boolean,
				new Regex("^(?<value>[Tt]rue|[Ff]alse)")
			},
			{
				TokenType.String,
				new Regex("^str:(?<value>[^,])")
			},
			{
				TokenType.IntegerValue,
				new Regex("^(?<!\\d)(?<value>-?i\\d)")
			},
			{
				TokenType.FloatValue,
				new Regex("^(?<!\\d)(?<value>-?f\\d)")
			},
			{
				TokenType.BooleanValue,
				new Regex("^(?<!\\d)(?<value>-?b\\d)")
			},
			{
				TokenType.Constant,
				new Regex("^(?<value>-?((\\d+(\\.(\\d+)?)?)|(\\.\\d+)))")
			},
			{
				TokenType.Variable,
				new Regex("^(?<value>[A-Za-z][A-Za-z0-9]+)")
			},
			{
				TokenType.Equal,
				new Regex("^(?<value>==)")
			},
			{
				TokenType.NotEqual,
				new Regex("^(?<value>!=)")
			},
			{
				TokenType.GreaterThanOrEqual,
				new Regex("^(?<value>\\>=)")
			},
			{
				TokenType.LessThanOrEqual,
				new Regex("^(?<value>\\<=)")
			},
			{
				TokenType.Assign,
				new Regex("^(?<value>=)")
			},
			{
				TokenType.GreaterThan,
				new Regex("^(?<value>\\>)")
			},
			{
				TokenType.LessThan,
				new Regex("^(?<value>\\<)")
			},
			{
				TokenType.LeftParenthese,
				new Regex("^(?<value>\\()")
			},
			{
				TokenType.RightParenthese,
				new Regex("^(?<value>\\))")
			},
			{
				TokenType.LeftBracket,
				new Regex("^(?<value>\\[)")
			},
			{
				TokenType.RightBracket,
				new Regex("^(?<value>\\])")
			},
			{
				TokenType.LeftBrace,
				new Regex("^(?<value>\\{)")
			},
			{
				TokenType.RightBrace,
				new Regex("^(?<value>\\})")
			},
			{
				TokenType.Dot,
				new Regex("^(?<value>\\.)")
			},
			{
				TokenType.Comma,
				new Regex("^(?<value>,)")
			},
			{
				TokenType.And,
				new Regex("^(?<value>&&)")
			},
			{
				TokenType.Or,
				new Regex("^(?<value>\\|\\|)")
			},
			{
				TokenType.Not,
				new Regex("^(?<value>!)")
			}
		};

		private enum TokenType
		{
			Function,
			ArrayIndex,
			Boolean = -1,
			String = -2,
			IntegerValue = -3,
			FloatValue = -4,
			BooleanValue = -5,
			Constant = -6,
			Variable = -7,
			Increment = 2,
			Decrement,
			Add,
			Subtract,
			Multipy,
			Divide,
			Equal,
			NotEqual,
			LessThanOrEqual,
			GreaterThanOrEqual,
			Assign,
			GreaterThan,
			LessThan,
			LeftParenthese,
			RightParenthese,
			LeftBracket,
			RightBracket,
			LeftBrace,
			RightBrace,
			Dot,
			Comma,
			And,
			Or,
			Not
		}

		private struct Token
		{
			public Token(string value, TokenType token)
			{
				this = default;
				this.value = value;
				type = token;
			}

			public override readonly string ToString() => string.Format("{0}, {1}", value, type);

			public string value;

			public TokenType type;
		}
	}
}
