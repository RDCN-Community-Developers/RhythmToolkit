using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDPointNI(int x, int y) : IEquatable<RDPointNI>
	{
		public RDPointNI(RDSizeNI sz) : this(sz.Width, sz.Height) { }
		public int X { get; set; } = x;
		public int Y { get; set; } = y;
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
		public static RDPointNI Ceiling(RDPointN value) => new(
			(int)Math.Ceiling((double)value.X),
			(int)Math.Ceiling((double)value.Y)
			);
		public static RDPointNI Add(RDPointNI pt, RDSizeNI sz) => new(
			pt.X + sz.Width, pt.Y + sz.Height
			);
		public static RDPointNI Truncate(RDPointN value) => new(
			(int)Math.Truncate((double)value.X),
			(int)Math.Truncate((double)value.Y)
			);
		public static RDPointNI Subtract(RDPointNI pt, RDSizeNI sz) => new(
			pt.X - sz.Width, pt.Y - sz.Height
			);
		public static RDPointNI Round(RDPointN value) => new(
			(int)Math.Round((double)value.X),
			(int)Math.Round((double)value.Y)
			);
		public readonly RDPoint MultipyByMatrix(float[,] matrix)
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
		public readonly RDPointN Rotate(RDPointN pivot, float angle) => ((RDPointN)this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);
		public override readonly bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDPointNI) && Equals((obj != null) ? ((RDPointNI)obj) : default);
		public override readonly int GetHashCode()
		{
			HashCode h = default;
			h.Add(X);
			h.Add(Y);
			return h.ToHashCode();
		}
		public override readonly string ToString() => string.Format("[{0}, {1}]", X, Y);
		readonly bool IEquatable<RDPointNI>.Equals(RDPointNI other) => other.X == X && other.Y == Y;
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
		public static implicit operator RDPointE(RDPointNI p)
		{
			RDPointE result = new((float)p.X, (float)p.Y);
			return result;
		}
		public static explicit operator RDSizeNI(RDPointNI p)
		{
			RDSizeNI result = new(p.X, p.Y);
			return result;
		}
	}
}
