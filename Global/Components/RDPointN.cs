using Newtonsoft.Json;
using RhythmBase.Global.Converters;
using RhythmBase.RhythmDoctor.Components;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Global.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="float" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDPointN(float x, float y) : IRDVortex<RDPointN, RDSizeN, float>, IRDVortex<RDPointN, RDSizeNI, float>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDPointN"/> struct with the specified size.
		/// </summary>
		/// <param name="sz">The size to initialize the point with.</param>
		public RDPointN(RDSizeN sz) : this(sz.Width, sz.Height) { }
		/// <summary>
		/// Gets or sets the X coordinate of the point.
		/// </summary>
		public float X { get; set; } = x;
		/// <summary>
		/// Gets or sets the Y coordinate of the point.
		/// </summary>
		public float Y { get; set; } = y;
		/// <summary>
		/// Offsets the point by the specified size.
		/// </summary>
		/// <param name="p">The size to offset the point by.</param>
		public void Offset(RDSizeN p)
		{
			X += p.Width;
			Y += p.Height;
		}
		/// <summary>
		/// Offsets the point by the specified point.
		/// </summary>
		/// <param name="p">The point to offset the point by.</param>
		public void Offset(RDPointN p)
		{
			X += p.X;
			Y += p.Y;
		}
		/// <summary>
		/// Offsets the point by the specified horizontal and vertical amounts.
		/// </summary>
		/// <param name="dx">The horizontal amount to offset the point by.</param>
		/// <param name="dy">The vertical amount to offset the point by.</param>
		public void Offset(float dx, float dy)
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
		public static RDPointN Add(RDPointN pt, RDSizeNI sz)
		{
			RDPointN Add = new(pt.X + sz.Width, pt.Y + sz.Height);
			return Add;
		}
		/// <summary>
		/// Adds the specified size to the point.
		/// </summary>
		/// <param name="pt">The point to add to.</param>
		/// <param name="sz">The size to add.</param>
		/// <returns>A new point that is the result of the addition.</returns>
		public static RDPointN Add(RDPointN pt, RDSizeN sz)
		{
			RDPointN Add = new(pt.X + sz.Width, pt.Y + sz.Height);
			return Add;
		}
		/// <summary>
		/// Subtracts the specified size from the point.
		/// </summary>
		/// <param name="pt">The point to subtract from.</param>
		/// <param name="sz">The size to subtract.</param>
		/// <returns>A new point that is the result of the subtraction.</returns>
		public static RDPointN Subtract(RDPointN pt, RDSizeNI sz)
		{
			RDPointN Subtract = new(pt.X - sz.Width, pt.Y - sz.Height);
			return Subtract;
		}
		/// <summary>
		/// Subtracts the specified size from the point.
		/// </summary>
		/// <param name="pt">The point to subtract from.</param>
		/// <param name="sz">The size to subtract.</param>
		/// <returns>A new point that is the result of the subtraction.</returns>
		public static RDPointN Subtract(RDPointN pt, RDSizeN sz)
		{
			RDPointN Subtract = new(pt.X - sz.Width, pt.Y - sz.Height);
			return Subtract;
		}
		/// <summary>
		/// Multiplies the point by the specified 2x2 matrix.
		/// </summary>
		/// <param name="matrix">The 2x2 matrix to multiply the point by.</param>
		/// <returns>A new point that is the result of the multiplication.</returns>
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
		public readonly RDPointN Rotate(RDPointN pivot, float angle) => (this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly bool Equals(object? obj) => obj is RDPointN e && Equals(e);
#else
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDPointN e && Equals(e);
#endif
		/// <inheritdoc/>
		public override readonly int GetHashCode()
		{
#if NETSTANDARD
			int hash = 17;
			hash = hash * 23 + X.GetHashCode();
			hash = hash * 23 + Y.GetHashCode();
			return hash;
#else
			HashCode h = default;
			h.Add(X);
			h.Add(Y);
			return h.ToHashCode();
#endif
		}
		/// <inheritdoc/>
		public override readonly string ToString() => $"[{X}, {Y}]";
		/// <inheritdoc/>
		public readonly bool Equals(RDPointN other) => other.X == X && other.Y == Y;
		/// <inheritdoc/>
		public static RDPointN operator +(RDPointN pt, RDSizeNI sz) => Add(pt, sz);
		/// <inheritdoc/>
		public static RDPointN operator +(RDPointN pt, RDSizeN sz) => Add(pt, sz);
		/// <inheritdoc/>
		public static RDPointN operator -(RDPointN pt, RDSizeNI sz) => Subtract(pt, sz);
		/// <inheritdoc/>
		public static RDPointN operator -(RDPointN pt, RDSizeN sz) => Subtract(pt, sz);
		/// <inheritdoc/>
		public static RDPointN operator *(RDPointN pt, float x) => new(pt.X * x, pt.Y * x);
		/// <inheritdoc/>
		public static RDPointN operator /(RDPointN pt, float x) => new(pt.X / x, pt.Y / x);
		/// <inheritdoc/>
		public static bool operator ==(RDPointN left, RDPointN right) => left.Equals(right);
		/// <inheritdoc/>
		public static bool operator !=(RDPointN left, RDPointN right) => !left.Equals(right);
		/// <summary>
		/// Implicitly converts an <see cref="RDPointN"/> to an <see cref="RDPoint"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointN"/> to convert.</param>
		/// <returns>A new <see cref="RDPoint"/> with the same coordinates.</returns>
		public static implicit operator RDPoint(RDPointN p) => new(new float?(p.X), new float?(p.Y));
		/// <summary>
		/// Implicitly converts an <see cref="RDPointN"/> to an <see cref="RDPointE"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointN"/> to convert.</param>
		/// <returns>A new <see cref="RDPointE"/> with the same coordinates.</returns>
		public static implicit operator RDPointE(RDPointN p) => new(p.X, p.Y);
		/// <summary>
		/// Explicitly converts an <see cref="RDPointN"/> to an <see cref="RDSizeN"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDPointN"/> to convert.</param>
		/// <returns>A new <see cref="RDSizeN"/> with the same dimensions.</returns>
		public static explicit operator RDSizeN(RDPointN p) => new(p.X, p.Y);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
