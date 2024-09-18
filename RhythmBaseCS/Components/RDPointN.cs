using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>non-nullable</strong> <see langword="float" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDPointN : IEquatable<RDPointN>
	{
		public RDPointN(RDSizeN sz)
		{
			this = default;
			X = sz.Width;
			Y = sz.Height;
		}

		public RDPointN(float x, float y)
		{
			this = default;
			this.X = x;
			this.Y = y;
		}

		public float X { get; set; }

		public float Y { get; set; }

		public void Offset(RDSizeN p)
		{
			X += p.Width;
			Y += p.Height;
		}

		public void Offset(float dx, float dy)
		{
			X += dx;
			Y += dy;
		}

		public static RDPointN Add(RDPointN pt, RDSizeNI sz)
		{
			RDPointN Add = new(pt.X + (float)sz.Width, pt.Y + (float)sz.Height);
			return Add;
		}

		public static RDPointN Add(RDPointN pt, RDSizeN sz)
		{
			RDPointN Add = new(pt.X + sz.Width, pt.Y + sz.Height);
			return Add;
		}

		public static RDPointN Subtract(RDPointN pt, RDSizeNI sz)
		{
			RDPointN Subtract = new(pt.X - (float)sz.Width, pt.Y - (float)sz.Height);
			return Subtract;
		}

		public static RDPointN Subtract(RDPointN pt, RDSizeN sz)
		{
			RDPointN Subtract = new(pt.X - sz.Width, pt.Y - sz.Height);
			return Subtract;
		}

		public RDPointN MultipyByMatrix(float[,] matrix)
		{
			if (matrix.Rank == 2 && matrix.Length == 4)
			{
				RDPointN MultipyByMatrix = new(X * matrix[0, 0] + Y * matrix[1, 0], X * matrix[0, 1] + Y * matrix[1, 1]);
				return MultipyByMatrix;
			}
			throw new Exception("Matrix not match, 2*2 matrix expected.");
		}
		/// <summary>
		/// Rotate.
		/// </summary>
		public RDPointN Rotate(float angle)
		{
			float[,] array = new float[2, 2];
			array[0, 0] = (float)Math.Cos((double)angle);
			array[0, 1] = (float)Math.Sin((double)angle);
			array[1, 0] = (float)-(float)Math.Sin((double)angle);
			array[1, 1] = (float)Math.Cos((double)angle);
			return MultipyByMatrix(array);
		}
		/// <summary>
		/// Rotate at a given pivot.
		/// </summary>
		/// <param name="pivot">Giver pivot.</param>
		/// <returns></returns>
		public RDPointN Rotate(RDPointN pivot, float angle) => (this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);

		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDPointN) && Equals((obj != null) ? ((RDPointN)obj) : default);

		public override int GetHashCode()
		{
			HashCode h = default;
			h.Add(X);
			h.Add(Y);
			return h.ToHashCode();
		}

		public override string ToString() => string.Format("[{0}, {1}]", X, Y);

		bool IEquatable<RDPointN>.Equals(RDPointN other) => other.X == X && other.Y == Y;

		public static RDPointN operator +(RDPointN pt, RDSizeNI sz) => Add(pt, sz);

		public static RDPointN operator +(RDPointN pt, RDSizeN sz) => Add(pt, sz);

		public static RDPointN operator -(RDPointN pt, RDSizeNI sz) => Subtract(pt, sz);

		public static RDPointN operator -(RDPointN pt, RDSizeN sz) => Subtract(pt, sz);

		public static bool operator ==(RDPointN left, RDPointN right) => left.Equals(right);

		public static bool operator !=(RDPointN left, RDPointN right) => !left.Equals(right);

		public static implicit operator RDPoint(RDPointN p)
		{
			RDPoint result = new(new float?(p.X), new float?(p.Y));
			return result;
		}

		public static implicit operator RDPointE(RDPointN p)
		{
			RDPointE result = new(p.X, p.Y);
			return result;
		}

		public static explicit operator RDSizeN(RDPointN p)
		{
			RDSizeN result = new(p.X, p.Y);
			return result;
		}
	}
}
