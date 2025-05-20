using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	internal enum TokenType
	{
		Integer,
		Float,
		Boolean,
		String,
		StringOrIdentifier,

		VariableInteger,
		VariableFloat,
		VariableBoolean,

		Add,
		Subtract,
		Multipy,
		Divide,

		True,
		False,

		Assignment,
		GreaterThanOrEqual,
		GreaterThan,
		LessThanOrEqual,
		LessThan,

		LeftParenthesis,
		RightParenthesis,

		Comma,
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
			string vstr = Value?.ToString()??"";
			if(TokenID is TokenType.VariableInteger)
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
		public static void TryParse(string code)
		{
			if (string.IsNullOrEmpty(code))
				return;
			Token[] tokens = ReadAsToken(code);
			Func(tokens, RDLang.Variables);
		}
	}
}
