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
#if NETSTANDARD
					if (values.Length == 2 &&
						values[0] is PatternGroup { GroupType: GroupType.Identifier } pg00 &&
						values[1] is PatternValue { TokenType: TokenType.OperatorIncreasement })
					{
						pattern.FloatValue = pg00.FloatValue + 1f;
					}
					else if (values.Length == 2 &&
						values[0] is PatternGroup { GroupType: GroupType.Identifier } pg01 &&
						values[1] is PatternValue { TokenType: TokenType.OperatorDecreasement })
					{
						pattern.FloatValue = pg01.FloatValue - 1f;
					}
					else if (values.Length == 3 &&
						values[0] is PatternGroup { GroupType: GroupType.Identifier } pg02 &&
						values[1] is PatternValue { TokenType: TokenType.OperatorAssignment } &&
						values[2] is PatternGroup { GroupType: GroupType.Expression } pg03)
					{
						FieldAssignment(pg02.AssignableToken, pg03.FloatValue, variables);
						pattern.FloatValue = pg03.FloatValue;
					}
					else if (values.Length == 1 &&
						values[0] is PatternGroup { GroupType: GroupType.Expression } pg04)
					{
						pattern.FloatValue = pg04.FloatValue;
					}
#else
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
							pattern.FloatValue = pg1.FloatValue;
							break;
						case [PatternGroup { GroupType: GroupType.Expression } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							break;
					}
#endif
					break;
				case GroupType.Identifier:
#if NETSTANDARD
					if (values.Length == 1 &&
						values[0] is PatternGroup { GroupType: GroupType.Variable } pg10)
					{
						pattern.FloatValue = pg10.FloatValue;
						pattern.AssignableToken = pg10.AssignableToken;
					}
					else if (values.Length == 3 &&
						values[0] is PatternValue { TokenType: TokenType.StringOrIdentifier } pg11 &&
						values[1] is PatternValue { TokenType: TokenType.Dot } &&
						values[2] is PatternGroup { GroupType: GroupType.Identifier } pg12)
					{
						pattern.StringValue = $"{pg11.Token.Value}.{pg12.StringValue}";
						pattern.AssignableToken = [new((string)pg11.Token.Value), .. pg12.AssignableToken];
					}
					else if (values.Length == 6 &&
						values[0] is PatternValue { TokenType: TokenType.StringOrIdentifier } pg13 &&
						values[1] is PatternValue { TokenType: TokenType.LeftBracket } &&
						values[2] is PatternValue { TokenType: TokenType.Integer } pg14 &&
						values[3] is PatternValue { TokenType: TokenType.RightBracket } &&
						values[4] is PatternValue { TokenType: TokenType.Dot } &&
						values[5] is PatternGroup { GroupType: GroupType.Identifier } pg15)
					{
						pattern.StringValue = $"{(string)pg13.Token.Value}[{pg14.Token.Value}].{pg15.StringValue}";
						pattern.AssignableToken = [new((string)pg13.Token.Value, (int)pg14.Token.Value), .. pg15.AssignableToken];
					}
#else
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
							pattern.StringValue = $"{pg0.Token.Value}.{pg1.StringValue}";
							pattern.AssignableToken = [new((string)pg0.Token.Value), .. pg1.AssignableToken];
							break;
						case [
								PatternValue { TokenType: TokenType.StringOrIdentifier } pg0,
								PatternValue { TokenType: TokenType.LeftBracket },
								PatternValue { TokenType: TokenType.Integer } pg1,
								PatternValue { TokenType: TokenType.RightBracket },
								PatternValue { TokenType: TokenType.Dot },
								PatternGroup { GroupType: GroupType.Identifier } pg2,
							]:
							pattern.StringValue = $"{(string)pg0.Token.Value}[{pg1.Token.Value}].{pg2.StringValue}";
							pattern.AssignableToken = [new((string)pg0.Token.Value, (int)pg1.Token.Value), .. pg2.AssignableToken];
							break;
					}
#endif
					break;
				case GroupType.Variable:
