using Newtonsoft.Json;
using RhythmBase.Global.Converters;
using RhythmBase.RhythmDoctor.Components;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Global.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>nullable</strong> <seealso cref="T:RhythmBase.Components.Expression" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDPointE(RDExpression? x, RDExpression? y) :
		IRDVortex<RDPointE, RDSize, RDExpression>,
		IRDVortex<RDPointE, RDSizeI, RDExpression>,
		IRDVortex<RDPointE, RDSizeE, RDExpression>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified size.
		/// </summary>
		/// <param name="sz">The size to initialize the point with.</param>
		public RDPointE(RDSize sz) : this(sz.Width, sz.Height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified coordinates.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public RDPointE(float x, float y) : this((RDExpression)x, (RDExpression)y) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified x-coordinate and y-coordinate.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public RDPointE(RDExpression? x, float y) : this(x, (RDExpression)y) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified x-coordinate and y-coordinate.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public RDPointE(float x, RDExpression? y) : this((RDExpression)x, y) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified x-coordinate and y-coordinate.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public RDPointE([AllowNull] string x, float y) : this(x ?? default(RDExpression?), (RDExpression)y) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified x-coordinate and y-coordinate.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public RDPointE(float x, [AllowNull] string y) : this((RDExpression)x, y ?? default(RDExpression?)) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified x-coordinate and y-coordinate.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public RDPointE([AllowNull] string x, RDExpression? y) : this(x ?? default(RDExpression?), y) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified x-coordinate and y-coordinate.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public RDPointE(RDExpression? x, [AllowNull] string y) : this(x, y ?? default(RDExpression?)) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified x-coordinate and y-coordinate.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public RDPointE([AllowNull] string x, [AllowNull] string y) : this(x ?? default(RDExpression?), y ?? default(RDExpression?)) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified point.
		/// </summary>
		/// <param name="p">The point to initialize the point with.</param>
		public RDPointE(RDPointI p) : this(p.X, p.Y) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointE"/> struct with the specified point.
		/// </summary>
		/// <param name="p">The point to initialize the point with.</param>
		public RDPointE(RDPoint p) : this(p.X, p.Y) { }
		/// <inheritdoc/>
		public readonly bool IsEmpty => X == null && Y == null;

		/// <inheritdoc/>
		public RDExpression? X { get; set; } = x;

		/// <inheritdoc/>
		public RDExpression? Y { get; set; } = y;

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
		/// <param name="dx">The amount to offset the x-coordinate.</param>
		/// <param name="dy">The amount to offset the y-coordinate.</param>
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
		/// <returns>The resulting point.</returns>
		public static RDPointE Add(RDPointE pt, RDSizeI sz) => new(
		pt.X + sz.Width, pt.Y + sz.Height
		);

		/// <inheritdoc cref="Add(RDPointE, RDSizeI)"/>
		public static RDPointE Add(RDPointE pt, RDSize sz) => new(
		pt.X + sz.Width, pt.Y + sz.Height
		);

		/// <inheritdoc cref="Add(RDPointE, RDSizeI)"/>
		public static RDPointE Add(RDPointE pt, RDSizeE sz) => new(
		pt.X + sz.Width, pt.Y + sz.Height
		);

		/// <summary>
		/// Subtracts the specified size from the point.
		/// </summary>
		/// <param name="pt">The point to subtract from.</param>
		/// <param name="sz">The size to subtract.</param>
		/// <returns>The resulting point.</returns>
		public static RDPointE Subtract(RDPointE pt, RDSizeI sz) => new(
		pt.X - sz.Width, pt.Y - sz.Height
		);

		/// <inheritdoc cref="Subtract(RDPointE, RDSizeI)"/>
		public static RDPointE Subtract(RDPointE pt, RDSize sz) => new(
		pt.X - sz.Width, pt.Y - sz.Height
		);

		/// <inheritdoc cref="Subtract(RDPointE, RDSizeI)"/>
		public static RDPointE Subtract(RDPointE pt, RDSizeE sz) => new(
		pt.X - sz.Width, pt.Y - sz.Height
		);

		/// <inheritdoc/>
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDPointE e && Equals(e);

		/// <inheritdoc/>
		public override readonly int GetHashCode() => HashCode.Combine(X, Y);

		/// <inheritdoc/>
		public override readonly string ToString() => $"[{(X?.ExpressionValue) ?? "null"},{(Y?.ExpressionValue) ?? "null"}]";

		/// <inheritdoc/>
		public readonly bool Equals(RDPointE other) => other.X == X && other.Y == Y;

		/// <summary>
		/// Multiplies the point by the specified matrix.
		/// </summary>
		/// <param name="matrix">The matrix to multiply by.</param>
		/// <returns>The resulting point.</returns>
		/// <exception cref="Exception">Thrown when the matrix is not 2x2.</exception>
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
		public readonly RDPointE Rotate(float angle)
		{
			RDExpression[,] array = new RDExpression[2, 2];
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
		public readonly RDPointE Rotate(RDPointE pivot, float angle) => (this - new RDSizeE(pivot)).Rotate(angle) + new RDSizeE(pivot);
		/// <inheritdoc/>
		public static RDPointE operator +(RDPointE pt, RDSizeI sz) => Add(pt, sz);
		/// <inheritdoc/>
		public static RDPointE operator +(RDPointE pt, RDSize sz) => Add(pt, sz);
		/// <inheritdoc/>
		public static RDPointE operator +(RDPointE pt, RDSizeE sz) => Add(pt, sz);
		/// <inheritdoc/>
		public static RDPointE operator -(RDPointE pt, RDSizeI sz) => Subtract(pt, sz);
		/// <inheritdoc/>
		public static RDPointE operator -(RDPointE pt, RDSize sz) => Subtract(pt, sz);
		/// <inheritdoc/>
		public static RDPointE operator -(RDPointE pt, RDSizeE sz) => Subtract(pt, sz);
		/// <inheritdoc/>
		public static RDPointE operator *(RDPointE pt, RDExpression x) => new(pt.X * x, pt.Y * x);
		/// <inheritdoc/>
		public static RDPointE operator /(RDPointE pt, RDExpression x) => new(pt.X / x, pt.Y / x);
		/// <inheritdoc/>
		public static bool operator ==(RDPointE left, RDPointE right) => left.Equals(right);
		/// <inheritdoc/>
		public static bool operator !=(RDPointE left, RDPointE right) => !left.Equals(right);

		/// <summary>
		/// Converts the specified <see cref="RDPointE"/> to an <see cref="RDSizeE"/>.
		/// </summary>
		/// <param name="p">The point to convert.</param>
		/// <returns>An <see cref="RDSizeE"/> that represents the converted point.</returns>
		public static explicit operator RDSizeE(RDPointE p) => new(p);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
