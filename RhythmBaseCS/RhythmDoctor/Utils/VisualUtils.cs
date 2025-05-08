using RhythmBase.Global.Components;

namespace RhythmBase.RhythmDoctor.Utils
{
	/// <summary>
	/// Visual utils.
	/// </summary>
	public static class VisualUtils
	{
		/// <summary>
		/// Converts percentage point to pixel point with default screen size (352 * 198).
		/// </summary>
		public static RDPointE PercentToPixel(RDPointE point) => PercentToPixel(point, RDSizeNI.Screen);
		/// <summary>
		/// Converts percentage point to pixel point with specified size.
		/// </summary>
		/// <param name="point">The percentage point.</param>
		/// <param name="size">Specified size.</param>
		/// <returns>The pixel point.</returns>
		public static RDPointE PercentToPixel(RDPointE point, RDSizeE size)
		{
			RDPointE PercentToPixel = new(point.X * size.Width / 100f, point.Y * size.Height / 100f);
			return PercentToPixel;
		}
		/// <summary>
		/// Converts pixel point to percentage point with default screen size (352 * 198).
		/// </summary>
		/// <param name="point">The pixel point.</param>
		/// <returns>The percentage point.</returns>
		public static (float? X, float? Y) PixelToPercent((float X, float Y) point) => PixelToPercent(point, (352f, 198f));
		/// <summary>
		/// Converts pixel point to percentage point with specified size.
		/// </summary>
		/// <param name="point">The pixel point.</param>
		/// <param name="size">Specified size.</param>
		/// <returns>The percentage point.</returns>
		public static (float? X, float? Y) PixelToPercent((float? X, float? Y) point, (float? X, float? Y) size)
		{
			(float? X, float? Y) PixelToPercent = (point.X * 100f / size.X, point.Y * 100f / size.Y);
			return PixelToPercent;
		}
		/// <summary>
		/// Translates a point in room perspective.
		/// </summary>
		/// <param name="p">The point to translate.</param>
		/// <param name="rect">The rectangle defining the room perspective.</param>
		/// <returns>The translated point.</returns>
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
		/// <summary>
		/// Converts degrees to radians.
		/// </summary>
		/// <param name="degree">The angle in degrees.</param>
		/// <returns>The angle in radians.</returns>
		public static float DegreeToRadius(float degree) => float.Pi * degree / 180f;
		/// <summary>
		/// Converts radians to degrees.
		/// </summary>
		/// <param name="radius">The angle in radians.</param>
		/// <returns>The angle in degrees.</returns>
		public static float RadiusToDegree(float radius) => radius * 180f / float.Pi;
	}
}
