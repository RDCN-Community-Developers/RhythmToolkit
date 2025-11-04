using RhythmBase.RhythmDoctor.Components;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Global.Components.Vector
{
	/// <summary>
	/// Represents a rectangle defined by four expressions: left, top, right, and bottom.
	/// </summary>
	/// <param name="left">The left expression of the rectangle.</param>
	/// <param name="top">The top expression of the rectangle.</param>
	/// <param name="right">The right expression of the rectangle.</param>
	/// <param name="bottom">The bottom expression of the rectangle.</param>
	public struct RDRectE(RDExpression? left, RDExpression? top, RDExpression? right, RDExpression? bottom) : IEquatable<RDRectE>
	{
		/// <summary>
		/// Gets or sets the left expression of the rectangle.
		/// </summary>
		public RDExpression? Left { get; set; } = left;
		/// <summary>
		/// Gets or sets the right expression of the rectangle.
		/// </summary>
		public RDExpression? Right { get; set; } = right;
		/// <summary>
		/// Gets or sets the top expression of the rectangle.
		/// </summary>
		public RDExpression? Top { get; set; } = top;
		/// <summary>
		/// Gets or sets the bottom expression of the rectangle.
		/// </summary>
		public RDExpression? Bottom { get; set; } = bottom;
		/// <summary>
		/// Gets the left-bottom point of the rectangle.
		/// </summary>
		public readonly RDPointE LeftBottom => new(Left, Bottom);
		/// <summary>
		/// Gets the right-bottom point of the rectangle.
		/// </summary>
		public readonly RDPointE RightBottom => new(Right, Bottom);
		/// <summary>
		/// Gets the left-top point of the rectangle.
		/// </summary>
		public readonly RDPointE LeftTop => new(Left, Top);
		/// <summary>
		/// Gets the right-top point of the rectangle.
		/// </summary>
		public readonly RDPointE RightTop => new(Right, Top);
		/// <summary>
		/// Gets the width of the rectangle.
		/// </summary>
		public readonly RDExpression? Width => Right - Left;
		/// <summary>
		/// Gets the height of the rectangle.
		/// </summary>
		public readonly RDExpression? Height => Top - Bottom;
		/// <summary>
		/// Initializes a new instance of the <see cref="RDRectE"/> struct with the specified location and size.
		/// </summary>
		/// <param name="location">The location of the rectangle.</param>
		/// <param name="size">The size of the rectangle.</param>
		public RDRectE(RDPointE? location, RDSizeE? size) : this(location?.X, location?.Y + size?.Height, location?.X + size?.Width, location?.Y) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="RDRectE"/> struct with the specified size.
		/// </summary>
		/// <param name="size">The size of the rectangle.</param>
		public RDRectE(RDSizeE size) : this(new RDExpression?(0f), size.Height, size.Width, new RDExpression?(0f)) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="RDRectE"/> struct with the specified width and height.
		/// </summary>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		public RDRectE(RDExpression? width, RDExpression? height) : this(new RDExpression?(0f), height, width, new RDExpression?(0f)) { }
		/// <summary>
		/// Gets the location of the rectangle.
		/// </summary>
		public readonly RDPointE Location => new(Left, Bottom);
		/// <summary>
		/// Gets the size of the rectangle.
		/// </summary>
		public readonly RDSizeE Size => new(Width, Height);
		/// <summary>
		/// Inflates the specified rectangle by the specified size.
		/// </summary>
		/// <param name="rect">The rectangle to inflate.</param>
		/// <param name="size">The size to inflate by.</param>
		/// <returns>The inflated rectangle.</returns>
		public static RDRectE Inflate(RDRectE rect, RDSizeE size)
		{
			RDRectE result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(size);
			return result;
		}
		/// <summary>
		/// Inflates the specified rectangle by the specified width and height.
		/// </summary>
		/// <param name="rect">The rectangle to inflate.</param>
		/// <param name="x">The width to inflate by.</param>
		/// <param name="y">The height to inflate by.</param>
		/// <returns>The inflated rectangle.</returns>
		public static RDRectE Inflate(RDRectE rect, RDExpression? x, RDExpression? y)
		{
			RDRectE result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(x, y);
			return result;
		}
		/// <summary>
		/// Truncates the specified rectangle.
		/// </summary>
		/// <param name="rect">The rectangle to truncate.</param>
		/// <returns>The truncated rectangle.</returns>
		public static RDRectE Truncate(RDRectE rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
		/// <summary>
		/// Offsets the rectangle by the specified width and height.
		/// </summary>
		/// <param name="x">The width to offset by.</param>
		/// <param name="y">The height to offset by.</param>
		public void Offset(RDExpression? x, RDExpression? y)
		{
			Left += x;
			Top += y;
			Right += x;
			Bottom += y;
		}
		/// <summary>
		/// Offsets the rectangle by the specified point.
		/// </summary>
		/// <param name="p">The point to offset by.</param>
		public void Offset(RDPointE p) => Offset(p.X, p.Y);
		/// <summary>
		/// Inflates the rectangle by the specified size.
		/// </summary>
		/// <param name="size">The size to inflate by.</param>
		public void Inflate(RDSizeE size)
		{
			Left -= size.Width;
			Top += size.Height;
			Right += size.Width;
			Bottom -= size.Height;
		}
		/// <summary>
		/// Inflates the rectangle by the specified width and height.
		/// </summary>
		/// <param name="width">The width to inflate by.</param>
		/// <param name="height">The height to inflate by.</param>
		public void Inflate(RDExpression? width, RDExpression? height)
		{
			Left -= width;
			Top += height;
			Right += width;
			Bottom -= height;
		}
		/// <inheritdoc/>
		public static bool operator ==(RDRectE rect1, RDRectE rect2) => rect1.Equals(rect2);
		/// <inheritdoc/>
		public static bool operator !=(RDRectE rect1, RDRectE rect2) => !rect1.Equals(rect2);
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly bool Equals(object? obj) => obj is RDRectE e && Equals(e);
#else
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDRectE e && Equals(e);
#endif
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly int GetHashCode()
		{
			int hash = 17;
			hash = hash * 31 + (Left?.GetHashCode() ?? 0);
			hash = hash * 31 + (Top?.GetHashCode() ?? 0);
			hash = hash * 31 + (Right?.GetHashCode() ?? 0);
			hash = hash * 31 + (Bottom?.GetHashCode() ?? 0);
			return hash;
		}
#else
		public override readonly int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);
#endif
		/// <inheritdoc/>
		public override readonly string ToString() => $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}";
		/// <inheritdoc/>
		public readonly bool Equals(RDRectE other) => Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;
	}
}
