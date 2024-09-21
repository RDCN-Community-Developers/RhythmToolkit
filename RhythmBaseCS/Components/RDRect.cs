using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct RDRect(float? left, float? top, float? right, float? bottom) : IEquatable<RDRect>
	{
		public float? Left { get; set; } = left;
		public float? Right { get; set; } = right;
		public float? Top { get; set; } = top;
		public float? Bottom { get; set; } = bottom;
		public readonly RDPoint LeftBottom { get => new(Left, Bottom); }
		public readonly RDPoint RightBottom { get => new(Right, Bottom); }
		public readonly RDPoint LeftTop { get => new(Left, Top); }
		public readonly RDPoint RightTop { get => new(Right, Top); }
		public readonly float? Width => Right - Left;
		public readonly float? Height => Top - Bottom;
		public RDRect(RDPoint location, RDSize size) : this(location.X, location.Y + size.Height, location.X + size.Width, location.Y) { }

		public RDRect(RDSize size) : this(new float?(0f), size.Height, size.Width, new float?(0f)) { }
		public RDRect(float? width, float? height) : this(new float?(0f), height, width, new float?(0f)) { }
		public readonly RDPoint Location => new(Left, Bottom);
		public RDSize Size => new(Width, Height);
		public static RDRect Inflate(RDRect rect, RDSize size)
		{
			RDRect result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(size);
			return result;
		}
		public static RDRect Inflate(RDRect rect, float? x, float? y)
		{
			RDRect result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(x, y);
			return result;
		}
		public static RDRect Union(RDRect rect1, RDRect rect2)
		{
			RDRect Union = new(new float?((rect1.Left == null || rect2.Left == null) ? 0f : Math.Min(rect1.Left.Value, rect2.Left.Value)), new float?((rect1.Top == null || rect2.Top == null) ? 0f : Math.Min(rect1.Top.Value, rect2.Top.Value)), new float?((rect1.Right == null || rect2.Right == null) ? 0f : Math.Min(rect1.Right.Value, rect2.Right.Value)), new float?((rect1.Bottom == null || rect2.Bottom == null) ? 0f : Math.Min(rect1.Bottom.Value, rect2.Bottom.Value)));
			return Union;
		}
		public static RDRect Intersect(RDRect rect1, RDRect rect2) => rect1.IntersectsWithInclusive(rect2) ? new RDRect(new float?((rect1.Left == null || rect2.Left == null) ? 0f : Math.Max(rect1.Left.Value, rect2.Left.Value)), new float?((rect1.Top == null || rect2.Top == null) ? 0f : Math.Max(rect1.Top.Value, rect2.Top.Value)), new float?((rect1.Right == null || rect2.Right == null) ? 0f : Math.Min(rect1.Right.Value, rect2.Right.Value)), new float?((rect1.Bottom == null || rect2.Bottom == null) ? 0f : Math.Min(rect1.Bottom.Value, rect2.Bottom.Value))) : default;
		public static RDRect Truncate(RDRect rect)
		{
			RDRect Truncate = new(
				rect.Left == null ? null : (float)Math.Truncate((double)rect.Left),
				rect.Top == null ? null : (float)Math.Truncate((double)rect.Top),
				rect.Right == null ? null : (float)Math.Truncate((double)rect.Right),
				rect.Bottom == null ? null : (float)Math.Truncate((double)rect.Bottom));
			return Truncate;
		}
		public void Offset(float? x, float? y)
		{
			Left += x;
			Top += y;
			Right += x;
			Bottom += y;
		}
		public void Offset(RDPoint p) => Offset(p.X, p.Y);
		public void Inflate(RDSize size)
		{
			Left -= size.Width;
			Top += size.Height;
			Right += size.Width;
			Bottom -= size.Height;
		}
		public void Inflate(float? width, float? height)
		{
			Left -= width;
			Top += height;
			Right += width;
			Bottom -= height;
		}
		public readonly bool Contains(float? x, float? y) => Left < x && x < Right && Bottom < y && y < Top;
		public readonly bool Contains(RDPoint p) => Contains(p.X, p.Y);
		public readonly bool Contains(RDRect rect) => Left < rect.Left && rect.Right < Right && Bottom < rect.Bottom && rect.Top < Top;
		public readonly RDRect Union(RDRect rect) => Union(this, rect);
		public readonly object Intersect(RDRect rect) => Intersect(this, rect);
		public readonly bool IntersectsWith(RDRect rect) => Left < rect.Right && Right > rect.Left && Top < rect.Bottom && Bottom > rect.Top;
		public readonly bool IntersectsWithInclusive(RDRect rect) => Left <= rect.Right && Right >= rect.Left && Top <= rect.Bottom && Bottom >= rect.Top;
		public static bool operator ==(RDRect rect1, RDRect rect2) => rect1.Equals(rect2);
		public static bool operator !=(RDRect rect1, RDRect rect2) => !rect1.Equals(rect2);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRect) && Equals((obj != null) ? ((RDRect)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);
		public override string ToString() => string.Format("{{Location=[{0},{1}],Size=[{2},{3}]}}",
			[
				Left,
				Bottom,
				Width,
				Height
			]);
		public readonly bool Equals(RDRect other) => Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;
		public static implicit operator RDRectE(RDRect rect) => new(rect.Left, rect.Top, rect.Right, rect.Bottom);
	}
}
