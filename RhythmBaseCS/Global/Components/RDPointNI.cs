using Newtonsoft.Json;
using RhythmBase.Global.Converters;
using RhythmBase.RhythmDoctor.Components;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Global.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDPointNI(int x, int y) : IRDVortex<RDPointNI, RDSizeNI, int>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointNI"/> struct with the specified size.
		/// </summary>
		/// <param name="sz">The size to initialize the point with.</param>
		public RDPointNI(RDSizeNI sz) : this(sz.Width, sz.Height) { }
		/// <summary>
		/// Gets or sets the X coordinate of the point.
		/// </summary>
		public int X { get; set; } = x;
		/// <summary>
		/// Gets or sets the Y coordinate of the point.
		/// </summary>
		public int Y { get; set; } = y;
		/// <summary>
		/// Offsets the point by the specified point.
		/// </summary>
		/// <param name="p">The point to offset by.</param>
		public void Offset(RDPointNI p)
		{
			X += p.X;
			Y += p.Y;
		}
		/// <summary>
		/// Offsets the point by the specified size.
		/// </summary>
		/// <param name="p">The size to offset by.</param>
		public void Offset(RDSizeNI p)
		{
			X += p.Width;
			Y += p.Height;
		}
		/// <summary>
		/// Offsets the point by the specified horizontal and vertical amounts.
		/// </summary>
		/// <param name="dx">The horizontal offset.</param>
		/// <param name="dy">The vertical offset.</param>
		public void Offset(int dx, int dy)
		{
			X += dx;
			Y += dy;
		}
		/// <summary>
		/// Returns a new point with coordinates rounded up to the nearest integer values.
		/// </summary>
		/// <param name="value">The point to round up.</param>
		/// <returns>A new point with coordinates rounded up.</returns>
		public static RDPointNI Ceiling(RDPointN value) => new(
		(int)Math.Ceiling((double)value.X),
		(int)Math.Ceiling((double)value.Y)
		);
		/// <summary>
		/// Adds the specified size to the point.
		/// </summary>
		/// <param name="pt">The point to add to.</param>
		/// <param name="sz">The size to add.</param>
		/// <returns>A new point with the size added.</returns>
		public static RDPointNI Add(RDPointNI pt, RDSizeNI sz) => new(
		pt.X + sz.Width, pt.Y + sz.Height
		);
		/// <summary>
		/// Returns a new point with coordinates truncated to the nearest integer values.
		/// </summary>
		/// <param name="value">The point to truncate.</param>
		/// <returns>A new point with coordinates truncated.</returns>
		public static RDPointNI Truncate(RDPointN value) => new(
		(int)Math.Truncate((double)value.X),
		(int)Math.Truncate((double)value.Y)
		);
		/// <summary>
		/// Subtracts the specified size from the point.
		/// </summary>
		/// <param name="pt">The point to subtract from.</param>
		/// <param name="sz">The size to subtract.</param>
		/// <returns>A new point with the size subtracted.</returns>
		public static RDPointNI Subtract(RDPointNI pt, RDSizeNI sz) => new(
		pt.X - sz.Width, pt.Y - sz.Height
		);
		/// <summary>
		/// Returns a new point with coordinates rounded to the nearest integer values.
		/// </summary>
		/// <param name="value">The point to round.</param>
		/// <returns>A new point with coordinates rounded.</returns>
		public static RDPointNI Round(RDPointN value) => new(
		(int)Math.Round((double)value.X),
		(int)Math.Round((double)value.Y)
		);
		/// <summary>
		/// Multiplies the point by the specified 2x2 matrix.
		/// </summary>
		/// <param name="matrix">The 2x2 matrix to multiply by.</param>
		/// <returns>A new point resulting from the matrix multiplication.</returns>
		/// <exception cref="Exception">Thrown when the matrix is not a 2x2 matrix.</exception>
		public readonly RDPointN MultipyByMatrix(float[,] matrix)
		{
			if (matrix.Rank == 2 && matrix.Length == 4)
			{
				RDPointN MultipyByMatrix = new(X * matrix[0, 0] + Y * matrix[1, 0], X * matrix[0, 1] + Y * matrix[1, 1]);
				return MultipyByMatrix;
			}
			throw new Exception("Matrix not match, 2*2 matrix expected.");
		}
		/// <summary>
		/// Rotate.
		/// </summary>
		public readonly RDPointN Rotate(float angle)
		{
			float[,] array = new float[2, 2];
			array[0, 0] = (float)Math.Cos((double)angle);
			array[0, 1] = (float)Math.Sin((double)angle);
			array[1, 0] = -(float)Math.Sin((double)angle);
			array[1, 1] = (float)Math.Cos((double)angle);
			return MultipyByMatrix(array);
		}
		/// <summary>
		/// Rotate at a given pivot.
		/// </summary>
		/// <param name="pivot">Given pivot.</param>
		/// <param name="angle">Angle.</param>
		/// <returns></returns>
		public readonly RDPointN Rotate(RDPointN pivot, float angle) => ((RDPointN)this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);
		/// <inheritdoc/>
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDPointNI e && Equals(e);
		/// <inheritdoc/>
		public override readonly int GetHashCode()
		{
			HashCode h = default;
			h.Add(X);
			h.Add(Y);
			return h.ToHashCode();
		}
		/// <inheritdoc/>
		public override readonly string ToString() => $"[{X}, {Y}]";
		/// <inheritdoc/>
		public readonly bool Equals(RDPointNI other) => other.X == X && other.Y == Y;
		/// <inheritdoc/>
		public static RDPointNI operator +(RDPointNI pt, RDSizeNI sz) => Add(pt, sz);
		/// <inheritdoc/>
		public static RDPointNI operator -(RDPointNI pt, RDSizeNI sz) => Subtract(pt, sz);
		/// <inheritdoc/>
		public static RDPointNI operator *(RDPointNI pt, int x) => new(pt.X * x, pt.Y * x);
		/// <inheritdoc/>
		public static RDPointNI operator /(RDPointNI pt, int x) => new(pt.X / x, pt.Y / x);
		/// <inheritdoc/>
		public static bool operator ==(RDPointNI left, RDPointNI right) => left.Equals(right);
		/// <inheritdoc/>
		public static bool operator !=(RDPointNI left, RDPointNI right) => !left.Equals(right);
		/// <summary>
		/// Implicitly converts an <see cref="RDPointNI"/> to an <see cref="RDPointN"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointNI"/> to convert.</param>
		/// <returns>An <see cref="RDPointN"/> with the same coordinates.</returns>
		public static implicit operator RDPointN(RDPointNI p) => new(p.X, p.Y);
		/// <summary>
		/// Implicitly converts an <see cref="RDPointNI"/> to an <see cref="RDPointI"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointNI"/> to convert.</param>
		/// <returns>An <see cref="RDPointI"/> with the same coordinates.</returns>
		public static implicit operator RDPointI(RDPointNI p) => new(new int?(p.X), new int?(p.Y));
		/// <summary>
		/// Implicitly converts an <see cref="RDPointNI"/> to an <see cref="RDPointE"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointNI"/> to convert.</param>
		/// <returns>An <see cref="RDPointE"/> with the same coordinates.</returns>
		public static implicit operator RDPointE(RDPointNI p) => new(p.X, p.Y);
		/// <summary>
		/// Explicitly converts an <see cref="RDPointNI"/> to an <see cref="RDSizeNI"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointNI"/> to convert.</param>
		/// <returns>An <see cref="RDSizeNI"/> with the same dimensions.</returns>
		public static explicit operator RDSizeNI(RDPointNI p) => new(p.X, p.Y);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
