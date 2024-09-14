using System;
using System.Diagnostics.CodeAnalysis;

namespace RhythmBase.Components
{

	public struct RDRect : IEquatable<RDRect>
	{

		public float? Left { get; set; }


		public float? Right { get; set; }


		public float? Top { get; set; }


		public float? Bottom { get; set; }


		public float? Width
		{
			get
			{
				return Right - Left;
			}
		}


		public float? Height
		{
			get
			{
				return Top - Bottom;
			}
		}


		public RDRect(float? left, float? top, float? right, float? bottom)
		{
			this = default;
			Left = left;
			Right = right;
			Top = top;
			Bottom = bottom;
		}


		public RDRect(RDPoint location, RDSize size)
		{
			this = new RDRect(location.X, location.Y + size.Height, location.X + size.Width, location.Y);
		}


		public RDRect(RDSize size)
		{
			this = new RDRect(new float?(0f), size.Height, size.Width, new float?(0f));
		}


		public RDRect(float? width, float? height)
		{
			this = new RDRect(new float?(0f), height, width, new float?(0f));
		}


		public RDPoint Location
		{
			get
			{
				RDPoint Location = new(Left, Bottom);
				return Location;
			}
		}


		public RDSize Size
		{
			get
			{
				RDSize Size = new(Width, Height);
				return Size;
			}
		}


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
			RDRect Truncate = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
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


