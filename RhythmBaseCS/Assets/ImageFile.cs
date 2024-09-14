using System;
using System.IO;
using RhythmBase.Components;
using SkiaSharp;

namespace RhythmBase.Assets
{

	public class ImageFile(string filename) : ISpriteFile
	{

		public SKBitmap Image { get; set; }


		public RDSizeNI Size { get; }


		public string Name
		{
			get
			{
				return _filename;
			}
		}

		/// <summary>
		/// Load the file contents into memory.
		/// </summary>

		public void Load(string directory)
		{
			string path = Path.Combine(directory, Name);
			bool flag = Path.Exists(path);
			if (flag)
			{
				try
				{
					SKBitmap imgFile = SKBitmap.Decode(path);
					Image = imgFile;
				}
				catch (Exception ex)
				{
				}
				return;
			}
			throw new FileNotFoundException("Cannot find the image file", path);
		}


		private readonly string _filename = filename;
	}
}
