using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDPointI(int? x, int? y) : IEquatable<RDPointI>
	{
		public RDPointI(RDSizeI sz) : this(sz.Width, sz.Height) { }
		public RDPointI(RDSizeN sz) : this(
			(int)Math.Round((double)sz.Width),
			(int)Math.Round((double)sz.Height))
		{ }
		public readonly bool IsEmpty => X == null && Y == null;
		public int? X { get; set; } = x;
		public int? Y { get; set; } = y;
		public void Offset(RDPointI p)
		{
			X += p.X;
			Y += p.Y;
		}
		public void Offset(int? dx, int? dy)
		{
			X += dx;
			Y += dy;
		}
		public static RDPointI Ceiling(RDPoint value) => new(
			(value.X == null) ? null : (int)Math.Ceiling((double)value.X),
			(value.Y == null) ? null : (int)Math.Ceiling((double)value.Y)
			);

		public static RDPointI Add(RDPointI pt, RDSizeI sz) => new(
			pt.X + sz.Width, pt.Y + sz.Height
			);
		public static RDPointI Truncate(RDPoint value) => new(
			(value.X == null) ? null : (int)Math.Truncate((double)value.X),
			(value.Y == null) ? null : (int)Math.Truncate((double)value.Y)
			);
		public static RDPointI Subtract(RDPointI pt, RDSizeI sz) => new(
			pt.X - sz.Width, pt.Y - sz.Height
			);
		public static RDPointI Round(RDPoint value) => new(
			((value.X == null) ? null : (int)Math.Round((double)value.X.Value)),
			((value.Y == null) ? null : (int)Math.Round((double)value.Y.Value))
			);
		public readonly RDPoint MultipyByMatrix(float[,] matrix)
		{
			if (matrix.Rank == 2 && matrix.Length == 4)
			{
				int? num = X;
				float? num2 = (num == null ? null : num) * matrix[0, 0];
				num = Y;
				float? x = num2 + ((num == null ? null : num) * matrix[1, 0]);
				num = X;
				float? num3 = (num == null ? null : num) * matrix[0, 1];
				num = Y;
				RDPoint MultipyByMatrix = new(x, num3 + ((num != null) ? num : null) * matrix[1, 1]);
				return MultipyByMatrix;
			}
			throw new Exception("Matrix not match, 2*2 matrix expected.");
		}
		/// <summary>
		/// Rotate.
		/// </summary>
		public readonly RDPoint Rotate(float angle)
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
		public readonly RDPoint Rotate(RDPointN pivot, float angle) => ((RDPoint)this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);
		public override readonly bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDPointI) && Equals((obj != null) ? ((RDPointI)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(X, Y);
		public override readonly string ToString() => $"[{(X?.ToString()) ?? "null"},{(Y?.ToString()) ?? "null"}]";
		readonly bool IEquatable<RDPointI>.Equals(RDPointI other) => other.X == X && other.Y == Y;
		public static RDPointI operator +(RDPointI pt, RDSizeI sz) => Add(pt, sz);
		public static RDPointI operator -(RDPointI pt, RDSizeI sz) => Subtract(pt, sz);
		public static bool operator ==(RDPointI left, RDPointI right) => left.Equals(right);
		public static bool operator !=(RDPointI left, RDPointI right) => !left.Equals(right);
		public static implicit operator RDPoint(RDPointI p) => new(p.X, p.Y);
		public static implicit operator RDPointE(RDPointI p) => new(p.X, p.Y);
		public static explicit operator RDSizeI(RDPointI p) => new(p);
	}
}
