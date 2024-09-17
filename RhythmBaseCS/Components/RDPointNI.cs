using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDPointNI : IEquatable<RDPointNI>
	{
		public RDPointNI(RDSizeI sz)
		{
			this = default;
			X = sz.Width ?? 0;
			Y = sz.Height ?? 0;
		}
		public RDPointNI(int x, int y)
		{
			this = default;
			this.X = x;
			this.Y = y;
		}
		public int X { get; set; }
		public int Y { get; set; }
		public void Offset(RDPointNI p)
		{
			X += p.X;
			Y += p.Y;
		}
		public void Offset(int dx, int dy)
		{
			X += dx;
			Y += dy;
		}
		public static RDPointNI Ceiling(RDPointN value)
		{
			RDPointNI Ceiling = checked(new RDPointNI((int)Math.Ceiling((double)value.X), (int)Math.Ceiling((double)value.Y)));
			return Ceiling;
		}
		public static RDPointNI Add(RDPointNI pt, RDSizeNI sz)
		{
			RDPointNI Add = checked(new RDPointNI(pt.X + sz.Width, pt.Y + sz.Height));
			return Add;
		}
		public static RDPointNI Truncate(RDPointN value)
		{
			RDPointNI Truncate = checked(new RDPointNI((int)(double)value.X, (int)(double)value.Y));
			return Truncate;
		}
		public static RDPointNI Subtract(RDPointNI pt, RDSizeNI sz)
		{
			RDPointNI Subtract = checked(new RDPointNI(pt.X - sz.Width, pt.Y - sz.Height));
			return Subtract;
		}
		public static RDPointNI Round(RDPointN value)
		{
			RDPointNI Round = checked(new RDPointNI((int)(double)value.X, (int)(double)value.Y));
			return Round;
		}
		public RDPoint MultipyByMatrix(float[,] matrix)
		{
			if (matrix.Rank == 2 && matrix.Length == 4)
			{
				RDPoint MultipyByMatrix = new(new float?((float)X * matrix[0, 0] + (float)Y * matrix[1, 0]), new float?((float)X * matrix[0, 1] + (float)Y * matrix[1, 1]));
				return MultipyByMatrix;
			}
			throw new Exception("Matrix not match, 2*2 matrix expected.");
		}
		/// <summary>
		/// Rotate.
		/// </summary>
		public RDPoint Rotate(float angle)
		{
			float[,] array = new float[2, 2];
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
		public RDPointN Rotate(RDPointN pivot, float angle) => ((RDPointN)this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDPointNI) && Equals((obj != null) ? ((RDPointNI)obj) : default);
		public override int GetHashCode()
		{
			HashCode h = default;
			h.Add(X);
			h.Add(Y);
			return h.ToHashCode();
		}
		public override string ToString() => string.Format("[{0}, {1}]", X, Y);
		bool IEquatable<RDPointNI>.Equals(RDPointNI other) => other.X == X && other.Y == Y;
		public static RDPointNI operator +(RDPointNI pt, RDSizeNI sz) => Add(pt, sz);
		public static RDPointNI operator -(RDPointNI pt, RDSizeNI sz) => Subtract(pt, sz);
		public static bool operator ==(RDPointNI left, RDPointNI right) => left.Equals(right);
		public static bool operator !=(RDPointNI left, RDPointNI right) => !left.Equals(right);
		public static implicit operator RDPointN(RDPointNI p)
		{
			RDPointN result = new((float)p.X, (float)p.Y);
			return result;
		}
		public static implicit operator RDPointI(RDPointNI p)
		{
			RDPointI result = new(new int?(p.X), new int?(p.Y));
			return result;
		}
		public static implicit operator PointE(RDPointNI p)
		{
			PointE result = new((float)p.X, (float)p.Y);
			return result;
		}
		public static explicit operator RDSizeNI(RDPointNI p)
		{
			RDSizeNI result = new(p.X, p.Y);
			return result;
		}
	}
}
