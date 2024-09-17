using System;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct RDRectE : IEquatable<RDRectE>
	{
		public Expression? Left { get; set; }

		public Expression? Right { get; set; }

		public Expression? Top { get; set; }

		public Expression? Bottom { get; set; }

		public Expression? Width
		{
			get
			{
				return Right - Left;
			}
		}

		public Expression? Height
		{
			get
			{
				return Top - Bottom;
			}
		}

		public RDRectE(Expression? left, Expression? top, Expression? right, Expression? bottom)
		{
			this = default;
			Left = left;
			Right = right;
			Top = top;
			Bottom = bottom;
		}

		public RDRectE(PointE location, RDSizeE size)
		{
			this = new RDRectE(location.X, location.Y + size.Height, location.X + size.Width, location.Y);
		}

		public RDRectE(RDSizeE size)
		{
			this = new RDRectE(new Expression?(0f), size.Height, size.Width, new Expression?(0f));
		}

		public RDRectE(Expression? width, Expression? height)
		{
			this = new RDRectE(new Expression?(0f), height, width, new Expression?(0f));
		}

		public PointE Location
		{
			get
			{
				PointE Location = new(Left, Bottom);
				return Location;
			}
		}

		public RDSizeE Size
		{
			get
			{
				RDSizeE Size = new(Width, Height);
				return Size;
			}
		}

		public static RDRectE Inflate(RDRectE rect, RDSizeE size)
		{
			RDRectE result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(size);
			return result;
		}

		public static RDRectE Inflate(RDRectE rect, Expression? x, Expression? y)
		{
			RDRectE result = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			result.Inflate(x, y);
			return result;
		}

		public static RDRectE Truncate(RDRectE rect)
		{
			RDRectE Truncate = new(rect.Left, rect.Top, rect.Right, rect.Bottom);
			return Truncate;
		}

		public void Offset(Expression? x, Expression? y)
		{
			Left += x;
			Top += y;
			Right += x;
			Bottom += y;
		}

		public void Offset(PointE p) => Offset(p.X, p.Y);

		public void Inflate(RDSizeE size)
		{
			Left -= size.Width;
			Top += size.Height;
			Right += size.Width;
			Bottom -= size.Height;
		}

		public void Inflate(Expression? width, Expression? height)
		{
			Left -= width;
			Top += height;
			Right += width;
			Bottom -= height;
		}

		public static bool operator ==(RDRectE rect1, RDRectE rect2) => rect1.Equals(rect2);

		public static bool operator !=(RDRectE rect1, RDRectE rect2) => !rect1.Equals(rect2);

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRectE) && Equals((obj != null) ? ((RDRectE)obj) : default);

		public override int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);

		public override string ToString() => string.Format("{{Location=[{0},{1}],Size=[{2},{3}]}}",
			[
				Left,
				Bottom,
				Width,
				Height
			]);

		public bool Equals(RDRectE other)
		{
			Expression? expression = Left;
			Expression? expression2 = other.Left;
			bool? flag2;
			bool? flag = flag2 = (expression != null & expression2 != null) ? new bool?(expression.GetValueOrDefault() == expression2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				expression2 = Top;
				expression = other.Top;
				flag3 = flag2 = (expression2 != null & expression != null) ? new bool?(expression2.GetValueOrDefault() == expression.GetValueOrDefault()) : null;
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
				expression = Right;
				expression2 = other.Right;
				flag6 = flag3 = (expression != null & expression2 != null) ? new bool?(expression.GetValueOrDefault() == expression2.GetValueOrDefault()) : null;
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
				expression2 = Bottom;
				expression = other.Bottom;
				flag9 = flag6 = (expression2 != null & expression != null) ? new bool?(expression2.GetValueOrDefault() == expression.GetValueOrDefault()) : null;
				flag10 = (flag6 != null) ? (flag8 & flag9.GetValueOrDefault()) : null;
			}
			else
			{
				flag10 = new bool?(false);
			}
			flag9 = flag10;
			return flag9.Value;
		}
	}
}
