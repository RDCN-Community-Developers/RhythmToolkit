using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// Represents a rotated rectangle with non-integer coordinates.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDRotatedRectNI(RDPointNI location, RDSizeNI size, RDPointNI pivot, float angle) : IEquatable<RDRotatedRectNI>
	{
		/// <summary>
		/// Gets or sets the location of the rectangle.
		/// </summary>
		public RDPointNI Location { get; set; } = location;		/// <summary>
		/// Gets or sets the size of the rectangle.
		/// </summary>
		public RDSizeNI Size { get; set; } = size;		/// <summary>
		/// Gets or sets the pivot point of the rotation.
		/// </summary>
		public RDPointNI Pivot { get; set; } = pivot;		/// <summary>
		/// Gets or sets the angle of rotation in degrees.
		/// </summary>
		public float Angle { get; set; } = angle;		/// <summary>
		/// Gets the top-left point of the rotated rectangle.
		/// </summary>
		public readonly RDPointN LeftTop => (Location - (RDSizeNI)Pivot + new RDSizeNI(0, Size.Height)).Rotate(Location, Angle);		/// <summary>
		/// Gets the top-right point of the rotated rectangle.
		/// </summary>
		public readonly RDPointN RightTop => (Location - (RDSizeNI)Pivot + Size).Rotate(Location, Angle);		/// <summary>
		/// Gets the bottom-left point of the rotated rectangle.
		/// </summary>
		public readonly RDPointN LeftBottom => (Location - (RDSizeNI)Pivot).Rotate(Location, Angle);		/// <summary>
		/// Gets the bottom-right point of the rotated rectangle.
		/// </summary>
		public readonly RDPointN RightBottom => (Location - (RDSizeNI)Pivot + new RDSizeNI(Size.Width, 0)).Rotate(Location, Angle);		/// <summary>
		/// Gets the rectangle without rotation.
		/// </summary>
		public readonly RDRectNI WithoutRotate => new(Location - (RDSizeNI)Pivot, Size);		/// <summary>
		/// Initializes a new instance of the <see cref="RDRotatedRectNI"/> struct.
		/// </summary>
		/// <param name="rect">The rectangle.</param>
		/// <param name="pivot">The pivot point.</param>
		/// <param name="angle">The angle of rotation.</param>
		public RDRotatedRectNI(RDRectNI rect, RDPointNI pivot, float angle) : this(rect.Location, rect.Size, pivot, angle) { }		/// <summary>
		/// Initializes a new instance of the <see cref="RDRotatedRectNI"/> struct.
		/// </summary>
		/// <param name="rect">The rectangle.</param>
		public RDRotatedRectNI(RDRectNI rect) : this(rect.Location, rect.Size, default, 0f) { }		/// <summary>
		/// Inflates the specified rectangle by the specified size.
		/// </summary>
		/// <param name="rect">The rectangle to inflate.</param>
		/// <param name="size">The size to inflate by.</param>
		/// <returns>The inflated rectangle.</returns>
		public static RDRotatedRectNI Inflate(RDRotatedRectNI rect, RDSizeNI size)
		{
			RDRotatedRectNI result = rect;
			result.Inflate(size);
			return result;
		}		/// <summary>
		/// Inflates the specified rectangle by the specified width and height.
		/// </summary>
		/// <param name="rect">The rectangle to inflate.</param>
		/// <param name="x">The width to inflate by.</param>
		/// <param name="y">The height to inflate by.</param>
		/// <returns>The inflated rectangle.</returns>
		public static RDRotatedRectNI Inflate(RDRotatedRectNI rect, int x, int y)
		{
			RDRotatedRectNI result = rect;
			result.Inflate(x, y);
			return result;
		}		/// <summary>
		/// Offsets the rectangle by the specified x and y values.
		/// </summary>
		/// <param name="x">The x value to offset by.</param>
		/// <param name="y">The y value to offset by.</param>
		public void Offset(int x, int y) => Location += (RDSizeNI)new RDPointNI(x, y);		/// <summary>
		/// Offsets the rectangle by the specified point.
		/// </summary>
		/// <param name="p">The point to offset by.</param>
		public void Offset(RDPointNI p) => Offset(p.X, p.Y);		/// <summary>
		/// Inflates the rectangle by the specified size.
		/// </summary>
		/// <param name="size">The size to inflate by.</param>
		public void Inflate(RDSizeNI size)
		{
			Size += new RDSizeNI(size.Width * 2, size.Height * 2);
			Pivot -= (RDSizeNI)new RDPointNI(size.Width, size.Height);
		}		/// <summary>
		/// Inflates the rectangle by the specified width and height.
		/// </summary>
		/// <param name="width">The width to inflate by.</param>
		/// <param name="height">The height to inflate by.</param>
		public void Inflate(int width, int height)
		{
			Size += new RDSizeNI(width * 2, height * 2);
			Pivot -= (RDSizeNI)new RDPointNI(width, height);
		}		/// <summary>
		/// Determines whether the rectangle contains the specified point.
		/// </summary>
		/// <param name="x">The x coordinate of the point.</param>
		/// <param name="y">The y coordinate of the point.</param>
		/// <returns><c>true</c> if the rectangle contains the point; otherwise, <c>false</c>.</returns>
		public readonly bool Contains(int x, int y) => WithoutRotate.Contains(new RDPointN((float)x, (float)y).Rotate(-Angle));		/// <summary>
		/// Determines whether the rectangle contains the specified point.
		/// </summary>
		/// <param name="p">The point.</param>
		/// <returns><c>true</c> if the rectangle contains the point; otherwise, <c>false</c>.</returns>
		public readonly bool Contains(RDPointN p) => WithoutRotate.Contains(p.Rotate(-Angle));		/// <summary>
		/// Determines whether the rectangle contains the specified rotated rectangle.
		/// </summary>
		/// <param name="rect">The rotated rectangle.</param>
		/// <returns><c>true</c> if the rectangle contains the rotated rectangle; otherwise, <c>false</c>.</returns>
		public readonly bool Contains(RDRotatedRectNI rect) =>
			Contains(rect.LeftTop) &&
			Contains(rect.RightTop) &&
			Contains(rect.LeftBottom) &&
			Contains(rect.RightBottom);		/// <summary>
		/// Determines whether the rectangle intersects with the specified rotated rectangle.
		/// </summary>
		/// <param name="rect">The rotated rectangle.</param>
		/// <returns><c>true</c> if the rectangle intersects with the rotated rectangle; otherwise, <c>false</c>.</returns>
		public readonly bool IntersectsWith(RDRotatedRectNI rect) =>
			Contains(rect.LeftTop) ||
			Contains(rect.RightTop) ||
			Contains(rect.LeftBottom) ||
			Contains(rect.RightBottom);
		/// <inheritdoc/>
		public static bool operator ==(RDRotatedRectNI rect1, RDRotatedRectNI rect2) => rect1.Equals(rect2);
		/// <inheritdoc/>
		public static bool operator !=(RDRotatedRectNI rect1, RDRotatedRectNI rect2) => !rect1.Equals(rect2);
		/// <inheritdoc/>
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDRotatedRectNI e && Equals(e);
		/// <inheritdoc/>
		public override readonly int GetHashCode() => HashCode.Combine(Location, Size, Pivot, Angle);
		/// <inheritdoc/>
		public override readonly string ToString() => $"{{Location=[{Location}],Size=[{Size}],Pivot[{Pivot}],Angle=[{Angle}]}}";
		/// <inheritdoc/>
		public readonly bool Equals(RDRotatedRectNI other) =>
			Location == other.Location &&
			Size == other.Size && Pivot
			== other.Pivot &&
			Angle == other.Angle;
		private readonly string GetDebuggerDisplay() => ToString();
	}
}