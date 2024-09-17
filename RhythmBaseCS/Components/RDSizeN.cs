using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="float" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDSizeN : IEquatable<RDSizeN>
	{
		public RDSizeN(RDPointN pt)
		{
			this = default;
			Width = pt.X;
			Height = pt.Y;
		}

		public RDSizeN(float width, float height)
		{
			this = default;
			this.Width = width;
			this.Height = height;
		}

		public float Width { get; set; }

		public float Height { get; set; }

		public float Area
		{
			get
			{
				return Width * Height;
			}
		}

		public static RDSizeN Add(RDSizeN sz1, RDSizeN sz2)
		{
			RDSizeN Add = new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
			return Add;
		}

		public static RDSizeN Subtract(RDSizeN sz1, RDSizeN sz2)
		{
			RDSizeN Subtract = new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
			return Subtract;
		}

		public override int GetHashCode()
		{
			HashCode h = default;
			h.Add(Width);
			h.Add(Height);
			return h.ToHashCode();
		}

		public override string ToString() => string.Format("[{0}, {1}]", Width, Height);

		public bool Equals(RDSizeN other) => Width == other.Width && Height == other.Height;

		public RDSizeNI ToSize()
		{
			RDSizeNI ToSize = checked(new RDSizeNI((int)Math.Round((double)Width), (int)Math.Round((double)Height)));
			return ToSize;
		}

		public RDPointN ToPointF()
		{
			RDPointN ToPointF = new(Width, Height);
			return ToPointF;
		}

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSizeN) && Equals((obj != null) ? ((RDSizeN)obj) : default);

		public static RDSizeN operator +(RDSizeN sz1, RDSizeN sz2) => Add(sz1, sz2);

		public static RDSizeN operator -(RDSizeN sz1, RDSizeN sz2) => Subtract(sz1, sz2);

		public static RDSizeN operator *(float left, RDSizeN right)
		{
			RDSizeN result = new(left * right.Width, left * right.Height);
			return result;
		}

		public static RDSizeN operator *(RDSizeN left, float right)
		{
			RDSizeN result = new(left.Width * right, left.Height * right);
			return result;
		}

		public static RDSizeN operator /(RDSizeN left, float right)
		{
			RDSizeN result = new(left.Width / right, left.Height / right);
			return result;
		}

		public static bool operator ==(RDSizeN sz1, RDSizeN sz2) => sz1.Equals(sz2);

		public static bool operator !=(RDSizeN sz1, RDSizeN sz2) => !sz1.Equals(sz2);

		public static implicit operator RDSize(RDSizeN size)
		{
			RDSize result = new(new float?(size.Width), new float?(size.Height));
			return result;
		}

		public static implicit operator RDSizeE(RDSizeN size)
		{
			RDSizeE result = new(size.Width, size.Height);
			return result;
		}

		public static explicit operator RDPointN(RDSizeN size)
		{
			RDPointN result = new(size.Width, size.Height);
			return result;
		}
	}
}
