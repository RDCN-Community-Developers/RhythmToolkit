using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct RDRectNI(int left, int top, int right, int bottom) : IEquatable<RDRectNI>
	{
		public int Left { get; set; } = left;
		public int Right { get; set; } = right;
		public int Top { get; set; } = top;
		public int Bottom { get; set; } = bottom;
		public readonly RDPointNI LeftBottom => new(Left, Bottom);
		public readonly RDPointNI RightBottom => new(Right, Bottom);
		public readonly RDPointNI LeftTop => new(Left, Top);
		public readonly RDPointNI RightTop => new(Right, Top);
		public readonly int Width => Right - Left;
		public readonly int Height => Top - Bottom;
		public RDRectNI(RDPointNI location, RDSizeNI size) : this(location.X, location.Y + size.Height, location.X + size.Width, location.Y) { }
		public RDRectNI(RDSizeNI size) : this(0, size.Height, size.Width, 0) { }
		public RDRectNI(int width, int height) : this(0, height, width, 0) { }
		public readonly RDPointNI Location => new(Left, Bottom);
		public readonly RDSizeNI Size => new(Width, Height);
		public static RDRectNI Inflate(RDRectNI rect, RDSizeNI size)
		{
			RDRectNI result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(size);
			return result;
		}
		public static RDRectNI Inflate(RDRectNI rect, int x, int y)
		{
			RDRectNI result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(x, y);
			return result;
		}
		public static RDRectNI Ceiling(RDRectN rect) => Ceiling(rect, false);
		public static RDRectNI Ceiling(RDRectN rect, bool outwards) => new(
				(int)Math.Round((outwards && rect.Width > 0f) ? Math.Floor((double)rect.Left) : Math.Ceiling((double)rect.Left)),
				(int)Math.Round((outwards && rect.Height > 0f) ? Math.Floor((double)rect.Top) : Math.Ceiling((double)rect.Top)),
				(int)Math.Round((outwards && rect.Width < 0f) ? Math.Floor((double)rect.Right) : Math.Ceiling((double)rect.Right)),
				(int)Math.Round((outwards && rect.Height < 0f) ? Math.Floor((double)rect.Bottom) : Math.Ceiling((double)rect.Bottom)));
		public static RDRectNI Floor(RDRectN rect) => Ceiling(rect, false);
		public static RDRectNI Floor(RDRectN rect, bool inwards) => new(
				(int)Math.Round((inwards && rect.Width > 0f) ? Math.Ceiling((double)rect.Left) : Math.Floor((double)rect.Left)),
				(int)Math.Round((inwards && rect.Height > 0f) ? Math.Ceiling((double)rect.Top) : Math.Floor((double)rect.Top)),
				(int)Math.Round((inwards && rect.Width < 0f) ? Math.Ceiling((double)rect.Right) : Math.Floor((double)rect.Right)),
				(int)Math.Round((inwards && rect.Height < 0f) ? Math.Ceiling((double)rect.Bottom) : Math.Floor((double)rect.Bottom)));
		public static RDRectNI Round(RDRectN rect) => new(
			(int)Math.Round((double)rect.Left),
			(int)Math.Round((double)rect.Top),
			(int)Math.Round((double)rect.Right),
			(int)Math.Round((double)rect.Bottom));
		public static RDRectNI Union(RDRectNI rect1, RDRectNI rect2) => new(
			Math.Min(rect1.Left, rect2.Left),
			Math.Max(rect1.Top, rect2.Top),
			Math.Max(rect1.Right, rect2.Right),
			Math.Min(rect1.Bottom, rect2.Bottom));
		public static RDRectNI Intersect(RDRectNI rect1, RDRectNI rect2) => rect1.IntersectsWithInclusive(rect2) ? new RDRectNI(
			Math.Max(rect1.Left, rect2.Left),
			Math.Max(rect1.Top, rect2.Top),
			Math.Min(rect1.Right, rect2.Right),
			Math.Min(rect1.Bottom, rect2.Bottom)) : default;
		public static RDRectNI Truncate(RDRectN rect) => new RDRectNI(
				(int)Math.Round((double)rect.Left),
				(int)Math.Round((double)rect.Top),
				(int)Math.Round((double)rect.Right),
				(int)Math.Round((double)rect.Bottom));
		public void Offset(int x, int y)
		{
			Left += x;
			Top += y;
			Right += x;
			Bottom += y;
		}
		public void Offset(RDPointNI p) => Offset(p.X, p.Y);
		public void Inflate(RDSizeNI size)
		{
			Left -= size.Width;
			Top += size.Height;
			Right += size.Width;
			Bottom -= size.Height;
		}

		public void Inflate(int width, int height)
		{
			Left -= width;
			Top += height;
			Right += width;
			Bottom -= height;
		}
		public readonly bool Contains(int x, int y) => Left < x && x < Right && Bottom < y && y < Top;
		public readonly bool Contains(RDPointN p) => (float)Left < p.X && p.X < (float)Right && (float)Bottom < p.Y && p.Y < (float)Top;
		public readonly bool Contains(RDRectNI rect) => Left < rect.Left && rect.Right < Right && Bottom < rect.Bottom && rect.Top < Top;
		public readonly RDRectNI Union(RDRectNI rect) => Union(this, rect);
		public readonly object Intersect(RDRectNI rect) => Intersect(this, rect);
		public readonly bool IntersectsWith(RDRectNI rect) => Left < rect.Right && Right > rect.Left && Top < rect.Bottom && Bottom > rect.Top;
		public readonly bool IntersectsWithInclusive(RDRectNI rect) => Left <= rect.Right && Right >= rect.Left && Top <= rect.Bottom && Bottom >= rect.Top;
		public static bool operator ==(RDRectNI rect1, RDRectNI rect2) => rect1.Equals(rect2);
		public static bool operator !=(RDRectNI rect1, RDRectNI rect2) => !rect1.Equals(rect2);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRectNI) && Equals((obj != null) ? ((RDRectNI)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);
		public override string ToString() => $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}";
		public readonly bool Equals(RDRectNI other) => Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;

		public static implicit operator RDRectN(RDRectNI rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);

		public static implicit operator RDRectI(RDRectNI rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);

		public static implicit operator RDRectE(RDRectNI rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
	}
}
