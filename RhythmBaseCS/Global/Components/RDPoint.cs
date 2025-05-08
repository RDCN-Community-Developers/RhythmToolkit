using Newtonsoft.Json;
using RhythmBase.RhythmDoctor.Extensions;
using RhythmBase.Global.Converters;
using RhythmBase.RhythmDoctor.Components;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Global.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="float" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDPoint(float? x, float? y) : IRDVortex<RDPoint, RDSize, float?>, IRDVortex<RDPoint, RDSizeI, float?>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDPoint"/> struct with the specified size.
		/// </summary>
		/// <param name="sz">The size to initialize the point with.</param>
		public RDPoint(RDSize sz) : this(sz.Width, sz.Height) { }

		/// <summary>
		/// Gets a value indicating whether this point is empty.
		/// </summary>
		public readonly bool IsEmpty => X == null && Y == null;

		/// <summary>
		/// Gets or sets the X coordinate of the point.
		/// </summary>
		public float? X { get; set; } = x;

		/// <summary>
		/// Gets or sets the Y coordinate of the point.
		/// </summary>
		public float? Y { get; set; } = y;

		/// <summary>
		/// Offsets the point by the specified point.
		/// </summary>
		/// <param name="p">The point to offset by.</param>
		public void Offset(RDPoint p)
		{
			X += p.X;
			Y += p.Y;
		}

		/// <summary>
		/// Offsets the point by the specified amounts.
		/// </summary>
		/// <param name="dx">The amount to offset the X coordinate.</param>
		/// <param name="dy">The amount to offset the Y coordinate.</param>
		public void Offset(float? dx, float? dy)
		{
			X += dx;
			Y += dy;
		}

		/// <summary>
		/// Adds the specified size to the point.
		/// </summary>
		/// <param name="pt">The point to add to.</param>
		/// <param name="sz">The size to add.</param>
		/// <returns>A new point that is the result of the addition.</returns>
		public static RDPoint Add(RDPoint pt, RDSizeI sz) => new(
		pt.X + sz.Width, pt.Y + sz.Height
		);

		/// <summary>
		/// Adds the specified size to the point.
		/// </summary>
		/// <param name="pt">The point to add to.</param>
		/// <param name="sz">The size to add.</param>
		/// <returns>A new point that is the result of the addition.</returns>
		public static RDPoint Add(RDPoint pt, RDSize sz) => new(
		pt.X + sz.Width, pt.Y + sz.Height
		);

		/// <summary>
		/// Subtracts the specified size from the point.
		/// </summary>
		/// <param name="pt">The point to subtract from.</param>
		/// <param name="sz">The size to subtract.</param>
		/// <returns>A new point that is the result of the subtraction.</returns>
		public static RDPoint Subtract(RDPoint pt, RDSizeI sz) => new(
		pt.X - sz.Width, pt.Y - sz.Height
		);

		/// <summary>
		/// Subtracts the specified size from the point.
		/// </summary>
		/// <param name="pt">The point to subtract from.</param>
		/// <param name="sz">The size to subtract.</param>
		/// <returns>A new point that is the result of the subtraction.</returns>
		public static RDPoint Subtract(RDPoint pt, RDSize sz) => new(
		pt.X - sz.Width, pt.Y - sz.Height
		);

		/// <summary>
		/// Multiplies the point by the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix to multiply by.</param>
		/// <returns>A new point that is the result of the multiplication.</returns>
		/// <exception cref="Exception">Thrown when the matrix is not a 2x2 matrix.</exception>
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
		public readonly RDPoint Rotate(float angle)
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
		public readonly RDPoint Rotate(RDPointN pivot, float angle) => (this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);
		/// <inheritdoc/>
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDPoint e && Equals(e);
		/// <inheritdoc/>
		public override readonly int GetHashCode()
		{
			HashCode h = default;
			h.Add(X);
			h.Add(Y);
			return h.ToHashCode();
		}
		/// <inheritdoc/>
		public override readonly string ToString() => $"[{X?.ToString() ?? "null"}, {Y?.ToString() ?? "null"}]";
		/// <inheritdoc/>
		public readonly bool Equals(RDPoint other) => other.X.NullableEquals(X) && other.Y.NullableEquals(Y);
		/// <inheritdoc/>
		public static RDPoint operator +(RDPoint pt, RDSizeI sz) => Add(pt, sz);
		/// <inheritdoc/>
		public static RDPoint operator +(RDPoint pt, RDSize sz) => Add(pt, sz);
		/// <inheritdoc/>
		public static RDPoint operator -(RDPoint pt, RDSizeI sz) => Subtract(pt, sz);
		/// <inheritdoc/>
		public static RDPoint operator -(RDPoint pt, RDSize sz) => Subtract(pt, sz);
		/// <inheritdoc/>
		public static RDPoint operator *(RDPoint pt, float? x) => new(pt.X * x, pt.Y * x);
		/// <inheritdoc/>
		public static RDPoint operator /(RDPoint pt, float? x) => new(pt.X / x, pt.Y / x);
		/// <inheritdoc/>
		public static bool operator ==(RDPoint left, RDPoint right) => left.Equals(right);
		/// <inheritdoc/>
		public static bool operator !=(RDPoint left, RDPoint right) => !left.Equals(right);
		/// <summary>
		/// Implicitly converts an <see cref="RDPoint"/> to an <see cref="RDPointE"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPoint"/> to convert.</param>
		/// <returns>A new <see cref="RDPointE"/> with the same coordinates as the input <see cref="RDPoint"/>.</returns>
		public static implicit operator RDPointE(RDPoint p) => new(p.X, p.Y);
		/// <summary>
		/// Explicitly converts an <see cref="RDPoint"/> to an <see cref="RDSize"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPoint"/> to convert.</param>
		/// <returns>A new <see cref="RDSize"/> with the same coordinates as the input <see cref="RDPoint"/>.</returns>
		public static explicit operator RDSize(RDPoint p) => new(p.X, p.Y);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
