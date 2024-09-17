using System;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct RDRectNI : IEquatable<RDRectNI>
	{
		public int Left { get; set; }

		public int Right { get; set; }

		public int Top { get; set; }

		public int Bottom { get; set; }

		public int Width
		{
			get
			{
				return checked(Right - Left);
			}
		}

		public int Height
		{
			get
			{
				return checked(Top - Bottom);
			}
		}

		public RDRectNI(int left, int top, int right, int bottom)
		{
			this = default;
			Left = left;
			Right = right;
			Top = top;
			Bottom = bottom;
		}

		public RDRectNI(RDPointNI location, RDSizeNI size)
		{
			this = checked(new RDRectNI(location.X, location.Y + size.Height, location.X + size.Width, location.Y));
		}

		public RDRectNI(RDSizeNI size)
		{
			this = new RDRectNI(0, size.Height, size.Width, 0);
		}

		public RDRectNI(int width, int height)
		{
			this = new RDRectNI(0, height, width, 0);
		}

		public RDPointNI Location
		{
			get
			{
				RDPointNI Location = new(Left, Bottom);
				return Location;
			}
		}

		public RDSizeNI Size
		{
			get
			{
				RDSizeNI Size = new(Width, Height);
				return Size;
			}
		}

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

		public static RDRectNI Ceiling(RDRectN rect, bool outwards)
		{
			RDRectNI Ceiling = checked(new RDRectNI((int)Math.Round((outwards && rect.Width > 0f) ? Math.Floor((double)rect.Left) : Math.Ceiling((double)rect.Left)), (int)Math.Round((outwards && rect.Height > 0f) ? Math.Floor((double)rect.Top) : Math.Ceiling((double)rect.Top)), (int)Math.Round((outwards && rect.Width < 0f) ? Math.Floor((double)rect.Right) : Math.Ceiling((double)rect.Right)), (int)Math.Round((outwards && rect.Height < 0f) ? Math.Floor((double)rect.Bottom) : Math.Ceiling((double)rect.Bottom))));
			return Ceiling;
		}

		public static RDRectNI Floor(RDRectN rect) => Ceiling(rect, false);

		public static RDRectNI Floor(RDRectN rect, bool inwards)
		{
			RDRectNI Floor = checked(new RDRectNI((int)Math.Round((inwards && rect.Width > 0f) ? Math.Ceiling((double)rect.Left) : Math.Floor((double)rect.Left)), (int)Math.Round((inwards && rect.Height > 0f) ? Math.Ceiling((double)rect.Top) : Math.Floor((double)rect.Top)), (int)Math.Round((inwards && rect.Width < 0f) ? Math.Ceiling((double)rect.Right) : Math.Floor((double)rect.Right)), (int)Math.Round((inwards && rect.Height < 0f) ? Math.Ceiling((double)rect.Bottom) : Math.Floor((double)rect.Bottom))));
			return Floor;
		}

		public static RDRectNI Round(RDRectN rect)
		{
			RDRectNI Round = checked(new RDRectNI((int)Math.Round((double)rect.Left), (int)Math.Round((double)rect.Top), (int)Math.Round((double)rect.Right), (int)Math.Round((double)rect.Bottom)));
			return Round;
		}

		public static RDRectNI Union(RDRectNI rect1, RDRectNI rect2)
		{
			RDRectNI Union = new(Math.Min(rect1.Left, rect2.Left), Math.Max(rect1.Top, rect2.Top), Math.Max(rect1.Right, rect2.Right), Math.Min(rect1.Bottom, rect2.Bottom));
			return Union;
		}

		public static RDRectNI Intersect(RDRectNI rect1, RDRectNI rect2) => rect1.IntersectsWithInclusive(rect2) ? new RDRectNI(Math.Max(rect1.Left, rect2.Left), Math.Max(rect1.Top, rect2.Top), Math.Min(rect1.Right, rect2.Right), Math.Min(rect1.Bottom, rect2.Bottom)) : default;

		public static RDRectNI Truncate(RDRectN rect)
		{
			RDRectNI Truncate = checked(new RDRectNI((int)Math.Round((double)rect.Left), (int)Math.Round((double)rect.Top), (int)Math.Round((double)rect.Right), (int)Math.Round((double)rect.Bottom)));
			return Truncate;
		}

		public void Offset(int x, int y)
		{
			checked
			{
				Left += x;
				Top += y;
				Right += x;
				Bottom += y;
			}
		}

		public void Offset(RDPointNI p) => Offset(p.X, p.Y);

		public void Inflate(RDSizeNI size)
		{
			checked
			{
				Left -= size.Width;
				Top += size.Height;
				Right += size.Width;
				Bottom -= size.Height;
			}
		}

		public void Inflate(int width, int height)
		{
			checked
			{
				Left -= width;
				Top += height;
				Right += width;
				Bottom -= height;
			}
		}

		public bool Contains(int x, int y) => Left < x && x < Right && Bottom < y && y < Top;

		public bool Contains(RDPointN p) => (float)Left < p.X && p.X < (float)Right && (float)Bottom < p.Y && p.Y < (float)Top;

		public bool Contains(RDRectNI rect) => Left < rect.Left && rect.Right < Right && Bottom < rect.Bottom && rect.Top < Top;

		public RDRectNI Union(RDRectNI rect) => Union(this, rect);

		public object Intersect(RDRectNI rect) => Intersect(this, rect);

		public bool IntersectsWith(RDRectNI rect) => Left < rect.Right && Right > rect.Left && Top < rect.Bottom && Bottom > rect.Top;

		public bool IntersectsWithInclusive(RDRectNI rect) => Left <= rect.Right && Right >= rect.Left && Top <= rect.Bottom && Bottom >= rect.Top;

		public static bool operator ==(RDRectNI rect1, RDRectNI rect2) => rect1.Equals(rect2);

		public static bool operator !=(RDRectNI rect1, RDRectNI rect2) => !rect1.Equals(rect2);

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRectNI) && Equals((obj != null) ? ((RDRectNI)obj) : default);

		public override int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);

		public override string ToString() => string.Format("{{Location=[{0},{1}],Size=[{2},{3}]}}",
			[
				Left,
				Bottom,
				Width,
				Height
			]);

		public bool Equals(RDRectNI other) => Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;

		public static implicit operator RDRectN(RDRectNI rect)
		{
			RDRectN result = new((float)rect.Left, (float)rect.Top, (float)rect.Right, (float)rect.Bottom);
			return result;
		}

		public static implicit operator RDRectI(RDRectNI rect)
		{
			RDRectI result = new(new int?(rect.Left), new int?(rect.Top), new int?(rect.Right), new int?(rect.Bottom));
			return result;
		}

		public static implicit operator RDRectE(RDRectNI rect)
		{
			RDRectE result = new(new Expression?((float)rect.Left), new Expression?((float)rect.Top), new Expression?((float)rect.Right), new Expression?((float)rect.Bottom));
			return result;
		}
	}
}
