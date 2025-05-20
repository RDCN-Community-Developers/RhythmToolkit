using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				Column = _column
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
						tokens.Add(NewToken(TokenType.Add, "+"));
						break;
					case '-':
						tokens.Add(NewToken(TokenType.Subtract, "-"));
						break;
					case '*':
						tokens.Add(NewToken(TokenType.Multipy, "*"));
						break;
					case '/':
						tokens.Add(NewToken(TokenType.Divide, "/"));
						break;
					case '=':
						tokens.Add(NewToken(TokenType.Assignment, "="));
						break;
					case '>':
						if (TryRead("="))
							tokens.Add(NewToken(TokenType.GreaterThanOrEqual, ">="));
						else
							tokens.Add(NewToken(TokenType.GreaterThan, ">"));
						break;
					case '<':
						if (TryRead("="))
							tokens.Add(NewToken(TokenType.LessThanOrEqual, "<="));
						else
							tokens.Add(NewToken(TokenType.LessThan, "<"));
						break;
					case '(':
						tokens.Add(NewToken(TokenType.LeftParenthesis, "("));
						break;
					case ')':
						tokens.Add(NewToken(TokenType.RightParenthesis, ")"));
						break;
					case ',':
						tokens.Add(NewToken(TokenType.Comma, ","));
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
							{
								floatValue = floatValue * 10 + (ReadChar() - '0');
							}
							tokens.Add(NewToken(TokenType.Float, floatValue));
						}
						else
						{
							tokens.Add(NewToken(TokenType.Integer, value));
						}
						break;
					case 'b':
						if (char.IsDigit(PeekChar))
							tokens.Add(NewToken(TokenType.VariableBoolean, ReadChar() - '0'));
						else
							goto strl;
						break;
					case 'f':
						if (TryRead("alse"))
							tokens.Add(NewToken(TokenType.False, false));
						else if (char.IsDigit(PeekChar))
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
					case 't':
						if (TryRead("rue"))
							tokens.Add(NewToken(TokenType.True, true));
						else
							goto strl;
						break;
					// str:[string]
					case 's':
						if (TryRead("tr:"))
						{
							string str1 = "";
							while (PeekChar != ',' && PeekChar != ')')
							{
								str1 += ReadChar();
							}
							tokens.Add(NewToken(TokenType.String, str1));
						}
						else
						{
							goto strl;
						}
						break;
					// "[string]"
					case '"':
						string str2 = "";
						while (PeekChar != '"')
						{
							if (IsChar(PeekChar, '\0', '\n', '\r'))
							{
								throw new Exception($"Unexpected end of string at {_line}:{_column}");
							}
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
						{
							isIdentifier = false;
						}
						while (!IsChar(PeekChar, '\0', '\n', '\r', ',', '(', ')'))
						{
							if (!char.IsLetterOrDigit(PeekChar) && PeekChar != '_')
							{
								isIdentifier = false;
							}
							str3 += ReadChar();
						}
						if (isIdentifier)
						{
							tokens.Add(NewToken(TokenType.StringOrIdentifier, str3));
						}
						else
						{
							tokens.Add(NewToken(TokenType.String, str3));
						}
						break;
				}
			}
			return [.. tokens];
		}
	}
}
