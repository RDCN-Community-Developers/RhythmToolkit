using Newtonsoft.Json;
using RhythmBase.Converters;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
namespace RhythmBase.Components
{
	/// <summary>
	/// A size whose horizontal and vertical coordinates are <strong>nullable</strong> <seealso cref="T:RhythmBase.Components.Expression" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public struct RDSizeE(RDExpression? width, RDExpression? height) :
		IRDVortex<RDSizeE, RDSizeI, RDExpression>,
		IRDVortex<RDSizeE, RDSize, RDExpression>,
		IRDVortex<RDSizeE, RDSizeE, RDExpression>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct with specified width and height as floats.
		/// </summary>
		/// <param name="width">The width as a float.</param>
		/// <param name="height">The height as a float.</param>
		public RDSizeE(float width, float height) : this((RDExpression)width, (RDExpression)height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct with specified width as an RDExpression and height as a float.
		/// </summary>
		/// <param name="width">The width as an RDExpression.</param>
		/// <param name="height">The height as a float.</param>
		public RDSizeE(RDExpression? width, float height) : this(width, (RDExpression)height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct with specified width as a float and height as an RDExpression.
		/// </summary>
		/// <param name="width">The width as a float.</param>
		/// <param name="height">The height as an RDExpression.</param>
		public RDSizeE(float width, RDExpression? height) : this((RDExpression)width, height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct with specified width as a string and height as a float.
		/// </summary>
		/// <param name="width">The width as a string.</param>
		/// <param name="height">The height as a float.</param>
		public RDSizeE(string width, float height) : this((RDExpression)width, (RDExpression)height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct with specified width as a float and height as a string.
		/// </summary>
		/// <param name="width">The width as a float.</param>
		/// <param name="height">The height as a string.</param>
		public RDSizeE(float width, string height) : this((RDExpression)width, (RDExpression)height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct with specified width and height as strings.
		/// </summary>
		/// <param name="width">The width as a string.</param>
		/// <param name="height">The height as a string.</param>
		public RDSizeE(string width, string height) : this((RDExpression)width, (RDExpression)height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct with specified width as a string and height as an RDExpression.
		/// </summary>
		/// <param name="width">The width as a string.</param>
		/// <param name="height">The height as an RDExpression.</param>
		public RDSizeE(string width, RDExpression? height) : this((RDExpression)width, height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct with specified width as an RDExpression and height as a string.
		/// </summary>
		/// <param name="width">The width as an RDExpression.</param>
		/// <param name="height">The height as a string.</param>
		public RDSizeE(RDExpression? width, string height) : this(width, (RDExpression)height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct from an <see cref="RDSizeI"/> instance.
		/// </summary>
		/// <param name="p">The <see cref="RDSizeI"/> instance.</param>
		public RDSizeE(RDSizeI p) : this((RDExpression?)p.Width, (RDExpression?)p.Height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct from an <see cref="RDSize"/> instance.
		/// </summary>
		/// <param name="p">The <see cref="RDSize"/> instance.</param>
		public RDSizeE(RDSize p) : this((RDExpression?)p.Width, (RDExpression?)p.Height) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct from an <see cref="RDPointI"/> instance.
		/// </summary>
		/// <param name="p">The <see cref="RDPointI"/> instance.</param>
		public RDSizeE(RDPointI p) : this((RDExpression?)p.X, (RDExpression?)p.Y) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct from an <see cref="RDPoint"/> instance.
		/// </summary>
		/// <param name="p">The <see cref="RDPoint"/> instance.</param>
		public RDSizeE(RDPoint p) : this((RDExpression?)p.X, (RDExpression?)p.Y) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RDSizeE"/> struct from an <see cref="RDPointE"/> instance.
		/// </summary>
		/// <param name="p">The <see cref="RDPointE"/> instance.</param>
		public RDSizeE(RDPointE p) : this(p.X, p.Y) { }

		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		public readonly bool IsEmpty => Width == null && Height == null;

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		public RDExpression? Width { get; set; } = width;

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		public RDExpression? Height { get; set; } = height;

		/// <summary>
		/// Gets the area of the size.
		/// </summary>
		public readonly RDExpression? Area => Width * Height;

		/// <summary>
		/// Adds two <see cref="RDSizeE"/> instances and returns the result.
		/// </summary>
		/// <param name="sz1">The first <see cref="RDSizeE"/> instance.</param>
		/// <param name="sz2">The second <see cref="RDSize"/> instance.</param>
		/// <returns>The result of the addition.</returns>
		public static RDSizeE Add(RDSizeE sz1, RDSize sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);

		/// <summary>
		/// Adds two <see cref="RDSizeE"/> instances and returns the result.
		/// </summary>
		/// <param name="sz1">The first <see cref="RDSizeE"/> instance.</param>
		/// <param name="sz2">The second <see cref="RDSizeE"/> instance.</param>
		/// <returns>The result of the addition.</returns>
		public static RDSizeE Add(RDSizeE sz1, RDSizeE sz2) => new(sz1.Width + sz2.Width, sz1.Height + sz2.Height);

		/// <summary>
		/// Subtracts one <see cref="RDSize"/> instance from another <see cref="RDSizeE"/> instance and returns the result.
		/// </summary>
		/// <param name="sz1">The first <see cref="RDSizeE"/> instance.</param>
		/// <param name="sz2">The second <see cref="RDSize"/> instance.</param>
		/// <returns>The result of the subtraction.</returns>
		public static RDSizeE Subtract(RDSizeE sz1, RDSize sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);

		/// <summary>
		/// Subtracts one <see cref="RDSizeE"/> instance from another <see cref="RDSizeE"/> instance and returns the result.
		/// </summary>
		/// <param name="sz1">The first <see cref="RDSizeE"/> instance.</param>
		/// <param name="sz2">The second <see cref="RDSizeE"/> instance.</param>
		/// <returns>The result of the subtraction.</returns>
		public static RDSizeE Subtract(RDSizeE sz1, RDSizeE sz2) => new(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
		/// <inheritdoc/>
		public override readonly int GetHashCode() => HashCode.Combine(Width, Height);
		/// <inheritdoc/>
		public override readonly string ToString() => $"[{Width},{Height}]";
		/// <inheritdoc/>
		public readonly bool Equals(RDSizeE other) => Width == other.Width && Height == other.Height;
		/// <inheritdoc/>
		public readonly RDPointE ToRDPointE() => new(Width, Height);
		/// <inheritdoc/>
		public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RDSize e && Equals(e);
		/// <inheritdoc/>
		public static RDSizeE operator +(RDSizeE sz1, RDSizeI sz2) => Add(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeE operator +(RDSizeE sz1, RDSize sz2) => Add(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeE operator +(RDSizeE sz1, RDSizeE sz2) => Add(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeE operator -(RDSizeE sz1, RDSizeI sz2) => Subtract(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeE operator -(RDSizeE sz1, RDSize sz2) => Subtract(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeE operator -(RDSizeE sz1, RDSizeE sz2) => Subtract(sz1, sz2);
		/// <inheritdoc/>
		public static RDSizeE operator *(int left, RDSizeE right) => new(left * right.Width, left * right.Height);
		/// <inheritdoc/>
		public static RDSizeE operator *(RDSizeE left, int right) => new(left.Width * right, left.Height * right);
		/// <inheritdoc/>
		public static RDSizeE operator *(float left, RDSizeE right) => new(left * right.Width, left * right.Height);
		/// <inheritdoc/>
		public static RDSizeE operator *(RDSizeE left, float right) => new(left.Width * right, left.Height * right);
		/// <inheritdoc/>
		public static RDSizeE operator *(RDExpression left, RDSizeE right) => new(left * right.Width, left * right.Height);
		/// <inheritdoc/>
		public static RDSizeE operator *(RDSizeE left, RDExpression right) => new(left.Width * right, left.Height * right);
		/// <inheritdoc/>
		public static RDSizeE operator /(RDSizeE left, float right) => new(left.Width / right, left.Height / right);
		/// <inheritdoc/>
		public static RDSizeE operator /(RDSizeE left, RDExpression right) => new(left.Width / right, left.Height / right);
		/// <inheritdoc/>
		public static bool operator ==(RDSizeE sz1, RDSizeE sz2) => sz1.Equals(sz2);
		/// <inheritdoc/>
		public static bool operator !=(RDSizeE sz1, RDSizeE sz2) => !sz1.Equals(sz2);
		/// <summary>
		/// Converts an <see cref="RDSizeE"/> instance to an <see cref="RDPointE"/> instance explicitly.
		/// </summary>
		/// <param name="size">The <see cref="RDSizeE"/> instance to convert.</param>
		/// <returns>An <see cref="RDPointE"/> instance with the same width and height as the <see cref="RDSizeE"/> instance.</returns>
		public static explicit operator RDPointE(RDSizeE size) => new(size.Width, size.Height);
		private readonly string GetDebuggerDisplay() => ToString();
	}
}
