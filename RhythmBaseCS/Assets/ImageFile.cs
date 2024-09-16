using RhythmBase.Components;
using RhythmBase.Extensions;
using SkiaSharp;
using System.Diagnostics.CodeAnalysis;

namespace RhythmBase.Assets
{
	public class ImageFile : ISpriteFile
	{
		public SKBitmap Image { get; set; }
		public RDSizeNI Size { get; private set; }
		public string FilePath { get; private set; }
		/// <summary>
		/// Load the file contents into memory.
		/// </summary>
		public static IAssetFile? Load([NotNull] string path)
		{
			ImageFile image = new();
			if (!Path.Exists(path))
				throw new FileNotFoundException("Cannot find the image file.", path);
			image.FilePath = path;
			SKBitmap imgFile = SKBitmap.Decode(path);
			if (imgFile == null)
				return null;//throw new FileNotFoundException("Illegal image file.", path);
			image.Image = imgFile;
			image.Size = new RDSizeNI(imgFile.Width, imgFile.Height);
			return image;
		}
		public void Save() => Image.Save(FilePath);
		public override string ToString() => Path.GetFileName(FilePath);
	}
}
