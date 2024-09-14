using System;
using RhythmBase.Components;

namespace RhythmBase.Assets
{

	public interface ISpriteFile : IAssetFile
	{

		RDSizeNI Size { get; }
	}
}
