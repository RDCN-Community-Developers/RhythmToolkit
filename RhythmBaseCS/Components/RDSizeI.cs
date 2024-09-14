using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Converters;

namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="integer" />
	/// </summary>

	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDSizeI : IEquatable<RDSizeI>
	{

		public RDSizeI(RDPointI pt)
		{
			this = default;
			Width = pt.X;
			Height = pt.Y;
		}


		public RDSizeI(int? width, int? height)
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


		public int? Width { get; set; }


		public int? Height { get; set; }


		public int? Area
		{
			get
			{
				return checked(Width * Height);
			}
		}


		public static RDSizeI Add(RDSizeI sz1, RDSizeI sz2)
		{
			RDSizeI Add = checked(new RDSizeI(sz1.Width + sz2.Width, sz1.Height + sz2.Height));
			return Add;
		}


		public static RDSizeI Truncate(RDSize value)
		{
			RDSizeI Truncate = checked(new RDSizeI(new int?((int)Math.Round((value.Width == null) ? 0.0 : Math.Truncate((double)value.Width.Value))), new int?((int)Math.Round((value.Height == null) ? 0.0 : Math.Truncate((double)value.Height.Value)))));
			return Truncate;
		}


		public static RDSizeI Subtract(RDSizeI sz1, RDSizeI sz2)
		{
			RDSizeI Subtract = checked(new RDSizeI(sz1.Width - sz2.Width, sz1.Height - sz2.Height));
			return Subtract;
		}


		public static RDSizeI Ceiling(RDSize value)
		{
			RDSizeI Ceiling = checked(new RDSizeI(new int?((int)Math.Round((value.Width == null) ? 0.0 : Math.Ceiling((double)value.Width.Value))), new int?((int)Math.Round((value.Height == null) ? 0.0 : Math.Ceiling((double)value.Height.Value)))));
			return Ceiling;
		}


		public static RDSizeI Round(RDSize value)
		{
			RDSizeI Round = checked(new RDSizeI(new int?((int)Math.Round((value.Width == null) ? 0.0 : Math.Round((double)value.Width.Value))), new int?((int)Math.Round((value.Height == null) ? 0.0 : Math.Round((double)value.Height.Value)))));
			return Round;
		}


		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSizeI) && Equals((obj != null) ? ((RDSizeI)obj) : default);


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
			int? num2;
			int? num = num2 = Width;
			object objectValue = RuntimeHelpers.GetObjectValue((num2 != null) ? num.GetValueOrDefault() : "null");
			num = num2 = Height;
			return string.Format(format, objectValue, RuntimeHelpers.GetObjectValue((num2 != null) ? num.GetValueOrDefault() : "null"));
		}


		public bool Equals(RDSizeI other)
		{
			int? num = Width;
			int? num2 = other.Width;
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


		public static RDSizeI operator +(RDSizeI sz1, RDSizeI sz2) => Add(sz1, sz2);


		public static RDSizeI operator -(RDSizeI sz1, RDSizeI sz2) => Subtract(sz1, sz2);


		public static RDSize operator *(float left, RDSizeI right)
		{
			int? num = right.Width;
			float? width = left * ((num != null) ? new float?((float)num.GetValueOrDefault()) : null);
			num = right.Height;
			RDSize result = new(width, left * ((num != null) ? new float?((float)num.GetValueOrDefault()) : null));
			return result;
		}


		public static RDSize operator *(RDSizeI left, float right)
		{
			int? num = left.Width;
			float? width = ((num != null) ? new float?((float)num.GetValueOrDefault()) : null) * right;
			num = left.Height;
			RDSize result = new(width, ((num != null) ? new float?((float)num.GetValueOrDefault()) : null) * right);
			return result;
		}


		public static RDSizeI operator *(int left, RDSizeI right)
		{
			RDSizeI result = checked(new RDSizeI(left * right.Width, left * right.Height));
			return result;
		}


		public static RDSizeI operator *(RDSizeI left, int right)
		{
			RDSizeI result = checked(new RDSizeI(left.Width * right, left.Height * right));
			return result;
		}


		public static RDSize operator /(RDSizeI left, float right)
		{
			int? num = left.Width;
			float? width = ((num != null) ? new float?((float)num.GetValueOrDefault()) : null) / right;
			num = left.Height;
			RDSize result = new(width, ((num != null) ? new float?((float)num.GetValueOrDefault()) : null) / right);
			return result;
		}


		public static RDSizeI operator /(RDSizeI left, int right)
		{
			int? num = left.Width;
			double? num2 = ((num != null) ? new double?((double)num.GetValueOrDefault()) : null) / (double)right;
			checked
			{
				int? width = (num2 != null) ? new int?((int)Math.Round(num2.GetValueOrDefault())) : null;
				num = left.Height;
				num2 = ((num != null) ? new double?((double)num.GetValueOrDefault()) : null) / (double)right;
				RDSizeI result = new(width, (num2 != null) ? new int?((int)Math.Round(num2.GetValueOrDefault())) : null);
				return result;
			}
		}


		public static bool operator ==(RDSizeI sz1, RDSizeI sz2) => sz1.Equals(sz2);


		public static bool operator !=(RDSizeI sz1, RDSizeI sz2) => !sz1.Equals(sz2);


		public static implicit operator RDSize(RDSizeI p)
		{
			int? num = p.Width;
			float? width = (num != null) ? new float?((float)num.GetValueOrDefault()) : null;
			num = p.Height;
			RDSize result = new(width, (num != null) ? new float?((float)num.GetValueOrDefault()) : null);
			return result;
		}


		public static implicit operator RDSizeE(RDSizeI p)
		{
			int? num2;
			int? num = num2 = p.Width;
			Expression? width = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
			num = num2 = p.Height;
			RDSizeE result = new(width, (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null);
			return result;
		}


		public static explicit operator RDPointI(RDSizeI size)
		{
			RDPointI result = new(size.Width, size.Height);
			return result;
		}
	}
}
