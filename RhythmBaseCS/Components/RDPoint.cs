using Newtonsoft.Json;
using RhythmBase.Converters;
using RhythmBase.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
namespace RhythmBase.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="float" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDPoint(float? x, float? y) : IEquatable<RDPoint>
	{
		public RDPoint(RDSize sz) : this(sz.Width, sz.Height) { }
		public readonly bool IsEmpty => X == null && Y == null;
		public float? X { get; set; } = x;
		public float? Y { get; set; } = y;
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
		public static RDPoint Add(RDPoint pt, RDSizeI sz) => new(
			pt.X + sz.Width, pt.Y + sz.Height
			);
		public static RDPoint Add(RDPoint pt, RDSize sz) => new(
			pt.X + sz.Width, pt.Y + sz.Height
			);
		public static RDPoint Subtract(RDPoint pt, RDSizeI sz) => new(
			pt.X - sz.Width, pt.Y - sz.Height
			);
		public static RDPoint Subtract(RDPoint pt, RDSize sz) => new(
			pt.X - sz.Width, pt.Y - sz.Height
			);
		public readonly RDPoint MultipyByMatrix(float[,] matrix)
		{
			if (matrix.Rank == 2 && matrix.Length == 4)
			{
				RDPoint MultipyByMatrix = new(X * matrix[0, 0] + Y * matrix[1, 0], X * matrix[0, 1] + Y * matrix[1, 1]);
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
		public readonly RDPoint Rotate(RDPointN pivot, float angle) => (this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);
		public override readonly bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDPoint) && Equals((obj != null) ? ((RDPoint)obj) : default);
		public override readonly int GetHashCode()
		{
			HashCode h = default;
			h.Add(X);
			h.Add(Y);
			return h.ToHashCode();
		}
		public override readonly string ToString()
		{
			string format = "[{0}, {1}]";
			float? num2;
			float? num = num2 = X;
			object objectValue = RuntimeHelpers.GetObjectValue((num2 != null) ? num.GetValueOrDefault() : "null");
			num = num2 = Y;
			return string.Format(format, objectValue, RuntimeHelpers.GetObjectValue((num2 != null) ? num.GetValueOrDefault() : "null"));
		}
		readonly bool IEquatable<RDPoint>.Equals(RDPoint other) => other.X.NullableEquals(X) && other.Y.NullableEquals(Y);
		public static RDPoint operator +(RDPoint pt, RDSizeI sz) => Add(pt, sz);
		public static RDPoint operator +(RDPoint pt, RDSize sz) => Add(pt, sz);
		public static RDPoint operator -(RDPoint pt, RDSizeI sz) => Subtract(pt, sz);
		public static RDPoint operator -(RDPoint pt, RDSize sz) => Subtract(pt, sz);
		public static bool operator ==(RDPoint left, RDPoint right) => left.Equals(right);
		public static bool operator !=(RDPoint left, RDPoint right) => !left.Equals(right);
		public static implicit operator RDPointE(RDPoint p) => new RDPointE(p.X, p.Y);
	}
}
