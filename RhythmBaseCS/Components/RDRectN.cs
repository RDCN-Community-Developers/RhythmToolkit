using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct RDRectN(float left, float top, float right, float bottom) : IEquatable<RDRectN>
	{
		public float Left { get; set; } = left;
		public float Right { get; set; } = top;
		public float Top { get; set; } = right;
		public float Bottom { get; set; } = bottom;
		public readonly RDPointN LeftBottom => new(Left, Bottom);
		public readonly RDPointN RightBottom => new(Right, Bottom);
		public readonly RDPointN LeftTop => new(Left, Top);
		public readonly RDPointN RightTop => new(Right, Top);
		public readonly float Width => Right - Left;
		public readonly float Height => Top - Bottom;
		public RDRectN(RDPointN location, RDSizeN size) : this(location.X, location.Y + size.Height, location.X + size.Width, location.Y) { }
		public RDRectN(RDSizeN size) : this(0f, size.Height, size.Width, 0f) { }
		public RDRectN(float width, float height) : this(0f, height, width, 0f) { }
		public readonly RDPointNI Location => new RDPointNI((int)Math.Round((double)Left), (int)Math.Round((double)Bottom));
		public readonly RDSizeNI Size => (new RDSizeNI((int)Math.Round((double)Width), (int)Math.Round((double)Height)));
		public static RDRectN Inflate(RDRectN rect, RDSizeNI size)
		{
			RDRectN result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(size);
			return result;
		}
		public static RDRectN Inflate(RDRectN rect, float x, float y)
		{
			RDRectN result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(x, y);
			return result;
		}
		public static RDRectN Union(RDRectN rect1, RDRectN rect2) => new(Math.Min(rect1.Left, rect2.Left), Math.Max(rect1.Top, rect2.Top), Math.Max(rect1.Right, rect2.Right), Math.Min(rect1.Bottom, rect2.Bottom));
		public static RDRectN Intersect(RDRectN rect1, RDRectN rect2) => rect1.IntersectsWithInclusive(rect2) ? new RDRectN(
			Math.Max(rect1.Left, rect2.Left),
			Math.Max(rect1.Top, rect2.Top),
			Math.Min(rect1.Right, rect2.Right),
			Math.Min(rect1.Bottom, rect2.Bottom)) : default;
		public static RDRectN Truncate(RDRectN rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
		public void Offset(float x, float y)
		{
			Left += x;
			Top += y;
			Right += x;
			Bottom += y;
		}
		public void Offset(RDPointN p) => Offset(p.X, p.Y);
		public void Inflate(RDSizeN size)
		{
			Left -= size.Width;
			Top += size.Height;
			Right += size.Width;
			Bottom -= size.Height;
		}
		public void Inflate(float width, float height)
		{
			Left -= width;
			Top += height;
			Right += width;
			Bottom -= height;
		}
		public readonly RDRectN Union(RDRectN rect) => Union(this, rect);
		public readonly bool IntersectsWithInclusive(RDRectN rect) => Left <= rect.Right && Right >= rect.Left && Top <= rect.Bottom && Bottom >= rect.Top;
		public static bool operator ==(RDRectN rect1, RDRectN rect2) => rect1.Equals(rect2);
		public static bool operator !=(RDRectN rect1, RDRectN rect2) => !rect1.Equals(rect2);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRectN) && Equals((obj != null) ? ((RDRectN)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);
		public override readonly string ToString() => $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}";
		public readonly bool Equals(RDRectN other) => Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;

		public static implicit operator RDRect(RDRectN rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);

		public static implicit operator RDRectE(RDRectN rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
	}
}
