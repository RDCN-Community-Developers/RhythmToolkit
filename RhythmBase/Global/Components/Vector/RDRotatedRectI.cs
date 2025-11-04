using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Global.Components.Vector
{
	/// <summary>
	/// Represents a rotated rectangle with non-integer coordinates.
	/// </summary>
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDRotatedRectI(RDPointI? location, RDSizeI? size, RDPointI? pivot, float? angle = 0) : IEquatable<RDRotatedRectI>
	{
		/// <summary>
		/// Gets or sets the location of the rectangle.
		/// </summary>
		public RDPointI? Location { get; set; } = location;
		/// <summary>
		/// Gets or sets the size of the rectangle.
		/// </summary>
		public RDSizeI? Size { get; set; } = size;
		/// <summary>
		/// Gets or sets the pivot point of the rotation.
		/// </summary>
		public RDPointI? Pivot { get; set; } = pivot;
		/// <summary>
		/// Gets or sets the angle of rotation in degrees.
		/// </summary>
		public float? Angle { get; set; } = angle;
		/// <summary>
		/// Gets the rectangle without rotation.
		/// </summary>
		public readonly RDRectI? WithoutRotate => Location is null && Pivot is null && Size is null ? null : new(Location - (RDSizeI?)Pivot, Size);
		/// <summary>
		/// IItializes a new instance of the <see cref="RDRotatedRectI"/> struct.
		/// </summary>
		/// <param name="rect">The rectangle.</param>
		/// <param name="pivot">The pivot point.</param>
		/// <param name="angle">The angle of rotation.</param>
		public RDRotatedRectI(RDRectI? rect, RDPointI? pivot, float angle) : this(rect?.Location, rect?.Size, pivot, angle) { }
		/// <summary>
		/// IItializes a new instance of the <see cref="RDRotatedRectI"/> struct.
		/// </summary>
		/// <param name="rect">The rectangle.</param>
		public RDRotatedRectI(RDRectI? rect) : this(rect?.Location, rect?.Size, null, 0f) { }
		/// <summary>
		/// Inflates the specified rectangle by the specified size.
		/// </summary>
		/// <param name="rect">The rectangle to inflate.</param>
		/// <param name="size">The size to inflate by.</param>
		/// <returns>The inflated rectangle.</returns>
		public static RDRotatedRectI Inflate(RDRotatedRectI rect, RDSizeI size)
		{
			RDRotatedRectI result = rect;
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
		public static RDRotatedRectI Inflate(RDRotatedRectI rect, int x, int y)
		{
			RDRotatedRectI result = rect;
			result.Inflate(x, y);
			return result;
		}
		/// <summary>
		/// Offsets the rectangle by the specified x and y values.
		/// </summary>
		/// <param name="x">The x value to offset by.</param>
		/// <param name="y">The y value to offset by.</param>
		public void Offset(int? x, int? y) => Location += (RDSizeI)new RDPointI(x, y);
		/// <summary>
		/// Offsets the rectangle by the specified point.
		/// </summary>
		/// <param name="p">The point to offset by.</param>
		public void Offset(RDPointI p) => Offset(p.X, p.Y);
		/// <summary>
		/// Inflates the rectangle by the specified size.
		/// </summary>
		/// <param name="size">The size to inflate by.</param>
		public void Inflate(RDSizeI size)
		{
			Size += new RDSizeI(size.Width * 2, size.Height * 2);
			Pivot -= (RDSizeI)new RDPointI(size.Width, size.Height);
		}
		/// <summary>
		/// Inflates the rectangle by the specified width and height.
		/// </summary>
		/// <param name="width">The width to inflate by.</param>
		/// <param name="height">The height to inflate by.</param>
		public void Inflate(int width, int height)
		{
			Size += new RDSizeI(width * 2, height * 2);
			Pivot -= (RDSizeI)new RDPointI(width, height);
		}
		/// <inheritdoc/>
		public static bool operator ==(RDRotatedRectI rect1, RDRotatedRectI rect2) => rect1.Equals(rect2);
		/// <inheritdoc/>
		public static bool operator !=(RDRotatedRectI rect1, RDRotatedRectI rect2) => !rect1.Equals(rect2);
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly bool Equals(object? obj) => obj is RDRotatedRectI e && Equals(e);
#else
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDRotatedRectI e && Equals(e);
#endif
		/// <inheritdoc/>
#if NETSTANDARD
		public override readonly int GetHashCode()
		{
			int hash = 17;
			hash = hash * 31 + (Location?.GetHashCode() ?? 0);
			hash = hash * 31 + (Size?.GetHashCode() ?? 0);
			hash = hash * 31 + (Pivot?.GetHashCode() ?? 0);
			hash = hash * 31 + (Angle?.GetHashCode() ?? 0);
			return hash;
		}
#else
		public override readonly int GetHashCode() => HashCode.Combine(Location, Size, Pivot, Angle);
#endif
		/// <inheritdoc/>
		public override readonly string ToString() => $"{{Location=[{Location}],Size=[{Size}],Pivot[{Pivot}],Angle={Angle}}}";
		/// <inheritdoc/>
		public readonly bool Equals(RDRotatedRectI other) =>
			Location == other.Location &&
			Size == other.Size && Pivot
			== other.Pivot &&
			Angle == other.Angle;
		private readonly string GetDebuggerDisplay() => ToString();
	}
}