using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="float" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDSize(float? width, float? height) : IEquatable<RDSize>
	{
		public RDSize(RDPoint pt) : this(pt.X, pt.Y) { }
		public readonly bool IsEmpty => Width == null && Height == null;
		public float? Width { get; set; } = width;
		public float? Height { get; set; } = height;
		public readonly float? Area => Width * Height;
		public static RDSize Add(RDSize sz1, RDSize sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		public static RDSize Subtract(RDSize sz1, RDSize sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
		public override readonly string ToString() => $"[{Width},{Height}]";
		public readonly bool Equals(RDSize other) => Width == other.Width && Height == other.Height;
		public readonly RDSizeI ToSize() => new((int?)Width, (int?)Height);
		public readonly RDPoint ToPointF() => new(Width, Height);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSize) && Equals((obj != null) ? ((RDSize)obj) : default);
		public static RDSize operator +(RDSize sz1, RDSize sz2) => Add(sz1, sz2);
		public static RDSize operator -(RDSize sz1, RDSize sz2) => Subtract(sz1, sz2);
		public static RDSize operator *(float left, RDSize right) => new(left * right.Width, left * right.Height);
		public static RDSize operator *(RDSize left, float right) => new(left.Width * right, left.Height * right);
		public static RDSize operator /(RDSize left, float right) => new(left.Width / right, left.Height / right);
		public static bool operator ==(RDSize sz1, RDSize sz2) => sz1.Equals(sz2);
		public static bool operator !=(RDSize sz1, RDSize sz2) => !sz1.Equals(sz2);

		public static implicit operator RDSizeE(RDSize size) => new(size.Width, size.Height);

		public static explicit operator RDPoint(RDSize size) => new(size.Width, size.Height);
	}
}
