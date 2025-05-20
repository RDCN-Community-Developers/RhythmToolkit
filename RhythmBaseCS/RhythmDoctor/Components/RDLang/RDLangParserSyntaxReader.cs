using System.Collections;
using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	internal enum GroupType
	{
		Expression,// = TokenType.Comma + 1,
		Digit,
		Operator,
		Args,
		Function,
	}
	internal interface IPattern
	{
	}

	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	internal struct PatternGroup(GroupType name) : IPattern, IEnumerable<IPattern>
	{
		public GroupType Name = name;
		public IPattern[] Patterns = [];
		public float FloatValue;
		public string StringValue = "";
		public PatternValue? AssignableToken;
		public void Add(IPattern pattern) => Patterns = [.. Patterns, pattern];
		public readonly IEnumerator<IPattern> GetEnumerator() => (IEnumerator<IPattern>)Patterns.GetEnumerator();
		readonly IEnumerator IEnumerable.GetEnumerator() => Patterns.GetEnumerator();
		public override readonly string ToString() => $"{Name} [{string.Join(", ", Patterns.Select(p => p is PatternGroup pg ? pg.Name.ToString() : p is PatternValue pv ? pv.TokenType.ToString() : ""))}]";
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
		private static readonly PatternGroup[] patterns = [
						new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Digit),},
						new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Function),},
						new PatternGroup(GroupType.Expression){new PatternValue(TokenType.LeftParenthesis), new PatternGroup(GroupType.Expression), new PatternValue(TokenType.RightParenthesis),},
						new PatternGroup(GroupType.Expression){new PatternGroup(GroupType.Operator), new PatternGroup(GroupType.Expression), new PatternGroup(GroupType.Expression),},

						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Integer),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Float),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Boolean),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.VariableInteger),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.VariableFloat),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.VariableBoolean),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.True),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.False),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Add), new PatternValue(TokenType.Integer),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Subtract), new PatternValue(TokenType.Integer),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Add), new PatternValue(TokenType.Float),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Subtract), new PatternValue(TokenType.Float),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Add), new PatternValue(TokenType.VariableInteger),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Subtract), new PatternValue(TokenType.VariableInteger),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Add), new PatternValue(TokenType.VariableFloat),},
						new PatternGroup(GroupType.Digit){new PatternValue(TokenType.Subtract), new PatternValue(TokenType.VariableFloat),},

						new PatternGroup(GroupType.Operator){new PatternValue(TokenType.Add),},
						new PatternGroup(GroupType.Operator){new PatternValue(TokenType.Subtract),},
						new PatternGroup(GroupType.Operator){new PatternValue(TokenType.Multipy),},
						new PatternGroup(GroupType.Operator){new PatternValue(TokenType.Divide),},
						new PatternGroup(GroupType.Operator){new PatternValue(TokenType.Assignment),},
						new PatternGroup(GroupType.Operator){new PatternValue(TokenType.GreaterThan),},
						new PatternGroup(GroupType.Operator){new PatternValue(TokenType.GreaterThanOrEqual),},
						new PatternGroup(GroupType.Operator){new PatternValue(TokenType.LessThan),},
						new PatternGroup(GroupType.Operator){new PatternValue(TokenType.LessThanOrEqual),},

						new PatternGroup(GroupType.Args){new PatternValue(TokenType.String),},
						new PatternGroup(GroupType.Args){new PatternValue(TokenType.StringOrIdentifier),},
						new PatternGroup(GroupType.Args){new PatternGroup(GroupType.Expression),},
						new PatternGroup(GroupType.Args){new PatternGroup(GroupType.Args), new PatternValue(TokenType.Comma), new PatternGroup(GroupType.Args),},

						new PatternGroup(GroupType.Function){new PatternValue(TokenType.StringOrIdentifier), new PatternValue(TokenType.LeftParenthesis), new PatternGroup(GroupType.Args), new PatternValue(TokenType.RightParenthesis),},
					];
		private static readonly Action[,] actions = new Action[,]
				{
					{"s13","","s7","","","s12","s6","","","s5","s4","","","","","","s15","s14","s11","s10","s9","s8",""},
					{"s17","s21","","","s20","","","s22","s23","","","s24","s25","s19","","","","s18","","","","",new()},
					{"r0","r0","r0","r0","r0","r0","r0","r0","r0","r0","r0","r0","r0","r0","r0","","r0","r0","r0","r0","r0","r0","r0"},
					{"r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","r1","","r1","r1","r1","r1","r1","r1","r1"},
					{"s13","","s7","","","s12","s6","","","s5","s4","","","","","","s15","s14","s11","s10","s9","s8",""},
					{"r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","r4","","r4","r4","r4","r4","r4","r4","r4"},
					{"r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","r5","","r5","r5","r5","r5","r5","r5","r5"},
					{"r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","r6","","r6","r6","r6","r6","r6","r6","r6"},
					{"r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","r7","","r7","r7","r7","r7","r7","r7","r7"},
					{"r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","r8","","r8","r8","r8","r8","r8","r8","r8"},
					{"r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","r9","","r9","r9","r9","r9","r9","r9","r9"},
					{"r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","r10","","r10","r10","r10","r10","r10","r10","r10"},
					{"r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","r11","","r11","r11","r11","r11","r11","r11","r11"},
					{"","","","","","","s28","","","s27","","","","","","","","","","","s30","s29",""},
					{"","","","","","","s32","","","s31","","","","","","","","","","","s34","s33",""},
					{"","","","","","","","","","","s35","","","","","","","","","","","",""},
					{"s13","","s7","","","s12","s6","","","s5","s4","","","","","","s15","s14","s11","s10","s9","s8",""},
					{"r20","r20","r20","r20","r20","r20","r20","r20","r20","r20","r20","r20","r20","r20","r20","","r20","r20","r20","r20","r20","r20","r20"},
					{"r21","r21","r21","r21","r21","r21","r21","r21","r21","r21","r21","r21","r21","r21","r21","","r21","r21","r21","r21","r21","r21","r21"},
					{"r22","r22","r22","r22","r22","r22","r22","r22","r22","r22","r22","r22","r22","r22","r22","","r22","r22","r22","r22","r22","r22","r22"},
					{"r23","r23","r23","r23","r23","r23","r23","r23","r23","r23","r23","r23","r23","r23","r23","","r23","r23","r23","r23","r23","r23","r23"},
					{"r24","r24","r24","r24","r24","r24","r24","r24","r24","r24","r24","r24","r24","r24","r24","","r24","r24","r24","r24","r24","r24","r24"},
					{"r25","r25","r25","r25","r25","r25","r25","r25","r25","r25","r25","r25","r25","r25","r25","","r25","r25","r25","r25","r25","r25","r25"},
					{"r26","r26","r26","r26","r26","r26","r26","r26","r26","r26","r26","r26","r26","r26","r26","","r26","r26","r26","r26","r26","r26","r26"},
					{"r27","r27","r27","r27","r27","r27","r27","r27","r27","r27","r27","r27","r27","r27","r27","","r27","r27","r27","r27","r27","r27","r27"},
					{"r28","r28","r28","r28","r28","r28","r28","r28","r28","r28","r28","r28","r28","r28","r28","","r28","r28","r28","r28","r28","r28","r28"},
					{"s17","s21","","","s20","","","s22","s23","","","s24","s25","s19","s37","","","s18","","","","",""},
					{"r12","r12","r12","r12","r12","r12","r12","r12","r12","r12","r12","r12","r12","r12","r12","","r12","r12","r12","r12","r12","r12","r12"},
					{"r14","r14","r14","r14","r14","r14","r14","r14","r14","r14","r14","r14","r14","r14","r14","","r14","r14","r14","r14","r14","r14","r14"},
					{"r16","r16","r16","r16","r16","r16","r16","r16","r16","r16","r16","r16","r16","r16","r16","","r16","r16","r16","r16","r16","r16","r16"},
					{"r18","r18","r18","r18","r18","r18","r18","r18","r18","r18","r18","r18","r18","r18","r18","","r18","r18","r18","r18","r18","r18","r18"},
					{"r13","r13","r13","r13","r13","r13","r13","r13","r13","r13","r13","r13","r13","r13","r13","","r13","r13","r13","r13","r13","r13","r13"},
					{"r15","r15","r15","r15","r15","r15","r15","r15","r15","r15","r15","r15","r15","r15","r15","","r15","r15","r15","r15","r15","r15","r15"},
					{"r17","r17","r17","r17","r17","r17","r17","r17","r17","r17","r17","r17","r17","r17","r17","","r17","r17","r17","r17","r17","r17","r17"},
					{"r19","r19","r19","r19","r19","r19","r19","r19","r19","r19","r19","r19","r19","r19","r19","","r19","r19","r19","r19","r19","r19","r19"},
					{"s13","","s7","","","s12","s6","","","s5","s4","","","","","s39","s40","s14","s11","s10","s9","s8",""},
					{"s17/r3","s21/r3","r3","r3","s20/r3","r3","r3","s22/r3","s23/r3","r3","r3","s24/r3","s25/r3","s19/r3","r3","","r3","s18/r3","r3","r3","r3","r3","r3"},
					{"r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","r2","","r2","r2","r2","r2","r2","r2","r2"},
					{"","","","s43","","","","","","","","","","","s42","","","","","","","",""},
					{"","","","r29","","","","","","","","","","","r29","","","","","","","",""},
					{"","","","r30","","","","","","","s35","","","","r30","","","","","","","",""},
					{"s17","s21","","r31","s20","","","s22","s23","","","s24","s25","s19","r31","","","s18","","","","",""},
					{"r33","r33","r33","r33","r33","r33","r33","r33","r33","r33","r33","r33","r33","r33","r33","","r33","r33","r33","r33","r33","r33","r33"},
					{"s13","","s7","","","s12","s6","","","s5","s4","","","","","s39","s40","s14","s11","s10","s9","s8",""},
					{"","","","s43/r32","","","","","","","","","","","r32","","","","","","","",""},
				};
		private static readonly List<TokenType> actionIndexes =
			[
			TokenType.Add,
			TokenType.Assignment,
			TokenType.Boolean,
			TokenType.Comma,
			TokenType.Divide,
			TokenType.False,
			TokenType.Float,
			TokenType.GreaterThan,
			TokenType.GreaterThanOrEqual,
			TokenType.Integer,
			TokenType.LeftParenthesis,
			TokenType.LessThan,
			TokenType.LessThanOrEqual,
			TokenType.Multipy,
			TokenType.RightParenthesis,
			TokenType.String,
			TokenType.StringOrIdentifier,
			TokenType.Subtract,
			TokenType.True,
			TokenType.VariableBoolean,
			TokenType.VariableFloat,
			TokenType.VariableInteger,
		];
		private static readonly Action[,] goTos = new Action[,]
		{
			{1,2,"","",3,},
			{"","",16,"","",},
			{"","","","","",},
			{"","","","","",},
			{26,2,"","",3,},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{36,2,"","",3,},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","",16,"","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{41,2,"",38,3,},
			{"","",16,"","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","","","","",},
			{"","",16,"","",},
			{"","","","","",},
			{41,2,"",44,3,},
			{"","","","","",},
		};
		private static readonly List<GroupType> goToIndexes =
			[
			GroupType.Expression,
			GroupType.Digit,
			GroupType.Operator,
			GroupType.Args,
			GroupType.Function,
		];
		private static readonly List<HashSet<TokenType>> priority = [
			[TokenType.Assignment],
			[TokenType.GreaterThan, TokenType.GreaterThanOrEqual, TokenType.LessThan, TokenType.LessThanOrEqual],
			[TokenType.Add, TokenType.Subtract],
			[TokenType.Multipy, TokenType.Divide],
		];
		private static Stack<PatternValue> inputStack = [];
		private static readonly Stack<IPattern> symbolStack = [];
		private static readonly Stack<int> stateStack = [];
		private static Action GetAction(int state, TokenType tokenType) => actions[state, actionIndexes.IndexOf(tokenType)];
		private static Action GetLastAction(int state) => actions[state, actions.GetLength(1) - 1];
		private static Action GetGoTo(int state, GroupType groupType) => goTos[state, goToIndexes.IndexOf(groupType)];
		private static void Func(Token[] tokens, RDVariables variables)
		{
			inputStack = new(tokens.Select(i => new PatternValue(i.TokenID) { Value = i }).Reverse());
			symbolStack.Clear();
			stateStack.Clear();
			stateStack.Push(0);
			while (true)
			{
				//Console.WriteLine($"State: {stateStack.Peek()}");
				//Console.WriteLine($"Input: {string.Join(", ", inputStack.Select(i => i.ToString()))}");
				//Console.WriteLine($"Symbol: {string.Join(", ", symbolStack.Select(i => i.ToString()).Reverse())}");
				//Console.WriteLine();
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
						stateStack.Push(GetGoTo(stateStack.Peek(), ps.Name).ShiftTo);
						break;
					case ActionType.Both:
						ps = patterns[action.ReduceTo];
						int inputPriority = priority.FindIndex(p => p.Contains(peekPattern.TokenType));
						IPattern reduceRight = ps.Patterns.Last();
						while (reduceRight is PatternGroup pv)
						{
							ps = patterns.FirstOrDefault(p => p.Name == pv.Name);
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
						return;
					default:
						throw new Exception($"Unknown action type: {action.ActionType}");
				}
			}
		}
	}
}