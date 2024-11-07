namespace RhythmBase.Utils
{
	/// <summary>
	/// Provides methods to encode and decode RDCode.
	/// </summary>
	public static class RDCodeUtils
	{
#pragma warning disable
		public enum TokenType
		{
			End,

			Symbol,
			VariableInt,
			VariableFloat,
			VariableBoolean,

			ConstInt,
			ConstFloat,
			ConstString,
			ConstTrue,
			ConstFalse,

			OperatorAdd,
			OperatorSubtract,
			OperatorMultiply,
			OperatorDivide,
			OperatorEquals,
			OperatorNotEquals,
			OperatorGreaterThan,
			OperatorLessThan,
			OperatorAssignment,

			ParenthesisOpen,
			ParenthesisClose,
			BracketOpen,
			BracketClose,
			BraceOpen,
			BraceClose,

			Dot,
			Comma,
#pragma warning enable
		}
		public struct Token()
		{
			public TokenType Type;
			public string Name;
			public object Value;
			public override readonly string ToString() => $"{Type}\t{Name}\t{Value}";
		}
		/// <summary>
		/// Reader class to read and tokenize RDCode.
		/// </summary>
		public class Reader()
		{
			private string code;
			private int position;

			/// <summary>
			/// Gets or sets the RDCode to be read.
			/// </summary>
			public required string Code
			{
				get => code;
				set
				{
					code = value;
					position = 0;
				}
			}

			/// <summary>
			/// Reads tokens from the RDCode until the end is reached.
			/// </summary>
			/// <returns>An enumerable collection of tokens.</returns>
			public IEnumerable<Token> ReadToEnd()
			{
				Token current;
				while ((current = Read()).Type != TokenType.End)
					yield return current;
			}
			private Token Read()
			{
				ReadSpace();
				if (!TryPeek(out char c))
					return new() { Type = TokenType.End };
				if (char.IsLetter(c))
					if (ReadMatch("str:"))
					{
						string str = "";
						while (TryPeek(out char d) && (char.IsLetter(d) || char.IsDigit(d) || d == '_'))
						{
							str += d;
							position++;
						}
						return new() { Type = TokenType.ConstString, Name = str, Value = str };
					}
					else if (ReadMatch("true"))
					{
						return new() { Type = TokenType.ConstTrue, Name = "true", Value = true };
					}
					else if (ReadMatch("false"))
					{
						return new() { Type = TokenType.ConstFalse, Name = "false", Value = false };
					}
					else if (ReadMatch('i') && TryPeek(out char i) && char.IsDigit(i))
					{
						position++;
						return new() { Type = TokenType.VariableInt, Value = i - '0' };
					}
					else if (ReadMatch('f') && TryPeek(out char f) && char.IsDigit(f))
					{
						position++;
						return new() { Type = TokenType.VariableInt, Value = f - '0' };
					}
					else if (ReadMatch('b') && TryPeek(out char b) && char.IsDigit(b))
					{
						position++;
						return new() { Type = TokenType.VariableInt, Value = b - '0' };
					}
					else
					{
						string str = "";
						while (TryPeek(out char d) && (char.IsLetter(d) || char.IsDigit(d) || d == '_'))
						{
							str += d;
							position++;
						}
						return new() { Type = TokenType.Symbol, Name = str, Value = str };
					}
				else if (char.IsDigit(c))
				{
					int intdata = ReadInt();
					if (TryPeek(out char dot) && dot == '.')
					{
						position++;
						float floatdata = intdata;
						int digit = position;
						float numdata = ReadInt();
						for (int i = 0; i < position - digit; i++)
							numdata /= 10;
						floatdata += numdata;
						return new() { Type = TokenType.ConstFloat, Value = floatdata };
					}
					return new() { Type = TokenType.ConstInt, Value = intdata };
				}
				else if ("+-*/><=!".Contains(c))
				{
					position++;
					return c switch
					{
						'+' => new() { Type = TokenType.OperatorAdd },
						'-' => new() { Type = TokenType.OperatorSubtract },
						'*' => new() { Type = TokenType.OperatorMultiply },
						'/' => new() { Type = TokenType.OperatorDivide },
						'>' => new() { Type = TokenType.OperatorGreaterThan },
						'<' => new() { Type = TokenType.OperatorLessThan },
						'=' => ReadMatch("=")
							? new() { Type = TokenType.OperatorEquals }
							: new() { Type = TokenType.OperatorAssignment },
						'!' => ReadMatch("=")
							? new() { Type = TokenType.OperatorNotEquals }
							: throw new NotImplementedException(),
						_ => throw new NotImplementedException(),
					};
				}
				else
				{
					position++;
					return c switch
					{
						'+' => new() { Type = TokenType.OperatorAdd },
						'-' => new() { Type = TokenType.OperatorAdd },
						'*' => new() { Type = TokenType.OperatorAdd },
						'/' => new() { Type = TokenType.OperatorAdd },
						'(' => new() { Type = TokenType.ParenthesisOpen },
						')' => new() { Type = TokenType.ParenthesisClose },
						'[' => new() { Type = TokenType.BracketOpen },
						']' => new() { Type = TokenType.BracketClose },
						'{' => new() { Type = TokenType.BraceOpen },
						'}' => new() { Type = TokenType.BraceClose },
						'.' => new() { Type = TokenType.Dot },
						',' => new() { Type = TokenType.Comma },
						_ => throw new NotImplementedException(),
					};
				}
			}
			private void ReadSpace()
			{
				bool flag = true;
				while (flag && position < code.Length)
				{
					char c = code[position];
					if (char.IsWhiteSpace(c))
						position++;
					else
						flag = false;
				}
			}
			private int ReadInt()
			{
				TryPeek(out char c);
				int result = 0;
				while (char.IsDigit(c))
				{
					TryRead(out c);
					result *= 10;
					result += c - '0';
					TryPeek(out c);
				}
				return result;
			}
			private bool TryPeek(out char c)
			{
				bool flag = position < code.Length;
				c = flag ? code[position] : '\0';
				return flag;
			}
			private bool TryPeek(out string s, int length)
			{
				bool flag = position + length - 1 < code.Length;
				s = flag ? code[(position)..(position + length)] : "";
				return flag;
			}
			private bool PeekMatch(string s) => TryPeek(out string so, s.Length) && s == so;
			private bool TryRead(out char c)
			{
				bool flag = position < code.Length;
				c = flag ? code[position] : '\0';
				if (flag) position++;
				return flag;
			}
			private bool TryRead(out string s, int length)
			{
				bool flag = position < code.Length;
				s = flag ? code[position..(position + length)] : "";
				if (flag) position += length;
				return flag;
			}
			private bool ReadMatch(char c)
			{
				if (position + 1 > code.Length) return false;
				char get = code[position];
				if (get != c) return false;
				position += 1;
				return true;
			}
			private bool ReadMatch(string s)
			{
				if (position + s.Length > code.Length) return false;
				string get = code[(position)..(position + s.Length)];
				if (get != s) return false;
				position += s.Length;
				return true;
			}
		}
	}
}
