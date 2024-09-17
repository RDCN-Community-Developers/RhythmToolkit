using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="float" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDSize : IEquatable<RDSize>
	{
		public RDSize(RDPoint pt)
		{
			this = default;
			Width = pt.X;
			Height = pt.Y;
		}

		public RDSize(float? width, float? height)
		{
			this = default;
			this.Width = width;
			this.Height = height;
		}

		public bool IsEmpty
		{
			get
			{
				return Width == null && Height == null;
			}
		}

		public float? Width { get; set; }

		public float? Height { get; set; }

		public float? Area
		{
			get
			{
				return Width * Height;
			}
		}

		public static RDSize Add(RDSize sz1, RDSize sz2)
		{
			RDSize Add = new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
			return Add;
		}

		public static RDSize Subtract(RDSize sz1, RDSize sz2)
		{
			RDSize Subtract = new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
			return Subtract;
		}

		public override int GetHashCode()
		{
			HashCode h = default;
			h.Add(Width);
			h.Add(Height);
			return h.ToHashCode();
		}

		public override string ToString()
		{
			string format = "[{0}, {1}]";
			float? num2;
			float? num = num2 = Width;
			object objectValue = RuntimeHelpers.GetObjectValue((num2 != null) ? num.GetValueOrDefault() : "null");
			num = num2 = Height;
			return string.Format(format, objectValue, RuntimeHelpers.GetObjectValue((num2 != null) ? num.GetValueOrDefault() : "null"));
		}

		public bool Equals(RDSize other)
		{
			float? num = Width;
			float? num2 = other.Width;
			bool? flag2;
			bool? flag = flag2 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() == num2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				num2 = Height;
				num = other.Height;
				flag3 = flag2 = (num2 != null & num != null) ? new bool?(num2.GetValueOrDefault() == num.GetValueOrDefault()) : null;
				flag4 = (flag2 != null) ? (flag & flag3.GetValueOrDefault()) : null;
			}
			else
			{
				flag4 = new bool?(false);
			}
			flag3 = flag4;
			return flag3.Value;
		}

		public RDSizeI ToSize()
		{
			float? num = Width;
			checked
			{
				int? width = (num != null) ? new int?((int)Math.Round((double)num.GetValueOrDefault())) : null;
				num = Height;
				RDSizeI ToSize = new(width, (num != null) ? new int?((int)Math.Round((double)num.GetValueOrDefault())) : null);
				return ToSize;
			}
		}

		public RDPoint ToPointF()
		{
			RDPoint ToPointF = new(Width, Height);
			return ToPointF;
		}

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSize) && Equals((obj != null) ? ((RDSize)obj) : default);

		public static RDSize operator +(RDSize sz1, RDSize sz2) => Add(sz1, sz2);

		public static RDSize operator -(RDSize sz1, RDSize sz2) => Subtract(sz1, sz2);

		public static RDSize operator *(float left, RDSize right)
		{
			RDSize result = new(left * right.Width, left * right.Height);
			return result;
		}

		public static RDSize operator *(RDSize left, float right)
		{
			RDSize result = new(left.Width * right, left.Height * right);
			return result;
		}

		public static RDSize operator /(RDSize left, float right)
		{
			RDSize result = new(left.Width / right, left.Height / right);
			return result;
		}

		public static bool operator ==(RDSize sz1, RDSize sz2) => sz1.Equals(sz2);

		public static bool operator !=(RDSize sz1, RDSize sz2) => !sz1.Equals(sz2);

		public static implicit operator RDSizeE(RDSize size)
		{
			float? num2;
			float? num = num2 = size.Width;
			Expression? width = (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null;
			num = num2 = size.Height;
			RDSizeE result = new(width, (num2 != null) ? new Expression?(num.GetValueOrDefault()) : null);
			return result;
		}

		public static explicit operator RDPoint(RDSize size)
		{
			RDPoint result = new(size.Width, size.Height);
			return result;
		}
	}
}
