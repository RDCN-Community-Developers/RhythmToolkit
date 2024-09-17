using Microsoft.VisualBasic.CompilerServices;
using SkiaSharp;
namespace RhythmBase.Extensions
{
	[StandardModule]
	public static class ImageExtension
	{
		public static SKBitmap FromFile(string path)
		{
			SKBitmap FromFile;
			using (FileStream stream = new FileInfo(path).OpenRead())
				FromFile = SKBitmap.Decode(stream);
			return FromFile;
		}
		/// <summary>
		/// Save the image to a file path.
		/// </summary>
		/// <param name="image"></param>
		/// <param name="path"></param>
		public static void Save(this SKBitmap image, string path)
		{
			using FileStream stream = new FileInfo(path).OpenWrite();
			image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);
		}
		public static bool TryDecode(ref SKBitmap image, string path)
		{
			try
			{
				image = SKBitmap.Decode(path);
			}
			catch
			{
				return false;
			}
			return true;
		}
		public static SKBitmap Copy(this SKBitmap image, SKRectI rect)
		{
			SKBitmap result = new(rect.Width, rect.Height, false);
			SKCanvas canvas = new(result);
			canvas.DrawBitmap(image, rect, new SKRectI(0, 0, rect.Width, rect.Height), null);
			return result;
		}
		public static SKBitmap OutLine(this SKBitmap image)
		{
			SKBitmap img = image.Copy();
			checked
			{
				int num = img.Width - 1;
				for (int x = 0; x <= num; x++)
				{
					int num2 = img.Height - 1;
					for (int y = 0; y <= num2; y++)
					{
						if (image.GetPixel(x, y).Alpha == 0
							&& (image.GetPixel(Math.Max(0, x - 1), y).Alpha == byte.MaxValue
							|| image.GetPixel(Math.Min(x + 1, img.Width - 1), y).Alpha == byte.MaxValue
							|| image.GetPixel(x, Math.Max(0, y - 1)).Alpha == byte.MaxValue
							|| image.GetPixel(x, Math.Min(y + 1, img.Width - 1)).Alpha == byte.MaxValue))
							img.SetPixel(x, y, new SKColor(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
						else
							img.SetPixel(x, y, default);
					}
				}
				return img;
			}
		}
		public static SKBitmap OutGlow(this SKBitmap image)
		{
			SKImageFilter shadowFilter = SKImageFilter.CreateDropShadow(0f, 0f, 10f, 10f, SKColors.White);
			SKPaint paint = new()
			{
				ImageFilter = shadowFilter
			};
			SKBitmap OutGlow;
			using (SKBitmap output = new(image.Width, image.Height, false))
			{
				using (SKCanvas canvas = new(output))
					canvas.DrawBitmap(output, default(SKPoint), paint);
				OutGlow = output;
			}
			return OutGlow;
		}
	}
}
