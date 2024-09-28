using RhythmBase.Components;

namespace RhythmBase.Utils
{
	public static class VisualUtils
	{       /// <summary>
			/// Converts percentage point to pixel point with default screen size (352 * 198).
			/// </summary>
		public static RDPointE PercentToPixel(RDPointE point) => PercentToPixel(point, RDSizeNI.Screen);
		/// <summary>
		/// Converts percentage point to pixel point with specified size.
		/// </summary>
		/// <param name="size">Specified size.</param>
		public static RDPointE PercentToPixel(RDPointE point, RDSizeE size)
		{
			RDPointE PercentToPixel = new(point.X * size.Width / 100f, point.Y * size.Height / 100f);
			return PercentToPixel;
		}
		/// <summary>
		/// Converts pixel point to percentage point with default screen size (352 * 198).
		/// </summary>
		public static (float? X, float? Y) PixelToPercent((float X, float Y) point) => PixelToPercent(point, (352f, 198f));
		/// <summary>
		/// Converts pixel point to percentage point with specified size.
		/// </summary>
		/// <param name="size">Specified size.</param>
		public static (float? X, float? Y) PixelToPercent((float? X, float? Y) point, (float? X, float? Y) size)
		{
			(float? X, float? Y) PixelToPercent = (point.X * 100f / size.X, point.Y * 100f / size.Y);
			return PixelToPercent;
		}
		public static RDPointN RoomPerspectiveTranslate(this RDPointN p, RDRectN rect)
		{
			RDPointN plb = rect.LeftBottom;
			RDPointN prb = rect.RightBottom;
			RDPointN plt = rect.LeftTop;
			RDPointN prt = rect.RightTop;
			p = new(p.X, p.Y);
			RDPointN p1p2 = prb - (RDSizeN)plb;
			RDPointN p1p4 = plt - (RDSizeN)plb;
			RDPointN p1p3 = prt - (RDSizeN)plb;
			RDPointN pr = plb + new RDSizeN(p1p2.X * p.X, p1p2.Y * p.X) +
				new RDSizeN((p1p3.X - p1p2.X - p1p4.X) * p.X * p.Y, (p1p3.Y - p1p2.Y - p1p4.Y) * p.X * p.Y) +
				new RDSizeN(p1p4.X * p.Y, p1p4.Y * p.Y);
			return pr;
		}
		public static float DegreeToRadius(float degree) => 3.1415927f * degree / 180f;
		public static float RadiusToDegree(float radius) => radius * 180f / 3.1415927f;
	}
}
