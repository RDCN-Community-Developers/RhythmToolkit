using System.Data;
using System.Diagnostics;

namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	internal enum RDLangType
	{
		Statement,
		Expression,
	}
	/// <summary>
	/// Represents the types of tokens that can be identified in the RDLang parser.
	/// </summary>
	public enum TokenType
	{
		/// <summary>
		/// Represents an integer value.
		/// </summary>
		Integer,

		/// <summary>
		/// Represents a floating-point value.
		/// </summary>
		Float,

		/// <summary>
		/// Represents a string value.
		/// </summary>
		String,

		/// <summary>
		/// Represents a string or an identifier.
		/// </summary>
		StringOrIdentifier,

		/// <summary>
		/// Represents a variable holding an integer value.
		/// </summary>
		VariableInteger,

		/// <summary>
		/// Represents a variable holding a floating-point value.
		/// </summary>
		VariableFloat,

		/// <summary>
		/// Represents a variable holding a boolean value.
		/// </summary>
		VariableBoolean,

		/// <summary>
		/// Represents the addition operator.
		/// </summary>
		OperatorAdd,

		/// <summary>
		/// Represents the subtraction operator.
		/// </summary>
		OperatorSubtract,

		/// <summary>
		/// Represents the multiplication operator.
		/// </summary>
		OperatorMultipy,

		/// <summary>
		/// Represents the division operator.
		/// </summary>
		OperatorDivide,

		/// <summary>
		/// Represents the increment operator.
		/// </summary>
		OperatorIncreasement,

		/// <summary>
		/// Represents the decrement operator.
		/// </summary>
		OperatorDecreasement,

		/// <summary>
		/// Represents the logical AND operator.
		/// </summary>
		OperatorAnd,

		/// <summary>
		/// Represents the logical OR operator.
		/// </summary>
		OperatorOr,

		/// <summary>
		/// Represents the logical NOT operator.
		/// </summary>
		OperatorNot,

		/// <summary>
		/// Represents the boolean value 'true'.
		/// </summary>
		True,

		/// <summary>
		/// Represents the boolean value 'false'.
		/// </summary>
		False,

		/// <summary>
		/// Represents the assignment operator.
		/// </summary>
		OperatorAssignment,

		/// <summary>
		/// Represents the greater-than-or-equal-to operator.
		/// </summary>
		OperatorGreaterThanOrEqual,

		/// <summary>
		/// Represents the greater-than operator.
		/// </summary>
		OperatorGreaterThan,

		/// <summary>
		/// Represents the less-than-or-equal-to operator.
		/// </summary>
		OperatorLessThanOrEqual,

		/// <summary>
		/// Represents the less-than operator.
		/// </summary>
		OperatorLessThan,

		/// <summary>
		/// Represents a left parenthesis '('.
		/// </summary>
		LeftParenthesis,

		/// <summary>
		/// Represents a right parenthesis ')'.
		/// </summary>
		RightParenthesis,

		/// <summary>
		/// Represents a left bracket '['.
		/// </summary>
		LeftBracket,

		/// <summary>
		/// Represents a right bracket ']'.
		/// </summary>
		RightBracket,

		/// <summary>
		/// Represents a comma ','.
		/// </summary>
		Comma,

		/// <summary>
		/// Represents a dot '.'.
		/// </summary>
		Dot,
	}

	/// <summary>
	/// Represents a token in the RDLang parser.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
#if NETSTANDARD
	public struct Token
#else
	public readonly struct Token
#endif
	{
		/// <summary>
		/// Gets the name of the token based on its type and value.
		/// </summary>
		public readonly string Name => TokenID switch
		{
			TokenType.VariableInteger => "i" + (int)Value,
			TokenType.VariableFloat => "f" + (int)Value,
			TokenType.VariableBoolean => "b" + (int)Value,
			_ => Value?.ToString() ?? "",
		};

		/// <summary>
		/// Gets or sets the value of the token.
		/// </summary>
#if NETSTANDARD
		public object Value { get; internal set; }
#else
		public object Value { get; internal init; }
#endif
		/// <summary>
		/// Gets or sets the type of the token.
		/// </summary>
#if NETSTANDARD
		public TokenType TokenID { get; internal set; }
#else
		public TokenType TokenID { get; internal init; }
#endif

		/// <summary>
		/// Gets or sets the line number where the token is located.
		/// </summary>
#if NETSTANDARD
		public int Line { get; internal set; }
#else
		public int Line { get; internal init; }
#endif

		/// <summary>
		/// Gets or sets the column number where the token is located.
		/// </summary>
#if NETSTANDARD
		public int Column { get; internal set; }
#else
		public int Column { get; internal init; }
#endif

		///<inherit />
		public readonly override string ToString() => $"{Name} [{TokenID}] at {Line}:{Column}";
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
			float result = Run(tokens, RDLang.Variables, type, out Error? error);
			if (error is null)
				return result;
			else
			{
				if (error?.Column == -1)
				{
					error = new Error($"{error?.Message} At line {error?.Line}, column {error?.Column}.")
					{
#if NETSTANDARD
						Line = tokens[tokens.Length - 1].Line,
						Column = tokens[tokens.Length - 1].Column,
						Name = tokens[tokens.Length - 1].Name
#else
						Line = tokens[^1].Line,
						Column = tokens[^1].Column,
						Name = tokens[^1].Name
#endif
					};
				}
				throw new SyntaxErrorException($"{error?.Message} At line {error?.Line}, column {error?.Column}.")
				{
					Data = {
						["Line"] = error?.Line,
						["Column"] = error ?.Column,
						["Name"] = error ?.Name
					},
				};
			}
		}
		public static bool TryRun(string code, RDLangType type, out float result)
		{
			result = 0;
			if (string.IsNullOrEmpty(code))
				return false;
			Token[] tokens = ReadAsToken(code);
			try
			{
				result = Run(tokens, RDLang.Variables, type, out Error? error);
				return true;
			}
			catch
			{
				return false;
			}
		}
		public static Token[] Analyze(string code)
		{
			return ReadAsToken(code);
		}
	}
}