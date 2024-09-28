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
	public struct RDExpression : IEquatable<RDExpression>
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
		public RDExpression(float value)
		{
			this = default;
			IsNumeric = true;
			NumericValue = value;
		}

		public RDExpression(string value)
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
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDExpression) && Equals((obj != null) ? ((RDExpression)obj) : default);
		public readonly bool Equals(RDExpression other) => (IsNumeric == other.IsNumeric && NumericValue == other.NumericValue) || _exp == other._exp;
		public override int GetHashCode()
		{
			HashCode hash = default;
			hash.Add(ExpressionValue);
			return hash.ToHashCode();
		}
		public override string ToString() => ExpressionValue;
		public static RDExpression? Nullable(string s) => s != null && s.Length != 0 ? new RDExpression?(new RDExpression(s)) : null;
		public static RDExpression operator +(RDExpression left, float right) => left.IsNumeric
				? new RDExpression(left.NumericValue + right)
				: new RDExpression(string.Format("{0}+{1}", left.ExpressionValue, right));
		public static RDExpression operator +(float left, RDExpression right) => right.IsNumeric
				? new RDExpression(left + right.NumericValue)
				: new RDExpression(string.Format("{0}+{1}", left, right.ExpressionValue));
		public static RDExpression operator +(RDExpression left, RDExpression right) => left.IsNumeric && right.IsNumeric
				? new RDExpression(left.NumericValue + right.NumericValue)
				: new RDExpression(string.Format("{0}+{1}", left.ExpressionValue, right.ExpressionValue));
		public static RDExpression operator -(RDExpression left, float right) => left.IsNumeric
				? new RDExpression(left.NumericValue - right)
				: new RDExpression(string.Format("{0}-{1}", left.ExpressionValue, right));
		public static RDExpression operator -(float left, RDExpression right) => right.IsNumeric
				? new RDExpression(left - right.NumericValue)
				: new RDExpression(string.Format("{0}-{1}", left, right.ExpressionValue));
		public static RDExpression operator -(RDExpression left, RDExpression right) => left.IsNumeric && right.IsNumeric
				? new RDExpression(left.NumericValue - right.NumericValue)
				: new RDExpression(string.Format("{0}-{1}", left.ExpressionValue, right.ExpressionValue));
		public static RDExpression operator *(RDExpression left, float right) => left.IsNumeric
				? new RDExpression(left.NumericValue * right)
				: new RDExpression(string.Format("({0})*{1}", left.ExpressionValue, right));
		public static RDExpression operator *(float left, RDExpression right) => right.IsNumeric
				? new RDExpression(left * right.NumericValue)
				: new RDExpression(string.Format("{0}*({1})", left, right.ExpressionValue));
		public static RDExpression operator *(RDExpression left, RDExpression right) => left.IsNumeric && right.IsNumeric
				? new RDExpression(left.NumericValue * right.NumericValue)
				: new RDExpression(string.Format("({0})*({1})", left.ExpressionValue, right.ExpressionValue));
		public static RDExpression operator /(RDExpression left, float right) => left.IsNumeric
				? new RDExpression(left.NumericValue / right)
				: new RDExpression(string.Format("({0})/{1}", left.ExpressionValue, right));
		public static RDExpression operator /(float left, RDExpression right) => right.IsNumeric
				? new RDExpression(left / right.NumericValue)
				: new RDExpression(string.Format("{0}/({1})", left, right.ExpressionValue));
		public static RDExpression operator /(RDExpression left, RDExpression right) => left.IsNumeric && right.IsNumeric
				? new RDExpression(left.NumericValue / right.NumericValue)
				: new RDExpression(string.Format("({0})/({1})", left.ExpressionValue, right.ExpressionValue));
		public static bool operator ==(RDExpression left, RDExpression right) => left.Equals(right);
		public static bool operator !=(RDExpression left, RDExpression right) => !(left == right);
		public static implicit operator RDExpression(float v) => new(v);
		public static implicit operator RDExpression(string v) => new(v);
		private readonly string _exp;
		public readonly bool IsNumeric;
	}
}
