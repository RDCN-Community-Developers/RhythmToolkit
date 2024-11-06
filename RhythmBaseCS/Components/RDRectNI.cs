using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// Represents a rectangle defined by its left, top, right, and bottom coordinates.
	/// </summary>
	/// <param name="left">The left coordinate of the rectangle.</param>
	/// <param name="top">The top coordinate of the rectangle.</param>
	/// <param name="right">The right coordinate of the rectangle.</param>
	/// <param name="bottom">The bottom coordinate of the rectangle.</param>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDRectNI(int left, int top, int right, int bottom) : IEquatable<RDRectNI>
	{
		/// <summary>
		/// Gets or sets the left coordinate of the rectangle.
		/// </summary>
		public int Left { get; set; } = left;

		/// <summary>
		/// Gets or sets the right coordinate of the rectangle.
		/// </summary>
		public int Right { get; set; } = right;

		/// <summary>
		/// Gets or sets the top coordinate of the rectangle.
		/// </summary>
		public int Top { get; set; } = top;

		/// <summary>
		/// Gets or sets the bottom coordinate of the rectangle.
		/// </summary>
		public int Bottom { get; set; } = bottom;

		/// <summary>
		/// Gets the bottom-left point of the rectangle.
		/// </summary>
		public readonly RDPointNI LeftBottom => new(Left, Bottom);

		/// <summary>
		/// Gets the bottom-right point of the rectangle.
		/// </summary>
		public readonly RDPointNI RightBottom => new(Right, Bottom);

		/// <summary>
		/// Gets the top-left point of the rectangle.
		/// </summary>
		public readonly RDPointNI LeftTop => new(Left, Top);

		/// <summary>
		/// Gets the top-right point of the rectangle.
		/// </summary>
		public readonly RDPointNI RightTop => new(Right, Top);

		/// <summary>
		/// Gets the width of the rectangle.
		/// </summary>
		public readonly int Width => Right - Left;

		/// <summary>
		/// Gets the height of the rectangle.
		/// </summary>
		public readonly int Height => Top - Bottom;

		/// <summary>
		/// Initializes a new instance of the <see cref="RDRectNI"/> struct with the specified location and size.
		/// </summary>
		/// <param name="location">The location of the rectangle.</param>
		/// <param name="size">The size of the rectangle.</param>
		public RDRectNI(RDPointNI location, RDSizeNI size) : this(location.X, location.Y + size.Height, location.X + size.Width, location.Y) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDRectNI"/> struct with the specified size.
		/// </summary>
		/// <param name="size">The size of the rectangle.</param>
		public RDRectNI(RDSizeNI size) : this(0, size.Height, size.Width, 0) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDRectNI"/> struct with the specified width and height.
		/// </summary>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		public RDRectNI(int width, int height) : this(0, height, width, 0) { }

		/// <summary>
		/// Gets the location of the rectangle.
		/// </summary>
		public readonly RDPointNI Location => new(Left, Bottom);

		/// <summary>
		/// Gets the size of the rectangle.
		/// </summary>
		public readonly RDSizeNI Size => new(Width, Height);

		/// <summary>
		/// Inflates the specified rectangle by the specified size.
		/// </summary>
		/// <param name="rect">The rectangle to inflate.</param>
		/// <param name="size">The size to inflate by.</param>
		/// <returns>The inflated rectangle.</returns>
		public static RDRectNI Inflate(RDRectNI rect, RDSizeNI size)
		{
			RDRectNI result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(size);
			return result;
		}

		/// <summary>
		/// Inflates the specified rectangle by the specified amounts.
		/// </summary>
		/// <param name="rect">The rectangle to inflate.</param>
		/// <param name="x">The amount to inflate the width by.</param>
		/// <param name="y">The amount to inflate the height by.</param>
		/// <returns>The inflated rectangle.</returns>
		public static RDRectNI Inflate(RDRectNI rect, int x, int y)
		{
			RDRectNI result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(x, y);
			return result;
		}
		/// <summary>
		/// Returns a rectangle structure that represents the smallest possible rectangle that can contain the specified rectangle, with each value rounded up to the nearest integer.
		/// </summary>
		/// <param name="rect">The rectangle to be rounded up.</param>
		/// <returns>A new rectangle structure with each value rounded up to the nearest integer.</returns>
		public static RDRectNI Ceiling(RDRectN rect) => Ceiling(rect, false);

		/// <summary>
		/// Returns a rectangle structure that represents the smallest possible rectangle that can contain the specified rectangle, with each value rounded up or down to the nearest integer.
		/// </summary>
		/// <param name="rect">The rectangle to be rounded.</param>
		/// <param name="outwards">If true, values are rounded outwards; otherwise, they are rounded inwards.</param>
		/// <returns>A new rectangle structure with each value rounded to the nearest integer.</returns>
		public static RDRectNI Ceiling(RDRectN rect, bool outwards) => new(
		(int)Math.Round((outwards && rect.Width > 0f) ? Math.Floor((double)rect.Left) : Math.Ceiling((double)rect.Left)),
		(int)Math.Round((outwards && rect.Height > 0f) ? Math.Floor((double)rect.Top) : Math.Ceiling((double)rect.Top)),
		(int)Math.Round((outwards && rect.Width < 0f) ? Math.Floor((double)rect.Right) : Math.Ceiling((double)rect.Right)),
		(int)Math.Round((outwards && rect.Height < 0f) ? Math.Floor((double)rect.Bottom) : Math.Ceiling((double)rect.Bottom)));

		/// <summary>
		/// Returns a rectangle structure that represents the largest possible rectangle that can fit within the specified rectangle, with each value rounded down to the nearest integer.
		/// </summary>
		/// <param name="rect">The rectangle to be rounded down.</param>
		/// <returns>A new rectangle structure with each value rounded down to the nearest integer.</returns>
		public static RDRectNI Floor(RDRectN rect) => Ceiling(rect, false);

		/// <summary>
		/// Returns a rectangle structure that represents the largest possible rectangle that can fit within the specified rectangle, with each value rounded up or down to the nearest integer.
		/// </summary>
		/// <param name="rect">The rectangle to be rounded.</param>
		/// <param name="inwards">If true, values are rounded inwards; otherwise, they are rounded outwards.</param>
		/// <returns>A new rectangle structure with each value rounded to the nearest integer.</returns>
		public static RDRectNI Floor(RDRectN rect, bool inwards) => new(
		(int)Math.Round((inwards && rect.Width > 0f) ? Math.Ceiling((double)rect.Left) : Math.Floor((double)rect.Left)),
		(int)Math.Round((inwards && rect.Height > 0f) ? Math.Ceiling((double)rect.Top) : Math.Floor((double)rect.Top)),
		(int)Math.Round((inwards && rect.Width < 0f) ? Math.Ceiling((double)rect.Right) : Math.Floor((double)rect.Right)),
		(int)Math.Round((inwards && rect.Height < 0f) ? Math.Ceiling((double)rect.Bottom) : Math.Floor((double)rect.Bottom)));

		/// <summary>
		/// Returns a rectangle structure that represents the specified rectangle, with each value rounded to the nearest integer.
		/// </summary>
		/// <param name="rect">The rectangle to be rounded.</param>
		/// <returns>A new rectangle structure with each value rounded to the nearest integer.</returns>
		public static RDRectNI Round(RDRectN rect) => new(
		(int)Math.Round((double)rect.Left),
		(int)Math.Round((double)rect.Top),
		(int)Math.Round((double)rect.Right),
		(int)Math.Round((double)rect.Bottom));

		/// <summary>
		/// Returns a rectangle structure that represents the union of two rectangles. The union is the smallest rectangle that contains both rectangles.
		/// </summary>
		/// <param name="rect1">The first rectangle.</param>
		/// <param name="rect2">The second rectangle.</param>
		/// <returns>A new rectangle structure that represents the union of the two rectangles.</returns>
		public static RDRectNI Union(RDRectNI rect1, RDRectNI rect2) => new(
		Math.Min(rect1.Left, rect2.Left),
		Math.Max(rect1.Top, rect2.Top),
		Math.Max(rect1.Right, rect2.Right),
		Math.Min(rect1.Bottom, rect2.Bottom));

		/// <summary>
		/// Returns a rectangle structure that represents the intersection of two rectangles. The intersection is the largest rectangle that is contained within both rectangles.
		/// </summary>
		/// <param name="rect1">The first rectangle.</param>
		/// <param name="rect2">The second rectangle.</param>
		/// <returns>A new rectangle structure that represents the intersection of the two rectangles, or the default rectangle if there is no intersection.</returns>
		public static RDRectNI Intersect(RDRectNI rect1, RDRectNI rect2) => rect1.IntersectsWithInclusive(rect2) ? new RDRectNI(
		Math.Max(rect1.Left, rect2.Left),
		Math.Max(rect1.Top, rect2.Top),
		Math.Min(rect1.Right, rect2.Right),
		Math.Min(rect1.Bottom, rect2.Bottom)) : default;

		/// <summary>
		/// Returns a rectangle structure that represents the specified rectangle, with each value truncated to the nearest integer.
		/// </summary>
		/// <param name="rect">The rectangle to be truncated.</param>
		/// <returns>A new rectangle structure with each value truncated to the nearest integer.</returns>
		public static RDRectNI Truncate(RDRectN rect) => new(
		(int)Math.Round((double)rect.Left),
		(int)Math.Round((double)rect.Top),
		(int)Math.Round((double)rect.Right),
		(int)Math.Round((double)rect.Bottom));

		/// <summary>
		/// Moves the rectangle by the specified horizontal and vertical amounts.
		/// </summary>
		/// <param name="x">The amount to move the rectangle horizontally.</param>
		/// <param name="y">The amount to move the rectangle vertically.</param>
		public void Offset(int x, int y)
		{
			Left += x;
			Top += y;
			Right += x;
			Bottom += y;
		}

		/// <summary>
		/// Moves the rectangle by the specified point.
		/// </summary>
		/// <param name="p">The point to move the rectangle by.</param>
		public void Offset(RDPointNI p) => Offset(p.X, p.Y);

		/// <summary>
		/// Inflates the rectangle by the specified size.
		/// </summary>
		/// <param name="size">The size to inflate the rectangle by.</param>
		public void Inflate(RDSizeNI size)
		{
			Left -= size.Width;
			Top += size.Height;
			Right += size.Width;
			Bottom -= size.Height;
		}

		/// <summary>
		/// Inflates the rectangle by the specified width and height.
		/// </summary>
		/// <param name="width">The amount to inflate the rectangle's width by.</param>
		/// <param name="height">The amount to inflate the rectangle's height by.</param>
		public void Inflate(int width, int height)
		{
			Left -= width;
			Top += height;
			Right += width;
			Bottom -= height;
		}

		/// <summary>
		/// Determines whether the rectangle contains the specified point.
		/// </summary>
		/// <param name="x">The x-coordinate of the point.</param>
		/// <param name="y">The y-coordinate of the point.</param>
		/// <returns>True if the rectangle contains the point; otherwise, false.</returns>
		public readonly bool Contains(int x, int y) => Left < x && x < Right && Bottom < y && y < Top;

		/// <summary>
		/// Determines whether the rectangle contains the specified point.
		/// </summary>
		/// <param name="p">The point to check.</param>
		/// <returns>True if the rectangle contains the point; otherwise, false.</returns>
		public readonly bool Contains(RDPointN p) => (float)Left < p.X && p.X < (float)Right && (float)Bottom < p.Y && p.Y < (float)Top;

		/// <summary>
		/// Determines whether the rectangle contains the specified rectangle.
		/// </summary>
		/// <param name="rect">The rectangle to check.</param>
		/// <returns>True if the rectangle contains the specified rectangle; otherwise, false.</returns>
		public readonly bool Contains(RDRectNI rect) => Left < rect.Left && rect.Right < Right && Bottom < rect.Bottom && rect.Top < Top;

		/// <summary>
		/// Returns a rectangle structure that represents the union of this rectangle and the specified rectangle.
		/// </summary>
		/// <param name="rect">The rectangle to union with.</param>
		/// <returns>A new rectangle structure that represents the union of the two rectangles.</returns>
		public readonly RDRectNI Union(RDRectNI rect) => Union(this, rect);

		/// <summary>
		/// Returns a rectangle structure that represents the intersection of this rectangle and the specified rectangle.
		/// </summary>
		/// <param name="rect">The rectangle to intersect with.</param>
		/// <returns>A new rectangle structure that represents the intersection of the two rectangles, or the default rectangle if there is no intersection.</returns>
		public readonly object Intersect(RDRectNI rect) => Intersect(this, rect);

		/// <summary>
		/// Determines whether this rectangle intersects with the specified rectangle.
		/// </summary>
		/// <param name="rect">The rectangle to check for intersection.</param>
		/// <returns>True if the rectangles intersect; otherwise, false.</returns>
		public readonly bool IntersectsWith(RDRectNI rect) => Left < rect.Right && Right > rect.Left && Top < rect.Bottom && Bottom > rect.Top;

		/// <summary>
		/// Determines whether this rectangle intersects with the specified rectangle, including the edges.
		/// </summary>
		/// <param name="rect">The rectangle to check for intersection.</param>
		/// <returns>True if the rectangles intersect, including the edges; otherwise, false.</returns>
		public readonly bool IntersectsWithInclusive(RDRectNI rect) => Left <= rect.Right && Right >= rect.Left && Top <= rect.Bottom && Bottom >= rect.Top;
		/// <inheritdoc/>
		public static bool operator ==(RDRectNI rect1, RDRectNI rect2) => rect1.Equals(rect2);
		/// <inheritdoc/>
		public static bool operator !=(RDRectNI rect1, RDRectNI rect2) => !rect1.Equals(rect2);
		/// <inheritdoc/>
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDRectNI e && Equals(e);
		/// <inheritdoc/>
		public override readonly int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);
		/// <inheritdoc/>
		public override readonly string ToString() => $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}";
		/// <inheritdoc/>
		public readonly bool Equals(RDRectNI other) => Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;

		/// <summary>
		/// Implicitly converts an <see cref="RDRectNI"/> to an <see cref="RDRectN"/>.
		/// </summary>
		/// <param name="rect">The <see cref="RDRectNI"/> to convert.</param>
		/// <returns>An <see cref="RDRectN"/> that represents the same rectangle.</returns>
		public static implicit operator RDRectN(RDRectNI rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);

		/// <summary>
		/// Implicitly converts an <see cref="RDRectNI"/> to an <see cref="RDRectI"/>.
		/// </summary>
		/// <param name="rect">The <see cref="RDRectNI"/> to convert.</param>
		/// <returns>An <see cref="RDRectI"/> that represents the same rectangle.</returns>
		public static implicit operator RDRectI(RDRectNI rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);

		/// <summary>
		/// Implicitly converts an <see cref="RDRectNI"/> to an <see cref="RDRectE"/>.
		/// </summary>
		/// <param name="rect">The <see cref="RDRectNI"/> to convert.</param>
		/// <returns>An <see cref="RDRectE"/> that represents the same rectangle.</returns>
		public static implicit operator RDRectE(RDRectNI rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
