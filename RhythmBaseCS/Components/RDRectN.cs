using System;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct RDRectN : IEquatable<RDRectN>
	{
		public float Left { get; set; }

		public float Right { get; set; }

		public float Top { get; set; }

		public float Bottom { get; set; }

		public float Width
		{
			get
			{
				return Right - Left;
			}
		}

		public float Height
		{
			get
			{
				return Top - Bottom;
			}
		}

		public RDRectN(float left, float top, float right, float bottom)
		{
			this = default;
			Left = left;
			Right = right;
			Top = top;
			Bottom = bottom;
		}

		public RDRectN(RDPointN location, RDSizeN size)
		{
			this = new RDRectN(location.X, location.Y + size.Height, location.X + size.Width, location.Y);
		}

		public RDRectN(RDSizeN size)
		{
			this = new RDRectN(0f, size.Height, size.Width, 0f);
		}

		public RDRectN(float width, float height)
		{
			this = new RDRectN(0f, height, width, 0f);
		}

		public RDPointNI Location
		{
			get
			{
				RDPointNI Location = checked(new RDPointNI((int)Math.Round((double)Left), (int)Math.Round((double)Bottom)));
				return Location;
			}
		}

		public RDSizeNI Size
		{
			get
			{
				RDSizeNI Size = checked(new RDSizeNI((int)Math.Round((double)Width), (int)Math.Round((double)Height)));
				return Size;
			}
		}

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

		public static RDRectN Union(RDRectN rect1, RDRectN rect2)
		{
			RDRectN Union = new(Math.Min(rect1.Left, rect2.Left), Math.Max(rect1.Top, rect2.Top), Math.Max(rect1.Right, rect2.Right), Math.Min(rect1.Bottom, rect2.Bottom));
			return Union;
		}

		public static RDRectN Intersect(RDRectN rect1, RDRectN rect2) => rect1.IntersectsWithInclusive(rect2) ? new RDRectN(Math.Max(rect1.Left, rect2.Left), Math.Max(rect1.Top, rect2.Top), Math.Min(rect1.Right, rect2.Right), Math.Min(rect1.Bottom, rect2.Bottom)) : default;

		public static RDRectN Truncate(RDRectN rect)
		{
			RDRectN Truncate = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			return Truncate;
		}

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

		public RDRectN Union(RDRectN rect) => Union(this, rect);

		public bool IntersectsWithInclusive(RDRectN rect) => Left <= rect.Right && Right >= rect.Left && Top <= rect.Bottom && Bottom >= rect.Top;

		public static bool operator ==(RDRectN rect1, RDRectN rect2) => rect1.Equals(rect2);

		public static bool operator !=(RDRectN rect1, RDRectN rect2) => !rect1.Equals(rect2);

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRectN) && Equals((obj != null) ? ((RDRectN)obj) : default);

		public override int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);

		public override string ToString() => string.Format("{{Location=[{0},{1}],Size=[{2},{3}]}}",
			[
				Left,
				Bottom,
				Width,
				Height
			]);

		public bool Equals(RDRectN other) => Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;

		public static implicit operator RDRect(RDRectN rect)
		{
			RDRect result = new(new float?(rect.Left), new float?(rect.Top), new float?(rect.Right), new float?(rect.Bottom));
			return result;
		}

		public static implicit operator RDRectE(RDRectN rect)
		{
			RDRectE result = new(new Expression?(rect.Left), new Expression?(rect.Top), new Expression?(rect.Right), new Expression?(rect.Bottom));
			return result;
		}
	}
}
