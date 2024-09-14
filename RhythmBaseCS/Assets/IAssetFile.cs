using System;

namespace RhythmBase.Assets
{

	public interface IAssetFile
	{

		string Name { get; }


		void Load(string directory);
	}
}
