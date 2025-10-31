using System.Text.Json.Serialization;
using RhythmBase.RhythmDoctor.Components;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Global.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="float" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDSize(float? width, float? height) : IRDVortex<RDSize, RDSize, float?>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDSize"/> struct with the specified point.
		/// </summary>
		/// <param name="pt">The point to initialize the size with.</param>
		public RDSize(RDPoint pt) : this(pt.X, pt.Y) { }
		/// <summary>
		/// Gets a value indicating whether this size is empty (both width and height are null).
		/// </summary>
		public readonly bool IsEmpty => Width == null && Height == null;
		/// <summary>
		/// Gets or sets the width of the size.
		/// </summary>
		public float? Width { get; set; } = width;
		/// <summary>
		/// Gets or sets the height of the size.
		/// </summary>
		public float? Height { get; set; } = height;
		/// <summary>
		/// Gets the area of the size.
		/// </summary>
		public readonly float? Area => Width * Height;
		/// <summary>
		/// Adds two sizes together.
		/// </summary>
		/// <param name="sz1">The first size.</param>
		/// <param name="sz2">The second size.</param>
		/// <returns>The sum of the two sizes.</returns>
		public static RDSize Add(RDSize sz1, RDSize sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		/// <summary>
		/// Subtracts one size from another.
		/// </summary>
		/// <param name="sz1">The first size.</param>
		/// <param name="sz2">The second size.</param>
		/// <returns>The difference between the two sizes.</returns>
		public static RDSize Subtract(RDSize sz1, RDSize sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly int GetHashCode()
		{
			int hash = 17;
			hash = hash * 23 + (Width?.GetHashCode() ?? 0);
			hash = hash * 23 + (Height?.GetHashCode() ?? 0);
			return hash;
		}
#else
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
#endif
		/// <inheritdoc/>
		public override readonly string ToString() => $"[{Width},{Height}]";
		/// <inheritdoc/>
		public readonly bool Equals(RDSize other) => Width == other.Width && Height == other.Height;
		/// <summary>
		/// Converts this size to an <see cref="RDSizeI"/>.
		/// </summary>
		/// <returns>An <see cref="RDSizeI"/> that represents this size.</returns>
		public readonly RDSizeI ToSize() => new((int?)Width, (int?)Height);
		/// <summary>
		/// Converts this size to an <see cref="RDPoint"/>.
		/// </summary>
		/// <returns>An <see cref="RDPoint"/> that represents this size.</returns>
		public readonly RDPoint ToPointF() => new(Width, Height);
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly bool Equals(object? obj) => obj is RDSize e && Equals(e);
#else
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDSize e && Equals(e);
#endif
		/// <inheritdoc/>
		public static RDSize operator +(RDSize sz1, RDSize sz2) => Add(sz1, sz2);
		/// <inheritdoc/>
		public static RDSize operator -(RDSize sz1, RDSize sz2) => Subtract(sz1, sz2);
		/// <inheritdoc/>
		public static RDSize operator *(float left, RDSize right) => new(left * right.Width, left * right.Height);
		/// <inheritdoc/>
		public static RDSize operator *(RDSize left, float? right) => new(left.Width * right, left.Height * right);
		/// <inheritdoc/>
		public static RDSize operator /(RDSize left, float? right) => new(left.Width / right, left.Height / right);
		/// <inheritdoc/>
		public static bool operator ==(RDSize sz1, RDSize sz2) => sz1.Equals(sz2);
		/// <inheritdoc/>
		public static bool operator !=(RDSize sz1, RDSize sz2) => !sz1.Equals(sz2);

		/// <summary>
		/// Performs an implicit conversion from <see cref="RDSize"/> to <see cref="RDSizeE"/>.
		/// </summary>
		/// <param name="size">The size to convert.</param>
		/// <returns>An <see cref="RDSizeE"/> that represents the converted size.</returns>
		public static implicit operator RDSizeE(RDSize size) => new(size.Width, size.Height);

		/// <summary>
		/// Performs an explicit conversion from <see cref="RDSize"/> to <see cref="RDPoint"/>.
		/// </summary>
		/// <param name="size">The size to convert.</param>
		/// <returns>An <see cref="RDPoint"/> that represents the converted size.</returns>
		public static explicit operator RDPoint(RDSize size) => new(size.Width, size.Height);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
