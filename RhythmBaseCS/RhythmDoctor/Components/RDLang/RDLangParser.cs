using RhythmBase.RhythmDoctor.Components;
using sly.lexer;
using sly.parser.generator;

namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	internal class RDLangParser
	{
		internal const string PlainString = "";
		private static Stack<Token<RDExpressionToken>> TokenStack { get; set; } = [];
		public RDVariables Variables { get; set; }
		[NodeName("integer")]
		[Production("primary: Number")]
		public static float Primary(Token<RDExpressionToken> intToken) => float.Parse(intToken.Value);
		[NodeName("boolean")]
		[Production("primary: True")]
		[Production("primary: False")]
		public static float Boolean(Token<RDExpressionToken> token) => token.TokenID switch
		{
			RDExpressionToken.True => 1.0f,
			RDExpressionToken.False => 0.0f,
			_ => throw new InvalidOperationException("Invalid boolean token")
		};
		[NodeName("group")]
		[Production("primary: LeftParenthesis [d] expression RightParenthesis [d]")]
		public static float Group(float groupValue) => groupValue;
		[NodeName("addOrSubstract")]
		[Production("expression : term Add expression")]
		[Production("expression : term Subtract expression")]
		public static float Expression(float left, Token<RDExpressionToken> operatorToken, float right) => operatorToken.TokenID switch
		{
			RDExpressionToken.Add => left + right,
			RDExpressionToken.Subtract => left - right,
			_ => throw new InvalidOperationException("Invalid operator")
		};
		[NodeName("expression")]
		[Production("expression : term")]
		public static float Expression_Term(float termValue) => termValue;
		[NodeName("multOrDivide")]
		[Production("term : factor Multiply term")]
		[Production("term : factor Divide term")]
		public static float Term(float left, Token<RDExpressionToken> operatorToken, float right) => operatorToken.TokenID switch
		{
			RDExpressionToken.Multiply => left * right,
			RDExpressionToken.Divide => left / right,
			_ => throw new InvalidOperationException("Invalid operator")
		};
		[NodeName("term")]
		[Production("term : factor")]
		public static float TermFactor(float factorValue) => factorValue;
		[NodeName("primary")]
		[Production("factor : primary")]
		public static float PrimaryFactor(float primValue) => primValue;
		[NodeName("negate")]
		[Production("factor : Add factor")]
		[Production("factor : Subtract factor")]
		public static float Factor(Token<RDExpressionToken> symbolToken, float factorValue) => symbolToken.TokenID switch
		{
			RDExpressionToken.Add => factorValue,
			RDExpressionToken.Subtract => -factorValue,
			_ => throw new InvalidOperationException("Invalid operator")
		};
		[NodeName("primary")]
		[Production("primary: VariableInt")]
		[Production("primary: VariableFloat")]
		[Production("primary: VariableBoolean")]
		[Production("primary: PlainString")]
		public float PrimaryVariable(Token<RDExpressionToken> variableToken)
		{
			return variableToken.TokenID switch
			{
				RDExpressionToken.VariableInt => Variables.i[int.Parse(variableToken.Value[1..])],
				RDExpressionToken.VariableFloat => Variables.f[int.Parse(variableToken.Value[1..])],
				RDExpressionToken.VariableBoolean => Variables.b[int.Parse(variableToken.Value[1..])] ? 1.0f : 0.0f,
				RDExpressionToken.PlainString => (float)Variables[variableToken.Value],
				_ => throw new InvalidOperationException("Invalid variable type"),
			};
		}
		[NodeName("string")]
		[Production("argument: String")]
		public static float String(Token<RDExpressionToken> stringToken)
		{
			TokenStack.Push(new(RDExpressionToken.String,
				stringToken.Value switch
				{
				['s', 't', 'r', ':', ..] => stringToken.Value[4..],
				['"', .., '"'] => stringToken.Value[1..^1],
					_ => stringToken.Value,
				},
				new()));
			return 1;
		}
		[NodeName("compare")]
		[Production("expression: term GreaterThanOrEqual expression")]
		[Production("expression: term GreaterThan expression")]
		[Production("expression: term LessThanOrEqual expression")]
		[Production("expression: term LessThan expression")]
		public static float Compare(float left, Token<RDExpressionToken> operatorToken, float right)
		{
			var result = operatorToken.TokenID switch
			{
				RDExpressionToken.GreaterThan => left > right,
				RDExpressionToken.GreaterThanOrEqual => left >= right,
				RDExpressionToken.LessThan => left < right,
				RDExpressionToken.LessThanOrEqual => left <= right,
				_ => throw new InvalidOperationException("Invalid operator")
			};
			return result ? 1.0f : 0.0f;
		}
		[NodeName("assignment")]
		[Production("primary: VariableInt Assignment expression")]
		[Production("primary: VariableFloat Assignment expression")]
		[Production("primary: VariableBoolean Assignment expression")]
		[Production("primary: PlainString Assignment expression")]
		public float Assignment(Token<RDExpressionToken> nameToken, Token<RDExpressionToken> _, float expression)
		{
			var variableName = nameToken.Value;
			switch (variableName[0])
			{
				case 'i':
					Variables.i[int.Parse(variableName[1..])] = (int)expression;
					break;
				case 'f':
					Variables.f[int.Parse(variableName[1..])] = expression;
					break;
				case 'b':
					Variables.b[int.Parse(variableName[1..])] = expression != 0.0f;
					break;
				default:
					Variables[variableName] = expression;
					break;
			}
			return expression;
		}
		[NodeName("function_call")]
		[Production("primary: Identifier LeftParenthesis [d] functionRight")]
		public static float FunctionCallWithArguments(Token<RDExpressionToken> identifierToken, float _) => EvaluateFunction(identifierToken.Value, TokenStack);
		[NodeName("function_right")]
		[Production("functionRight: RightParenthesis [d]")]
		public static float FunctionCall() => 0;
		[NodeName("function_right")]
		[Production("functionRight: Number RightParenthesis [d]")]
		[Production("functionRight: String RightParenthesis [d]")]
		[Production("functionRight: PlainString RightParenthesis [d]")]
		public static float FunctionEnd(Token<RDExpressionToken> valueToken)
		{
			TokenStack.Push(valueToken);
			return 1;
		}
		[NodeName("function_right")]
		[Production("functionRight: expression RightParenthesis [d]")]
		public static float FunctionEnd2(float arg)
		{
			TokenStack.Push(new(RDExpressionToken.Number, arg.ToString(), new()));
			return 1;
		}
		[NodeName("arguments")]
		[Production("functionRight: Number Comma [d] functionRight")]
		[Production("functionRight: String Comma [d] functionRight")]
		[Production("functionRight: PlainString Comma [d] functionRight")]
		public static float ArgumentListWithComma(Token<RDExpressionToken> valueToken, float argument)
		{
			TokenStack.Push(valueToken);
			return 1 + argument;
		}
		[NodeName("arguments")]
		[Production("functionRight: expression Comma [d] functionRight")]
		public static float ArgumentListWithComma2(float arg, float argument)
		{
			TokenStack.Push(new(RDExpressionToken.Number, arg.ToString(), new()));
			return 1 + argument;
		}
		private static float EvaluateFunction(string functionName, Stack<Token<RDExpressionToken>> tokenList)
		{
			return functionName switch
			{
				"Rand" => TokenStack.Count == 1
					? int.TryParse(tokenList.Pop().Value, out int result)
						? RDVariables.Rand(result)
						: throw new ArgumentException("Invalid argument type for Rand function")
					: throw new ArgumentException("Invalid argument count for Rand function"),
				"atLeastRank" => TokenStack.Count == 1
					? RDVariables.atLeastRank(tokenList.Pop().Value)
						? 1.0f
						: 0.0f
					: throw new ArgumentException("Invalid argument count for atLeastRank function"),
				"atLeastNPerfects" => TokenStack.Count == 2
					? RDVariables.atLeastNPerfects(int.Parse(tokenList.Pop().Value), int.Parse(tokenList.Pop().Value))
						? 1.0f
						: 0.0f
					: throw new ArgumentException("Invalid argument count for atLeastNPerfects function"),
				_ => throw new InvalidOperationException($"Unknown function: {functionName}"),
			};
		}
	}
}
