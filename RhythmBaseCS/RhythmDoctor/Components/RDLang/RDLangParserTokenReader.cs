namespace RhythmBase.RhythmDoctor.Components.RDLang
{
	partial class RDLangParser
	{
		private static string _code = "";
		private static int _index;
		private static int _line;
		private static int _column;
		private static bool IsChar(char c, params char[] chars)
		{
			foreach (char ch in chars)
				if (c == ch)
					return true;
			return false;
		}
		private static void NewLine()
		{
			_line++;
			_column = 0;
		}
		private static Token NewToken(TokenType tokenType, object value)
		{
			return new Token
			{
				TokenID = tokenType,
				Value = value,
				Line = _line,
				Column = _column - tokenType switch
				{
					TokenType.VariableInteger or TokenType.VariableFloat or TokenType.VariableBoolean => 2,
					_ => value?.ToString()?.Length ?? 0,
				}
			};
		}
		private static char ReadChar()
		{
			if (_index >= _code.Length)
				return '\0';
			char c = _code[_index];
			_index++;
			_column++;
			return c;
		}
		private static bool TryRead(string str)
		{
			if (_index + str.Length > _code.Length)
				return false;
			for (int i = 0; i < str.Length; i++)
			{
				if (_code[_index + i] != str[i])
					return false;
			}
			_index += str.Length;
			_column += str.Length;
			return true;
		}
		private static char PeekChar
		{
			get
			{
				if (_index >= _code.Length)
					return '\0';
				return _code[_index];
			}
		}
		private static Token[] ReadAsToken(string code)
		{
			_code = code;
			_index = 0;
			_line = 1;
			_column = 0;
			List<Token> tokens = [];
			while (PeekChar != '\0')
			{
				char c = ReadChar();
				switch (c)
				{
					case ' ' or '\t' or '\r':
						break;
					case '\n':
						NewLine();
						break;
					case '+':
						if (PeekChar == '+')
							tokens.Add(NewToken(TokenType.OperatorIncreasement, "++"));
						else
							tokens.Add(NewToken(TokenType.OperatorAdd, "+"));
						break;
					case '-':
						if (PeekChar == '-')
							tokens.Add(NewToken(TokenType.OperatorDecreasement, "--"));
						else
							tokens.Add(NewToken(TokenType.OperatorSubtract, "-"));
						break;
					case '*':
						tokens.Add(NewToken(TokenType.OperatorMultipy, "*"));
						break;
					case '/':
						tokens.Add(NewToken(TokenType.OperatorDivide, "/"));
						break;
					case '=':
						tokens.Add(NewToken(TokenType.OperatorAssignment, "="));
						break;
					case '>':
						if (TryRead("="))
							tokens.Add(NewToken(TokenType.OperatorGreaterThanOrEqual, ">="));
						else
							tokens.Add(NewToken(TokenType.OperatorGreaterThan, ">"));
						break;
					case '<':
						if (TryRead("="))
							tokens.Add(NewToken(TokenType.OperatorLessThanOrEqual, "<="));
						else
							tokens.Add(NewToken(TokenType.OperatorLessThan, "<"));
						break;
					case '(':
						tokens.Add(NewToken(TokenType.LeftParenthesis, "("));
						break;
					case ')':
						tokens.Add(NewToken(TokenType.RightParenthesis, ")"));
						break;
					case '[':
						tokens.Add(NewToken(TokenType.LeftBracket, "["));
						break;
					case ']':
						tokens.Add(NewToken(TokenType.RightBracket, "]"));
						break;
					case ',':
						tokens.Add(NewToken(TokenType.Comma, ","));
						break;
					case '.':
						tokens.Add(NewToken(TokenType.Dot, "."));
						break;
					case >= '0' and <= '9':
						int value = c - '0';
						while (char.IsDigit(PeekChar))
						{
							value = value * 10 + (ReadChar() - '0');
						}
						if (PeekChar == '.')
						{
							ReadChar();
							float floatValue = value;
							while (char.IsDigit(PeekChar))
								floatValue = floatValue * 10 + (ReadChar() - '0');
							tokens.Add(NewToken(TokenType.Float, floatValue));
						}
						else
							tokens.Add(NewToken(TokenType.Integer, value));
						break;
					case '!':
						tokens.Add(NewToken(TokenType.OperatorNot, "!"));
						break;
					case 'b':
						if (char.IsDigit(PeekChar))
							tokens.Add(NewToken(TokenType.VariableBoolean, ReadChar() - '0'));
						else
							goto strl;
						break;
					case 'f':
						if (char.IsDigit(PeekChar))
							tokens.Add(NewToken(TokenType.VariableFloat, ReadChar() - '0'));
						else
							goto strl;
						break;
					case 'i':
						if (char.IsDigit(PeekChar))
							tokens.Add(NewToken(TokenType.VariableInteger, ReadChar() - '0'));
						else
							goto strl;
						break;
					// "[string]"
					case '"':
						string str2 = "";
						while (PeekChar != '"')
						{
							if (IsChar(PeekChar, '\0', '\n', '\r'))
								throw new Exception($"Unexpected end of string at {_line}:{_column}");
							str2 += ReadChar();
						}
						ReadChar(); // consume the closing "
						tokens.Add(NewToken(TokenType.String, str2));
						break;
					// identifier or [string]
					default:
					strl:
						bool isIdentifier = true;
						string str3 = c.ToString();
						if (!char.IsLetter(c) && c != '_')
							isIdentifier = false;
						while (!IsChar(PeekChar, '\0', '\n', '\r', ',', '(', ')'))
						{
							if (!char.IsLetterOrDigit(PeekChar) && PeekChar != '_')
								isIdentifier = false;
							str3 += ReadChar();
						}
						if (isIdentifier)
						{
							switch (str3)
							{
								case "And":
									tokens.Add(NewToken(TokenType.OperatorAnd, str3));
									break;
								case "Or":
									tokens.Add(NewToken(TokenType.OperatorOr, str3));
									break;
								case "true":
									tokens.Add(NewToken(TokenType.True, true));
									break;
								case "false":
									tokens.Add(NewToken(TokenType.False, false));
									break;
								default:
									tokens.Add(NewToken(TokenType.StringOrIdentifier, str3));
									break;
							}
						}
						else
						{
							switch (str3)
							{
								case ['s', 't', 'r', ':']:
									tokens.Add(NewToken(TokenType.String, str3[4..]));
									break;
								default:
									tokens.Add(NewToken(TokenType.String, str3));
									break;
							}
						}
						break;
				}
			}
			return [.. tokens];
		}
	}
}
