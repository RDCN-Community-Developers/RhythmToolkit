using Microsoft.VisualBasic.CompilerServices;

namespace RhythmBase.Extensions
{
	/// <summary>
	/// Image extensions.
	/// </summary>
	[StandardModule]
	public static class ImageExtension
	{
		/// <summary>
		/// Loads an image from the specified file path.
		/// </summary>
		/// <param name="path">The file path to load the image from.</param>
		/// <returns>The loaded <see cref="SKBitmap"/> image.</returns>
		public static SKBitmap FromFile(string path)
		{
			SKBitmap FromFile;
			using (FileStream stream = new FileInfo(path).OpenRead())
				FromFile = SKBitmap.Decode(stream);
			return FromFile;
		}

		/// <summary>
		/// Saves the image to the specified file path.
		/// </summary>
		/// <param name="image">The <see cref="SKBitmap"/> image to save.</param>
		/// <param name="path">The file path to save the image to.</param>
		public static void Save(this SKBitmap image, string path)
		{
			using FileStream stream = new FileInfo(path).OpenWrite();
			image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);
		}

		/// <summary>
		/// Tries to decode an image from the specified file path.
		/// </summary>
		/// <param name="image">The <see cref="SKBitmap"/> image to decode into.</param>
		/// <param name="path">The file path to decode the image from.</param>
		/// <returns><c>true</c> if the image was successfully decoded; otherwise, <c>false</c>.</returns>
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

		/// <summary>
		/// Copies a portion of the image defined by the specified rectangle.
		/// </summary>
		/// <param name="image">The <see cref="SKBitmap"/> image to copy from.</param>
		/// <param name="rect">The rectangle defining the portion of the image to copy.</param>
		/// <returns>A new <see cref="SKBitmap"/> containing the copied portion of the image.</returns>
		public static SKBitmap Copy(this SKBitmap image, SKRectI rect)
		{
			SKBitmap result = new(rect.Width, rect.Height, false);
			SKCanvas canvas = new(result);
			canvas.DrawBitmap(image, rect, new SKRectI(0, 0, rect.Width, rect.Height), null);
			return result;
		}

		/// <summary>
		/// Creates an outline of the image.
		/// </summary>
		/// <param name="image">The <see cref="SKBitmap"/> image to outline.</param>
		/// <returns>A new <see cref="SKBitmap"/> containing the outlined image.</returns>
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
							img.SetPixel(x, y, new RDColor(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
						else
							img.SetPixel(x, y, default);
					}
				}
				return img;
			}
		}

		/// <summary>
		/// Applies an outer glow effect to the image.
		/// </summary>
		/// <param name="image">The <see cref="SKBitmap"/> image to apply the glow effect to.</param>
		/// <returns>A new <see cref="SKBitmap"/> containing the image with the glow effect.</returns>
		public static SKBitmap OutGlow(this SKBitmap image)
		{
			SKImageFilter shadowFilter = SKImageFilter.CreateDropShadow(0f, 0f, 10f, 10f, RDColors.White);
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