#if NETSTANDARD
					if (values.Length == 1 &&
						values[0] is PatternValue { TokenType: TokenType.VariableInteger } pg20)
					{
						pattern.FloatValue = variables.i[(int)pg20.Token.Value];
						pattern.AssignableToken = [new($"i{pg20.Token.Value}")];
					}
					else if (values.Length == 1 &&
						values[0] is PatternValue { TokenType: TokenType.VariableFloat } pg21)
					{
						pattern.FloatValue = variables.f[(int)pg21.Token.Value];
						pattern.AssignableToken = [new($"f{pg21.Token.Value}")];
					}
					else if (values.Length == 1 &&
						values[0] is PatternValue { TokenType: TokenType.VariableBoolean } pg22)
					{
						pattern.FloatValue = variables.b[(int)pg22.Token.Value] ? 1f : 0f;
						pattern.AssignableToken = [new($"b{pg22.Token.Value}")];
					}
					else if (values.Length == 1 &&
						values[0] is PatternValue { TokenType: TokenType.StringOrIdentifier } pg23)
					{
						object? tokenValue = pg23.Token.TokenID switch
						{
							TokenType.VariableInteger => variables.i[(int)pg23.Token.Value],
							TokenType.VariableFloat => variables.f[(int)pg23.Token.Value],
							TokenType.VariableBoolean => variables.b[(int)pg23.Token.Value],
							TokenType.StringOrIdentifier => variables[(string)pg23.Token.Value],
							_ => null,
						};
						pattern.FloatValue = tokenValue switch
						{
							int i => i,
							float f => f,
							bool b => b ? 1f : 0f,
							_ => 0f,
						};
						if (tokenValue is string str){
							pattern.StringValue = str;
							pattern.AssignableToken = [new(str)];
						}
					}
					else if (values.Length == 4 &&
						values[0] is PatternValue { TokenType: TokenType.StringOrIdentifier } pg24 &&
						values[1] is PatternValue { TokenType: TokenType.LeftBracket } &&
						values[2] is PatternValue { TokenType: TokenType.Integer } pg25 &&
						values[3] is PatternValue { TokenType: TokenType.RightBracket })
					{
						pattern.AssignableToken = [new((string)pg24.Token.Value, (int)pg25.Token.Value)];
					}
#else
					switch (values)
					{
						case [PatternValue { TokenType: TokenType.VariableInteger } pg0,]:
							pattern.FloatValue = variables.i[(int)pg0.Token.Value];
							pattern.AssignableToken = [new($"i{pg0.Token.Value}")];
							break;
						case [PatternValue { TokenType: TokenType.VariableFloat } pg0,]:
							pattern.FloatValue = variables.f[(int)pg0.Token.Value];
							pattern.AssignableToken = [new($"f{pg0.Token.Value}")];
							break;
						case [PatternValue { TokenType: TokenType.VariableBoolean } pg0,]:
							pattern.FloatValue = variables.b[(int)pg0.Token.Value] ? 1f : 0f;
							pattern.AssignableToken = [new($"b{pg0.Token.Value}")];
							break;
						case [PatternValue { TokenType: TokenType.StringOrIdentifier } pg0,]:
							object? tokenValue = pg0.Token.TokenID switch
							{
								TokenType.VariableInteger => variables.i[(int)pg0.Token.Value],
								TokenType.VariableFloat => variables.f[(int)pg0.Token.Value],
								TokenType.VariableBoolean => variables.b[(int)pg0.Token.Value],
								TokenType.StringOrIdentifier => variables[(string)pg0.Token.Value],
								_ => null,
							};
							pattern.FloatValue = tokenValue switch
							{
								int i => i,
								float f => f,
								bool b => b ? 1f : 0f,
								_ => 0f,
							};
							if (tokenValue is string str)
							{
								pattern.StringValue = str;
								pattern.AssignableToken = [new(str)];
							}
							break;
						case [
								PatternValue { TokenType: TokenType.StringOrIdentifier } pg0,
								PatternValue { TokenType: TokenType.LeftBracket },
								PatternValue { TokenType: TokenType.Integer } pg1,
								PatternValue { TokenType: TokenType.RightBracket },
							]:
							pattern.AssignableToken = [new((string)pg0.Token.Value, (int)pg1.Token.Value)];
							break;
					}
