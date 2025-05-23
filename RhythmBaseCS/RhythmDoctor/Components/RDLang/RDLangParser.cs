using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	internal enum RDLangType
	{
		Statement,
		Expression,
	}
	internal enum TokenType
	{
		Integer,
		Float,
		String,
		StringOrIdentifier,

		VariableInteger,
		VariableFloat,
		VariableBoolean,

		OperatorAdd,
		OperatorSubtract,
		OperatorMultipy,
		OperatorDivide,
		OperatorIncreasement,
		OperatorDecreasement,
		OperatorAnd,
		OperatorOr,
		OperatorNot,

		True,
		False,

		OperatorAssignment,
		OperatorGreaterThanOrEqual,
		OperatorGreaterThan,
		OperatorLessThanOrEqual,
		OperatorLessThan,

		LeftParenthesis,
		RightParenthesis,
		LeftBracket,
		RightBracket,

		Comma,
		Dot,
	}

	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	internal struct Token
	{
		public object Value { get; set; }
		public TokenType TokenID { get; set; }
		public int Line { get; set; }
		public int Column { get; set; }
		public readonly override string ToString()
		{
			string vstr = Value?.ToString() ?? "";
			if (TokenID is TokenType.VariableInteger)
				vstr = "i" + vstr;
			else if (TokenID is TokenType.VariableFloat)
				vstr = "f" + vstr;
			else if (TokenID is TokenType.VariableBoolean)
				vstr = "b" + vstr;
			return $"{vstr} [{TokenID}] at {Line}:{Column}";
		}
		private readonly string GetDebuggerDisplay()
		{
			return ToString();
		}
	}
	internal static partial class RDLangParser
	{
		public static float Run(string code, RDLangType type)
		{
			if (string.IsNullOrEmpty(code))
				return 0;
			Token[] tokens = ReadAsToken(code);
			return Run(tokens, RDLang.Variables, type);
		}
		public static bool TryRun(string code, RDLangType type, out float result)
		{
			result = 0;
			if (string.IsNullOrEmpty(code))
				return false;
			Token[] tokens = ReadAsToken(code);
			try
			{
				result = Run(tokens, RDLang.Variables, type);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}