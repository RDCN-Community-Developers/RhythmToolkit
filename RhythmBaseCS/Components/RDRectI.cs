using System;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	public struct RDRectI : IEquatable<RDRectI>
	{
		public int? Left { get; set; }

		public int? Right { get; set; }

		public int? Top { get; set; }

		public int? Bottom { get; set; }

		public int? Width
		{
			get
			{
				return checked(Right - Left);
			}
		}

		public int? Height
		{
			get
			{
				return checked(Top - Bottom);
			}
		}

		public RDRectI(int? left, int? top, int? right, int? bottom)
		{
			this = default;
			Left = left;
			Right = right;
			Top = top;
			Bottom = bottom;
		}

		public RDRectI(RDPointI location, RDSizeI size)
		{
			this = checked(new RDRectI(location.X, location.Y + size.Height, location.X + size.Width, location.Y));
		}

		public RDRectI(RDSizeI size)
		{
			this = new RDRectI(new int?(0), size.Height, size.Width, new int?(0));
		}

		public RDRectI(int? width, int? height)
		{
			this = new RDRectI(new int?(0), height, width, new int?(0));
		}

		public RDPointI Location
		{
			get
			{
				RDPointI Location = new(Left, Bottom);
				return Location;
			}
		}

		public RDSizeI Size
		{
			get
			{
				RDSizeI Size = new(Width, Height);
				return Size;
			}
		}

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

		public static RDRectI Ceiling(RDRect rect, bool outwards)
		{
			double a;
			if (rect.Left != null)
			{
				if (outwards)
				{
					float? num = rect.Width;
					if (((num != null) ? new bool?(num.GetValueOrDefault() > 0f) : null).GetValueOrDefault())
					{
						a = Math.Floor((double)rect.Left.Value);
						goto IL_7C;
					}
				}
				a = Math.Ceiling((double)rect.Left.Value);
			IL_7C:;
			}
			else
			{
				a = 0.0;
			}
			checked
			{
				int? left = new int?((int)Math.Round(a));
				double a2;
				if (rect.Top != null)
				{
					if (outwards)
					{
						float? num = rect.Height;
						if (((num != null) ? new bool?(num.GetValueOrDefault() > 0f) : null).GetValueOrDefault())
						{
							a2 = Math.Floor((double)rect.Top.Value);
							goto IL_10B;
						}
					}
					a2 = Math.Ceiling((double)rect.Top.Value);
				IL_10B:;
				}
				else
				{
					a2 = 0.0;
				}
				int? top = new int?((int)Math.Round(a2));
				double a3;
				if (rect.Right != null)
				{
					if (outwards)
					{
						float? num = rect.Width;
						if (((num != null) ? new bool?(num.GetValueOrDefault() < 0f) : null).GetValueOrDefault())
						{
							a3 = Math.Floor((double)rect.Right.Value);
							goto IL_19A;
						}
					}
					a3 = Math.Ceiling((double)rect.Right.Value);
				IL_19A:;
				}
				else
				{
					a3 = 0.0;
				}
				int? right = new int?((int)Math.Round(a3));
				double a4;
				if (rect.Bottom != null)
				{
					if (outwards)
					{
						float? num = rect.Height;
						if (((num != null) ? new bool?(num.GetValueOrDefault() < 0f) : null).GetValueOrDefault())
						{
							a4 = Math.Floor((double)rect.Bottom.Value);
							goto IL_229;
						}
					}
					a4 = Math.Ceiling((double)rect.Bottom.Value);
				IL_229:;
				}
				else
				{
					a4 = 0.0;
				}
				RDRectI Ceiling = new(left, top, right, new int?((int)Math.Round(a4)));
				return Ceiling;
			}
		}

		public static RDRectI Floor(RDRect rect) => Ceiling(rect, false);

		public static RDRectI Floor(RDRect rect, bool inwards)
		{
			double a;
			if (rect.Left != null)
			{
				if (inwards)
				{
					float? num = rect.Width;
					if (((num != null) ? new bool?(num.GetValueOrDefault() > 0f) : null).GetValueOrDefault())
					{
						a = Math.Ceiling((double)rect.Left.Value);
						goto IL_7C;
					}
				}
				a = Math.Floor((double)rect.Left.Value);
			IL_7C:;
			}
			else
			{
				a = 0.0;
			}
			checked
			{
				int? left = new int?((int)Math.Round(a));
				double a2;
				if (rect.Top != null)
				{
					if (inwards)
					{
						float? num = rect.Height;
						if (((num != null) ? new bool?(num.GetValueOrDefault() > 0f) : null).GetValueOrDefault())
						{
							a2 = Math.Ceiling((double)rect.Top.Value);
							goto IL_10B;
						}
					}
					a2 = Math.Floor((double)rect.Top.Value);
				IL_10B:;
				}
				else
				{
					a2 = 0.0;
				}
				int? top = new int?((int)Math.Round(a2));
				double a3;
				if (rect.Right != null)
				{
					if (inwards)
					{
						float? num = rect.Width;
						if (((num != null) ? new bool?(num.GetValueOrDefault() < 0f) : null).GetValueOrDefault())
						{
							a3 = Math.Ceiling((double)rect.Right.Value);
							goto IL_19A;
						}
					}
					a3 = Math.Floor((double)rect.Right.Value);
				IL_19A:;
				}
				else
				{
					a3 = 0.0;
				}
				int? right = new int?((int)Math.Round(a3));
				double a4;
				if (rect.Bottom != null)
				{
					if (inwards)
					{
						float? num = rect.Height;
						if (((num != null) ? new bool?(num.GetValueOrDefault() < 0f) : null).GetValueOrDefault())
						{
							a4 = Math.Ceiling((double)rect.Bottom.Value);
							goto IL_229;
						}
					}
					a4 = Math.Floor((double)rect.Bottom.Value);
				IL_229:;
				}
				else
				{
					a4 = 0.0;
				}
				RDRectI Floor = new(left, top, right, new int?((int)Math.Round(a4)));
				return Floor;
			}
		}

		public static RDRectI Round(RDRect rect)
		{
			RDRectI Round = checked(new RDRectI(new int?((int)Math.Round((rect.Left == null) ? 0.0 : Math.Round((double)rect.Left.Value))), new int?((int)Math.Round((rect.Top == null) ? 0.0 : Math.Round((double)rect.Top.Value))), new int?((int)Math.Round((rect.Right == null) ? 0.0 : Math.Round((double)rect.Right.Value))), new int?((int)Math.Round((rect.Bottom == null) ? 0.0 : Math.Round((double)rect.Bottom.Value)))));
			return Round;
		}

		public static RDRectI Union(RDRectI rect1, RDRectI rect2)
		{
			RDRectI Union = new(new int?((rect1.Left == null || rect2.Left == null) ? 0 : Math.Min(rect1.Left.Value, rect2.Left.Value)), new int?((rect1.Top == null || rect2.Top == null) ? 0 : Math.Min(rect1.Top.Value, rect2.Top.Value)), new int?((rect1.Right == null || rect2.Right == null) ? 0 : Math.Min(rect1.Right.Value, rect2.Right.Value)), new int?((rect1.Bottom == null || rect2.Bottom == null) ? 0 : Math.Min(rect1.Bottom.Value, rect2.Bottom.Value)));
			return Union;
		}

		public static RDRectI Intersect(RDRectI rect1, RDRectI rect2) => rect1.IntersectsWithInclusive(rect2) ? new RDRectI(new int?((rect1.Left == null || rect2.Left == null) ? 0 : Math.Max(rect1.Left.Value, rect2.Left.Value)), new int?((rect1.Top == null || rect2.Top == null) ? 0 : Math.Max(rect1.Top.Value, rect2.Top.Value)), new int?((rect1.Right == null || rect2.Right == null) ? 0 : Math.Min(rect1.Right.Value, rect2.Right.Value)), new int?((rect1.Bottom == null || rect2.Bottom == null) ? 0 : Math.Min(rect1.Bottom.Value, rect2.Bottom.Value))) : default;

		public static RDRectI Truncate(RDRect rect)
		{
			float? num = rect.Left;
			checked
			{
				int? left = (num != null) ? new int?((int)Math.Round((double)num.GetValueOrDefault())) : null;
				num = rect.Top;
				int? top = (num != null) ? new int?((int)Math.Round((double)num.GetValueOrDefault())) : null;
				num = rect.Right;
				int? right = (num != null) ? new int?((int)Math.Round((double)num.GetValueOrDefault())) : null;
				num = rect.Bottom;
				RDRectI Truncate = new(left, top, right, (num != null) ? new int?((int)Math.Round((double)num.GetValueOrDefault())) : null);
				return Truncate;
			}
		}

		public void Offset(int? x, int? y)
		{
			checked
			{
				Left += x;
				Top += y;
				Right += x;
				Bottom += y;
			}
		}

		public void Offset(RDPointI p) => Offset(p.X, p.Y);

		public void Inflate(RDSizeI size)
		{
			checked
			{
				Left -= size.Width;
				Top += size.Height;
				Right += size.Width;
				Bottom -= size.Height;
			}
		}

		public void Inflate(int? width, int? height)
		{
			checked
			{
				Left -= width;
				Top += height;
				Right += width;
				Bottom -= height;
			}
		}

		public RDRectI Union(RDRectI rect) => Union(this, rect);

		public bool IntersectsWithInclusive(RDRectI rect)
		{
			int? num = Left;
			int? num2 = rect.Right;
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

		public static bool operator ==(RDRectI rect1, RDRectI rect2) => rect1.Equals(rect2);

		public static bool operator !=(RDRectI rect1, RDRectI rect2) => !rect1.Equals(rect2);

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRectI) && Equals((obj != null) ? ((RDRectI)obj) : default);

		public override int GetHashCode() => HashCode.Combine(Left, Top, Right, Bottom);

		public override string ToString() => string.Format("{{Location=[{0},{1}],Size=[{2},{3}]}}",
			[
				Left,
				Bottom,
				Width,
				Height
			]);

		public bool Equals(RDRectI other)
		{
			int? num = Left;
			int? num2 = other.Left;
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

		public static implicit operator RDRect(RDRectI rect)
		{
			int? num = rect.Left;
			float? left = (num != null) ? new float?((float)num.GetValueOrDefault()) : null;
			num = rect.Top;
			float? top = (num != null) ? new float?((float)num.GetValueOrDefault()) : null;
			num = rect.Right;
			float? right = (num != null) ? new float?((float)num.GetValueOrDefault()) : null;
			num = rect.Bottom;
			RDRect result = new(left, top, right, (num != null) ? new float?((float)num.GetValueOrDefault()) : null);
			return result;
		}

		public static implicit operator RDRectE(RDRectI rect)
		{
			int? num2;
			int? num = num2 = rect.Left;
			Expression? left = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
			num = num2 = rect.Top;
			Expression? top = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
			num = num2 = rect.Right;
			Expression? right = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
			num = num2 = rect.Bottom;
			RDRectE result = new(left, top, right, (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null);
			return result;
		}
	}
}