#endif
					break;
				case GroupType.Expression:
#if NETSTANDARD
					if (values.Length == 1 &&
						values[0] is PatternGroup { GroupType: GroupType.Number } pg30)
					{
						pattern.FloatValue = pg30.FloatValue;
						pattern.StringValue = pg30.StringValue;
						pattern.AssignableToken = pg30.AssignableToken;
					}
					else if (values.Length == 1 &&
						values[0] is PatternGroup { GroupType: GroupType.Function } pg31)
					{
						pattern.FloatValue = pg31.FloatValue;
					}
					else if (values.Length == 3 &&
						values[0] is PatternValue { TokenType: TokenType.LeftParenthesis } &&
						values[1] is PatternGroup { GroupType: GroupType.Expression } pg32 &&
						values[2] is PatternValue { TokenType: TokenType.RightParenthesis })
					{
						pattern.FloatValue = pg32.FloatValue;
					}
					else if (values.Length == 3 &&
						values[0] is PatternGroup { GroupType: GroupType.Expression } pg33 &&
						values[1] is PatternGroup { GroupType: GroupType.Operator, Patterns: IPattern[] pv } &&
							pv.Length == 1 && pv[0] is PatternValue pg34pv &&
						values[2] is PatternGroup { GroupType: GroupType.Expression } pg35)
					{
						switch (pg34pv.TokenType)
						{
							case TokenType.OperatorAdd:
								pattern.FloatValue = pg33.FloatValue + pg35.FloatValue;
								break;
							case TokenType.OperatorSubtract:
								pattern.FloatValue = pg33.FloatValue - pg35.FloatValue;
								break;
							case TokenType.OperatorMultipy:
								pattern.FloatValue = pg33.FloatValue * pg35.FloatValue;
								break;
							case TokenType.OperatorDivide:
								pattern.FloatValue = pg33.FloatValue / pg35.FloatValue;
								break;
							case TokenType.OperatorAssignment:
								pattern.FloatValue = pg33.FloatValue == pg35.FloatValue ? 1f : 0f;
								break;
							case TokenType.OperatorGreaterThan:
								pattern.FloatValue = pg33.FloatValue > pg35.FloatValue ? 1f : 0f;
								break;
							case TokenType.OperatorGreaterThanOrEqual:
								pattern.FloatValue = pg33.FloatValue >= pg35.FloatValue ? 1f : 0f;
								break;
							case TokenType.OperatorLessThan:
								pattern.FloatValue = pg33.FloatValue < pg35.FloatValue ? 1f : 0f;
								break;
							case TokenType.OperatorLessThanOrEqual:
								pattern.FloatValue = pg33.FloatValue <= pg35.FloatValue ? 1f : 0f;
								break;
							case TokenType.OperatorAnd:
								pattern.FloatValue = (pg33.FloatValue != 0f && pg35.FloatValue != 0f) ? 1f : 0f;
								break;
						}
					}
					else if (values.Length == 2 &&
						values[0] is PatternValue { TokenType: TokenType.OperatorAdd } &&
						values[1] is PatternGroup { GroupType: GroupType.Expression } pg36)
					{
						pattern.FloatValue = pg36.FloatValue;
					}
					else if (values.Length == 2 &&
						values[0] is PatternValue { TokenType: TokenType.OperatorSubtract } &&
						values[1] is PatternGroup { GroupType: GroupType.Expression } pg37)
					{
						pattern.FloatValue = -pg37.FloatValue;
					}
#else
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
#endif
					break;
				case GroupType.Number:
