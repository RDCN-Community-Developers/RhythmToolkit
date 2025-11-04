using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace RhythmBase.Global.Components.Vector
{
	/// <summary>
	/// Represents a rotated rectangle with non-nullable float coordinates.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDRotatedRectN(RDPointN location, RDSizeN size, RDPointN pivot, float angle = 0) : IEquatable<RDRotatedRectN>
	{
		/// <summary>
		/// Gets or sets the location of the rectangle.
		/// </summary>
		public RDPointN Location { get; set; } = location;
		/// <summary>
		/// Gets or sets the size of the rectangle.
		/// </summary>
		public RDSizeN Size { get; set; } = size;
		/// <summary>
		/// Gets or sets the pivot point of the rotation.
		/// </summary>
		public RDPointN Pivot { get; set; } = pivot;
		/// <summary>
		/// Gets or sets the angle of rotation in radians.
		/// </summary>
		public float Angle { get; set; } = angle;
		/// <summary>
		/// Gets the top-left point of the rotated rectangle.
		/// </summary>
		public readonly RDPointN LeftTop => (Location - (RDSizeN)Pivot + new RDSizeN(0, Size.Height)).Rotate(Location, Angle);
		/// <summary>
		/// Gets the top-right point of the rotated rectangle.
		/// </summary>
		public readonly RDPointN RightTop => (Location - (RDSizeN)Pivot + Size).Rotate(Location, Angle);
		/// <summary>
		/// Gets the bottom-left point of the rotated rectangle.
		/// </summary>
		public readonly RDPointN LeftBottom => (Location - (RDSizeN)Pivot).Rotate(Location, Angle);
		/// <summary>
		/// Gets the bottom-right point of the rotated rectangle.
		/// </summary>
		public readonly RDPointN RightBottom => (Location - (RDSizeN)Pivot + new RDSizeN(Size.Width, 0)).Rotate(Location, Angle);
		/// <summary>
		/// Gets the rectangle without rotation.
		/// </summary>
		public readonly RDRectN WithoutRotate => new(Location - (RDSizeN)Pivot, Size);
		/// <summary>
		/// Initializes a new instance of the <see cref="RDRotatedRectN"/> struct.
		/// </summary>
		/// <param name="rect">The rectangle.</param>
		/// <param name="pivot">The pivot point.</param>
		/// <param name="angle">The angle of rotation.</param>
		public RDRotatedRectN(RDRectN rect, RDPointN pivot, float angle) : this(rect.Location, rect.Size, pivot, angle) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="RDRotatedRectN"/> struct.
		/// </summary>
		/// <param name="rect">The rectangle.</param>
		public RDRotatedRectN(RDRectN rect) : this(rect.Location, rect.Size, default, 0f) { }
		/// <summary>
		/// Inflates the specified rectangle by the specified size.
		/// </summary>
		/// <param name="rect">The rectangle to inflate.</param>
		/// <param name="size">The size to inflate by.</param>
		/// <returns>The inflated rectangle.</returns>
		public static RDRotatedRectN Inflate(RDRotatedRectN rect, RDSizeN size)
		{
			RDRotatedRectN result = rect;
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
		public static RDRotatedRectN Inflate(RDRotatedRectN rect, int x, int y)
		{
			RDRotatedRectN result = rect;
			result.Inflate(x, y);
			return result;
		}
		/// <summary>
		/// Offsets the rectangle by the specified x and y values.
		/// </summary>
		/// <param name="x">The x value to offset by.</param>
		/// <param name="y">The y value to offset by.</param>
		public void Offset(float x, float y) => Location += (RDSizeN)new RDPointN(x, y);
		/// <summary>
		/// Offsets the rectangle by the specified point.
		/// </summary>
		/// <param name="p">The point to offset by.</param>
		public void Offset(RDPointN p) => Offset(p.X, p.Y);
		/// <summary>
		/// Inflates the rectangle by the specified size.
		/// </summary>
		/// <param name="size">The size to inflate by.</param>
		public void Inflate(RDSizeN size)
		{
			Size += new RDSizeN(size.Width * 2, size.Height * 2);
			Pivot -= (RDSizeN)new RDPointN(size.Width, size.Height);
		}
		/// <summary>
		/// Inflates the rectangle by the specified width and height.
		/// </summary>
		/// <param name="width">The width to inflate by.</param>
		/// <param name="height">The height to inflate by.</param>
		public void Inflate(float width, float height)
		{
			Size += new RDSizeN(width * 2, height * 2);
			Pivot -= (RDSizeN)new RDPointN(width, height);
		}
		/// <summary>
		/// Determines whether the rectangle contains the specified point.
		/// </summary>
		/// <param name="x">The x coordinate of the point.</param>
		/// <param name="y">The y coordinate of the point.</param>
		/// <returns><c>true</c> if the rectangle contains the point; otherwise, <c>false</c>.</returns>
		public readonly bool Contains(float x, float y) => WithoutRotate.Contains(new RDPointN(x, y).Rotate(-Angle));
		/// <summary>
		/// Determines whether the rectangle contains the specified point.
		/// </summary>
		/// <param name="p">The point.</param>
		/// <returns><c>true</c> if the rectangle contains the point; otherwise, <c>false</c>.</returns>
		public readonly bool Contains(RDPointN p) => WithoutRotate.Contains(p.Rotate(-Angle));
		/// <summary>
		/// Determines whether the rectangle contains the specified rotated rectangle.
		/// </summary>
		/// <param name="rect">The rotated rectangle.</param>
		/// <returns><c>true</c> if the rectangle contains the rotated rectangle; otherwise, <c>false</c>.</returns>
		public readonly bool Contains(RDRotatedRectN rect) => Contains(rect.LeftTop) &&
			Contains(rect.RightTop) &&
			Contains(rect.LeftBottom) &&
			Contains(rect.RightBottom);
		/// <summary>
		/// Determines whether the rectangle intersects with the specified rotated rectangle.
		/// </summary>
		/// <param name="rect">The rotated rectangle.</param>
		/// <returns><c>true</c> if the rectangle intersects with the rotated rectangle; otherwise, <c>false</c>.</returns>
		public readonly bool IntersectsWith(RDRotatedRectN rect) => Contains(rect.LeftTop) ||
			Contains(rect.RightTop) ||
			Contains(rect.LeftBottom) ||
			Contains(rect.RightBottom);
		/// <summary>
		/// Determines whether two rotated rectangles are equal.
		/// </summary>
		/// <param name="rect1">The first rotated rectangle.</param>
		/// <param name="rect2">The second rotated rectangle.</param>
		/// <returns><c>true</c> if the rectangles are equal; otherwise, <c>false</c>.</returns>
		public static bool operator ==(RDRotatedRectN rect1, RDRotatedRectN rect2) => rect1.Equals(rect2);
		/// <summary>
		/// Determines whether two rotated rectangles are not equal.
		/// </summary>
		/// <param name="rect1">The first rotated rectangle.</param>
		/// <param name="rect2">The second rotated rectangle.</param>
		/// <returns><c>true</c> if the rectangles are not equal; otherwise, <c>false</c>.</returns>
		public static bool operator !=(RDRotatedRectN rect1, RDRotatedRectN rect2) => !rect1.Equals(rect2);
		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
#if NETSTANDARD
		public override readonly bool Equals(object? obj) => obj is RDRotatedRectN e && Equals(e);
#else
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDRotatedRectN e && Equals(e);
#endif
		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
#if NETSTANDARD
		public override readonly int GetHashCode()
		{
			int hash = 17;
			hash = hash * 31 + Location.GetHashCode();
			hash = hash * 31 + Size.GetHashCode();
			hash = hash * 31 + Pivot.GetHashCode() ;
			hash = hash * 31 + Angle.GetHashCode();
			return hash;
		}
#else
		public override readonly int GetHashCode() => HashCode.Combine(Location, Size, Pivot, Angle);
#endif
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override readonly string ToString() => $"{{Location=[{Location}],Size=[{Size}],Pivot[{Pivot}],Angle={Angle}}}";
		/// <summary>
		/// Determines whether the specified <see cref="RDRotatedRectN"/> is equal to the current <see cref="RDRotatedRectN"/>.
		/// </summary>
		/// <param name="other">The <see cref="RDRotatedRectN"/> to compare with the current <see cref="RDRotatedRectN"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="RDRotatedRectN"/> is equal to the current <see cref="RDRotatedRectN"/>; otherwise, <c>false</c>.</returns>
		public readonly bool Equals(RDRotatedRectN other) => Location == other.Location &&
			Size == other.Size && Pivot
			== other.Pivot &&
			Angle == other.Angle;
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
