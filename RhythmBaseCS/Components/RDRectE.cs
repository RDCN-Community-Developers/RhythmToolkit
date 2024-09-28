using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct RDRectE(RDExpression? left, RDExpression? top, RDExpression? right, RDExpression? bottom) : IEquatable<RDRectE>
	{
		public RDExpression? Left { get; set; } = left;
		public RDExpression? Right { get; set; } = right;
		public RDExpression? Top { get; set; } = top;
		public RDExpression? Bottom { get; set; } = bottom;
		public readonly RDPointE LeftBottom => new(Left, Bottom);
		public readonly RDPointE RightBottom => new(Right, Bottom);
		public readonly RDPointE LeftTop => new(Left, Top);
		public readonly RDPointE RightTop => new(Right, Top);
		public readonly RDExpression? Width => Right - Left;
		public readonly RDExpression? Height => Top - Bottom;
		public RDRectE(RDPointE location, RDSizeE size):this(location.X, location.Y + size.Height, location.X + size.Width, location.Y) { }
		public RDRectE(RDSizeE size):this(new RDExpression?(0f), size.Height, size.Width, new RDExpression?(0f)) { }
		public RDRectE(RDExpression? width, RDExpression? height) : this(new RDExpression?(0f), height, width, new RDExpression?(0f)) { }
		public readonly RDPointE Location => new(Left, Bottom);
		public RDSizeE Size => new(Width, Height);
		public static RDRectE Inflate(RDRectE rect, RDSizeE size)
		{
			RDRectE result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(size);
			return result;
		}
		public static RDRectE Inflate(RDRectE rect, RDExpression? x, RDExpression? y)
		{
			RDRectE result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(x, y);
			return result;
		}
		public static RDRectE Truncate(RDRectE rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
		public void Offset(RDExpression? x, RDExpression? y)
		{
			Left += x;
			Top += y;
			Right += x;
			Bottom += y;
		}
		public void Offset(RDPointE p) => Offset(p.X, p.Y);
		public void Inflate(RDSizeE size)
		{
			Left -= size.Width;
			Top += size.Height;
			Right += size.Width;
			Bottom -= size.Height;
		}
		public void Inflate(RDExpression? width, RDExpression? height)
		{
			Left -= width;
			Top += height;
			Right += width;
			Bottom -= height;
		}
		public static bool operator ==(RDRectE rect1, RDRectE rect2) => rect1.Equals(rect2);
		public static bool operator !=(RDRectE rect1, RDRectE rect2) => !rect1.Equals(rect2);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRectE) && Equals((obj != null) ? ((RDRectE)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);
		public override string ToString() => string.Format("{{Location=[{0},{1}],Size=[{2},{3}]}}",
			[
				Left,
				Bottom,
				Width,
				Height
			]);
		public readonly bool Equals(RDRectE other) => Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;
	}
}
