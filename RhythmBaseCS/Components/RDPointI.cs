using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDPointI(int? x, int? y) : IRDVortex<RDPointI, RDSizeI, int?>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointI"/> struct with the specified size.
		/// </summary>
		/// <param name="sz">The size to initialize the point with.</param>
		public RDPointI(RDSizeI sz) : this(sz.Width, sz.Height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointI"/> struct with the specified nullable size.
		/// </summary>
		/// <param name="sz">The nullable size to initialize the point with.</param>
		public RDPointI(RDSizeN sz) : this(
		   (int)Math.Round((double)sz.Width),
		   (int)Math.Round((double)sz.Height))
		{ }

		/// <summary>
		/// Gets a value indicating whether this point is empty.
		/// </summary>
		public readonly bool IsEmpty => X == null && Y == null;

		/// <summary>
		/// Gets or sets the X coordinate of this point.
		/// </summary>
		public int? X { get; set; } = x;

		/// <summary>
		/// Gets or sets the Y coordinate of this point.
		/// </summary>
		public int? Y { get; set; } = y;

		/// <summary>
		/// Offsets this point by the specified point.
		/// </summary>
		/// <param name="p">The point to offset by.</param>
		public void Offset(RDPointI p)
		{
			X += p.X;
			Y += p.Y;
		}

		/// <summary>
		/// Offsets this point by the specified amounts.
		/// </summary>
		/// <param name="dx">The amount to offset the X coordinate.</param>
		/// <param name="dy">The amount to offset the Y coordinate.</param>
		public void Offset(int? dx, int? dy)
		{
			X += dx;
			Y += dy;
		}

		/// <summary>
		/// Returns a new point that is the ceiling of the specified point.
		/// </summary>
		/// <param name="value">The point to ceiling.</param>
		/// <returns>A new point that is the ceiling of the specified point.</returns>
		public static RDPointI Ceiling(RDPoint value) => new(
		   (value.X == null) ? null : (int)Math.Ceiling((double)value.X),
		   (value.Y == null) ? null : (int)Math.Ceiling((double)value.Y)
		   );

		/// <summary>
		/// Adds the specified size to the specified point.
		/// </summary>
		/// <param name="pt">The point to add to.</param>
		/// <param name="sz">The size to add.</param>
		/// <returns>A new point that is the sum of the specified point and size.</returns>
		public static RDPointI Add(RDPointI pt, RDSizeI sz) => new(
		   pt.X + sz.Width, pt.Y + sz.Height
		   );

		/// <summary>
		/// Returns a new point that is the truncated value of the specified point.
		/// </summary>
		/// <param name="value">The point to truncate.</param>
		/// <returns>A new point that is the truncated value of the specified point.</returns>
		public static RDPointI Truncate(RDPoint value) => new(
		   (value.X == null) ? null : (int)Math.Truncate((double)value.X),
		   (value.Y == null) ? null : (int)Math.Truncate((double)value.Y)
		   );

		/// <summary>
		/// Subtracts the specified size from the specified point.
		/// </summary>
		/// <param name="pt">The point to subtract from.</param>
		/// <param name="sz">The size to subtract.</param>
		/// <returns>A new point that is the difference of the specified point and size.</returns>
		public static RDPointI Subtract(RDPointI pt, RDSizeI sz) => new(
		   pt.X - sz.Width, pt.Y - sz.Height
		   );

		/// <summary>
		/// Returns a new point that is the rounded value of the specified point.
		/// </summary>
		/// <param name="value">The point to round.</param>
		/// <returns>A new point that is the rounded value of the specified point.</returns>
		public static RDPointI Round(RDPoint value) => new(
		   ((value.X == null) ? null : (int)Math.Round((double)value.X.Value)),
		   ((value.Y == null) ? null : (int)Math.Round((double)value.Y.Value))
		   );

		/// <summary>
		/// Multiplies this point by the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix to multiply by.</param>
		/// <returns>A new point that is the result of the multiplication.</returns>
		/// <exception cref="Exception">Thrown when the matrix is not a 2x2 matrix.</exception>
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
		/// Rotates this point by the specified angle.
		/// </summary>
		/// <param name="angle">The angle to rotate by.</param>
		/// <returns>A new point that is the result of the rotation.</returns>
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
		/// <param name="pivot">Given pivot.</param>
		/// <param name="angle">Angle.</param>
		/// <returns></returns>
		public readonly RDPoint Rotate(RDPointN pivot, float angle) => ((RDPoint)this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);
		/// <inheritdoc/>
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDPointI e && Equals(e);
		/// <inheritdoc/>
		public override readonly int GetHashCode() => HashCode.Combine(X, Y);
		/// <inheritdoc/>
		public override readonly string ToString() => $"[{(X?.ToString()) ?? "null"},{(Y?.ToString()) ?? "null"}]";
		/// <inheritdoc/>
		public readonly bool Equals(RDPointI other) => other.X == X && other.Y == Y;
		/// <inheritdoc/>
		public static RDPointI operator +(RDPointI pt, RDSizeI sz) => Add(pt, sz);
		/// <inheritdoc/>
		public static RDPointI operator -(RDPointI pt, RDSizeI sz) => Subtract(pt, sz);
		/// <inheritdoc/>
		public static RDPointI operator *(RDPointI pt, int? x) => new(pt.X * x, pt.Y * x);
		/// <inheritdoc/>
		public static RDPointI operator /(RDPointI pt, int? x) => new(pt.X / x, pt.Y / x);
		/// <inheritdoc/>
		public static bool operator ==(RDPointI left, RDPointI right) => left.Equals(right);
		/// <inheritdoc/>
		public static bool operator !=(RDPointI left, RDPointI right) => !left.Equals(right);
		/// <summary>
		/// Implicitly converts an <see cref="RDPointI"/> to an <see cref="RDPoint"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointI"/> to convert.</param>
		/// <returns>A new <see cref="RDPoint"/> with the same coordinates as the input <see cref="RDPointI"/>.</returns>
		public static implicit operator RDPoint(RDPointI p) => new(p.X, p.Y);

		/// <summary>
		/// Implicitly converts an <see cref="RDPointI"/> to an <see cref="RDPointE"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointI"/> to convert.</param>
		/// <returns>A new <see cref="RDPointE"/> with the same coordinates as the input <see cref="RDPointI"/>.</returns>
		public static implicit operator RDPointE(RDPointI p) => new(p.X, p.Y);

		/// <summary>
		/// Explicitly converts an <see cref="RDPointI"/> to an <see cref="RDSizeI"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointI"/> to convert.</param>
		/// <returns>A new <see cref="RDSizeI"/> with the same dimensions as the input <see cref="RDPointI"/>.</returns>
		public static explicit operator RDSizeI(RDPointI p) => new(p);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
