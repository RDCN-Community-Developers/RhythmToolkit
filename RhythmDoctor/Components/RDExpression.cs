using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace RhythmBase.RhythmDoctor.Components
{
	/// <summary>
	/// An Expression
	/// </summary>
	public struct RDExpression
#if NET7_0_OR_GREATER
		: INumber<RDExpression>
#endif
	{
		/// <summary>
		/// Gets the numeric value of the expression.
		/// </summary>
		public float NumericValue { get; }
		/// <summary>
		/// Gets the expression value as a string.
		/// </summary>
		public readonly string ExpressionValue
		{
			get
			{
				bool isNumeric = IsNumeric;
				string ExpressionValue = isNumeric ? NumericValue.ToString() : _exp;
				return ExpressionValue;
			}
		}
		/// <summary>
		/// Gets the evaluated value of the expression.
		/// </summary>
		public readonly float Value => IsNumeric ? NumericValue : Calculate(ExpressionValue);
		private static float Calculate(string exp)
		{
			if (string.IsNullOrWhiteSpace(exp))
				return 0;
			if (float.TryParse(exp, out float result))
				return result;
			return 0;
		}
#if NET7_0_OR_GREATER
		static RDExpression INumberBase<RDExpression>.One => 1;
		static int INumberBase<RDExpression>.Radix => 10;
#endif
		/// <summary>
		/// Gets the additive identity for the <see cref="RDExpression"/> type.
		/// </summary>
		public static RDExpression Zero => 0;
#if NET7_0_OR_GREATER
		static RDExpression IAdditiveIdentity<RDExpression, RDExpression>.AdditiveIdentity => 0;
		static RDExpression IMultiplicativeIdentity<RDExpression, RDExpression>.MultiplicativeIdentity => 1;
#endif
		/// <summary>
		/// Initializes a new instance of the <see cref="RDExpression"/> struct with a numeric value.
		/// </summary>
		/// <param name="value">The numeric value of the expression.</param>
		public RDExpression(float value)
		{
			this = default;
			IsNumeric = true;
			NumericValue = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="RDExpression"/> struct with a string value.
		/// </summary>
		/// <param name="value">The string value of the expression.</param>
#if NETSTANDARD
		public RDExpression(string value)
#else
		public RDExpression([AllowNull] string value)
#endif
		{
			IsNumeric = float.TryParse(value, out float numeric);
			if (IsNumeric)
				NumericValue = numeric;
			else
				_exp = value ?? "";
		}
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly bool Equals(object? obj) => obj is RDExpression e && Equals(e);
#else
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDExpression e && Equals(e);
#endif
		/// <inheritdoc/>
		public readonly bool Equals(RDExpression other) => IsNumeric == other.IsNumeric && NumericValue == other.NumericValue || _exp == other._exp;
		/// <inheritdoc/>
#if NETCOREAPP2_1_OR_GREATER
		public override readonly int GetHashCode()
		{
			HashCode hash = default;
			hash.Add(ExpressionValue);
			return hash.ToHashCode();
		}
#else
		public override readonly int GetHashCode()
		{
			int hash = 17;
			hash = hash * 31 + ExpressionValue.GetHashCode();
			return hash;
		}
#endif
		/// <inheritdoc/>
		public override readonly string ToString() => ExpressionValue;
		/// <summary>
		/// Converts a string to a nullable RDExpression.
		/// </summary>
		/// <param name="s">The string to convert.</param>
		/// <returns>A nullable RDExpression if the string is not null or empty; otherwise, null.</returns>
		public static RDExpression? Nullable(string s) => s != null && s.Length != 0 ? new RDExpression?(new RDExpression(s)) : null;

#if NET7_0_OR_GREATER
		readonly int IComparable.CompareTo(object? obj)
		{
			if (obj is RDExpression other)
			{
				return CompareTo(other);
			}
			throw new ArgumentException("Object is not a RDExpression");
		}
#endif
		/// <inheritdoc/>
		public readonly int CompareTo(RDExpression other)
		{
			if (IsNumeric && other.IsNumeric)
			{
				return NumericValue.CompareTo(other.NumericValue);
			}
			return string.Compare(ExpressionValue, other.ExpressionValue, StringComparison.Ordinal);
		}
#if NET7_0_OR_GREATER
		static RDExpression INumberBase<RDExpression>.Abs(RDExpression value)
		{
			return value.IsNumeric ? new RDExpression(Math.Abs(value.NumericValue)) : value;
		}
		static bool INumberBase<RDExpression>.IsCanonical(RDExpression value)
		{
			return true;
		}
		static bool INumberBase<RDExpression>.IsComplexNumber(RDExpression value)
		{
			return false;
		}
		static bool INumberBase<RDExpression>.IsEvenInteger(RDExpression value)
		{
			return value.IsNumeric && value.NumericValue % 2 == 0;
		}
		static bool INumberBase<RDExpression>.IsFinite(RDExpression value)
		{
			return value.IsNumeric && !float.IsInfinity(value.NumericValue);
		}
		static bool INumberBase<RDExpression>.IsImaginaryNumber(RDExpression value)
		{
			return false;
		}
		static bool INumberBase<RDExpression>.IsInfinity(RDExpression value)
		{
			return value.IsNumeric && float.IsInfinity(value.NumericValue);
		}
		static bool INumberBase<RDExpression>.IsInteger(RDExpression value)
		{
			return value.IsNumeric && value.NumericValue == Math.Floor(value.NumericValue);
		}
		static bool INumberBase<RDExpression>.IsNaN(RDExpression value)
		{
			return value.IsNumeric && float.IsNaN(value.NumericValue);
		}
		static bool INumberBase<RDExpression>.IsNegative(RDExpression value)
		{
			return value.IsNumeric ? value.NumericValue < 0 : Calculate(value.ExpressionValue) < 0;
		}
		static bool INumberBase<RDExpression>.IsNegativeInfinity(RDExpression value)
		{
			return value.IsNumeric && float.IsNegativeInfinity(value.NumericValue);
		}
		static bool INumberBase<RDExpression>.IsNormal(RDExpression value)
		{
			return value.IsNumeric && !float.IsSubnormal(value.NumericValue);
		}
		static bool INumberBase<RDExpression>.IsOddInteger(RDExpression value)
		{
			return value.IsNumeric && value.NumericValue % 2 != 0;
		}
		static bool INumberBase<RDExpression>.IsPositive(RDExpression value)
		{
			return value.IsNumeric ? value.NumericValue > 0 : Calculate(value.ExpressionValue) > 0;
		}
		static bool INumberBase<RDExpression>.IsPositiveInfinity(RDExpression value)
		{
			return value.IsNumeric && float.IsPositiveInfinity(value.NumericValue);
		}
		static bool INumberBase<RDExpression>.IsRealNumber(RDExpression value)
		{
			return value.IsNumeric;
		}
		static bool INumberBase<RDExpression>.IsSubnormal(RDExpression value)
		{
			return value.IsNumeric && float.IsSubnormal(value.NumericValue);
		}
		static bool INumberBase<RDExpression>.IsZero(RDExpression value)
		{
			return value.IsNumeric ? value.NumericValue == 0 : Calculate(value.ExpressionValue) == 0;
		}
		static RDExpression INumberBase<RDExpression>.MaxMagnitude(RDExpression x, RDExpression y)
		{
			return x.IsNumeric && y.IsNumeric ? Math.Abs(x.NumericValue) > Math.Abs(y.NumericValue) ? x : y : throw new NotImplementedException();
		}
		static RDExpression INumberBase<RDExpression>.MaxMagnitudeNumber(RDExpression x, RDExpression y)
		{
			return x.IsNumeric && y.IsNumeric ? Math.Abs(x.NumericValue) > Math.Abs(y.NumericValue) ? x : y : throw new NotImplementedException();
		}
		static RDExpression INumberBase<RDExpression>.MinMagnitude(RDExpression x, RDExpression y)
		{
			return x.IsNumeric && y.IsNumeric ? Math.Abs(x.NumericValue) < Math.Abs(y.NumericValue) ? x : y : throw new NotImplementedException();
		}
		static RDExpression INumberBase<RDExpression>.MinMagnitudeNumber(RDExpression x, RDExpression y)
		{
			return x.IsNumeric && y.IsNumeric ? Math.Abs(x.NumericValue) < Math.Abs(y.NumericValue) ? x : y : throw new NotImplementedException();
		}
		static RDExpression INumberBase<RDExpression>.Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
		{
			if (float.TryParse(s, style, provider, out float result))
			{
				return new RDExpression(result);
			}
			throw new FormatException("Input string was not in a correct format.");
		}
		static RDExpression INumberBase<RDExpression>.Parse(string s, NumberStyles style, IFormatProvider? provider)
		{
			if (float.TryParse(s, style, provider, out float result))
			{
				return new RDExpression(result);
			}
			throw new FormatException("Input string was not in a correct format.");
		}
		static bool INumberBase<RDExpression>.TryConvertFromChecked<TOther>(TOther value, out RDExpression result)
		{
			result = new RDExpression();
			return false;
		}
		static bool INumberBase<RDExpression>.TryConvertFromSaturating<TOther>(TOther value, out RDExpression result)
		{
			result = new RDExpression();
			return false;
		}
		static bool INumberBase<RDExpression>.TryConvertFromTruncating<TOther>(TOther value, out RDExpression result)
		{
			result = new RDExpression();
			return false;
		}
		static bool INumberBase<RDExpression>.TryConvertToChecked<TOther>(RDExpression value, out TOther result)
		{
			result = default!;
			return false;
		}
		static bool INumberBase<RDExpression>.TryConvertToSaturating<TOther>(RDExpression value, out TOther result)
		{
			result = default!;
			return false;
		}
		static bool INumberBase<RDExpression>.TryConvertToTruncating<TOther>(RDExpression value, out TOther result)
		{
			result = default!;
			return false;
		}
		static bool INumberBase<RDExpression>.TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out RDExpression result)
		{
			result = new(s.ToString());
			return true;
		}
		static bool INumberBase<RDExpression>.TryParse(string? s, NumberStyles style, IFormatProvider? provider, out RDExpression result)
		{
			result = new(s ?? "0");
			return true;
		}
		readonly bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
		{
			return NumericValue.TryFormat(destination, out charsWritten, format, provider);
		}
		readonly string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
		{
			return NumericValue.ToString(format, formatProvider);
		}
		static RDExpression ISpanParsable<RDExpression>.Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
		{
			if (float.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, provider, out float result))
			{
				return new RDExpression(result);
			}
			throw new FormatException("Input string was not in a correct format.");
		}
		static bool ISpanParsable<RDExpression>.TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out RDExpression result)
		{
			if (float.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, provider, out float numericResult))
			{
				result = new RDExpression(numericResult);
				return true;
			}
			result = default;
			return false;
		}
		static RDExpression IParsable<RDExpression>.Parse(string s, IFormatProvider? provider)
		{
			if (float.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, provider, out float result))
			{
				return new RDExpression(result);
			}
			throw new FormatException("Input string was not in a correct format.");
		}
		static bool IParsable<RDExpression>.TryParse(string? s, IFormatProvider? provider, out RDExpression result)
		{
			if (float.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, provider, out float numericResult))
			{
				result = new RDExpression(numericResult);
				return true;
			}
			result = default;
			return false;
		}
