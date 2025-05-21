using System.Collections;
using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	internal enum GroupType
	{
		Sentence,
		Identifier,
		Variable,
		Expression,
		Number,
		Operator,
		Args,
		Function,
	}
	internal interface IPattern { }

	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	internal struct IdentifierToken
	{
		public string Identifier;
		public int Index;
		public IdentifierToken(string identifier)
		{
			Identifier = identifier;
			Index = -1;
		}
		public IdentifierToken(string identifier, int index)
		{
			Identifier = identifier;
			Index = index;
		}
		public override readonly string ToString() => Index >= 0 ? $"{Identifier}[{Index}]" : Identifier;
		private readonly string GetDebuggerDisplay() => ToString();
	}

	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	internal struct PatternGroup(GroupType name) : IPattern, IEnumerable<IPattern>
	{
		public GroupType GroupType = name;
		public IPattern[] Patterns = [];
		public float FloatValue;
		public string StringValue = "";
		public IdentifierToken[] AssignableToken = [];
		public void Add(IPattern pattern) => Patterns = [.. Patterns, pattern];
		public readonly IEnumerator<IPattern> GetEnumerator() => (IEnumerator<IPattern>)Patterns.GetEnumerator();
		readonly IEnumerator IEnumerable.GetEnumerator() => Patterns.GetEnumerator();
		public override readonly string ToString() => $"{GroupType} [{string.Join(", ", Patterns.Select(p => p is PatternGroup pg ? pg.GroupType.ToString() : p is PatternValue pv ? pv.TokenType.ToString() : ""))}]";
		private readonly string GetDebuggerDisplay() => ToString();
	}

	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	internal struct PatternValue(TokenType tokenType) : IPattern
	{
		public TokenType TokenType = tokenType;
		public Token Value;
		public static implicit operator PatternValue(TokenType tokenType) => new(tokenType);
		public override readonly string ToString() => TokenType.ToString();
		private readonly string GetDebuggerDisplay() => ToString();
	}
	internal enum ActionType
	{
		Shift = 0b01,
		Reduce = 0b10,
		Both = 0b11,
		Error = 0b100,
		Accept = 0b00,
	}
	internal enum ActionTarget
	{
		Action,
		GoTo,
	}
	internal enum GoToType
	{
		Shift = 0b01,
		Error = 0b10,
	}

	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	internal struct Action()
	{
		public ActionType ActionType = ActionType.Accept;
		public int ShiftTo;
		public int ReduceTo;
		public string ErrorInfo = "";
		public ActionTarget ActionTarget = ActionTarget.Action;
		public static implicit operator Action(string s)
		{
			string[] parts = s.Split('/');
			if (parts.Length == 2)
			{
				return new()
				{
					ActionType = ActionType.Both,
					ShiftTo = int.Parse(parts[0][1..]),
					ReduceTo = int.Parse(parts[1][1..]),
				};
			}
			else
			{
				if (s.Length == 0)
					return new() { ActionType = ActionType.Error, ErrorInfo = s };
				return s[0] switch
				{
					's' => new() { ActionType = ActionType.Shift, ShiftTo = int.Parse(s[1..]) },
					'r' => new() { ActionType = ActionType.Reduce, ReduceTo = int.Parse(s[1..]) },
					_ => new() { ErrorInfo = s, ActionType = ActionType.Error },
				};
			}
		}
		public static implicit operator Action(int shiftTo) => new() { ShiftTo = shiftTo, ActionType = ActionType.Reduce, ActionTarget = ActionTarget.GoTo };
		public override readonly string ToString()
		{
			return ActionType switch
			{
				ActionType.Shift => $"S{ShiftTo}",
				ActionType.Reduce => $"R{ReduceTo}",
				ActionType.Both => $"S{ShiftTo}/R{ReduceTo}",
				ActionType.Error => ErrorInfo,
				ActionType.Accept => $"ACC",
				_ => throw new NotImplementedException(),
			};
		}
		private readonly string GetDebuggerDisplay() => ToString();
	}
	partial class RDLangParser
	{
		/*
Sentence ->
	| Identifier OperatorIncreasement
	| Identifier OperatorDecreasement
	| Identifier OperatorAssignment Expression
	| Function

Identifier ->
	| Variable
	| StringOrIdentifier Dot Identifier
	| StringOrIdentifier LeftBracket Integer RightBracket Dot Identifier

Variable ->
	| VariableInteger
	| VariableFloat
	| VariableBoolean
	| StringOrIdentifier
	| StringOrIdentifier LeftBracket Integer RightBracket

Expression ->
	| Number
	| Function
    | LeftParenthesis Expression RightParenthesis
	| Expression Operator Expression
	| OperatorAdd Expression
	| OperatorSubtract Expression

Number ->
	| Integer
	| Float
	| Boolean
	| Identifier
	| True
	| False

Operator ->
	| OperatorAdd
	| OperatorSubtract
	| OperatorMultipy
	| OperatorDivide
	| OperatorAssignment
	| OperatorGreaterThan
	| OperatorGreaterThanOrEqual
	| OperatorLessThan
	| OperatorLessThanOrEqual
	| OperatorAnd
	| OperatorOr
	| OperatorNot

Args ->
	| String
	| Expression
	| Args Comma Args

Function ->
	| Identifier LeftParenthesis RightParenthesis
	| Identifier LeftParenthesis Args RightParenthesis
		 */
		private static readonly PatternGroup[] patterns = [
			new PatternGroup(GroupType.Sentence){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.OperatorIncreasement),},
			new PatternGroup(GroupType.Sentence){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.OperatorDecreasement),},
			new PatternGroup(GroupType.Sentence){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.OperatorAssignment), new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Sentence){new PatternGroup(GroupType.Function),},

			new PatternGroup(GroupType.Identifier){new PatternGroup(GroupType.Variable),},
			new PatternGroup(GroupType.Identifier){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.Dot), new PatternGroup(GroupType.Identifier) },
			new PatternGroup(GroupType.Identifier){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.LeftBracket), new PatternValue(TokenType.Integer), new PatternValue(TokenType.RightBracket),},

			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.VariableInteger),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.VariableFloat),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.VariableBoolean),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.StringOrIdentifier),},
			new PatternGroup(GroupType.Variable){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.LeftBracket), new PatternValue(TokenType.Integer), new PatternValue(TokenType.RightBracket),},

			new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Number),},
			new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Function),},
			new PatternGroup(GroupType.Expression){new PatternValue(TokenType.LeftParenthesis), new PatternGroup(GroupType.Expression), new PatternValue(TokenType.RightParenthesis),},
			new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Expression), new PatternGroup(GroupType.Operator), new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Expression){new PatternValue(TokenType.OperatorAdd), new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Expression){new PatternValue(TokenType.OperatorSubtract), new PatternGroup(GroupType.Expression),},

			new PatternGroup(GroupType.Number){new PatternValue(TokenType.Integer),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.Float),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.Boolean),},
			new PatternGroup(GroupType.Number){new PatternGroup(GroupType.Identifier),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.True),},
			new PatternGroup(GroupType.Number){new PatternValue(TokenType.False),},

			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorAdd),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorSubtract),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorMultipy),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorDivide),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorAssignment),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorGreaterThan),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorGreaterThanOrEqual),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorLessThan),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorLessThanOrEqual),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorAnd),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorOr),},
			new PatternGroup(GroupType.Operator){new PatternValue(TokenType.OperatorNot),},

			new PatternGroup(GroupType.Args){new PatternValue(TokenType.String),},
			new PatternGroup(GroupType.Args){new PatternGroup(GroupType.Expression),},
			new PatternGroup(GroupType.Args){new PatternGroup(GroupType.Args), new PatternValue(TokenType.Comma), new PatternGroup(GroupType.Args),},

			new PatternGroup(GroupType.Function){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.LeftParenthesis), new PatternValue(TokenType.RightParenthesis),},
			new PatternGroup(GroupType.Function){new PatternGroup(GroupType.Identifier), new PatternValue(TokenType.LeftParenthesis), new PatternGroup(GroupType.Args), new PatternValue(TokenType.RightParenthesis),},
					];
		private static readonly Action[,] actions = new Action[,]
				{
{"","","","","","","","","","","","","","","","","","","","","","","","","","s5","","s8","s7","s6",""},
{"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","",new()},
{"","","","","","","","s12","","","s11","s10","","","","s9","","","","","","","","","","","","","","",""},
{"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","r3"},
{"r4","r4","","r4","r4","r4","","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","","r4","","r4","r4","r4","r4","r4","r4"},
{"r10","r10","s13","r10","r10","r10","s14","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","","r10","","r10","r10","r10","r10","r10","r10"},
{"r7","r7","","r7","r7","r7","","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","","r7","","r7","r7","r7","r7","r7","r7"},
{"r8","r8","","r8","r8","r8","","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","","r8","","r8","r8","r8","r8","r8","r8"},
{"r9","r9","","r9","r9","r9","","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","","r9","","r9","r9","r9","r9","r9","r9"},
{"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","r0"},
{"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","","r1"},
{"s23","","","s26","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s25","s8","s7","s6",""},
{"s23","","","s26","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","s27","s29","s5","s25","s8","s7","s6",""},
{"","","","","","","","","","","","","","","","","","","","","","","","","","s5","","s8","s7","s6",""},
{"","","","","","s32","","","","","","","","","","","","","","","","","","","","","","","","",""},
{"","","","","","","","","s34","s43","s38","","s37","s39","s40","","s41","s42","s36","s45","s44","s35","","","","","","","","","r2"},
{"r12","r12","","r12","r12","r12","","r12","r12","r12","r12","","r12","r12","r12","","r12","r12","r12","r12","r12","r12","","r12","","r12","r12","r12","r12","r12","r12"},
{"r13","r13","","r13","r13","r13","","r13","r13","r13","r13","","r13","r13","r13","","r13","r13","r13","r13","r13","r13","","r13","","r13","r13","r13","r13","r13","r13"},
{"s23","","","s26","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s25","s8","s7","s6",""},
{"s23","","","s26","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s25","s8","s7","s6",""},
{"s23","","","s26","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s25","s8","s7","s6",""},
{"r18","r18","","r18","r18","r18","","r18","r18","r18","r18","","r18","r18","r18","","r18","r18","r18","r18","r18","r18","","r18","","r18","r18","r18","r18","r18","r18"},
{"r19","r19","","r19","r19","r19","","r19","r19","r19","r19","","r19","r19","r19","","r19","r19","r19","r19","r19","r19","","r19","","r19","r19","r19","r19","r19","r19"},
{"r20","r20","","r20","r20","r20","","r20","r20","r20","r20","","r20","r20","r20","","r20","r20","r20","r20","r20","r20","","r20","","r20","r20","r20","r20","r20","r20"},
{"r21","r21","","r21","r21","r21","","s12","r21","r21","r21","","r21","r21","r21","","r21","r21","r21","r21","r21","r21","","r21","","r21","r21","r21","r21","r21","r21"},
{"r22","r22","","r22","r22","r22","","r22","r22","r22","r22","","r22","r22","r22","","r22","r22","r22","r22","r22","r22","","r22","","r22","r22","r22","r22","r22","r22"},
{"r23","r23","","r23","r23","r23","","r23","r23","r23","r23","","r23","r23","r23","","r23","r23","r23","r23","r23","r23","","r23","","r23","r23","r23","r23","r23","r23"},
{"r39","r39","","r39","r39","r39","","r39","r39","r39","r39","","r39","r39","r39","","r39","r39","r39","r39","r39","r39","","r39","","r39","r39","r39","r39","r39","r39"},
{"","s50","","","","","","","","","","","","","","","","","","","","","","s49","","","","","","",""},
{"","r36","","","","","","","","","","","","","","","","","","","","","","r36","","","","","","",""},
{"","r37","","","","","","","s34","s43","s38","","s37","s39","s40","","s41","s42","s36","s45","s44","s35","","r37","","","","","","",""},
{"r5","r5","","r5","r5","r5","","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","","r5","","r5","r5","r5","r5","r5","r5"},
{"","","","","","","","","","","","","","","","","","","","","","","s51","","","","","","","",""},
{"s23","","","s26","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","","s5","s25","s8","s7","s6",""},
{"r24","r24","","r24","r24","r24","","r24","r24","r24","r24","","r24","r24","r24","","r24","r24","r24","r24","r24","r24","","r24","","r24","r24","r24","r24","r24","r24"},
{"r25","r25","","r25","r25","r25","","r25","r25","r25","r25","","r25","r25","r25","","r25","r25","r25","r25","r25","r25","","r25","","r25","r25","r25","r25","r25","r25"},
{"r26","r26","","r26","r26","r26","","r26","r26","r26","r26","","r26","r26","r26","","r26","r26","r26","r26","r26","r26","","r26","","r26","r26","r26","r26","r26","r26"},
{"r27","r27","","r27","r27","r27","","r27","r27","r27","r27","","r27","r27","r27","","r27","r27","r27","r27","r27","r27","","r27","","r27","r27","r27","r27","r27","r27"},
{"r28","r28","","r28","r28","r28","","r28","r28","r28","r28","","r28","r28","r28","","r28","r28","r28","r28","r28","r28","","r28","","r28","r28","r28","r28","r28","r28"},
{"r29","r29","","r29","r29","r29","","r29","r29","r29","r29","","r29","r29","r29","","r29","r29","r29","r29","r29","r29","","r29","","r29","r29","r29","r29","r29","r29"},
{"r30","r30","","r30","r30","r30","","r30","r30","r30","r30","","r30","r30","r30","","r30","r30","r30","r30","r30","r30","","r30","","r30","r30","r30","r30","r30","r30"},
{"r31","r31","","r31","r31","r31","","r31","r31","r31","r31","","r31","r31","r31","","r31","r31","r31","r31","r31","r31","","r31","","r31","r31","r31","r31","r31","r31"},
{"r32","r32","","r32","r32","r32","","r32","r32","r32","r32","","r32","r32","r32","","r32","r32","r32","r32","r32","r32","","r32","","r32","r32","r32","r32","r32","r32"},
{"r33","r33","","r33","r33","r33","","r33","r33","r33","r33","","r33","r33","r33","","r33","r33","r33","r33","r33","r33","","r33","","r33","r33","r33","r33","r33","r33"},
{"r34","r34","","r34","r34","r34","","r34","r34","r34","r34","","r34","r34","r34","","r34","r34","r34","r34","r34","r34","","r34","","r34","r34","r34","r34","r34","r34"},
{"r35","r35","","r35","r35","r35","","r35","r35","r35","r35","","r35","r35","r35","","r35","r35","r35","r35","r35","r35","","r35","","r35","r35","r35","r35","r35","r35"},
{"","","","","","","","","s34","s43","s38","","s37","s39","s40","","s41","s42","s36","s45","s44","s35","","s53","","","","","","",""},
{"r16","r16","","r16","r16","r16","","r16","s34","s43","s38","","s37","s39","s40","","s41","s42","s36","s45","s44","s35","","r16","","r16","r16","r16","r16","r16","r16"},
{"r17","r17","","r17","r17","r17","","r17","s34","s43","s38","","s37","s39","s40","","s41","s42","s36","s45","s44","s35","","r17","","r17","r17","r17","r17","r17","r17"},
{"r40","r40","","r40","r40","r40","","r40","r40","r40","r40","","r40","r40","r40","","r40","r40","r40","r40","r40","r40","","r40","","r40","r40","r40","r40","r40","r40"},
{"s23","","","s26","s22","s21","","s18","s19","","","","","","","","","","","","","s20","","","s29","s5","s25","s8","s7","s6",""},
{"r11","r11","s55","r11","r11","r11","","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","","r11","","r11","r11","r11","r11","r11","r11"},
{"r15","r15","","r15","r15","r15","","r15","s34","s43","s38","","s37","s39","s40","","s41","s42","s36","s45","s44","s35","","r15","","r15","r15","r15","r15","r15","r15"},
{"r14","r14","","r14","r14","r14","","r14","r14","r14","r14","","r14","r14","r14","","r14","r14","r14","r14","r14","r14","","r14","","r14","r14","r14","r14","r14","r14"},
{"","s50","","","","","","","","","","","","","","","","","","","","","","r38","","","","","","",""},
{"","","","","","","","","","","","","","","","","","","","","","","","","","s5","","s8","s7","s6",""},
{"r6","r6","","r6","r6","r6","","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","","r6","","r6","r6","r6","r6","r6","r6"},
				};
		private static readonly List<TokenType> actionIndexes =
			[
			TokenType.Boolean,
			TokenType.Comma,
			TokenType.Dot,
			TokenType.False,
			TokenType.Float,
			TokenType.Integer,
			TokenType.LeftBracket,
			TokenType.LeftParenthesis,
			TokenType.OperatorAdd,
			TokenType.OperatorAnd,
			TokenType.OperatorAssignment,
			TokenType.OperatorDecreasement,
			TokenType.OperatorDivide,
			TokenType.OperatorGreaterThan,
			TokenType.OperatorGreaterThanOrEqual,
			TokenType.OperatorIncreasement,
			TokenType.OperatorLessThan,
			TokenType.OperatorLessThanOrEqual,
			TokenType.OperatorMultipy,
			TokenType.OperatorNot,
			TokenType.OperatorOr,
			TokenType.OperatorSubtract,
			TokenType.RightBracket,
			TokenType.RightParenthesis,
			TokenType.String,
			TokenType.StringOrIdentifier,
			TokenType.OperatorSubtract,
			TokenType.True,
			TokenType.VariableBoolean,
			TokenType.VariableFloat,
			TokenType.VariableInteger,
		];
		private static readonly Action[,] goTos = new Action[,]
		{
{1,2,4,"","","","",3},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",24,4,15,16,"","",17},
{"",24,4,30,16,"",28,17},
{"",31,4,"","","","",""},
{"","","","","","","",""},
{"","","","","",33,"",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",24,4,46,16,"","",17},
{"",24,4,47,16,"","",17},
{"",24,4,48,16,"","",17},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","",33,"",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",24,4,52,16,"","",17},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"","","","","",33,"",""},
{"","","","","",33,"",""},
{"","","","","",33,"",""},
{"","","","","","","",""},
{"",24,4,30,16,"",54,17},
{"","","","","","","",""},
{"","","","","",33,"",""},
{"","","","","","","",""},
{"","","","","","","",""},
{"",56,4,"","","","",""},
{"","","","","","","",""},
		};
		private static readonly List<GroupType> goToIndexes =
			[
			GroupType.Sentence,
			GroupType.Identifier,
			GroupType.Variable,
			GroupType.Expression,
			GroupType.Number,
			GroupType.Operator,
			GroupType.Args,
			GroupType.Function,
		];
		private static readonly List<HashSet<TokenType>> priority = [
			[TokenType.OperatorAssignment],
			[TokenType.OperatorGreaterThan, TokenType.OperatorGreaterThanOrEqual, TokenType.OperatorLessThan, TokenType.OperatorLessThanOrEqual],
			[TokenType.OperatorAdd, TokenType.OperatorSubtract],
			[TokenType.OperatorMultipy, TokenType.OperatorDivide],
		];
		private static Stack<PatternValue> inputStack = [];
		private static readonly Stack<IPattern> symbolStack = [];
		private static readonly Stack<int> stateStack = [];
		private static Action GetAction(int state, TokenType tokenType) => actions[state, actionIndexes.IndexOf(tokenType)];
		private static Action GetLastAction(int state) => actions[state, actions.GetLength(1) - 1];
		private static Action GetGoTo(int state, GroupType groupType) => goTos[state, goToIndexes.IndexOf(groupType)];
		private static float Run(Token[] tokens, RDVariables variables)
		{
			inputStack = new(tokens.Select(i => new PatternValue(i.TokenID) { Value = i }).Reverse());
			symbolStack.Clear();
			stateStack.Clear();
			stateStack.Push(0);
			while (true)
			{
				Console.WriteLine($"State: {stateStack.Peek()}");
				Console.WriteLine($"Input: {string.Join(", ", inputStack.Select(i => i.ToString()))}");
				Console.WriteLine($"Symbol: {string.Join(", ", symbolStack.Select(i => i.ToString()).Reverse())}");
				Console.WriteLine();
				int state = stateStack.Peek();
				Action action;
				if (!inputStack.TryPeek(out PatternValue peekPattern))
					action = GetLastAction(state);
				else
					action = GetAction(state, peekPattern.TokenType);
				switch (action.ActionType)
				{
					case ActionType.Shift:
					shift:
						symbolStack.Push(inputStack.Pop());
						stateStack.Push(action.ShiftTo);
						break;
					case ActionType.Reduce:
					reduce:
						Stack<IPattern> ptargs = [];
						var ps = patterns[action.ReduceTo];
						for (int i = 0; i < ps.Patterns.Length; i++)
						{
							var peak = symbolStack.Pop();
							ptargs.Push(peak);
							stateStack.Pop();
							if (peak.GetType() != ps.Patterns[^(i + 1)].GetType())
								throw new Exception($"Pattern mismatch at state {state}: expected {ps.Patterns[i].GetType()}, got {peak.GetType()}");
						}
						Calculate(ref ps, [.. ptargs], variables);
						symbolStack.Push(ps);
						stateStack.Push(GetGoTo(stateStack.Peek(), ps.GroupType).ShiftTo);
						break;
					case ActionType.Both:
						ps = patterns[action.ReduceTo];
						int inputPriority = priority.FindIndex(p => p.Contains(peekPattern.TokenType));
						IPattern reduceRight = ps.Patterns.Last();
						while (reduceRight is PatternGroup pv)
						{
							ps = patterns.FirstOrDefault(p => p.GroupType == pv.GroupType);
							reduceRight = ps.Patterns.Last();
						}
						int reducePriority = priority.FindIndex(p => p.Contains(((PatternValue)reduceRight).TokenType));
						if (inputPriority > reducePriority)
							goto shift;
						else
							goto reduce;
					case ActionType.Error:
						throw new Exception($"Error at state {state}: {action.ErrorInfo}");
					case ActionType.Accept:
						return ((PatternGroup)symbolStack.Single()).FloatValue;
					default:
						throw new Exception($"Unknown action type: {action.ActionType}");
				}
			}
		}
	}
}