		public bool Contains(float? x, float? y)
		{
			float? num = Left;
			bool? flag2;
			bool? flag = flag2 = (num != null & x != null) ? new bool?(num.GetValueOrDefault() < x.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				num = x;
				float? num2 = Right;
				flag3 = flag2 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() < num2.GetValueOrDefault()) : null;
				flag4 = (flag2 != null) ? (flag & flag3.GetValueOrDefault()) : null;
			}
			else
			{
				flag4 = new bool?(false);
			}
			bool? flag5 = flag3 = flag4;
			bool? flag6;
			bool? flag7;
			if (flag3 == null || flag5.GetValueOrDefault())
			{
				float? num2 = Bottom;
				flag6 = flag3 = (num2 != null & y != null) ? new bool?(num2.GetValueOrDefault() < y.GetValueOrDefault()) : null;
				flag7 = (flag3 != null) ? (flag5 & flag6.GetValueOrDefault()) : null;
			}
			else
			{
				flag7 = new bool?(false);
			}
			bool? flag8 = flag6 = flag7;
			bool? flag9;
			bool? flag10;
			if (flag6 == null || flag8.GetValueOrDefault())
			{
				float? num2 = y;
				num = Top;
				flag9 = flag6 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() < num.GetValueOrDefault()) : null;
				flag10 = (flag6 != null) ? (flag8 & flag9.GetValueOrDefault()) : null;
			}
			else
			{
				flag10 = new bool?(false);
			}
			flag9 = flag10;
			return flag9.Value;
		}


		public bool Contains(RDPoint p)
		{
			float? num = Left;
			float? num2 = p.X;
			bool? flag2;
			bool? flag = flag2 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() < num2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				num2 = p.X;
				num = Right;
				flag3 = flag2 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() < num.GetValueOrDefault()) : null;
				flag4 = (flag2 != null) ? (flag & flag3.GetValueOrDefault()) : null;
			}
			else
			{
				flag4 = new bool?(false);
			}
			bool? flag5 = flag3 = flag4;
			bool? flag6;
			bool? flag7;
			if (flag3 == null || flag5.GetValueOrDefault())
			{
				num = Bottom;
				num2 = p.Y;
				flag6 = flag3 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() < num2.GetValueOrDefault()) : null;
				flag7 = (flag3 != null) ? (flag5 & flag6.GetValueOrDefault()) : null;
			}
			else
			{
				flag7 = new bool?(false);
			}
			bool? flag8 = flag6 = flag7;
			bool? flag9;
			bool? flag10;
			if (flag6 == null || flag8.GetValueOrDefault())
			{
				num2 = p.Y;
				num = Top;
				flag9 = flag6 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() < num.GetValueOrDefault()) : null;
				flag10 = (flag6 != null) ? (flag8 & flag9.GetValueOrDefault()) : null;
			}
			else
			{
				flag10 = new bool?(false);
			}
			flag9 = flag10;
			return flag9.Value;
		}


		public bool Contains(RDRect rect)
		{
			float? num = Left;
			float? num2 = rect.Left;
			bool? flag2;
			bool? flag = flag2 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() < num2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				num2 = rect.Right;
				num = Right;
				flag3 = flag2 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() < num.GetValueOrDefault()) : null;
				flag4 = (flag2 != null) ? (flag & flag3.GetValueOrDefault()) : null;
			}
			else
			{
				flag4 = new bool?(false);
			}
			bool? flag5 = flag3 = flag4;
			bool? flag6;
			bool? flag7;
			if (flag3 == null || flag5.GetValueOrDefault())
			{
				num = Bottom;
				num2 = rect.Bottom;
				flag6 = flag3 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() < num2.GetValueOrDefault()) : null;
				flag7 = (flag3 != null) ? (flag5 & flag6.GetValueOrDefault()) : null;
			}
			else
			{
				flag7 = new bool?(false);
			}
			bool? flag8 = flag6 = flag7;
			bool? flag9;
			bool? flag10;
			if (flag6 == null || flag8.GetValueOrDefault())
			{
				num2 = rect.Top;
				num = Top;
				flag9 = flag6 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() < num.GetValueOrDefault()) : null;
				flag10 = (flag6 != null) ? (flag8 & flag9.GetValueOrDefault()) : null;
			}
			else
			{
				flag10 = new bool?(false);
			}
			flag9 = flag10;
			return flag9.Value;
		}


		public RDRect Union(RDRect rect) => Union(this, rect);


		public object Intersect(RDRect rect) => Intersect(this, rect);


		public bool IntersectsWith(RDRect rect)
		{
			float? num = Left;
			float? num2 = rect.Right;
			bool? flag2;
			bool? flag = flag2 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() < num2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				num2 = Right;
				num = rect.Left;
				flag3 = flag2 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() > num.GetValueOrDefault()) : null;
				flag4 = (flag2 != null) ? (flag & flag3.GetValueOrDefault()) : null;
			}
			else
			{
				flag4 = new bool?(false);
			}
			bool? flag5 = flag3 = flag4;
			bool? flag6;
			bool? flag7;
			if (flag3 == null || flag5.GetValueOrDefault())
			{
				num = Top;
				num2 = rect.Bottom;
				flag6 = flag3 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() < num2.GetValueOrDefault()) : null;
				flag7 = (flag3 != null) ? (flag5 & flag6.GetValueOrDefault()) : null;
			}
			else
			{
				flag7 = new bool?(false);
			}
			bool? flag8 = flag6 = flag7;
			bool? flag9;
			bool? flag10;
			if (flag6 == null || flag8.GetValueOrDefault())
			{
				num2 = Bottom;
				num = rect.Top;
				flag9 = flag6 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() > num.GetValueOrDefault()) : null;
				flag10 = (flag6 != null) ? (flag8 & flag9.GetValueOrDefault()) : null;
			}
			else
			{
				flag10 = new bool?(false);
			}
			flag9 = flag10;
			return flag9.Value;
		}


		public bool IntersectsWithInclusive(RDRect rect)
		{
			float? num = Left;
			float? num2 = rect.Right;
			bool? flag2;
			bool? flag = flag2 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() <= num2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				num2 = Right;
				num = rect.Left;
				flag3 = flag2 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() >= num.GetValueOrDefault()) : null;
				flag4 = (flag2 != null) ? (flag & flag3.GetValueOrDefault()) : null;
			}
			else
			{
				flag4 = new bool?(false);
			}
			bool? flag5 = flag3 = flag4;
			bool? flag6;
			bool? flag7;
			if (flag3 == null || flag5.GetValueOrDefault())
			{
				num = Top;
				num2 = rect.Bottom;
				flag6 = flag3 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() <= num2.GetValueOrDefault()) : null;
				flag7 = (flag3 != null) ? (flag5 & flag6.GetValueOrDefault()) : null;
			}
			else
			{
				flag7 = new bool?(false);
			}
			bool? flag8 = flag6 = flag7;
			bool? flag9;
			bool? flag10;
			if (flag6 == null || flag8.GetValueOrDefault())
			{
				num2 = Bottom;
				num = rect.Top;
				flag9 = flag6 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() >= num.GetValueOrDefault()) : null;
				flag10 = (flag6 != null) ? (flag8 & flag9.GetValueOrDefault()) : null;
			}
			else
			{
				flag10 = new bool?(false);
			}
			flag9 = flag10;
			return flag9.Value;
		}


		public static bool operator ==(RDRect rect1, RDRect rect2) => rect1.Equals(rect2);


		public static bool operator !=(RDRect rect1, RDRect rect2) => !rect1.Equals(rect2);


		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRect) && Equals((obj != null) ? ((RDRect)obj) : default);


		public override int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);


		public override string ToString() => string.Format("{{Location=[{0},{1}],Size=[{2},{3}]}}",
			[
				Left,
				Bottom,
				Width,
				Height
			]);


		public bool Equals(RDRect other)
		{
			float? num = Left;
			float? num2 = other.Left;
			bool? flag2;
			bool? flag = flag2 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() == num2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				num2 = Top;
				num = other.Top;
				flag3 = flag2 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() == num.GetValueOrDefault()) : null;
				flag4 = (flag2 != null) ? (flag & flag3.GetValueOrDefault()) : null;
			}
			else
			{
				flag4 = new bool?(false);
			}
			bool? flag5 = flag3 = flag4;
			bool? flag6;
			bool? flag7;
			if (flag3 == null || flag5.GetValueOrDefault())
			{
				num = Right;
				num2 = other.Right;
				flag6 = flag3 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() == num2.GetValueOrDefault()) : null;
				flag7 = (flag3 != null) ? (flag5 & flag6.GetValueOrDefault()) : null;
			}
			else
			{
				flag7 = new bool?(false);
			}
			bool? flag8 = flag6 = flag7;
			bool? flag9;
			bool? flag10;
			if (flag6 == null || flag8.GetValueOrDefault())
			{
				num2 = Bottom;
				num = other.Bottom;
				flag9 = flag6 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() == num.GetValueOrDefault()) : null;
				flag10 = (flag6 != null) ? (flag8 & flag9.GetValueOrDefault()) : null;
			}
			else
			{
				flag10 = new bool?(false);
			}
			flag9 = flag10;
			return flag9.Value;
		}


		public static implicit operator RDRectE(RDRect rect)
		{
			float? num2;
			float? num = num2 = rect.Left;
			Expression? left = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
			num = num2 = rect.Top;
			Expression? top = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
			num = num2 = rect.Right;
			Expression? right = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
			num = num2 = rect.Bottom;
			RDRectE result = new(left, top, right, (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null);
			return result;
		}
	}
}
