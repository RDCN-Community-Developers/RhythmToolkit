using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDSizeI(int? width, int? height) : IEquatable<RDSizeI>
	{
		public RDSizeI(RDPointI pt) : this(pt.X, pt.Y) { }
		public readonly bool IsEmpty => Width == null && Height == null;
		public int? Width { get; set; } = width;
		public int? Height { get; set; } = height;
		public readonly int? Area => Width * Height;
		public static RDSizeI Add(RDSizeI sz1, RDSizeI sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		public static RDSizeI Truncate(RDSize value) => new(
			(int)Math.Round((value.Width == null) ? 0.0 : Math.Truncate((double)value.Width.Value)),
			(int)Math.Round((value.Height == null) ? 0.0 : Math.Truncate((double)value.Height.Value)));
		public static RDSizeI Subtract(RDSizeI sz1, RDSizeI sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		public static RDSizeI Ceiling(RDSize value) => new(
			(int)Math.Round((value.Width == null) ? 0.0 : Math.Ceiling((double)value.Width.Value)),
			(int)Math.Round((value.Height == null) ? 0.0 : Math.Ceiling((double)value.Height.Value)));
		public static RDSizeI Round(RDSize value) => new(
				new int?((int)Math.Round((value.Width == null) ? 0.0 : Math.Round((double)value.Width.Value))),
				new int?((int)Math.Round((value.Height == null) ? 0.0 : Math.Round((double)value.Height.Value))));
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSizeI) && Equals((obj != null) ? ((RDSizeI)obj) : default);
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
		public override readonly string ToString() => $"[{Width},{Height}]";
		public readonly bool Equals(RDSizeI other) => Width == other.Width && Height == other.Height;
		public static RDSizeI operator +(RDSizeI sz1, RDSizeI sz2) => Add(sz1, sz2);
		public static RDSizeI operator -(RDSizeI sz1, RDSizeI sz2) => Subtract(sz1, sz2);
		public static RDSize operator *(float left, RDSizeI right) => new(left * right.Width, left * right.Height);
		public static RDSize operator *(RDSizeI left, float right) => new(left.Width * right, left.Height * right);
		public static RDSizeI operator *(int left, RDSizeI right) => new(left * right.Width, left * right.Height);
		public static RDSizeI operator *(RDSizeI left, int right) => new(left.Width * right, left.Height * right);
		public static RDSize operator /(RDSizeI left, float right) => new RDSize(left.Width / right, left.Height / right);
		public static RDSizeI operator /(RDSizeI left, int right) => new RDSizeI(left.Width / right, left.Height / right);
		public static bool operator ==(RDSizeI sz1, RDSizeI sz2) => sz1.Equals(sz2);
		public static bool operator !=(RDSizeI sz1, RDSizeI sz2) => !sz1.Equals(sz2);

		public static implicit operator RDSize(RDSizeI p) => new(p.Width, p.Height);

		public static implicit operator RDSizeE(RDSizeI p) => new(p.Width, p.Height);

		public static explicit operator RDPointI(RDSizeI size) => new(size.Width, size.Height);
	}
}
