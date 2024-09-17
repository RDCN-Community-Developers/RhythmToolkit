using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDSizeNI : IEquatable<RDSizeNI>
	{
		public RDSizeNI(RDPointNI pt)
		{
			this = default;
			Width = pt.X;
			Height = pt.Y;
		}

		public RDSizeNI(int width, int height)
		{
			this = default;
			this.Width = width;
			this.Height = height;
		}

		public int Width { get; set; }

		public int Height { get; set; }

		public int Area
		{
			get
			{
				return checked(Width * Height);
			}
		}

		public static RDSizeNI Add(RDSizeNI sz1, RDSizeNI sz2)
		{
			RDSizeNI Add = checked(new RDSizeNI(sz1.Width + sz2.Width, sz1.Height + sz2.Height));
			return Add;
		}

		public static RDSizeNI Truncate(RDSizeN value)
		{
			RDSizeNI Truncate = checked(new RDSizeNI((int)(double)value.Width, (int)(double)value.Height));
			return Truncate;
		}

		public static RDSizeNI Subtract(RDSizeNI sz1, RDSizeNI sz2)
		{
			RDSizeNI Subtract = checked(new RDSizeNI(sz1.Width - sz2.Width, sz1.Height - sz2.Height));
			return Subtract;
		}

		public static RDSizeNI Ceiling(RDSizeN value)
		{
			RDSizeNI Ceiling = checked(new RDSizeNI((int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height)));
			return Ceiling;
		}

		public static RDSizeNI Round(RDSizeN value)
		{
			RDSizeNI Round = checked(new RDSizeNI((int)Math.Round((double)value.Width), (int)Math.Round((double)value.Height)));
			return Round;
		}

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSizeNI) && Equals((obj != null) ? ((RDSizeNI)obj) : default);

		public override int GetHashCode()
		{
			HashCode h = default;
			h.Add(Width);
			h.Add(Height);
			return h.ToHashCode();
		}

		public override string ToString() => string.Format("[{0}, {1}]", Width, Height);

		public bool Equals(RDSizeNI other) => Width == other.Width && Height == other.Height;

		public static RDSizeNI operator +(RDSizeNI sz1, RDSizeNI sz2) => Add(sz1, sz2);

		public static RDSizeNI operator -(RDSizeNI sz1, RDSizeNI sz2) => Subtract(sz1, sz2);

		public static RDSizeN operator *(float left, RDSizeNI right)
		{
			RDSizeN result = new(left * (float)right.Width, left * (float)right.Height);
			return result;
		}

		public static RDSizeN operator *(RDSizeNI left, float right)
		{
			RDSizeN result = new((float)left.Width * right, (float)left.Height * right);
			return result;
		}

		public static RDSizeNI operator *(int left, RDSizeNI right)
		{
			RDSizeNI result = checked(new RDSizeNI(left * right.Width, left * right.Height));
			return result;
		}

		public static RDSizeNI operator *(RDSizeNI left, int right)
		{
			RDSizeNI result = checked(new RDSizeNI(left.Width * right, left.Height * right));
			return result;
		}

		public static RDSizeN operator /(RDSizeNI left, float right)
		{
			RDSizeN result = new((float)left.Width / right, (float)left.Height / right);
			return result;
		}

		public static RDSizeNI operator /(RDSizeNI left, int right)
		{
			RDSizeNI result = checked(new RDSizeNI((int)Math.Round((double)left.Width / (double)right), (int)Math.Round((double)left.Height / (double)right)));
			return result;
		}

		public static bool operator ==(RDSizeNI sz1, RDSizeNI sz2) => sz1.Equals(sz2);

		public static bool operator !=(RDSizeNI sz1, RDSizeNI sz2) => !sz1.Equals(sz2);

		public static implicit operator RDSizeN(RDSizeNI p)
		{
			RDSizeN result = new((float)p.Width, (float)p.Height);
			return result;
		}

		public static implicit operator RDSizeI(RDSizeNI p)
		{
			RDSizeI result = new(new int?(p.Width), new int?(p.Height));
			return result;
		}

		public static implicit operator RDSizeE(RDSizeNI p)
		{
			RDSizeE result = new((float)p.Width, (float)p.Height);
			return result;
		}

		public static explicit operator RDPointNI(RDSizeNI size)
		{
			RDPointNI result = new(size.Width, size.Height);
			return result;
		}
	}
}
