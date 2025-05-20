using RhythmBase.Global.Exceptions;

namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	partial class RDLangParser
	{
		/*
Expression ->
	| Digit
	| Function
    | leftParenthesis Expression rightParenthesis
	| Expression Operator Expression

Digit ->
	| integer
	| float
	| boolean
	| variableInteger
	| variableFloat
	| variableBoolean
	| true
	| false
    | add integer
    | subtract integer
    | add float
    | subtract float
    | add variableInteger
    | subtract variableInteger
    | add variableFloat
    | subtract variableFloat

Operator ->
	| add
	| subtract
	| multipy
	| divide
	| assignment
	| greaterThan
	| greaterThanOrEqual
	| lessThan
	| lessThanOrEqual

Args ->
	| string
	| stringOrIdentifier
	| Expression
	| Args comma Args

Function ->
	| stringOrIdentifier leftParenthesis Args rightParenthesis
		 */
		private static readonly Stack<IPattern> argsStack = new();
		private static void Calculate(ref PatternGroup pattern, IPattern[] values, RDVariables variables)
		{
			switch (pattern.Name)
			{
				case GroupType.Expression:
					switch (values)
					{
						case [PatternGroup { Name: GroupType.Digit } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							pattern.StringValue = pg0.StringValue;
							pattern.AssignableToken = pg0.AssignableToken;
							break;
						case [PatternGroup { Name: GroupType.Function } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							break;
						case [
								PatternValue { TokenType: TokenType.LeftParenthesis },
								PatternGroup { Name: GroupType.Expression } pg1,
								PatternValue { TokenType: TokenType.RightParenthesis },
							]:
							pattern.FloatValue = pg1.FloatValue;
							break;
						case [
								PatternGroup { Name: GroupType.Expression } pg0,
								PatternGroup { Name: GroupType.Operator } pg1,
								PatternGroup { Name: GroupType.Expression } pg2,
							]:
							switch (((PatternValue)(pg1.Patterns[0])).TokenType)
							{
								case TokenType.Add:
									pattern.FloatValue = pg0.FloatValue + pg2.FloatValue;
									break;
								case TokenType.Subtract:
									pattern.FloatValue = pg0.FloatValue - pg2.FloatValue;
									break;
								case TokenType.Multipy:
									pattern.FloatValue = pg0.FloatValue * pg2.FloatValue;
									break;
								case TokenType.Divide:
									pattern.FloatValue = pg0.FloatValue / pg2.FloatValue;
									break;
								case TokenType.Assignment:
									if (pg0.AssignableToken is PatternValue token)
									{
										pattern.FloatValue = pg0.FloatValue = pg2.FloatValue;
										if (token.Value.Value is string value)
											pattern.StringValue = value;
										switch (token.Value.TokenID)
										{
											case TokenType.VariableInteger:
												variables.i[(int)token.Value.Value] = (int)pg2.FloatValue;
												break;
											case TokenType.VariableFloat:
												variables.f[(int)token.Value.Value] = pg2.FloatValue;
												break;
											case TokenType.VariableBoolean:
												variables.b[(int)token.Value.Value] = pg2.FloatValue != 0f;
												break;
											case TokenType.StringOrIdentifier:
												variables[(string)token.Value.Value] = pg2.FloatValue;
												break;
											default:
												break;
										}
										break;
									}
									else
										pattern.FloatValue = pg0.FloatValue == pg2.FloatValue ? 1f : 0f;
									break;
								case TokenType.GreaterThan:
									pattern.FloatValue = pg0.FloatValue > pg2.FloatValue ? 1f : 0f;
									break;
								case TokenType.GreaterThanOrEqual:
									pattern.FloatValue = pg0.FloatValue >= pg2.FloatValue ? 1f : 0f;
									break;
								case TokenType.LessThan:
									pattern.FloatValue = pg0.FloatValue < pg2.FloatValue ? 1f : 0f;
									break;
								case TokenType.LessThanOrEqual:
									pattern.FloatValue = pg0.FloatValue <= pg2.FloatValue ? 1f : 0f;
									break;
								default:
									break;
							}
							break;
						default:
							break;
					}
					break;
				case GroupType.Digit:
					switch (values)
					{
						case [PatternValue { TokenType: TokenType.Integer } pg0,]:
							pattern.FloatValue = (int)pg0.Value.Value;
							break;
						case [PatternValue { TokenType: TokenType.Float } pg0,]:
							pattern.FloatValue = (float)pg0.Value.Value;
							break;
						case [PatternValue { TokenType: TokenType.Boolean } pg0,]:
							pattern.FloatValue = (bool)pg0.Value.Value ? 1f : 0f;
							break;
						case [PatternValue { TokenType: TokenType.VariableInteger } pg0,]:
							pattern.FloatValue = variables.i[(int)pg0.Value.Value];
							pattern.AssignableToken = pg0;
							break;
						case [PatternValue { TokenType: TokenType.VariableFloat } pg0,]:
							pattern.FloatValue = variables.f[(int)pg0.Value.Value];
							pattern.AssignableToken = pg0;
							break;
						case [PatternValue { TokenType: TokenType.VariableBoolean } pg0,]:
							pattern.FloatValue = variables.b[(int)pg0.Value.Value] ? 1f : 0f;
							pattern.AssignableToken = pg0;
							break;
						case [PatternValue { TokenType: TokenType.True },]:
							pattern.FloatValue = 1f;
							break;
						case [PatternValue { TokenType: TokenType.False },]:
							pattern.FloatValue = 0f;
							break;
						case [PatternValue { TokenType: TokenType.Add }, PatternValue { TokenType: TokenType.Integer } pg1,]:
							pattern.FloatValue = (int)pg1.Value.Value;
							break;
						case [PatternValue { TokenType: TokenType.Subtract }, PatternValue { TokenType: TokenType.Integer } pg1,]:
							pattern.FloatValue = -(int)pg1.Value.Value;
							break;
						case [PatternValue { TokenType: TokenType.Add }, PatternValue { TokenType: TokenType.Float } pg1,]:
							pattern.FloatValue = (float)pg1.Value.Value;
							break;
						case [PatternValue { TokenType: TokenType.Subtract }, PatternValue { TokenType: TokenType.Float } pg1,]:
							pattern.FloatValue = -(float)pg1.Value.Value;
							break;
						case [PatternValue { TokenType: TokenType.Add }, PatternValue { TokenType: TokenType.VariableInteger } pg1,]:
							pattern.FloatValue = variables.i[(int)pg1.Value.Value];
							break;
						case [PatternValue { TokenType: TokenType.Subtract }, PatternValue { TokenType: TokenType.VariableInteger } pg1,]:
							pattern.FloatValue = -variables.i[(int)pg1.Value.Value];
							break;
						case [PatternValue { TokenType: TokenType.Add }, PatternValue { TokenType: TokenType.VariableFloat } pg1,]:
							pattern.FloatValue = variables.f[(int)pg1.Value.Value];
							break;
						case [PatternValue { TokenType: TokenType.Subtract }, PatternValue { TokenType: TokenType.VariableFloat } pg1,]:
							pattern.FloatValue = -variables.f[(int)pg1.Value.Value];
							break;
						default:
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
						case [PatternValue { TokenType: TokenType.StringOrIdentifier } pg0,]:
							object? tokenValue = pg0.Value.TokenID switch
							{
								TokenType.VariableInteger => variables.i[(int)pg0.Value.Value],
								TokenType.VariableFloat => variables.f[(int)pg0.Value.Value],
								TokenType.VariableBoolean => variables.b[(int)pg0.Value.Value],
								TokenType.StringOrIdentifier => variables[(string)pg0.Value.Value],
								_ => null,
							};
							pattern.AssignableToken = pg0;
							break;
						case [PatternGroup { Name: GroupType.Expression } pg0,]:
							pattern.FloatValue = pg0.FloatValue;
							break;
						case [
								PatternGroup { Name: GroupType.Args },
								PatternValue { TokenType: TokenType.Comma },
								PatternGroup { Name: GroupType.Args },
							]:
							pattern.Patterns = values;
							break;
						default:
							break;
					}
					break;
				case GroupType.Function:
					switch (values)
					{
						case [
								PatternValue { TokenType: TokenType.StringOrIdentifier } pg0,
								PatternValue { TokenType: TokenType.LeftParenthesis },
								PatternGroup { Name: GroupType.Args } pg1,
								PatternValue { TokenType: TokenType.RightParenthesis },
							]:
							Stack<PatternGroup> argList = new();
							while (pg1.Patterns is [
								PatternGroup { Name: GroupType.Args } subpg1,
								PatternValue { TokenType: TokenType.Comma },
								PatternGroup { Name: GroupType.Args } subpg2])
							{
								argList.Push(subpg2);
								pg1 = subpg1;
							}
							argList.Push(pg1);
							float value = FunctionCalling((string)pg0.Value.Value, [.. argList], variables);
							pattern.FloatValue = value;
							break;
						default:
							break;
					}
					break;
				default:
					break;
			}
		}
		private static float FunctionCalling(string identifier, PatternGroup[] args, RDVariables variables)
		{
			switch (identifier)
			{
				case "rand":
					if (args.Length == 1)
						return RDVariables.Rand((int)args[0].FloatValue);
					throw new RhythmBaseException("Invalid number of arguments for 'rand'. Expected 1 argument.");
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
		private static void FieldAssignment(string identifier, IPattern[] args, RDVariables variables)
		{

		}
	}
}