#if NETSTANDARD
					if (values.Length == 1 &&
						values[0] is PatternGroup { GroupType: GroupType.Identifier } pg40)
					{
						pattern.FloatValue = (int)pg40.FloatValue;
						pattern.StringValue = pg40.StringValue;
						pattern.AssignableToken = pg40.AssignableToken;
					}
					else if (values.Length == 1 &&
						values[0] is PatternValue { TokenType: TokenType.Integer } pg41)
					{
						pattern.FloatValue = (int)pg41.Token.Value;
					}
					else if (values.Length == 1 &&
						values[0] is PatternValue { TokenType: TokenType.Float } pg42)
					{
						pattern.FloatValue = (float)pg42.Token.Value;
					}
					else if (values.Length == 1 &&
						values[0] is PatternValue { TokenType: TokenType.True })
					{
						pattern.FloatValue = 1f;
					}
					else if (values.Length == 1 &&
						values[0] is PatternValue { TokenType: TokenType.False })
					{
						pattern.FloatValue = 0f;
					}
#else
					switch (values)
					{
						case [PatternGroup { GroupType: GroupType.Identifier } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							pattern.StringValue = pg0.StringValue;
							pattern.AssignableToken = pg0.AssignableToken;
							break;
						case [PatternValue { TokenType: TokenType.Integer } pg0,]:
							pattern.FloatValue = (int)pg0.Token.Value;
							break;
						case [PatternValue { TokenType: TokenType.Float } pg0,]:
							pattern.FloatValue = (float)pg0.Token.Value;
							break;
						case [PatternValue { TokenType: TokenType.True },]:
							pattern.FloatValue = 1f;
							break;
						case [PatternValue { TokenType: TokenType.False },]:
							pattern.FloatValue = 0f;
							break;
					}
#endif
					break;
				case GroupType.Operator:
					break;
				case GroupType.Args:
#if NETSTANDARD
					if (values.Length == 1 &&
						values[0] is PatternGroup { GroupType: GroupType.Expression } pg50)
					{
						pattern.FloatValue = pg50.FloatValue;
					}
					else if (values.Length == 3 &&
						values[0] is PatternGroup { GroupType: GroupType.Args } pg51 &&
						values[1] is PatternValue { TokenType: TokenType.Comma } &&
						values[2] is PatternGroup { GroupType: GroupType.Args } pg52)
					{
						pattern.Patterns = [.. pg51.Patterns, .. pg52.Patterns];
					}
#else
					switch (values)
					{
						case [PatternValue { TokenType: TokenType.String } pg0,]:
							pattern.StringValue = (string)pg0.Token.Value;
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
#endif
					break;
				case GroupType.Function:
#if NETSTANDARD
					if (values.Length == 4 &&
						values[0] is PatternGroup { GroupType: GroupType.Identifier } pg60 &&
						values[1] is PatternValue { TokenType: TokenType.LeftParenthesis } &&
						values[2] is PatternGroup { GroupType: GroupType.Args } pg61 &&
						values[3] is PatternValue { TokenType: TokenType.RightParenthesis })
					{
						Stack<PatternGroup> argList = new();
						while (pg61.Patterns.Length == 3 &&
							pg61.Patterns[0] is PatternGroup { GroupType: GroupType.Args } subpg1 &&
							pg61.Patterns[1] is PatternValue { TokenType: TokenType.Comma } &&
							pg61.Patterns[2] is PatternGroup { GroupType: GroupType.Args } subpg2)
						{
							argList.Push(subpg2);
							pg61 = subpg1;
						}
						argList.Push(pg61);
						pattern.FloatValue = FunctionCalling(pg60, [.. argList], variables);
					}
					else if (values.Length == 3 &&
						values[0] is PatternGroup { GroupType: GroupType.Identifier } pg62 &&
						values[1] is PatternValue { TokenType: TokenType.LeftParenthesis } &&
						values[2] is PatternValue { TokenType: TokenType.RightParenthesis })
					{
						pattern.FloatValue = FunctionCalling(pg62, [], variables);
					}
#else
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
#endif
					break;
			}
		}
		private static float FunctionCalling(PatternGroup identifier, PatternGroup[] args, RDVariables variables)
		{
#if NETSTANDARD
			string identifierName = string.Join(".", identifier.AssignableToken.Select(i => i.ToString()));
#else
			string identifierName = string.Join('.', identifier.AssignableToken.Select(i => i.ToString()));
#endif
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
