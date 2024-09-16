using RhythmBase.Components;

namespace RhythmBase.Assets
{
	public interface ISpriteFile : IAssetFile
	{
		RDSizeNI Size { get; }
		static IAssetFile? Load(Type type, string path)
		{
			if (Path.GetExtension(path) == "")
				return SpriteFile.Load(path);
			else
				return ImageFile.Load(path);
		}
	}
}
