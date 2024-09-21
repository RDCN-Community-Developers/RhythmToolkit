using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDSizeNI(int width, int height) : IEquatable<RDSizeNI>
	{
		public RDSizeNI(RDPointNI pt):this(pt.X, pt.Y) { }
		public int Width { get; set; } = width;
		public int Height { get; set; } = height;
		public readonly int Area => Width * Height;
		public static RDSizeNI Screen => new(352, 198);
		public static RDSizeNI Add(RDSizeNI sz1, RDSizeNI sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		public static RDSizeNI Truncate(RDSizeN value) => new((int)value.Width, (int)value.Height);
		public static RDSizeNI Subtract(RDSizeNI sz1, RDSizeNI sz2) => new RDSizeNI(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		public static RDSizeNI Ceiling(RDSizeN value) => new(
			(int)Math.Ceiling((double)value.Width),
			(int)Math.Ceiling((double)value.Height));
		public static RDSizeNI Round(RDSizeN value) => new(
			(int)Math.Round((double)value.Width),
			(int)Math.Round((double)value.Height));
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSizeNI) && Equals((obj != null) ? ((RDSizeNI)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
		public override readonly string ToString() => $"[{Width},{Height}]";
		public readonly bool Equals(RDSizeNI other) => Width == other.Width && Height == other.Height;
		public static RDSizeNI operator +(RDSizeNI sz1, RDSizeNI sz2) => Add(sz1, sz2);
		public static RDSizeNI operator -(RDSizeNI sz1, RDSizeNI sz2) => Subtract(sz1, sz2);
		public static RDSizeN operator *(float left, RDSizeNI right) => new(left * right.Width, left * right.Height);
		public static RDSizeN operator *(RDSizeNI left, float right) => new(left.Width * right, left.Height * right);
		public static RDSizeNI operator *(int left, RDSizeNI right) => new(left * right.Width, left * right.Height);
		public static RDSizeNI operator *(RDSizeNI left, int right) => new(left.Width * right, left.Height * right);
		public static RDSizeN operator /(RDSizeNI left, float right) => new(left.Width / right, left.Height / right);
		public static RDSizeNI operator /(RDSizeNI left, int right) => new RDSizeNI(
			(int)Math.Round(left.Width / (double)right),
			(int)Math.Round(left.Height / (double)right));
		public static bool operator ==(RDSizeNI sz1, RDSizeNI sz2) => sz1.Equals(sz2);
		public static bool operator !=(RDSizeNI sz1, RDSizeNI sz2) => !sz1.Equals(sz2);

		public static implicit operator RDSizeN(RDSizeNI p) => new(p.Width, p.Height);

		public static implicit operator RDSizeI(RDSizeNI p) => new(p.Width, p.Height);

		public static implicit operator RDSizeE(RDSizeNI p) => new(p.Width, p.Height);

		public static explicit operator RDPointNI(RDSizeNI size) => new(size.Width, size.Height);
	}
}
