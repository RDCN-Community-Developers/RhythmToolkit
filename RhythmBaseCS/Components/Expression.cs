using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// An Expression
	/// </summary>
	[JsonConverter(typeof(ExpressionConverter))]
	public struct Expression : IEquatable<Expression>
	{
		public float NumericValue { get; }
		public readonly string ExpressionValue
		{
			get
			{
				bool isNumeric = IsNumeric;
				string ExpressionValue = isNumeric ? NumericValue.ToString() : _exp;
				return ExpressionValue;
			}
		}
		public readonly Func<Variables, float> ExpressionFunction => throw new NotImplementedException();
		public Expression(float value)
		{
			this = default;
			IsNumeric = true;
			NumericValue = value;
		}

		public Expression(string value)
		{
			this = default;
			float numeric;
			if (float.TryParse(value, out numeric))
			{
				IsNumeric = true;
				NumericValue = numeric;
			}
			else
			{
				IsNumeric = false;
				_exp = value;
			}
		}
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(Expression) && Equals((obj != null) ? ((Expression)obj) : default);
		public readonly bool Equals(Expression other) => (IsNumeric == other.IsNumeric && NumericValue == other.NumericValue) || _exp == other._exp;
		public override int GetHashCode()
		{
			HashCode hash = default;
			hash.Add(ExpressionValue);
			return hash.ToHashCode();
		}
		public override string ToString() => ExpressionValue;
		public static Expression? Nullable(string s) => s != null && s.Length != 0 ? new Expression?(new Expression(s)) : null;
		public static Expression operator +(Expression left, float right) => left.IsNumeric
				? new Expression(left.NumericValue + right)
				: new Expression(string.Format("{0}+{1}", left.ExpressionValue, right));
		public static Expression operator +(float left, Expression right) => right.IsNumeric
				? new Expression(left + right.NumericValue)
				: new Expression(string.Format("{0}+{1}", left, right.ExpressionValue));
		public static Expression operator +(Expression left, Expression right) => left.IsNumeric && right.IsNumeric
				? new Expression(left.NumericValue + right.NumericValue)
				: new Expression(string.Format("{0}+{1}", left.ExpressionValue, right.ExpressionValue));
		public static Expression operator -(Expression left, float right) => left.IsNumeric
				? new Expression(left.NumericValue - right)
				: new Expression(string.Format("{0}-{1}", left.ExpressionValue, right));
		public static Expression operator -(float left, Expression right) => right.IsNumeric
				? new Expression(left - right.NumericValue)
				: new Expression(string.Format("{0}-{1}", left, right.ExpressionValue));
		public static Expression operator -(Expression left, Expression right) => left.IsNumeric && right.IsNumeric
				? new Expression(left.NumericValue - right.NumericValue)
				: new Expression(string.Format("{0}-{1}", left.ExpressionValue, right.ExpressionValue));
		public static Expression operator *(Expression left, float right) => left.IsNumeric
				? new Expression(left.NumericValue * right)
				: new Expression(string.Format("({0})*{1}", left.ExpressionValue, right));
		public static Expression operator *(float left, Expression right) => right.IsNumeric
				? new Expression(left * right.NumericValue)
				: new Expression(string.Format("{0}*({1})", left, right.ExpressionValue));
		public static Expression operator *(Expression left, Expression right) => left.IsNumeric && right.IsNumeric
				? new Expression(left.NumericValue * right.NumericValue)
				: new Expression(string.Format("({0})*({1})", left.ExpressionValue, right.ExpressionValue));
		public static Expression operator /(Expression left, float right) => left.IsNumeric
				? new Expression(left.NumericValue / right)
				: new Expression(string.Format("({0})/{1}", left.ExpressionValue, right));
		public static Expression operator /(float left, Expression right) => right.IsNumeric
				? new Expression(left / right.NumericValue)
				: new Expression(string.Format("{0}/({1})", left, right.ExpressionValue));
		public static Expression operator /(Expression left, Expression right) => left.IsNumeric && right.IsNumeric
				? new Expression(left.NumericValue / right.NumericValue)
				: new Expression(string.Format("({0})/({1})", left.ExpressionValue, right.ExpressionValue));
		public static bool operator ==(Expression left, Expression right) => left.Equals(right);
		public static bool operator !=(Expression left, Expression right) => !(left == right);
		public static implicit operator Expression(float v) => new(v);
		public static implicit operator Expression(string v) => new(v);
		private readonly string _exp;
		public readonly bool IsNumeric;
	}
}
