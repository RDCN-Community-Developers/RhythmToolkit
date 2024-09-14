using System;
using System.Diagnostics.CodeAnalysis;

namespace RhythmBase.Components
{

	public struct RDRotatedRectNI : IEquatable<RDRotatedRectNI>
	{

		public RDPointNI Location { get; set; }


		public RDSizeNI Size { get; set; }


		public RDPointNI Pivot { get; set; }

		/// <summary>
		/// Radius angle value
		/// </summary>
		/// <returns></returns>

		public float Angle { get; set; }


		public RDPointN LeftTop
		{
			get
			{
				return (Location - (RDSizeNI)Pivot + new RDSizeNI(0, Size.Height)).Rotate(Location, Angle);
			}
		}


		public RDPointN RightTop
		{
			get
			{
				return (Location - (RDSizeNI)Pivot + Size).Rotate(Location, Angle);
			}
		}


		public RDPointN LeftBottom
		{
			get
			{
				return (Location - (RDSizeNI)Pivot).Rotate(Location, Angle);
			}
		}


		public RDPointN RightBottom
		{
			get
			{
				return (Location - (RDSizeNI)Pivot + new RDSizeNI(Size.Width, 0)).Rotate(Location, Angle);
			}
		}


		public RDRectNI WithoutRotate
		{
			get
			{
				RDRectNI WithoutRotate = new(Location - (RDSizeNI)Pivot, Size);
				return WithoutRotate;
			}
		}


		public RDRotatedRectNI(RDPointNI location, RDSizeNI size, RDPointNI pivot, float angle)
		{
			this = default;
			Location = location;
			Size = size;
			Pivot = pivot;
			Angle = angle;
		}


		public RDRotatedRectNI(RDRectNI rect)
		{
			this = new RDRotatedRectNI(rect.Location, rect.Size, default, 0f);
		}


		public static RDRotatedRectNI Inflate(RDRotatedRectNI rect, RDSizeNI size)
		{
			RDRotatedRectNI result = rect;
			result.Inflate(size);
			return result;
		}


		public static RDRotatedRectNI Inflate(RDRotatedRectNI rect, int x, int y)
		{
			RDRotatedRectNI result = rect;
			result.Inflate(x, y);
			return result;
		}


		public void Offset(int x, int y) => Location += (RDSizeNI)new RDPointNI(x, y);


		public void Offset(RDPointNI p) => Offset(p.X, p.Y);


		public void Inflate(RDSizeNI size)
		{
			Size += checked(new RDSizeNI(size.Width * 2, size.Height * 2));
			Pivot -= (RDSizeNI)new RDPointNI(size.Width, size.Height);
		}


		public void Inflate(int width, int height)
		{
			Size += checked(new RDSizeNI(width * 2, height * 2));
			Pivot -= (RDSizeNI)new RDPointNI(width, height);
		}


		public bool Contains(int x, int y) => WithoutRotate.Contains(new RDPointN((float)x, (float)y).Rotate(-Angle));


		public bool Contains(RDPointN p) => WithoutRotate.Contains(p.Rotate(-Angle));


		public bool Contains(RDRotatedRectNI rect) => Contains(rect.LeftTop) && Contains(rect.RightTop) && Contains(rect.LeftBottom) && Contains(rect.RightBottom);


		public bool IntersectsWith(RDRotatedRectNI rect) => Contains(rect.LeftTop) || Contains(rect.RightTop) || Contains(rect.LeftBottom) || Contains(rect.RightBottom);


		public static bool operator ==(RDRotatedRectNI rect1, RDRotatedRectNI rect2) => rect1.Equals(rect2);


		public static bool operator !=(RDRotatedRectNI rect1, RDRotatedRectNI rect2) => !rect1.Equals(rect2);


		public override bool Equals([NotNullWhen(true)] object obj) => obj.GetType() == typeof(RDRotatedRectNI) && Equals((obj != null) ? ((RDRotatedRectNI)obj) : default);


		public override int GetHashCode() => HashCode.Combine(Location, Size, Pivot, Angle);


		public override string ToString() => string.Format("{{Location={0},Size={1},Pivot={2},Angle={3}}}",
			[
				Location,
				Size,
				Pivot,
				Angle
			]);


		public bool Equals(RDRotatedRectNI other) => Location == other.Location && Size == other.Size && Pivot == other.Pivot && Angle == other.Angle;
	}
}