#endif
		/// <inheritdoc/>
		public static RDExpression operator +(RDExpression left, float right) => left.IsNumeric
				? new RDExpression(left.NumericValue + right)
				: new RDExpression(string.Format("{0}+{1}", left.ExpressionValue, right));
		/// <inheritdoc/>
		public static RDExpression operator +(float left, RDExpression right) => right.IsNumeric
				? new RDExpression(left + right.NumericValue)
				: new RDExpression(string.Format("{0}+{1}", left, right.ExpressionValue));
		/// <inheritdoc/>
		public static RDExpression operator +(RDExpression left, RDExpression right) => left.IsNumeric && right.IsNumeric
				? new RDExpression(left.NumericValue + right.NumericValue)
				: new RDExpression(string.Format("{0}+{1}", left.ExpressionValue, right.ExpressionValue));
		/// <inheritdoc/>
		public static RDExpression operator -(RDExpression left, float right) => left.IsNumeric
				? new RDExpression(left.NumericValue - right)
				: new RDExpression(string.Format("{0}-{1}", left.ExpressionValue, right));
		/// <inheritdoc/>
		public static RDExpression operator -(float left, RDExpression right) => right.IsNumeric
				? new RDExpression(left - right.NumericValue)
				: new RDExpression(string.Format("{0}-{1}", left, right.ExpressionValue));
		/// <inheritdoc/>
		public static RDExpression operator -(RDExpression left, RDExpression right) => left.IsNumeric && right.IsNumeric
				? new RDExpression(left.NumericValue - right.NumericValue)
				: new RDExpression(string.Format("{0}-{1}", left.ExpressionValue, right.ExpressionValue));
		/// <inheritdoc/>
		public static RDExpression operator *(RDExpression left, float right) => left.IsNumeric
				? new RDExpression(left.NumericValue * right)
				: new RDExpression(string.Format("({0})*{1}", left.ExpressionValue, right));
		/// <inheritdoc/>
		public static RDExpression operator *(float left, RDExpression right) => right.IsNumeric
				? new RDExpression(left * right.NumericValue)
				: new RDExpression(string.Format("{0}*({1})", left, right.ExpressionValue));
		/// <inheritdoc/>
		public static RDExpression operator *(RDExpression left, RDExpression right) => left.IsNumeric && right.IsNumeric
				? new RDExpression(left.NumericValue * right.NumericValue)
				: new RDExpression(string.Format("({0})*({1})", left.ExpressionValue, right.ExpressionValue));
		/// <inheritdoc/>
		public static RDExpression operator /(RDExpression left, float right) => left.IsNumeric
				? new RDExpression(left.NumericValue / right)
				: new RDExpression(string.Format("({0})/{1}", left.ExpressionValue, right));
		/// <inheritdoc/>
		public static RDExpression operator /(float left, RDExpression right) => right.IsNumeric
				? new RDExpression(left / right.NumericValue)
				: new RDExpression(string.Format("{0}/({1})", left, right.ExpressionValue));
		/// <inheritdoc/>
		public static RDExpression operator /(RDExpression left, RDExpression right) => left.IsNumeric && right.IsNumeric
				? new RDExpression(left.NumericValue / right.NumericValue)
				: new RDExpression(string.Format("({0})/({1})", left.ExpressionValue, right.ExpressionValue));
		/// <inheritdoc/>
		public static bool operator ==(RDExpression left, RDExpression right) => left.Equals(right);
		/// <inheritdoc/>
		public static bool operator !=(RDExpression left, RDExpression right) => !(left == right);
		/// <inheritdoc/>
		public static implicit operator RDExpression(float v) => new(v);
		/// <inheritdoc/>
		public static implicit operator RDExpression(string v) => new(v);
