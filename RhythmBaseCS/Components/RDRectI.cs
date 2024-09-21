using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct RDRectI(int? left, int? top, int? right, int? bottom) : IEquatable<RDRectI>
	{
		public int? Left { get; set; } = left;
		public int? Right { get; set; } = right;
		public int? Top { get; set; } = top;
		public int? Bottom { get; set; } = bottom;
		public readonly RDPointI LeftBottom { get => new(Left, Bottom); }
		public readonly RDPointI RightBottom { get => new(Right, Bottom); }
		public readonly RDPointI LeftTop { get => new(Left, Top); }
		public readonly RDPointI RightTop { get => new(Right, Top); }
		public readonly int? Width => checked(Right - Left);
		public readonly int? Height => checked(Top - Bottom);
		public RDRectI(RDPointI location, RDSizeI size) : this(location.X, location.Y + size.Height, location.X + size.Width, location.Y) { }
		public RDRectI(RDSizeI size) : this(new int?(0), size.Height, size.Width, new int?(0)) { }
		public RDRectI(int? width, int? height) : this(new int?(0), height, width, new int?(0)) { }
		public readonly RDPointI Location => new(Left, Bottom);
		public RDSizeI Size => new(Width, Height);
		public static RDRectI Inflate(RDRectI rect, RDSizeI size)
		{
			RDRectI result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(size);
			return result;
		}
		public static RDRectI Inflate(RDRectI rect, int? x, int? y)
		{
			RDRectI result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(x, y);
			return result;
		}
		public static RDRectI Ceiling(RDRect rect) => Ceiling(rect, false);
		public static RDRectI Ceiling(RDRect rect, bool outwards) => new(
				rect.Left == null ? null : (int)(outwards && rect.Width > 0 ? Math.Floor((double)rect.Left) : Math.Ceiling((double)rect.Left)),
				rect.Top == null ? null : (int)(outwards && rect.Height > 0 ? Math.Floor((double)rect.Top) : Math.Ceiling((double)rect.Top)),
				rect.Right == null ? null : (int)(outwards && rect.Width < 0 ? Math.Floor((double)rect.Right) : Math.Ceiling((double)rect.Right)),
				rect.Bottom == null ? null : (int)(outwards && rect.Height < 0 ? Math.Floor((double)rect.Bottom) : Math.Ceiling((double)rect.Bottom)));
		public static RDRectI Floor(RDRect rect) => Ceiling(rect, false);
		public static RDRectI Floor(RDRect rect, bool inwards) => new(
				rect.Left == null ? null : (int)(inwards && rect.Width > 0 ? Math.Ceiling((double)rect.Left) : Math.Floor((double)rect.Left)),
				rect.Top == null ? null : (int)(inwards && rect.Height > 0 ? Math.Ceiling((double)rect.Top) : Math.Floor((double)rect.Top)),
				rect.Right == null ? null : (int)(inwards && rect.Width < 0 ? Math.Ceiling((double)rect.Right) : Math.Floor((double)rect.Right)),
				rect.Bottom == null ? null : (int)(inwards && rect.Height < 0 ? Math.Ceiling((double)rect.Bottom) : Math.Floor((double)rect.Bottom)));
		public static RDRectI Round(RDRect rect) => new(
				new int?((int)Math.Round((rect.Left == null) ? 0.0 : Math.Round((double)rect.Left.Value))),
				new int?((int)Math.Round((rect.Top == null) ? 0.0 : Math.Round((double)rect.Top.Value))),
				new int?((int)Math.Round((rect.Right == null) ? 0.0 : Math.Round((double)rect.Right.Value))),
				new int?((int)Math.Round((rect.Bottom == null) ? 0.0 : Math.Round((double)rect.Bottom.Value))));
		public static RDRectI Union(RDRectI rect1, RDRectI rect2) => new(
				new int?((rect1.Left == null || rect2.Left == null) ? 0 : Math.Min(rect1.Left.Value, rect2.Left.Value)),
				new int?((rect1.Top == null || rect2.Top == null) ? 0 : Math.Min(rect1.Top.Value, rect2.Top.Value)),
				new int?((rect1.Right == null || rect2.Right == null) ? 0 : Math.Min(rect1.Right.Value, rect2.Right.Value)),
				new int?((rect1.Bottom == null || rect2.Bottom == null) ? 0 : Math.Min(rect1.Bottom.Value, rect2.Bottom.Value)));
		public static RDRectI Intersect(RDRectI rect1, RDRectI rect2) => rect1.IntersectsWithInclusive(rect2) ? new RDRectI(
			new int?((rect1.Left == null || rect2.Left == null) ? 0 : Math.Max(rect1.Left.Value, rect2.Left.Value)),
			new int?((rect1.Top == null || rect2.Top == null) ? 0 : Math.Max(rect1.Top.Value, rect2.Top.Value)),
			new int?((rect1.Right == null || rect2.Right == null) ? 0 : Math.Min(rect1.Right.Value, rect2.Right.Value)),
			new int?((rect1.Bottom == null || rect2.Bottom == null) ? 0 : Math.Min(rect1.Bottom.Value, rect2.Bottom.Value))) : default;
		public static RDRectI Truncate(RDRect rect) => new(
				(int?)rect.Left,
				(int?)rect.Top,
				(int?)rect.Right,
				(int?)rect.Bottom);
		public void Offset(int? x, int? y)
		{
			Left += x;
			Top += y;
			Right += x;
			Bottom += y;
		}
		public void Offset(RDPointI p) => Offset(p.X, p.Y);
		public void Inflate(RDSizeI size)
		{
			Left -= size.Width;
			Top += size.Height;
			Right += size.Width;
			Bottom -= size.Height;
		}
		public void Inflate(int? width, int? height)
		{
			Left -= width;
			Top += height;
			Right += width;
			Bottom -= height;
		}
		public readonly RDRectI Union(RDRectI rect) => Union(this, rect);
		public readonly bool IntersectsWithInclusive(RDRectI rect) => Left <= rect.Right && Right >= rect.Left && Top <= rect.Bottom && Bottom >= rect.Top;
		public static bool operator ==(RDRectI rect1, RDRectI rect2) => rect1.Equals(rect2);
		public static bool operator !=(RDRectI rect1, RDRectI rect2) => !rect1.Equals(rect2);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRectI) && Equals((obj != null) ? ((RDRectI)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);
		public override string ToString() => $"{{Location=[{Left},{Bottom}],Size=[{Width},{Height}]}}";
		public readonly bool Equals(RDRectI other) => Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;

		public static implicit operator RDRect(RDRectI rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);

		public static implicit operator RDRectE(RDRectI rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
	}
}
