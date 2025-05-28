using RhythmBase.Global.Exceptions;
using System.Reflection;

namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	partial class RDLangParser
	{
		private static readonly Type variablesType = typeof(RDVariables);
		private static void Calculate(ref PatternGroup pattern, IPattern[] values, RDVariables variables)
		{
			switch (pattern.GroupType)
			{
				case GroupType.Sentence:
					switch (values)
					{
						case [
								PatternGroup { GroupType: GroupType.Identifier } pg0,
								PatternValue { TokenType: TokenType.OperatorIncreasement }
							]:
							pattern.FloatValue = pg0.FloatValue + 1f;
							break;
						case [
								PatternGroup { GroupType: GroupType.Identifier } pg0,
								PatternValue { TokenType: TokenType.OperatorDecreasement }
							]:
							pattern.FloatValue = pg0.FloatValue - 1f;
							break;
						case [
								PatternGroup { GroupType: GroupType.Identifier } pg0,
								PatternValue { TokenType: TokenType.OperatorAssignment },
								PatternGroup { GroupType: GroupType.Expression } pg1
							]:
							FieldAssignment(pg0.AssignableToken, pg1.FloatValue, variables);
							break;
						case [PatternGroup { GroupType: GroupType.Expression } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							break;
					}
					break;
				case GroupType.Identifier:
					switch (values)
					{
						case [PatternGroup { GroupType: GroupType.Variable } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							pattern.AssignableToken = pg0.AssignableToken;
							break;
						case [
								PatternValue { TokenType: TokenType.StringOrIdentifier } pg0,
								PatternValue { TokenType: TokenType.Dot },
								PatternGroup { GroupType: GroupType.Identifier } pg1,
							]:
							pattern.StringValue = $"{pg0.Value.Value}.{pg1.StringValue}";
							pattern.AssignableToken = [new((string)pg0.Value.Value), ..pg1.AssignableToken];
							break;
						case [
								PatternValue { TokenType: TokenType.StringOrIdentifier } pg0,
								PatternValue { TokenType: TokenType.LeftBracket },
								PatternValue { TokenType: TokenType.Integer } pg1,
								PatternValue { TokenType: TokenType.RightBracket },
								PatternValue { TokenType: TokenType.Dot },
								PatternGroup { GroupType: GroupType.Identifier } pg2,
							]:
							pattern.StringValue = $"{(string)pg0.Value.Value}[{pg1.Value.Value}].{pg2.StringValue}";
							pattern.AssignableToken = [new((string)pg0.Value.Value, (int)pg1.Value.Value), .. pg2.AssignableToken];
							break;
					}
					break;
				case GroupType.Variable:
					switch (values)
					{
						case [PatternValue { TokenType: TokenType.VariableInteger } pg0,]:
							pattern.FloatValue = variables.i[(int)pg0.Value.Value];
							pattern.AssignableToken = [new($"i{pg0.Value.Value}")];
							break;
						case [PatternValue { TokenType: TokenType.VariableFloat } pg0,]:
							pattern.FloatValue = variables.f[(int)pg0.Value.Value];
							pattern.AssignableToken = [new($"f{pg0.Value.Value}")];
							break;
						case [PatternValue { TokenType: TokenType.VariableBoolean } pg0,]:
							pattern.FloatValue = variables.b[(int)pg0.Value.Value] ? 1f : 0f;
							pattern.AssignableToken = [new($"b{pg0.Value.Value}")];
							break;
						case [PatternValue { TokenType: TokenType.StringOrIdentifier } pg0,]:
							object? tokenValue = pg0.Value.TokenID switch
							{
								TokenType.VariableInteger => variables.i[(int)pg0.Value.Value],
								TokenType.VariableFloat => variables.f[(int)pg0.Value.Value],
								TokenType.VariableBoolean => variables.b[(int)pg0.Value.Value],
								TokenType.StringOrIdentifier => variables[(string)pg0.Value.Value],
								_ => null,
							};
							pattern.FloatValue = tokenValue is float f ? f : 0f;
							pattern.StringValue = (string)pg0.Value.Value;
							pattern.AssignableToken = [new((string)pg0.Value.Value)];
							break;
						case [
								PatternValue { TokenType: TokenType.StringOrIdentifier } pg0,
								PatternValue { TokenType: TokenType.LeftBracket },
								PatternValue { TokenType: TokenType.Integer } pg1,
								PatternValue { TokenType: TokenType.RightBracket },
							]:
							pattern.FloatValue = variables.i[(int)pg1.Value.Value];
							pattern.AssignableToken = [new((string)pg0.Value.Value, (int)pg1.Value.Value)];
							break;
					}
					break;
				case GroupType.Expression:
					switch (values)
					{
						case [PatternGroup { GroupType: GroupType.Number } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							pattern.StringValue = pg0.StringValue;
							pattern.AssignableToken = pg0.AssignableToken;
							break;
						case [PatternGroup { GroupType: GroupType.Function } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							break;
						case [
								PatternValue { TokenType: TokenType.LeftParenthesis },
								PatternGroup { GroupType: GroupType.Expression } pg1,
								PatternValue { TokenType: TokenType.RightParenthesis },
							]:
							pattern.FloatValue = pg1.FloatValue;
							break;
						case [
								PatternGroup { GroupType: GroupType.Expression } pg0,
								PatternGroup { GroupType: GroupType.Operator, Patterns: [PatternValue pg1pv] },
								PatternGroup { GroupType: GroupType.Expression } pg2,
							]:
							switch (pg1pv.TokenType)
							{
								case TokenType.OperatorAdd:
									pattern.FloatValue = pg0.FloatValue + pg2.FloatValue;
									break;
								case TokenType.OperatorSubtract:
									pattern.FloatValue = pg0.FloatValue - pg2.FloatValue;
									break;
								case TokenType.OperatorMultipy:
									pattern.FloatValue = pg0.FloatValue * pg2.FloatValue;
									break;
								case TokenType.OperatorDivide:
									pattern.FloatValue = pg0.FloatValue / pg2.FloatValue;
									break;
								case TokenType.OperatorAssignment:
									pattern.FloatValue = pg0.FloatValue == pg2.FloatValue ? 1f : 0f;
									break;
								case TokenType.OperatorGreaterThan:
									pattern.FloatValue = pg0.FloatValue > pg2.FloatValue ? 1f : 0f;
									break;
								case TokenType.OperatorGreaterThanOrEqual:
									pattern.FloatValue = pg0.FloatValue >= pg2.FloatValue ? 1f : 0f;
									break;
								case TokenType.OperatorLessThan:
									pattern.FloatValue = pg0.FloatValue < pg2.FloatValue ? 1f : 0f;
									break;
								case TokenType.OperatorLessThanOrEqual:
									pattern.FloatValue = pg0.FloatValue <= pg2.FloatValue ? 1f : 0f;
									break;
								case TokenType.OperatorAnd:
									pattern.FloatValue = (pg0.FloatValue != 0f && pg2.FloatValue != 0f) ? 1f : 0f;
									break;
							}
							break;
						case [
								PatternValue { TokenType: TokenType.OperatorAdd },
								PatternGroup { GroupType: GroupType.Expression } pg1,
							]:
							pattern.FloatValue = pg1.FloatValue;
							break;
						case [
								PatternValue { TokenType: TokenType.OperatorSubtract },
								PatternGroup { GroupType: GroupType.Expression } pg1,
							]:
							pattern.FloatValue = -pg1.FloatValue;
							break;
					}
					break;
				case GroupType.Number:
					switch (values)
					{
						case [PatternValue { TokenType: TokenType.Integer } pg0,]:
							pattern.FloatValue = (int)pg0.Value.Value;
							break;
						case [PatternValue { TokenType: TokenType.Float } pg0,]:
							pattern.FloatValue = (float)pg0.Value.Value;
							break;
						case [PatternValue { TokenType: TokenType.True },]:
							pattern.FloatValue = 1f;
							break;
						case [PatternValue { TokenType: TokenType.False },]:
							pattern.FloatValue = 0f;
							break;
					}
					break;
				case GroupType.Operator:
					break;
				case GroupType.Args:
					switch (values)
					{
						case [PatternValue { TokenType: TokenType.String } pg0,]:
							pattern.StringValue = (string)pg0.Value.Value;
							break;
						case [PatternGroup { GroupType: GroupType.Expression } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							break;
						case [
								PatternGroup { GroupType: GroupType.Args },
								PatternValue { TokenType: TokenType.Comma },
								PatternGroup { GroupType: GroupType.Args },
							]:
							pattern.Patterns = values;
							break;
					}
					break;
				case GroupType.Function:
					switch (values)
					{
						case [
								PatternGroup { GroupType: GroupType.Identifier } pg0,
								PatternValue { TokenType: TokenType.LeftParenthesis },
								PatternGroup { GroupType: GroupType.Args } pg1,
								PatternValue { TokenType: TokenType.RightParenthesis },
							]:
							Stack<PatternGroup> argList = new();
							while (pg1.Patterns is [
								PatternGroup { GroupType: GroupType.Args } subpg1,
								PatternValue { TokenType: TokenType.Comma },
								PatternGroup { GroupType: GroupType.Args } subpg2])
							{
								argList.Push(subpg2);
								pg1 = subpg1;
							}
							argList.Push(pg1);
							pattern.FloatValue = FunctionCalling(pg0, [.. argList], variables);
							break;
						case [
								PatternGroup { GroupType: GroupType.Identifier } pg0,
								PatternValue { TokenType: TokenType.LeftParenthesis },
								PatternValue { TokenType: TokenType.RightParenthesis },
							]:
							pattern.FloatValue = FunctionCalling(pg0, [], variables);
							break;
					}
					break;
			}
		}
		private static float FunctionCalling(PatternGroup identifier, PatternGroup[] args, RDVariables variables)
		{
			string identifierName = string.Join('.', identifier.AssignableToken.Select(i=>i.ToString()));
			switch (identifierName)
			{
				case "Rand":
					if (args.Length == 1)
						return RDVariables.Rand((int)args[0].FloatValue);
					throw new RhythmBaseException("Invalid number of arguments for 'Rand'. Expected 1 argument.");
				case "atLeastRank":
					if (args.Length == 1)
						return RDVariables.atLeastRank(args[0].StringValue) ? 1f : 0f;
					throw new RhythmBaseException("Invalid number of arguments for 'atLeastRank'. Expected 1 argument.");
				case "atLeastNPerfects":
					if (args.Length == 2)
						return RDVariables.atLeastNPerfects((int)args[0].FloatValue, (int)args[1].FloatValue) ? 1f : 0f;
					throw new RhythmBaseException("Invalid number of arguments for 'atLeastNPerfects'. Expected 2 arguments.");
				case "IIf":
					if (args.Length == 3)
						return args[0].FloatValue != 0f ? args[1].FloatValue : args[2].FloatValue;
					throw new RhythmBaseException("Invalid number of arguments for 'IIf'. Expected 3 arguments.");
				default:
					throw new Exception($"Function '{identifier}' not found.");
			}
		}
		private static void FieldAssignment(IdentifierToken[] tokens, float value, RDVariables variables)
		{
			if (tokens.Length == 1)
			{
				variables[tokens[0].Identifier] = value;
				return;
			}
			PropertyInfo? info = null;
			object? key = RDLang.Variables;
			for (int i = 0; i < tokens.Length; i++)
			{
				if (tokens[i].Index >= 0)
				{
					info = variablesType.GetProperty(tokens[i].Identifier);
					key = info?.GetValue(key) ?? null;
					info = info?.PropertyType.GetProperty("Item");
					key = info?.GetValue(key, [tokens[i].Index]) ?? null;
				}
				else
				{
					info = variablesType.GetProperty(tokens[i].Identifier);
					key = info?.GetValue(key) ?? null;
				}
			}
			if (info != null && key != null)
			{
				info.SetValue(key, value);
			}
		}
	}
}