#if NET7_0_OR_GREATER
		static bool IComparisonOperators<RDExpression, RDExpression, bool>.operator >(RDExpression left, RDExpression right)
		{
			return left.CompareTo(right) > 0;
		}
		static bool IComparisonOperators<RDExpression, RDExpression, bool>.operator >=(RDExpression left, RDExpression right)
		{
			return left.CompareTo(right) >= 0;
		}
		static bool IComparisonOperators<RDExpression, RDExpression, bool>.operator <(RDExpression left, RDExpression right)
		{
			return left.CompareTo(right) < 0;
		}
		static bool IComparisonOperators<RDExpression, RDExpression, bool>.operator <=(RDExpression left, RDExpression right)
		{
			return left.CompareTo(right) <= 0;
		}
		static RDExpression IModulusOperators<RDExpression, RDExpression, RDExpression>.operator %(RDExpression left, RDExpression right)
		{
			if (left.IsNumeric && right.IsNumeric)
			{
				return new RDExpression(left.NumericValue % right.NumericValue);
			}
			throw new NotImplementedException("Modulus operator is not implemented for non-numeric expressions.");
		}
		static RDExpression IDecrementOperators<RDExpression>.operator --(RDExpression value)
		{
			if (value.IsNumeric)
			{
				return new RDExpression(value.NumericValue - 1);
			}
			throw new NotImplementedException("Decrement operator is not implemented for non-numeric expressions.");
		}
		static RDExpression IIncrementOperators<RDExpression>.operator ++(RDExpression value)
		{
			if (value.IsNumeric)
			{
				return new RDExpression(value.NumericValue + 1);
			}
			throw new NotImplementedException("Increment operator is not implemented for non-numeric expressions.");
		}
		static RDExpression IUnaryNegationOperators<RDExpression, RDExpression>.operator -(RDExpression value)
		{
			if (value.IsNumeric)
			{
				return new RDExpression(-value.NumericValue);
			}
			throw new NotImplementedException("Unary negation operator is not implemented for non-numeric expressions.");
		}
		static RDExpression IUnaryPlusOperators<RDExpression, RDExpression>.operator +(RDExpression value)
		{
			return value;
		}
#endif
		private readonly string _exp = "";
		/// <summary>
		/// 
		/// </summary>
		public bool IsNumeric { get; private set; } = false;
	}
}
