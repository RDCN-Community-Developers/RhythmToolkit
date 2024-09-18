using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>nullable</strong> <seealso cref="T:RhythmBase.Components.Expression" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDPointE : IEquatable<RDPointE>
	{
		public RDPointE(RDSize sz)
		{
			this = default;
			float? num2;
			float? num = num2 = sz.Width;
			X = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
			num = num2 = sz.Height;
			Y = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
		}

		public RDPointE(float x, float y)
		{
			this = default;
			this.X = new Expression?(x);
			this.Y = new Expression?(y);
		}

		public RDPointE(Expression? x, float y)
		{
			this = default;
			this.X = x;
			this.Y = new Expression?(y);
		}

		public RDPointE(float x, Expression? y)
		{
			this = default;
			this.X = new Expression?(x);
			this.Y = y;
		}

		public RDPointE(Expression? x, Expression? y)
		{
			this = default;
			this.X = x;
			this.Y = y;
		}

		public RDPointE(string x, float y)
		{
			this = default;
			this.X = Expression.Nullable(x);
			this.Y = new Expression?(y);
		}

		public RDPointE(float x, string y)
		{
			this = default;
			this.X = new Expression?(x);
			this.Y = Expression.Nullable(y);
		}

		public RDPointE(string x, Expression? y)
		{
			this = default;
			this.X = Expression.Nullable(x);
			this.Y = y;
		}

		public RDPointE(Expression? x, string y)
		{
			this = default;
			this.X = x;
			if (y != null && y.Length != 0)
			{
				this.Y = new Expression?(y);
			}
		}

		public RDPointE(string x, string y)
		{
			this = default;
			this.X = Expression.Nullable(x);
			this.Y = Expression.Nullable(y);
		}

		public RDPointE(RDPointI p)
		{
			this = default;
			int? num2;
			int? num = num2 = p.X;
			this.X = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
			num = num2 = p.Y;
			this.Y = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
		}

		public RDPointE(RDPoint p)
		{
			this = default;
			float? num2;
			float? num = num2 = p.X;
			this.X = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
			num = num2 = p.Y;
			this.Y = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
		}

		public bool IsEmpty
		{
			get
			{
				return X == null && Y == null;
			}
		}

		public Expression? X { get; set; }

		public Expression? Y { get; set; }

		public void Offset(RDPoint p)
		{
			X += p.X;
			Y += p.Y;
		}

		public void Offset(float? dx, float? dy)
		{
			X += dx;
			Y += dy;
		}

		public static RDPointE Add(RDPointE pt, RDSizeI sz)
		{
			Expression? expression = pt.X;
			int? num = sz.Width;
			float? num2 = (num != null) ? new float?((float)num.GetValueOrDefault()) : null;
			if (!(expression != null & num2 != null))
			{
				Expression? expression2 = null;
			}
			else
			{
				new Expression?(expression.GetValueOrDefault() + num2.GetValueOrDefault());
			}
			expression = pt.X;
			num = sz.Width;
			Expression? x = expression + ((num != null) ? new float?((float)num.GetValueOrDefault()) : null);
			expression = pt.Y;
			num = sz.Height;
			RDPointE Add = new(x, expression + ((num != null) ? new float?((float)num.GetValueOrDefault()) : null));
			return Add;
		}

		public static RDPointE Add(RDPointE pt, RDSize sz)
		{
			RDPointE Add = new(pt.X + sz.Width, pt.Y + sz.Height);
			return Add;
		}

		public static RDPointE Add(RDPointE pt, RDSizeE sz)
		{
			RDPointE Add = new(pt.X + sz.Width, pt.Y + sz.Height);
			return Add;
		}

		public static RDPointE Subtract(RDPointE pt, RDSizeI sz)
		{
			Expression? expression = pt.X;
			int? num = sz.Width;
			Expression? x = expression - ((num != null) ? new float?((float)num.GetValueOrDefault()) : null);
			expression = pt.Y;
			num = sz.Height;
			RDPointE Subtract = new(x, expression - ((num != null) ? new float?((float)num.GetValueOrDefault()) : null));
			return Subtract;
		}

		public static RDPointE Subtract(RDPointE pt, RDSize sz)
		{
			RDPointE Subtract = new(pt.X - sz.Width, pt.Y - sz.Height);
			return Subtract;
		}

		public static RDPointE Subtract(RDPointE pt, RDSizeE sz)
		{
			RDPointE Subtract = new(pt.X - sz.Width, pt.Y - sz.Height);
			return Subtract;
		}

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDPoint) && Equals((obj != null) ? ((RDPoint)obj) : default);

		public override int GetHashCode()
		{
			HashCode h = default;
			h.Add(X);
			h.Add(Y);
			return h.ToHashCode();
		}

		public override string ToString()
		{
			string format = "[{0}, {1}]";
			Expression? expression = X;
			object arg = ((expression != null) ? expression.GetValueOrDefault().ExpressionValue : null) ?? "null";
			expression = Y;
			return string.Format(format, arg, ((expression != null) ? expression.GetValueOrDefault().ExpressionValue : null) ?? "null");
		}

		bool IEquatable<RDPointE>.Equals(RDPointE other)
		{
			Expression? expression = other.X;
			Expression? expression2 = X;
			bool? flag2;
			bool? flag = flag2 = (expression != null & expression2 != null) ? new bool?(expression.GetValueOrDefault() == expression2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				expression2 = other.Y;
				expression = Y;
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

		public RDPointE MultipyByMatrix(Expression[,] matrix)
		{
			if (matrix.Rank == 2 && matrix.Length == 4)
			{
				RDPointE MultipyByMatrix = new(X * matrix[0, 0] + Y * matrix[1, 0], X * matrix[0, 1] + Y * matrix[1, 1]);
				return MultipyByMatrix;
			}
			throw new Exception("Matrix not match, 2*2 matrix expected.");
		}
		/// <summary>
		/// Rotate.
		/// </summary>
		public RDPointE Rotate(float angle)
		{
			Expression[,] array = new Expression[2, 2];
			array[0, 0] = (float)Math.Cos((double)angle);
			array[0, 1] = (float)Math.Sin((double)angle);
			array[1, 0] = (float)-(float)Math.Sin((double)angle);
			array[1, 1] = (float)Math.Cos((double)angle);
			return MultipyByMatrix(array);
		}
		/// <summary>
		/// Rotate at a given pivot.
		/// </summary>
		/// <param name="pivot">Giver pivot.</param>
		/// <returns></returns>
		public RDPointE Rotate(RDPointE pivot, float angle) => (this - new RDSizeE(pivot)).Rotate(angle) + new RDSizeE(pivot);

		public static RDPointE operator +(RDPointE pt, RDSizeI sz) => Add(pt, sz);

		public static RDPointE operator +(RDPointE pt, RDSize sz) => Add(pt, sz);

		public static RDPointE operator +(RDPointE pt, RDSizeE sz) => Add(pt, sz);

		public static RDPointE operator -(RDPointE pt, RDSizeI sz) => Subtract(pt, sz);

		public static RDPointE operator -(RDPointE pt, RDSize sz) => Subtract(pt, sz);

		public static RDPointE operator -(RDPointE pt, RDSizeE sz) => Subtract(pt, sz);

		public static bool operator ==(RDPointE left, RDPointE right) => left.Equals(right);

		public static bool operator !=(RDPointE left, RDPointE right) => !left.Equals(right);

		public static explicit operator RDSizeE(RDPointE v)
		{
			RDSizeE result = new(v);
			return result;
		}
	}
}
