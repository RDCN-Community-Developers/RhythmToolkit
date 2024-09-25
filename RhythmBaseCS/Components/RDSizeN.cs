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
	public struct RDSizeN(float width, float height) : IEquatable<RDSizeN>
	{
		public RDSizeN(RDPointN pt):this(pt.X, pt.Y) { }
		public float Width { get; set; }=width;
		public float Height { get; set; }=height;
		public readonly float Area => Width * Height;
		public static RDSizeN Add(RDSizeN sz1, RDSizeN sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		public static RDSizeN Subtract(RDSizeN sz1, RDSizeN sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
		public override readonly string ToString() => $"[{Width},{Height}]";
		public readonly bool Equals(RDSizeN other) => Width == other.Width && Height == other.Height;
		public readonly RDSizeNI ToSize() => new((int)Math.Round((double)Width), (int)Math.Round((double)Height));
		public readonly RDPointN ToPointF() => new(Width, Height);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSizeN) && Equals((obj != null) ? ((RDSizeN)obj) : default);
		public static RDSizeN operator +(RDSizeN sz1, RDSizeN sz2) => Add(sz1, sz2);
		public static RDSizeN operator -(RDSizeN sz1, RDSizeN sz2) => Subtract(sz1, sz2);
		public static RDSizeN operator *(float left, RDSizeN right) => new(left * right.Width, left * right.Height);
		public static RDSizeN operator *(RDSizeN left, float right) => new(left.Width * right, left.Height * right);
		public static RDSizeN operator /(RDSizeN left, float right) => new(left.Width / right, left.Height / right);
		public static bool operator ==(RDSizeN sz1, RDSizeN sz2) => sz1.Equals(sz2);
		public static bool operator !=(RDSizeN sz1, RDSizeN sz2) => !sz1.Equals(sz2);
		public static implicit operator RDSize(RDSizeN size) => new(new float?(size.Width), new float?(size.Height));
		public static implicit operator RDSizeE(RDSizeN size) => new(size.Width, size.Height);
		public static explicit operator RDPointN(RDSizeN size) => new(size.Width, size.Height);
	}
}
