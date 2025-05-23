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
		private static Stack<PatternValue> inputStack = [];
		private static readonly Stack<IPattern> symbolStack = [];
		private static readonly Stack<int> stateStack = [];
		private static Action GetAction(int state, TokenType tokenType) => actions[state, actionIndexes.IndexOf(tokenType)];
		private static Action GetLastAction(int state, RDLangType type) => type switch
		{
			RDLangType.Statement => actions[state, actions.GetLength(1) - 1],
			RDLangType.Expression => actions2[state, actions.GetLength(1) - 1],
			_ => throw new NotImplementedException(),
		};
		private static Action GetGoTo(int state, GroupType groupType, RDLangType type) => type switch
		{
			RDLangType.Statement => goTos[state, goToIndexes.IndexOf(groupType)],
			RDLangType.Expression => goTos2[state, goToIndexes.IndexOf(groupType)],
			_ => throw new NotImplementedException(),
		};
		private static float Run(Token[] tokens, RDVariables variables, RDLangType type)
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
					action = GetLastAction(state, type);
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
						stateStack.Push(GetGoTo(stateStack.Peek(), ps.GroupType, type).ShiftTo);
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