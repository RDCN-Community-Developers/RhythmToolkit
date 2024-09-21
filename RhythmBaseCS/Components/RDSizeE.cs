using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>nullable</strong> <seealso cref="T:RhythmBase.Components.Expression" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDSizeE(Expression? width, Expression? height) : IEquatable<RDSizeE>
	{
		public RDSizeE(float width, float height) : this((Expression)width, (Expression)height) { }
		public RDSizeE(Expression? width, float height) : this(width, (Expression)height) { }
		public RDSizeE(float width, Expression? height) : this((Expression)width, height) { }
		public RDSizeE(string width, float height) : this((Expression)width, (Expression)height) { }
		public RDSizeE(float width, string height) : this((Expression)width, (Expression)height) { }
		public RDSizeE(string width, string height) : this((Expression)width, (Expression)height) { }
		public RDSizeE(string width, Expression? height) : this((Expression)width, height) { }
		public RDSizeE(Expression? width, string height) : this(width, (Expression)height) { }
		public RDSizeE(RDSizeI p) : this((Expression?)p.Width, (Expression?)p.Height) { }
		public RDSizeE(RDSize p):this((Expression?)p.Width, (Expression?)p.Height) { }
		public RDSizeE(RDPointI p):this((Expression?)p.X, (Expression?)p.Y) { }
		public RDSizeE(RDPoint p):this((Expression?)p.X,(Expression?)p.Y) { }
		public readonly bool IsEmpty => Width == null && Height == null;
		public Expression? Width { get; set; } = width;
		public Expression? Height { get; set; }=height;
		public readonly Expression? Area => Width * Height;
		public static RDSizeE Add(RDSizeE sz1, RDSize sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		public static RDSizeE Add(RDSizeE sz1, RDSizeE sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
		public static RDSizeE Subtract(RDSizeE sz1, RDSize sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		public static RDSizeE Subtract(RDSizeE sz1, RDSizeE sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
		public override readonly string ToString() => $"[{Width},{Height}]";
		public readonly bool Equals(RDSizeE other) => Width == other.Width && Height == other.Height;
		public readonly RDPointE ToRDPointE() => new(Width, Height);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDSize) && Equals((obj != null) ? ((RDSize)obj) : default);
		public static RDSizeE operator +(RDSizeE sz1, RDSizeI sz2) => Add(sz1, sz2);
		public static RDSizeE operator +(RDSizeE sz1, RDSize sz2) => Add(sz1, sz2);
		public static RDSizeE operator +(RDSizeE sz1, RDSizeE sz2) => Add(sz1, sz2);
		public static RDSizeE operator -(RDSizeE sz1, RDSizeI sz2) => Subtract(sz1, sz2);
		public static RDSizeE operator -(RDSizeE sz1, RDSize sz2) => Subtract(sz1, sz2);
		public static RDSizeE operator -(RDSizeE sz1, RDSizeE sz2) => Subtract(sz1, sz2);
		public static RDSizeE operator *(int left, RDSizeE right) => new(left * right.Width, left * right.Height);
		public static RDSizeE operator *(RDSizeE left, int right) => new(left.Width * right, left.Height * right);
		public static RDSizeE operator *(float left, RDSizeE right) => new(left * right.Width, left * right.Height);
		public static RDSizeE operator *(RDSizeE left, float right) => new(left.Width * right, left.Height * right);
		public static RDSizeE operator *(Expression left, RDSizeE right) => new(left * right.Width, left * right.Height);
		public static RDSizeE operator *(RDSizeE left, Expression right) => new(left.Width * right, left.Height * right);
		public static RDSizeE operator /(RDSizeE left, float right) => new(left.Width / right, left.Height / right);
		public static RDSizeE operator /(RDSizeE left, Expression right) => new(left.Width / right, left.Height / right);
		public static bool operator ==(RDSizeE sz1, RDSizeE sz2) => sz1.Equals(sz2);
		public static bool operator !=(RDSizeE sz1, RDSizeE sz2) => !sz1.Equals(sz2);

		public static explicit operator RDPointE(RDSizeE size) => new(size.Width, size.Height);
	}
}
