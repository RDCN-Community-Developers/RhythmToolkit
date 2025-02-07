using sly.lexer;

namespace RhythmBase.Components.RDLang
{
	internal enum RDExpressionToken
	{
		[Lexeme(@"[0-9]+(\.[0-9]+)?")]
		Number,
		[Lexeme("[i][0-9]")]
		VariableInt,
		[Lexeme("[f][0-9]")]
		VariableFloat,
		[Lexeme("[b][0-9]")]
		VariableBoolean,
		[Lexeme(@"\+")]
		Add,
		[Lexeme("-")]
		Subtract,
		[Lexeme(@"\*")]
		Multiply,
		[Lexeme("/")]
		Divide,
		[Lexeme("true")]
		True,
		[Lexeme("false")]
		False,
		[Lexeme("=")]
		Assignment,
		[Lexeme(">=")]
		GreaterThanOrEqual,
		[Lexeme("<=")]
		LessThanOrEqual,
		[Lexeme(">")]
		GreaterThan,
		[Lexeme("<")]
		LessThan,
		[Lexeme(",")]
		Comma,
		[Lexeme(@"[A-Za-z0-9]+(?=\()")]
		Identifier,
		[Lexeme(@"\(")]
		LeftParenthesis,
		[Lexeme(@"\)")]
		RightParenthesis,
		[Lexeme(@"[A-Za-z0-9]+")]
		PlainString,
		[Lexeme(@"[A-Za-z0-9\+]+(?=\s*[\),])|""([A-Za-z0-9\+]+)""|str:([A-Za-z0-9\+]+)")]
		String,
		[Lexeme(@"[ \t\r\n]", IsSkippable = true)]
		Whitespace,
	}
}
