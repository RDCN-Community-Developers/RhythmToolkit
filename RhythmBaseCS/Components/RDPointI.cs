using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using RhythmBase.Converters;
namespace RhythmBase.Components
{
	/// <summary>
	/// A point whose horizontal and vertical coordinates are <strong>nullable</strong> <see langword="integer" />
	/// </summary>
	[JsonConverter(typeof(RDPointsConverter))]
	public struct RDPointI : IEquatable<RDPointI>
	{
		public RDPointI(RDSizeI sz)
		{
			this = default;
			X = sz.Width;
			Y = sz.Height;
		}
		public RDPointI(RDSizeN sz)
		{
			this = default;
			X = new int?((int)Math.Round((double)sz.Width));
			Y = new int?((int)Math.Round((double)sz.Height));
		}
		public RDPointI(int? x, int? y)
		{
			this = default;
			this.X = x;
			this.Y = y;
		}
		public readonly bool IsEmpty => X == null && Y == null;
		public int? X { get; set; }
		public int? Y { get; set; }
		public void Offset(RDPointI p)
		{
			X += p.X;
			Y += p.Y;
		}
		public void Offset(int? dx, int? dy)
		{
			X += dx;
			Y += dy;
		}
		public static RDPointI Ceiling(RDPoint value)
		{
			RDPointI Ceiling = checked(new RDPointI(new int?((int)Math.Round((value.X == null) ? 0.0 : Math.Ceiling((double)value.X.Value))), new int?((int)Math.Round((value.Y == null) ? 0.0 : Math.Ceiling((double)value.Y.Value)))));
			return Ceiling;
		}
		public static RDPointI Add(RDPointI pt, RDSizeI sz)
		{
			RDPointI Add = checked(new RDPointI(pt.X + sz.Width, pt.Y + sz.Height));
			return Add;
		}
		public static RDPointI Truncate(RDPoint value)
		{
			RDPointI Truncate = checked(new RDPointI(new int?((int)Math.Round((value.X == null) ? 0.0 : Math.Truncate((double)value.X.Value))), new int?((int)Math.Round((value.Y == null) ? 0.0 : Math.Truncate((double)value.Y.Value)))));
			return Truncate;
		}
		public static RDPointI Subtract(RDPointI pt, RDSizeI sz)
		{
			RDPointI Subtract = checked(new RDPointI(pt.X - sz.Width, pt.Y - sz.Height));
			return Subtract;
		}
		public static RDPointI Round(RDPoint value)
		{
			RDPointI Round = checked(new RDPointI(new int?((int)Math.Round((value.X == null) ? 0.0 : Math.Truncate((double)value.X.Value))), new int?((int)Math.Round((value.Y == null) ? 0.0 : Math.Truncate((double)value.Y.Value)))));
			return Round;
		}
		public RDPoint MultipyByMatrix(float[,] matrix)
		{
			if (matrix.Rank == 2 && matrix.Length == 4)
			{
				int? num = X;
				float? num2 = ((num != null) ? new float?((float)num.GetValueOrDefault()) : null) * matrix[0, 0];
				num = Y;
				float? x = num2 + ((num != null) ? new float?((float)num.GetValueOrDefault()) : null) * matrix[1, 0];
				num = X;
				float? num3 = ((num != null) ? new float?((float)num.GetValueOrDefault()) : null) * matrix[0, 1];
				num = Y;
				RDPoint MultipyByMatrix = new(x, num3 + ((num != null) ? new float?((float)num.GetValueOrDefault()) : null) * matrix[1, 1]);
				return MultipyByMatrix;
			}
			throw new Exception("Matrix not match, 2*2 matrix expected.");
		}
		/// <summary>
		/// Rotate.
		/// </summary>
		public RDPoint Rotate(float angle)
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
		public RDPoint Rotate(RDPointN pivot, float angle) => ((RDPoint)this - new RDSizeN(pivot)).Rotate(angle) + new RDSizeN(pivot);
		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDPointI) && Equals((obj != null) ? ((RDPointI)obj) : default);
		public override int GetHashCode()
		{
			HashCode h = default;
			h.Add(X);
			h.Add(Y);
			return h.ToHashCode();
		}
		public override string ToString()
		{
			string format = "[{0}, {1}]";
			int? num2;
			int? num = num2 = X;
			object objectValue = RuntimeHelpers.GetObjectValue((num2 != null) ? num.GetValueOrDefault() : "null");
			num = num2 = Y;
			return string.Format(format, objectValue, RuntimeHelpers.GetObjectValue((num2 != null) ? num.GetValueOrDefault() : "null"));
		}
		bool IEquatable<RDPointI>.Equals(RDPointI other)
		{
			int? num = other.X;
			int? num2 = X;
			bool? flag2;
			bool? flag = flag2 = (num != null & num2 != null) ? new bool?(num.GetValueOrDefault() == num2.GetValueOrDefault()) : null;
			bool? flag3;
			bool? flag4;
			if (flag2 == null || flag.GetValueOrDefault())
			{
				num2 = other.Y;
				num = Y;
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
		public static RDPointI operator +(RDPointI pt, RDSizeI sz) => Add(pt, sz);
		public static RDPointI operator -(RDPointI pt, RDSizeI sz) => Subtract(pt, sz);
		public static bool operator ==(RDPointI left, RDPointI right) => left.Equals(right);
		public static bool operator !=(RDPointI left, RDPointI right) => !left.Equals(right);
		public static implicit operator RDPoint(RDPointI p)
		{
			int? num = p.X;
			float? x = (num != null) ? new float?((float)num.GetValueOrDefault()) : null;
			num = p.Y;
			RDPoint result = new(x, (num != null) ? new float?((float)num.GetValueOrDefault()) : null);
			return result;
		}
		public static implicit operator RDPointE(RDPointI p)
		{
			int? num2;
			int? num = num2 = p.X;
			Expression? x = (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null;
			num = num2 = p.Y;
			RDPointE result = new(x, (num2 != null) ? new Expression?((float)num.GetValueOrDefault()) : null);
			return result;
		}
		public static explicit operator RDSizeI(RDPointI p)
		{
			RDSizeI result = new(p.X, p.Y);
			return result;
		}
	}
}
