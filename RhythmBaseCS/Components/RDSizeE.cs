using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>nullable</strong> <seealso cref="T:RhythmBase.Components.Expression" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDSizeE : IEquatable<RDSizeE>
	{
		public RDSizeE(float width, float height)
		{
			this = default;
			this.Width = new Expression?(width);
			this.Height = new Expression?(height);
		}

		public RDSizeE(Expression? width, float height)
		{
			this = default;
			this.Width = width;
			this.Height = new Expression?(height);
		}

		public RDSizeE(float width, Expression? height)
		{
			this = default;
			this.Width = new Expression?(width);
			this.Height = height;
		}

		public RDSizeE(Expression? width, Expression? height)
		{
			this = default;
			this.Width = width;
			this.Height = height;
		}

		public RDSizeE(string width, float height)
		{
			this = default;
			this.Width = Expression.Nullable(width);
			this.Height = new Expression?(height);
		}

		public RDSizeE(float width, string height)
		{
			this = default;
			this.Width = new Expression?(width);
			this.Height = Expression.Nullable(height);
		}

		public RDSizeE(string width, string height)
		{
			this = default;
			this.Width = Expression.Nullable(width);
			this.Height = Expression.Nullable(height);
		}

		public RDSizeE(string width, Expression? height)
		{
			this = default;
			this.Width = Expression.Nullable(width);
			this.Height = height;
		}

		public RDSizeE(Expression? width, string height)
		{
			this = default;
			this.Width = width;
			this.Height = Expression.Nullable(height);
		}

		public RDSizeE(RDSizeI p)
		{
			this = default;
			int? num2;
			int? num = num2 = p.Width;
			this.Width = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
			num = num2 = p.Height;
			this.Height = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
		}

		public RDSizeE(RDSize p)
		{
			this = default;
			float? num2;
			float? num = num2 = p.Width;
			this.Width = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
			num = num2 = p.Height;
			this.Height = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
		}

		public RDSizeE(RDPointI p)
		{
			this = default;
			int? num2;
			int? num = num2 = p.X;
			this.Width = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
			num = num2 = p.Y;
			this.Height = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
		}

		public RDSizeE(RDPoint p)
		{
			this = default;
			float? num2;
			float? num = num2 = p.X;
			this.Width = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
			num = num2 = p.Y;
			this.Height = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
		}

		public RDSizeE(RDPointE p)
		{
			this = default;
			this.Width = p.X;
			this.Height = p.Y;
		}

		public bool IsEmpty
		{
			get
			{
				return Width == null && Height == null;
			}
		}

		public Expression? Width { get; set; }

		public Expression? Height { get; set; }

		public Expression? Area
		{
			get
			{
				return Width * Height;
			}
		}

		public static RDSizeE Add(RDSizeE sz1, RDSize sz2)
		{
			RDSizeE Add = new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
			return Add;
		}

		public static RDSizeE Add(RDSizeE sz1, RDSizeE sz2)
		{
			RDSizeE Add = new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
			return Add;
		}

		public static RDSizeE Subtract(RDSizeE sz1, RDSize sz2)
		{
			RDSizeE Subtract = new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
			return Subtract;
		}

		public static RDSizeE Subtract(RDSizeE sz1, RDSizeE sz2)
		{
			RDSizeE Subtract = new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
			return Subtract;
		}

		public override int GetHashCode()
		{
			HashCode h = default;
			h.Add(Width);
			h.Add(Height);
			return h.ToHashCode();
		}

		public override string ToString()
		{
			string format = "[{0}, {1}]";
			Expression? expression = Width;
			object arg = ((expression != null) ? expression.GetValueOrDefault().ExpressionValue : null) ?? "null";
			expression = Height;
			return string.Format(format, arg, ((expression != null) ? expression.GetValueOrDefault().ExpressionValue : null) ?? "null");
		}

		public bool Equals(RDSizeE other)
		{
			Expression? expression = Width;
			Expression? expression2 = other.Width;
			bool? flag2;
			bool? flag = flag2 = (expression != null & expression2 != null) ? new bool?(expression.GetValueOrDefault() == expression2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				expression2 = Height;
				expression = other.Height;
				flag3 = flag2 = (expression2 != null & expression != null) ? new bool?(expression2.GetValueOrDefault() == expression.GetValueOrDefault()) : null;
				flag4 = (flag2 != null) ? (flag & flag3.GetValueOrDefault()) : null;
			}
			else
			{
				flag4 = new bool?(false);
			}
			flag3 = flag4;
			return flag3.Value;
		}

		public RDPointE ToRDPointE()
		{
			RDPointE ToRDPointE = new(Width, Height);
			return ToRDPointE;
		}

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSize) && Equals((obj != null) ? ((RDSize)obj) : default);

		public static RDSizeE operator +(RDSizeE sz1, RDSizeI sz2) => Add(sz1, sz2);

		public static RDSizeE operator +(RDSizeE sz1, RDSize sz2) => Add(sz1, sz2);

		public static RDSizeE operator +(RDSizeE sz1, RDSizeE sz2) => Add(sz1, sz2);

		public static RDSizeE operator -(RDSizeE sz1, RDSizeI sz2) => Subtract(sz1, sz2);

		public static RDSizeE operator -(RDSizeE sz1, RDSize sz2) => Subtract(sz1, sz2);

		public static RDSizeE operator -(RDSizeE sz1, RDSizeE sz2) => Subtract(sz1, sz2);

		public static RDSizeE operator *(int left, RDSizeE right)
		{
			RDSizeE result = new((float)left * right.Width, (float)left * right.Height);
			return result;
		}

		public static RDSizeE operator *(RDSizeE left, int right)
		{
			RDSizeE result = new(left.Width * (float)right, left.Height * (float)right);
			return result;
		}

		public static RDSizeE operator *(float left, RDSizeE right)
		{
			RDSizeE result = new(left * right.Width, left * right.Height);
			return result;
		}

		public static RDSizeE operator *(RDSizeE left, float right)
		{
			RDSizeE result = new(left.Width * right, left.Height * right);
			return result;
		}

		public static RDSizeE operator *(Expression left, RDSizeE right)
		{
			RDSizeE result = new(left * right.Width, left * right.Height);
			return result;
		}

		public static RDSizeE operator *(RDSizeE left, Expression right)
		{
			RDSizeE result = new(left.Width * right, left.Height * right);
			return result;
		}

		public static RDSizeE operator /(RDSizeE left, float right)
		{
			RDSizeE result = new(left.Width / right, left.Height / right);
			return result;
		}

		public static RDSizeE operator /(RDSizeE left, Expression right)
		{
			RDSizeE result = new(left.Width / right, left.Height / right);
			return result;
		}

		public static bool operator ==(RDSizeE sz1, RDSizeE sz2) => sz1.Equals(sz2);

		public static bool operator !=(RDSizeE sz1, RDSizeE sz2) => !sz1.Equals(sz2);

		public static explicit operator RDPointE(RDSizeE size)
		{
			RDPointE result = new(size.Width, size.Height);
			return result;
		}
	}
}
