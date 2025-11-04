using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Global.Components.Vector
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="float" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDSizeN(float width, float height) : IRDVector<RDSizeN, RDSizeN, float>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeN"/> struct with the specified point.
		/// </summary>
		/// <param name="pt">The point to initialize the size with.</param>
		public RDSizeN(RDPointN pt) : this(pt.X, pt.Y) { }
		/// <summary>
		/// Gets or sets the width of the size.
		/// </summary>
		public float Width { get; set; } = width;
		/// <summary>
		/// Gets or sets the height of the size.
		/// </summary>
		public float Height { get; set; } = height;
		/// <summary>
		/// Gets the area of the size.
		/// </summary>
		public readonly float Area => Width * Height;
		/// <summary>
		/// Adds two sizes together.
		/// </summary>
		/// <param name="sz1">The first size.</param>
		/// <param name="sz2">The second size.</param>
		/// <returns>The result of adding the two sizes.</returns>
		public static RDSizeN Add(RDSizeN sz1, RDSizeN sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		/// <summary>
		/// Subtracts one size from another.
		/// </summary>
		/// <param name="sz1">The size to subtract from.</param>
		/// <param name="sz2">The size to subtract.</param>
		/// <returns>The result of subtracting the second size from the first size.</returns>
		public static RDSizeN Subtract(RDSizeN sz1, RDSizeN sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly int GetHashCode()
		{
			int hash = 17;
			hash = hash * 23 + Width.GetHashCode();
			hash = hash * 23 + Height.GetHashCode();
			return hash;
		}
#else
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
#endif
		/// <inheritdoc/>
		public override readonly string ToString() => $"[{Width},{Height}]";
		/// <inheritdoc/>
		public readonly bool Equals(RDSizeN other) => Width == other.Width && Height == other.Height;
		/// <summary>
		/// Converts the current size to an integer size.
		/// </summary>
		/// <returns>A new <see cref="RDSizeNI"/> instance with the width and height rounded to the nearest integer.</returns>
		public readonly RDSizeNI ToSizeI() => new((int)Math.Round((double)Width), (int)Math.Round((double)Height));
		/// <summary>
		/// Converts the current size to a point.
		/// </summary>
		/// <returns>A new <see cref="RDPointN"/> instance with the width as the X coordinate and the height as the Y coordinate.</returns>
		public readonly RDPointN ToPoint() => new(Width, Height);
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly bool Equals(object? obj) => obj is RDSizeN e && Equals(e);
#else
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDSizeN e && Equals(e);
#endif
		/// <inheritdoc/>
		public static RDSizeN operator +(RDSizeN sz1, RDSizeN sz2) => Add(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeN operator -(RDSizeN sz1, RDSizeN sz2) => Subtract(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeN operator *(float left, RDSizeN right) => new(left * right.Width, left * right.Height);
		/// <inheritdoc/>
		public static RDSizeN operator *(RDSizeN left, float right) => new(left.Width * right, left.Height * right);
		/// <inheritdoc/>
		public static RDSizeN operator /(RDSizeN left, float right) => new(left.Width / right, left.Height / right);
		/// <inheritdoc/>
		public static bool operator ==(RDSizeN sz1, RDSizeN sz2) => sz1.Equals(sz2);
		/// <inheritdoc/>
		public static bool operator !=(RDSizeN sz1, RDSizeN sz2) => !sz1.Equals(sz2);
		/// <summary>
		/// Implicitly converts an <see cref="RDSizeN"/> to an <see cref="RDSize"/>.
		/// </summary>
		/// <param name="size">The <see cref="RDSizeN"/> to convert.</param>
		/// <returns>A new <see cref="RDSize"/> instance.</returns>
		public static implicit operator RDSize(RDSizeN size) => new(new float?(size.Width), new float?(size.Height));
		/// <summary>
		/// Implicitly converts an <see cref="RDSizeN"/> to an <see cref="RDSizeE"/>.
		/// </summary>
		/// <param name="size">The <see cref="RDSizeN"/> to convert.</param>
		/// <returns>A new <see cref="RDSizeE"/> instance.</returns>
		public static implicit operator RDSizeE(RDSizeN size) => new(size.Width, size.Height);
		/// <summary>
		/// Explicitly converts an <see cref="RDSizeN"/> to an <see cref="RDPointN"/>.
		/// </summary>
		/// <param name="size">The <see cref="RDSizeN"/> to convert.</param>
		/// <returns>A new <see cref="RDPointN"/> instance.</returns>
		public static explicit operator RDPointN(RDSizeN size) => new(size.Width, size.Height);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
