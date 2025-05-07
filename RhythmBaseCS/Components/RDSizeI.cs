using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDSizeI(int? width, int? height) : IRDVortex<RDSizeI, RDSizeI, int?>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeI"/> struct with the specified point.
		/// </summary>
		/// <param name="pt">The point to initialize the size with.</param>
		public RDSizeI(RDPointI pt) : this(pt.X, pt.Y) { }		/// <summary>
		/// Gets a value indicating whether this size is empty (both width and height are null).
		/// </summary>
		public readonly bool IsEmpty => Width == null && Height == null;		/// <summary>
		/// Gets or sets the width of the size.
		/// </summary>
		public int? Width { get; set; } = width;		/// <summary>
		/// Gets or sets the height of the size.
		/// </summary>
		public int? Height { get; set; } = height;		/// <summary>
		/// Gets the area of the size (width multiplied by height).
		/// </summary>
		public readonly int? Area => Width * Height;		/// <summary>
		/// Adds two sizes together.
		/// </summary>
		/// <param name="sz1">The first size.</param>
		/// <param name="sz2">The second size.</param>
		/// <returns>The sum of the two sizes.</returns>
		public static RDSizeI Add(RDSizeI sz1, RDSizeI sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);		/// <summary>
		/// Truncates the specified size to the nearest integer values.
		/// </summary>
		/// <param name="value">The size to truncate.</param>
		/// <returns>The truncated size.</returns>
		public static RDSizeI Truncate(RDSize value) => new(
			(int)Math.Round((value.Width == null) ? 0.0 : Math.Truncate((double)value.Width.Value)),
			(int)Math.Round((value.Height == null) ? 0.0 : Math.Truncate((double)value.Height.Value)));		/// <summary>
		/// Subtracts one size from another.
		/// </summary>
		/// <param name="sz1">The size to subtract from.</param>
		/// <param name="sz2">The size to subtract.</param>
		/// <returns>The difference between the two sizes.</returns>
		public static RDSizeI Subtract(RDSizeI sz1, RDSizeI sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);		/// <summary>
		/// Rounds the specified size up to the nearest integer values.
		/// </summary>
		/// <param name="value">The size to round up.</param>
		/// <returns>The rounded size.</returns>
		public static RDSizeI Ceiling(RDSize value) => new(
			(int)Math.Round((value.Width == null) ? 0.0 : Math.Ceiling((double)value.Width.Value)),
			(int)Math.Round((value.Height == null) ? 0.0 : Math.Ceiling((double)value.Height.Value)));		/// <summary>
		/// Rounds the specified size to the nearest integer values.
		/// </summary>
		/// <param name="value">The size to round.</param>
		/// <returns>The rounded size.</returns>
		public static RDSizeI Round(RDSize value) => new(
				new int?((int)Math.Round((value.Width == null) ? 0.0 : Math.Round((double)value.Width.Value))),
				new int?((int)Math.Round((value.Height == null) ? 0.0 : Math.Round((double)value.Height.Value))));
		/// <inheritdoc/>
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDSizeI e && Equals(e);
		/// <inheritdoc/>
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
		/// <inheritdoc/>
		public override readonly string ToString() => $"[{Width},{Height}]";
		/// <inheritdoc/>
		public readonly bool Equals(RDSizeI other) => Width == other.Width && Height == other.Height;
		/// <inheritdoc/>
		public static RDSizeI operator +(RDSizeI sz1, RDSizeI sz2) => Add(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeI operator -(RDSizeI sz1, RDSizeI sz2) => Subtract(sz1, sz2);
		/// <inheritdoc/>
		public static RDSize operator *(float left, RDSizeI right) => new(left * right.Width, left * right.Height);
		/// <inheritdoc/>
		public static RDSize operator *(RDSizeI left, float right) => new(left.Width * right, left.Height * right);
		/// <inheritdoc/>
		public static RDSizeI operator *(int left, RDSizeI right) => new(left * right.Width, left * right.Height);
		/// <inheritdoc/>
		public static RDSizeI operator *(RDSizeI left, int? right) => new(left.Width * right, left.Height * right);
		/// <inheritdoc/>
		public static RDSize operator /(RDSizeI left, float right) => new(left.Width / right, left.Height / right);
		/// <inheritdoc/>
		public static RDSizeI operator /(RDSizeI left, int? right) => new(left.Width / right, left.Height / right);
		/// <inheritdoc/>
		public static bool operator ==(RDSizeI sz1, RDSizeI sz2) => sz1.Equals(sz2);
		/// <inheritdoc/>
		public static bool operator !=(RDSizeI sz1, RDSizeI sz2) => !sz1.Equals(sz2);
		/// <summary>
		/// Implicitly converts an <see cref="RDSizeI"/> to an <see cref="RDSize"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDSizeI"/> to convert.</param>
		/// <returns>A new <see cref="RDSize"/> with the same width and height.</returns>
		public static implicit operator RDSize(RDSizeI p) => new(p.Width, p.Height);
		/// <summary>
		/// Implicitly converts an <see cref="RDSizeI"/> to an <see cref="RDSizeE"/>.
		/// </summary>
		/// <param name="p">The <see cref="RDSizeI"/> to convert.</param>
		/// <returns>A new <see cref="RDSizeE"/> with the same width and height.</returns>
		public static implicit operator RDSizeE(RDSizeI p) => new(p.Width, p.Height);
		/// <summary>
		/// Explicitly converts an <see cref="RDSizeI"/> to an <see cref="RDPointI"/>.
		/// </summary>
		/// <param name="size">The <see cref="RDSizeI"/> to convert.</param>
		/// <returns>A new <see cref="RDPointI"/> with the same width and height.</returns>
		public static explicit operator RDPointI(RDSizeI size) => new(size.Width, size.Height);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
