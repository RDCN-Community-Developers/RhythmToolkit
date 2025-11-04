using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Global.Components.Vector
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDSizeNI(int width, int height) : IRDVector<RDSizeNI, RDSizeNI, int>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeNI"/> struct with the specified point.
		/// </summary>
		/// <param name="pt">The point to initialize the size with.</param>
		public RDSizeNI(RDPointNI pt) : this(pt.X, pt.Y) { }
		/// <summary>
		/// Gets or sets the width of the size.
		/// </summary>
		public int Width { get; set; } = width;
		/// <summary>
		/// Gets or sets the height of the size.
		/// </summary>
		public int Height { get; set; } = height;
		/// <summary>
		/// Gets the area of the size.
		/// </summary>
		public readonly int Area => Width * Height;
		/// <summary>
		/// Gets the screen size.
		/// </summary>
		public static RDSizeNI Screen => new(352, 198);
		/// <summary>
		/// Adds two sizes together.
		/// </summary>
		/// <param name="sz1">The first size.</param>
		/// <param name="sz2">The second size.</param>
		/// <returns>The sum of the two sizes.</returns>
		public static RDSizeNI Add(RDSizeNI sz1, RDSizeNI sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		/// <summary>
		/// Truncates the specified size to integer values.
		/// </summary>
		/// <param name="value">The size to truncate.</param>
		/// <returns>The truncated size.</returns>
		public static RDSizeNI Truncate(RDSizeN value) => new((int)value.Width, (int)value.Height);
		/// <summary>
		/// Subtracts one size from another.
		/// </summary>
		/// <param name="sz1">The first size.</param>
		/// <param name="sz2">The second size.</param>
		/// <returns>The difference between the two sizes.</returns>
		public static RDSizeNI Subtract(RDSizeNI sz1, RDSizeNI sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		/// <summary>
		/// Rounds up the specified size to the nearest integer values.
		/// </summary>
		/// <param name="value">The size to round up.</param>
		/// <returns>The rounded-up size.</returns>
		public static RDSizeNI Ceiling(RDSizeN value) => new(
			(int)Math.Ceiling((double)value.Width),
			(int)Math.Ceiling((double)value.Height));
		/// <summary>
		/// Rounds the specified size to the nearest integer values.
		/// </summary>
		/// <param name="value">The size to round.</param>
		/// <returns>The rounded size.</returns>
		public static RDSizeNI Round(RDSizeN value) => new(
			(int)Math.Round((double)value.Width),
			(int)Math.Round((double)value.Height));
		/// <summary>
		/// Converts the current size to a point.
		/// </summary>
		/// <returns>A <see cref="RDPointNI"/> with the same width and height as the current size.</returns>
		public readonly RDPointNI ToPoint() => new(Width, Height);
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly bool Equals(object? obj) => obj is RDSizeNI e && Equals(e);
#else
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDSizeNI e && Equals(e);
#endif
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly int GetHashCode()
		{
			int hash = 17;
			hash = hash * 31 + Width.GetHashCode();
			hash = hash * 31 + Height.GetHashCode();
			return hash;
		}
#else
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
#endif
		/// <inheritdoc/>
		public override readonly string ToString() => $"[{Width},{Height}]";
		/// <inheritdoc/>
		public readonly bool Equals(RDSizeNI other) => Width == other.Width && Height == other.Height;
		/// <inheritdoc/>
		public static RDSizeNI operator +(RDSizeNI sz1, RDSizeNI sz2) => Add(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeNI operator -(RDSizeNI sz1, RDSizeNI sz2) => Subtract(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeN operator *(float left, RDSizeNI right) => new(left * right.Width, left * right.Height);
		/// <inheritdoc/>
		public static RDSizeN operator *(RDSizeNI left, float right) => new(left.Width * right, left.Height * right);
		/// <inheritdoc/>
		public static RDSizeNI operator *(int left, RDSizeNI right) => new(left * right.Width, left * right.Height);
		/// <inheritdoc/>
		public static RDSizeNI operator *(RDSizeNI left, int right) => new(left.Width * right, left.Height * right);
		/// <inheritdoc/>
		public static RDSizeN operator /(RDSizeNI left, float right) => new(left.Width / right, left.Height / right);
		/// <inheritdoc/>
		public static RDSizeNI operator /(RDSizeNI left, int right) => new(
			left.Width / right,
			left.Height / right);
		/// <inheritdoc/>
		public static bool operator ==(RDSizeNI sz1, RDSizeNI sz2) => sz1.Equals(sz2);
		/// <inheritdoc/>
		public static bool operator !=(RDSizeNI sz1, RDSizeNI sz2) => !sz1.Equals(sz2);
		/// <summary>
		/// Implicitly converts an <see cref="RDSizeNI"/> to an <see cref="RDSizeN"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDSizeNI"/> to convert.</param>
		/// <returns>An <see cref="RDSizeN"/> with the same width and height as the input.</returns>
		public static implicit operator RDSizeN(RDSizeNI p) => new(p.Width, p.Height);
		/// <summary>
		/// Implicitly converts an <see cref="RDSizeNI"/> to an <see cref="RDSizeI"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDSizeNI"/> to convert.</param>
		/// <returns>An <see cref="RDSizeI"/> with the same width and height as the input.</returns>
		public static implicit operator RDSizeI(RDSizeNI p) => new(p.Width, p.Height);
		/// <summary>
		/// Implicitly converts an <see cref="RDSizeNI"/> to an <see cref="RDSizeE"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDSizeNI"/> to convert.</param>
		/// <returns>An <see cref="RDSizeE"/> with the same width and height as the input.</returns>
		public static implicit operator RDSizeE(RDSizeNI p) => new(p.Width, p.Height);
		/// <summary>
		/// Explicitly converts an <see cref="RDSizeNI"/> to an <see cref="RDPointNI"/>.
		/// </summary>
		/// <param name="size">The <see cref="RDSizeNI"/> to convert.</param>
		/// <returns>An <see cref="RDPointNI"/> with the same width and height as the input.</returns>
		public static explicit operator RDPointNI(RDSizeNI size) => new(size.Width, size.Height);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
