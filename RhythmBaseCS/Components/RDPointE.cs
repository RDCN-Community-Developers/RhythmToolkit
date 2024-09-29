using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>nullable</strong> <seealso cref="T:RhythmBase.Components.Expression" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDPointE(RDExpression? x,RDExpression? y) : IEquatable<RDPointE>
	{
		public RDPointE(RDSize sz):this(sz.Width,sz.Height) { }
		public RDPointE(float x, float y):this( (RDExpression)x,(RDExpression)y) { }
		public RDPointE(RDExpression? x, float y) : this(x, (RDExpression)y) { }
		public RDPointE(float x, RDExpression? y) : this((RDExpression)x, y) { }
		public RDPointE(string x, float y):this((RDExpression)x, (RDExpression)y) { }	
		public RDPointE(float x, string y):this((RDExpression)x, (RDExpression)y) { }
		public RDPointE(string x, RDExpression? y):this((RDExpression)x, (RDExpression)y) {  }
		public RDPointE(RDExpression? x, string y):this(x, (RDExpression)y) { }
		public RDPointE(string x, string y):this ((RDExpression)x, (RDExpression)y) { }
		public RDPointE(RDPointI p):this(p.X,p.Y) { }
		public RDPointE(RDPoint p):this(p.X,p.Y) { }
		public readonly bool IsEmpty => X == null && Y == null;
		public RDExpression? X { get; set; } = x;
		public RDExpression? Y { get; set; } = y;
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
		public static RDPointE Add(RDPointE pt, RDSizeI sz) => new(
			pt.X + sz.Width, pt.Y + sz.Height
			);
		public static RDPointE Add(RDPointE pt, RDSize sz) => new(
			pt.X + sz.Width, pt.Y + sz.Height
			);
		public static RDPointE Add(RDPointE pt, RDSizeE sz) => new(
			pt.X + sz.Width, pt.Y + sz.Height
			);
		public static RDPointE Subtract(RDPointE pt, RDSizeI sz) => new(
			pt.X - sz.Width, pt.Y - sz.Height
			);
		public static RDPointE Subtract(RDPointE pt, RDSize sz) => new(
			pt.X - sz.Width, pt.Y - sz.Height
			);
		public static RDPointE Subtract(RDPointE pt, RDSizeE sz) => new(
			pt.X - sz.Width, pt.Y - sz.Height
			);
		public override readonly bool Equals([NotNullWhen(true)] object obj) => 
			obj.GetType() == typeof(RDPoint) && Equals((obj != null) ? ((RDPoint)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(X, Y);
		public override readonly string ToString() => $"[{(X?.ExpressionValue) ?? "null"},{(Y?.ExpressionValue) ?? "null"}]";
		readonly bool IEquatable<RDPointE>.Equals(RDPointE other) => other.X == X && other.Y == Y;
		public readonly RDPointE MultipyByMatrix(RDExpression[,] matrix)
		{
			if (matrix.Rank == 2 && matrix.Length == 4)
			{
				RDPointE MultipyByMatrix = new(
					X * matrix[0, 0] + Y * matrix[1, 0], 
					X * matrix[0, 1] + Y * matrix[1, 1]);
				return MultipyByMatrix;
			}
			throw new Exception("Matrix not match, 2*2 matrix expected.");
		}
		/// <summary>
		/// Rotate.
		/// </summary>
		public RDPointE Rotate(float angle)
		{
			RDExpression[,] array = new RDExpression[2, 2];
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
		public readonly RDPointE Rotate(RDPointE pivot, float angle) => (this - new RDSizeE(pivot)).Rotate(angle) + new RDSizeE(pivot);
		public static RDPointE operator +(RDPointE pt, RDSizeI sz) => Add(pt, sz);
		public static RDPointE operator +(RDPointE pt, RDSize sz) => Add(pt, sz);
		public static RDPointE operator +(RDPointE pt, RDSizeE sz) => Add(pt, sz);
		public static RDPointE operator -(RDPointE pt, RDSizeI sz) => Subtract(pt, sz);
		public static RDPointE operator -(RDPointE pt, RDSize sz) => Subtract(pt, sz);
		public static RDPointE operator -(RDPointE pt, RDSizeE sz) => Subtract(pt, sz);
		public static bool operator ==(RDPointE left, RDPointE right) => left.Equals(right);
		public static bool operator !=(RDPointE left, RDPointE right) => !left.Equals(right);

		public static explicit operator RDSizeE(RDPointE p) => new(p);
	}
}
